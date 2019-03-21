using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        public List<TopicTargetData> Data { get; set; } = new List<TopicTargetData>();

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
            TopicChartInit();
        }

        private void TopicChartInit()
        {
            int manCount = 0;
            int bgCount = 0;
            var quiz = realm.All<Quiz>().Where(q => q.UserId == User.Id);
            foreach (Quiz q in quiz)
            {
                Debug.WriteLine("Topic : " + q.Topic);
                if (q.Score == "3")
                {
                    Debug.WriteLine("FULL SCORE!");
                    switch (q.Topic) {
                        case "Management":
                            manCount++;
                            break;
                        case "Blood Glucose":
                            bgCount++;
                            break;
                        default:
                            break;
                    }
                }
            }
            Data.Add(new TopicTargetData { TopicCount = manCount, Topic = "MANAGEMENT" });
            Data.Add(new TopicTargetData { TopicCount = bgCount, Topic = "BLOOD GLUCOSE" });
            Data.Add(new TopicTargetData { TopicCount = 2, Topic = "INSULIN" });
            Data.Add(new TopicTargetData { TopicCount = 3, Topic = "DIABETES TYPE" });
            Data.Add(new TopicTargetData { TopicCount = 5, Topic = "NUTRITION" });

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
            }
        }
    }

    public class TopicTargetData
    {
        public int TopicCount { get; set; }
        public string Topic { get; set; }
    }
}
