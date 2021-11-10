using NRIApp.Helpers;
using NRIApp.MyNeeds.Features.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;

namespace NRIApp.MyNeeds.Features.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Adreceipt : ContentPage
    {
        IUserDialogs dialog = UserDialogs.Instance;
        public Adreceipt(MyNeedData addata)
        {
            InitializeComponent();
            this.Title = "View Receipt";
            getreceipt(addata);
        }
        public async void getreceipt(MyNeedData addata)
        {
            try
            {
                dialog.ShowLoading("", null);
                var receipt = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                if(addata.services=="Day Care" || addata.services == "daycare")
                {
                    addata.oldclpostid = addata.adid;
                }
                var receiptData = await receipt.getreceipt(addata.oldclpostid,addata.titleurl,addata.services);
                //adposteddate,typeofad,verified date,ad expireson,orderid,amount paid
                if(receiptData!=null && receiptData.RowData.Count!=0)
                {
                    if (!string.IsNullOrEmpty(receiptData.RowData[0].Amount))
                    {
                        amountlayout.IsVisible = true;
                        amount.Text = receiptData.RowData[0].Amount.Replace(".0", "") + " USD";
                    }
                    else
                    {
                        amountlayout.IsVisible = false;
                    }
                    verifieddate.Text = receiptData.RowData[0].Verifieddate;
                    package.Text = receiptData.RowData[0].Ordertype;
                    if (!string.IsNullOrEmpty(receiptData.RowData[0].Orderid) && receiptData.RowData[0].Orderid != "0")
                    {
                        orderidlayout.IsVisible = true;
                        orderid.Text = receiptData.RowData[0].Orderid;
                    }
                    else
                    {
                        orderidlayout.IsVisible = false;
                    }

                    expirydate.Text = receiptData.RowData[0].Enddate;
                    addate.Text = receiptData.RowData[0].Startdate;
                }
                else
                {
                    dialog.Toast("Please enable/renew ad to view receipt...");
                }
               
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
    }
}