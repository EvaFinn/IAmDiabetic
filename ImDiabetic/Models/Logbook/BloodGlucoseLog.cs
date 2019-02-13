using System;
using Realms;

namespace ImDiabetic.Models.Logbook
{
    public class BloodGlucoseLog : RealmObject
    {
        public string BloodGlucose { get; set; }
    }
}
