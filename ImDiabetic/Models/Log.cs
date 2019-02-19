using System;
using ImDiabetic.Models.Logbook;
using Realms;

namespace ImDiabetic.Models
{
    public class Log : RealmObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public DateTimeOffset LogDate { get; set; }
        public string BloodGlucose { get; set; }
        public InsulinLog Insulin { get; set; }
        public MedicationLog Medication { get; set; }
        public FoodLog Food { get; set; }
        public ActivityLog Activity { get; set; }
    }
}
