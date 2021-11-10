using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.RangeSlider.Forms;

namespace NRIApp.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            try
            {
                // if you want to use a different Application Delegate class from "AppDelegate"
                // you can specify it here.
                UIApplication.Main(args, null, "AppDelegate");
                var t1 = typeof(Xamarin.RangeSlider.RangeSliderControl);
                var t2 = typeof(RangeSliderRenderer);

            }
            catch (Exception e)
            {
                string Error = e.Message + e.StackTrace;
            }
            //check
        }
    }
}
