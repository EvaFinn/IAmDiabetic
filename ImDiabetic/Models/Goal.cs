using System;
using Realms;

namespace ImDiabetic.Models
{
    public class Goal : RealmObject
    {
        public string GoalID { get; set; } = Guid.NewGuid().ToString();
        public string UserID { get; set; } 
        public DateTimeOffset Date { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public bool Done { get; set; }
    }
}
