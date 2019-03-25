using System;
using System.Collections.Generic;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
using Xamarin.Forms;

namespace ImDiabetic.Views.More
{
    public partial class SingleAchievementsPage : ContentPage
    {
        public SingleAchievementsPage(AppUser user, string achievement)
        {
            InitializeComponent();
            this.BindingContext = new AchievementsViewModel(user, achievement);
            image.Source = ImageSource.FromResource("ImDiabetic.Icons.trophy.png");
        }
    }
}
