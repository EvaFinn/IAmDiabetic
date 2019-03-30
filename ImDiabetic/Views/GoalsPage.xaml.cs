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
        public AppUser User { get; set; }
        public GoalsPage(AppUser user)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            if (Device.RuntimePlatform == Device.iOS) Padding = new Thickness(0, 30, 0, 0);
            InitializeComponent();
            User = user;
            this.BindingContext = new GoalsViewModel(User);
        }

        protected override void OnAppearing()
        {
            listView.ItemsSource = (BindingContext as GoalsViewModel).Goals;
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
            ((ListView)sender).SelectedItem = null;
        }

        async void OnBack(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
