using System;
using Realms;

namespace ImDiabetic.Models
{
    public class QuizQuestion : RealmObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Question { get; set; }
        public string Topic { get; set; }
        public string OptionOne { get; set; }
        public string OptionTwo { get; set; }
        public string OptionThree { get; set; }
        public string Answer { get; set; }
    }
}
