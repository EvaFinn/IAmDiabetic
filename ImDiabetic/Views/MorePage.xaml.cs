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
        }
    }
}
