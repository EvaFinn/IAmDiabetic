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
        public string DisplayProgress { get; set; }
        int score = 0;

        public QuizPage(AppUser user, string ChosenTopic)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            User = user;
            this.BindingContext = new QuestionViewModel(ChosenTopic, User);
            (BindingContext as QuestionViewModel).LoadQuestions();
            DisplayProgress =QuizSettings.CurrentQuestion + " / " + QuizSettings.QUESTIONS_COUNT;
            Progress.Text = DisplayProgress;
        }

        private void DoAnswer()
        {
            if (QuizSettings.CurrentQuestion < QuizSettings.QUESTIONS_COUNT)
            {
                QuizSettings.CurrentQuestion++;
                ((QuestionViewModel)BindingContext).ChooseNewQuestion();
                DisplayProgress = QuizSettings.CurrentQuestion + " / " + QuizSettings.QUESTIONS_COUNT;
                Progress.Text = DisplayProgress;
            }
            else
            {
                QuizSettings.Score += score;
                (BindingContext as QuestionViewModel).AddQuizToDB(QuizSettings.Score);
                ResetQuizStats();
                NavigateToEndPage();
            }
        }

        public void AnswerOneClicked(object sender, System.EventArgs e)
        {
            if ((BindingContext as QuestionViewModel).CheckIfCorrect(1))
            {
                score++;
            }
            DoAnswer();
        }

        public void AnswerTwoClicked(object sender, System.EventArgs e)
        {
            if ((BindingContext as QuestionViewModel).CheckIfCorrect(2))
            {
                score++;
            }
            DoAnswer();
        }

        public void AnswerThreeClicked(object sender, System.EventArgs e)
        {
            if ((BindingContext as QuestionViewModel).CheckIfCorrect(3))
            {
                score++;
            }
            DoAnswer();
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

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}

