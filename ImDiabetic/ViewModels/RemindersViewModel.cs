using System;
using System.Collections.Generic;
using System.Linq;
using ImDiabetic.Models;
using Plugin.LocalNotifications;

namespace ImDiabetic.ViewModels
{
    public class RemindersViewModel : BaseViewModel
    {
        public AppUser User { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTimeOffset RemindDate { get; set; }
        public TimeSpan Time { get; set; }
        const int _SAMPLE_ID = 1;
        public Reminders Reminder { get; set; }
        public List<Reminders> Reminders { get; set; } = new List<Reminders>();

        public RemindersViewModel(AppUser user)
        {
            User = user;
            GetReminders();
        }

        public RemindersViewModel(AppUser user, Reminders reminders)
        {
            User = user;
            Reminder = reminders;
            Title = Reminder.Title;
            Body = Reminder.Body;
            RemindDate = Reminder.ReminderDate;
            Time = Reminder.ReminderDate.TimeOfDay;
            GetReminders();
        }

        public List<Reminders> GetReminders()
        {
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

        public void SaveReminder()
        {
            Reminders remind = new Reminders { UserId = User.Id, ReminderDate = RemindDate, Title = Title, Body = Body };
            if (Time != null)
            {
                CrossLocalNotifications.Current.Show(remind.Title, remind.Body, _SAMPLE_ID, remind.ReminderDate.DateTime);
            }
            else
            {
                CrossLocalNotifications.Current.Show(remind.Title, remind.Body, _SAMPLE_ID);
            }
            realm.Write(() =>
            {
                realm.Add(remind);
            });
        }

        public void UpdateReminders(Reminders reminder)
        {
            realm.Write(() =>
            {
                reminder.Title = Title;
                reminder.Body = Body;
                reminder.ReminderDate = RemindDate;
            });
            CrossLocalNotifications.Current.Show(Title, Body, _SAMPLE_ID, RemindDate.DateTime);
        }

        public void DeleteReminder(Reminders reminder)
        {
            realm.Write(() =>
            {
                realm.Remove(reminder);
            });
        }

        public void SetTriggerTime()
        {
            RemindDate = DateTime.Today + Time;
            if (RemindDate < DateTime.Now)
            {
                RemindDate += TimeSpan.FromDays(1);
            }
        }
    }
}
