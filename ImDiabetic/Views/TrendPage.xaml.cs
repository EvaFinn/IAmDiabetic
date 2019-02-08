using System;
using System.Collections.Generic;
using Entry = Microcharts.Entry;

using Xamarin.Forms;
using SkiaSharp;
using Microcharts;

namespace ImDiabetic.Views
{
    public partial class TrendPage : ContentPage
    {
        public TrendPage()
        {
            InitializeComponent();
            List<Entry> entries = new List<Entry>
            {
                new Entry(200)
                {
                    Color=SKColor.Parse("#FF1943"),
                    Label ="January",
                    ValueLabel = "200"
                },
                new Entry(400)
                {
                    Color = SKColor.Parse("00BFFF"),
                    Label = "March",
                    ValueLabel = "400"
                },
                new Entry(-100)
                {
                    Color =  SKColor.Parse("#00CED1"),
                    Label = "Octobar",
                    ValueLabel = "-100"
                },
            };

            Chart3.Chart = new LineChart() { 
                Entries = entries,
                LineMode = LineMode.Straight,
                LineSize = 8,
                PointMode = PointMode.Square,
                PointSize = 18
            };

        }
    }
}
