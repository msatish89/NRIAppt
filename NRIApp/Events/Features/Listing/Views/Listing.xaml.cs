using NRIApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Events.Features.Listing.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Listing : ContentPage
	{
		public Listing ()
		{
          
            InitializeComponent ();
            BindingContext = new NRIApp.Events.Features.Listing.ViewModel.Eevents_VM();
            Title = "Events Tickets in " + Commonsettings.Usercity;
        }
	}

   
}