using System;
using Realms;

namespace ImDiabetic.Models.Logbook
{
    public class FoodLog : RealmObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Calories { get; set; }
        public string Carbohydrates { get; set; }
        public DateTimeOffset LogDate { get; set; }
    }
}
