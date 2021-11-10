using System;
using System.Collections.Generic;
using System.Linq;
using Acr.UserDialogs;
using AsNum.XFControls.iOS;
using Foundation;
using Intents;
using ObjCRuntime;
using UIKit;
using Xamarin.RangeSlider.Forms;


[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.RangeSlider.Forms.RangeSlider), typeof(Xamarin.RangeSlider.Forms.RangeSliderRenderer))]

[assembly: Xamarin.Forms.ExportRenderer(typeof(Xamarin.RangeSlider.Forms.RangeSlider), typeof(Xamarin.RangeSlider.Forms.RangeSliderRenderer))]
namespace NRIApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.

      
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //Rg.Plugins.Popup.Popup.Init();
            // AsNumAssemblyHelper.HoldAssembly();
            global::Xamarin.Forms.Forms.Init();
            #region For Screen Height & Width  
            App.screenWidth = (int)UIScreen.MainScreen.Bounds.Width;
            App.screenHeight = (int)UIScreen.MainScreen.Bounds.Height;
            #endregion
            LoadApplication(new App());
          
            ToastConfig.DefaultPosition = ToastPosition.Top;
            return base.FinishedLaunching(app, options);
        }
     
    }
}
