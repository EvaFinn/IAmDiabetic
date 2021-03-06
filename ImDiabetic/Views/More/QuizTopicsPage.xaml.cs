﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using ImDiabetic.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ImDiabetic.Views.More
{
    public partial class QuizTopicsPage : ContentPage
    {
        AppUser User { get; set; }
        public QuizTopicsPage(AppUser user)
        {
            InitializeComponent();
            User = user;
            string[] questionTopics = GetQustionTopics();
            this.BindingContext = questionTopics.Distinct().ToArray();
            var template = new DataTemplate(typeof(TextCell));
            template.SetValue(TextCell.TextColorProperty, Color.DeepSkyBlue);
            template.SetBinding(TextCell.TextProperty, ".");
            listView.ItemTemplate = template;
        }

        private string[] GetQustionTopics()
        {
            List<QuizQuestion> jsonresult;
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(QuizQuestion)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("ImDiabetic.quizquestion.json");
            string json = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }
            jsonresult = JsonConvert.DeserializeObject<List<QuizQuestion>>(json);

            string[] questionTopics = new string[jsonresult.Count];

            for (int i = 0; i < jsonresult.Count; i++)
            {
                questionTopics[i] = jsonresult.ElementAt(i).Topic;
            }
            return questionTopics;
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return;
            Navigation.PushAsync(new QuizPage(User, e.Item.ToString()));
            ((ListView)sender).SelectedItem = null;
        }
    }
}
