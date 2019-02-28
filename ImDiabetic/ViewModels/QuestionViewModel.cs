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
        public User User { get; set; }
        private int questionNumber = 0;
        //public string Question { get; set; }
        //public string Topic { get; set; }
        //public string OptionOne { get; set; }
        //public string OptionTwo { get; set; }
        //public string OptionThree { get; set; }
        //public string Answer { get; set; }
        //public string Message { get; set; }
        //private int questioncount;

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
        List<QuizQuestion> QuestionList
        {
            get { return this._questionList; }
            set
            {
                this._questionList = value;
                PropertyChanged(this,
                    new PropertyChangedEventArgs("QuestionList"));
            }
        }
        Random rnd = new Random();

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isLoading;
        public bool IsLoading
        {
            get { return this._isLoading; }
            set
            {
                this._isLoading = value;
                PropertyChanged(this,
                    new PropertyChangedEventArgs("IsLoading"));
            }
        }

        private string _message;
        public string Message
        {
            get
            {
                return this._message;
            }
            set
            {
                this._message = value;
                PropertyChanged(this,
                    new PropertyChangedEventArgs("Message"));
            }
        }

        public QuestionViewModel()
        {
            //LoadQuestions();
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

            IsLoading = true;
            QuestionList = jsonresult;



            IsLoading = false;
            ChooseNewQuestion();
        }

        public void ChooseNewQuestion()
        {
            IsLoading = true;
            //if(questionNumber == 3) { 
                
            //}

            QuizQuestion selectedItem = QuestionList[questionNumber];

            Answer1Enabled = true;
            Answer2Enabled = true;
            Answer3Enabled = true;

            Question = selectedItem.Question;
            Answer1 = selectedItem.OptionOne;
            Answer2 = selectedItem.OptionTwo;
            Answer3 = selectedItem.OptionThree;

            CorrectAnswer = int.Parse(selectedItem.Answer);

            IsLoading = false;
            Debug.WriteLine("*********** COUNT JSON : " + questionNumber);
            questionNumber++;
        }




        //public QuestionViewModel(User user, int question)
        //{
        //    User = user;
        //    questioncount = question;
        //    InitQuestion();
        //}

        //private void InitQuestion()
        //{
        //    var quizzes = realm.All<Quiz>().Where(u => u.UserId == User.Id);
        //    foreach (Quiz quiz in quizzes)
        //    {
        //        Debug.WriteLine("QQQQQQQ  " + quiz.Question.Question + ",," + quiz.Question.Answer);
        //    }

        //    var q = quizzes.FirstOrDefault();
        //    //Question = q.Question.Question;
        //    //Topic = q.Question.Topic;
        //    //OptionOne = q.Question.OptionOne;
        //    //OptionTwo = q.Question.OptionTwo;
        //    //OptionThree = q.Question.OptionThree;
        //    //Message = q.Question.Answer;
        //    //questioncount = 0;
        //    PrepareQuastion();


        //}

        //private void PrepareQuastion()
        //{
        //    string json, earthquakes;
        //    List<QuizQuestion> jsonresult;

        //    var assembly = IntrospectionExtensions.GetTypeInfo(typeof(QuizQuestion)).Assembly;
        //    Stream stream = assembly.GetManifestResourceStream("ImDiabetic.quizquestion.json");
        //    json = "";
        //    using (var reader = new System.IO.StreamReader(stream))
        //    {
        //        json = reader.ReadToEnd();
        //    }

        //    jsonresult = JsonConvert.DeserializeObject<List<QuizQuestion>>(json);
        //    earthquakes = jsonresult.ElementAt(0).Answer;


        //    Question = jsonresult.ElementAt(questioncount).Question;
        //    Topic = jsonresult.ElementAt(questioncount).Topic;
        //    OptionOne = jsonresult.ElementAt(questioncount).OptionOne;
        //    OptionTwo = jsonresult.ElementAt(questioncount).OptionTwo;
        //    OptionThree = jsonresult.ElementAt(questioncount).OptionThree;
        //    Answer = jsonresult.ElementAt(questioncount).Answer;
        //    Message = "Message";

        //    Debug.WriteLine("*********** COUNT JSON : " + jsonresult.Count);
        //    Debug.WriteLine("*********** TESTTEST : " + questioncount);


        //    //if(questioncount == 2) {
        //    //    _navigation.PopAsync();
        //    //}
        //    //else {
        //    //    Debug.WriteLine("i don't even know");
        //    //}
        //}

        //public bool CheckAnswer(string answerchoice) {
        //    Debug.WriteLine("!!!!!!!!!! WHAT : " + answerchoice);
        //    questioncount++;
        //    bool result;
        //    if (answerchoice.Equals(Answer)) {
        //        Message = "CORRECT ANSWER";
        //        Debug.WriteLine("CORRECT");
        //        result = true;
        //    }
        //    else {
        //        Message = "INCORRECT FOOL";
        //        Debug.WriteLine("INCORRECT");
        //        result = false;
        //    }
        //    //PrepareQuastion();
        //    return result;
        //}
    }
}