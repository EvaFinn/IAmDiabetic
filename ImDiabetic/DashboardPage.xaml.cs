using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ImDiabetic
{
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();
        }

        async void Logout_Click(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
