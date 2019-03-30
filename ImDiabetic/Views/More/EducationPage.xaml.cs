using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Plugin.FilePicker.Abstractions;
using Plugin.FilePicker;
using System.IO;
using ImDiabetic.ViewModels;
using ImDiabetic.Models;

namespace ImDiabetic.Views.More
{
    public partial class EducationPage : ContentPage
    {
        AppUser User { get; set; }
        public EducationPage(AppUser user)
        {
            InitializeComponent();
            User = user;
            this.BindingContext = new EducationContentViewModel();
            var template = new DataTemplate(typeof(TextCell));
            template.SetValue(TextCell.TextColorProperty, Color.DeepSkyBlue);
            template.SetBinding(TextCell.TextProperty, ".");
            listView.ItemTemplate = template;
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return;
            Navigation.PushAsync(new EducationContentPage(User, e.Item.ToString()));
            ((ListView)sender).SelectedItem = null;
        }

        async private void On_Click(object sender, System.EventArgs e)
        {
            try
            {
                FileData fileData = new FileData();
                fileData = await CrossFilePicker.Current.PickFile();

                if (fileData != null)
                {
                    byte[] data = fileData.DataArray;
                    Stream MyStream = new MemoryStream(data);
                    await Navigation.PushAsync(new EducationContentPage(User, MyStream));
                }
            }
            catch (InvalidOperationException ex)
            {
                ex.ToString();
            }
        }
    }
}
