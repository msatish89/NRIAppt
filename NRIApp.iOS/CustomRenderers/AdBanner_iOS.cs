using System;
using NRIApp.iOS.CustomRenderers;
using CoreGraphics;
using Google.MobileAds;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using NRIApp.Helpers;

[assembly: ExportRenderer(typeof(AdBanner), typeof(AdBanner_iOS))]
namespace NRIApp.iOS.CustomRenderers
{
    public class AdBanner_iOS : ViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                BannerView bannerView = null;

                switch ((Element as AdBanner).Size)
                {
                    case AdBanner.Sizes.Standardbanner:
                        bannerView = new BannerView(AdSizeCons.Banner, new CGPoint(0, 0));
                        break;
                    case AdBanner.Sizes.LargeBanner:
                        bannerView = new BannerView(AdSizeCons.LargeBanner, new CGPoint(0, 0));
                        break;
                    case AdBanner.Sizes.MediumRectangle:
                        bannerView = new BannerView(AdSizeCons.MediumRectangle, new CGPoint(0, 0));
                        break;
                    case AdBanner.Sizes.FullBanner:
                        bannerView = new BannerView(AdSizeCons.FullBanner, new CGPoint(0, 0));
                        break;
                    case AdBanner.Sizes.Leaderboard:
                        bannerView = new BannerView(AdSizeCons.Leaderboard, new CGPoint(0, 0));
                        break;
                    case AdBanner.Sizes.SmartBannerPortrait:
                        bannerView = new BannerView(AdSizeCons.SmartBannerPortrait, new CGPoint(0, 0));
                        break;
                    default:
                        bannerView = new BannerView(AdSizeCons.Banner, new CGPoint(0, 0));
                        break;
                }

                // TODO: change this id to your admob id  
                bannerView.AdUnitID = (Element as AdBanner).AdUnitId;
                foreach (UIWindow uiWindow in UIApplication.SharedApplication.Windows)
                {
                    if (uiWindow.RootViewController != null)
                    {
                        bannerView.RootViewController = uiWindow.RootViewController;
                    }
                }
                var request = Request.GetDefaultRequest();
                bannerView.LoadRequest(request);
                SetNativeControl(bannerView);
            }

        }

    }
}