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
