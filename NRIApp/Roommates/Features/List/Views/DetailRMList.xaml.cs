using NRIApp.Roommates.Features.List.Interface;
using NRIApp.Roommates.Features.List.Models;
using NRIApp.Roommates.Features.List.ViewModels;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Roommates.Features.List.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailRMList : ContentPage
	{
        public DetailRMList (string Contentid,string cityurl)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new VM_Detail(Contentid,cityurl);
        }
	}
}