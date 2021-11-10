using NRIApp.DayCare.Features.Post.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.DayCare.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PostThird : ContentPage
	{
		public PostThird (Daycareposting postdata)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            if(postdata.ismyneed=="1")
            {
                listingbtn.IsVisible = false;
                //txtcontactphone.IsEnabled = false;
            }
            BindingContext = new ViewModels.PostThird_VM(postdata);
            if (postdata.ismyneed == "1")
            {
                if (!postdata.countrycode.Contains("+"))
                {
                    countrycode.SelectedItem = "+" + postdata.countrycode;
                }
                else
                {
                    countrycode.SelectedItem = postdata.countrycode;
                }
            }
            Title = "Contact Details";
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.sulekha.com/collateral/terms.aspx"));
        }
        
    }
}