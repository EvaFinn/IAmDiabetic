using System;
using System.Collections.Generic;
using ImDiabetic.ViewModels;
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
            BindingContext = new ProfileViewModel(User);
            profileImage.Source = ImageSource.FromResource("ImDiabetic.Icons.profile.png");
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
