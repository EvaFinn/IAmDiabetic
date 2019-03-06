using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string Level { get; set; }
        public string Points { get; set; }

        public DashboardViewModel(User user)
        {
            User = user;
            Welcome = "Welcome " + User.FirstName + "!";
            DailyStreak = "Daily Streak : " + User.DailyStreak.ToString();
            DailyStreakCheck();
            HasLogs();
            Level = "Level : " + User.Level;
            Points = "XP\t: " + User.Score;
        }

        public void HasLogs()
        {
            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            if (logs.Count() < 1)
            {
                Test = "No logs made yet";
            }
            else
            {
                List<Log> todaysLogs = new List<Log>();

                foreach (Log log in logs)
                {
                    if (log.LogDate.Day.Equals(DateTimeOffset.Now.Day))
                    {
                        Debug.WriteLine("*********** LOGS " + log.BloodGlucose);
                        todaysLogs.Add(log);
                    }
                }
                Test = "Logs made today : " + todaysLogs.Count;
            }
        }

        public void DailyStreakCheck()
        {
            TimeSpan dif = DateTimeOffset.Now - User.LastLogInDate;
            //TODO handle going from 31st of month to 1st of next month? -- maybe fixed? must test lol.
            if (DateTimeOffset.Now.Day >= User.LastLogInDate.Day + 1)
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
            else
            {
                if (DateTimeOffset.Now.Day == 1 && (User.LastLogInDate.Day == 31 || User.LastLogInDate.Day == 30 || (User.LastLogInDate.Day == 28 && User.LastLogInDate.Month == 2)))
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
