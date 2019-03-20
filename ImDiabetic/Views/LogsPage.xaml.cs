﻿using System;
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

        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                switch (picker.SelectedItem.ToString())
                {
                    case "Blood Glucose":
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
                    default:
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
