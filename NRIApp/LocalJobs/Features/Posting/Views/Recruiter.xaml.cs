using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.LocalJobs.Features.Posting.ViewModels;


namespace NRIApp.LocalJobs.Features.Posting.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Recruiter : ContentPage
	{
		public Recruiter ()
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Establish yourself ";
            this.BindingContext = new JobsRecruiterVM();

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //Listing.Models.LocalJobSenddata listdatas = new Listing.Models.LocalJobSenddata();
            //Navigation.PushAsync(new LocalJobs.Features.Listing.Views.JobList(listdatas));
             Navigation.PushAsync(new LocalJobs.Features.Listing.Views.LJobtypes());
        }
    }
}