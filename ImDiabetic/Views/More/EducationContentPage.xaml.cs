using System;
using System.Collections.Generic;
using System.IO;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
using Xamarin.Forms;

namespace ImDiabetic.Views.More
{
    public partial class EducationContentPage : ContentPage
    {
        public EducationContentPage(AppUser user, string topic)
        {
            InitializeComponent();
            this.BindingContext = new PdfViewerViewModel(topic);
        }

        public EducationContentPage(AppUser user, Stream stream)
        {
            InitializeComponent();
            this.BindingContext = new PdfViewerViewModel(stream);
        }
    }
}
