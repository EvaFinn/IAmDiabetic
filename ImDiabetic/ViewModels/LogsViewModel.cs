using System;
using System.Linq;
using ImDiabetic.Models;
using ImDiabetic.Models.Logbook;

namespace ImDiabetic.ViewModels
{
    public class LogsViewModel : BaseViewModel
    {
        public AppUser User { get; set; }
        public string BloodGlucose { get; set; }
        public string Insulin { get; set; }
        public string Pills { get; set; }
        public string Activity { get; set; }
        public string Carbs { get; set; }
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
                LastBloogGlucoseLog = int.Parse(logs.Last().BloodGlucose);
            }
        }

        public void AddLog()
        {
            //bloodGlucoseLog = new BloodGlucoseLog { UserId = User.Id, BloodGlucoseLevel = BloodGlucose, LogDate = DateTimeOffset.Now };
            //insulinLog = new InsulinLog { UserId = User.Id, LogDate = DateTimeOffset.Now, InsulinAmount = Insulin};
            //medicationLog = new MedicationLog { UserId = User.Id, LogDate = DateTimeOffset.Now, MedsTaken = Pills};
            //foodLog = new FoodLog { UserId = User.Id, Carbohydrates = Carbs, LogDate = DateTimeOffset.Now};
            //activityLog = new ActivityLog { UserId = User.Id, LogDate = DateTimeOffset.Now, Activity = Activity};

            realm.Write(() =>
            {
                Log log = new Log { UserId = User.Id, LogDate = DateTime.Now, BloodGlucose = BloodGlucose, Insulin = Insulin, Pills = Pills, Carbs = Carbs, Activity = Activity };
                realm.Add(log);
            });
        }
    }
}
