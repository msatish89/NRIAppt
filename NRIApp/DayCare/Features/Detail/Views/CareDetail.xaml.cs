using NRIApp.DayCare.Features.List.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
using NRIApp.Helpers;

namespace NRIApp.DayCare.Features.Detail.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CareDetail : ContentPage
	{
        IUserDialogs _Dialog = UserDialogs.Instance;
        public string businessID = "", adURL = "", premiumAD = "";
		public CareDetail (string businessid,string adurl,string premiumad)
		{
			InitializeComponent ();
            businessID = businessid;
            adURL = adurl;
            premiumAD = premiumad;
            BindingContext = new ViewModels.CareDetailVM(businessid,adurl,premiumad);
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if(Commonsettings.UserDeviceOSVersion == "android")
            {
                Navigation.PopAsync();
            }
           else
            {
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            }
        }

       
    }
}