using System;
using Realms;

namespace ImDiabetic.ViewModels
{
    public class RegistrationViewModel
    {
        Realm realm;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string TargetBloodGlucose { get; set; }
        public string Password { get; set; }
        public User RegisteredUser { get; set; }

        public RegistrationViewModel()
        {
            var config = new RealmConfiguration() { SchemaVersion = 2 };
            realm = Realm.GetInstance(config);
        }

        public void AddUser()
        {
            realm.Write(() =>
            {
                var user = new User { FirstName = FirstName, LastName = LastName, Age = Age, Gender = Gender, Password = Password };
                RegisteredUser = user;
                realm.Add(user);
            });
        }
    }
}
