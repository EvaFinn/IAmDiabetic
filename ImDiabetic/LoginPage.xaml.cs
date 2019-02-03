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

            var config = new RealmConfiguration() { SchemaVersion = 2 };
            realm = Realm.GetInstance(config);
            //realm = Realm.GetInstance();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var users = realm.All<User>().Where(u => u.FirstName == firstNameEntry.Text && u.Password == passwordEntry.Text);
            if (users.Count() > 0)
            {
                User user = users.FirstOrDefault();
                await Navigation.PushAsync(new MasterDetailNav(user));
            } else {
                await DisplayAlert("Alert", "User does not exist", "OK");
            }
        }

        async void Register_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
