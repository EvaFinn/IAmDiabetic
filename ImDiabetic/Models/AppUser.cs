using System;
using Realms;

namespace ImDiabetic.Models
{
    public class AppUser : RealmObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public double MaxTargetBloodGlucose { get; set; }
        public double MinTargetBloodGlucose { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Password { get; set; }
        public int DailyStreak { get; set; } = 0;
        public DateTimeOffset LastLogInDate { get; set; } = DateTimeOffset.Now;
        public int Level { get; set; } = 1;
        public int Score { get; set; } = 0;
        public byte[] ProfilePicture { get; set; }
    }
}
