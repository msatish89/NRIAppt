using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Posting.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Resumepkgbizdetails : ContentPage
	{
		public Resumepkgbizdetails ()
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Company Details";
            this.BindingContext = new ViewModels.ResumePosting_VM();
		}
        private void Uaddress_Focused(object sender, FocusEventArgs e)
        {
            stackbiz.IsVisible = false;
            stackind.IsVisible = false;
        }

        private void Uindname_Focused(object sender, FocusEventArgs e)
        {
            stackbiz.IsVisible = false;
        }
    }
}