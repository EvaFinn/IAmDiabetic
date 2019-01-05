﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;
using Xamarin.Forms;

namespace ImDiabetic
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Debug.WriteLine("*********** REALM AT " + RealmConfigurationBase.GetPathToRealm());
        }

        async void Login_Click(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        async void Register_Click(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}
