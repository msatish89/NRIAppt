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
	public partial class Contactjobseeker : ContentPage
	{
		public Contactjobseeker (string adid,string premiumad)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
			// BindingContext = new LocalJobs.Features.Detail.ViewModels.ApplyformVM(adid, premiumad, resumemandatory, cprofile);
			BindingContext = new LocalJobs.Features.Jobresume.ViewModels.ContactJobseeker_VM(adid,premiumad);
			Title = "Contact Jobseeker";
        }

        private void gototc(object sender, EventArgs e)
        {
			Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
		}
    }
}