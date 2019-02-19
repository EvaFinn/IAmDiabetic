using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using ImDiabetic.ViewModels;
using ImDiabetic.Views.More;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class MorePage : ContentPage
    {
        User User { get; set; }
        public MorePage(User user)
        {
            InitializeComponent();
            User = user;
            this.BindingContext = new MoreViewModel(this.Navigation, user);
            Btn1.ImageSource = ImageSource.FromResource("ImDiabetic.Icons.medal.png");
            Btn2.ImageSource = ImageSource.FromResource("ImDiabetic.Icons.edu.png");
            Btn3.ImageSource = ImageSource.FromResource("ImDiabetic.Icons.quiz.png");
        }
    }
}
