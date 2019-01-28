using System;
using Realms;

namespace ImDiabetic.Models
{
    public class Log : RealmObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public DateTimeOffset LogDate { get; set; }
        public string BloodGlucose { get; set; }
        public string Insulin { get; set; }
        public string Pills { get; set; }
        public string Carbs { get; set; }
        public string Activity { get; set; }
    }
}
