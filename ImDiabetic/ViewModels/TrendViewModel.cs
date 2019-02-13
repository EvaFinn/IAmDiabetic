using System;
using SkiaSharp;
using Microcharts;
using Entry = Microcharts.Entry;
using System.Collections.Generic;
using ImDiabetic.Models;
using System.Linq;
using ImDiabetic.Models.Logbook;
using System.Diagnostics;
using Syncfusion.SfChart.XForms;

namespace ImDiabetic.ViewModels
{
    //enum BloodGlucoseLevelType { HIGH, NORMAL, LOW }
    public class TrendViewModel : BaseViewModel
    {
        public User User { get; set; }
        public Log Log { get; set; }
        public List<Log> ListOfLogs { get; set; }

        //public List<BloodGlucoseLevelType> BGTypes { get; set; }
        //public List<BGType> Data { get; set; }
        //public List<ChartDataPoint> Data2 { get; set; }
        public List<Test> Data3 { get; set; }


        public TrendViewModel(User user)
        {
            User = user;
            LogsForCharts();
            CheckTargetBG();
        }

        public void LogsForCharts() {
            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            //var lg = realm.All<BloodGlucoseLog>().Where(w => w.UserId == User.Id);
            ListOfLogs = new List<Log>();
            if (logs.Count() > 0)
            {
                foreach (Log log in logs)
                {
                    ListOfLogs.Add(log);
                }
            }

        }

        public void CheckTargetBG() {
            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            //List<BloodGlucoseLevelType> types = new List<BloodGlucoseLevelType>();
            //BGTypes = new List<BloodGlucoseLevelType>();
            //Data = new List<BGType>();
            //Data2 = new List<ChartDataPoint>();
            Data3 = new List<Test>();
            int highcount = 0;
            int lowcount = 0; 
            int normalcount = 0;

            foreach (Log log in logs) {
                //BloodGlucoseLevelType bglt;
                if (float.Parse(log.BloodGlucose) < float.Parse(User.TargetBloodGlucose)) {
                    //bglt = BloodGlucoseLevelType.LOW;
                    lowcount++;
                }
                else if (float.Parse(log.BloodGlucose) > float.Parse(User.TargetBloodGlucose)) {
                    //bglt = BloodGlucoseLevelType.HIGH;
                    highcount++;
                }
                else {
                    //bglt = BloodGlucoseLevelType.NORMAL;
                    normalcount++;
                }
                //types.Add(bglt);
                //BGTypes.Add(bglt);
                //Data.Add(new BGType { BGLevel = log.BloodGlucose, LevelType = Enum.GetName(typeof(BloodGlucoseLevelType), bglt)});
                //Data2.Add(new ChartDataPoint(log.BloodGlucose, (double)bglt));
                
            }
            Data3.Add(new Test { BDTargetCounts = lowcount, LevelType = "LOW"});
            Data3.Add(new Test { BDTargetCounts = normalcount, LevelType = "NORMAL" });
            Data3.Add(new Test { BDTargetCounts = highcount, LevelType = "HIGH" });
        }

    }

    public class BGType
    { 
        public string BGLevel { get; set; }
        public string LevelType { get; set; }
    }

    public class Test
    {
        public int BDTargetCounts { get; set; }
        public string LevelType { get; set; }
    }
}
