using System;
using System.Linq;
using ImDiabetic.Models;

namespace ImDiabetic.ViewModels
{
    public class QuizViewModel : BaseViewModel
    {
        public User User { get; set; }
        public string Score { get; set; }
        public string Topic { get; set; }

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
        }
    }
}
