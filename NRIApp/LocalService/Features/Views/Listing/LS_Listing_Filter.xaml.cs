using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views.Listing
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LS_Listing_Filter : ContentPage
	{
		public LS_Listing_Filter ()
		{
			InitializeComponent ();
          
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#e30045");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            string ptagid = ViewModels.LS_ViewModel.stagid;
            BindingContext = new ViewModels.LS_Listing_Search(ptagid,"filter","1");
		}
	}
}