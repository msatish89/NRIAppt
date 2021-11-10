using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NRIApp.Helpers
{
   public static class DeviceInfo
    {
       
        public static string GetDeviceId()
        {
            return DependencyService.Get<IDeviceInfo>().GetDeviceId();
        }

        public static string GetDeviceName()
        {
            return DependencyService.Get<IDeviceInfo>().GetDeviceName();
        }

        public static string GetOSVersion()
        {
            return DependencyService.Get<IDeviceInfo>().GetDeviceOS() + " "
                                    + DependencyService.Get<IDeviceInfo>().GetDeviceOSVersion();
        }

        //public static string GetDeviceMobilenumber()
        //{
        //    return DependencyService.Get<IDeviceInfo>().GetDeviceMobileno();
        //}
        public static string GetVersionno()
        {
            return DependencyService.Get<IDeviceInfo>().GetVersionNumber();
        }
        public static string GetBuildno()
        {
            return DependencyService.Get<IDeviceInfo>().GetBuildNumber();
        }
        public static void SetDeviceDetails()
        {
            Commonsettings.UserDeviceId = GetDeviceId();
            Commonsettings.UserDeviceName = GetDeviceName();
            Commonsettings.UserDeviceOSVersion = GetOSVersion();
            Commonsettings.Userappversion = GetVersionno();
        }
        
    }
}
