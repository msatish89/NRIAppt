using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NRIApp.Rentals.Features.Post.ViewModels;
using NRIApp.Rentals.Features.Post.Models;

namespace NRIApp.Rentals.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Catsec : ContentPage
	{
		public Catsec (RTCategoryList obj)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new VMRTCategory(obj, "2");
            if (obj.primarycategoryid == 43)
            Title = "Number of Vehicles";
            else
            Title = "Number of Beds";
        }
	}
}