using System;
using System.Collections.Generic;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
using Xamarin.Forms;

namespace ImDiabetic.Views.More
{
    public partial class QuizResultPage : ContentPage
    {
        public AppUser User { get; }
        public QuizResultPage(AppUser user)
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
