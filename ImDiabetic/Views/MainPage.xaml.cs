﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BackgroundImage.Source = ImageSource.FromResource("ImDiabetic.Icons.bluebackground.jpg");
            Debug.WriteLine("*********** REALM AT " + RealmConfigurationBase.GetPathToRealm());
            Debug.WriteLine("*********** DATE IS " + DateTime.Now);

        }

        async void Login_Click(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        async void Register_Click(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
