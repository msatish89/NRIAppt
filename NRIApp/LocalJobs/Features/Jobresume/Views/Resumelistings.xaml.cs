using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Listing.Models;
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
    public partial class Resumelistings : ContentPage
    {
        LocalJobSenddata reflistdata = new LocalJobSenddata();
        public Resumelistings(LocalJobSenddata listdata)
        {
            InitializeComponent();
            reflistdata = listdata;
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            if (string.IsNullOrEmpty(listdata.jobtype))
            {
                listdata.jobtype = "-1";
            }
            if (string.IsNullOrEmpty(listdata.nearby))
            {
                listdata.nearby = "0";
            }
            if (string.IsNullOrEmpty(listdata.orderby))
            {
                listdata.orderby = "ordergroup";
            }
            if (string.IsNullOrEmpty(listdata.zipcode))
            {
                listdata.userlat = Commonsettings.UserLat;
                listdata.userlong = Commonsettings.UserLong;
                listdata.userpid = Commonsettings.UserPid;
                listdata.cityurl = Commonsettings.Usercityurl;
                listdata.city = Commonsettings.Usercity;
                listdata.zipcode = Commonsettings.Userzipcode;
                listdata.statecode = Commonsettings.Userstatecode;
            }
            // Title = "Find All Latest IT / Non IT Job Offers";
            Title = "Find Jobseekers";
            if (LocalJobs.Features.Jobresume.ViewModels.Resumelistings_VM.isdownloadresumecnt != 1)
            {
                BindingContext = new LocalJobs.Features.Jobresume.ViewModels.Resumelistings_VM(listdata);
            }
        }
        protected override void OnAppearing()
        {
            try
            {
                if(LocalJobs.Features.Jobresume.ViewModels.Resumelistings_VM.isdownloadresumecnt == 1)
                {
                    BindingContext = new LocalJobs.Features.Jobresume.ViewModels.Resumelistings_VM(reflistdata);
                }
                //base.OnAppearing();
                NRIApp.LocalJobs.Features.Jobresume.ViewModels.Resumelistings_VM.filtercnt = 0;
                NRIApp.LocalJobs.Features.Jobresume.ViewModels.Resumelistings_VM.sortingcnt = 0;
                NRIApp.LocalJobs.Features.Jobresume.Views.Seekerresponsetq.resumedtlpagecode = 0;
                if (NRIApp.LocalJobs.Features.Jobresume.ViewModels.Resumelistings_VM.logincode == 1 && Commonsettings.UserPid != "0" && Commonsettings.UserPid != null && Commonsettings.UserPid != "")
                {
                    Navigation.PushAsync(new NRIApp.LocalJobs.Features.Jobresume.Views.Savedresumes());
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}