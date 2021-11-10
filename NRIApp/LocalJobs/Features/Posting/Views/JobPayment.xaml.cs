using NRIApp.LocalJobs.Features.Posting.Models;
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
	public partial class JobPayment : ContentPage
	{
        public JobPayment (string amount, string banneramnt, string nationwideamnt, int citycnt, string ordertype, string totalamnt,string percityamnt, Postingdata pst, decimal topbannerchk, decimal exclusivemailerchk, decimal jobslotchk, decimal bonusdaychk,string daycnt)
		{
			InitializeComponent ();
            Title = "Payment";
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            packamt.Text = "$ " + amount.Replace(".0", ".00"); ;
            if(pst.isnationwidechk=="1")
            {
                decimal natonamnt = Convert.ToDecimal(nationwideamnt);
                nationwideamt.Text = "$ " + natonamnt.ToString("N");
                stacknationwide.IsVisible = true;
                stackcity.IsVisible = false;
            }
            else if(citycnt >0)
            {
                decimal cityamount = citycnt * Convert.ToDecimal(percityamnt);
                cityamt.Text = "$ " + cityamount.ToString("N");
                stacknationwide.IsVisible = false;
                stackcity.IsVisible = true;
            }
           
            totalamt.Text =  totalamnt;
            if (pst.isbannerchk=="1")
            {
                stackbanner.IsVisible = true;
                decimal banamnt = Convert.ToDecimal(banneramnt);
                banneramt.Text = "$ " + banamnt.ToString("N");
            }
            if(!string.IsNullOrEmpty(pst.appcouponcode))
            {
                appcouponcdtxt.Text = pst.appcouponcode;
            }
            this.BindingContext = new ViewModels.JobPayment_VM(amount,   nationwideamnt,  citycnt,  ordertype,  totalamnt, percityamnt, pst , topbannerchk, exclusivemailerchk, jobslotchk, bonusdaychk,daycnt);
		}

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.sulekha.com/collateral/terms.aspx"));
        }
    }
}