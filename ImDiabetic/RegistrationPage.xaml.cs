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
        public RegistrationPage()
        {
            InitializeComponent();

            var config = new RealmConfiguration() { SchemaVersion = 1 };
            realm = Realm.GetInstance(config);
            //realm = Realm.GetInstance();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            AddUser();
            await Navigation.PushAsync(new DashboardPage());
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

            int selectedIndex = picker.SelectedIndex;
            String gender = (string)picker.ItemsSource[selectedIndex];
            Debug.WriteLine("********Pick " + selectedIndex + " and " + gender);

            String password = passwordEntry.Text;

            realm.Write(() =>
            {
                var user = new User { FirstName = firstName, LastName = lastName, Age = age, Gender = gender, Password = password };
                realm.Add(user);
            });
        }
    }
}
