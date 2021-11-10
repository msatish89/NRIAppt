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
using NRIApp.Droid.CustomRenderers;
using NRIApp.Helpers;
using Android.Gms.Ads;
using System.Threading;

[assembly: Dependency(typeof(AdInterstitial_Droid))]
namespace NRIApp.Droid.CustomRenderers
{

   
    public class AdInterstitial_Droid : IAdInterstitial
    {
       // InterstitialAd interstitialAd;

        public AdInterstitial_Droid()
        {
            //  interstitialAd = new InterstitialAd(Android.App.Application.Context);

            // TODO: change this id to your admob id 
            //   interstitialAd.AdUnitId = "ca-app-pub-3940256099942544/1033173712";
            //   LoadAd();
        }

        //void LoadAd()
        //{
        //    var requestbuilder = new AdRequest.Builder();
        //    interstitialAd.LoadAd(requestbuilder.Build());

        //}
        InterstitialAd _ad;
        public void ShowAd(string adUnit)
        {
            var context = Android.App.Application.Context;
            _ad = new InterstitialAd(context);
            _ad.AdUnitId = adUnit;

            var intlistener = new InterstitialAdListener(_ad);
            intlistener.OnAdLoaded();
            _ad.AdListener = intlistener;

            var requestbuilder = new AdRequest.Builder();
            _ad.LoadAd(requestbuilder.Build());
        }

        //public async void ShowAd(string unitid)
        //{
        //    //interstitialAd = new InterstitialAd(Android.App.Application.Context);
        //    //interstitialAd.AdUnitId = unitid;
        //    //LoadAd();

        //    //var context = Android.App.Application.Context;
        //    //interstitialAd = new InterstitialAd(context);
        //    //interstitialAd.AdUnitId = unitid;
        //    //var intlistener = new InterstitialAdListener(interstitialAd);
        //    //intlistener.OnAdLoaded();
        //    //interstitialAd.AdListener = intlistener;
        //    //var requestbuilder = new AdRequest.Builder();
        //    //interstitialAd.LoadAd(requestbuilder.Build());

        //    //Thread.Sleep(10000);

        //    interstitialAd = new InterstitialAd(Android.App.Application.Context);

        //    // TODO: change this id to your admob id 
        //    interstitialAd.AdUnitId = unitid;
        //    LoadAd();
        //    Thread.Sleep(10000);

        //    if (interstitialAd.IsLoaded)
        //        interstitialAd.Show();

        //    LoadAd();
        //}
    }
}