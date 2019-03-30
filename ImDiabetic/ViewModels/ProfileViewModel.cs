using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public string Info { get; set; }
        public byte[] Photo { get; set; }
        public string Pet { get; set; }
        public List<TopicTargetData> Data { get; set; } 
        public List<string> ListOfAchievements { get; set; } 
        public ObservableCollection<Achievement> As { get; set; }

        public ProfileViewModel(AppUser user)
        {
            User = user;

            UserName = User.FirstName;
            Info = User.Age + ", " + User.Gender;
            Photo = User.ProfilePicture;
            Pet = User.Pet;
            Data = new List<TopicTargetData>();
            ListOfAchievements = new List<string>();
            As = new ObservableCollection<Achievement>();
            TopicChartInit();
            GetAchievements();
        }

        public void UpdatePet(string pet) {
            realm.Write(() =>
            {
                User.Pet = pet;
            });
        }

        public void GetAchievements() {
            var achievements = realm.All<Achievement>().Where(a => a.UserId == User.Id);
            if(achievements.Count() > 0) { 
                foreach(Achievement a in achievements) {
                    ListOfAchievements.Add(a.Name);
                    As.Add(a);
                }
            }
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
                if (q.Score == "6")
                {
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

        public void ReadPhoto(Stream input)
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
