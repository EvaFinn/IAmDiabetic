using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class RegistrationPage : ContentPage
    {
        Regex patdi = new Regex(@"[0-9]+");//match digits
        Regex patupp = new Regex(@"[A-Z]+"); //match upper cases
        Regex patlow = new Regex(@"[a-z]+"); //match lower cases

        public RegistrationPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            BindingContext = new RegistrationViewModel();

            firstNameEntry.ReturnCommand = new Command(() => lastNameEntry.Focus());
            lastNameEntry.ReturnCommand = new Command(() => ageEntry.Focus());
            ageEntry.ReturnCommand = new Command(() => picker.Focus());
            weightEntry.ReturnCommand = new Command(() => heightEntry.Focus());
            heightEntry.ReturnCommand = new Command(() => maxTarget.Focus());
        }
        private bool IsValidEntry(string text)
        {
            if (text == null || text.Equals("") || String.IsNullOrWhiteSpace(text))
            {
                return false;
            }
            return true;
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            (BindingContext as RegistrationViewModel).AddUser();
            AppUser user = (BindingContext as RegistrationViewModel).RegisteredUser;
            await Navigation.PushAsync(new MasterDetailNav(user));
        }

        async void Cancel_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            string text = (sender as Entry).Text;
            bool valid = IsValidEntry(text);
            string type = (sender as Entry).Placeholder;
            if (valid)
            {
                switch (type)
                {
                    case "First Name":
                        FNCheck.IsVisible = false;
                        break;
                    case "Last Name":
                        LNCheck.IsVisible = false;
                        break;
                    case "Age":
                        if (int.Parse(text) >= 12)
                        {
                            AgeCheck.IsVisible = false;
                        }
                        break;
                    case "Weight - lb":
                        WCheck.IsVisible = false;
                        break;
                    case "Height - inches":
                        HCheck.IsVisible = false;
                        break;
                    case "Password":
                        if (patdi.IsMatch(text) && patlow.IsMatch(text) && patupp.IsMatch(text))
                        {
                            PWCheck.IsVisible = false;
                            registerBtn.IsEnabled = true;
                        }

                        break;
                }
            }
        }

        void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string text = (sender as Picker).SelectedItem.ToString();
            bool valid = IsValidEntry(text);
            string type = (sender as Picker).Title;
            if (valid)
            {
                switch (type)
                {
                    case "Gender":
                        GenderCheck.IsVisible = false;
                        break;
                    case "Max Blood Glucose Target":
                        MaxCheck.IsVisible = false;
                        break;
                    case "Min Blood Glucose Target":
                        MinCheck.IsVisible = false;
                        break;
                }
            }
        }
    }
}
