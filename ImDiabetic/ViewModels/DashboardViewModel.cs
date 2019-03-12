using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using ImDiabetic.Models;
using Xamarin.Forms;

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
        public string FoodText { get; set; }
        public int LevelOne { get; set; } = 10;
        public int LevelTwo { get; set; } = 20;
        public int LevelThree { get; set; } = 30;
        public int CurrentLevel { get; set; }
        public int NextLevel { get; set; }

        public DashboardViewModel(User user)
        {
            User = user;
            Welcome = "Welcome " + User.FirstName + "!";
            DailyStreak = User.DailyStreak.ToString();
            Points = User.Score.ToString();
            DailyStreakCheck();
            //LevelStuff();
            HasLogs();
            CurrentLevel = User.Level;
            NextLevel = CurrentLevel + 1;
            Level = CurrentLevel.ToString();
            FoodText = "Getting hungry, eat soon";
        }

        //public void LevelStuff()
        //{
        //    if (int.Parse(Points) >= LevelThree)
        //    {
        //        realm.Write(() =>
        //        {
        //            User.Level = 0;
        //            User.Score = 0;
        //            //User.Level++;
        //        });
        //    }
        //    else if (int.Parse(Points) >= LevelTwo)
        //    {
        //        realm.Write(() =>
        //        {
        //            //User.Level++;
        //            User.Level = 0;
        //            User.Score = 0;
        //        });
        //    }
        //    else if (int.Parse(Points) >= LevelOne)
        //    {
        //        realm.Write(() =>
        //        {
        //            //User.Level++;
        //            User.Level = 0;
        //            User.Score = 0;
        //        });
        //    }
        //}

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
            if (DateTimeOffset.Now.Day >= User.LastLogInDate.Day + 1)
            {
                if (dif.TotalHours > 24)
                {
                    realm.Write(() =>
                    {
                        User.DailyStreak = 0;
                    });
                    //DailyStreak = User.DailyStreak.ToString();
                }
                else
                {
                    realm.Write(() =>
                    {
                        User.DailyStreak++;
                    });
                    //DailyStreak = User.DailyStreak.ToString();
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
                        //DailyStreak = User.DailyStreak.ToString();
                    }
                    else
                    {
                        realm.Write(() =>
                        {
                            User.DailyStreak++;
                        });
                        //DailyStreak = User.DailyStreak.ToString();
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
