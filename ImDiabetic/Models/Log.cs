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
        public string Type { get; set; }
        public string Amount { get; set; }

    }
}
