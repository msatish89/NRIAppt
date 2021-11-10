using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.DayCare.Features.List.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Savedads : ContentPage
	{
		public Savedads ()
		{
			InitializeComponent ();
            NRIApp.DayCare.Features.List.ViewModels.DaycareListingVM.logincode = 0;
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new Features.List.ViewModels.Savedads_VM();
            Title = "Saved Ads";
        }
	}
}