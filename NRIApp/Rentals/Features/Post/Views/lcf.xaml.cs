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
	public partial class lcf : ContentPage
	{
		public lcf (RTCategoryList obj)
		{
			InitializeComponent ();
         
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new VMRTLCF(obj);
            if (obj.ismyneed == "1")
            {
                pricemodetxt.TextColor = Color.FromHex("#212121");
                leasetypetxt.TextColor = Color.FromHex("#212121");
                accomodatestxt.TextColor = Color.FromHex("#212121");
                attachedbathtxt.TextColor = Color.FromHex("#212121");
                parkingfrequency.TextColor = Color.FromHex("#212121");
                parkingtype.TextColor = Color.FromHex("#212121");
            }
            Title = "Enter Availability & Rent";
            //change title
        }
        protected override bool OnBackButtonPressed()
        {
            //if(contentlayout.IsVisible == true)
            //{
            //    contentlayout.IsVisible = false;
            //}
            //else if(contentlayout.IsVisible==false)
            //{
            //    Navigation.PopAsync();
            //}
            Navigation.PopAsync();
            return true;
        }
        private Page GetCurrentPage()
        {
            var currentpg = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpg;
        }
    }
}