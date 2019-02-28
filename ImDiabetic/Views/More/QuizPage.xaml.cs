using System;
using System.Collections.Generic;
using System.Diagnostics;
using Android.Widget;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;

namespace ImDiabetic.Views.More
{
    public partial class QuizPage : ContentPage
    {
       public User User { get; set; }

        //int _choice = 0;
        int score = 100;

        public QuizPage(User user, string ChosenTopic)
        {
            InitializeComponent();
            User = user;
            this.BindingContext = new QuestionViewModel(ChosenTopic);
            (BindingContext as QuestionViewModel).LoadQuestions();

            btnAnswerOne.Clicked += (sender, ea) =>
            {
                if ((BindingContext as QuestionViewModel).CheckIfCorrect(1)) DoAnswer();
                else
                {
                    score = score / 2;
                }
            };

            btnAnswerTwo.Clicked += (sender, ea) =>
            {
                if ((BindingContext as QuestionViewModel).CheckIfCorrect(2)) DoAnswer();
                else
                {
                    score = score / 2;
                }
            };

            btnAnswerThree.Clicked += (sender, ea) =>
            {
                if ((BindingContext as QuestionViewModel).CheckIfCorrect(3))
                {
                    DoAnswer();
                }
                else
                {
                    score = score / 2;
                }
            };
        }

        private void DoAnswer()
        {
            QuizSettings.Score += score;
            Debug.WriteLine("CQ : " + QuizSettings.CurrentQuestion);
            Debug.WriteLine("QC : " + QuizSettings.QUESTIONS_COUNT);
            Debug.WriteLine("S : " + QuizSettings.Score);

            if (QuizSettings.CurrentQuestion < QuizSettings.QUESTIONS_COUNT)
            {
                QuizSettings.CurrentQuestion++;
                ((QuestionViewModel)BindingContext).ChooseNewQuestion();
            }
            else
            {
                QuizSettings.CurrentQuestion = 1;
                QuizSettings.Score = 0;
                NavigateToEndPage();
            }
        }

        async private void NavigateToEndPage()
        {
            //await Navigation.PushAsync(new DashboardPage(User));
            await Navigation.PopToRootAsync();
        }
    }
}

