using NRIApp.DayCare.Features.List.Interface;
using NRIApp.DayCare.Features.List.Models;
using NRIApp.Helpers;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.DayCare.Features.List.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Search_DC : ContentPage
	{
        public string city_url = "";
        public string city_lat;
        public string city_lng;
        public string metro_url = "";
        public string userpid = Commonsettings.UserPid;
        DClistinfo searchres = new DClistinfo();
        public Search_DC (DClistinfo lstdt)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            searchres = lstdt;
            //listdata = lstdt;
            city_url = lstdt.cityurl;
            city_lat = lstdt.userlat;
            city_lng = lstdt.userlong;
            metro_url = "";
        }
        private async void DC_search(object sender, TextChangedEventArgs e)
        {
            try
            {
                //https://techjobs.sulekha.com/mobileapp/localjobs/jobsposting-api.aspx?type=searchads&title=busin&rowstofetch=20&cityurl=new-york-ny
                string keyword = textsearch.Text;
                if (keyword.Length > 1)
                {
                    var currentpage = GetCurrentPage();
                    var nsAPI = RestService.For<IDClistings>(Commonsettings.DaycareAPI);
                    if (!string.IsNullOrEmpty(metro_url))
                    {
                        city_url = "";
                    }
                    else
                    {
                        metro_url = "";
                    }

                    Models.DCsearch list = await nsAPI.searchlist(keyword, city_url, city_lat, city_lng, metro_url, city_url);
                    if (list.RowDs.Count() > 0 && list != null)
                    {
                        foreach (var i in list.RowDs)
                        {
                            if (i.Search == "offered_latlong")
                            {
                                i.header = "TOP CAREGIVERS IN AND NEAR NEW YORK";
                                i.stackdetails = true;
                            }
                            if (i.Search == "wanted_latlong")
                            {
                                i.header = "TOP CARE JOBS IN AND NEAR NEW YORK";
                                i.stackdetails = true;
                            }
                            if (i.Search == "daycare_latest")
                            {
                                i.header = "RECENTLY POSTED CARE PROVIDERS & SEEKERS";
                                i.stackdetails = true;
                            }
                            if (i.Search == "daycare_latest_category")
                            {
                                i.header = "RECENTLY POSTED CARE PROVIDERS & SEEKERS";
                                i.stackdetails = false;
                            }
                          
                            i.adtype = i.secondarycategoryvalue + " , " + i.tertiarycategoryvalue + " in " + i.City + " , " + i.Statecode;
                        }
                        var sorted = from srch in list.RowDs group srch by srch.header into searchGroup select new Grouping<string, DCsearch_Data>(searchGroup.Key, searchGroup);

                        srchlistview.ItemsSource = sorted;
                        stacknoresult.IsVisible = false;
                        srchlistview.IsVisible = true;
                    }
                    else
                    {
                        stacknoresult.IsVisible = true;
                        srchlistview.IsVisible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                string mm = ex.Message;
            }
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
        private void Backbtncommand(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        public string Contentid = "";
        private void srchlistview_Tap(object sender, ItemTappedEventArgs e)
        {
            var radiogrp = sender as ListView;
            DCsearch_Data lsow = new DCsearch_Data();
            lsow = (DCsearch_Data)radiogrp.SelectedItem;
            userpid = Commonsettings.UserPid;
            if (string.IsNullOrEmpty(userpid))
            {
                userpid = "";
            }
            Contentid = lsow.Businessid;
            if (lsow.Search == "offered_latlong" || lsow.Search == "wanted_latlong" || lsow.Search == "wanted_latlong" || lsow.Search== "daycare_latest")
            {
                Navigation.PushAsync(new Features.Detail.Views.CareDetail(lsow.Businessid,lsow.Trackurl,""));
            }
            else //daycare_latest_category
            {
                //searchres.careprovidertype = lsow.Titleurl;
                searchres.newcategoryurl = lsow.Titleurl;
                searchres.cityurl = lsow.Newcityurl;
               // if(string.IsNullOrEmpty(searchres.careprovidertype))
                //{
                    searchres.careprovidertype = lsow.Titleurl;
                //}
                if(string.IsNullOrEmpty(lsow.City))
                {
                    searchres.city = Commonsettings.Usercity;
                }
                Navigation.PushAsync(new Features.List.Views.DaycareListing(searchres));
            }
        }
    }
}