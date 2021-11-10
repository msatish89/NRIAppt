using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using NRIApp.Helpers;
using NRIApp.iOS.CustomRenderers;
using UIKit;
using Google.MobileAds;

[assembly: Xamarin.Forms.Dependency(typeof(AdmobInterstitial))]
namespace NRIApp.iOS.CustomRenderers
{
    public class AdmobInterstitial : IAdInterstitial
    {
        Interstitial _adInterstitial;

        public void ShowAd(string adUnit)
        {
            _adInterstitial = new Interstitial(adUnit);
            var request = Request.GetDefaultRequest();
            _adInterstitial.AdReceived += (sender, args) =>
            {
                if (_adInterstitial.IsReady)
                {
                    var window = UIApplication.SharedApplication.KeyWindow;
                    var vc = window.RootViewController;
                    while (vc.PresentedViewController != null)
                    {
                        vc = vc.PresentedViewController;
                    }
                    _adInterstitial.PresentFromRootViewController(vc);
                }
            };
            _adInterstitial.LoadRequest(request);

        }
    }
}