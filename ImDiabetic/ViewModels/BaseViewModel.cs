using System;
using Realms;
using Realms.Sync;
using Xamarin.Forms;

namespace ImDiabetic.ViewModels
{
    public class BaseViewModel : BindableObject
    {
        protected Realm realm;


        public BaseViewModel()
        {
            SetUpRealm();
        }

        protected void SetUpRealm() {
            var config = new RealmConfiguration() { SchemaVersion = 13 };
            //config.ShouldDeleteIfMigrationNeeded = true;
            realm = Realm.GetInstance(config);
        }
    }
}
