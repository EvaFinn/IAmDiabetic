using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
        public int LastBloogGlucoseLog { get; set; }
        public int LastActivityLog { get; set; }


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
                LastActivityLog = 0;
            }
            else
            {
                List<Log> BGList = new List<Log>();
                List<Log> AList = new List<Log>();

                foreach (Log log in logs)
                {
                    if (log.Type == "Blood Glucose")
                    {
                        BGList.Add(log);
                    } 
                    if(log.Type == "Activity") {
                        AList.Add(log);
                    }

                    if (BGList.Count > 0)
                    {
                        LastBloogGlucoseLog = int.Parse(BGList.Last().Amount);
                    }

                    if (AList.Count > 0)
                    {
                        LastActivityLog = int.Parse(AList.Last().Amount);
                    }
                }
            }
        }

        public void AddLog()
        {
            realm.Write(() =>
            {
                Log log = new Log { UserId = User.Id, LogDate = DateTime.Now, Type = LogType, Amount = Amount, Calorie = Calorie };
                realm.Add(log);
            });
        }
    }
}
