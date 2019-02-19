using System;
using Realms;

namespace ImDiabetic.Models.Logbook
{
    public class ActivityLog : RealmObject 
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public DateTimeOffset LogDate { get; set; }
        public string Activity { get; set; }
    }
}
