using System;
using System.Collections.Generic;
using Realms;
using Xamarin.Forms;

namespace ImDiabetic
{
    public partial class ProfilePage : ContentPage
    {
        Realm realm;
        public User User { get; }

        public ProfilePage(User user)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            var config = new RealmConfiguration() { SchemaVersion = 2 };
            realm = Realm.GetInstance(config);
            User = user;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
