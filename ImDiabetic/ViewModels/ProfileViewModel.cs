using System;
using ImDiabetic.Models;

namespace ImDiabetic.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public AppUser User { get; set; }
        public string UserName { get; set; }
        public string DailyStreak { get; set; }
        public string Points { get; set; }
        public string DisplayPoints { get; set; }
        public string Info { get; set; }
        public int PointsNeeded { get; set; }
        public int CurrentLevel { get; set; }

        public ProfileViewModel(AppUser user)
        {
            User = user;

            UserName = User.FirstName;
            DailyStreak = User.DailyStreak.ToString();
            CurrentLevel = User.Level;
            PointsNeeded = 25 * CurrentLevel * (1 + CurrentLevel);
            Points = User.Score.ToString();
            DisplayPoints = Points + "/" + PointsNeeded;
            Info = User.Age + ", " + User.Gender;
        }
    }
}
