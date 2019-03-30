using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Acr.UserDialogs;
using ImDiabetic.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace ImDiabetic.ViewModels
{
    public class AchievementsViewModel : BaseViewModel
    {
        public AppUser User { get; set; }
        public string Achieve { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Points { get; set; }
        public string Date { get; set; }
        private List<Achievement> AllAchievements = new List<Achievement>();

        public AchievementsViewModel(AppUser user, string achievement)
        {
            User = user;
            Achieve = achievement;
            LoadAchievements();
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
            List<Achievement> ViewAchievementList = new List<Achievement>();

            for (int i = 0; i < jsonresult.Count; i++)
            {
                AllAchievements.Add(jsonresult[i]);
                if (jsonresult.ElementAt(i).Name.Equals(Achieve))
                {
                    ViewAchievementList.Add(jsonresult[i]);
                }
            }

            if (ViewAchievementList.Any())
            {
                Name = ViewAchievementList.ElementAt(0).Name;
                Description = ViewAchievementList.ElementAt(0).Description;
                Points = "You will receive " + ViewAchievementList.ElementAt(0).PointsAwarded + " points when completed";
            }

            var achieveDB = realm.All<Achievement>().Where(q => q.UserId == User.Id && q.Name == Achieve);
            if (achieveDB.Count() > 0)
            {
                GetAchievementsForChallenge(Achieve);
                foreach (Achievement a in achieveDB)
                {
                    Name = a.Name;
                    Description = a.Description;
                    Points = "You gained " + a.PointsAwarded + " points! Well done!";
                    Date = "Date achieved : " + a.AchievedDate.Date.ToString("d");
                }
            }
        }

        public void CheckAchievements()
        {
            CheckBloodGlucoseLoggerOneDay();
            CheckActivityLogAchievement();
            CheckQuizTakenAchievement();
            CheckDailyStreak(3);
            CheckDailyStreak(7);
            CheckDailyStreak(14);

        }

        private void CheckBloodGlucoseLoggerOneDay()
        {
            LoadAchievements();
            bool HaveIt = false;
            GetAchievementsForChallenge("Blood Glucose Logger");

            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            var achieves = realm.All<Achievement>().Where(a => a.UserId == User.Id);
            if (logs.Count() > 0)
            {
                List<Log> todaysLogs = new List<Log>();
                DateTimeOffset date = DateTimeOffset.Now;

                foreach (Log log in logs)
                {
                    if (log.LogDate.Day.Equals(DateTimeOffset.Now.Day) && log.Type == "Blood Glucose")
                    {
                        todaysLogs.Add(log);
                    }
                }

                foreach (Achievement a in achieves)
                {
                    HaveIt |= (a.Name == "Blood Glucose Logger" && a.IsAchieved == true);
                }

                if (todaysLogs.Count == 3 && !HaveIt)
                {
                    AddAchievement();
                }
            }
        }

        private void GetAchievementsForChallenge(string achievement)
        {
            for (int i = 0; i < AllAchievements.Count; i++)
            {
                if (AllAchievements.ElementAt(i).Name.Equals(achievement))
                {
                    Name = AllAchievements.ElementAt(i).Name;
                    Description = AllAchievements.ElementAt(i).Description;
                    Points = AllAchievements.ElementAt(i).PointsAwarded;
                    Date = DateTimeOffset.Now.Date.ToString();
                }
            }
        }

        async private void ShareAchievement()
        {
            bool share = await UserDialogs.Instance.ConfirmAsync("YOU HAVE WON THE " + Name + " CHALLENGE", "Congratulations", "Share", "OK");
            if (share == true)
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Text = "I just won the " + Name + " Challenge on my ImDiabetic App!",
                    Title = "Achievement"
                });
            }
        }

        private void CheckDailyStreak(int checkDays)
        {
            var achieves = realm.All<Achievement>().Where(a => a.UserId == User.Id);
            LoadAchievements();
            bool HaveIt = false;

            if (User.DailyStreak == checkDays)
            {
                Debug.WriteLine("omg won award!!! streak is {0} long", checkDays);

                switch (checkDays)
                {
                    case 3:
                        GetAchievementsForChallenge("3 Day Streak");
                        foreach (Achievement a in achieves)
                        {
                            HaveIt |= (a.Name == "3 Day Streak" && a.IsAchieved == true);
                        }
                        if (!HaveIt)
                        {
                            AddAchievement();
                        }
                        break;
                    case 7:
                        GetAchievementsForChallenge("7 Day Streak");
                        foreach (Achievement a in achieves)
                        {
                            HaveIt |= (a.Name == "7 Day Streak" && a.IsAchieved == true);
                        }
                        if (!HaveIt)
                        {
                            AddAchievement();
                        }
                        break;
                    case 14:
                        GetAchievementsForChallenge("14 Day Streak");
                        foreach (Achievement a in achieves)
                        {
                            HaveIt |= (a.Name == "14 Day Streak" && a.IsAchieved == true);
                        }
                        if (!HaveIt)
                        {
                            AddAchievement();
                        }
                        break;
                }
            }
        }

        private void CheckActivityLogAchievement()
        {
            LoadAchievements();
            bool HaveIt = false;
            int TotalActivity = 0;
            int GoalActivity = 30;
            GetAchievementsForChallenge("Active Life");

            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            var achieves = realm.All<Achievement>().Where(a => a.UserId == User.Id);
            if (logs.Count() > 0)
            {
                foreach (Log log in logs)
                {
                    if (log.LogDate.Day.Equals(DateTimeOffset.Now.Day) && log.Type == "Activity")
                    {
                        TotalActivity = TotalActivity + int.Parse(log.Amount);
                    }
                }

                foreach (Achievement a in achieves)
                {
                    HaveIt |= (a.Name == "Active Life" && a.IsAchieved == true);
                }

                if (TotalActivity >= GoalActivity && !HaveIt)
                {
                    AddAchievement();
                }
            }
        }

        private void CheckQuizTakenAchievement()
        {
            LoadAchievements();
            bool HaveIt = false;
            GetAchievementsForChallenge("Quiz Master");

            var quiz = realm.All<Quiz>().Where(q => q.UserId == User.Id);
            List<Quiz> quizList = new List<Quiz>();
            var achieves = realm.All<Achievement>().Where(a => a.UserId == User.Id);
            if (quiz.Count() > 0)
            {
                foreach (Quiz q in quiz)
                {
                    quizList.Add(q);
                }

                foreach (Achievement a in achieves)
                {
                    HaveIt |= (a.Name == "Quiz Master" && a.IsAchieved == true);
                }

                if (quizList.Count() == 1 && !HaveIt)
                {
                    AddAchievement();
                }
            }
        }

        private void AddAchievement()
        {
            realm.Write(() =>
            {
                var achievement3log = new Achievement
                {
                    UserId = User.Id,
                    Name = Name,
                    Description = Description,
                    IsAchieved = true,
                    AchievedDate = DateTimeOffset.Now,
                    PointsAwarded = Points
                };
                realm.Add(achievement3log);
                User.Score = User.Score + int.Parse(achievement3log.PointsAwarded);
            });
            ShareAchievement();
        }
    }
}
