using NRIApp.Helpers;
using NRIApp.Rentals.Features.List.Views;
using NRIApp.Rentals.Features.Post.Interface;
using NRIApp.Rentals.Features.Post.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Rentals.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RTSearch : ContentPage
	{
        public string city_url = "";
        public string userpid = "";
        public string Contentid = "";

        public RTSearch (string cityurl)
		{
            city_url = cityurl;
			InitializeComponent ();
            Getcommonsearchresults();
        }
        public void Getcommonsearchresults()
        {
            List<RentalsSearch> propertytype = new List<RentalsSearch>();
            
            propertytype.Add(new RentalsSearch { roomtype = "8", title = "Homes", search = "search_term", header = "Related Services" });
            propertytype.Add(new RentalsSearch { roomtype = "2", title = "Apartment", search = "search_term", header = "Related Services" });
            propertytype.Add(new RentalsSearch { roomtype = "3", title = "Condo", search = "search_term", header = "Related Services" });
            propertytype.Add(new RentalsSearch { roomtype = "6", title = "Town house", search = "search_term", header = "Related Services" });
           // propertytype.Add(new RentalsSearch { roomtype = "4", title = "Office Space", search = "search_term", header = "Related Services" });
            //propertytype.Add(new RentalsSearch { roomtype = "5", title = "Retail Outlet", search = "search_term", header = "Related Services" });
            propertytype.Add(new RentalsSearch { roomtype = "1", title = "Single Family Home", search = "search_term", header = "Related Services" });
            propertytype.Add(new RentalsSearch { roomtype = "9", title = "Houses", search = "search_term", header = "Related Services" });
            propertytype.Add(new RentalsSearch { roomtype = "10", title = "Hostels", search = "search_term", header = "Related Services" });
            propertytype.Add(new RentalsSearch { roomtype = "11", title = "Hotels", search = "search_term", header = "Related Services" });
            propertytype.Add(new RentalsSearch { roomtype = "12", title = "Basement Apartment", search = "search_term", header = "Related Services" });
            propertytype.Add(new RentalsSearch { roomtype = "13", title = "Parking Garage", search = "search_term", header = "Related Services" });
            //propertytype.Add(new RentalsSearch { roomtype = "7", title = "Others", search = "search_term", header = "Related Services" });

            var groupedData = propertytype.GroupBy(e => e.header)
                 .Select(e => new ObservableGroupCollection<string, RentalsSearch>(e))
                 .ToList();
            srchlistview.ItemsSource = groupedData;

            //List<adtype> lsow = new List<adtype>();
            //lsow.Add(new adtype { typeid = 1, stagid = 2, type = "I have a room to rent" });
            //lsow.Add(new adtype { typeid = 0, stagid = 5, type = "I need a room for rent" });
            //offerwantlist = lsow;
        }
        protected override void OnAppearing()
        {
            textsearch.Focus();
        }

        public async void homesearch(object sender, TextChangedEventArgs e)
        {
            //IsVisibleheadertext.IsVisible = false;
            string keyword = textsearch.Text;
            if (keyword.Length > 1)
            {
                var nsAPI = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                Models.RentalsSearchList list = await nsAPI.getsearchresult(keyword, Commonsettings.Usercityurl);
                if (list.ROW_DS.Count() > 0 && list != null)
                {

                    foreach (var i in list.ROW_DS)
                    {
                        if (i.search == "rentals_latest")
                        {
                            i.header = "Latest ads in Rentals";
                            i.stackdetails = true;
                        }
                        else if (i.search == "rentals_latest_location")
                        {
                            i.header = "Top Rentals Results (In and near " + Commonsettings.Usercity + ")";
                            i.stackdetails = true;
                        }
                        else if (i.search == "search_term")
                        {
                            i.header = "Related search term";
                            i.stackdetails = false;
                        }
                    }
                    var sorted = from srch in list.ROW_DS group srch by srch.header into searchGroup select new Grouping<string, RentalsSearch>(searchGroup.Key, searchGroup);

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
        RTResponse searchres = new RTResponse();
        private void srchlistview_Tap(object sender, ItemTappedEventArgs e)
        {
            var radiogrp = sender as ListView;
            RentalsSearch lsow = new RentalsSearch();
            lsow = (RentalsSearch)radiogrp.SelectedItem;
            userpid = Commonsettings.UserPid;
            if (string.IsNullOrEmpty(userpid))
            {
                userpid = "";
            }
            Contentid = lsow.adid;
            if(lsow.search== "rentals_latest" || lsow.search== "rentals_latest_location")
            {
                Navigation.PushAsync(new DetailRentalList(Contentid, city_url));
            }
            else
            {
                if (lsow.id == "38")
                    searchres.primarycategoryvalue = "8";
                if (lsow.id == "39")
                    searchres.primarycategoryvalue = "9";
                if (lsow.id == "40")
                    searchres.primarycategoryvalue = "10";
                if (lsow.id == "41")
                    searchres.primarycategoryvalue = "11";
                if (lsow.id == "42")
                    searchres.primarycategoryvalue = "12";
                if (lsow.id == "43")
                    searchres.primarycategoryvalue = "13";
                if (lsow.id == "28")
                    searchres.primarycategoryvalue = "1";
                if (lsow.id == "29")
                    searchres.primarycategoryvalue = "2";
                if (lsow.id == "31")
                    searchres.primarycategoryvalue = "6";
                if (lsow.id == "30")
                    searchres.primarycategoryvalue = "3";

                Navigation.PushAsync(new RentalList(searchres, city_url));
            }
            

        }

        private void Backbtncommand(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}