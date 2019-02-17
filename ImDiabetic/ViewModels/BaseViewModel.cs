using System;
using Realms;

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
            var config = new RealmConfiguration() { SchemaVersion = 6 }; //change in masterdetail page also
            realm = Realm.GetInstance(config);
        }
    }
}
