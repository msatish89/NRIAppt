using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.Rentals.Features.Post.Models;
using NRIApp.Rentals.Features.Post.ViewModels;

namespace NRIApp.Rentals.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class lcfsecond : ContentPage
	{
        public static int postsuccessbackbtn = 0;
		public lcfsecond (RTResponse Catl, DateTime FrDate, string txtrent,string countflag)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new VMRTLCF(Catl, FrDate, txtrent,countflag);
            countrycode.SelectedIndex = 0;
            Title = "Enter Contact Details";
            if (Catl.ismyneed == "1")
            {
                txtcontactemail.IsEnabled = false;
            }
        }
        public void gototc()
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }
        
    }
}