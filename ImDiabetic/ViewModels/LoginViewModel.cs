using System; using System.Collections.Generic; using System.Diagnostics;
using System.Linq;  namespace ImDiabetic.ViewModels {     public class LoginViewModel : BaseViewModel     {         public string FirstName { get; set; }         public string Password { get; set; }         public User LoggedInUser { get; set; }          public LoginViewModel()         {         }          public bool CheckUser()         {             var users = realm.All<User>().Where(u => u.FirstName == FirstName && u.Password == Password);             LoggedInUser = users.FirstOrDefault();             return users.Count() > 0;         }     } }