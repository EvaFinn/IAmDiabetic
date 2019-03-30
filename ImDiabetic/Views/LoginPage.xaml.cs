using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Acr.UserDialogs;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
using ImDiabetic.Views.More;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BackgroundImage.Source = ImageSource.FromResource("ImDiabetic.Icons.bluebackground.jpg");
            BindingContext = new LoginViewModel();

            var privacyTap = new TapGestureRecognizer();
            privacyTap.Tapped += (sender, e) =>
            {
                Navigation.PushAsync(new EducationContentPage("PrivacyAgreement"));
                Debug.WriteLine("View it!");
            };
            PrivacyStack.GestureRecognizers.Add(privacyTap);
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

        public void Register_Clicked(object sender, System.EventArgs e)
        {

            //await Navigation.PushAsync(new RegistrationPage());
            PrivacyAgree();
        }

        async private void PrivacyAgree()
        {
            bool share = await UserDialogs.Instance.ConfirmAsync("By using this app, you agree to the privacy agreement along with the terms and conditions.", "Privacy", "Ok", "No thanks");
            if (share == true)
            {
                await Navigation.PushAsync(new RegistrationPage());
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
