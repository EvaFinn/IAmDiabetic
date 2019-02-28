using System;
using System.Collections.Generic;
using Realms;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class ProfilePage : ContentPage
    {
        public User User { get; }

        public ProfilePage(User user)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            User = user;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
