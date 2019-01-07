using System;
using System.Collections.Generic;
using Realms;
using Xamarin.Forms;

namespace ImDiabetic
{
    public partial class LogsPage : ContentPage
    {
        Realm realm;
        public User User { get; }

        public LogsPage(User user)
        {
            InitializeComponent();
            var config = new RealmConfiguration() { SchemaVersion = 1 };
            realm = Realm.GetInstance(config);
            User = user;
        }
    }
}
