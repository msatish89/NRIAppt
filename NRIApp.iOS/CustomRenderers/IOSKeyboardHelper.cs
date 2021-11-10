using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using NRIApp.Helpers;
using NRIApp.iOS.CustomRenderers;

[assembly: Xamarin.Forms.Dependency(typeof(IOSKeyboardHelper))]
namespace NRIApp.iOS.CustomRenderers
{
    public class IOSKeyboardHelper : IKeyboardHelper
    {
        //public IOSKeyboardHelper() : base()
        //{
        //}

        public void HideKeyboard()
        {
            try
            {
                UIApplication.SharedApplication.KeyWindow.EndEditing(true);
            }
            catch (Exception e)
            {
                string Error = e.Message + e.StackTrace;
            }
        }
        public void Gotosettings()
        {

            //var url = new NSUrl("prefs:root=Settings");
            //UIApplication.SharedApplication.OpenUrl(url);
            UIApplication.SharedApplication.OpenUrl(new NSUrl(UIApplication.OpenSettingsUrlString));
        }
    }
}