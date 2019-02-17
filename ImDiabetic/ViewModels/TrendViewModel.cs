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
        public User User { get; set; }
        public Log Log { get; set; }
        public List<Log> ListOfLogs { get; set; }
        public List<BGTargetData> Data3 { get; set; }

        public TrendViewModel(User user)
        {
            User = user;
            LogsForCharts();
            CheckTargetBG();
        }

        public void LogsForCharts()
        {
            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            ListOfLogs = new List<Log>();
            if (logs.Count() > 0)
            {
                foreach (Log log in logs)
                {
                    ListOfLogs.Add(log);
                }
            }
        }

        public void CheckTargetBG()
        {
            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            Data3 = new List<BGTargetData>();
            int highcount = 0;
            int lowcount = 0;
            int normalcount = 0;

            foreach (Log log in logs)
            {
                if (float.Parse(log.BloodGlucose) < User.MinTargetBloodGlucose)
                {
                    lowcount++;
                }
                else if (float.Parse(log.BloodGlucose) > User.MaxTargetBloodGlucose)
                {
                    highcount++;
                }
                else
                {
                    normalcount++;
                }
            }
            Data3.Add(new BGTargetData { BGTargetCounts = lowcount, LevelType = "LOW" });
            Data3.Add(new BGTargetData { BGTargetCounts = normalcount, LevelType = "NORMAL" });
            Data3.Add(new BGTargetData { BGTargetCounts = highcount, LevelType = "HIGH" });
        }
    }

    public class BGTargetData
    {
        public int BGTargetCounts { get; set; }
        public string LevelType { get; set; }
    }
}
