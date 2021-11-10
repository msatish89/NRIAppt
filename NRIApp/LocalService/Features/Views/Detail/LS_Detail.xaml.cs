using NRIApp.LocalService.Features.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views.Detail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LS_Detail : ContentPage
	{
		public LS_Detail (LS_LISTINGMODEL_DATA LMD)
		{
			InitializeComponent ();
            Title = LMD.title;
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new  NRIApp.LocalService.Features.ViewModels.LS_Detail_VM(LMD);
            var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
           

        }
        protected  override bool OnBackButtonPressed()
        {
            var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
             curpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Listing.LS_Listing());
            return true;
        }
    }
}