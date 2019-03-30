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
        private readonly IQueryable<Achievement> MyAchievements;

        public AchievementsViewModel(AppUser user, string achievement)
        {
            User = user;
            Achieve = achievement;
            LoadAchievements();
            MyAchievements = realm.All<Achievement>().Where(a => a.UserId == User.Id);
        }

        public AchievementsViewModel(AppUser user)
        {
            User = user;
            MyAchievements = realm.All<Achievement>().Where(a => a.UserId == User.Id);
        }

        private void LoadAchievements()
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
            if (logs.Count() > 0)
            {
                List<Log> todaysLogs = new List<Log>();
                foreach (Log log in logs)
                {
                    if (log.LogDate.Day.Equals(DateTimeOffset.Now.Day) && log.Type == "Blood Glucose")
                    {
                        todaysLogs.Add(log);
                    }
                }
                HaveIt = CheckIfAchieved(HaveIt, "Blood Glucose Logger");

                if (todaysLogs.Count == 3 && !HaveIt)
                {
                    AddAchievement();
                }
            }
        }

        private void CheckDailyStreak(int checkDays)
        {
            LoadAchievements();
            bool HaveIt = false;
            if (User.DailyStreak == checkDays)
            {
                switch (checkDays)
                {
                    case 3:
                        StreakAchieve("3 Day Streak", HaveIt);
                        break;
                    case 7:
                        StreakAchieve("7 Day Streak", HaveIt);
                        break;
                    case 14:
                        StreakAchieve("14 Day Streak", HaveIt);
                        break;
                }
            }
        }

        private void StreakAchieve(string Challenge, bool HaveIt)
        {
            GetAchievementsForChallenge(Challenge);
            HaveIt = CheckIfAchieved(HaveIt, Challenge);
            if (!HaveIt)
            {
                AddAchievement();
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
            if (logs.Count() > 0)
            {
                foreach (Log log in logs)
                {
                    if (log.LogDate.Day.Equals(DateTimeOffset.Now.Day) && log.Type == "Activity")
                    {
                        TotalActivity = TotalActivity + int.Parse(log.Amount);
                    }
                }

                HaveIt = CheckIfAchieved(HaveIt, "Active Life");

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
            if (quiz.Count() > 0)
            {
                foreach (Quiz q in quiz)
                {
                    quizList.Add(q);
                }
                HaveIt = CheckIfAchieved(HaveIt, "Quiz Master");

                if (quizList.Count() == 1 && !HaveIt)
                {
                    AddAchievement();
                }
            }
        }

        private bool CheckIfAchieved(bool HaveIt, string ChallengeName)
        {
            foreach (Achievement a in MyAchievements)
            {
                HaveIt |= (a.Name == ChallengeName && a.IsAchieved == true);
            }
            return HaveIt;
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
    }
}
