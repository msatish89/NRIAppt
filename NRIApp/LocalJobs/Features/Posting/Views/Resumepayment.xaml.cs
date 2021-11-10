using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRIApp.LocalJobs.Features.Posting.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Posting.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Resumepayment : ContentPage
	{
		public Resumepayment (Resumepkgdetails details)
		{
			InitializeComponent ();
            resmcnt.Text = details.totalresume;
            adcnt.Text = details.totaladscount +" ads";
            if(details.totalamount.Contains("$"))
            {
                totalamt.Text = details.totalamount;
                packamt.Text = details.totalamount;
            }
            else
            {
                totalamt.Text = "$ " + details.totalamount;
                packamt.Text = "$ " + details.totalamount;
            }
            Title = "Payment";
            this.BindingContext = new ViewModels.ResumePkgpayment_VM(details, "payment");
		}
	}
}