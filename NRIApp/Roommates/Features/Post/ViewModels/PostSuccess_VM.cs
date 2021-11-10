using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using Refit;
using NRIApp.Helpers;
using NRIApp.Roommates.Features.Post.Interface;
using Acr.UserDialogs;

namespace NRIApp.Roommates.Features.Post.ViewModels
{
    public class PostSuccess_VM:INotifyPropertyChanged
    {
        IUserDialogs dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public static string viewadlinkurl = "",cityurl="",radid="";
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
            set { _Adpostdate = value;OnPropertyChanged(nameof(Adpostdate)); }
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
        private string _adtext = "";
        public string adtext
        {
            get { return _adtext; }
            set { _adtext = value;OnPropertyChanged(nameof(adtext)); }
        }
        private string _successtext;
        public string successtext
        {
            get { return _successtext; }
            set { _successtext = value;OnPropertyChanged(nameof(successtext)); }
        }
        private bool _hidevisible = true;
        public bool hidevisible
        {
            get { return _hidevisible; }
            set { _hidevisible = value;OnPropertyChanged(nameof(hidevisible)); }
        }
        public PostSuccess_VM(string adid,string usertype,string packagetype)
        {
            GetPaymentdata(adid,usertype,packagetype);
            PostClose = new Command(ClickPostClose);
            Viewad = new Command(viewad);

        }
        public void ClickPostClose()
        {
            dialog.ShowLoading("");
            var curpage = GetCurrentPage();
            curpage.Navigation.PopToRootAsync();
            dialog.HideLoading();
        }
        public void viewad()
        {
            if(!string.IsNullOrEmpty(viewadlinkurl))
            {
                var curpage = GetCurrentPage();
                curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.List.Views.DetailRMList(radid, cityurl));
               // Device.OpenUri(new Uri(viewadlinkurl));
            }
        }
        public async void GetPaymentdata(string adid,string usertype,string packagetype)
        {
            try
            {
                radid = adid;
                dialog.ShowLoading("");
                var getpayment = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                var paymentinfo = await getpayment.getpaymentinfo(adid,usertype,packagetype);

                if (paymentinfo != null)
                {
                    Packagetype = paymentinfo.ROW_DATA[0].packagetype;
                    if (Packagetype == "Free" )
                    {
                        hidevisible = false;
                        Adpostdate = paymentinfo.ROW_DATA[0].adpostdate;
                        Adexpirydate = paymentinfo.ROW_DATA[0].adexpirydate;
                        viewadlinkurl = paymentinfo.ROW_DATA[0].linkurl;
                        cityurl = paymentinfo.ROW_DATA[0].newcityurl;
                        radid = paymentinfo.ROW_DATA[0].adid;

                    }
                    else
                    {
                        if(usertype == "2" && paymentinfo.ROW_DATA[0].orderid=="0")
                        {
                            hidevisible = false;
                            Adpostdate = paymentinfo.ROW_DATA[0].adpostdate;
                            Adexpirydate = paymentinfo.ROW_DATA[0].adexpirydate;
                            viewadlinkurl = paymentinfo.ROW_DATA[0].linkurl;
                            cityurl = paymentinfo.ROW_DATA[0].newcityurl;
                            radid = paymentinfo.ROW_DATA[0].adid;
                        }
                        else
                        {
                            hidevisible = true;
                            Orderid = paymentinfo.ROW_DATA[0].orderid;
                            Adpostdate = paymentinfo.ROW_DATA[0].adpostdate;
                            Adexpirydate = paymentinfo.ROW_DATA[0].adexpirydate;
                            Totalamount = "$ " + Payment_VM.totalamount.Replace("$", "");
                            cityurl = paymentinfo.ROW_DATA[0].newcityurl;
                            viewadlinkurl = paymentinfo.ROW_DATA[0].linkurl;
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
                dialog.HideLoading();
            }
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName=null)
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
