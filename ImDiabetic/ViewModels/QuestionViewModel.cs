using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using ImDiabetic.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            //var serializer = JsonSerializer.CreateDefault();
            //var jsonArray = JArray.Parse(jsonString);
            //var reports = new List<User>();
            //realm.Write(() =>
            //{
            //    foreach (var jsonValue in jsonArray)
            //    {
            //        var report = realm.CreateObject<User>();
            //        serializer.Populate(new JTokenReader(jsonValue), report);
            //        reports.Add(report);
            //    }
            //});

            var quizzes = realm.All<Quiz>().Where(u => u.UserId == User.Id);
            foreach (Quiz quiz in quizzes)
            {
                Debug.WriteLine("QQQQQQQ  " + quiz.Question.Question + ",," + quiz.Question.Answer);
            }

            var q = quizzes.FirstOrDefault();
            Question = q.Question.Question;
            Topic = q.Question.Topic;
            OptionOne = q.Question.OptionOne;
            OptionTwo = q.Question.OptionTwo;
            OptionThree = q.Question.OptionThree;
            Message = q.Question.Answer;


            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(QuizQuestion)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("ImDiabetic.quizquestion.json");
            string json = "";
            string earthquakes;

            using (var reader = new System.IO.StreamReader(stream))
            {
                json = reader.ReadToEnd();
                var jsonresult = JsonConvert.DeserializeObject<List<QuizQuestion>>(json);
                earthquakes = jsonresult.ElementAt(1).Answer;
            }

            Debug.WriteLine("*********** TESTTEST: " + json);
            Debug.WriteLine("@@@@@@@@@@@ TESTTEST: " + earthquakes);
            //QuizQuestion ques = new QuizQuestion { Question =  };

            //var question = new QuizQuestion { Question = "Question?", Answer = "Answer", OptionOne = "Answer", OptionTwo = "Not Answer 2", OptionThree = "Not Answer 3" };
            // SAVE JSON DATA INTO QUESTION OBJECT. MAYBE ADD TOPIC FIELD TO QUESTION/QUIZ? SORT BY TOPIC WHEN 
            // DISPLAYING AVAILABLE QUIZZES.

        }
    }
}