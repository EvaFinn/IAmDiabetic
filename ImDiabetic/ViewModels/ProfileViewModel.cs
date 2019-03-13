using System;
using System.IO;
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
        public byte[] Photo { get; set; }

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
            Photo = User.ProfilePicture;
        }

        public void ReadFully(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                Photo = ms.ToArray();
                realm.Write(() =>
                {
                    User.ProfilePicture = Photo;
                });
                //return Photo;
            }
        }
    }
}
