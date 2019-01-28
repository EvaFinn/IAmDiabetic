using System;
using System.Collections.Generic;
using System.Diagnostics;
using Realms;
using Xamarin.Forms;

namespace ImDiabetic
{
    public partial class RegistrationPage : ContentPage
    {
        Realm realm;
        User user;
        public RegistrationPage()
        {
            InitializeComponent();

            var config = new RealmConfiguration() { SchemaVersion = 2 };
            realm = Realm.GetInstance(config);
            //realm = Realm.GetInstance();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            AddUser();
            await Navigation.PushAsync(new DashboardPage(user));
        }

        async void Cancel_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void AddUser()
        {
            String firstName = firstNameEntry.Text;
            String lastName = lastNameEntry.Text;
            String age = ageEntry.Text; 
            String gender = (string)picker.ItemsSource[picker.SelectedIndex];

            String password = passwordEntry.Text;

            realm.Write(() =>
            {
                user = new User { FirstName = firstName, LastName = lastName, Age = age, Gender = gender, Password = password };
                realm.Add(user);
            });
        }
    }
}
