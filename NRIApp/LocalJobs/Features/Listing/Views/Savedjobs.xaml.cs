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
	public partial class Savedjobs : ContentPage
	{
		public Savedjobs ()
		{
			InitializeComponent ();
            ViewModels.JoblistVM.logincode = 0;
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new Features.Listing.ViewModels.SavedjobsVM();
            Title = "Saved Jobs";
		}
	}
}