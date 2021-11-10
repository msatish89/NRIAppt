using NRIApp.LocalJobs.Features.Posting.Models;
using NRIApp.LocalService.Features.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views.Detail
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OnlineClassPayment : ContentPage
	{
        public OnlineClassPayment(string guserid,string classdetailid)
		{
			InitializeComponent ();
           
            this.BindingContext = new ViewModels.LS_Online_Class_Payment_VM(guserid, classdetailid);
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }
    }
}