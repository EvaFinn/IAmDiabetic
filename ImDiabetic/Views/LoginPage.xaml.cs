using System;
using System.Collections.Generic;
using System.Linq;
using ImDiabetic.ViewModels;
using Realms;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class LoginPage : ContentPage
    {
        private Realm realm;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();

            var config = new RealmConfiguration() { SchemaVersion = 2 };
            realm = Realm.GetInstance(config);
            //realm = Realm.GetInstance();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            if ((BindingContext as LoginViewModel).CheckUser())
            {
                User user = (BindingContext as LoginViewModel).LoggedInUser;
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
