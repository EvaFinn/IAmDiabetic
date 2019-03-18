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
        public AppUser User { get; set; }
        public string Welcome { get; set; }
        public string Test { get; set; }
        public string DailyStreak { get; set; }
        public string Level { get; set; }
        public string Points { get; set; }
        public string FoodText { get; set; }
        public int CurrentLevel { get; set; }
        public int DailyTotalCarbs { get; set; }
        public int TotalCal { get; set; }
        public string DisplayOne { get; set; }
        public string DisplayTwo { get; set; }

        public DashboardViewModel(AppUser user)
        {
            User = user;
            Welcome = "Welcome " + User.FirstName + "!";
            DailyStreak = User.DailyStreak.ToString();
            Points = User.Score.ToString();
            DailyStreakCheck();
            HasLogs();
            CurrentLevel = User.Level;
            LevelStuff();
            Level = CurrentLevel.ToString();
            FoodText = "Getting hungry, eat soon";
            AchievementsViewModel vm = new AchievementsViewModel(User);
            vm.CheckAchievements();
        }

        public void LevelStuff()
        {
            int pointsRequiredToLevelUp = 25 * CurrentLevel * 2;
            if (int.Parse(Points) >= pointsRequiredToLevelUp)
            {
                realm.Write(() =>
                {
                    User.Score = 0;
                    User.Level++;
                });
                Debug.WriteLine("******** Points " + Points);
                Debug.WriteLine("******** Level" + Level);
                Debug.WriteLine("******** points needed " + pointsRequiredToLevelUp);
            }
        }

        public void HasLogs()
        {
            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            DailyTotalCarbs = 0;
            TotalCal = 0;
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
                        Debug.WriteLine("*********** LOGS " + log.Amount);
                        todaysLogs.Add(log);
                        if (log.Type == "Food Item")
                        {
                            DailyTotalCarbs = DailyTotalCarbs + int.Parse(log.Amount);

                            if (log.Calorie != null)
                            {
                                TotalCal = TotalCal + int.Parse(log.Calorie);
                            }
                        }
                    }
                }
                Test = "Logs made today : " + todaysLogs.Count;
            }
            DisplayOne = "Total Carbs Today : " + DailyTotalCarbs;
            DisplayTwo = "Total Calories : " + TotalCal;
        }

        public void DailyStreakCheck()
        {
            TimeSpan dif = DateTimeOffset.Now - User.LastLogInDate;
            if (DateTimeOffset.Now.Day >= User.LastLogInDate.Day + 1)
            {
                CheckHours(dif);
            }
            else
            {
                if (DateTimeOffset.Now.Day == 1 && (User.LastLogInDate.Day == 31 || User.LastLogInDate.Day == 30 || (User.LastLogInDate.Day == 28 && User.LastLogInDate.Month == 2)))
                {
                    CheckHours(dif);
                }
            }

            realm.Write(() =>
            {
                User.LastLogInDate = DateTimeOffset.Now;
            });
        }

        private void CheckHours(TimeSpan dif)
        {
            if (dif.TotalHours > 24)
            {
                realm.Write(() =>
                {
                    User.DailyStreak = 0;
                });
            }
            else
            {
                realm.Write(() =>
                {
                    User.DailyStreak++;
                });
            }
        }
    }
}
