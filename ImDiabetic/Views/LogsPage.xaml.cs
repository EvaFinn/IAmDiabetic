using System;
using System.Collections.Generic;
using System.Linq;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class LogsPage : ContentPage
    {
        public User User { get; }

        public LogsPage(User user)
        {
            InitializeComponent();
            BindingContext = new LogsViewModel(user);
            User = (BindingContext as LogsViewModel).User;
            bloodglucose.Value = (BindingContext as LogsViewModel).LastBloogGlucoseLog;
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            (BindingContext as LogsViewModel).AddLog();
            await Navigation.PushAsync(new MasterDetailNav(User));
        }
    }
}
