using System;
using System.Collections.Generic;
using System.ComponentModel;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class RemindersPage : ContentPage
    {
        public AppUser User { get; set; }
        public Reminders Reminder { get; set; }
        public bool IsNew { get; set; }
        public RemindersPage(AppUser user)
        {
            InitializeComponent();
            User = user;
            this.BindingContext = new RemindersViewModel(User);
            SendButton.IsEnabled = false;
            DeleteBtn.IsVisible = false;
            IsNew = true;
        }

        public RemindersPage(AppUser user, Reminders reminders)
        {
            InitializeComponent();
            User = user;
            Reminder = reminders;
            this.BindingContext = new RemindersViewModel(User, Reminder);
            SendButton.IsEnabled = false;
            IsNew = false;
        }

        private void SendButtonClicked(object sender, EventArgs e)
        {
            if (IsNew)
            {
                (BindingContext as RemindersViewModel).SaveReminder();
            }
            else
            {
                (BindingContext as RemindersViewModel).UpdateReminders(Reminder);
            }
            Navigation.PushAsync(new RemindersListPage(User));
        }

        private void DeleteButtonClicked(object sender, EventArgs e)
        {

            (BindingContext as RemindersViewModel).DeleteReminder(Reminder);
            Navigation.PopAsync();
        }

        private void OnTimePickerPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Time")
            {
                (BindingContext as RemindersViewModel).SetTriggerTime();
                SendButton.IsEnabled = true;
            }
        }
    }
}
