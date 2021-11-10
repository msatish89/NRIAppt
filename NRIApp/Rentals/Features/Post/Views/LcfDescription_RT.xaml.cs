using NRIApp.Rentals.Features.Post.Models;
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
	public partial class LcfDescription_RT : ContentPage
	{
		public LcfDescription_RT (RTResponse res, DateTime Frdate)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Ad Description";
            if(res.ispostdesc=="1")
            {
                res.ispostdesc = "1";            }
            BindingContext = new ViewModels.VMRTLCF(res, Frdate,  res.ExpRent, res.countflag);
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            lcfsecond.postsuccessbackbtn = 0;
        }
    }
}