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
                Score = "Your Score is " + LastQuiz.Score + " out of 6";
            }
            realm.Write(() =>
            {
                User.Score = User.Score + int.Parse(LastQuiz.Score);
            });
        }
    }
}
