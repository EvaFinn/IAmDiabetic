using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
using Lottie.Forms;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class DashboardPage : ContentPage
    {
        public AppUser User { get; set; }

        public DashboardPage(AppUser user)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            User = user;
            this.BindingContext = new DashboardViewModel(User);
            AnimationView.Animation = User.Pet.ToLower() + ".json";
            flameImage.Source = ImageSource.FromResource("ImDiabetic.Utils.flame.png");
            TapOptions();
            logTestLabel.IsVisible = false;
        }

        private void TapOptions()
        {
            imgGoals.Source = ImageSource.FromResource("ImDiabetic.Icons.trophiesStep.png");
            imgLogs.Source = ImageSource.FromResource("ImDiabetic.Icons.logs.png");
            imgTrends.Source = ImageSource.FromResource("ImDiabetic.Icons.trends.png");
            imgMore.Source = ImageSource.FromResource("ImDiabetic.Icons.more.png");

            var goalsTap = new TapGestureRecognizer();
            goalsTap.Tapped += async (sender, e) =>
            {
                stckGoals.BackgroundColor = Color.White;
                await Navigation.PushAsync(new GoalsPage(User));
                DefaultBackground();
            };
            stckGoals.GestureRecognizers.Add(goalsTap);

            var logsTap = new TapGestureRecognizer();
            logsTap.Tapped += async (sender, e) =>
            {
                stckLogs.BackgroundColor = Color.White;
                await Navigation.PushAsync(new LogsPage(User));
                DefaultBackground();
            };
            stckLogs.GestureRecognizers.Add(logsTap);

            var trendsTap = new TapGestureRecognizer();
            trendsTap.Tapped += async (sender, e) =>
            {
                stckTrends.BackgroundColor = Color.White;
                await Navigation.PushAsync(new TrendPage(User));
                DefaultBackground();
            };
            stckTrends.GestureRecognizers.Add(trendsTap);

            var moreTap = new TapGestureRecognizer();
            moreTap.Tapped += async (sender, e) =>
            {
                stckMore.BackgroundColor = Color.White;
                await Navigation.PushAsync(new MorePage(User));
                DefaultBackground();
            };
            stckMore.GestureRecognizers.Add(moreTap);
        }

        public void DefaultBackground()
        {
            stckGoals.BackgroundColor = Color.DeepSkyBlue;
            stckLogs.BackgroundColor = Color.DeepSkyBlue;
            stckTrends.BackgroundColor = Color.DeepSkyBlue;
            stckMore.BackgroundColor = Color.DeepSkyBlue;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
