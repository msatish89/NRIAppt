using NRIApp.Rentals.Features.List.Models;
using NRIApp.Rentals.Features.List.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Rentals.Features.List.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailRentalList : ContentPage
	{
        public DetailRentalList(string Adid,string cityurl)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            this.BindingContext = new VMRT_Detail(Adid,cityurl);
           
		}
	}
}