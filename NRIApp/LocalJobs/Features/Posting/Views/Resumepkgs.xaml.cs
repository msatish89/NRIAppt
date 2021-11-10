using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Posting.Interfaces;
using NRIApp.LocalJobs.Features.Posting.Models;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Posting.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Resumepkgs : ContentPage
	{
		public Resumepkgs (Resumepkgdetails list)
		{
			InitializeComponent ();
            Title = "Choose Package";
            //try
            //{
            //    var couponapi = RestService.For<IJobPayment>(Commonsettings.LocaljobsAPI);
            //    var details = couponapi.Getcouponcode();
            //    if (details != null)
            //    {
            //        list.appcouponcode = "Coupon code: " + details.Result.resultinformation;
            //    }
            //}
            //catch (Exception ex)
            //{

            //}
            this.BindingContext = new ViewModels.ResumePkgpayment_VM(list, "package");
		}
	}
}