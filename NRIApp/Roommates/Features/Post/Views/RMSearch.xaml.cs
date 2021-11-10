using NRIApp.Helpers;
using NRIApp.Roommates.Features.Post.Interface;
using NRIApp.Roommates.Features.Post.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Roommates.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RMSearch : ContentPage
	{
        public string city_url = "";
        public string userpid = "";
        public string Contentid = "";
        public List<SearchList> getpropertytype { get; set; }
        public RMSearch (string cityurl)
		{
			InitializeComponent ();
            city_url = cityurl;
            Getcommonsearchresults();
        }
        public void Getcommonsearchresults()
        {
            List<Search> propertytype = new List<Search>();
            propertytype.Add(new Search { id = "6",roomtype="1", title = "Shared Room", search = "search_term",header= "Related Services" });
            propertytype.Add(new Search { id = "7", roomtype = "2", title = "Single Room", search = "search_term", header = "Related Services" });
            propertytype.Add(new Search { id = "8", roomtype = "4", title = "Paying Guest", search = "search_term", header = "Related Services" });

            var groupedData = propertytype.GroupBy(e => e.header)
                 .Select(e => new ObservableGroupCollection<string, Search>(e))
                 .ToList();
            srchlistview.ItemsSource = groupedData;
        }
        protected override void OnAppearing()
        {
            textsearch.Focus();
        }

        public async void homesearch(object sender, TextChangedEventArgs e)
        {
           // IsVisibleheadertext.IsVisible = false;
            string keyword = textsearch.Text;
            if (keyword.Length > 1)
            {
                var currentpage = GetCurrentPage();
                var nsAPI = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                Models.SearchList list = await nsAPI.getsearchresult(keyword,city_url);
                string usercity = city_url.Replace("-", " ");
                if (list.ROW_DS.Count()>0 && list != null)
                {

                    foreach (var i in list.ROW_DS)
                    {
                        if (i.search == "roommates_latest")
                        {
                            i.header = "Latest ads in Roommates";
                            i.stackdetails = true;
                        }
                        else if (i.search == "roommates_latest_location")
                        {
                            i.header = "Top Roomates Results (In and near " + Commonsettings.Usercity + ")";
                            i.stackdetails = true;
                        }
                        else if (i.search == "search_term")
                        {
                            i.header = "Related search term";
                            i.stackdetails = false;
                        }

                    }
                    var sorted = from srch in list.ROW_DS group srch by srch.header into searchGroup select new Grouping<string, Search>(searchGroup.Key, searchGroup);

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
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
        Response searchres = new Response();
        private void srchlistview_Tap(object sender, ItemTappedEventArgs e)
        {
            var radiogrp = sender as ListView;
            Search lsow = new Search();
            lsow = (Search)radiogrp.SelectedItem;
            userpid = Commonsettings.UserPid;
            if (string.IsNullOrEmpty(userpid))
            {
                userpid = "";
            }
            Contentid = lsow.adid;
            if(lsow.search== "roommates_latest" || lsow.search== "roommates_latest_location")
            {
                Navigation.PushAsync(new Features.List.Views.DetailRMList(Contentid, userpid));
            }
            else
            {
                if (lsow.id == "7")
                    searchres.primarycategoryvalue = "2";
                if (lsow.id == "6")
                    searchres.primarycategoryvalue = "1";
                if (lsow.id == "8")
                    searchres.primarycategoryvalue = "4";
                //searchres.gender = "3";
                //searchres.categoryname = "Individual";
                Navigation.PushAsync(new Features.List.Views.Category(searchres, city_url));
            }
        }
   
        private void Backbtncommand(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}