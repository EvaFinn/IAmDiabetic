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
using Xamarin.Forms;

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
        private int questioncount;

        public QuestionViewModel(User user, int question)
        {
            User = user;
            questioncount = question;
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
            //questioncount = 0;
            PrepareQuastion();


        }

        private void PrepareQuastion()
        {
            string json, earthquakes;
            List<QuizQuestion> jsonresult;

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(QuizQuestion)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("ImDiabetic.quizquestion.json");
            json = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            jsonresult = JsonConvert.DeserializeObject<List<QuizQuestion>>(json);
            earthquakes = jsonresult.ElementAt(0).Answer;


            Question = jsonresult.ElementAt(questioncount).Question;
            Topic = jsonresult.ElementAt(questioncount).Topic;
            OptionOne = jsonresult.ElementAt(questioncount).OptionOne;
            OptionTwo = jsonresult.ElementAt(questioncount).OptionTwo;
            OptionThree = jsonresult.ElementAt(questioncount).OptionThree;
            Answer = jsonresult.ElementAt(questioncount).Answer;
            Message = "Message";

            Debug.WriteLine("*********** COUNT JSON : " + jsonresult.Count);
            Debug.WriteLine("*********** TESTTEST : " + questioncount);


            //if(questioncount == 2) {
            //    _navigation.PopAsync();
            //}
            //else {
            //    Debug.WriteLine("i don't even know");
            //}
        }

        public bool CheckAnswer(string answerchoice) {
            Debug.WriteLine("!!!!!!!!!! WHAT : " + answerchoice);
            questioncount++;
            bool result;
            if (answerchoice.Equals(Answer)) {
                Message = "CORRECT ANSWER";
                Debug.WriteLine("CORRECT");
                result = true;
            }
            else {
                Message = "INCORRECT FOOL";
                Debug.WriteLine("INCORRECT");
                result = false;
            }
            //PrepareQuastion();
            return result;
        }
    }
}