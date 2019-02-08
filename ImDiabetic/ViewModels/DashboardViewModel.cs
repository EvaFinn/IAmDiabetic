using System;
using System.Diagnostics;
using System.Linq;
using ImDiabetic.Models;
using Realms;

namespace ImDiabetic.ViewModels
{
    public class DashboardViewModel
    {
        Realm realm;
        public User User { get; set; }
        public string Welcome { get; set; }
        public string Test { get; set; }
        public Log FirstLog { get; set; }

        public DashboardViewModel(User user)
        {
            var config = new RealmConfiguration() { SchemaVersion = 2 };
            realm = Realm.GetInstance(config);
            User = user;
            Welcome = "Welcome " + User.FirstName + "!";
            HasLogs();
        }

        public void HasLogs() {
            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            if (logs.Count() < 1)
            {
                Test = "No logs made today";
            }
            else
            {
                foreach (Log log in logs)
                {
                    Debug.WriteLine("*********** LOGS " + log.Id + ", " + log.BloodGlucose + ", " + log.LogDate);
                }
                FirstLog = logs.FirstOrDefault();
                Test = FirstLog.Id + "," + FirstLog.BloodGlucose;
            }
        }
    }
}
