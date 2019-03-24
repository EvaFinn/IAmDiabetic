using System;
using Realms;

namespace ImDiabetic.Models
{
    public class Achievement : RealmObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAchieved { get; set; } = false;
        public DateTimeOffset AchievedDate { get; set; }
        public string PointsAwarded { get; set; }
    }
}
