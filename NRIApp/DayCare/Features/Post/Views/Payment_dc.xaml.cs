using NRIApp.DayCare.Features.Post.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.DayCare.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Payment_dc : ContentPage
	{
		public Payment_dc (Daycareposting postdata,string pkgname, string paymentamt)
		{
			InitializeComponent ();
            Title = "Payment";
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            //packamt.Text = "$ " + paymentamt.Replace(".0", ".00"); 
            packamt.Text = paymentamt.Replace(".0", ".00");
            totalamt.Text= paymentamt.Replace(".0", ".00");
            BindingContext = new ViewModels.PaymentDC_VM(postdata, pkgname,paymentamt);
        }
	}
}