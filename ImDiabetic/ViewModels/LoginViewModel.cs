﻿using System;
using System.Collections.Generic;
using System.Linq;
using Realms;
using Xamarin.Forms;
using static Android.Resource;

namespace ImDiabetic.ViewModels
{
    public class LoginViewModel
    {
        Realm realm;
        public string FirstName { get; private set; }
        public string Password { get; private set; }
        public User LoggedInUser { get; set; }

        public LoginViewModel()
        {
            var config = new RealmConfiguration() { SchemaVersion = 2 };
            realm = Realm.GetInstance(config);
        }

        public bool CheckUser()
        {
            var users = realm.All<User>().Where(u => u.FirstName == FirstName && u.Password == Password);
            LoggedInUser = users.FirstOrDefault();
            return users.Count() > 0;
        }
    }
}
