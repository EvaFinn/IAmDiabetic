using System;
using System.Collections.Generic;
using ImDiabetic.ViewModels;
using Xamarin.Forms;

namespace ImDiabetic.Views.More
{
    public partial class QuizResultPage : ContentPage
    {
        public User User { get; }
        public QuizResultPage(User user)
        {
            InitializeComponent();
            User = user;
            BindingContext = new QuizViewModel(User);
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new DashboardPage(User));
        }
    }
}
