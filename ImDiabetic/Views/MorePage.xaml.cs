using System;
using System.Collections.Generic;
using ImDiabetic.ViewModels;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class MorePage : ContentPage
    {
        public MorePage(User user)
        {
            InitializeComponent();
            this.BindingContext = new MoreViewModel(user);
            Btn1.ImageSource = ImageSource.FromResource("ImDiabetic.Icons.medal.png");
            Btn2.ImageSource = ImageSource.FromResource("ImDiabetic.Icons.edu.png");
            Btn3.ImageSource = ImageSource.FromResource("ImDiabetic.Icons.quiz.png");
        }

        public MorePage() {
            InitializeComponent();
        }
    }
}
