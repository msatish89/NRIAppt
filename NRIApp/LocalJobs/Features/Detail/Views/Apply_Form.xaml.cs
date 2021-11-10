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
	public partial class Apply_Form : ContentPage
	{
		public Apply_Form (string adid, string premiumad, string resumemandatory,string cprofile)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new LocalJobs.Features.Detail.ViewModels.ApplyformVM(adid,premiumad,resumemandatory,cprofile);
            Title = "Apply for Job";
		}

        private void gototc(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }
    }
}