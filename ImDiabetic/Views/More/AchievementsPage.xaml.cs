using System;
using System.Collections.Generic;
using System.Diagnostics;
using ImDiabetic.ViewModels;
using Xamarin.Forms;
using System.Linq;
using System.Reflection;
using ImDiabetic.Models;
using Newtonsoft.Json;
using System.IO;

namespace ImDiabetic.Views.More
{
    public partial class AchievementsPage : ContentPage
    {
        public AppUser User { get; set; }
        public AchievementsPage(AppUser user)
        {
            InitializeComponent();
            User = user;
            string[] Achievements = GetAchievements();
            this.BindingContext = Achievements.Distinct().ToArray();
            var template = new DataTemplate(typeof(TextCell));
            template.SetValue(TextCell.TextColorProperty, Color.DeepSkyBlue);
            template.SetBinding(TextCell.TextProperty, ".");
            listView.ItemTemplate = template;
            image.Source = ImageSource.FromResource("ImDiabetic.Icons.trophiesStep.png");
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return;
            Debug.WriteLine("Tapped: " + e.Item);
            Navigation.PushAsync(new SingleAchievementsPage(User, e.Item.ToString()));
            ((ListView)sender).SelectedItem = null;
        }

        private string[] GetAchievements()
        {
            List<Achievement> jsonresult;
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Achievement)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("ImDiabetic.achievements.json");
            string json = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }
            jsonresult = JsonConvert.DeserializeObject<List<Achievement>>(json);

            string[] acheivements = new string[jsonresult.Count];

            for (int i = 0; i < jsonresult.Count; i++)
            {
                acheivements[i] = jsonresult.ElementAt(i).Name;
                Debug.WriteLine("Name : " + acheivements[i]);
            }
            return acheivements;
        }
    }
}
