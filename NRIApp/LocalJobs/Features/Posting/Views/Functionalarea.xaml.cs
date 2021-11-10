using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRIApp.LocalJobs.Features.Posting.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Posting.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Functionalarea : ContentPage
	{
		public Functionalarea (int jobtype, Models.Postingdata jobdata )
		{
			InitializeComponent ();
            Title = "Functional Area";
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
			try
            {
				MyneedsNxtVisible.IsVisible = false;
                if (jobdata.ismyneed == "1")
                {
                    MyneedsNxtVisible.IsVisible = true;
                }
                this.BindingContext = new FunctionalareaVM(jobtype, jobdata);
			}
			catch(Exception ex)
            {

            }
		}
	}
}