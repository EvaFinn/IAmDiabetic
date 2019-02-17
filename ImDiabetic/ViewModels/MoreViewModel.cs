using System;
using System.Collections.Generic;
using ImDiabetic.Views;
using Xamarin.Forms;

namespace ImDiabetic.ViewModels
{
    public class MoreViewModel : BaseViewModel
    {
        User User { get; set; }


        public MoreViewModel(User user)
        {
            User = user;
        }

        public List<MasterMenuItems> GetMenuList()
        {
            var list = new List<MasterMenuItems>
            {
                new MasterMenuItems()
                {
                    Text = "Profile",
                    ImagePath = ImageSource.FromResource("ImDiabetic.Icons.rocket.png"),
                    TargetPage = typeof(ProfilePage)
                },

                new MasterMenuItems()
                {
                    Text = "Dashboard",
                    ImagePath = ImageSource.FromResource("ImDiabetic.Icons.rocket.png"),
                    TargetPage = typeof(DashboardPage)
                },

                new MasterMenuItems()
                {
                    Text = "Settings",
                    ImagePath = ImageSource.FromResource("ImDiabetic.Icons.rocket.png"),
                    TargetPage = typeof(ProfilePage)
                }
            };
            return list;
        }

    }
}
