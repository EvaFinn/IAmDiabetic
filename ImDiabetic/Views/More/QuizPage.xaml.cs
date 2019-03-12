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
        public AppUser User { get; set; }
        int score = 0;

        public QuizPage(AppUser user, string ChosenTopic)
        {
            InitializeComponent();
            User = user;
            this.BindingContext = new QuestionViewModel(ChosenTopic, User);
            (BindingContext as QuestionViewModel).LoadQuestions();

            btnAnswerOne.Clicked += (sender, ea) =>
            {
                if ((BindingContext as QuestionViewModel).CheckIfCorrect(1))
                {
                    score++;
                }
                DoAnswer();
            };

            btnAnswerTwo.Clicked += (sender, ea) =>
            {
                if ((BindingContext as QuestionViewModel).CheckIfCorrect(2))
                {
                    score++;
                }
                DoAnswer();
            };

            btnAnswerThree.Clicked += (sender, ea) =>
            {
                if ((BindingContext as QuestionViewModel).CheckIfCorrect(3))
                {
                    score++;
                }
                DoAnswer();
            };
        }

        private void DoAnswer()
        {
            if (QuizSettings.CurrentQuestion < QuizSettings.QUESTIONS_COUNT)
            {
                QuizSettings.CurrentQuestion++;
                ((QuestionViewModel)BindingContext).ChooseNewQuestion();
            }
            else
            {
                QuizSettings.Score += score;
                Debug.WriteLine("Final Score : " + QuizSettings.Score);
                (BindingContext as QuestionViewModel).AddQuizToDB(QuizSettings.Score);
                ResetQuizStats();
                NavigateToEndPage();
            }
        }

        private static void ResetQuizStats()
        {
            QuizSettings.CurrentQuestion = 1;
            QuizSettings.Score = 0;
        }

        async private void NavigateToEndPage()
        {
            await Navigation.PushAsync(new QuizResultPage(User));
        }
    }
}

