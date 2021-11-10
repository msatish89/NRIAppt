using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NRIApp.Helpers;
using NRIApp.USHome.Interfaces;
using Refit;
using Firebase.Iid;

namespace NRIApp.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            SendNotifictaiontoAppServer(refreshedToken);
        }
        public async void SendNotifictaiontoAppServer(string refreshtoken)
        {
            string userdeviceid = Commonsettings.UserDeviceId;
            var appnotification = RestService.For<IUSHome>(Commonsettings.USHomeAPI);
            var appnotificationdata = await appnotification.getnotification(userdeviceid, refreshtoken);
        }
    }
}