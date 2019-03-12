using System; using System.Collections.Generic; using System.Diagnostics;
using System.Linq; using ImDiabetic.Models;

namespace ImDiabetic.ViewModels {     public class LoginViewModel : BaseViewModel     {         public string FirstName { get; set; }         public string Password { get; set; }         public AppUser LoggedInUser { get; set; }          public LoginViewModel()         {         }          public bool CheckUser()         {             var uuu = realm.All<AppUser>();             foreach(AppUser use in uuu) {
                Debug.WriteLine("NAME " + use.FirstName + ",,," + use.Password);             }

            var users = realm.All<AppUser>().Where(u => u.FirstName == FirstName && u.Password == Password);             LoggedInUser = users.FirstOrDefault();             return users.Count() > 0;         }          public void DeleteUsers() {              using (var db = realm.BeginWrite())
            {
                realm.RemoveAll();
                db.Commit();             }             realm.Dispose();
        }          //public void Dispose() {
        //    realm.Dispose();         //}     } }