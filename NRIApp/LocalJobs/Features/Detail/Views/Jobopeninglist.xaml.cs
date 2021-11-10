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
	public partial class Jobopeninglist : ContentPage
	{
		public Jobopeninglist (string sdata)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            string type = "JOtype";
            Title = "Job Openings";
            BindingContext = new Features.Detail.ViewModels.CompanyprofileVM(sdata,type);
		}
	}
}