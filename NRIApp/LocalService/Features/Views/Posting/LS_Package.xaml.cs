using NRIApp.LocalService.Features.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views.Posting
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LS_Package : ContentPage
	{
		public LS_Package (string adid)
		{
			InitializeComponent ();
            Title = "Choose your package";
            LS_ViewModel.packageamount.Clear();
            LS_ViewModel.packagevalueamountdetails.Clear();
            LS_ViewModel.addionalcities.Clear();
            LS_ViewModel.valueadded = 0;
            LS_ViewModel.Valueaddedcontentid = "";
            if (LS_ViewModel.isOTPpage == 1)
            {
                var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
                currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                LS_ViewModel.isOTPpage = 0;             

            }
        //    BindingContext = new NRIApp.LocalService.Features.ViewModels.LS_Package_VM(adid);
		}

        
        protected override bool  OnBackButtonPressed()
        {
            LS_ViewModel.packageamount.Clear();
            LS_ViewModel.packagevalueamountdetails.Clear();
            LS_ViewModel.addionalcities.Clear();
            LS_ViewModel.valueadded = 0;
            LS_ViewModel.Valueaddedcontentid = "";
            if (LS_ViewModel.isOTPpage == 1)
            {
                var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
                currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                LS_ViewModel.isOTPpage = 0;
            }
            return true;
        }
     }
}