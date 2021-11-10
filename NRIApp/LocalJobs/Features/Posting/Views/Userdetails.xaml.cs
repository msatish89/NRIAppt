using NRIApp.LocalJobs.Features.Posting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRIApp.LocalJobs.Features.Posting.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Posting.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Userdetails : ContentPage
	{
        public Userdetails(Postingdata pd)
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Enter Contact Details";
            if(pd.ismyneed=="1" && string.IsNullOrEmpty(pd.useremail))
            {
                lblemail.IsEnabled = false;
            }
            this.BindingContext = new USerdetailsVM(pd);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.sulekha.com/collateral/terms.aspx"));
        }
    }
}