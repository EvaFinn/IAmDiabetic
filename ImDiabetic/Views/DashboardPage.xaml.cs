﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class DashboardPage : ContentPage
    {
        public User User { get; }

        public DashboardPage(User user)
        {
            //haven't finished extracting to viewmodel, binding in xaml file for UI objects?!?!?!?!
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = new DashboardViewModel(user);
            User = (BindingContext as DashboardViewModel).User;
            TapOptions();
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
                stckGoals.BackgroundColor = Color.DeepSkyBlue;
                await Navigation.PushAsync(new GoalsPage());
                DefaultBackground();
            };
            stckGoals.GestureRecognizers.Add(goalsTap);

            var logsTap = new TapGestureRecognizer();
            logsTap.Tapped += async (sender, e) =>
            {
                stckLogs.BackgroundColor = Color.DeepSkyBlue;
                await Navigation.PushAsync(new LogsPage(User));
                DefaultBackground();
            };
            stckLogs.GestureRecognizers.Add(logsTap);

            var trendsTap = new TapGestureRecognizer();
            trendsTap.Tapped += async (sender, e) =>
            {
                ;
                stckTrends.BackgroundColor = Color.DeepSkyBlue;
                await Navigation.PushAsync(new TrendPage());
                DefaultBackground();
            };
            stckTrends.GestureRecognizers.Add(trendsTap);

            var moreTap = new TapGestureRecognizer();
            moreTap.Tapped += async (sender, e) =>
            {
                stckMore.BackgroundColor = Color.DeepSkyBlue;
                await Navigation.PushAsync(new MorePage());
                DefaultBackground();
            };
            stckMore.GestureRecognizers.Add(moreTap);
        }

        public void DefaultBackground()
        {
            stckGoals.BackgroundColor = Color.White;
            stckLogs.BackgroundColor = Color.White;
            stckTrends.BackgroundColor = Color.White;
            stckMore.BackgroundColor = Color.White;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
