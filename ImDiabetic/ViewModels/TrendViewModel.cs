using System;
using System.Collections.Generic;
using ImDiabetic.Models;
using System.Linq;
using ImDiabetic.Models.Logbook;
using System.Diagnostics;
using Syncfusion.SfChart.XForms;

namespace ImDiabetic.ViewModels
{

    public class TrendViewModel : BaseViewModel
    {
        public AppUser User { get; set; }
        public List<Log> ListOfLogs { get; set; } = new List<Log>();
        public List<Log> ListOfFoodLogs { get; set; } = new List<Log>();
        public List<Log> ListOfActivity { get; set; } = new List<Log>();
        public List<BGTargetData> BGChartData { get; set; }
        public string Average { get; set; }
        public string Highest { get; set; }
        public string Lowest { get; set; }
        public int DailyTotalCarbs { get; set; }
        public int TotalCal { get; set; }
        public string RecCals { get; set; }
        public int TotalMins { get; set; }

        public TrendViewModel(AppUser user)
        {
            User = user;
            LogsForCharts();
            CheckTargetBG();
        }

        public void LogsForCharts()
        {
            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            DailyTotalCarbs = 0;
            TotalCal = 0;
            if (logs.Count() > 0)
            {
                foreach (Log log in logs)
                {
                    switch (log.Type)
                    {
                        case "Blood Glucose":
                            List<int> Amounts = new List<int>();
                            ListOfLogs.Add(log);
                            foreach(Log l in ListOfLogs) {
                                Amounts.Add(int.Parse(l.Amount));
                            }
                            Average = Amounts.Average().ToString("F1");
                            Highest = Amounts.Max().ToString();
                            Lowest = Amounts.Min().ToString();
                            break;
                        case "Food Item":
                            ListOfFoodLogs.Add(log);
                            if (log.Calorie != null)
                            {
                                TotalCal = TotalCal + int.Parse(log.Calorie);
                            }
                            if (log.LogDate.Day == DateTimeOffset.Now.Day)
                            {
                                DailyTotalCarbs = DailyTotalCarbs + int.Parse(log.Amount);
                            }
                            CalorieCal();
                            break;
                        case "Activity":
                            ListOfActivity.Add(log);
                            if (log.LogDate.Day == DateTimeOffset.Now.Day)
                            {
                                TotalMins = TotalMins + int.Parse(log.Amount);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void CheckTargetBG()
        {
            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            BGChartData = new List<BGTargetData>();
            int highcount = 0;
            int lowcount = 0;
            int normalcount = 0;

            foreach (Log log in logs)
            {
                if (log.Type == "Blood Glucose")
                {
                    if (float.Parse(log.Amount) < User.MinTargetBloodGlucose)
                    {
                        lowcount++;
                    }
                    else if (float.Parse(log.Amount) > User.MaxTargetBloodGlucose)
                    {
                        highcount++;
                    }
                    else
                    {
                        normalcount++;
                    }
                }
            }
            BGChartData.Add(new BGTargetData { BGTargetCounts = lowcount, LevelType = "LOW" });
            BGChartData.Add(new BGTargetData { BGTargetCounts = normalcount, LevelType = "NORMAL" });
            BGChartData.Add(new BGTargetData { BGTargetCounts = highcount, LevelType = "HIGH" });
        }

        private void CalorieCal() {
            double recCalories;
            //TODO hardcoded values here!
            if (User.Gender == "Female")
            {
                recCalories = (665 + (4.3 * 140) + (4.7 * 65) - (4.7 * int.Parse(User.Age))) * 1.55;
            }
            else
            {
                recCalories = (66 + (6.3 * 140) + (12.9 * 65) - (6.8 * int.Parse(User.Age))) * 1.55;
            }
            string Cal = recCalories.ToString("F0");
            RecCals = TotalCal + "/" + Cal;
        }
    }

    public class BGTargetData
    {
        public int BGTargetCounts { get; set; }
        public string LevelType { get; set; }
    }
}
