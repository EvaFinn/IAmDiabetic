using System;
using System.Collections.Generic;
using System.Linq;
using ImDiabetic.Models;
using ImDiabetic.Models.Logbook;

namespace ImDiabetic.ViewModels
{
    public class LogsViewModel : BaseViewModel
    {
        public AppUser User { get; set; }
        public string LogType { get; set; }
        public string Amount { get; set; }
        public string BloodGlucose { get; set; }
        public string Insulin { get; set; }
        public string Pills { get; set; }
        public string Activity { get; set; }
        public string Carbs { get; set; }
        public string Calorie { get; set; }
        public Log Log { get; set; }
        //private BloodGlucoseLog bloodGlucoseLog;
        //private InsulinLog insulinLog;
        //private MedicationLog medicationLog;
        //private FoodLog foodLog;
        //private ActivityLog activityLog;
        public int LastBloogGlucoseLog { get; set; }

        public LogsViewModel(AppUser user)
        {
            User = user;
            GetLastBGLog();
        }

        private void GetLastBGLog()
        {
            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            if (logs.Count() < 1)
            {
                LastBloogGlucoseLog = 0;
            }
            else
            {
                List<Log> BGList = new List<Log>();
                foreach (Log log in logs)
                {
                    if(log.Type == "Blood Glucose") {
                        BGList.Add(log);
                    }
                }
                //LastBloogGlucoseLog = int.Parse(logs.Last().BloodGlucose);
                LastBloogGlucoseLog = int.Parse(BGList.Last().Amount);
            }
        }

        public void AddLog()
        {
            realm.Write(() =>
            {
                //Log log = new Log { UserId = User.Id, LogDate = DateTime.Now, BloodGlucose = BloodGlucose, Insulin = Insulin, Pills = Pills, Carbs = Carbs, Activity = Activity };
                Log log = new Log { UserId = User.Id, LogDate = DateTime.Now, Type = LogType, Amount = Amount, Calorie = Calorie };
                realm.Add(log);
            });
        }
    }
}
