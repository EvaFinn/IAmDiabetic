using System;
using Realms;

namespace ImDiabetic.Models
{
    public class Reminders : RealmObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public DateTimeOffset ReminderDate { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
