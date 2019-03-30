using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Acr.UserDialogs;
using ImDiabetic.Models;
using Nutritionix.Standard;

namespace ImDiabetic.ViewModels
{
    public class LogsViewModel : BaseViewModel
    {
        public AppUser User { get; set; }
        public string LogType { get; set; }
        public string Amount { get; set; }
        public string Carbs { get; set; }
        public string Calorie { get; set; }
        public int LastBloogGlucoseLog { get; set; } 
        public int LastActivityLog { get; set; } 


        public LogsViewModel(AppUser user)
        {
            User = user;
            GetLastBGLog();
        }

        private void GetLastBGLog()
        {
            List<Log> BGList = new List<Log>();
            List<Log> AList = new List<Log>();
            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            if (logs.Count() > 0)
            {
                foreach (Log log in logs)
                {
                    if (log.Type == "Blood Glucose")
                    {
                        BGList.Add(log);
                    }
                    else if (log.Type == "Activity")
                    {
                        AList.Add(log);
                    }
                }
            }
            LastBloogGlucoseLog = BGList.Any() ? int.Parse(BGList.Last().Amount) : 0;
            LastActivityLog = AList.Any() ? int.Parse(AList.Last().Amount) : 0;
        }

        public void AddFoodByBarcode(string barcodeResult)
        {
            var nutritionix = new NutritionixClient();
            nutritionix.Initialize("6a11f905", "e73b446372659100c44aafd008b5b705");
            Item food = null;
            try
            {
                food = nutritionix.RetrieveItemByUPC(barcodeResult);
                if (food != null)
                {
                    Carbs = food.NutritionFactTotalCarbohydrate.ToString();
                    Calorie = food.NutritionFactCalories.ToString();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                UserDialogs.Instance.Alert("Food item not found.", "Sorry", "OK");
                Carbs = "";
                Calorie = "";
            }
        }

        public void AddLog()
        {
            realm.Write(() =>
            {
                Log log = new Log { UserId = User.Id, LogDate = DateTime.Now, Type = LogType, Amount = Amount, Calorie = Calorie };
                realm.Add(log);
                User.Score++;
            });
        }
    }
}
