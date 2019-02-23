using System;
using System.Diagnostics;
using System.Linq;
using ImDiabetic.Models;
using Realms;

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
                var user = new User
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

            var uuu = realm.All<User>();
            foreach (User use in uuu)
            {
                Debug.WriteLine("NAME " + use.FirstName + ",,," + use.Password);
            }

            var question = new QuizQuestion { Question = "Question?", Topic = "Food", Answer = "Answer", OptionOne = "Answer", OptionTwo = "Not Answer 2", OptionThree = "Not Answer 3" };
            var quiz = new Quiz { Question = question, UserId = RegisteredUser.Id, Score = "0" };

            realm.Write(() => {
                realm.Add(question);
                realm.Add(quiz);
            });

        }
    }
}
