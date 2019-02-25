using System;
using System.Collections.Generic;
using ImDiabetic.ViewModels;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class EducationContentPage : ContentPage
    {
        public EducationContentPage(User user, string topic)
        {
            InitializeComponent();
            //this.BindingContext = new EducationContentViewModel(user, topic);
            this.BindingContext = new PdfViewerViewModel();
        }
    }
}
