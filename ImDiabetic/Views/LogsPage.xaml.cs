using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
using Nutritionix.Standard;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;


namespace ImDiabetic.Views
{
    public partial class LogsPage : ContentPage
    {
        public AppUser User { get; }

        public LogsPage(AppUser user)
        {
            InitializeComponent();
            BindingContext = new LogsViewModel(user);
            User = (BindingContext as LogsViewModel).User;
            bgThing.Value = (BindingContext as LogsViewModel).LastBloogGlucoseLog;
            timeStep.Value = (BindingContext as LogsViewModel).LastActivityLog;

            insulinEntry.IsVisible = false;
            insulinLabel.IsVisible = false;
            calEntry.IsVisible = false;
            calLabel.IsVisible = false;
            carbsEntry.IsVisible = false;
            carbsLabel.IsVisible = false;
            scanBtn.IsVisible = false;
            bgLabel.IsVisible = false;
            bgThing.IsVisible = false;
            saveBtn.IsEnabled = false;
            timeStep.IsVisible = false;
            activityLabel.IsVisible = false;
        }

        private void RadioButton_StateChanged(object sender, StateChangedEventArgs e)
        {
            if (e.IsChecked.HasValue && e.IsChecked.Value)
            {
                ChooseLabel.IsVisible = false;
                string text = (sender as SfRadioButton).Text;
                switch (text)
                {
                    case "Blood Glucose":
                        (BindingContext as LogsViewModel).LogType = "Blood Glucose";
                        insulinEntry.IsVisible = false;
                        insulinLabel.IsVisible = false;
                        carbsEntry.IsVisible = false;
                        carbsLabel.IsVisible = false;
                        calEntry.IsVisible = false;
                        calLabel.IsVisible = false;
                        scanBtn.IsVisible = false;
                        bgLabel.IsVisible = true;
                        bgThing.IsVisible = true;
                        saveBtn.IsEnabled = true;
                        timeStep.IsVisible = false;
                        activityLabel.IsVisible = false;
                        break;
                    case "Food Item":
                        (BindingContext as LogsViewModel).LogType = "Food Item";
                        insulinEntry.IsVisible = false;
                        insulinLabel.IsVisible = false;
                        bgLabel.IsVisible = false;
                        bgThing.IsVisible = false;
                        carbsEntry.IsVisible = true;
                        carbsLabel.IsVisible = true;
                        calEntry.IsVisible = true;
                        calLabel.IsVisible = true;
                        scanBtn.IsVisible = true;
                        saveBtn.IsEnabled = true;
                        timeStep.IsVisible = false;
                        activityLabel.IsVisible = false;
                        break;
                    case "Activity":
                        (BindingContext as LogsViewModel).LogType = "Activity";
                        insulinEntry.IsVisible = false;
                        insulinLabel.IsVisible = false;
                        calEntry.IsVisible = false;
                        calLabel.IsVisible = false;
                        carbsEntry.IsVisible = false;
                        carbsLabel.IsVisible = false;
                        scanBtn.IsVisible = false;
                        bgLabel.IsVisible = false;
                        bgThing.IsVisible = false;
                        saveBtn.IsEnabled = true;
                        timeStep.IsVisible = true;
                        activityLabel.IsVisible = true;
                        break;
                    case "Insulin":
                        (BindingContext as LogsViewModel).LogType = "Insulin";
                        insulinEntry.IsVisible = true;
                        insulinLabel.IsVisible = true;
                        bgLabel.IsVisible = false;
                        bgThing.IsVisible = false;
                        carbsEntry.IsVisible = false;
                        carbsLabel.IsVisible = false;
                        calEntry.IsVisible = false;
                        calLabel.IsVisible = false;
                        scanBtn.IsVisible = false;
                        saveBtn.IsEnabled = true;
                        timeStep.IsVisible = false;
                        activityLabel.IsVisible = false;
                        break;
                    case "Medication":
                        (BindingContext as LogsViewModel).LogType = "Medication";
                        insulinEntry.IsVisible = true;
                        insulinLabel.IsVisible = true;
                        bgLabel.IsVisible = false;
                        bgThing.IsVisible = false;
                        carbsEntry.IsVisible = false;
                        carbsLabel.IsVisible = false;
                        calEntry.IsVisible = false;
                        calLabel.IsVisible = false;
                        scanBtn.IsVisible = false;
                        saveBtn.IsEnabled = true;
                        timeStep.IsVisible = false;
                        activityLabel.IsVisible = false;
                        break;
                }
            }
        }


        public async void Handle_Clicked(object sender, System.EventArgs e)
        {
            (BindingContext as LogsViewModel).AddLog();
            await Navigation.PushAsync(new MasterDetailNav(User));
        }

        public async void ScanClick(object sender, EventArgs e)
        {
            var ScannerPage = new ZXingScannerPage();
            ScannerPage.OnScanResult += (result) =>
            {
                ScannerPage.IsScanning = false;
                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();
                    (BindingContext as LogsViewModel).AddFoodByBarcode(result.Text);
                    calEntry.Text = (BindingContext as LogsViewModel).Calorie;
                    carbsEntry.Text = (BindingContext as LogsViewModel).Carbs;
                });
            };
            await Navigation.PushAsync(ScannerPage);
        }
    }
}
