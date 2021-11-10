using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Jobresume.Interfaces;
using NRIApp.LocalJobs.Features.Jobresume.Models;
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

namespace NRIApp.LocalJobs.Features.Jobresume.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    //public partial class Jobrolesearch : ContentPage
    //{
    //    public Jobrolesearch()
    //    {
    //        InitializeComponent();
    //    }
    //}
    public partial class Jobrolesearch : ContentPage
    {
        public string city_url = "";
        public double city_lat;
        public double city_lng;
        public string metro_url = "";
        public string userpid = Commonsettings.UserPid;
        LocalJobSenddata listdata = new LocalJobSenddata();
        public Jobrolesearch(LocalJobSenddata lstdt)
        {
            InitializeComponent();
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
            //listdata.industryids = "";
            //listdata.visatypeids = "";
            //listdata.skillids = "";
            //listdata.functionids = 0;
            //listdata.roleid = "";
            //listdata.employmenttype = "";

            //listdata.businessurl = "";
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
                    var nsAPI = RestService.For<IJobresume>(Commonsettings.LocaljobsAPI);
                    //if (!string.IsNullOrEmpty(metro_url))
                    //{
                    //    city_url = "";
                    //}
                    //else
                    //{
                    //    metro_url = "";
                    //}

                    NRIApp.LocalJobs.Features.Jobresume.Models.Jobrole_search list = await nsAPI.Getjobroles(keyword,"3");
                    if (list.RowData.Count() > 0 && list != null)
                    {
                        foreach (var i in list.RowData)
                        {
                            if (i.Categorytype == "role")
                            {
                                i.header = "JOB BY ROLE";
                            }
                         
                        }
                        var sorted = from srch in list.RowData group srch by srch.header into searchGroup select new Grouping<string, Jobrole_search_data>(searchGroup.Key, searchGroup);

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
            Jobrole_search_data lsow = new Jobrole_search_data();
            lsow = (Jobrole_search_data)radiogrp.SelectedItem;
            //string titleurl = lsow.Tagurl;
            if (string.IsNullOrEmpty(userpid))
            {
                userpid = "";
            }
            //string Contentid = lsow.adid;
            listdata.jobrole = listdata.roletxt = lsow.Tag;
            
            listdata.roleid = lsow.Contentid;
            listdata.jobtype = "-1";
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            Navigation.PushAsync(new NRIApp.LocalJobs.Features.Jobresume.Views.Resumelistings(listdata));
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
    }
}
        