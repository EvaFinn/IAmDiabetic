using System;
using System.Collections.Generic;
using System.Diagnostics;
using ImDiabetic.ViewModels;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
            BindingContext = new RegistrationViewModel();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            (BindingContext as RegistrationViewModel).AddUser();
            User user = (BindingContext as RegistrationViewModel).RegisteredUser;
            await Navigation.PushAsync(new MasterDetailNav(user));
        }

        async void Cancel_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
