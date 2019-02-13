using System; using System.Collections.Generic; using System.Diagnostics;
using System.Linq;  namespace ImDiabetic.ViewModels {     public class LoginViewModel : BaseViewModel     {         public string FirstName { get; set; }         public string Password { get; set; }         public User LoggedInUser { get; set; }          public LoginViewModel()         {         }          public bool CheckUser()         {             var users = realm.All<User>().Where(u => u.FirstName == FirstName && u.Password == Password);             LoggedInUser = users.FirstOrDefault();             return users.Count() > 0;         }          public void DailyStreak() {             //LoggedInUser.LastLogInDate = DateTimeOffset.Now;
            TimeSpan dif = LoggedInUser.LastLogInDate - DateTimeOffset.Now;             Debug.WriteLine("=========== User Last LogIn " + LoggedInUser.LastLogInDate);             Debug.WriteLine(">>>>>>>>>>> Date Difference " + dif);             if(dif.TotalHours > 24) {
                //go back to zero
                Debug.WriteLine(">>>>>>>>>>> Back To Zero");                 LoggedInUser.DailyStreak = 0;             }              //if () {              //}  
            //if (user.LastLogInDate.Day > DateTimeOffset.Now.Day) { 
            //}
        }     } }