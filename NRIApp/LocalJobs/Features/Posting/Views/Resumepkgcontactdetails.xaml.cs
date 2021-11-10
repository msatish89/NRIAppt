using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRIApp.LocalJobs.Features.Posting.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Posting.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Resumepkgcontactdetails : ContentPage
	{
		public Resumepkgcontactdetails (Resumepkgdetails details)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Your Contact Details";
            this.BindingContext = new ViewModels.ResumePosting_VM(details);
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.sulekha.com/collateral/terms"));
        }
    }
}