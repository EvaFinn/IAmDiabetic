using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ImDiabetic.Models;
using ImDiabetic.ViewModels;
using Realms;
using Realms.Sync;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class MasterDetailNav : MasterDetailPage
    {
        Realm realm;
        public AppUser User { get; }

        public MasterDetailNav(AppUser user)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            var config = new RealmConfiguration() { SchemaVersion = 13 };
            //config.ShouldDeleteIfMigrationNeeded = true;
            realm = Realm.GetInstance(config);

            User = user;
            aboutList.ItemsSource = GetMenuList();
            var homePage = typeof(DashboardPage);
            Detail = new NavigationPage(new DashboardPage(User));
        }

        private void OnMenuItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedMenuItem = (MasterMenuItems)e.SelectedItem;
            if(selectedMenuItem == null) {
                return;
            }
            Type selectedPage = selectedMenuItem.TargetPage;
            if (selectedPage == typeof(LoginPage)) {
                IsGestureEnabled = false;
            }

            Detail = new NavigationPage((Page)Activator.CreateInstance(selectedPage, User));
            IsPresented = false;
            aboutList.SelectedItem = null;
        }

        public List<MasterMenuItems> GetMenuList()
        {
            var list = new List<MasterMenuItems>
            {
                new MasterMenuItems()
                {
                    Text = "Profile",
                    TargetPage = typeof(ProfilePage)
                },

                new MasterMenuItems()
                {
                    Text = "Dashboard",
                    TargetPage = typeof(DashboardPage)
                },

                new MasterMenuItems()
                {
                    Text = "Reminders",
                    TargetPage = typeof(RemindersListPage)
                }
            };
            return list;
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        async void DeleteAccount(object sender, System.EventArgs e)
        {
            var userToDelete = realm.All<AppUser>().Where(b => b.Id == User.Id).FirstOrDefault();

            using (var trans = realm.BeginWrite())
            {
                realm.Remove(userToDelete);
                trans.Commit();
            }
            await Navigation.PushAsync(new LoginPage());
        }
    }
}