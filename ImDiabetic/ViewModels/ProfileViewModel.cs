using System;
namespace ImDiabetic.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public string UserName { get; set; }
        public string DailyStreak { get; set; }
        public string Level { get; set; }
        public string Points { get; set; }
        public string Info { get; set; }
        public ProfileViewModel(User user)
        {
            User = user;

            UserName = User.FirstName;
            DailyStreak = User.DailyStreak.ToString();
            Level = User.Level.ToString();
            Points = User.Score.ToString();
            Info = User.Age + ", " + User.Gender;
        }
    }
}
