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
	public partial class JobDetails : ContentPage
	{
        
		public JobDetails (string adid,string titleurl,string userpid)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            NRIApp.LocalJobs.Features.Detail.Views.Responsetq.dtlpagecode = 1;
            BindingContext = new ViewModels.JobDetailVM(adid,titleurl,userpid);
        }
	}
}