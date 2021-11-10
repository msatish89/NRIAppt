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
	public partial class Catthird : ContentPage
	{
		public Catthird (RTCategoryList obj)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new VMRTCategory(obj, "3");
            Title = "About You";
		}
	}
}