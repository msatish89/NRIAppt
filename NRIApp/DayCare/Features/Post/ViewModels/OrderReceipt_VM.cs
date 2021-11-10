using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Linq;
using Acr.UserDialogs;
using Refit;
using NRIApp.DayCare.Features.Post.Interface;
using NRIApp.Helpers;

namespace NRIApp.DayCare.Features.Post.ViewModels
{
    public class OrderReceipt_VM : INotifyPropertyChanged
    {
        IUserDialogs dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public static string viewadlinkurl = "", cityurl = "", radid = "";
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
        private string _adtext = "";
        public string adtext
        {
            get { return _adtext; }
            set { _adtext = value; OnPropertyChanged(nameof(adtext)); }
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
        public OrderReceipt_VM(string businessid,string noofdays, string pkgname)
        {
            getreceiptdtls(businessid,noofdays,pkgname);
            PostClose = new Command(ClickPostClose);
            Viewad = new Command(viewad);
        }
        public void ClickPostClose()
        {
            dialog.ShowLoading("");
            var curpage = Getcurrentpage();
            curpage.Navigation.PopToRootAsync();
            dialog.HideLoading();
        }
        public void viewad()
        {
          try
            {
                if (!string.IsNullOrEmpty(viewadlinkurl))
                {
                    var curpage = Getcurrentpage();
                    curpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Detail.Views.CareDetail(radid, viewadlinkurl, Commonsettings.UserPid));
                    //Device.OpenUri(new Uri("http://daycare.sulekha.com/" + viewadlinkurl));
                }
            }
            catch(Exception ex)
            {
            }
        }
        public async void getreceiptdtls(string businessid, string noofdays, string pkgname)
        {
            try
            {
                dialog.ShowLoading("", null);
                if(string.IsNullOrEmpty(noofdays))
                {
                    noofdays = "";
                }
                var DCreceiptAPI = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                //  var postresponse = await DCpostAPI.postsecondsubmit(post);
                 var paymentinfo = await DCreceiptAPI.getorderdetails(businessid,noofdays);
               // var paymentinfo = await DCreceiptAPI.getorderdetails("254651","60");
                if (paymentinfo != null)
                {
                    if (paymentinfo.ROW_DATA.Last().Orderid == "0" || string.IsNullOrEmpty(paymentinfo.ROW_DATA.Last().Orderid) || pkgname=="free")
                    {
                        hidevisible = false;
                    }
                    else
                    {
                        hidevisible = true;
                    }
                    Packagetype = paymentinfo.ROW_DATA[0].Ordertype;
                    Orderid = paymentinfo.ROW_DATA[0].Orderid;
                    Adpostdate = paymentinfo.ROW_DATA[0].Startdate;
                    Adexpirydate = paymentinfo.ROW_DATA[0].Enddate;
                   // Totalamount = "$ " + paymentinfo.ROW_DATA[0].Amount; //total amt 
                    Totalamount = "$ " + paymentinfo.ROW_DATA[0].Paidamount; //total amount plus addons
                    // cityurl = paymentinfo.ROW_DATA[0].newcityurl;
                    viewadlinkurl = paymentinfo.ROW_DATA[0].newlinkurl;
                    radid = paymentinfo.ROW_DATA[0].Businessid;

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
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page Getcurrentpage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}
