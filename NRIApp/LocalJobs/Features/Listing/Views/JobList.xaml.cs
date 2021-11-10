using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Listing.Models;
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
	public partial class JobList : ContentPage
	{
        
        public JobList (LocalJobSenddata listdata)
		{
			InitializeComponent ();
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
            Title = "Find the job offers";
            BindingContext = new LocalJobs.Features.Listing.ViewModels.JoblistVM(listdata);
		}
        protected override void OnAppearing()
        {
          try
            {
                //base.OnAppearing();
                ViewModels.JoblistVM.filtercnt = 0;
                ViewModels.JoblistVM.sortingcnt = 0;
                NRIApp.LocalJobs.Features.Detail.Views.Responsetq.dtlpagecode = 0;
                if (ViewModels.JoblistVM.logincode == 1 && Commonsettings.UserPid != "0" && Commonsettings.UserPid != null && Commonsettings.UserPid != "")
                {
                    Navigation.PushAsync(new NRIApp.LocalJobs.Features.Listing.Views.Savedjobs());
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}