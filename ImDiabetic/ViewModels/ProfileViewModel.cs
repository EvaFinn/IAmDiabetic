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
        public string Pet { get; set; }
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
            Pet = User.Pet;
            TopicChartInit();
        }

        public void UpdatePet(string pet) {
            realm.Write(() =>
            {
                User.Pet = pet;
            });
        }

        private void TopicChartInit()
        {
            int manCount = 0;
            int bgCount = 0;
            int insCount = 0;
            int diaCount = 0;
            int nutCount = 0;
            var quiz = realm.All<Quiz>().Where(q => q.UserId == User.Id);
            foreach (Quiz q in quiz)
            {
                Debug.WriteLine("Topic : " + q.Topic);
                if (q.Score == "6")
                {
                    Debug.WriteLine("FULL SCORE!");
                    switch (q.Topic) {
                        case "Management":
                            manCount++;
                            break;
                        case "Blood Glucose":
                            bgCount++;
                            break;
                        case "Insulin":
                            insCount++;
                            break;
                        case "Diabetes":
                            diaCount++;
                            break;
                        case "Nutrition":
                            nutCount++;
                            break;
                        default:
                            break;
                    }
                }
            }
            Data.Add(new TopicTargetData { TopicCount = manCount, Topic = "MANAGEMENT" });
            Data.Add(new TopicTargetData { TopicCount = bgCount, Topic = "BLOOD GLUCOSE" });
            Data.Add(new TopicTargetData { TopicCount = insCount, Topic = "INSULIN" });
            Data.Add(new TopicTargetData { TopicCount = diaCount, Topic = "DIABETES" });
            Data.Add(new TopicTargetData { TopicCount = nutCount, Topic = "NUTRITION" });

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
