using System;
using ImDiabetic.Models;

namespace ImDiabetic.ViewModels
{
    public class LogsViewModel : BaseViewModel
    {
        public User User { get; set; }
        public string BloodGlucose { get; set; }
        public string Insulin { get; set; }
        public string Pills { get; set; }
        public string Activity { get; set; }
        public string Carbs { get; set; }
        public Log Log { get; set; }

        public LogsViewModel(User user)
        {
            User = user;
        }

        public void AddLog()
        {
            realm.Write(() =>
            {
                Log log = new Log { UserId = User.Id, LogDate = DateTime.Now, BloodGlucose = BloodGlucose, Insulin = Insulin, Pills = Pills, Carbs = Carbs, Activity = Activity };
                realm.Add(log);
            });
        }
    }
}
