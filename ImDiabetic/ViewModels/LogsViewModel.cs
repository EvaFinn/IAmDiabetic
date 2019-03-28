using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Acr.UserDialogs;
using ImDiabetic.Models;
using ImDiabetic.Models.Logbook;
using Nutritionix.Standard;

namespace ImDiabetic.ViewModels
{
    public class LogsViewModel : BaseViewModel
    {
        public AppUser User { get; set; }
        public string LogType { get; set; }
        public string Amount { get; set; }
        public string BloodGlucose { get; set; }
        public string Insulin { get; set; }
        public string Pills { get; set; }
        public string Activity { get; set; }
        public string Carbs { get; set; } 
        public string Calorie { get; set; } 
        public Log Log { get; set; }
        public int LastBloogGlucoseLog { get; set; }
        public int LastActivityLog { get; set; }


        public LogsViewModel(AppUser user)
        {
            User = user;
            GetLastBGLog();
        }

        private void GetLastBGLog()
        {
            var logs = realm.All<Log>().Where(l => l.UserId == User.Id);
            if (logs.Count() < 1)
            {
                LastBloogGlucoseLog = 0; //(int)User.MinTargetBloodGlucose;
                LastActivityLog = 0;
            }
            else
            {
                List<Log> BGList = new List<Log>();
                List<Log> AList = new List<Log>();

                foreach (Log log in logs)
                {
                    if (log.Type == "Blood Glucose")
                    {
                        BGList.Add(log);
                    } 
                    if(log.Type == "Activity") {
                        AList.Add(log);
                    }

                    if (BGList.Count > 0)
                    {
                        LastBloogGlucoseLog = int.Parse(BGList.Last().Amount);
                    }

                    if (AList.Count > 0)
                    {
                        LastActivityLog = int.Parse(AList.Last().Amount);
                    }
                }
            }
        }

        public void AddFoodByBarcode(string barcodeResult) {
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
            catch {
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
