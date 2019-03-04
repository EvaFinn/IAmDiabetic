using System;
using System.Collections.Generic;
using ImDiabetic.ViewModels;
using Xamarin.Forms;

namespace ImDiabetic.Views.More
{
    public partial class EducationContentPage : ContentPage
    {
        public EducationContentPage(User user, string topic)
        {
            InitializeComponent();
            //TODO allow user upload their own content.
            //this.BindingContext = new EducationContentViewModel(user, topic);
            this.BindingContext = new PdfViewerViewModel();
        }
    }
}
