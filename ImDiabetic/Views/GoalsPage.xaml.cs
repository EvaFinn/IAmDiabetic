using System;
using System.Collections.Generic;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
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
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GoalItemPage());
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new GoalItemPage(e.SelectedItem as Goal));
            }
        }
    }
}
