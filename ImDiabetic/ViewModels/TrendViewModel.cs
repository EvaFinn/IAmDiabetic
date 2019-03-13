﻿using System;
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
        public List<Log> ListOfLogs { get; set; }
        public List<BGTargetData> BGChartData { get; set; }

        public TrendViewModel(AppUser user)
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
                    if (log.Type == "Blood Glucose")
                    {
                        ListOfLogs.Add(log);
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
