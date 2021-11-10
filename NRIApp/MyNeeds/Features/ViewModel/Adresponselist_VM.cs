using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.MyNeeds.Features.Models;
using Plugin.Messaging;
using Refit;
using Xamarin.Forms;

namespace NRIApp.MyNeeds.Features.ViewModel
{
    public class Adresponselist_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;
        public Command<Ad_Response_Data> postresponsecmd { get; set; }
        public Command<Ad_Response_Data> mobilenocmd { get; set; }
        public Command<Ad_Response_Data> mailcmd { get; set; }
        public Command Taptodownload { get; set; }
        public List<Ad_Response_Data> Reviewreply { get; set; }
        private bool _IsBusy = false;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { _IsBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }
        private bool _nointernet = false;
        public bool nointernet
        {
            get { return _nointernet; }
            set { _nointernet = value; OnPropertyChanged(nameof(nointernet)); }
        }
        private bool _postedago = false;
        public bool postedago
        {
            get { return _postedago; }
            set { _postedago = value; OnPropertyChanged(nameof(postedago)); }
        }
        private string _jobresumeid;
        public string jobresumeid
        {
            get { return _jobresumeid; }
            set { _jobresumeid = value; OnPropertyChanged(nameof(jobresumeid)); }
        }

        public Adresponselist_VM(MyNeedData addata)
        {
            try
            {
                getadresponselist(addata.adid,addata.titleurl,addata.services);
                mailcmd = new Command<Ad_Response_Data>(sendmail);
                mobilenocmd = new Command<Ad_Response_Data>(Makeusercall);
                postresponsecmd = new Command<Ad_Response_Data>(postresponse);
                Taptodownload = new Command(downloadresume);
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public void Makeusercall(Ad_Response_Data data)
        {
            var phonecall = CrossMessaging.Current.PhoneDialer;
            if (phonecall.CanMakePhoneCall)
                phonecall.MakePhoneCall(data.Mobileno, null);
        }
       public void sendmail(Ad_Response_Data data)
        {
            Device.OpenUri(new Uri("mailto:" + data.Email ));
        }

        public void postresponse(Ad_Response_Data pstresponse)
        {
            var currentpage = GetCurrentPage();
            currentpage.Navigation.PushAsync(new NRIApp.MyNeeds.Features.Views.Replytoresponse(pstresponse));
        }
        public async void getadresponselist(string adid,string titleurl,string service)
        {
            try
            {
                dialog.ShowLoading("", null);
                var Adresponse = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                var AdresponseData = await Adresponse.getadresponsedetails(adid,titleurl,service,Commonsettings.UserPid);
                //if the ad is in disable state there is no results.
                if (AdresponseData!=null)
                {
                    foreach (var item in AdresponseData.RowData)
                    {
                        if (!string.IsNullOrEmpty(item.Contributor))
                        {
                            item.replytext = item.Contributor.Substring(0, 1).ToUpper();
                        }
                        if(item.Ismobileverified=="1")
                        {
                            item.phoneimage = "phoneverified.png";
                            item.Mobileno = item.Mobileno + " (Phone Verified)";
                        }
                        else
                        {
                            item.phoneimage = "phoneNoVerified.png";
                            item.Mobileno = item.Mobileno + " (Phone not verified)";
                        }
                        item.Shortdesc = item.Shortdesc.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
                        if(item.isresume=="1")
                        {
                            item.isresumevisible = true;
                            jobresumeid = item.Contentid;
                        }
                        else
                        {
                            item.isresumevisible = false;
                        }
                    }
                    OnPropertyChanged(nameof(Reviewreply));
                    Reviewreply = AdresponseData.RowData;
                    var CurrentPage = GetCurrentPage();
                    var listreviewreply = CurrentPage.FindByName<ListView>("responseListview");
                    listreviewreply.ItemsSource = null;
                    listreviewreply.ItemsSource = Reviewreply;
                }
                else
                {
                    dialog.Toast("Please enable the ad to see the details");
                }
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void downloadresume()
        {
            try
            {
                
                string ipaddress = DependencyService.Get<Techjobs.Features.LeadForm.Interfaces.IIPAddressManager>().GetIPAddress();
                //https://techjobs.sulekha.com/Mobileapp/localjobs
                var resumedownloadapi = RestService.For<LocalJobs.Features.Jobseeker.Interface.IJobseeker>("http://localjobs.sulekha.com/mobileappresume");
                var downloaddata = await resumedownloadapi.downloadresumeurl(Commonsettings.UserPid, Commonsettings.UserMobileno, Commonsettings.UserEmail, jobresumeid, ipaddress);
                if (!string.IsNullOrEmpty(downloaddata.resumepath))
                {
                    // Device.OpenUri(new Uri(downloaddata.resumepath));
                    Device.OpenUri(new Uri(downloaddata.resumepath));
                }
                else
                {
                    dialog.Toast("Couldn't download...try again later...");
                }
            }
            catch (Exception ex)
            {

            }
        }
        public Page GetCurrentPage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
