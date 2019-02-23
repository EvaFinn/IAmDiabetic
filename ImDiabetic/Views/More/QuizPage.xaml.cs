using System;
using System.Collections.Generic;
using ImDiabetic.ViewModels;
using Xamarin.Forms;

namespace ImDiabetic.Views.More
{
    public partial class QuizPage : ContentPage
    {
        public QuizPage(User user)
        {
            InitializeComponent();
            this.BindingContext = new QuestionViewModel(user);
        }
    }
}
