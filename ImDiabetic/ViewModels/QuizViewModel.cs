using System;
using System.Diagnostics;
using System.Linq;
using ImDiabetic.Models;

namespace ImDiabetic.ViewModels
{
    public class QuizViewModel : BaseViewModel
    {
        public AppUser User { get; set; }
        public string Score { get; set; }
        public string Topic { get; set; }
        public string Points { get; set; }
        public int Level { get; set; }
        public int LevelOne { get; set; } = 10;
        public int LevelTwo { get; set; } = 20;
        public int LevelThree { get; set; } = 30;

        public QuizViewModel(AppUser user)
        {
            User = user;
            var quizzes = realm.All<Quiz>().Where(u => u.UserId == User.Id);
            Quiz LastQuiz = quizzes.Last();
            if (LastQuiz.Score == "6")
            {
                Score = "Your Score is " + LastQuiz.Score + "! Full Marks!";
            }
            else
            {
                Score = "Your Score is " + LastQuiz.Score + "!";
            }
            Topic = "Topic was " + LastQuiz.Topic;
            realm.Write(() =>
            {
                User.Score = User.Score + int.Parse(LastQuiz.Score);
            });

            Points = User.Score.ToString();
            Level = User.Level;
        }
    }
}
