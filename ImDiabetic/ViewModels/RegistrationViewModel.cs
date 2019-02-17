﻿using System;

namespace ImDiabetic.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
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
        public User RegisteredUser { get; set; }

        public RegistrationViewModel()
        {

        }

        public void AddUser()
        {
            realm.Write(() =>
            {
                var user = new User { 
                    FirstName = FirstName, LastName = LastName, Age = Age, 
                    Gender = Gender, Weight = Weight, Height = Height,
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
