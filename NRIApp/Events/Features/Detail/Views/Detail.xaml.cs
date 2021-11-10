using NRIApp.Events.Features.Listing.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Events.Features.Detail.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Detail : ContentPage
	{
		public Detail (string eventid,string title,string ticketingid)
		{           
			InitializeComponent();
            Title = title;
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new Events.Features.Detail.ViewModel.Detail_VM(eventid, ticketingid);

           

        }
	}
}