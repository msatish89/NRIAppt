using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Listing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Detail.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CompanyProfile : ContentPage
	{
		public CompanyProfile (Search cdata)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new Features.Detail.ViewModels.CompanyprofileVM(cdata);
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void scrolldown(object sender, EventArgs e)
        {
          try
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    cmscroll.ScrollToAsync(cmlayout, ScrollToPosition.End, true);
                    
                });
            }
            catch(Exception ex)
            {

            }
        }
        protected override void OnAppearing()
        {
           try
            {
                if (NRIApp.USHome.Views.HomePage.seekerlogincode == 1 && Commonsettings.UserPid != "0" && Commonsettings.UserPid != null && Commonsettings.UserPid != "")
                {
                    Jobseeker.Models.Jobseekers_DATA data = new Jobseeker.Models.Jobseekers_DATA();
                    Navigation.PushAsync(new Features.Jobseeker.View.Jobseekerprofile(data));
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}