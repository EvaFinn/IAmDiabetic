using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ImDiabetic.Models;
using Realms;
using Xamarin.Forms;

namespace ImDiabetic
{
    public partial class DashboardPage : ContentPage
    {
        Realm realm;
        public User User { get; }

        public DashboardPage(User user)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            var config = new RealmConfiguration() { SchemaVersion = 2 };
            realm = Realm.GetInstance(config);
            User = user;
            welcomeUserLabel.Text = "Welcome " + User.FirstName + "!";

            TapOptions();
            CheckLogs();
        }

        private void CheckLogs()
        {
            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            if (logs.Count() < 1)
            {
                logTestLabel.Text = "No logs made.";
            } else {
                foreach(Log log in logs) {
                    Debug.WriteLine("*********** LOGS " + log.Id + ", " + log.BloodGlucose + ", " + log.LogDate);
                }
                Log testLog = logs.FirstOrDefault();
                logTestLabel.Text = testLog.Id + "," + testLog.BloodGlucose;
            }

        }

        private void TapOptions()
        {

            imgGoals.Source = ImageSource.FromResource("ImDiabetic.Icons.trophiesStep.png");
            imgLogs.Source = ImageSource.FromResource("ImDiabetic.Icons.logs.png");
            imgTrends.Source = ImageSource.FromResource("ImDiabetic.Icons.trends.png");
            imgMore.Source = ImageSource.FromResource("ImDiabetic.Icons.more.png");
            //imgHome.Source = ImageSource.FromResource("ImDiabetic.Icons.home.png");

            var goalsTap = new TapGestureRecognizer();
            goalsTap.Tapped += async (sender, e) =>
            {
                //DefaultBackground();
                stckGoals.BackgroundColor = Color.DeepSkyBlue;
                await Navigation.PushAsync(new GoalsPage());
                DefaultBackground();
            };
            stckGoals.GestureRecognizers.Add(goalsTap);

            var logsTap = new TapGestureRecognizer();
            logsTap.Tapped += async (sender, e) =>
            {
                //DefaultBackground();
                stckLogs.BackgroundColor = Color.DeepSkyBlue;
                await Navigation.PushAsync(new LogsPage(User));
                DefaultBackground();
            };
            stckLogs.GestureRecognizers.Add(logsTap);

            var trendsTap = new TapGestureRecognizer();
            trendsTap.Tapped += async (sender, e) =>
            {
                //DefaultBackground();
                stckTrends.BackgroundColor = Color.DeepSkyBlue;
                await Navigation.PushAsync(new TrendsPage());
                DefaultBackground();
            };
            stckTrends.GestureRecognizers.Add(trendsTap);

            var moreTap = new TapGestureRecognizer();
            moreTap.Tapped += async (sender, e) =>
            {
                //DefaultBackground();
                stckMore.BackgroundColor = Color.DeepSkyBlue;
                await Navigation.PushAsync(new MorePage());
                DefaultBackground();
            };
            stckMore.GestureRecognizers.Add(moreTap);

            //var homeTap = new TapGestureRecognizer();
            //homeTap.Tapped += async (sender, e) =>
            //{
            //    //DefaultBackground();
            //    stckHome.BackgroundColor = Color.DeepSkyBlue;
            //    await Navigation.PushAsync(new DashboardPage(User));
            //    DefaultBackground();
            //};
            //stckHome.GestureRecognizers.Add(homeTap);
        }

        public void DefaultBackground()
        {
            stckGoals.BackgroundColor = Color.White;
            stckLogs.BackgroundColor = Color.White;
            stckTrends.BackgroundColor = Color.White;
            stckMore.BackgroundColor = Color.White;
        }

        //void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        //{
        //    Detail =new NavigationPage(new ProfilePage(User));
        //    IsPresented = false;
        //}

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
