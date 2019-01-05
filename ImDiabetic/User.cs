using System;
using Realms;
//if changing/adding/removing fields, update schema number
namespace ImDiabetic
{
    public class User: RealmObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
    }
}
