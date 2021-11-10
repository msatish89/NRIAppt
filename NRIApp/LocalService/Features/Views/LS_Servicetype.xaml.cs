using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LS_Servicetype : ContentPage
	{
		public LS_Servicetype (string type="")
		{
			InitializeComponent ();
            Title = "Service type";
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");

            BindingContext = new NRIApp.LocalService.Features.ViewModels.LS_Servicetype(type);

        }
        
    }
}