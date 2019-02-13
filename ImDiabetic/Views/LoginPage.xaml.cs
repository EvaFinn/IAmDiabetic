using System;
using System.Collections.Generic;
using System.Linq;
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
                User user = (BindingContext as LoginViewModel).LoggedInUser;
                //user.LastLogInDate = DateTimeOffset.Now;
                //(BindingContext as LoginViewModel).DailyStreak();
                //await Navigation.PushAsync(new TrendPage(user));
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

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
