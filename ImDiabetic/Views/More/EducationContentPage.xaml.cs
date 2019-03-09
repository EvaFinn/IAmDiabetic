using System;
using System.Collections.Generic;
using System.IO;
using ImDiabetic.ViewModels;
using Xamarin.Forms;

namespace ImDiabetic.Views.More
{
    public partial class EducationContentPage : ContentPage
    {
        public EducationContentPage(User user, string topic)
        {
            InitializeComponent();
            this.BindingContext = new PdfViewerViewModel("Hypoglycaemia");
        }

        public EducationContentPage(User user, Stream stream)
        {
            InitializeComponent();
            this.BindingContext = new PdfViewerViewModel(stream);
        }
    }
}
