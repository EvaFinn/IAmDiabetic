using System; using System.Collections.Generic; using System.Diagnostics;
using System.Linq; using ImDiabetic.Models;

namespace ImDiabetic.ViewModels {     public class LoginViewModel : BaseViewModel     {         public string FirstName { get; set; }         public string Password { get; set; }         public AppUser LoggedInUser { get; set; }          public bool CheckUser()         {
            var users = realm.All<AppUser>().Where(u => u.FirstName == FirstName && u.Password == Password);             LoggedInUser = users.FirstOrDefault();             return users.Count() > 0;         }     } }