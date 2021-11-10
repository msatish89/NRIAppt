using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using NRIApp.Helpers;
using NRIApp.iOS.CustomRenderers;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(IOSDeviceInfo))]
namespace NRIApp.iOS.CustomRenderers
{
   public class IOSDeviceInfo : IDeviceInfo
    {
        public string GetDeviceId()
        {
            return UIDevice.CurrentDevice.IdentifierForVendor.AsString();
        }

        public string GetDeviceModel()
        {
            return UIDevice.CurrentDevice.Model;
        }

        public string GetDeviceName()
        {
            return UIDevice.CurrentDevice.Name;
            //return UIDevice.CurrentDevice.IdentifierForVendor.ToString();
            //return DependencyService.Get<IDeviceHelper>().GetVersion();
        }
        public string GetDeviceOSVersion()
        {
            return UIDevice.CurrentDevice.SystemVersion;
        }
        public string GetDeviceOS()
        {
            return UIDevice.CurrentDevice.SystemName;
        }
        public string GetVersionNumber()
        {
            //var VersionNumber = NSBundle.MainBundle.InfoDictionary.ValueForKey(new NSString("CFBundleShortVersionString")).ToString();   
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }
        public string GetBuildNumber()
        {
            //var BuildNumber = NSBundle.MainBundle.InfoDictionary.ValueForKey(new NSString("CFBundleVersion")).ToString();   
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString();
        }

    }
}