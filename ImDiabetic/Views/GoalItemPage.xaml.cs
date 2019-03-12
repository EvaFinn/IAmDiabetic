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
            mySwitch.Toggled += (object sende, ToggledEventArgs ee) =>
            {
                Console.WriteLine("Switch.Toggled event sent");
            };
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
                //TODO fix: doesn't update list
            }
            else
            {
                (BindingContext as GoalsViewModel).UpdateGoal(Goal);
            }
            //await Navigation.PushAsync(new GoalsPage(User));
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as GoalsViewModel).DeleteGoal(Goal);
            //TODO fix: doesn't update list
            //await Navigation.PushAsync(new GoalsPage(User));
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    return true;
        //}
    }
}
