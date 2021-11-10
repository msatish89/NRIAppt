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
using Acr.UserDialogs;

namespace NRIApp.LocalJobs.Features.Posting.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Resumesuccess : ContentPage
	{
        IUserDialogs dialog = UserDialogs.Instance;
        public Resumesuccess (string agentid)
		{
			InitializeComponent ();
            Getpaymentinfo(agentid);
        }

        public async void Getpaymentinfo(string agentid)
        {
           try
            {
                dialog.ShowLoading("");
                var getpayment = RestService.For<IJobPayment>(Commonsettings.LocaljobsAPI);
                var paymentinfo = await getpayment.getresumepaymentinfo(agentid);
                if (paymentinfo != null)
                {

                    lblorderid.Text = paymentinfo.ROW_DATA[0].orderid;
                    lbladdate.Text = paymentinfo.ROW_DATA[0].posteddate;
                    adenddate.Text = paymentinfo.ROW_DATA[0].enddate;
                    lblamnt.Text = "$ " + paymentinfo.ROW_DATA[0].amount.Replace(".0","").Replace(".00", "");
                    lblresumes.Text = paymentinfo.ROW_DATA[0].resumescount;
                    if (!string.IsNullOrEmpty(paymentinfo.ROW_DATA[0].adscount))
                    {
                        totaladslayout.IsVisible = true;
                    }
                    else
                    {
                        totaladslayout.IsVisible = false;
                    }
                    lblads.Text = paymentinfo.ROW_DATA[0].adscount;
                    dialog.HideLoading();
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            dialog.ShowLoading("");
            Navigation.PopToRootAsync();
            dialog.HideLoading();
        }
        protected override bool OnBackButtonPressed()
        {
            Navigation.PopToRootAsync();
            return true;
        }
    }
}