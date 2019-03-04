using System;
using System.Collections.Generic;
using ImDiabetic.Models;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class GoalItemPage : ContentPage
    {
        public GoalItemPage()
        {
            InitializeComponent();
        }

        public GoalItemPage(Goal goal)
        {
            InitializeComponent();
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            //viewmodel save goal
            //var todoItem = (TodoItem)BindingContext;
            //await App.Database.SaveItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            //view model delete goal
            //var todoItem = (TodoItem)BindingContext;
            //await App.Database.DeleteItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
