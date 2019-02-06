using System;
using System.Collections.Generic;
using System.Linq;
using ImDiabetic.Models;
using Realms;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class LogsPage : ContentPage
    {
        Realm realm;
        public User User { get; }

        public LogsPage(User user)
        {
            InitializeComponent();
            var config = new RealmConfiguration() { SchemaVersion = 2 };
            realm = Realm.GetInstance(config);
            User = user;
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            AddLog();
        }

        private void AddLog()
        {
            String bloodGluse = bloodGlucoseEntry.Text;
            String insulin = insulinEntry.Text;
            String pills = pillEntry.Text;
            String activity = activityEntry.Text;
            String carbs = carbsEntry.Text;


            realm.Write(() =>
            {
                Log log = new Log { UserId = User.Id, LogDate = DateTime.Now, BloodGlucose = bloodGluse, Insulin = insulin, Pills = pills, Carbs = carbs, Activity = activity };
                realm.Add(log);
            });
        }
    }
}
