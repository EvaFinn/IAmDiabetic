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
            btn2.Source = ImageSource.FromResource("ImDiabetic.Icons.rocket.png");

            TapOptions();
            CheckLogs();
            btn1.Text = User.FirstName;
            //App.Current.Properties
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

            //imgGoal2.Source = ImageSource.FromResource("ImDiabetic.Icons.trophiesStep.png");
            //imgGoal3.Source = ImageSource.FromResource("ImDiabetic.Icons.trophiesStep.png");
            //imgGoal4.Source = ImageSource.FromResource("ImDiabetic.Icons.trophiesStep.png");
            //imgGoal5.Source = ImageSource.FromResource("ImDiabetic.Icons.trophiesStep.png");




            //imgGoals2.Source = ImageSource.FromResource("ImDiabetic.Icons.rocket.png");
            //imgLogs2.Source = ImageSource.FromResource("ImDiabetic.Icons.rocket.png");
            //imgTrends2.Source = ImageSource.FromResource("ImDiabetic.Icons.rocket.png");
            //imgMore2.Source = ImageSource.FromResource("ImDiabetic.Icons.rocket.png");

            //Tap Gesture Recognizer  
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


            //var goalsTap2 = new TapGestureRecognizer();
            //goalsTap2.Tapped += async (sender, e) =>
            //{
            //    //DefaultBackground();
            //    stckGoals2.BackgroundColor = Color.DeepSkyBlue;
            //    await Navigation.PushAsync(new GoalsPage());
            //    DefaultBackground();
            //};
            //stckGoals2.GestureRecognizers.Add(goalsTap2);

            //var logsTap2 = new TapGestureRecognizer();
            //logsTap2.Tapped += async (sender, e) =>
            //{
            //    //DefaultBackground();
            //    stckLogs2.BackgroundColor = Color.DeepSkyBlue;
            //    await Navigation.PushAsync(new LogsPage(User));
            //    DefaultBackground();
            //};
            //stckLogs2.GestureRecognizers.Add(logsTap2);

            //var trendsTap2 = new TapGestureRecognizer();
            //trendsTap.Tapped += async (sender, e) =>
            //{
            //    //DefaultBackground();
            //    stckTrend2.BackgroundColor = Color.DeepSkyBlue;
            //    await Navigation.PushAsync(new TrendsPage());
            //    DefaultBackground();
            //};
            //stckTrend2.GestureRecognizers.Add(trendsTap2);

            //var moreTap2 = new TapGestureRecognizer();
            //moreTap2.Tapped += async (sender, e) =>
            //{
            //    //DefaultBackground();
            //    stckMore2.BackgroundColor = Color.DeepSkyBlue;
            //    await Navigation.PushAsync(new MorePage());
            //    DefaultBackground();
            //};
            //stckMore2.GestureRecognizers.Add(moreTap2);
        }

        public void DefaultBackground()
        {
            stckGoals.BackgroundColor = Color.White;
            stckLogs.BackgroundColor = Color.White;
            stckTrends.BackgroundColor = Color.White;
            stckMore.BackgroundColor = Color.White;
        }

        async void Logout_Click(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
