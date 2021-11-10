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
using Acr.UserDialogs;
using NRIApp.LocalJobs.Features.Posting.Interfaces;

namespace NRIApp.LocalJobs.Features.Posting.ViewModels
{
    class Jobsuccess_VM : INotifyPropertyChanged
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
        public Jobsuccess_VM(string adid, string usertype)
        {
            GetPaymentdata(adid, usertype);
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
            if (!string.IsNullOrEmpty(viewadlinkurl))
            {
                var curpage = GetCurrentPage();
                curpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Detail.Views.JobDetails(radid, viewadlinkurl,Commonsettings.UserPid));
                // Device.OpenUri(new Uri(viewadlinkurl));
            }
        }
        public async void GetPaymentdata(string adid, string usertype)
        {
            try
            {
               // radid = adid;
                dialog.ShowLoading("");
                NRIApp.LocalJobs.Features.Posting.Views.JobSuccess.viewadcode = 1;
                var getpayment = RestService.For<IJobPayment>(Commonsettings.LocaljobsAPI);
                var paymentinfo = await getpayment.getpaymentinfo(adid, usertype); 

                if (paymentinfo != null)
                {
                    if(paymentinfo.ROW_DATA.Last().orderid=="0"|| string.IsNullOrEmpty(paymentinfo.ROW_DATA.Last().orderid))
                    {
                        hidevisible = false;
                    }
                    else
                    {
                        hidevisible = true;
                    }
                    Packagetype = paymentinfo.ROW_DATA[0].ordertype;
                            Orderid = paymentinfo.ROW_DATA[0].orderid;
                            Adpostdate = paymentinfo.ROW_DATA[0].startdate;
                            Adexpirydate = paymentinfo.ROW_DATA[0].enddate;
                            
                           // cityurl = paymentinfo.ROW_DATA[0].newcityurl;
                            viewadlinkurl = paymentinfo.ROW_DATA[0].titleurl;
                            radid = paymentinfo.ROW_DATA[0].adsid;

                    if(string.IsNullOrEmpty(viewadlinkurl))
                    {
                        viewadlinkurl =paymentinfo.ROW_DATA[0].clickurl;
                    }
                    if (!string.IsNullOrEmpty(paymentinfo.ROW_DATA[0].amount))
                    { Totalamount = "$ " + paymentinfo.ROW_DATA[0].amount.Replace(".00", "").Replace(".0", ""); }
                    successtext = "You have successfully posted " + Packagetype + " ad on Sulekha.com";
                    adtext = "A copy of the receipt has been mailed to your email id " + Commonsettings.UserEmail + ". You may see your Ad by clicking";
                }

                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
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

