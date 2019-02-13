using System;
using System.Diagnostics;
using System.Linq;
using ImDiabetic.Models;

namespace ImDiabetic.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        public User User { get; set; }
        public string Welcome { get; set; }
        public string Test { get; set; }
        public string DailyStreak { get; set; }
        public Log FirstLog { get; set; }

        public DashboardViewModel(User user)
        {
            User = user;
            Welcome = "Welcome " + User.FirstName + "!";
            DailyStreakCheck();
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
                DailyStreak = "Daily Streak : " + User.DailyStreak.ToString();
            }
        }

        public void DailyStreakCheck()
        {
            //LoggedInUser.LastLogInDate = DateTimeOffset.Now;
            TimeSpan dif = DateTimeOffset.Now - User.LastLogInDate;
            Debug.WriteLine("=========== User Last LogIn " + User.LastLogInDate);
            Debug.WriteLine(">>>>>>>>>>> Date Difference " + dif.TotalDays + ", " + dif.TotalHours + ", " + dif.TotalMinutes);
            //if (dif.TotalDays == 1) { 
            //}
            if (dif.TotalHours > 24)
            {
                //go back to zero
                Debug.WriteLine(">>>>>>>>>>> Back To Zero");
                User.DailyStreak = 0;
            }

            realm.Write(() =>
            {
                User.LastLogInDate = DateTimeOffset.Now;
            });

            //if (user.LastLogInDate.Day > DateTimeOffset.Now.Day) { 
            //}
        }
    }
}
