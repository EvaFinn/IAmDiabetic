using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

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
    }
}
