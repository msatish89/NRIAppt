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
	public partial class Contact_RM : ContentPage
	{
		public Contact_RM (string adid)
		{
            int type = 0;int type1 = 0;int type2 = 0;
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Contact Advertiser";
            BindingContext = new ViewModels.VM_Detail(adid, type, type1, type2);
        }
        public void gototc()
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }
    }
}