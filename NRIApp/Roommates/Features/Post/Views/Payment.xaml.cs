using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using NRIApp.Roommates.Features.Post.Models;
//using Stripe;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Roommates.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Payment : ContentPage
	{
        IUserDialogs dialog = UserDialogs.Instance;
        public Payment (string amount, string ordertype,string adid, Postfirst pst)
		{
			InitializeComponent ();
            Title = "Payment";
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            packamt.Text = amount;
            totalamt.Text = amount;
            countrycodetxt.Title = "+" + pst.Countrycode;
            BindingContext = new ViewModels.Payment_VM(amount,ordertype,adid,pst);
		}
       
    }
}