using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ImDiabetic.Models;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;

namespace ImDiabetic.ViewModels
{
    public class EducationContentViewModel
    {
        public string Topic { get; set; }
        public string Test { get; set; }
        public AppUser User { get; set; }
        public List<string> Items { get; set; } = new List<string>();
        public Stream MyStream { get; set; }

        public EducationContentViewModel(AppUser user, string topic)
        {
            User = user;
            Topic = topic;

            Items.Add("Food");
            Items.Add("Insulin");
            Items.Add("Blood Glucose Monitoring");
            Items.Add("Hypoglycaemia");
            Items.Add("High Blood Sugar");
        }

        async public void PickFile()
        {
            FileData fileData = new FileData();
            fileData = await CrossFilePicker.Current.PickFile();

            if (fileData != null)
            {
                byte[] data = fileData.DataArray;
                string name = fileData.FileName;
                string filePath = fileData.FilePath;
                Test = name;

                Items.Add(name);
                MyStream = new MemoryStream(data);

                foreach (string item in Items)
                {
                    Debug.WriteLine("ITEM : " + item);
                }
            }
        }
    }
}
