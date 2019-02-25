using System;
namespace ImDiabetic.ViewModels
{
    public class EducationContentViewModel
    {
        public string Topic { get; set; }
        public User User { get; set; }
        public EducationContentViewModel(User user, string topic)
        {
            User = user;
            Topic = topic;
        }
    }
}
