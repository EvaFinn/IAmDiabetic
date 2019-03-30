using System;
using System.Collections.Generic;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class GoalItemPage : ContentPage
    {
        public AppUser User { get; set; }
        public Goal Goal { get; set; }
        private bool IsNew { get; set; }
        public GoalItemPage(AppUser user)
        {
            InitializeComponent();
            User = user;
            IsNew = true;
            this.BindingContext = new GoalsViewModel(User);
        }

        public GoalItemPage(Goal goal, AppUser user)
        {
            InitializeComponent();
            User = user;
            Goal = goal;
            IsNew = false;
            this.BindingContext = new GoalsViewModel(User, Goal);
        }

        async private void Update()
        {
            (BindingContext as GoalsViewModel).UpdateGoal(Goal);
            await Navigation.PopAsync();
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            if (IsNew)
            {
                (BindingContext as GoalsViewModel).SaveGoal();
            }
            else
            {
                (BindingContext as GoalsViewModel).UpdateGoal(Goal);
            }
            await Navigation.PushAsync(new GoalsPage(User));
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as GoalsViewModel).DeleteGoal(Goal);
            await Navigation.PushAsync(new GoalsPage(User));
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
