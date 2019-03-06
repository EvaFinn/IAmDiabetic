using System;
using System.Collections.Generic;
using Realms;
using Xamarin.Forms;

namespace ImDiabetic.Views
{
    public partial class MasterDetailNav : MasterDetailPage
    {
        Realm realm;
        public User User { get; }

        public MasterDetailNav(User user)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            var config = new RealmConfiguration() { SchemaVersion = 11 };
            config.ShouldDeleteIfMigrationNeeded = true;
            realm = Realm.GetInstance(config);
            User = user;
            profileImage.Source = ImageSource.FromResource("ImDiabetic.Icons.profile.png");
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
            if (selectedPage == typeof(MainPage)) {
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

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}