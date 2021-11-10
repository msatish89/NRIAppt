using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Detail.Views;
using NRIApp.LocalJobs.Features.Listing.Interfaces;
using NRIApp.LocalJobs.Features.Listing.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Listing.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Jobsearch : ContentPage
	{
        public string city_url = "";
        public double city_lat;
        public double city_lng;
        public string metro_url = "";
        public string userpid = Commonsettings.UserPid;
        LocalJobSenddata listdata = new LocalJobSenddata();
        public Jobsearch (LocalJobSenddata lstdt)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");

            listdata = lstdt;
            city_url = lstdt.cityurl;
            city_lat = lstdt.userlat;
            city_lng = lstdt.userlong;
            metro_url = lstdt.metrourl;

        }
      
        private void Backbtncommand(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        protected override void OnAppearing()
        {
            textsearch.Focus();
            listdata.industryids = "";
            listdata.visatypeids = "";
            listdata.skillids = "";
            listdata.functionids = 0;
            listdata.roleid = "";
            listdata.employmenttype = "";
            
            listdata.businessurl = "";
        }

        private async void jobsearch(object sender, TextChangedEventArgs e)
        {
            try
            {
                //https://techjobs.sulekha.com/mobileapp/localjobs/jobsposting-api.aspx?type=searchads&title=busin&rowstofetch=20&cityurl=new-york-ny
                string keyword = textsearch.Text;
                if (keyword.Length > 1)
                {
                    var currentpage = GetCurrentPage();
                    var nsAPI = RestService.For<ILocalJob>(Commonsettings.LocaljobsAPI);
                    if(!string.IsNullOrEmpty(metro_url))
                    {
                        city_url = "";
                    }
                    else
                    {
                        metro_url = "";
                    }
                    
                    Models.SearchList_Jobs list = await nsAPI.searchlist(keyword, city_url, city_lat, city_lng,metro_url,listdata.city,"+1",listdata.statecode);
                    if (list.ROW_DS.Count() > 0 && list != null)
                    {
                        foreach (var i in list.ROW_DS)
                        {
                            if (i.search == "functionalarea")
                            {
                                i.header = "JOB BY FUNCTIONAL AREA";
                                i.stackdetails = false;
                            }
                            if (i.search == "role")
                            {
                                i.header = "JOB BY ROLE";
                                i.stackdetails = false;
                            }
                            if (i.search == "industry")
                            {
                                i.header = "JOB BY INDUSTRY";
                                i.stackdetails = false;
                            }
                            if (i.search == "skills")
                            {
                                i.header = "JOB BY SKILLS";
                                i.stackdetails = false;
                            }
                            if (i.search == "company")
                            {
                                i.header = "JOB BY COMPANY";
                                i.stackdetails = false;
                            }
                            if (i.search == "emptype")
                            {
                                i.header = "JOB BY TYPE";
                                i.stackdetails = false;
                            }
                            if (i.search == "workauth")
                            {
                                i.header = "JOB BY WORK AUTHORIZATION";
                                i.stackdetails = false;
                            }
                            if (i.search == "localjobs_latlong")
                            {
                                i.header = "TOP JOBS RESULTS (TOP JOBS RESULTS " + Commonsettings.Usercity.ToUpper() + ")";
                                i.stackdetails = true;
                            }
                            if (i.search == "localjobs_latest")
                            {
                                i.header = "LATEST JOBS RESULTS";
                                i.stackdetails = true;
                            }
                            i.adtype = i.adtype + " , " + i.jobrole + " in " + i.cityname + " , " + i.statecode;
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
            catch (Exception ex)
            {
                string mm = ex.Message;
            }
        }
        private void srchlistview_Tap(object sender, ItemTappedEventArgs e)
        {
            var radiogrp = sender as ListView;
            Search lsow = new Search();
            lsow = (Search)radiogrp.SelectedItem;
            string titleurl = lsow.titleurl;
            if (string.IsNullOrEmpty(userpid))
            {
                userpid = "";
            }
            string Contentid = lsow.adid;
            if (lsow.search == "localjobs_latlong" || lsow.search == "localjobs_latest")
            {
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                Navigation.PushAsync(new JobDetails(Contentid,titleurl,userpid));
            }
            else
            {
                if (lsow.search == "emptype")
                {
                    listdata.employmenttype = lsow.contentid.ToString();
                }
                else if (lsow.search == "role")
                {
                    listdata.roleid = lsow.contentid.ToString();
                }
                else if (lsow.search == "skills")
                {
                    listdata.skillids = lsow.contentid.ToString();
                }
                else if (lsow.search == "functionalarea")
                {
                    listdata.functionids = lsow.contentid;
                }
                else if (lsow.search == "company")
                {
                    listdata.businessurl = lsow.titleurl;
                }
                else if (lsow.search == "industry")
                {
                    listdata.industryids = lsow.contentid.ToString();
                }
                else if (lsow.search == "workauth")
                {
                    listdata.visatypeids = lsow.contentid.ToString();
                }
                if(lsow.search== "company")
                {
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                    Navigation.PushAsync(new Features.Detail.Views.CompanyProfile(lsow));
                }
                else
                {
                    listdata.jobtype = "-1";
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                    Navigation.PushAsync(new JobList(listdata));
                }
            }
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
    }
}