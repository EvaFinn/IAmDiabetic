using System;
using System.Collections.Generic;
using System.Linq;
using Realms;
using Xamarin.Forms;

namespace ImDiabetic
{
    public partial class LoginPage : ContentPage
    {
        Realm realm;

        public LoginPage()
        {
            InitializeComponent();

            var config = new RealmConfiguration() { SchemaVersion = 1 };
            realm = Realm.GetInstance(config);
            //realm = Realm.GetInstance();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var users = realm.All<User>().Where(u => u.FirstName == firstNameEntry.Text && u.Password == passwordEntry.Text);
            if (users.Count() > 0)
            {
                await Navigation.PushAsync(new DashboardPage());
            } else {
                await DisplayAlert("Alert", "User does not exist lad", "OK");
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
