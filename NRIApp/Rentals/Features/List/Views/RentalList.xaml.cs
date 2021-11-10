using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRIApp.Rentals.Features.List.ViewModels;
using NRIApp.Rentals.Features.Post.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Rentals.Features.List.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RentalList : ContentPage
	{
		public RentalList (RTResponse categoryurl, string cityurl)
        {
            InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new VMRentals(categoryurl, cityurl);
            Title = "Rentals";
        }
	}
}