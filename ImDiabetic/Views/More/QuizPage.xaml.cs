using System;
using System.Collections.Generic;
using Android.Widget;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;

namespace ImDiabetic.Views.More
{
    public partial class QuizPage : ContentPage
    {
        string answerchoice;
        int questcount;
        public User User { get; set; }

        int _choice = 0;
        int score = 100;

        public QuizPage(User user)
        {
            InitializeComponent();
            User = user;
            this.BindingContext = new QuestionViewModel();
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
            if (QuizSettings.CurrentQuestion < QuizSettings.QUESTIONS_COUNT)
            {
                QuizSettings.CurrentQuestion++;
                ((QuestionViewModel)BindingContext).ChooseNewQuestion();
            }
            else
            {
                NavigateToEndPage();
            }
        }

        async private void NavigateToEndPage()
        {
            await Navigation.PushAsync(new MorePage(User));
        }
    }

    //    public QuizPage(User user, int qcount)
    //    {
    //        InitializeComponent();
    //        User = user;
    //        questcount = qcount;
    //        this.BindingContext = new QuestionViewModel();
    //    }

    //    private void CheckBox_StateChanged(object sender, StateChangedEventArgs e)
    //    {
    //        if (checkAnsOne.IsChecked.Value)
    //        {
    //            answerchoice = checkAnsOne.Text;
    //        }
    //        if (checkAnsTwo.IsChecked.Value)
    //        {
    //            answerchoice = checkAnsTwo.Text;
    //        }
    //        if (checkAnsThree.IsChecked.Value)
    //        {
    //            answerchoice = checkAnsThree.Text;
    //        }
    //    }

    //    void Handle_Clicked(object sender, System.EventArgs e)
    //    {
    //        checkAnsOne.IsChecked = false;
    //        checkAnsTwo.IsChecked = false;
    //        checkAnsThree.IsChecked = false;
    //        bool IsCorrect = false;

    //        if (answerchoice != null)
    //        {
    //            IsCorrect = (BindingContext as QuestionViewModel).CheckAnswer(answerchoice);
    //        }
    //        answerchoice = null;

    //        if (IsCorrect) {
    //            lblMessage.TextColor = Color.Green;
    //            lblMessage.Text = "Correct!";
    //        }
    //        else if (!IsCorrect) {
    //            lblMessage.TextColor = Color.Red;
    //            lblMessage.Text = "Incorrect!";
    //        }

    //       Navigation.PushAsync(new QuizPage(User, 1));
    //    }
}

