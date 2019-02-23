using System;
using Realms;

namespace ImDiabetic.Models
{
    public class Quiz : RealmObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Score { get; set; }
        public string UserId { get; set; }
        public QuizQuestion Question { get; set; }
    }
}
