using System;
using System.Collections.Generic;
using Xamarin.Forms;
using ImDiabetic.ViewModels;
using Syncfusion.SfChart.XForms;
using ImDiabetic.Models;

namespace ImDiabetic.Views
{
    public partial class TrendPage : ContentPage
    {
        public AppUser User { get; }
        List<Color> customColors;

        public TrendPage(AppUser user)
        {
            InitializeComponent();
            User = user;
            this.BindingContext = new TrendViewModel(User);
            UIForChart();
        }

        public void UIForChart()
        {
            customColors = new List<Color>
            {
                Color.FromHex("#f44141"),    //red
                Color.FromHex("#56f442"),   //green
                Color.FromHex("#f4d041")    //yellow 
            };

            BGChart.ColorModel.Palette = ChartColorPalette.Custom;
            BGChart.ColorModel.CustomBrushes = customColors;
        }
    }
}
