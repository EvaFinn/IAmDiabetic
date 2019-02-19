﻿using System;
using Realms;

namespace ImDiabetic.Models.Logbook
{
    public class BloodGlucoseLog : RealmObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string BloodGlucoseLevel { get; set; }
        public DateTimeOffset LogDate { get; set; }
    }
}
