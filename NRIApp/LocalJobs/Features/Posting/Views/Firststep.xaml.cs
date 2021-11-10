using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Listing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Posting.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Firststep : ContentPage
	{
		public Firststep ()
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
           // Title = "Job type";
        }
        
        private void recruiterTap(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Recruiter());
            //LocalJobSenddata listdatas = new LocalJobSenddata();
            //Navigation.PushAsync(new LocalJobs.Features.Listing.Views.JobList(listdatas));
        }
        Models.Postingdata jobdata = new Models.Postingdata();
        private async void postjobtap(object sender, EventArgs e)
        {
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                await Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
            }
            else
            {
                await Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Jobtype(jobdata));
            }
        }

        private void findresumetap(object sender, EventArgs e)
        {
            LocalJobSenddata listdata = new LocalJobSenddata();
            Navigation.PushAsync(new NRIApp.LocalJobs.Features.Jobresume.Views.Resumelistings(listdata));
        }
    }
}