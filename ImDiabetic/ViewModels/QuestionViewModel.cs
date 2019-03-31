using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class QuestionViewModel :BaseViewModel, INotifyPropertyChanged
    {
        public AppUser User { get; set; }
        private int questionNumber = 0;
        private string _displayprogress = "";
        public string DisplayProgress
        {
            get { return this._displayprogress; }
            set
            {
                this._displayprogress = value;
                PropertyChanged(this,
                    new PropertyChangedEventArgs("DisplayProgress"));
            }
        } 
        private int _correctAnswer = 0;
        public int CorrectAnswer
        {
            get { return this._correctAnswer; }
            set
            {
                this._correctAnswer = value;
                PropertyChanged(this,
                    new PropertyChangedEventArgs("CorrectAnswer"));
            }
        }
        private string _question;
        public string Question
        {
            get { return this._question; }
            set
            {
                this._question = value;
                PropertyChanged(this,
                    new PropertyChangedEventArgs("Question"));
            }
        }
        private string _answer1;
        public string Answer1
        {
            get { return this._answer1; }
            set
            {
                this._answer1 = value;
                PropertyChanged(this,
                    new PropertyChangedEventArgs("Answer1"));
            }
        }
        private bool _answer1Enabled;
        public bool Answer1Enabled
        {
            get { return this._answer1Enabled; }
            set
            {
                this._answer1Enabled = value;
                PropertyChanged(this,
                    new PropertyChangedEventArgs("Answer1Enabled"));
            }
        }
        private string _answer2;
        public string Answer2
        {
            get { return this._answer2; }
            set
            {
                this._answer2 = value;
                PropertyChanged(this,
                    new PropertyChangedEventArgs("Answer2"));
            }
        }
        private bool _answer2Enabled;
        public bool Answer2Enabled
        {
            get { return this._answer2Enabled; }
            set
            {
                this._answer2Enabled = value;
                PropertyChanged(this,
                    new PropertyChangedEventArgs("Answer2Enabled"));
            }
        }
        private string _answer3;
        public string Answer3
        {
            get { return this._answer3; }
            set
            {
                this._answer3 = value;
                PropertyChanged(this,
                    new PropertyChangedEventArgs("Answer3"));
            }
        }
        private bool _answer3Enabled;
        public bool Answer3Enabled
        {
            get { return this._answer3Enabled; }
            set
            {
                this._answer3Enabled = value;
                PropertyChanged(this,
                    new PropertyChangedEventArgs("Answer3Enabled"));
            }
        }
        private List<QuizQuestion> _questionList;
        public List<QuizQuestion> QuestionList
        {
            get { return this._questionList; }
            set
            {
                this._questionList = value;
                PropertyChanged(this,
                    new PropertyChangedEventArgs("QuestionList"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private string _message;
        public string Message
        {
            get { return this._message; }
            set
            {
                this._message = value;
                PropertyChanged(this,
                    new PropertyChangedEventArgs("Message"));
            }
        }
        private string ChosenTopic { get; set; }


        public QuestionViewModel(string topic, AppUser user)
        {
            User = user;
            ChosenTopic = topic;
        }

        public bool CheckIfCorrect(int correct)
        {
            if (CorrectAnswer == correct)
            {
                Message = "Correct Answer!";
                return true;
            }
            Message = "Incorrect!";
            return false;
        }

        public void LoadQuestions()
        {
            List<QuizQuestion> jsonresult;
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(QuizQuestion)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("ImDiabetic.quizquestion.json");
            string json = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }
            jsonresult = JsonConvert.DeserializeObject<List<QuizQuestion>>(json);
            List<QuizQuestion> questions = new List<QuizQuestion>();

            for (int i = 0; i < jsonresult.Count; i++) {
                if (jsonresult.ElementAt(i).Topic.Equals(ChosenTopic))
                {
                    questions.Add(jsonresult[i]);
                }
            }
            QuestionList = questions;
            ChooseNewQuestion();
        }

        public void ChooseNewQuestion()
        {
            QuizQuestion selectedItem = QuestionList[questionNumber];
            DisplayProgress = (questionNumber+1) + " / 6";

            Answer1Enabled = true;
            Answer2Enabled = true;
            Answer3Enabled = true;

            Question = selectedItem.Question;
            Answer1 = selectedItem.OptionOne;
            Answer2 = selectedItem.OptionTwo;
            Answer3 = selectedItem.OptionThree;
            CorrectAnswer = int.Parse(selectedItem.Answer);
            questionNumber++;
        }

        public void AddQuizToDB(int finalScore) {
            var quiz = new Quiz { UserId = User.Id, Score = finalScore.ToString(), Topic = ChosenTopic };
            realm.Write(() => {
                realm.Add(quiz);
            });
        }
    }
}