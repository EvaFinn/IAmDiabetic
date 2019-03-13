using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
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
            bloodglucose.Value = (BindingContext as LogsViewModel).LastBloogGlucoseLog;


            insulinEntry.IsVisible = false;
            insulinLabel.IsVisible = false;
            carbsEntry.IsVisible = false;
            carbsLabel.IsVisible = false;
            scanBtn.IsVisible = false;
            bgLabel.IsVisible = false;
            bgThing.IsVisible = false;
            saveBtn.IsEnabled = false;

        }

        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                if (picker.SelectedItem.ToString() == "Blood Glucose")
                {
                    insulinEntry.IsVisible = false;
                    insulinLabel.IsVisible = false;
                    carbsEntry.IsVisible = false;
                    carbsLabel.IsVisible = false;
                    scanBtn.IsVisible = false;
                    bgLabel.IsVisible = true;
                    bgThing.IsVisible = true;
                    saveBtn.IsEnabled = true;
                }
                else if (picker.SelectedItem.ToString() == "Carbs")
                {
                    insulinEntry.IsVisible = false;
                    insulinLabel.IsVisible = false;
                    bgLabel.IsVisible = false;
                    bgThing.IsVisible = false;
                    carbsEntry.IsVisible = true;
                    carbsLabel.IsVisible = true;
                    scanBtn.IsVisible = true;
                    saveBtn.IsEnabled = true;
                }
                else
                {
                    insulinEntry.IsVisible = true;
                    insulinLabel.IsVisible = true;
                    bgLabel.IsVisible = false;
                    bgThing.IsVisible = false;
                    carbsEntry.IsVisible = false;
                    carbsLabel.IsVisible = false;
                    scanBtn.IsVisible = false;
                    saveBtn.IsEnabled = true;
                }
            }
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (typepicker.SelectedItem != null)
            {
                (BindingContext as LogsViewModel).AddLog();
                await Navigation.PushAsync(new MasterDetailNav(User));
            }
            else
            {
                await DisplayAlert("Error", "Must set log type", "OK");
            }
        }

        private async void ScanClick(object sender, EventArgs e)
        {
            var ScannerPage = new ZXingScannerPage();

            ScannerPage.OnScanResult += (result) =>
            {
                ScannerPage.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    await DisplayAlert("Scanned Barcode", result.Text, "OK");
                });
            };
            await Navigation.PushAsync(ScannerPage);
        }
    }
}
