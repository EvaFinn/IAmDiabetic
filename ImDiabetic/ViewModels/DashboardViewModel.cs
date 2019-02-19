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
            DailyStreak = "Daily Streak : " + User.DailyStreak.ToString();
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
                    Debug.WriteLine("*********** LOGS " + log.Id + "** " + log.BloodGlucose + "** " + log.LogDate);
                }
                FirstLog = logs.FirstOrDefault();
                Test = FirstLog.Id + "," + FirstLog.BloodGlucose;
            }
        }

        public void DailyStreakCheck()
        {
            TimeSpan dif = DateTimeOffset.Now - User.LastLogInDate;
            //WARNING TODO will not handle going from 31st of month to 1st of next month, etc. FIX! -- update, maybe fix? must test lol.
            if (DateTimeOffset.Now.Day >= User.LastLogInDate.Day + 1) {
                if (dif.TotalHours > 24)
                {
                    realm.Write(() =>
                    {
                        User.DailyStreak = 0;
                    });
                    DailyStreak = "Daily Streak : " + User.DailyStreak.ToString();
                }
                else {
                    realm.Write(() =>
                    {
                        User.DailyStreak++;
                    });
                    DailyStreak = "Daily Streak : " + User.DailyStreak.ToString();
                }
            } else {
                if(DateTimeOffset.Now.Day == 1 && (User.LastLogInDate.Day == 31 || User.LastLogInDate.Day == 30 || (User.LastLogInDate.Day == 28 && User.LastLogInDate.Month == 2)))
                {
                    if (dif.TotalHours > 24)
                    {
                        realm.Write(() =>
                        {
                            User.DailyStreak = 0;
                        });
                        DailyStreak = "Daily Streak : " + User.DailyStreak.ToString();
                    }
                    else
                    {
                        realm.Write(() =>
                        {
                            User.DailyStreak++;
                        });
                        DailyStreak = "Daily Streak : " + User.DailyStreak.ToString();
                    }
                }
            }

            realm.Write(() =>
            {
                User.LastLogInDate = DateTimeOffset.Now;
            });
        }
    }
}
