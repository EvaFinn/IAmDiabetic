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

namespace ImDiabetic.Views.More
{
    public partial class EducationPage : ContentPage
    {
        User User { get; set; }
        public EducationPage(User user)
        {
            InitializeComponent();
            User = user;
            this.BindingContext = new EducationContentViewModel(user, "topic");
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

        async private void On_Click(object sender, System.EventArgs e)
        {
            try
            {
                //(BindingContext as EducationContentViewModel).PickFile();
                FileData fileData = new FileData();
                fileData = await CrossFilePicker.Current.PickFile();

                if (fileData != null)
                {
                    byte[] data = fileData.DataArray;
                    string name = fileData.FileName;
                    string filePath = fileData.FilePath;
                    (BindingContext as EducationContentViewModel).Test = name;

                    Debug.WriteLine("File name is: " + name);
                    Debug.WriteLine("File Path : " + filePath);

                    (BindingContext as EducationContentViewModel).Items.Add(name);
                    Stream MyStream = new MemoryStream(data);
                    await Navigation.PushAsync(new EducationContentPage(User, MyStream));

                    foreach (string item in (BindingContext as EducationContentViewModel).Items)
                    {
                        Debug.WriteLine("ITEM : " + item);
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                ex.ToString();
            }
        }
    }
}
