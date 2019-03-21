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
    public class AchievementsViewModel : BaseViewModel
    {
        public AppUser User { get; set; }
        public string Achieve { get; set; }
        public Achievement Achievement { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Points { get; set; }
        public string Date { get; set; }
        //public bool IsAwarded { get; set; } = false;

        public AchievementsViewModel(AppUser user, string achievement)
        {
            User = user;
            Achieve = achievement;
            LoadAchievements();
            //realm achievements?
            if (Achievement != null)
            {
                Name = Achievement.Name;
                Description = Achievement.Description;
                Points = Achievement.PointsAwarded;
                Date = Achievement.AchievedDate.Date.ToString();
            }
        }

        public AchievementsViewModel(AppUser user)
        {
            User = user;
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

            //Achievement = new Achievement
            //{
            Name = achievements.ElementAt(0).Name;
            Description = achievements.ElementAt(0).Description;
            Points = "Points awarded : " + achievements.ElementAt(0).PointsAwarded;
            Date = DateTimeOffset.Now.Date.ToString();
            //};
        }

        //AchievementManager class?
        public void CheckAchievements()
        {
            CheckLoggerAchievements();
            CheckLoggerAchievementsTwo();

            CheckDailyStreak(3);
            CheckDailyStreak(7);
            CheckDailyStreak(14);
        }

        private void CheckLoggerAchievements()
        {
            //if isachieved == false
            //int sevendaycheck = 0;
            //int fourteendaycheck = 0;
            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            if (logs.Count() > 0)
            {
                List<Log> todaysLogs = new List<Log>();
                //TimeSpan = DateTimeOffset.Now.Subtract((DateTimeOffset.Now.Day - 7));
                DateTimeOffset date = DateTimeOffset.Now;
                Debug.WriteLine("*** date: " + date);
                DateTimeOffset newdate = date.AddDays(-1);
                Debug.WriteLine("$$$$ new date : " + newdate);


                foreach (Log log in logs)
                {
                    if (log.LogDate.Day.Equals(DateTimeOffset.Now.Day))
                    {
                        todaysLogs.Add(log);
                    }
                }
                if (todaysLogs.Count == 3)
                {
                    Debug.WriteLine("omg won award!!! ");
                }
                //set is achieved to true
            }
        }

        private void CheckLoggerAchievementsTwo()
        {
            //if isachieved == false
            int sevendaycheck = 0;
            //int fourteendaycheck = 0;
            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            if (logs.Count() > 0)
            {
                List<Log> todaysLogs = new List<Log>();
                DateTimeOffset date = DateTimeOffset.Now;

                for (int i = 0; i < 7; i++)
                {
                    List<Log> someLogs = new List<Log>();
                    foreach (Log log in logs)
                    {
                        if (log.LogDate.Day.Equals(date.Day - i))
                        {
                            Debug.WriteLine("$$$$$$$++++ datetest thing  " + (date.Day - i) + ",,,,,  " + i);

                            someLogs.Add(log);
                        }
                    }
                    if (someLogs.Count == 3)
                    {
                        Debug.WriteLine("$$$$$$$ had 3 today!");

                        //Debug.WriteLine("omg won award!!! ");
                        sevendaycheck++;
                    }
                }
                Debug.WriteLine("$$$$$$$ seven day check is: " + sevendaycheck);

                //set is achieved to true
            }
        }

        private void CheckDailyStreak(int checkDays)
        {
            if (User.DailyStreak == checkDays)
            {
                //if is won equals false
                Debug.WriteLine("omg won award!!! streak is {0} long", checkDays);
                //checkdays == 7 -> 7 days win set to true?
                switch (checkDays) {
                    case 3:
                        //3 day win = true
                        break;
                    case 7:
                        //7 day win = true
                        break;
                    case 14:
                        //14 day win = true
                        break;
                }
            }
        }
    }
}
