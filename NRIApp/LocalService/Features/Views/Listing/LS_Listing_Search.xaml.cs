using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views.Listing
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LS_Listing_Search : ContentPage
	{
		public LS_Listing_Search (string ptagid,string cityurl)
		{
			InitializeComponent ();

            BindingContext = new ViewModels.LS_Listing_Search(ptagid,cityurl);
		}
	}
}