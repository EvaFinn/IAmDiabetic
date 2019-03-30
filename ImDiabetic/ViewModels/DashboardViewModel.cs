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
        public string DailyStreak { get; set; }
        public string Level { get; set; }
        public string DisplayPoints { get; set; }
        public int PointsNeeded { get; set; }
        public int CurrentLevel { get; set; }
        public int Points { get; set; }
        public string FoodText { get; set; }

        public DashboardViewModel(AppUser user)
        {
            User = user;
            FoodText = "All good!";
            DailyStreakCheck();
            CheckFoodLogs();
            Welcome = "Welcome " + User.FirstName + "!";
            DailyStreak = User.DailyStreak.ToString();
            AchievementsViewModel vm = new AchievementsViewModel(User);
            vm.CheckAchievements();
            CurrentLevel = User.Level;
            PointsNeeded = 25 * CurrentLevel * 2;
            Points = User.Score;
            DisplayPoints = Points + "/" + PointsNeeded;
            LevelUpCheck();
            Level = User.Level.ToString();
        }

        public void LevelUpCheck()
        {
            if (Points >= PointsNeeded)
            {
                realm.Write(() =>
                {
                    User.Score = 0;
                    User.Level++;
                });
                ShareLevelUp();
            }
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

        public void CheckFoodLogs()
        {
            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            List<Log> foodLogs = new List<Log>();
            if (logs.Count() > 0 )
            {
                foreach (Log log in logs)
                {
                    if(log.Type == "Food Item")
                    foodLogs.Add(log);
                }
                if (foodLogs.Count > 0)
                {
                    Log lastLog = foodLogs.Last();
                    TimeSpan timeSpan = DateTimeOffset.Now.Subtract(lastLog.LogDate);

                    if (timeSpan.TotalHours >= 4)
                    {
                        CrossLocalNotifications.Current.Show("Remember to eat!", "You should probably eat soon...", 1);
                        FoodText = "Getting hungry, eat soon";
                    }
                }
            }
        }

        public void DailyStreakCheck()
        {
            TimeSpan dif = DateTimeOffset.Now - User.LastLogInDate;
            if (DateTimeOffset.Now.Day >= User.LastLogInDate.Day + 1)
            {
                CheckHours(dif);
            }
            else if (DateTimeOffset.Now.Day == 1 && (User.LastLogInDate.Day == 31 || User.LastLogInDate.Day == 30 || (User.LastLogInDate.Day == 28 && User.LastLogInDate.Month == 2)))
            {
                CheckHours(dif);
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
