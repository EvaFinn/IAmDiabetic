using System; using System.Collections.Generic; using System.Diagnostics;
using System.Linq;  namespace ImDiabetic.ViewModels {     public class LoginViewModel : BaseViewModel     {         public string FirstName { get; set; }         public string Password { get; set; }         public User LoggedInUser { get; set; }          public LoginViewModel()         {         }          public bool CheckUser()         {             var uuu = realm.All<User>();             foreach(User use in uuu) {
                Debug.WriteLine("NAME " + use.FirstName + ",,," + use.Password);             }

            var users = realm.All<User>().Where(u => u.FirstName == FirstName && u.Password == Password);             LoggedInUser = users.FirstOrDefault();             return users.Count() > 0;         }          public void DeleteUsers() {              //realm.Write(() =>             //{             //    realm.RemoveAll("User");             //});              using (var db = realm.BeginWrite())
            {
                realm.RemoveAll("User");
                db.Commit();
            }

            //realm.Write(() =>
            //{
            //    realm.RemoveAll("Log");
            //});
        }     } }