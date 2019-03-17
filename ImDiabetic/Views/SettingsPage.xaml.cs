using System;
using ImDiabetic.Models;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class SettingsPage : ContentPage
    {
        public AppUser User { get; set; }

        public SettingsPage(AppUser user)
        {
            InitializeComponent();
            User = user;
        }
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new RemindersListPage(User));
        }
    }
}
