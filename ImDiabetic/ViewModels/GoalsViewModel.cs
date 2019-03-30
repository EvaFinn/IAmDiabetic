using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ImDiabetic.Models;

namespace ImDiabetic.ViewModels
{
    public class GoalsViewModel : BaseViewModel
    {
        public AppUser User { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }
        public string Notes { get; set; }
        public Goal Goal { get; set; }
        public string Text { get; set; }
        public List<Goal> Goals { get; set; } = new List<Goal>();

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
            if (goals.Count() > 0)
            {
                foreach (Goal goal in goals)
                {
                    Goals.Add(goal);
                }
                Text = "Goals";
            }
            else { Text = "No Goals Yet."; }
        }

        public void SaveGoal()
        {
            var newGoal = new Goal
            {
                UserID = User.Id,
                Date = DateTimeOffset.Now,
                Name = Name,
                Notes = Notes,
                Done = Done
            };

            realm.Write(() =>
            {
                Goal = newGoal;
                realm.Add(newGoal);
            });
        }

        public void DeleteGoal(Goal goal)
        {
            realm.Write(() =>
            {
                realm.Remove(goal);
            });
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
    }
}
