using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
using System.Drawing;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class GoalsPage : ContentPage
    {
        public User User { get; set; }
        public GoalsPage(User user)
        {
            InitializeComponent();
            User = user;
            this.BindingContext = new GoalsViewModel(user);
            //if ((BindingContext as GoalsViewModel).Goals != null)
            //{
            //    listView.ItemsSource = (BindingContext as GoalsViewModel).Goals;
            //}
            //checkImg.Source = ImageSource.FromResource("ImDiabetic.Icons.checked.png");
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GoalItemPage(User));
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new GoalItemPage(e.SelectedItem as Goal, User));
            }
        }

        //async void OnBack(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new DashboardPage(User));
        //}

        protected override bool OnBackButtonPressed()
        {
            //Device.BeginInvokeOnMainThread(async () =>
            //{

            //    base.OnBackButtonPressed();

            //    await Navigation.PushAsync(new DashboardPage(User));

            //});
            return false;
        }
    }
}
