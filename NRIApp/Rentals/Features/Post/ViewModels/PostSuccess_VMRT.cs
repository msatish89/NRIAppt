using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Acr.UserDialogs;
using Refit;
using NRIApp.Helpers;
using NRIApp.Rentals.Features.Post.Interface;

namespace NRIApp.Rentals.Features.Post.ViewModels
{
    public class PostSuccess_VMRT : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        IUserDialogs dialog = UserDialogs.Instance;
        public static string Viewadlinkurl = "", cityurl = "", radid = "";

        public Command PostClose { get; set; }
        public Command Viewad { get; set; }

        private string _Orderid;
        public string Orderid
        {
            get { return _Orderid; }
            set { _Orderid = value; OnPropertyChanged(nameof(Orderid)); }
        }
        private string _Adpostdate;
        public string Adpostdate
        {
            get { return _Adpostdate; }
            set { _Adpostdate = value; OnPropertyChanged(nameof(Adpostdate)); }
        }
        private string _Adexpirydate;
        public string Adexpirydate
        {
            get { return _Adexpirydate; }
            set { _Adexpirydate = value; OnPropertyChanged(nameof(Adexpirydate)); }
        }
        private string _Packagetype;
        public string Packagetype
        {
            get { return _Packagetype; }
            set { _Packagetype = value; OnPropertyChanged(nameof(Packagetype)); }
        }
        private string _Totalamount;
        public string Totalamount
        {
            get { return _Totalamount; }
            set { _Totalamount = value; OnPropertyChanged(nameof(Totalamount)); }
        }
        private string _adtext;
        public string adtext
        {
            get { return _adtext; }
            set { _adtext = value;OnPropertyChanged(nameof(adtext)); }
        }
        private string _successtext;
        public string successtext
        {
            get { return _successtext; }
            set { _successtext = value; OnPropertyChanged(nameof(successtext)); }
        }
        private bool _hidevisible = true;
        public bool hidevisible
        {
            get { return _hidevisible; }
            set { _hidevisible = value; OnPropertyChanged(nameof(hidevisible)); }
        }
        public PostSuccess_VMRT(string adid,string usertype,string packagetype)
        {
            PostClose = new Command(ClickPostClose);
            Viewad = new Command(viewad);
            GetPaymentdata(adid,usertype,packagetype);
        }
        public async void GetPaymentdata(string adid,string usertype,string packagetype)
        {
            try
            {
                dialog.ShowLoading("");
                var getpayment = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                var paymentinfo = await getpayment.getpaymentinfo(adid,usertype,packagetype);
                if (paymentinfo != null)
                {
                    Packagetype = paymentinfo.ROW_DATA[0].packagetype;
                    
                    if(Packagetype=="Free" )
                    {
                        hidevisible = false;
                        Adpostdate = paymentinfo.ROW_DATA[0].adpostdate;
                        Adexpirydate = paymentinfo.ROW_DATA[0].adexpirydate;
                        Viewadlinkurl = paymentinfo.ROW_DATA[0].linkurl;
                        cityurl = paymentinfo.ROW_DATA[0].newcityurl;
                        radid = paymentinfo.ROW_DATA[0].adid;
                    }
                    else
                    {
                        if (usertype == "2" && paymentinfo.ROW_DATA[0].orderid == "0")
                        {
                            hidevisible = false;
                            Adpostdate = paymentinfo.ROW_DATA[0].adpostdate;
                            Adexpirydate = paymentinfo.ROW_DATA[0].adexpirydate;
                            Viewadlinkurl = paymentinfo.ROW_DATA[0].linkurl;
                            cityurl = paymentinfo.ROW_DATA[0].newcityurl;
                            radid = paymentinfo.ROW_DATA[0].adid;
                        }
                        else
                        {
                            hidevisible = true;
                            Orderid = paymentinfo.ROW_DATA[0].orderid;
                            Adpostdate = paymentinfo.ROW_DATA[0].adpostdate;
                            Adexpirydate = paymentinfo.ROW_DATA[0].adexpirydate;
                            Totalamount = "$ " + Payment_VMRT.totalamount.Replace("$", "");
                            Viewadlinkurl = paymentinfo.ROW_DATA[0].linkurl;
                            cityurl = paymentinfo.ROW_DATA[0].newcityurl;
                            radid = paymentinfo.ROW_DATA[0].adid;
                        }
                            
                    }
                    successtext = "You have successfully posted " + Packagetype + " ad on Sulekha.com";
                    adtext = "A copy of the receipt has been mailed to your email id " + Commonsettings.UserEmail + ". You may see your Ad by clicking";
                }
                dialog.HideLoading();
            }
            catch(Exception ex)
            {

            }
        }
        public async void ClickPostClose()
        {
            var curpage = GetCurrentPage();
            await curpage.Navigation.PopToRootAsync();
        }
        public void viewad()
        {
            if(!string.IsNullOrEmpty(Viewadlinkurl))
            {
                var curpage = GetCurrentPage();
                curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.List.Views.DetailRentalList(radid, cityurl));
                // Device.OpenUri(new Uri(viewadlinkurl));
              
            }
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page GetCurrentPage()
        {
            var currentpage = App.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}
