using System;
using System.Collections.Generic;
using System.Linq;
using ImDiabetic.Models;

namespace ImDiabetic.ViewModels
{
    public class RemindersListViewModel : BaseViewModel
    {
        public AppUser User { get; set; }
        public List<Reminders> Reminders { get; set; } = new List<Reminders>();

        public RemindersListViewModel(AppUser user)
        {
            User = user;
        }

        public List<Reminders> GetReminders() {
            var reminders = realm.All<Reminders>().Where(r => r.UserId == User.Id);
            if (reminders.Count() > 0)
            {
                foreach (Reminders remind in reminders)
                {
                    Reminders.Add(remind);
                }
            }
            return Reminders;
        }
    }
}
