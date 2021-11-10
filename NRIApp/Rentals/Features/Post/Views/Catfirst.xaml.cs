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
	public partial class Catfirst : ContentPage
	{
		public Catfirst (int stagid, string stype, RTCategoryList needlist)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            
            BindingContext = new VMRTCategory(stagid, stype,needlist);
            Title = "Property Type";
        }
        
	}
}