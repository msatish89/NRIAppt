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
    public partial class LS_Prime : ContentPage
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
       
            int _supertagid = 0;string _supertag = "", _ptag = "", _ptagid = "", _checkvalue = "", _checktext = "";
        public LS_Prime(int supertagid, string supertag, string ptag, string ptagid, string checkvalue, string checktext)
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
                _supertagid = supertagid; _supertag = supertag;
                 BindingContext = new ViewModels.LS_Leadform_VM(supertagid, supertag, ptagid, ptag, checkvalue, checktext);
                //Bindquestionaries(ptagid);
                string sPostedviaid = "";
                if (Commonsettings.UserMobileOS.ToLower().Contains("iphone"))
                {
                    sPostedviaid = "194";
                }
                else
                {
                    sPostedviaid = "197";
                }
                
               string renderurl = "https://us.sulekha.com/metrocityresolver_app.aspx?ptagid=" + LS_ViewModel.primarytagid + "&titleurl=" + LS_ViewModel.primarytagurl+"&ismobileapp=1&postedviaid="+ sPostedviaid+"&postedvia="+ Commonsettings.UserMobileOS.ToLower()+"&contactname="+Commonsettings.UserName+"&contactemail="+Commonsettings.UserEmail+"&contactno="+Commonsettings.UserMobileno+"&countrycode="+Commonsettings.Usercountry;
                //string renderurl = "http://localjobs.sulekha.com/common/resolve-return-htmlad.aspx?folderid=270&adid=1347844&category=jobs";
                var htm = new UrlWebViewSource
                {
                    Url = renderurl
                };

              //  string script = @"<script>document.getElementById('txtsrchlstsemailid').value='" + Commonsettings.UserEmail+ "';document.getElementById('txtsrchlstsname').value='"+Commonsettings.UserName+ "';document.getElementById('txtsrchlstscontactno').value='" + Commonsettings.UserMobileno+"'</script>";                       
              
                Urlwebviewsource = htm;
               articleweb.Source = htm;

                
               // articleweb.HeightRequest =Convert.ToDouble(Commonsettings.UserDeviceHeight);
                //articleweb.GestureRecognizers
                //var tapGestureRecognizer = new TapGestureRecognizer();
                //tapGestureRecognizer.Tapped += (s, e) => {

                //};

                //articleweb.GestureRecognizers.Add(tapGestureRecognizer);
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

        private  void Articleweb_Clicked(object sender, Behaviors.ClickEventArgs e)
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
            if (arrkk[0].Replace("id=", "") == "linkbrowsing")
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
            //if (arrkk[0].Replace("id=", "") == "linktermsconditions")
            //{
            //    Keyboard = Keyboard.Numeric;
            //}

        }
        private async void NextAsyncMethod()
        {

            var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Listing.LS_Listing());

        }
    }






   
}