using System;
namespace ImDiabetic.ViewModels
{
    public class GoalsViewModel : BaseViewModel
    {
        public User User { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }
        public string Notes { get; set; }

        public GoalsViewModel(User user)
        {
        }

        public void SaveGoal() { 
        
        }

        public void DeleteGoal() { 
        
        }
    }
}
