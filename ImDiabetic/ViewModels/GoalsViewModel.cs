using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ImDiabetic.Models;

namespace ImDiabetic.ViewModels
{
    public class GoalsViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public AppUser User { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }
        public string Notes { get; set; }
        private List<Goal> _goals = new List<Goal>();
        public List<Goal> Goals
        {
            get
            {
                return _goals;
            }
            set
            {
                _goals = value;
                NotifyPropertyChanged("Goals");
            }
        }

        public Goal Goal { get; set; } 
        public string Text { get; set; }

        public GoalsViewModel(AppUser user)
        {
            User = user;
            GetGoals();
        }

        public GoalsViewModel(AppUser user, Goal goal)
        {
            User = user;
            GetGoals();
            Name = goal.Name;
            Notes = goal.Notes;
            Done = goal.Done;
        }

        private void GetGoals()
        {
            var goals = realm.All<Goal>().Where(g => g.UserID == User.Id);
            if (goals.Count() > 0) {
                foreach (Goal goal in goals)
                {
                    _goals.Add(goal);
                }
                Text = "Goals";
            } else { Text = "No Goals Yet."; }
        }

        public void SaveGoal() {
            var newGoal = new Goal { UserID = User.Id, Date = DateTimeOffset.Now,
                Name = Name, Notes = Notes, Done = Done };

            realm.Write(() =>
            {
                Goal = newGoal;
                realm.Add(newGoal);
            });
            GetGoals();
        }

        public void DeleteGoal(Goal goal) {
            realm.Write(() =>
            {
                realm.Remove(goal);
            });
            GetGoals();
        }

        public void UpdateGoal(Goal goal)
        {
            realm.Write(() =>
            {
                goal.Name = Name;
                goal.Notes = Notes;
                goal.Done = Done;
            });
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
