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
	public partial class additionalcities : ContentPage
	{
		public additionalcities (Postingdata data, string package, string amnt, decimal cityamnt, string banneramnt, string nationwideamnt,string page,string emailblastamt,string addonsmt,decimal perdaycost,int daycounttxtID, string topbannerbtntxtid, string excmailerbtntxtid, string jobslotbtntxtid, string bonusdaybtntxtid)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Expand to more cities";
            this.BindingContext = new ViewModels.JobPackage_VM(data, package, amnt, cityamnt, banneramnt, nationwideamnt,page,  emailblastamt,  addonsmt,  perdaycost,daycounttxtID, topbannerbtntxtid, excmailerbtntxtid, jobslotbtntxtid, bonusdaybtntxtid);
		}
	}
}