using System;
using System.Collections.Generic;
using System.IO;
using ImDiabetic.Utils;
using ImDiabetic.ViewModels;
using Realms;
using Xamarin.Forms;


namespace ImDiabetic.Views
{
    public partial class ProfilePage : ContentPage
    {
        public User User { get; }

        public ProfilePage(User user)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            User = user;
            BindingContext = new ProfileViewModel(User);
            //profileImage.Source = ImageSource.FromResource("ImDiabetic.Icons.profile.png");
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

                profileImage.Source = ImageSource.FromStream(() => stream);
                profileImage.BackgroundColor = Color.Gray;

                TapGestureRecognizer recognizer = new TapGestureRecognizer();
                recognizer.Tapped += (sender2, args) =>
                {
                    pickPictureButton.IsEnabled = true;
                };
                profileImage.GestureRecognizers.Add(recognizer);

                //Uri myUir = Uri.
            }
            else
            {
                pickPictureButton.IsEnabled = true;
            }
        }
    }
}
