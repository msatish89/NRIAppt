using NRIApp.Helpers;
using NRIApp.Rentals.Features.Post.Interface;
using NRIApp.Rentals.Features.Post.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Rentals.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Payment_RT : ContentPage
	{
		public Payment_RT (string amount,string ordertype ,string adid,RTPostFirst pft)
		{
			InitializeComponent ();
            BindingContext = new ViewModels.Payment_VMRT(amount,ordertype,adid,pft);
            Title = "Payment";
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            packamt.Text = amount;
            totalamt.Text = amount;
            countrycodetxt.Title ="+"+pft.Countrycode;
        }
      

    }
}