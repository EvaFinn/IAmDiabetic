using System;
using System.Collections.Generic;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class RemindersListPage : ContentPage
    {
        public AppUser User { get; set; }
        public RemindersListPage(AppUser user)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            User = user;
            this.BindingContext = new RemindersViewModel(User);
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return;
            Navigation.PushAsync(new RemindersPage(User, e.Item as Reminders));
            ((ListView)sender).SelectedItem = null;
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new RemindersPage(User));
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

    }
}
