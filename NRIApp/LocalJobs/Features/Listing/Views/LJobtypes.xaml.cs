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
	public partial class LJobtypes : ContentPage
	{
		public LJobtypes ()
		{
			InitializeComponent ();
            Title = "Job Type";
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
        }
        public void ittapped(object sender, EventArgs e)
        {
            LocalJobSenddata listdatas = new LocalJobSenddata();
            listdatas.jobtype = "1";
            Navigation.PushAsync(new LocalJobs.Features.Listing.Views.JobList(listdatas));
        }
        public void nonittapped(object sender, EventArgs e)
        {
            LocalJobSenddata listdatas = new LocalJobSenddata();
            listdatas.jobtype = "2";
            Navigation.PushAsync(new LocalJobs.Features.Listing.Views.JobList(listdatas));
        }

        private void Anytapped(object sender, EventArgs e)
        {
            LocalJobSenddata listdatas = new LocalJobSenddata();
            listdatas.jobtype = "-1";
            Navigation.PushAsync(new LocalJobs.Features.Listing.Views.JobList(listdatas));
        }
    }
}