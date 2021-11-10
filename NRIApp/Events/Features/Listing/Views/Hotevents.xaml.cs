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
	public partial class Hotevents : ContentPage
	{
		public Hotevents (string tagtype)
		{
			InitializeComponent ();
            Title = "Top Events";
            BindingContext = new NRIApp.Events.Features.Listing.ViewModel.Eevents_VM("hotevents",tagtype);
		}
	}
}