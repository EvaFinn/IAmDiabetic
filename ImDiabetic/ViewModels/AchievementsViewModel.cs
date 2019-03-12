using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using ImDiabetic.Models;
using Newtonsoft.Json;

namespace ImDiabetic.ViewModels
{
    public class AchievementsViewModel
    {
        public AppUser User { get; set; }
        public string Achieve { get; set; }
        public Achievement Achievement { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Points { get; set; }
        public DateTimeOffset Date { get; set; }
        //public bool IsAwarded { get; set; } = false;

        public AchievementsViewModel(AppUser user , string achievement)
        {
            User = user;
            Achieve = achievement;
            LoadAchievements();
            if (Achievement != null)
            {
                Name = Achievement.Name;
                Description = Achievement.Description;
                Points = Achievement.PointsAwarded;
                //Date = Achievement.AchievedDate;
            }
        }

        public void LoadAchievements()
        {
            List<Achievement> jsonresult;

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Achievement)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("ImDiabetic.achievements.json");
            string json = "";
            using (var reader = new System.IO.StreamReader(stream)) { json = reader.ReadToEnd(); }

            jsonresult = JsonConvert.DeserializeObject<List<Achievement>>(json);
            List<Achievement> achievements = new List<Achievement>();

            for (int i = 0; i < jsonresult.Count; i++)
            {
                if (jsonresult.ElementAt(i).Name.Equals(Achieve))
                {
                    achievements.Add(jsonresult[i]);
                }
            }

            Achievement = new Achievement
            {
                Name = achievements.ElementAt(0).Name,
                Description = achievements.ElementAt(0).Description,
                PointsAwarded = achievements.ElementAt(0).PointsAwarded,
                //AchievedDate = DateTimeOffset.Now
            };
        }
    }
}
