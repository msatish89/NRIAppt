using System;
using NRIApp;
using Android.Gms.Ads;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using NRIApp.Droid.CustomRenderers;
using Android.Content;
using NRIApp.Helpers;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(AdBanner), typeof(AdBanner_Droid))]
namespace NRIApp.Droid.CustomRenderers
{
    public class AdBanner_Droid : ViewRenderer
    {
        Context context;
        public AdBanner_Droid(Context _context) : base(_context)
        {
            context = _context;
        }
        //protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        //{
        //    base.OnElementChanged(e);

        //    if (e.NewElement != null && Control == null)
        //        SetNativeControl(CreateAdView());
        //}


        //protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    base.OnElementPropertyChanged(sender, e);

        //    if (e.PropertyName == nameof(AdView.AdUnitId))
        //        Control.AdUnitId = Element.AdUnitId;
        //}
        //private AdView CreateAdView()
        //{
        //    var adView = new AdView(Context)
        //    {
        //        AdSize = AdSize.SmartBanner,
        //        AdUnitId = Element.AdUnitId
        //    };

        //    adView.LoadAd(new AdRequest.Builder().Build());

        //    return adView;
        //}
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {


            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var adView = new AdView(Context);
                
                switch ((Element as AdBanner).Size)
                {
                    case AdBanner.Sizes.Standardbanner:
                        adView.AdSize = AdSize.Banner;
                        break;
                    case AdBanner.Sizes.LargeBanner:
                        adView.AdSize = AdSize.LargeBanner;
                        break;
                    case AdBanner.Sizes.MediumRectangle:
                        adView.AdSize = AdSize.MediumRectangle;
                        break;
                    case AdBanner.Sizes.FullBanner:
                        adView.AdSize = AdSize.FullBanner;
                        break;
                    case AdBanner.Sizes.Leaderboard:
                        adView.AdSize = AdSize.Leaderboard;
                        break;
                    case AdBanner.Sizes.SmartBannerPortrait:
                        adView.AdSize = AdSize.SmartBanner;
                        break;
                    default:
                        adView.AdSize = AdSize.Banner;
                        break;
                }


                // TODO: change this id to your admob id  

                adView.AdUnitId = (Element as AdBanner).AdUnitId; ;
                var requestbuilder = new AdRequest.Builder();
                adView.LoadAd(requestbuilder.Build());
                SetNativeControl(adView);

                //adView.AdUnitId = (Element as AdBanner).AdUnitId;
                //var requestbuilder = new AdRequest.Builder();
                //adView.LoadAd(requestbuilder.Build());
                //SetNativeControl(adView);
            }
        }
    }
}