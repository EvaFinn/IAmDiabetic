﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ImDiabetic.Models;
using ImDiabetic.Utils;
using ImDiabetic.ViewModels;
using Realms;
using Xamarin.Forms;


namespace ImDiabetic.Views
{
    public partial class ProfilePage : ContentPage
    {
        public AppUser User { get; }

        public ProfilePage(AppUser user)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            User = user;
            BindingContext = new ProfileViewModel(User);
            profileImage.Source = ImageSource.FromStream(() => new MemoryStream((BindingContext as ProfileViewModel).Photo));
            backgroundImage.Source = ImageSource.FromResource("ImDiabetic.Icons.blue.jpg");
            progressbar.IsVisible = true;
            flameImage.Source = ImageSource.FromResource("ImDiabetic.Utils.flame.png");

        }

        void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string text = (sender as Picker).SelectedItem.ToString();
            (BindingContext as ProfileViewModel).UpdatePet(text);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        async private void Handle_Clicked(object sender, System.EventArgs e)
        {
            pickPictureButton.IsEnabled = false;
            Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();

            if (stream != null)
            {
                (BindingContext as ProfileViewModel).ReadFully(stream);
                profileImage.Source = ImageSource.FromStream(() => new MemoryStream((BindingContext as ProfileViewModel).Photo));
                profileImage.BackgroundColor = Color.White;

                TapGestureRecognizer recognizer = new TapGestureRecognizer();
                recognizer.Tapped += (sender2, args) =>
                {
                    pickPictureButton.IsEnabled = true;
                };
                profileImage.GestureRecognizers.Add(recognizer);
            }
            else
            {
                pickPictureButton.IsEnabled = true;
            }
        }
    }
}
