using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Acr.UserDialogs;
using ImDiabetic.Models;

namespace ImDiabetic.ViewModels
{
    public class RegistrationViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public double MaxTarget { get; set; }
        public double MinTarget { get; set; }
        public string Password { get; set; }
        public AppUser RegisteredUser { get; set; }

        public void AddUser()
        {
            realm.Write(() =>
            {
                var user = new AppUser
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Age = Age,
                    Gender = Gender,
                    Weight = Weight,
                    Height = Height,
                    MaxTargetBloodGlucose = MaxTarget,
                    MinTargetBloodGlucose = MinTarget,
                    Password = Password
                };
                RegisteredUser = user;
                realm.Add(user);
            });
        }
    }
}
