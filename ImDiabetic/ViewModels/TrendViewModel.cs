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
        public List<BGTargetData> BGChartData { get; set; }
        public int DailyTotalCarbs { get; set; }
        public int TotalCal { get; set; }
        public string DisplayOne { get; set; }
        public string DisplayTwo { get; set; }

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
                    if (log.Type == "Blood Glucose")
                    {
                        ListOfLogs.Add(log);
                    }
                    if(log.Type == "Food Item") {
                        ListOfFoodLogs.Add(log);
                        if (log.Calorie != null)
                        {
                            TotalCal = TotalCal + int.Parse(log.Calorie);
                        }
                        if (log.LogDate.Day == DateTimeOffset.Now.Day) {
                            DailyTotalCarbs = DailyTotalCarbs + int.Parse(log.Amount);
                        }
                    }
                }
            }
            DisplayOne = "Total Carbs Today : " + DailyTotalCarbs;
            DisplayTwo = "Total Calories : " + TotalCal;
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
                    Debug.WriteLine("*********" + log.Amount);
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
    }

    public class BGTargetData
    {
        public int BGTargetCounts { get; set; }
        public string LevelType { get; set; }
    }
}
