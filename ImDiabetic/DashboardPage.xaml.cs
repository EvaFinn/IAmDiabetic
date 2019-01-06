using System;
using System.Collections.Generic;
using System.Linq;
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
            InitializeComponent();
            var config = new RealmConfiguration() { SchemaVersion = 1 };
            realm = Realm.GetInstance(config);
            User = user;
            welcomeUserLabel.Text = "Welcome " + user.FirstName + "!";
            TapOptions();
        }

        private void TapOptions()
        {

            imgHome.Source = ImageSource.FromResource("ImDiabetic.Icons.home.png");
            imgLogs.Source = ImageSource.FromResource("ImDiabetic.Icons.logs.png");
            imgTrends.Source = ImageSource.FromResource("ImDiabetic.Icons.trends.png");
            imgMore.Source = ImageSource.FromResource("ImDiabetic.Icons.more.png");

            //Tap Gesture Recognizer  
            var homeTap = new TapGestureRecognizer();
            homeTap.Tapped += (sender, e) =>
            {
                DefaultBackground();
                stckHome.BackgroundColor = Color.DeepSkyBlue;
            };
            stckHome.GestureRecognizers.Add(homeTap);

            var logsTap = new TapGestureRecognizer();
            logsTap.Tapped += async (sender, e) =>
            {
                DefaultBackground();
                stckLogs.BackgroundColor = Color.DeepSkyBlue;
                await Navigation.PushAsync(new LogsPage());
            };
            stckLogs.GestureRecognizers.Add(logsTap);

            var trendsTap = new TapGestureRecognizer();
            trendsTap.Tapped += async (sender, e) =>
            {
                DefaultBackground();
                stckTrends.BackgroundColor = Color.DeepSkyBlue;
                await Navigation.PushAsync(new TrendsPage());
            };
            stckTrends.GestureRecognizers.Add(trendsTap);

            var moreTap = new TapGestureRecognizer();
            moreTap.Tapped += async (sender, e) =>
            {
                DefaultBackground();
                stckMore.BackgroundColor = Color.DeepSkyBlue;
                await Navigation.PushAsync(new MorePage());
            };
            stckMore.GestureRecognizers.Add(moreTap);
        }

        public void DefaultBackground()
        {
            stckHome.BackgroundColor = Color.White;
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
