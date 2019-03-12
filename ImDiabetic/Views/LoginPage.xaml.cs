﻿using System;
using System.Collections.Generic;
using System.Linq;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            if ((BindingContext as LoginViewModel).CheckUser())
            {
                AppUser user = (BindingContext as LoginViewModel).LoggedInUser;
                await Navigation.PushAsync(new MasterDetailNav(user));
            }
            else
            {
                await DisplayAlert("Alert", "User does not exist", "OK");
            }
        }

        async void Register_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        public void Delete_Clicked(object sender, System.EventArgs e)
        {
            (BindingContext as LoginViewModel).DeleteUsers();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();

        //    (BindingContext as LoginViewModel).Dispose();
        //}
    }
}
