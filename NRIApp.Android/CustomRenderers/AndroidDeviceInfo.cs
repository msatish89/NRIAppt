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
using Xamarin.Forms;
using Android.Provider;
using NRIApp.Helpers;
using NRIApp.Droid.CustomRenderers;
using B = Android.OS.Build;
using PM = Android.Content.PM;
using Android.Telephony;
using Android.Content.PM;


[assembly: Xamarin.Forms.Dependency(typeof(AndroidDeviceInfo))]
namespace NRIApp.Droid.CustomRenderers
{
   public class AndroidDeviceInfo : IDeviceInfo
    {
        public string GetDeviceId()
        {
            return Settings.Secure.GetString(Forms.Context.ContentResolver, Settings.Secure.AndroidId);
        }

        public string GetDeviceModel()
        {
            return B.Model;
        }

        public string GetDeviceName()
        {
            return B.Device;
        }

        public string GetDeviceOS()
        {
            return B.VERSION.Release;
        }

        public string GetDeviceOSVersion()
        {
            return B.VERSION.SdkInt.ToString();
        }

        

        //public void Callconnect()
        //{
        //    var telephonyManagerService = (TelephonyManager)Forms.Context.ApplicationContext.GetSystemService(Android.Content.Context.TelephonyService);
        //    var getCurrentState = telephonyManagerService?.CallState;
        //    switch (getCurrentState)
        //    {
        //        case CallState.Idle:
        //            //No call
        //            break;
        //        case CallState.Ringing:
        //            //Ringing 
        //            break;
        //        case CallState.Offhook:
        //            //On call
        //            break;
        //        default:
        //            break;
        //    }
        //}
        //public string GetDeviceMobileno()
        //{
        //    var tMgr = (TelephonyManager)Forms.Context.ApplicationContext.GetSystemService(Android.Content.Context.TelephonyService);
        //    return tMgr.Line1Number;
        //}

        PackageInfo _appInfo;
        public string GetVersionNumber()
        {
            var context = Android.App.Application.Context;
            _appInfo = context.PackageManager.GetPackageInfo(context.PackageName, 0);
            return _appInfo.VersionName;
        }
        public string GetBuildNumber()
        {
            return _appInfo.VersionCode.ToString();
        }

    }
}