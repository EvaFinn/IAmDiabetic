﻿using System;
using ImDiabetic;
using ImDiabetic.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtoniOS))]
namespace ImDiabetic.iOS
{
    public class CustomButtoniOS : ButtonRenderer
    {
        public CustomButtoniOS()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.TitleLabel.LineBreakMode = UILineBreakMode.WordWrap;
                Control.TitleLabel.Lines = 0;
                Control.TitleLabel.TextAlignment = UITextAlignment.Center;
            }
        }
    }
}
