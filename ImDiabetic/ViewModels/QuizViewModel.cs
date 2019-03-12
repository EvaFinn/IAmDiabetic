using System;
using System.Diagnostics;
using System.Linq;
using ImDiabetic.Models;

namespace ImDiabetic.ViewModels
{
    public class QuizViewModel : BaseViewModel
    {
        public User User { get; set; }
        public string Score { get; set; }
        public string Topic { get; set; }
        public string Points { get; set; }
        public int Level { get; set; }
        public int LevelOne { get; set; } = 10;
        public int LevelTwo { get; set; } = 20;
        public int LevelThree { get; set; } = 30;

        public QuizViewModel(User user)
        {
            User = user;
            var quizzes = realm.All<Quiz>().Where(u => u.UserId == User.Id);
            Quiz LastQuiz = quizzes.Last();
            Score = "Your Score is " + LastQuiz.Score + "!";
            Topic = "Topic was " + LastQuiz.Topic;
            realm.Write(() =>
            {
                User.Score = User.Score + int.Parse(LastQuiz.Score);
            });


            Points = User.Score.ToString();
            Level = User.Level;
            LevelStuff();
        }

        public void LevelStuff()
        {
            int pointsRequiredToLevelUp = (15 * Level) + (9 * (Level - 1));
            //level 1 - 15
            //level 2 - 39
            //level 3 - 63
            //level 4 - 87
            //level 5 - 111
            //very simple one

            if (int.Parse(Points) >= pointsRequiredToLevelUp)
            {
                realm.Write(() =>
                {
                    //User.Level = 1;
                    //User.Score = 0;
                    User.Level++;
                });
                Debug.WriteLine("******** Points " + Points);
                Debug.WriteLine("******** Level" + Level);
                Debug.WriteLine("******** points needed" + pointsRequiredToLevelUp);
            }
        }
    }
}
