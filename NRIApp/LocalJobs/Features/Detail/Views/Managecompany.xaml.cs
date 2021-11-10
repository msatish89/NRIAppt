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
	public partial class Managecompany : ContentPage
	{
		public Managecompany (Search cdata)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new Features.Detail.ViewModels.ManagecomapanyVM(cdata);
            Title = "Manage this company now!";

        }
	}
}