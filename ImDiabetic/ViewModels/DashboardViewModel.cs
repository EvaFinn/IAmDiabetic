using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Acr.UserDialogs;
using ImDiabetic.Models;
using Plugin.LocalNotifications;
using Xamarin.Essentials;
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
        public string DisplayPoints { get; set; }
        public int PointsNeeded { get; set; }
        public int CurrentLevel { get; set; }
        public int Points { get; set; }
        public string FoodText { get; set; }
        public int DailyTotalCarbs { get; set; }
        public int TotalCal { get; set; }
        public string DisplayOne { get; set; }
        public string DisplayTwo { get; set; }
        public bool LevelUp { get; set; } = false;

        public DashboardViewModel(AppUser user)
        {
            User = user;
            FoodText = "All good!";
            DailyStreakCheck();
            HasLogs();
            Welcome = "Welcome " + User.FirstName + "!";
            DailyStreak = User.DailyStreak.ToString();
            AchievementsViewModel vm = new AchievementsViewModel(User);
            vm.CheckAchievements();
            CurrentLevel = User.Level;
            PointsNeeded = 25 * CurrentLevel * 2;
            Points = User.Score;
            DisplayPoints = Points + "/" + PointsNeeded;
            LevelUp = LevelStuff();
            Level = User.Level.ToString();
        }

        public bool LevelStuff()
        {
            //int pointsRequiredToLevelUp = 25 * CurrentLevel * 2;
            if (Points >= PointsNeeded)
            {
                realm.Write(() =>
                {
                    User.Score = 0;
                    User.Level++;
                });
                LevelUp = true;
                ShareLevelUp();

            } else { LevelUp = false; }

            return LevelUp;
        }

        async private void ShareLevelUp()
        {
            bool share = await UserDialogs.Instance.ConfirmAsync("YOU HAVE LEVELED UP", "CONGRATULATIONS", "Share", "OK");
            if (share == true)
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Text = "I just leveled up on my ImDiabetic app!",
                    Title = "Level Up"
                });
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
                        todaysLogs.Add(log);
                        if (log.Type == "Food Item")
                        {
                            DailyTotalCarbs = DailyTotalCarbs + int.Parse(log.Amount);
                            Debug.WriteLine("Log Hour : " + log.LogDate.Hour);
                            Debug.WriteLine("Now Hour : " + DateTimeOffset.Now.Hour);
                            TimeSpan timeSpan = DateTimeOffset.Now.Subtract(log.LogDate);


                            if (timeSpan.TotalHours >= 4) {

                                CrossLocalNotifications.Current.Show("Remember to eat!", "You should probably eat soon...", 1);
                                FoodText = "Getting hungry, eat soon";
                            }

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
