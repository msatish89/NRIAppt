using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.LocalService.Features.Models;
using Plugin.Connectivity;
using Refit;
using NRIApp.LocalService.Features.Interfaces;
using NRIApp.LocalService.Features.ViewModels;
using System.Net.Http;
using Newtonsoft.Json;
using NRIApp.Helpers;

namespace NRIApp.LocalService.Features.Views.Leadform
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LS_Prime_detail : ContentPage
    {
        private HtmlWebViewSource _Htmlwebviewsource;

        public HtmlWebViewSource Htmlwebviewsource

        {

            get
            {
                return _Htmlwebviewsource;
            }

            set
            {

                _Htmlwebviewsource = value;

            }

        }

        private UrlWebViewSource _Urlwebviewsource;

        public UrlWebViewSource Urlwebviewsource

        {

            get
            {
                return _Urlwebviewsource;
            }

            set
            {

                _Urlwebviewsource = value;

            }

        }
        public LS_Prime_detail(string adid,string premiumad,string titleurl,string cityurl)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == false)
                Navigation.PushModalAsync(new NRIApp.USHome.Views.Nointernet());
            InitializeComponent();
            Title = "Tell us about your need";

            try
            {
                // lblformtitle.Text = ptag;
                if (Device.RuntimePlatform == Device.iOS)
                    NavigationPage.SetBackButtonTitle(this, "Back");

                if (string.IsNullOrEmpty(cityurl))
                {
                    cityurl = Commonsettings.Usercityurl;
                }
                string sPostedviaid = "";
                if (Commonsettings.UserMobileOS.ToLower().Contains("iphone"))
                {
                    sPostedviaid = "194";
                }
                else
                {
                    sPostedviaid = "197";
                }
                //Bindquestionaries(ptagid);
                string renderurl = "https://us.sulekha.com/primarytagcityv3_detail_app.aspx?ptagid=" + LS_ViewModel.primarytagid+"&tagurl="+LS_ViewModel.primarytagurl+"&adid="+adid+"&titleurl="+titleurl+"&cityurl="+ cityurl+ "&premiumad="+ premiumad + "&contentid=" + adid + "&ismobileapp=1&postedviaid=" + sPostedviaid + "&postedvia=" + Commonsettings.UserMobileOS.ToLower() + "&contactname=" + Commonsettings.UserName + "&contactemail=" + Commonsettings.UserEmail + "&contactno=" + Commonsettings.UserMobileno + "&countrycode=" + Commonsettings.Usercountry;
                var htm = new UrlWebViewSource
                {
                    Url = renderurl
                };

                var htm1 = new HtmlWebViewSource
                {
                    BaseUrl = renderurl
                };

                Urlwebviewsource = htm;                
                articleweb.Source = htm;
                // Renderpage(renderurl);
                //leadcountrycode.SelectedIndex = 0;
            }
            catch (Exception e)
            {

            }
        }
        public void gototc()
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }

        private void Articleweb_Clicked(object sender, Behaviors.ClickEventArgs e)
        {

            //var kk = e.Element;
            //kk = kk.Replace("id=", "");
            //if (kk == "linkbrowsing")
            //{
            //    Device.BeginInvokeOnMainThread(NextAsyncMethod);
            //}
            string cityurl = "";
            var kk = e.Element;
            string[] arrkk = System.Text.RegularExpressions.Regex.Split(kk, " ");
            if (arrkk[0].Replace("id=", "") == "linkbrowsingdetail")
            {
                if (!string.IsNullOrEmpty(arrkk[1].ToString()))
                {
                    cityurl = arrkk[1].Replace("cityurl=", "");
                }
                Device.BeginInvokeOnMainThread(NextAsyncMethod);
            }
            if (arrkk[0].Replace("id=", "") == "linktermsconditions")
            {
                Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
            }
           
            

        }
        private async void NextAsyncMethod()
        {
            var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Listing.LS_Listing());
        }




    }






   
}