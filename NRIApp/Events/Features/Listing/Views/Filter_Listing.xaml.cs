using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRIApp.Events.Features.Listing.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Events.Features.Listing.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Filter_Listing : ContentPage
	{
		public Filter_Listing (string metrourl,string category, string language, string startrprice, string endprice, string startdate, string enddate)
		{
			InitializeComponent ();
            Title = "Filter Listing";
            BindingContext = new Events_Filter_VM(metrourl, category, language, startrprice, endprice, startdate, enddate);
		}
	}
}