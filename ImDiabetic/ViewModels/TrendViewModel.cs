using System;
using SkiaSharp;
using Microcharts;
using Entry = Microcharts.Entry;
using System.Collections.Generic;
using ImDiabetic.Models;
using System.Linq;

namespace ImDiabetic.ViewModels
{
    public class TrendViewModel : BaseViewModel
    {
        public User User { get; set; }
        public Chart Chart { get; set; }
        public Log Log { get; set; }
        public List<Log> ListOfLogs { get; set; }

        public TrendViewModel(User user)
        {
            User = user;

            LogsForCharts();
        }

        public void LogsForCharts() {
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
    }
}
