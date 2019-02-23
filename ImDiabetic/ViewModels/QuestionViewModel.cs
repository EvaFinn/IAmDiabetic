using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using ImDiabetic.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Syncfusion.XForms.Buttons;

namespace ImDiabetic.ViewModels
{
    public class QuestionViewModel :BaseViewModel
    {
        public User User { get; set; }
        public string Question { get; set; }
        public string Topic { get; set; }
        public string OptionOne { get; set; }
        public string OptionTwo { get; set; }
        public string OptionThree { get; set; }
        public string Answer { get; set; }
        public string Message { get; set; }

        public QuestionViewModel(User user)
        {
            User = user;
            InitQuestion();

        }

        private void InitQuestion()
        {
            var quizzes = realm.All<Quiz>().Where(u => u.UserId == User.Id);
            foreach (Quiz quiz in quizzes)
            {
                Debug.WriteLine("QQQQQQQ  " + quiz.Question.Question + ",," + quiz.Question.Answer);
            }

            var q = quizzes.FirstOrDefault();
            //Question = q.Question.Question;
            //Topic = q.Question.Topic;
            //OptionOne = q.Question.OptionOne;
            //OptionTwo = q.Question.OptionTwo;
            //OptionThree = q.Question.OptionThree;
            //Message = q.Question.Answer;


            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(QuizQuestion)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("ImDiabetic.quizquestion.json");
            string json = "";
            string earthquakes;

            using (var reader = new System.IO.StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            var jsonresult = JsonConvert.DeserializeObject<List<QuizQuestion>>(json);
            earthquakes = jsonresult.ElementAt(1).Answer;
            Question = jsonresult.ElementAt(1).Question;
            Topic = jsonresult.ElementAt(1).Topic;
            OptionOne = jsonresult.ElementAt(1).OptionOne;
            OptionTwo = jsonresult.ElementAt(1).OptionTwo;
            OptionThree = jsonresult.ElementAt(1).OptionThree;
            Answer = jsonresult.ElementAt(1).Answer;
            Message = "Message";


            Debug.WriteLine("*********** TESTTEST: " + json);
            Debug.WriteLine("@@@@@@@@@@@ TESTTEST: " + earthquakes);
        }

        public bool CheckAnswer(string answerchoice) {
            Debug.WriteLine("!!!!!!!!!! WHAT : " + answerchoice);
            if (answerchoice.Equals(Answer)) {
                Message = "CORRECT ANSWER";
                Debug.WriteLine("CORRECT");
                return true;
            }
            else {
                Message = "INCORRECT FOOL";
                Debug.WriteLine("INCORRECT");
                return false;
            }
        }
    }
}