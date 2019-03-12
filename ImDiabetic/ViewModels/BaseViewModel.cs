using System;
using Realms;
using Realms.Sync;

namespace ImDiabetic.ViewModels
{
    public class BaseViewModel
    {
        protected Realm realm;
        public BaseViewModel()
        {
            SetUpRealm();

        }

        protected void SetUpRealm() {
            var config = new RealmConfiguration() { SchemaVersion = 11 }; //change in masterdetail page also
            //config.ShouldDeleteIfMigrationNeeded = true;
            realm = Realm.GetInstance(config);
            //realm.Dispose();
            
            //Realm.DeleteRealm(config);
        }
    }
}
