using System;
using System.Collections.Generic;
using Entry = Microcharts.Entry;

using Xamarin.Forms;
using SkiaSharp;
using Microcharts;
using ImDiabetic.ViewModels;

namespace ImDiabetic.Views
{
    public partial class TrendPage : ContentPage
    {
        public User User { get; }
        public TrendPage(User user)
        {
            InitializeComponent();
            User = user;
            this.BindingContext = new TrendViewModel(user);
        }
    }
}
