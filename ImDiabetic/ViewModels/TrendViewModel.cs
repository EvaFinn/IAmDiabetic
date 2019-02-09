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

            CreateChart();
        }

        public void CreateChart() {
            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            List<Entry> entries = new List<Entry>();
            if (logs.Count() > 0)
            {
                foreach (Log log in logs)
                {
                    string colourToShow = CheckColour(log);
                    Entry entry = new Entry(float.Parse(log.BloodGlucose))
                    {
                        Label = log.LogDate.Day.ToString() + "/" + log.LogDate.Month.ToString() + "/" + log.LogDate.Year.ToString(),
                        ValueLabel = log.BloodGlucose,
                        Color = SKColor.Parse(colourToShow)
                    };

                    entries.Add(entry);
                }
            }

            this.Chart = new LineChart()
            {
                Entries = entries,
                LineMode = LineMode.Straight,
                LineSize = 8,
                PointMode = PointMode.Square,
                PointSize = 18,
                MaxValue = 500,
                MinValue = 0,
                LabelTextSize = 20,
            };

        }

        private string CheckColour(Log log) {
            float colour = float.Parse(log.BloodGlucose);
            //TODO 120 - 140 target, hardcoded, will change to user specified targets
            if(colour < 120) {
                return "#002ED1";
            }
            else if(colour > 140) {
                return "#D10B00";
            }
            else {
                return "#07D100";
            }
        }
    }
}
