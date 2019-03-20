using System;
using System.Collections.ObjectModel;
using Syncfusion.SfPicker.XForms;
using Xamarin.Forms;

namespace ImDiabetic.Utils
{
    public class CustomPicker : SfPicker
    {
        public ObservableCollection<object> Time { get; set; }
        public ObservableCollection<object> Minute;
        public ObservableCollection<object> Hour;
        public ObservableCollection<string> Headers { get; set; }

        public CustomPicker()
        {
            Time = new ObservableCollection<object>();
            Hour = new ObservableCollection<object>();
            Minute = new ObservableCollection<object>();

            Headers = new ObservableCollection<string>();
            if (Device.RuntimePlatform == Device.Android)
            {
                Headers.Add("HOUR");
                Headers.Add("MINUTE");
            } else
            {
                Headers.Add("Hour");
                Headers.Add("Minute");
            }
            HeaderText = "TIME PICKER";
            this.ColumnHeaderText = Headers;
            PopulateTimeCollection();
            this.ItemsSource = Time;
            ShowFooter = true;
            ShowHeader = true;
            ShowColumnHeader = true;
        }

        private void PopulateTimeCollection()
        {
            for (int i = 0; i <= 12; i++)
            {
                Hour.Add(i.ToString());
            }
            for (int j = 0; j < 60; j++)
            {
                if (j < 10)
                {
                    Minute.Add("0" + j);
                }
                else
                    Minute.Add(j.ToString());
            }
            Time.Add(Hour);
            Time.Add(Minute);
        }
    }
}
