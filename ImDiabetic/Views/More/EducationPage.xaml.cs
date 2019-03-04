using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

namespace ImDiabetic.Views.More
{
    public partial class EducationPage : ContentPage
    {
        User User { get; set; }
        public EducationPage(User user)
        {
            InitializeComponent();
            User = user;
            this.BindingContext = new[] { "Food", "Insulin", "Hypoglycaemia", "Hyperglycaemia" };
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return;
            Debug.WriteLine("Tapped: " + e.Item);
            Navigation.PushAsync(new EducationContentPage(User, e.Item.ToString()));
            ((ListView)sender).SelectedItem = null;
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            Debug.WriteLine("Add Doc!");
            await Navigation.PushAsync(new AddEducationalContentPage());
        }
    }
}
