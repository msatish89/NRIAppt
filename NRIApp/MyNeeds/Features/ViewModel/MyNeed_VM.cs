using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;
using Acr.UserDialogs;
using Refit;
using NRIApp.Helpers;
using NRIApp.MyNeeds.Features.Models;
using System.Threading.Tasks;
using NRIApp.Rentals.Features.Post.Views;
using XLabs.Forms.Controls;
using Newtonsoft.Json;
using System.Xml;
using System.Windows.Input;

namespace NRIApp.MyNeeds.Features.ViewModel
{
    public class MyNeed_VM : INotifyPropertyChanged
    {
        IUserDialogs dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;

        public List<MyNeedData> myneedLSlistdata { get; set; }
        public List<MyNeedData> myneedRMlistdata { get; set; }
        public List<MyNeedData> myneedRTlistdata { get; set; }
        public List<MyNeedData> myneedDClistdata { get; set; }
        public List<MyNeedData> myneedJobslistdata { get; set; }

        public int LSimgnum = 0;
        public int Roommatesimgnum = 0;
        public int Rentalsimgnum = 0;
        public int Daycareimgnum = 0;
        public int Jobsimgnum = 0;
        // public int LSAdimgnum = 0;
        public int RoommatesAdimgnum = 0;
        public int RentalsAdimgnum = 0;
        public Command LS_Tap { get; set; }
        public Command RM_Tap { get; set; }
        public Command RT_Tap { get; set; }
        public Command DC_Tap { get; set; }
        public Command Jobs_Tap { get; set; }

        public Command<MyNeedData> RenewPaymentAdTap { get; set; }
        public List<MyNeedpartialData> myneedLSpartiallistdata { get; set; }
        public Command<MyNeedData> ExpandLSAdDetails { get; set; }
        public Command<MyNeedpartialData> ExpandLSPartialAdDetails { get; set; }
        public Command<MyNeedData> ExpandRMAdDetails { get; set; }
        public Command<MyNeedData> ExpandRTAdDetails { get; set; }
        public Command<MyNeedData> ExpandDCAdDetails { get; set; }
        public Command<MyNeedData> ExpandJobsAdDetails { get; set; }
        public Command<MyNeedData> EnableAdTap { get; set; }
        public Command<MyNeedData> DisableAdTap { get; set; }
        public Command<MyNeedData> EditAdTap { get; set; }
        public Command<MyNeedData> EditAdTapDC { get; set; }
        public Command<MyNeedData> EditjobAdTap { get; set; }
        public Command<MyNeedData> completeDCpayment { get; set; }
        public Command<MyNeedData> RenewAdTapDC { get; set; }
        public Command<MyNeedData> RenewAdTap { get; set; }
        public Command<MyNeedData> RenewjobAdTap { get; set; }
        public Command<MyNeedData> UpgradeAdTap { get; set; }
        public Command<MyNeedData> CPaymentTap { get; set; }
        public Command<MyNeedData> jobCPaymentTap { get; set; }
        public Command<MyNeedData> ResponseAdTap { get; set; }
        public Command<MyNeedData> ViewreceiptAdTap { get; set; }
        public Command<MyNeedData> SendreceiptAdTap { get; set; }
        public Command<MyNeedData> SendmailverifyTap { get; set; }
        public Command<MyNeedData> ViewAdTap { get; set; }

        public Command<MyNeedpartialData> PartialadremoveTap { get; set; }
        public Command<MyNeedpartialData> PartialPaymentTap { get; set; }


        private string _LSimg;
        public string LSimg
        {
            get { return _LSimg; }
            set { _LSimg = value; OnPropertyChanged(nameof(LSimg)); }
        }
        private string _LSAdimg;
        public string LSAdimg
        {
            get { return _LSAdimg; }
            set { _LSAdimg = value;OnPropertyChanged(nameof(LSAdimg)); }
        }
        private string _RMAdimg;
        public string RMAdimg
        {
            get { return _RMAdimg; }
            set { _RMAdimg = value; OnPropertyChanged(nameof(RMAdimg)); }
        }
        private string _RTAdimg;
        public string RTAdimg
        {
            get { return _RTAdimg; }
            set { _RTAdimg = value; OnPropertyChanged(nameof(RTAdimg)); }
        }
        private string _DCAdimg;
        public string DCAdimg
        {
            get { return _DCAdimg; }
            set { _DCAdimg = value; OnPropertyChanged(nameof(DCAdimg)); }
        }

        private string _RMimg;
        public string RMimg
        {
            get { return _RMimg; }
            set { _RMimg = value; OnPropertyChanged(nameof(RMimg)); }
        }
        private string _RTimg;
        public string RTimg
        {
            get { return _RTimg; }
            set { _RTimg = value; OnPropertyChanged(nameof(RTimg)); }
        }
        private string _DCimg;
        public string DCimg
        {
            get { return _DCimg; }
            set { _DCimg = value; OnPropertyChanged(nameof(DCimg)); }
        }
        private string _Jobsimg;
        public string Jobsimg
        {
            get { return _Jobsimg; }
            set { _Jobsimg = value; OnPropertyChanged(nameof(Jobsimg)); }
        }
        private bool _LSblkvisible = false;
        public bool LSblkvisible
        {
            get { return _LSblkvisible; }
            set { _LSblkvisible = value; OnPropertyChanged(nameof(LSblkvisible)); }
        }
        private bool _RMblkvisible = false;
        public bool RMblkvisible
        {
            get { return _RMblkvisible; }
            set { _RMblkvisible = value; OnPropertyChanged(nameof(RMblkvisible)); }
        }
        private bool _Rentalsblkvisible = false;
        public bool Rentalsblkvisible
        {
            get { return _Rentalsblkvisible; }
            set { _Rentalsblkvisible = value; OnPropertyChanged(nameof(Rentalsblkvisible)); }
        }

        private bool _Daycareblkvisible = false;
        public bool Daycareblkvisible
        {
            get { return _Daycareblkvisible; }
            set { _Daycareblkvisible = value; OnPropertyChanged(nameof(Daycareblkvisible)); }
        }

        private bool _Jobsblkvisible = false;
        public bool Jobsblkvisible
        {
            get { return _Jobsblkvisible; }
            set { _Jobsblkvisible = value; OnPropertyChanged(nameof(Jobsblkvisible)); }
        }
        private bool _LSNeedData = false;
        public bool LSNeedData
        {
            get { return _LSNeedData; }
            set { _LSNeedData = value;OnPropertyChanged(nameof(LSNeedData)); }
        }
        private bool _NoAdsLS = false;
        public bool NoAdsLS
        {
            get { return _NoAdsLS; }
            set { _NoAdsLS = value;OnPropertyChanged(nameof(NoAdsLS)); }
        }
        private bool _NoAdsRM = false;
        public bool NoAdsRM
        {
            get { return _NoAdsRM; }
            set { _NoAdsRM = value; OnPropertyChanged(nameof(NoAdsRM)); }
        }
        private bool _NoAdsRT = false;
        public bool NoAdsRT
        {
            get { return _NoAdsRT; }
            set { _NoAdsRT = value; OnPropertyChanged(nameof(NoAdsRT)); }
        }

        private bool _NoAdsDC = false;
        public bool NoAdsDC
        {
            get { return _NoAdsDC; }
            set { _NoAdsDC = value; OnPropertyChanged(nameof(NoAdsDC)); }
        }
        private bool _NoAdsJobs = false;
        public bool NoAdsJobs
        {
            get { return _NoAdsJobs; }
            set { _NoAdsJobs = value; OnPropertyChanged(nameof(NoAdsJobs)); }
        }
        private bool _LSblkpartialadvisible = false;
        public bool LSblkpartialadvisible
        {
            get { return _LSblkpartialadvisible; }
            set { _LSblkpartialadvisible = value; OnPropertyChanged(nameof(LSblkpartialadvisible)); }
        }
        public MyNeed_VM()
        {
            if(!string.IsNullOrEmpty(Commonsettings.UserPid))
            {
                LSimg = "downarrowGrey.png";
                RMimg = "downarrowGrey.png";
                RTimg = "downarrowGrey.png";
                DCimg = "downarrowGrey.png";
                Jobsimg = "downarrowGrey.png";
                LSAdimg = "PlusGrey.png";
                RMAdimg = "downarrowGrey.png";
                RTAdimg = "downarrowGrey.png";
                DCAdimg = "downarrowGrey.png";

                RM_Tap = new Command(ExpandRM_Needs);
                RT_Tap = new Command(ExpandRT_Needs);
                LS_Tap = new Command(ExpandLS_Needs);
                DC_Tap = new Command(ExpandDC_Needs);
                Jobs_Tap = new Command(ExpandJobs_Needs);

                ExpandLSAdDetails = new Command<MyNeedData>(Lsaddetails);
                ExpandLSPartialAdDetails = new Command<MyNeedpartialData>(Lspartialaddetails);
                ExpandRMAdDetails = new Command<MyNeedData>(RMadDetails);
                //ExpandRMAdDetails = new Command<MyNeedData>((dt) =>
                //{
                //    //Device.BeginInvokeOnMainThread(() => { 
                //    //    dialog.ShowLoading("", null);
                //    //   // await Task.Delay(200);

                //    //     });
                //    RMadDetails(dt);
                //    //dialog.HideLoading();
                //});
                ExpandRTAdDetails = new Command<MyNeedData>(RTadDetails);
                ExpandDCAdDetails = new Command<MyNeedData>(DCadDetails);
                ExpandJobsAdDetails = new Command<MyNeedData>(JobsadDetails);

                EnableAdTap = new Command<MyNeedData>(Enablead);
                DisableAdTap = new Command<MyNeedData>(DisableAd);
                EditAdTap = new Command<MyNeedData>(EditAd);
                EditjobAdTap = new Command<MyNeedData>(editjobs);
                EditAdTapDC = new Command<MyNeedData>(editDC);
                completeDCpayment = new Command<MyNeedData>(completeDC);
                RenewAdTapDC = new Command<MyNeedData>(renewDC);
                RenewAdTap = new Command<MyNeedData>(completepayment);
                RenewPaymentAdTap = new Command<MyNeedData>(RenewAdLS);
                RenewjobAdTap = new Command<MyNeedData>(RenewAd);
                UpgradeAdTap = new Command<MyNeedData>(UpgradeAd);
                CPaymentTap = new Command<MyNeedData>(completepayment);
                jobCPaymentTap = new Command<MyNeedData>(jobcompletepayment);
                ResponseAdTap = new Command<MyNeedData>(sendresponsepage);
                PartialadremoveTap = new Command<MyNeedpartialData>(Removepartialad);
                PartialPaymentTap = new Command<MyNeedpartialData>(Selectpartialadpayment);
                ViewreceiptAdTap = new Command<MyNeedData>(viewreceiptpage);
                SendreceiptAdTap = new Command<MyNeedData>(sendreceipt);
                SendmailverifyTap = new Command<MyNeedData>(sendmailverify);
                ViewAdTap = new Command<MyNeedData>(ViewAd);
                

                getlocalserviceaddetails("1");
                getroommatesaddetails("1");
                getrentalsaddetails("1");
                getjobsdetails("1");
                getdaycareaddetails("1");
            }
            else
            {
                dialog.Toast("Please Login and try again");
            }
        }
        public void Lspartialaddetails(MyNeedpartialData data)
        {
            foreach (var lst in myneedLSpartiallistdata)
            {
                if (lst.adid == data.adid)
                {
                    if (lst.LSAdimg == "PlusGrey.png")
                    {
                        lst.LSAdimg = "minusGrey.png";
                        lst.LSNeedData = true;
                        isexpanddatavisible = 1;
                    }
                    else
                    {
                        lst.LSAdimg = "PlusGrey.png";
                        lst.LSNeedData = false;
                        isexpanddatavisible = 0;
                    }
                }
                else
                {
                    lst.LSAdimg = "PlusGrey.png";
                    lst.LSNeedData = false;
                }
            }

            var currentpage = GetCurrentPage();
            HVScrollGridView LSlistdata = currentpage.FindByName<HVScrollGridView>("myneedLSpartiallist");
            LSlistdata.ItemsSource = null;
            LSlistdata.ItemsSource = myneedLSpartiallistdata;
            OnPropertyChanged(nameof(myneedLSpartiallistdata));

        }
        public async void viewreceiptpage(MyNeedData addata)
        {
            try
            {
                if(addata.enablevisible==true && addata.services== "Day Care")
                {
                    dialog.Toast("Please enable ad to view receipt");
                }
                //else if(addata.renewvisible == true && addata.services == "Day Care")
                //{
                //    dialog.Toast("Please renew ad to view receipt");
                //}
                else
                {
                    var currentpage = GetCurrentPage();
                    await currentpage.Navigation.PushAsync(new MyNeeds.Features.Views.Adreceipt(addata));
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void sendreceipt(MyNeedData addata)
        {
            try
            {
                dialog.ShowLoading("", null);
                if(addata.services=="Jobs")
                {
                    addata.adid = addata.oldclpostid;
                }
                var sendmailreceiptAd = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                //string adid,  string userpid, string emailid, string service
                //var sendmailreceipData = await sendmailreceiptAd.sendreceipt(addata.adid, Commonsettings.UserPid, Commonsettings.UserEmail, addata.services, addata.titleurl);
                var sendmailreceipData = await sendmailreceiptAd.sendreceipt(addata.adid, Commonsettings.UserPid, "bhavatharanip@sulekha.net", addata.services, addata.titleurl);
                dialog.HideLoading();
                if (sendmailreceipData.resultinformation == "success")
                {
                    var alert = await dialog.ConfirmAsync("", "Receipt mail sent successfully.", "", "Ok");
                }
                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void sendmailverify(MyNeedData addata)
        {
            try
            {
                dialog.ShowLoading("", null);
                //var sendmailreceiptAd = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                //http://mycity.sulekha.com/postanadv2/postadcommon.aspx?sQustr=sendemailverification&emailid=prithvik1020@gmail.com&callback=fnemailverificationsuccessv2
                var sendmailreceiptAd = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>("http://mycity.sulekha.com");
                var sendmailreceipData = await sendmailreceiptAd.sendmailverifymail(Commonsettings.UserEmail,Commonsettings.UserName , Commonsettings.UserPid, Commonsettings.UserId);
                //var sendmailreceipData = await sendmailreceiptAd.sendmailverifymail("bhavatharanip@sulekha.net", "tharani", Commonsettings.UserPid, Commonsettings.UserId);
                dialog.HideLoading();
                if (sendmailreceipData.resultinformation == "success")
                {
                    var alert = await dialog.ConfirmAsync("Verification mail sent successfully.","Sulekha Alert!",  "", "Ok");
                }
                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
    
        public async void sendresponsepage(MyNeedData addata)
        {

           try
            {

                //Device.BeginInvokeOnMainThread(() =>
                //{
                //    //Xamarin.Forms.Device.OpenUri(new Uri("skype://555-1111"));
                //    Xamarin.Forms.Device.OpenUri(new Uri("app2business://122345"));
                //});
                //  await Browser.OpenAsync(url, BrowserLaunchMode.External);
                //await DependencyService.Get<IAppHandler>().LaunchApp("app2://path");
                //await DependencyService.Get<IAppHandler>().LaunchApp("https://play.google.com/store/apps/details?id=com.sulekha.nribusiness&hl=en");
               
                if (!string.IsNullOrEmpty(addata.responses) && addata.responses != "0")
                {
                    if (addata.adstatustxt == "Expired" && addata.upgradevisible == true)
                    {
                        dialog.Toast("Please upgrade the ad to see the details");
                    }
                    else if (addata.enablevisible == true || (addata.renewvisible == true && addata.disablevisible == false))
                    {
                        if(addata.enablevisible == true)
                        {
                            dialog.Toast("Please enable the ad to see the details");
                        }
                        else
                        {
                            dialog.Toast("Please renew the ad to see the details");
                        }
                            
                    }
                    else
                    {
                        var currentpage = GetCurrentPage();
                        if (addata.services == "Local Services")
                        {
                            if (addata.premiumad == "1")
                            {
                                string url = "";
                                if(Commonsettings.UserMobileOS=="android")
                                {
                                    url = "https://play.google.com/store/apps/details?id=com.sulekha.nribusiness&hl=en";
                                }
                                else
                                {

                                }
                                
                                Device.OpenUri(new Uri(url));
                            }
                            else
                            {
                                await currentpage.Navigation.PushAsync(new MyNeeds.Features.Views.LSresponselist(addata.oldclpostid,addata.adid));
                            }
                        }
                        else
                        {
                            await currentpage.Navigation.PushAsync(new MyNeeds.Features.Views.Adresponselist(addata));
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
        public async void Enablead(MyNeedData addata)
        {
            //Rentals Roommates  Local Services
          try
            {
                dialog.ShowLoading("");
                if (addata.services == "Roommates")
                {
                    var EnableAdRM = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                    var EnableAdRMData = await EnableAdRM.EnableRMAd(addata.adid, Commonsettings.UserPid, addata.contributor, addata.oldclpostid, "0");
                    if (EnableAdRMData.resultinformation == "inserted")
                    {
                        RMblkvisible = true;
                        getroommatesaddetails(addata.adid);
                    }
                }
                else if (addata.services == "Rentals")
                {
                    var EnableAdRT = RestService.For<Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                    var EnableAdRTData = await EnableAdRT.EnableRTAd(addata.adid, Commonsettings.UserPid, addata.contributor, addata.oldclpostid, "0");
                    if (EnableAdRTData.resultinformation == "inserted")
                    {
                        Rentalsblkvisible = true;
                        getrentalsaddetails(addata.adid);
                    }
                }
                else if (addata.services == "Local Services")
                {
                    var EnableAdLS = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                    var EnableAdLSData = await EnableAdLS.EnableLSAd(addata.adid, Commonsettings.UserPid, addata.contributor, addata.oldclpostid, "0");
                    if (EnableAdLSData.resultinformation == "inserted")
                    {
                        LSblkvisible = true;
                        getlocalserviceaddetails(addata.adid);
                    }
                }
                else if (addata.services == "Daycare" || addata.services== "Day Care")
                {
                    var EnableAdDC = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                    var EnableAdDCData = await EnableAdDC.EnableDCAd(addata.adid, Commonsettings.UserPid, Commonsettings.UserName , addata.contentid, "0");
                    if (EnableAdDCData.resultinformation == "inserted" || EnableAdDCData.resultinformation == "updated")
                    {
                        addata.premiumadreceiptvisible = true;
                        LSblkvisible = true;
                        getdaycareaddetails(addata.adid);
                    }
                }
                else if (addata.services == "Jobs")
                {
                    var EnableAdLS = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                    var EnableAdLSData = await EnableAdLS.EnableJobsAd(addata.adid, Commonsettings.UserPid, addata.contributor, addata.oldclpostid, "0");
                    if (EnableAdLSData.resultinformation == "inserted")
                    {
                        LSblkvisible = true;
                        getjobsdetails(addata.adid);
                    }
                }
                //dialog.HideLoading();
                dialog.Toast("Ad enabled successfully");
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void DisableAd(MyNeedData addata)
        {
            try
            {
                dialog.ShowLoading("");
                if (addata.services == "Roommates")
                {
                    var DisableAdRM = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                    var DisableAdRMData = await DisableAdRM.DisableRMAd(addata.adid, Commonsettings.UserPid, addata.contributor, addata.oldclpostid, "1");
                    if (DisableAdRMData.resultinformation == "updated")
                    {
                        getroommatesaddetails(addata.adid);
                        RMblkvisible = true;
                    }
                }
                else if (addata.services == "Rentals")
                {
                    var DisableAdRT = RestService.For<Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                    var DisableAdRTData = await DisableAdRT.DisableRTAd(addata.adid, Commonsettings.UserPid, addata.contributor, addata.oldclpostid, "1");
                    if (DisableAdRTData.resultinformation == "updated")
                    {
                        getrentalsaddetails(addata.adid);
                        Rentalsblkvisible = true;
                    }
                }
                else if (addata.services == "Local Services")
                {
                    var DisableAdLS = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                    var DisableAdLSData = await DisableAdLS.DisableLSAd(addata.adid, Commonsettings.UserPid, addata.contributor, addata.oldclpostid, "1");
                    if (DisableAdLSData.resultinformation == "updated")
                    {
                        getlocalserviceaddetails(addata.adid);
                        LSblkvisible = true;
                    }
                }
                else if (addata.services == "Daycare" || addata.services == "Day Care")
                {
                    var DisableAdDC = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                    var DisableAdDCData = await DisableAdDC.DisableDCAd(addata.adid, Commonsettings.UserPid, Commonsettings.UserName, addata.contentid, "1");
                    if (DisableAdDCData.resultinformation == "updated")
                    {
                        getdaycareaddetails(addata.adid);
                        Daycareblkvisible = true;
                    }
                }
                else if (addata.services == "Jobs")
                {
                    var DisableAdLS = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                    var DisableAdLSData = await DisableAdLS.DisableJobsAd(addata.adid, Commonsettings.UserPid, addata.contributor, addata.contentid, "1");
                    if (DisableAdLSData.resultinformation == "updated")
                    {
                        getjobsdetails(addata.adid);
                        Jobsblkvisible = true;
                    }
                }
                // dialog.HideLoading();
                dialog.Toast("Ad disabled successfully");
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        Roommates.Features.Post.Models.CategoryList catlist = new Roommates.Features.Post.Models.CategoryList();
        Rentals.Features.Post.Models.RTCategoryList rtcatlist = new Rentals.Features.Post.Models.RTCategoryList();
        //NRIApp.DayCare.Features.Post.Models.DC_CategoryList dccatlist = new DayCare.Features.Post.Models.DC_CategoryList();
        NRIApp.DayCare.Features.Post.Models.Daycareposting dccatlist = new DayCare.Features.Post.Models.Daycareposting();
    
        public async void EditAd(MyNeedData addata)
        {
            try
            {
                dialog.ShowLoading("",null);
                var currentpage = GetCurrentPage();
                var AdDetailsAPI = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                if(addata.services== "Day Care")
                {
                    addata.oldclpostid = addata.adid;
                }
                var AdDetails = await AdDetailsAPI.getadDetails(addata.oldclpostid,"1", Commonsettings.UserEmail,addata.services);
                if (addata.services == "Roommates")
                {
                    //if(AdDetails.Supercategoryid=="1")
                    catlist.pricemode = AdDetails.ROW_DATA[0].pricemode;
                    catlist.accomodate = AdDetails.ROW_DATA[0].accomodate;
                    catlist.stayperiod = AdDetails.ROW_DATA[0].stayperiod;
                    catlist.Rent = AdDetails.ROW_DATA[0].Rent;
                    catlist.attachedbaths = AdDetails.ROW_DATA[0].attachedbaths;
                    catlist.availablefrm = AdDetails.ROW_DATA[0].availablefrm;
                    catlist.service = AdDetails.ROW_DATA[0].Service;
                    catlist.adid = AdDetails.ROW_DATA[0].Adid;
                    //AdDetails.ROW_DATA[0].Adsid;
                    catlist.agentid = AdDetails.ROW_DATA[0].Agentid;
                    catlist.Isagent = AdDetails.ROW_DATA[0].Isagent;
                    catlist.XmlPhotopath = AdDetails.ROW_DATA[0].XmlPhotopath;
                    catlist.Imagepath = AdDetails.ROW_DATA[0].Imagepath;
                    //catlist.Imagepath = AdDetails.ROW_DATA[0].HtmlPhotopath;
                    catlist.Photoscnt = AdDetails.ROW_DATA[0].Photoscnt;
                    catlist.Ispaid = AdDetails.ROW_DATA[0].Ispaid;
                    catlist.Category = AdDetails.ROW_DATA[0].Category;
                    catlist.Subcategory = AdDetails.ROW_DATA[0].Subcategory;
                    if(!string.IsNullOrEmpty(AdDetails.ROW_DATA[0].Adtype))
                    {
                        if(AdDetails.ROW_DATA[0].Adtype=="offered")
                        {
                            catlist.adtype = "1";
                        }
                        else
                        {
                            catlist.adtype = "0";
                        }
                    }
                    //catlist.adtype = AdDetails.ROW_DATA[0].Adtype;
                    
                    catlist.Additionalcity = AdDetails.ROW_DATA[0].Additionalcity;
                    catlist.Citycount = AdDetails.ROW_DATA[0].Citycount;
                    //pst.me AdDetails.ROW_DATA[0].Metrototalamount;
                    catlist.Citytotalamount = AdDetails.ROW_DATA[0].Citytotalamount;
                    catlist.supercategoryid = AdDetails.ROW_DATA[0].Supercategoryid;
                    catlist.primarycategoryid = AdDetails.ROW_DATA[0].Primarycategoryid.ToString();
                    catlist.secondarycategoryid = AdDetails.ROW_DATA[0].Secondarycategoryid;
                    catlist.supercategoryvalue = AdDetails.ROW_DATA[0].Supercategoryvalue;
                    catlist.primarycategoryvalue = AdDetails.ROW_DATA[0].Primarycategoryvalue;
                    catlist.secondarycategoryvalue = AdDetails.ROW_DATA[0].Secondarycategoryvalue;
                    catlist.Tags = AdDetails.ROW_DATA[0].Tags;
                    catlist.businessname = AdDetails.ROW_DATA[0].Businessname;
                    catlist.licenseno = AdDetails.ROW_DATA[0].licenseno;
                    if(catlist.licenseno== "new id")
                    {
                        catlist.licenseno = "";
                    }
                    catlist.url = AdDetails.ROW_DATA[0].Url;
                    catlist.supercategoryid = Convert.ToInt32(AdDetails.ROW_DATA[0].Supertagid);
                    catlist.Bestindustry = AdDetails.ROW_DATA[0].Bestindustry;
                    catlist.Hideaddress = AdDetails.ROW_DATA[0].Hideaddress;
                    catlist.Linkedinurl = AdDetails.ROW_DATA[0].Linkedinurl;
                    catlist.Trimetros = AdDetails.ROW_DATA[0].Trimetros;
                    catlist.Nationwide = AdDetails.ROW_DATA[0].Nationwide;
                    catlist.Salarymode = AdDetails.ROW_DATA[0].Salarymode;
                    catlist.Salarymodeid = AdDetails.ROW_DATA[0].Salarymodeid;
                    catlist.Streetname = catlist.Locationname = AdDetails.ROW_DATA[0].Streetname;
                    catlist.Zipcode = AdDetails.ROW_DATA[0].Zipcode;
                    catlist.Lat = AdDetails.ROW_DATA[0].Lat;
                    catlist.Long = AdDetails.ROW_DATA[0].Long;
                    catlist.Newcityurl = AdDetails.ROW_DATA[0].Newcityurl;
                    catlist.City = AdDetails.ROW_DATA[0].City;
                    catlist.State =catlist.StateName= AdDetails.ROW_DATA[0].State;
                    catlist.Country = AdDetails.ROW_DATA[0].Country;
                    catlist.title = AdDetails.ROW_DATA[0].Title;
                    catlist.Titleurl = AdDetails.ROW_DATA[0].Titleurl;
                    catlist.description = AdDetails.ROW_DATA[0].Description;
                    catlist.Contributor = catlist.ContactName = AdDetails.ROW_DATA[0].Contributor;
                    catlist.Status = AdDetails.ROW_DATA[0].Status;
                    catlist.Displayphone = AdDetails.ROW_DATA[0].Displayphone;
                    catlist.Mailerstatus = AdDetails.ROW_DATA[0].Mailerstatus;
                    catlist.Userpid = AdDetails.ROW_DATA[0].Userpid;
                    catlist.Ordertype = AdDetails.ROW_DATA[0].Ordertype;
                    catlist.Customerid = AdDetails.ROW_DATA[0].Customerid;
                    catlist.Campaignid = AdDetails.ROW_DATA[0].Campaignid;
                    catlist.Postedby = AdDetails.ROW_DATA[0].Postedby;
                    catlist.Enddate = AdDetails.ROW_DATA[0].Enddate;
                    catlist.Email = catlist.ContactEmail = AdDetails.ROW_DATA[0].Email;
                    catlist.Phone = catlist.ContactPhone = AdDetails.ROW_DATA[0].Phone;
                    catlist.Startdate = AdDetails.ROW_DATA[0].Startdate;
                    catlist.Numberofdays = AdDetails.ROW_DATA[0].Numberofdays;
                    catlist.Amount = AdDetails.ROW_DATA[0].Amount;
                    catlist.Folderid = AdDetails.ROW_DATA[0].Folderid;
                    catlist.Countrycode = AdDetails.ROW_DATA[0].Countrycode;
                    catlist.Ismobileverified = AdDetails.ROW_DATA[0].Ismobileverified;
                    catlist.Statecode = AdDetails.ROW_DATA[0].Statecode;
                    catlist.Metro = AdDetails.ROW_DATA[0].Metro;
                    catlist.Metrourl = AdDetails.ROW_DATA[0].Metrourl;
                    catlist.Supercategoryvalueurl = AdDetails.ROW_DATA[0].Supercategoryvalueurl;
                    catlist.Primarycategoryvalueurl = AdDetails.ROW_DATA[0].Primarycategoryvalueurl;
                    catlist.Secondarycategoryvalueurl = AdDetails.ROW_DATA[0].Secondarycategoryvalueurl;
                    catlist.Orderid = AdDetails.ROW_DATA[0].Orderid;
                    catlist.Expirydate = AdDetails.ROW_DATA[0].Expirydate;
                    catlist.Noofadcnt = AdDetails.ROW_DATA[0].Noofadcnt;
                    catlist.Adpostedcnt = AdDetails.ROW_DATA[0].Adpostedcnt;
                    catlist.Responsemail = AdDetails.ROW_DATA[0].Responsemail;
                    catlist.Gender = AdDetails.ROW_DATA[0].Gender;
                    catlist.Disclosedbusiness = AdDetails.ROW_DATA[0].Disclosedbusiness;
                    catlist.Couponcode = AdDetails.ROW_DATA[0].Couponcode;
                    catlist.Salespersonemail = AdDetails.ROW_DATA[0].Salespersonemail;
                    catlist.Companytype = AdDetails.ROW_DATA[0].Companytype;
                    catlist.Bannerstatus = AdDetails.ROW_DATA[0].Bannerstatus;
                    catlist.Banneramount = AdDetails.ROW_DATA[0].Banneramount;
                    catlist.Categoryflag = AdDetails.ROW_DATA[0].Categoryflag;
                    catlist.Freeadsflag = AdDetails.ROW_DATA[0].Freeadsflag;
                    catlist.Islisting = AdDetails.ROW_DATA[0].Islisting;
                    catlist.Addonamount = AdDetails.ROW_DATA[0].Addonamount;
                    catlist.Emailblastamount = AdDetails.ROW_DATA[0].Emailblastamount;
                    catlist.Bonusdays = AdDetails.ROW_DATA[0].Bonusdays;
                    catlist.Bonusamount = AdDetails.ROW_DATA[0].Bonusamount;
                    catlist.Pendingpaycouponcode = AdDetails.ROW_DATA[0].Pendingpaycouponcode;

                    catlist.Benefits = AdDetails.ROW_DATA[0].Benefits;
                    catlist.ismyneed = "1";

                    await currentpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.offerwant(catlist));
                    //if (Commonsettings.CAdTypeID == 0)
                    //{
                    //    await currentpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.lcf(catlist));
                    //}
                    //else
                    //{
                    //    await currentpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.postfirst(catlist));
                    //}
                    //currentpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Catfirst());
                }
                else if (addata.services == "Rentals")
                {
                    rtcatlist.pricemode = AdDetails.ROW_DATA[0].pricemode;
                    rtcatlist.accomodate = AdDetails.ROW_DATA[0].accomodate;
                    rtcatlist.stayperiod = AdDetails.ROW_DATA[0].stayperiod;
                    rtcatlist.Rent = AdDetails.ROW_DATA[0].Rent;
                    rtcatlist.attachedbaths = AdDetails.ROW_DATA[0].attachedbaths;
                    rtcatlist.availablefrm = AdDetails.ROW_DATA[0].availablefrm;
                    rtcatlist.Movedate = AdDetails.ROW_DATA[0].Movedate;
                    rtcatlist.service = AdDetails.ROW_DATA[0].Service;
                    rtcatlist.adid = AdDetails.ROW_DATA[0].Adid;
                    //AdDetails.ROW_DATA[0].Adsid;
                    rtcatlist.agentid = AdDetails.ROW_DATA[0].Agentid;
                    rtcatlist.Isagent = AdDetails.ROW_DATA[0].Isagent;
                    rtcatlist.XmlPhotopath = AdDetails.ROW_DATA[0].XmlPhotopath;
                    rtcatlist.Imagepath = AdDetails.ROW_DATA[0].Imagepath;
                    //rtcatlist.Imagepath = AdDetails.ROW_DATA[0].HtmlPhotopath;
                    rtcatlist.Photoscnt = AdDetails.ROW_DATA[0].Photoscnt;
                    rtcatlist.Ispaid = AdDetails.ROW_DATA[0].Ispaid;
                    rtcatlist.Category = AdDetails.ROW_DATA[0].Category;
                    rtcatlist.Subcategory = AdDetails.ROW_DATA[0].Subcategory;
                    if (!string.IsNullOrEmpty(AdDetails.ROW_DATA[0].Adtype))
                    {
                        if (AdDetails.ROW_DATA[0].Adtype == "offered")
                        {
                            rtcatlist.adtype = "1";
                        }
                        else
                        {
                            rtcatlist.adtype = "0";
                        }
                    }
                    //rtcatlist.adtype = AdDetails.ROW_DATA[0].Adtype;

                    rtcatlist.Additionalcity = AdDetails.ROW_DATA[0].Additionalcity;
                    rtcatlist.Citycount = AdDetails.ROW_DATA[0].Citycount;
                    //pst.me AdDetails.ROW_DATA[0].Metrototalamount;
                    //rtcatlist.Citytotalamount = AdDetails.ROW_DATA[0].Citytotalamount;
                    rtcatlist.supercategoryid = Convert.ToInt32(AdDetails.ROW_DATA[0].Supercategoryid);
                    rtcatlist.primarycategoryid = AdDetails.ROW_DATA[0].Primarycategoryid;
                    rtcatlist.secondarycategoryid = AdDetails.ROW_DATA[0].Secondarycategoryid;
                    rtcatlist.supercategoryvalue = AdDetails.ROW_DATA[0].Supercategoryvalue;
                    rtcatlist.primarycategoryvalue = AdDetails.ROW_DATA[0].Primarycategoryvalue;
                    rtcatlist.secondarycategoryvalue = AdDetails.ROW_DATA[0].Secondarycategoryvalue;
                    rtcatlist.tertiarycategoryid = Convert.ToInt32(AdDetails.ROW_DATA[0].Tertiarycategoryid);
                    rtcatlist.tertiarycategoryvalue = AdDetails.ROW_DATA[0].Tertiarycategoryvalue;
                    rtcatlist.Tags = AdDetails.ROW_DATA[0].Tags;
                    rtcatlist.businessname = AdDetails.ROW_DATA[0].Businessname;
                    rtcatlist.licenseno = AdDetails.ROW_DATA[0].licenseno;
                    if (rtcatlist.licenseno == "new id")
                    {
                        rtcatlist.licenseno = "";
                    }
                    //rtcatlist.Companyweburl = AdDetails.ROW_DATA[0].Companyweburl;
                    rtcatlist.url = AdDetails.ROW_DATA[0].Url;
                    rtcatlist.supercategoryid = Convert.ToInt32(AdDetails.ROW_DATA[0].Supertagid);
                    rtcatlist.Bestindustry = AdDetails.ROW_DATA[0].Bestindustry;
                    //rtcatlist.Minsalary = AdDetails.ROW_DATA[0].Minsalary;
                    //rtcatlist.Qualification = AdDetails.ROW_DATA[0].Maxsalary;
                    //rtcatlist.Qualification = AdDetails.ROW_DATA[0].Qualification;
                    //rtcatlist.Visastatus = AdDetails.ROW_DATA[0].Visastatus;
                    //rtcatlist.Experience = AdDetails.ROW_DATA[0].Experience;
                    //rtcatlist.Experiencefrom = AdDetails.ROW_DATA[0].Experiencefrom;
                    //rtcatlist.Experienceto = AdDetails.ROW_DATA[0].Experienceto;
                    //rtcatlist.Experienceto = AdDetails.ROW_DATA[0].Noofopening;
                    //rtcatlist.Employmenttype = AdDetails.ROW_DATA[0].Employmenttype;
                    //rtcatlist.Jobtype = AdDetails.ROW_DATA[0].Jobtype;
                    //rtcatlist.Jobroleid = AdDetails.ROW_DATA[0].Jobroleid;
                    //rtcatlist.Jobrole = AdDetails.ROW_DATA[0].Jobrole;
                    //rtcatlist.Isresumeavail = AdDetails.ROW_DATA[0].Isresumeavail;
                    rtcatlist.Hideaddress = AdDetails.ROW_DATA[0].Hideaddress;
                    //rtcatlist.Linkedinurl = AdDetails.ROW_DATA[0].Linkedinurl;
                    rtcatlist.Trimetros = AdDetails.ROW_DATA[0].Trimetros;
                    rtcatlist.Nationwide = AdDetails.ROW_DATA[0].Nationwide;
                    rtcatlist.Salarymode = AdDetails.ROW_DATA[0].Salarymode;
                    rtcatlist.Salarymodeid = AdDetails.ROW_DATA[0].Salarymodeid;
                    rtcatlist.Streetname = rtcatlist.Locationname = AdDetails.ROW_DATA[0].Streetname;
                    rtcatlist.Zipcode = AdDetails.ROW_DATA[0].Zipcode;
                    rtcatlist.Lat = AdDetails.ROW_DATA[0].Lat;
                    rtcatlist.Long = AdDetails.ROW_DATA[0].Long;
                    rtcatlist.Newcityurl = AdDetails.ROW_DATA[0].Newcityurl;
                    rtcatlist.City = AdDetails.ROW_DATA[0].City;
                    rtcatlist.State = rtcatlist.StateName = AdDetails.ROW_DATA[0].State;
                    rtcatlist.Country = AdDetails.ROW_DATA[0].Country;
                    rtcatlist.title = AdDetails.ROW_DATA[0].Title;
                    rtcatlist.Titleurl = AdDetails.ROW_DATA[0].Titleurl;
                    rtcatlist.description = AdDetails.ROW_DATA[0].Description;
                    rtcatlist.Contributor = rtcatlist.ContactName = AdDetails.ROW_DATA[0].Contributor;
                    rtcatlist.Status = AdDetails.ROW_DATA[0].Status;
                    rtcatlist.Displayphone = AdDetails.ROW_DATA[0].Displayphone;
                    rtcatlist.Mailerstatus = AdDetails.ROW_DATA[0].Mailerstatus;
                    rtcatlist.Userpid = AdDetails.ROW_DATA[0].Userpid;
                    rtcatlist.Ordertype = AdDetails.ROW_DATA[0].Ordertype;
                    rtcatlist.Customerid = AdDetails.ROW_DATA[0].Customerid;
                    rtcatlist.Campaignid = AdDetails.ROW_DATA[0].Campaignid;
                    rtcatlist.Postedby = AdDetails.ROW_DATA[0].Postedby;
                    rtcatlist.Enddate = AdDetails.ROW_DATA[0].Enddate;
                    rtcatlist.Email = rtcatlist.ContactEmail = AdDetails.ROW_DATA[0].Email;
                    rtcatlist.Phone = rtcatlist.ContactPhone = AdDetails.ROW_DATA[0].Phone;
                    rtcatlist.Startdate = AdDetails.ROW_DATA[0].Startdate;
                    rtcatlist.Numberofdays = AdDetails.ROW_DATA[0].Numberofdays;
                    rtcatlist.Amount = AdDetails.ROW_DATA[0].Amount;
                    rtcatlist.Folderid = AdDetails.ROW_DATA[0].Folderid;
                    rtcatlist.Countrycode = AdDetails.ROW_DATA[0].Countrycode;
                    rtcatlist.Ismobileverified = AdDetails.ROW_DATA[0].Ismobileverified;
                    rtcatlist.Statecode = AdDetails.ROW_DATA[0].Statecode;
                    rtcatlist.Metro = AdDetails.ROW_DATA[0].Metro;
                    rtcatlist.Metrourl = AdDetails.ROW_DATA[0].Metrourl;
                    rtcatlist.Supercategoryvalueurl = AdDetails.ROW_DATA[0].Supercategoryvalueurl;
                    rtcatlist.Primarycategoryvalueurl = AdDetails.ROW_DATA[0].Primarycategoryvalueurl;
                    rtcatlist.Secondarycategoryvalueurl = AdDetails.ROW_DATA[0].Secondarycategoryvalueurl;
                    rtcatlist.Orderid = AdDetails.ROW_DATA[0].Orderid;
                    rtcatlist.Expirydate = AdDetails.ROW_DATA[0].Expirydate;
                    rtcatlist.Noofadcnt = AdDetails.ROW_DATA[0].Noofadcnt;
                    rtcatlist.Adpostedcnt = AdDetails.ROW_DATA[0].Adpostedcnt;
                    rtcatlist.Responsemail = AdDetails.ROW_DATA[0].Responsemail;
                    rtcatlist.Gender = AdDetails.ROW_DATA[0].Gender;
                    rtcatlist.Disclosedbusiness = AdDetails.ROW_DATA[0].Disclosedbusiness;
                    rtcatlist.Couponcode = AdDetails.ROW_DATA[0].Couponcode;
                    rtcatlist.Salespersonemail = AdDetails.ROW_DATA[0].Salespersonemail;
                    rtcatlist.Companytype = AdDetails.ROW_DATA[0].Companytype;
                    rtcatlist.Bannerstatus = AdDetails.ROW_DATA[0].Bannerstatus;
                    rtcatlist.Banneramount = AdDetails.ROW_DATA[0].Banneramount;
                    rtcatlist.Categoryflag = AdDetails.ROW_DATA[0].Categoryflag;
                    rtcatlist.Freeadsflag = AdDetails.ROW_DATA[0].Freeadsflag;
                    rtcatlist.Islisting = AdDetails.ROW_DATA[0].Islisting;
                    rtcatlist.Addonamount = AdDetails.ROW_DATA[0].Addonamount;
                    //rtcatlist.Emailblastamount = AdDetails.ROW_DATA[0].Emailblastamount;
                    rtcatlist.Bonusdays = AdDetails.ROW_DATA[0].Bonusdays;
                    rtcatlist.Bonusamount = AdDetails.ROW_DATA[0].Bonusamount;
                    rtcatlist.Pendingpaycouponcode = AdDetails.ROW_DATA[0].Pendingpaycouponcode;

                    rtcatlist.Benefits = AdDetails.ROW_DATA[0].Benefits;
                    rtcatlist.noofbaths= AdDetails.ROW_DATA[0].noofbaths;
                    rtcatlist.area = AdDetails.ROW_DATA[0].area;
                    rtcatlist.ismyneed = "1";
                    await currentpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.offerwant(rtcatlist));
                }
                else if (addata.services == "Local Services")
                {
                    var partialbusiness = RestService.For<LocalService.Features.Interfaces.IServiceAPI>(Commonsettings.TechjobsAPI);
                    var result = await partialbusiness.GetPartialaddetails(addata.oldclpostid);
                    if (result != null && result.ROW_DATA.Count > 0)
                    {
                        //LS_Posting_VM(string stagid, string stag, string ptag, string ptagid, string checkvalue, string checktext,string oldclpostid="")
                        string stagid = result.ROW_DATA[0].supertagid;
                        string stag = result.ROW_DATA[0].supertag;
                        string ptag = result.ROW_DATA[0].primarytag;
                        string ptagid = result.ROW_DATA[0].primarytagid;
                        string checkvalue = result.ROW_DATA[0].tagids;
                        string checktext = result.ROW_DATA[0].tags;
                        NRIApp.LocalService.Features.ViewModels.LS_ViewModel.islseditable = 1;
                        await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.LS_Leaftypes(ptag, Convert.ToInt32(ptagid), 2, 3, Convert.ToInt32(stagid), stag, addata.oldclpostid, checkvalue, checktext));
                    }
                }
                //else if (addata.services == "Jobs")
                //{
                //    ljpst.ismyneed = "edit";
                //    jobsedit(addata);
                //}
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public void editDC(MyNeedData data)
        {
            dccatlist.mode = "edit";
            EditAddaycare(data);
        }
        public void completeDC(MyNeedData data)
        {
            dccatlist.mode = "";
            EditAddaycare(data);
        }
        
        public void renewDC(MyNeedData data)
        {
            if(data.premiumad=="0")
            {
                dccatlist.mode = "upgrade";
            }
            else
            {
                dccatlist.mode = "renew";
            }
            EditAddaycare(data);
            //var currentpg = GetCurrentPage();
            //currentpg.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Payment_dc(dccatlist,"",""));
        }
        
        string ipaddress = DependencyService.Get<Techjobs.Features.LeadForm.Interfaces.IIPAddressManager>().GetIPAddress();

        public async void ViewAd(MyNeedData addata)
        {
            try
            {
                var currentpage = GetCurrentPage();
                if (addata.services == "Day Care")
                {
                    //string businessid,string adurl,string premiumad
                    await currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Detail.Views.CareDetail(addata.adid,addata.titleurl,addata.premiumad));
                }
                else if (addata.services == "Roommates")
                {
                    await currentpage.Navigation.PushAsync(new NRIApp.Roommates.Features.List.Views.DetailRMList(addata.adid,addata.newcityurl));
                }
                else if (addata.services == "Rentals")
                {
                    await currentpage.Navigation.PushAsync(new NRIApp.Rentals.Features.List.Views.DetailRentalList(addata.adid, addata.newcityurl));
                }
                else if (addata.services == "Jobs")
                {
                    await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Detail.Views.JobDetails(addata.adid, addata.titleurl, Commonsettings.UserPid));
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async void EditAddaycare(MyNeedData addata)
        {
            try
            {
                dialog.ShowLoading("", null);
                var currentpage = GetCurrentPage();
                var AdDetailsAPI = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                addata.oldclpostid = addata.adid;
                var AdDetails = await AdDetailsAPI.getaddaycareDetails(addata.oldclpostid, "1", Commonsettings.UserEmail, addata.services);
                if (addata.services == "Day Care")
                {
                    var optDCapi = RestService.For<DayCare.Features.Post.Interface.IDaycareposting>(Commonsettings.DaycareAPI);
                    var optlist = await optDCapi.getqandalist(addata.adid, "1");
                    foreach (var data in optlist.ROW_DATA)
                    {
                        if (!string.IsNullOrEmpty(data.xml_answeredas))
                        {
                            if (data.questionid == "3" || data.questionid == "9" || data.questionid == "14")
                            {
                                dccatlist.Experience = data.xml_answeredas.Replace("</answer>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("<answers>", ""); //<answers><answer>2012</answer></answers>
                            }
                            else if (data.questionid == "18")//carepreference
                            {
                                //dccatlist.
                                dccatlist.preference = data.xml_answeredas.Replace("</answer>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("<answers>", "");
                            }
                            else if (data.questionid == "8")//certification
                            {
                                //dccatlist.
                                dccatlist.certifications = data.xml_answeredas.Replace("</answer><answer>", ",").Replace("</answer>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("<answers>", ""); 
                            }
                            else if (data.questionid == "12")//experience
                            {
                                //dccatlist.
                                dccatlist.establishmentyear = data.xml_answeredas.Replace("</answer>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("<answers>", "");
                            }
                            else if (data.questionid == "42")
                            {
                                //response
                                dccatlist.responsetime = data.xml_answeredas.Replace("</answer>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("<answers>", "");
                            }
                            else if (data.questionid == "43")
                            {
                                //driver
                                dccatlist.drive = data.xml_answeredas;
                            }
                            else if (data.questionid == "44")
                            {
                                //vehicle
                                dccatlist.vehicle = data.xml_answeredas;
                            }
                            else if (data.questionid == "45")
                            {
                                //license
                                dccatlist.licenseno = data.xml_answeredas;
                            }
                            else if (data.questionid == "47")
                            {
                                //activeenrichment
                                dccatlist.activityenrichments = data.xml_answeredas.Replace("</answer><answer>", ",").Replace("</answer>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("<answers>", "");//<answers><answer>Role play</answer><answer>Counting activities</answer><answer>Fancy dress</answer><answer>Clay moulding</answer><answer>Outdoor games</answer><answer>Learning concepts</answer><answer>Dance</answer><answer>Hand Painting</answer></answers>
                            }
                            else if (data.questionid == "48")
                            {
                                //<answers><answer>lunch^~^12:30 am^~^2:00 am|||</answer></answers> dayschedule
                                dccatlist.availablitytable = data.xml_answeredas.Replace("</answer><answer>", ",").Replace("</answer>", "").Replace("<answer>", "");//<answers><answer>Role play</answer><answer>Counting activities</answer><answer>Fancy dress</answer><answer>Clay moulding</answer><answer>Outdoor games</answer><answer>Learning concepts</answer><answer>Dance</answer><answer>Hand Painting</answer></answers>
                            }
                            else if (data.questionid == "6" || data.questionid=="20")
                            {
                                dccatlist.gender = data.xml_answeredas.Replace("</answers>", "").Replace("<answers>", "").Replace("</answer>", "").Replace("<answer>", "");//<answers><answer>Role play</answer><answer>Counting activities</answer><answer>Fancy dress</answer><answer>Clay moulding</answer><answer>Outdoor games</answer><answer>Learning concepts</answer><answer>Dance</answer><answer>Hand Painting</answer></answers>
                                dccatlist.Genderid = "::" + data.questionid + "::" + data.questionorder;
                            }
                            else if (data.questionid == "5")
                            {
                                dccatlist.languages = data.xml_answeredas.Replace("</answer><answer>", ",").Replace("</answer>", "").Replace("<answer>", "").Replace("</answers>","").Replace("<answers>", "");
                                dccatlist.Languageid = "::" + data.questionid + "::" + data.questionorder;
                            }
                            else if (data.questionid == "30") //work experience description
                            {
                                dccatlist.Worktype= "::" + data.questionid + "::" + data.questionorder;
                                dccatlist.workdesc = data.xml_answeredas.Replace("</answers>", "").Replace("<answers>", "").Replace("</answer>", "").Replace("<answer>", "");
                            }
                        }
                     }

                    dccatlist.businessid = AdDetails.ROW_DATA[0].Businessid;
                    dccatlist.businessname = AdDetails.ROW_DATA[0].Businessname;
                    dccatlist.Businesstitleurl = AdDetails.ROW_DATA[0].Businesstitleurl;
                    dccatlist.Profiletitle = AdDetails.ROW_DATA[0].Profiletitle;
                    dccatlist.contactName = AdDetails.ROW_DATA[0].Contactname;
                    //dccatlist.Licenseno = AdDetails.ROW_DATA[0].Licenseno;
                    dccatlist.adtype = AdDetails.ROW_DATA[0].Adtype;
                    //dccatlist.Businessname = AdDetails.ROW_DATA[0].Businessname1;
                    //dccatlist.Businesstitleurl = AdDetails.ROW_DATA[0].Businesstitleurl1;
                    dccatlist.Businessaddress = AdDetails.ROW_DATA[0].Businessaddress;
                    dccatlist.zipcode = AdDetails.ROW_DATA[0].Zipcode;
                    dccatlist.city = AdDetails.ROW_DATA[0].City;
                    dccatlist.state = AdDetails.ROW_DATA[0].State;
                    dccatlist.Customerid = AdDetails.ROW_DATA[0].Customerid;
                    dccatlist.Campaignid = AdDetails.ROW_DATA[0].Campaignid;
                    dccatlist.Campaignstarts = AdDetails.ROW_DATA[0].Campaignstarts;
                    dccatlist.Campaignend = AdDetails.ROW_DATA[0].Campaignend;
                    dccatlist.country = AdDetails.ROW_DATA[0].Country;
                    //dccatlist.pricemode = AdDetails.ROW_DATA[0].Crdate;
                    dccatlist.emailid = AdDetails.ROW_DATA[0].Email;
                    dccatlist.ContactNo = AdDetails.ROW_DATA[0].Phone;
                    //dccatlist.Phone2 = AdDetails.ROW_DATA[0].Phone2;
                    dccatlist.Education = AdDetails.ROW_DATA[0].Education;
                    //dccatlist.Experience = AdDetails.ROW_DATA[0].Experience;
                    dccatlist.Backgroundcheck = AdDetails.ROW_DATA[0].Backgroundcheck;
                    dccatlist.Salarydescription = AdDetails.ROW_DATA[0].Salarydescription;
                    dccatlist.Perkdescription = AdDetails.ROW_DATA[0].Perkdescription;
                    dccatlist.Workingfrom = AdDetails.ROW_DATA[0].Workingfrom;
                    dccatlist.Workingto = AdDetails.ROW_DATA[0].Workingto;
                    dccatlist.description = AdDetails.ROW_DATA[0].Description;
                    dccatlist.Details = AdDetails.ROW_DATA[0].Details;
                   // dccatlist.Genderid = AdDetails.ROW_DATA[0].Genderid;
                    dccatlist.Preferredworktiming = AdDetails.ROW_DATA[0].Preferredworktiming;
                    dccatlist.Staffratio = AdDetails.ROW_DATA[0].Staffratio;
                    dccatlist.Showvn = AdDetails.ROW_DATA[0].Showvn;
                    dccatlist.Showvs = AdDetails.ROW_DATA[0].Showvs;
                    dccatlist.Milestotravel = AdDetails.ROW_DATA[0].Milestotravel;
                    dccatlist.Noofkids = AdDetails.ROW_DATA[0].Noofkids;
                    dccatlist.Noofadults = AdDetails.ROW_DATA[0].Noofadults;
                    dccatlist.Noofspecialneeds = AdDetails.ROW_DATA[0].Noofspecialneeds;
                    dccatlist.Salaryrate = AdDetails.ROW_DATA[0].Salaryrate;
                    dccatlist.Perkscost = AdDetails.ROW_DATA[0].Perkscost;
                    dccatlist.premiumad = AdDetails.ROW_DATA[0].Premiumad;
                    dccatlist.availablefrom = AdDetails.ROW_DATA[0].Availablefrom;
                    dccatlist.Availableto = AdDetails.ROW_DATA[0].Availableto;
                    dccatlist.Neededdays = AdDetails.ROW_DATA[0].Neededdays;
                    dccatlist.Salarymode = AdDetails.ROW_DATA[0].Salarymode;
                    dccatlist.Salarymodeid = AdDetails.ROW_DATA[0].Salarymodeid;
                    //dccatlist.gender = AdDetails.ROW_DATA[0].Gender;
                    dccatlist.userpid = AdDetails.ROW_DATA[0].Userpid;
                    dccatlist.Startdate = AdDetails.ROW_DATA[0].Startdate;
                    dccatlist.Enddate = AdDetails.ROW_DATA[0].Enddate;
                    dccatlist.Verifieddate = AdDetails.ROW_DATA[0].Verifieddate;
                    dccatlist.Status = AdDetails.ROW_DATA[0].Status;
                    dccatlist.Agegroupid = AdDetails.ROW_DATA[0].Agegroupid;
                    dccatlist.agegroups = AdDetails.ROW_DATA[0].Agegroup;
                    dccatlist.Supertag = AdDetails.ROW_DATA[0].Supertag;
                    dccatlist.supertagid = AdDetails.ROW_DATA[0].Supertagid;
                    dccatlist.primarycategoryid = AdDetails.ROW_DATA[0].Primarytagid;
                    dccatlist.Primarytag = AdDetails.ROW_DATA[0].Primarytag;
                    dccatlist.Cityurl = AdDetails.ROW_DATA[0].Cityurl;
                    //dccatlist.Languageid = AdDetails.ROW_DATA[0].Languageid;
                    //dccatlist.languages = AdDetails.ROW_DATA[0].Language;
                    dccatlist.Trainingid = AdDetails.ROW_DATA[0].Trainingid;
                    dccatlist.Training = AdDetails.ROW_DATA[0].Training;
                    dccatlist.Airportcode = AdDetails.ROW_DATA[0].Airportcode;
                    dccatlist.Statecode = AdDetails.ROW_DATA[0].Statecode;
                    dccatlist.Noofpersons = AdDetails.ROW_DATA[0].Noofpersons;
                    dccatlist.Neededtime = AdDetails.ROW_DATA[0].Neededtime;
                    dccatlist.Category = AdDetails.ROW_DATA[0].Category;
                    dccatlist.Statename = AdDetails.ROW_DATA[0].Statename;
                    dccatlist.Metro = AdDetails.ROW_DATA[0].Metro;
                    dccatlist.Nooflivefreeads = AdDetails.ROW_DATA[0].Nooflivefreeads;
                    dccatlist.Freeadposted = AdDetails.ROW_DATA[0].Freeadposted;
                    dccatlist.Freeliveadid = AdDetails.ROW_DATA[0].Freeliveadid;
                    dccatlist.Freeliveadtitle = AdDetails.ROW_DATA[0].Freeliveadtitle;
                    dccatlist.Freeliveadtitleurl = AdDetails.ROW_DATA[0].Freeliveadtitleurl;
                    dccatlist.Freeliveadnewcityurl = AdDetails.ROW_DATA[0].Freeliveadnewcityurl;
                    dccatlist.Metrourl = AdDetails.ROW_DATA[0].Metrourl;
                    dccatlist.Radius = AdDetails.ROW_DATA[0].Radius;
                    dccatlist.lat = AdDetails.ROW_DATA[0].Lat;
                    dccatlist.longtitude = AdDetails.ROW_DATA[0].Long;
                    dccatlist.maincategoryid = AdDetails.ROW_DATA[0].Maincategoryid;
                    dccatlist.Photoid = AdDetails.ROW_DATA[0].Photoid;
                    dccatlist.photo = AdDetails.ROW_DATA[0].Photos;
                    dccatlist.Videoid = AdDetails.ROW_DATA[0].Videoid;
                    dccatlist.videourl = AdDetails.ROW_DATA[0].Videourl;
                    dccatlist.Externalvideourl = AdDetails.ROW_DATA[0].Externalvideourl;
                    dccatlist.Britecode = AdDetails.ROW_DATA[0].Britecode;
                    dccatlist.Age = AdDetails.ROW_DATA[0].Age;
                    dccatlist.Website = AdDetails.ROW_DATA[0].Website;
                    dccatlist.Agentid = AdDetails.ROW_DATA[0].Agentid;
                    dccatlist.newcityurl = AdDetails.ROW_DATA[0].Newcityurl;
                    dccatlist.Folderid = AdDetails.ROW_DATA[0].Folderid;
                    dccatlist.ordergroup = AdDetails.ROW_DATA[0].Ordergroup;
                    dccatlist.ordertype = AdDetails.ROW_DATA[0].Ordertype;
                    dccatlist.numberofdays = AdDetails.ROW_DATA[0].Numberofdays;
                    dccatlist.Upgradeordergroup = AdDetails.ROW_DATA[0].Upgradeordergroup;
                    dccatlist.Upgradeordertype = AdDetails.ROW_DATA[0].Upgradeordertype;
                    dccatlist.Upgradenumberofdays = AdDetails.ROW_DATA[0].Upgradenumberofdays;
                    dccatlist.Alladid = AdDetails.ROW_DATA[0].Alladid;
                    dccatlist.Services = AdDetails.ROW_DATA[0].Services;
                    //dccatlist.Worktype = AdDetails.ROW_DATA[0].Worktype;
                    dccatlist.Carespecialchild = AdDetails.ROW_DATA[0].Carespecialchild;
                    dccatlist.Hideemail = AdDetails.ROW_DATA[0].Hideemail;
                    dccatlist.Hideaddress = AdDetails.ROW_DATA[0].Hideaddress;
                    dccatlist.Classtypeid = AdDetails.ROW_DATA[0].Classtypeid;
                    dccatlist.Classtype = AdDetails.ROW_DATA[0].Classtype;
                    dccatlist.Pettypeid = AdDetails.ROW_DATA[0].Pettypeid;
                    dccatlist.Pettype = AdDetails.ROW_DATA[0].Pettype;
                    dccatlist.supercategoryid = AdDetails.ROW_DATA[0].Supercategoryid;
                    dccatlist.supercategoryvalue = AdDetails.ROW_DATA[0].Supercategoryvalue;
                    dccatlist.primarycategoryid = AdDetails.ROW_DATA[0].Primarycategoryid;
                    dccatlist.primarycategoryvalue = AdDetails.ROW_DATA[0].Primarycategoryvalue;
                    dccatlist.secondarycategoryid = AdDetails.ROW_DATA[0].Secondarycategoryid;
                    dccatlist.secondarycategoryvalue = AdDetails.ROW_DATA[0].Secondarycategoryvalue;
                    dccatlist.tertiarycategoryid = AdDetails.ROW_DATA[0].Tertiarycategoryid;
                    dccatlist.tertiarycategoryvalue = AdDetails.ROW_DATA[0].Tertiarycategoryvalue;
                    dccatlist.countrycode = AdDetails.ROW_DATA[0].Countrycode;
                    dccatlist.Ismobileverified = AdDetails.ROW_DATA[0].Ismobileverified;
                    dccatlist.Contentid = AdDetails.ROW_DATA[0].Contentid;
                    dccatlist.Primarycategoryvalueurl = AdDetails.ROW_DATA[0].Primarycategoryvalueurl;
                    dccatlist.Secondarycategoryvalueurl = AdDetails.ROW_DATA[0].Secondarycategoryvalueurl;
                    dccatlist.Agentid1 = AdDetails.ROW_DATA[0].Agentid1;
                    dccatlist.Agentstatus = AdDetails.ROW_DATA[0].Agentstatus;
                    dccatlist.Totalads = AdDetails.ROW_DATA[0].Totalads;
                    dccatlist.Adsposted = AdDetails.ROW_DATA[0].Adsposted;
                    dccatlist.Noofadslive = AdDetails.ROW_DATA[0].Noofadslive;
                    dccatlist.Noofadsremain = AdDetails.ROW_DATA[0].Noofadsremain;
                    dccatlist.Licenseno1 = AdDetails.ROW_DATA[0].Licenseno1;
                    dccatlist.Agentdisplayphone = AdDetails.ROW_DATA[0].Agentdisplayphone;
                    dccatlist.Agentcity = AdDetails.ROW_DATA[0].Agentcity;
                    dccatlist.Agentzipcode = AdDetails.ROW_DATA[0].Agentzipcode;
                    dccatlist.Agentstatecode = AdDetails.ROW_DATA[0].Agentstatecode;
                    dccatlist.Agentcountry = AdDetails.ROW_DATA[0].Agentcountry;
                    dccatlist.Agentbusinessname = AdDetails.ROW_DATA[0].Agentbusinessname;
                    dccatlist.Agentaddress = AdDetails.ROW_DATA[0].Agentaddress;
                    dccatlist.Agentphone = AdDetails.ROW_DATA[0].Agentphone;
                    dccatlist.usertype = AdDetails.ROW_DATA[0].Usertype;
                    dccatlist.Agentemail = AdDetails.ROW_DATA[0].Agentemail;
                    dccatlist.Agentphotourl = AdDetails.ROW_DATA[0].Agentphotourl;
                    dccatlist.Agentadordertype = AdDetails.ROW_DATA[0].Agentadordertype;
                    dccatlist.Packageexpiredate = AdDetails.ROW_DATA[0].Packageexpiredate;
                    dccatlist.Packageexpiredays = AdDetails.ROW_DATA[0].Packageexpiredays;
                    dccatlist.Agentmetros = AdDetails.ROW_DATA[0].Agentmetros;
                    dccatlist.Availability = AdDetails.ROW_DATA[0].Availability;
                    dccatlist.Amount = AdDetails.ROW_DATA[0].Amount;
                    dccatlist.Discountamt = AdDetails.ROW_DATA[0].Discountamt;
                    dccatlist.Paymentamt = AdDetails.ROW_DATA[0].Paymentamt;
                    dccatlist.Couponcode = AdDetails.ROW_DATA[0].Couponcode;
                    dccatlist.addonamount = AdDetails.ROW_DATA[0].Addonamount;
                    dccatlist.Addontype = AdDetails.ROW_DATA[0].Addontype;
                    dccatlist.Categoryflag = AdDetails.ROW_DATA[0].Categoryflag;
                    dccatlist.Businessorigin = AdDetails.ROW_DATA[0].Businessorigin;
                    dccatlist.Adsourceurl = AdDetails.ROW_DATA[0].Adsourceurl;
                    dccatlist.Packageid = AdDetails.ROW_DATA[0].Packageid;
                    dccatlist.virtualnanny = AdDetails.ROW_DATA[0].Virtualnanny;
                    dccatlist.caretypes = AdDetails.ROW_DATA[0].Primarytagmobile; 
                    dccatlist.categoryurl = AdDetails.ROW_DATA[0].Secondarycategoryvalueurl;
                    dccatlist.otherserviceprovider = AdDetails.ROW_DATA[0].categoryvalues;
                    dccatlist.ismyneed = "1";
                    dccatlist.locationAddress = AdDetails.ROW_DATA[0].Businessaddress;
                    dccatlist.pkgid = AdDetails.ROW_DATA[0].Packageid;
                    dccatlist.smoke = AdDetails.ROW_DATA[0].smoke;
                    dccatlist.comfortablewithpets = AdDetails.ROW_DATA[0].comfortablewithpets;
                    dccatlist.tranportation = AdDetails.ROW_DATA[0].tranportation;
                    dccatlist.jobtype= AdDetails.ROW_DATA[0].jobtype;
                    dccatlist.salaryratemax= AdDetails.ROW_DATA[0].salaryratemax.Replace(".0","").Replace(".00","");
                    //if ((dccatlist.primarycategoryid == "3" || dccatlist.primarycategoryid == "4" || dccatlist.primarycategoryid == "5") && (dccatlist.secondarycategoryid == "8" || dccatlist.secondarycategoryid == "11" || dccatlist.secondarycategoryid == "13") && dccatlist.supercategoryid == "1")
                    //{
                    //    dccatlist.maincategoryid = "1";
                    //}
                    //else if ((dccatlist.primarycategoryid == "4" || dccatlist.primarycategoryid == "3") && (dccatlist.secondarycategoryid == "12" || dccatlist.secondarycategoryid == "9") && dccatlist.supercategoryid == "1")
                    //{
                    //    dccatlist.maincategoryid = "2";
                    //}
                    //else if (dccatlist.supercategoryid == "2" && dccatlist.primarycategoryid == "0" && dccatlist.secondarycategoryid == "0" && dccatlist.tertiarycategoryid == "0")
                    //{
                    //    dccatlist.maincategoryid = "3";
                    //}
                    //else
                    //{
                    //    dccatlist.maincategoryid = "0";
                    //}
                    //if(dccatlist.mode=="renew")
                    //{
                    //    await currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Package(dccatlist));
                    //}
                    //else if(dccatlist.mode == "edit")
                    //{
                    //    await currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.PostFirst(dccatlist));
                    //}
                    //else
                    //{
                    //    //var dcapi = RestService.For<DayCare.Features.Post.Interface.IDaycareposting>(Commonsettings.DaycareAPI);
                    //    //var postfirstdata = await dcapi.postfirstsubmit(dccatlist);
                    //    await currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.PostFirst(dccatlist));
                    //}
                    if (dccatlist.mode == "renew" || (dccatlist.mode=="upgrade" && addata.adstatus =="11"))
                    {
                        var DCpostAPI = RestService.For<DayCare.Features.Post.Interface.IDaycareposting>(Commonsettings.DaycareAPI);
                        var postresponse = await DCpostAPI.renewupdate(addata.adid, Commonsettings.UserPid);
                    }
                       
                    await currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.PostFirst(dccatlist));

                    //await currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Catfist(dccatlist));
                    // await currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.PostFirst(dccatlist));
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        
        Roommates.Features.Post.Models.Postfirst pst = new Roommates.Features.Post.Models.Postfirst();
        Rentals.Features.Post.Models.RTPostFirst rtpst = new Rentals.Features.Post.Models.RTPostFirst();
        DayCare.Features.Post.Models.Daycareposting dcpst = new DayCare.Features.Post.Models.Daycareposting();
        LocalJobs.Features.Posting.Models.Postingdata ljpst = new LocalJobs.Features.Posting.Models.Postingdata();
        Roommates.Features.Post.Models.CategoryList catlst=new Roommates.Features.Post.Models.CategoryList();
        public void RenewAd(MyNeedData addata)
        {
            ljpst.mode = "renew";
            jobsedit(addata);
        }
        public async void RenewAdLS(MyNeedData addata)
        {
            try
            {
                dialog.ShowLoading("");
                var currentpage = GetCurrentPage();
                if (addata.services == "Local Services")
                {
                    NRIApp.LocalService.Features.ViewModels.LS_ViewModel.isrenewclickble = 1;
                    await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Posting.PrimePakage(addata.oldclpostid, addata.adid));
                }

            }
            catch (Exception e)
            {

            }

        }
        public void UpgradeAd(MyNeedData addata)
        {
            ljpst.mode = "upgrade";
            jobsedit(addata);
        }
        public void jobcompletepayment(MyNeedData addata)
        {
            if (addata.services == "Jobs")
            {
                ljpst.mode = "completepayment";
                jobsedit(addata);
            }
        }
        public void editjobs(MyNeedData addata)
        {
            if (addata.services == "Jobs")
            {
                ljpst.mode = "edit";
                jobsedit(addata);
            }
        }
        public async void completepayment(MyNeedData addata)
        {
            //gotopackage page 
            try
            {
                dialog.ShowLoading("");
                var currentpage = GetCurrentPage();
                var AdDetailsAPI = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                if(addata.services=="Day Care")
                {
                    addata.oldclpostid = addata.adid;
                }
                var AdDetails = await AdDetailsAPI.getadDetails(addata.oldclpostid, "1", Commonsettings.UserEmail, addata.services);

                if (addata.services == "Roommates")
                {
                    pst.pricemode = AdDetails.ROW_DATA[0].pricemode;
                    pst.accomodate = AdDetails.ROW_DATA[0].accomodate;
                    pst.stayperiod = AdDetails.ROW_DATA[0].stayperiod;
                    pst.rent = AdDetails.ROW_DATA[0].Rent;
                    pst.attachedbaths = AdDetails.ROW_DATA[0].attachedbaths;
                    pst.availablefrm = AdDetails.ROW_DATA[0].availablefrm;
                    pst.service = AdDetails.ROW_DATA[0].Service;
                    pst.adid = AdDetails.ROW_DATA[0].Adid;
                    //AdDetails.ROW_DATA[0].Adsid;
                    pst.agentid = AdDetails.ROW_DATA[0].Agentid;
                    pst.Isagent = AdDetails.ROW_DATA[0].Isagent;
                    pst.XmlPhotopath = AdDetails.ROW_DATA[0].XmlPhotopath;
                    pst.Imagepath = AdDetails.ROW_DATA[0].Imagepath;
                    //pst.Imagepath = AdDetails.ROW_DATA[0].HtmlPhotopath;
                    pst.Photoscnt = AdDetails.ROW_DATA[0].Photoscnt;
                    pst.Ispaid = AdDetails.ROW_DATA[0].Ispaid;
                    pst.Category = AdDetails.ROW_DATA[0].Category;
                    pst.Subcategory = AdDetails.ROW_DATA[0].Subcategory;
                    if (!string.IsNullOrEmpty(AdDetails.ROW_DATA[0].Adtype))
                    {
                        if (AdDetails.ROW_DATA[0].Adtype == "offered")
                        {
                            pst.adtype = "1";
                        }
                        else
                        {
                            pst.adtype = "0";
                        }
                    }
                    //pst.adtype = AdDetails.ROW_DATA[0].Adtype;

                    pst.Additionalcity = AdDetails.ROW_DATA[0].Additionalcity;
                    pst.Citycount = AdDetails.ROW_DATA[0].Citycount;
                    //pst.me AdDetails.ROW_DATA[0].Metrototalamount;
                    pst.Citytotalamount = AdDetails.ROW_DATA[0].Citytotalamount;
                    pst.supercategoryid = AdDetails.ROW_DATA[0].Supercategoryid;
                    pst.primarycategoryid = AdDetails.ROW_DATA[0].Primarycategoryid.ToString();
                    pst.secondarycategoryid = AdDetails.ROW_DATA[0].Secondarycategoryid;
                    pst.supercategoryvalue = AdDetails.ROW_DATA[0].Supercategoryvalue;
                    pst.primarycategoryvalue = AdDetails.ROW_DATA[0].Primarycategoryvalue;
                    pst.secondarycategoryvalue = AdDetails.ROW_DATA[0].Secondarycategoryvalue;
                    pst.Tags = AdDetails.ROW_DATA[0].Tags;
                    pst.businessname = AdDetails.ROW_DATA[0].Businessname;
                    pst.licenseno = AdDetails.ROW_DATA[0].licenseno;
                    if (pst.licenseno == "new id")
                    {
                        pst.licenseno = "";
                    }
                    pst.url = AdDetails.ROW_DATA[0].Url;
                    pst.supercategoryid = Convert.ToInt32(AdDetails.ROW_DATA[0].Supertagid);
                    pst.Bestindustry = AdDetails.ROW_DATA[0].Bestindustry;
                    pst.Hideaddress = AdDetails.ROW_DATA[0].Hideaddress;
                    pst.Linkedinurl = AdDetails.ROW_DATA[0].Linkedinurl;
                    pst.Trimetros = AdDetails.ROW_DATA[0].Trimetros;
                    pst.Nationwide = AdDetails.ROW_DATA[0].Nationwide;
                    pst.Salarymode = AdDetails.ROW_DATA[0].Salarymode;
                    pst.Salarymodeid = AdDetails.ROW_DATA[0].Salarymodeid;
                    pst.Streetname = pst.Locationname = AdDetails.ROW_DATA[0].Streetname;
                    pst.Zipcode = AdDetails.ROW_DATA[0].Zipcode;
                    pst.Lat = AdDetails.ROW_DATA[0].Lat;
                    pst.Long = AdDetails.ROW_DATA[0].Long;
                    pst.Newcityurl = AdDetails.ROW_DATA[0].Newcityurl;
                    pst.City = AdDetails.ROW_DATA[0].City;
                    pst.State = pst.StateName = AdDetails.ROW_DATA[0].State;
                    pst.Country = AdDetails.ROW_DATA[0].Country;
                    pst.title = AdDetails.ROW_DATA[0].Title;
                    pst.Titleurl = AdDetails.ROW_DATA[0].Titleurl;
                    pst.description = AdDetails.ROW_DATA[0].Description;
                    pst.Contributor = pst.ContactName = AdDetails.ROW_DATA[0].Contributor;
                    pst.Status = AdDetails.ROW_DATA[0].Status;
                    pst.Displayphone = AdDetails.ROW_DATA[0].Displayphone;
                    pst.Mailerstatus = AdDetails.ROW_DATA[0].Mailerstatus;
                    pst.userpid = AdDetails.ROW_DATA[0].Userpid;
                    pst.Ordertype = AdDetails.ROW_DATA[0].Ordertype;
                    pst.Customerid = AdDetails.ROW_DATA[0].Customerid;
                    pst.Campaignid = AdDetails.ROW_DATA[0].Campaignid;
                    pst.Postedby = AdDetails.ROW_DATA[0].Postedby;
                    pst.Enddate = AdDetails.ROW_DATA[0].Enddate;
                    pst.Email = pst.ContactEmail = AdDetails.ROW_DATA[0].Email;
                    pst.Phone = pst.ContactPhone = AdDetails.ROW_DATA[0].Phone;
                    pst.Startdate = AdDetails.ROW_DATA[0].Startdate;
                    pst.Numberofdays = AdDetails.ROW_DATA[0].Numberofdays;
                    pst.Amount = AdDetails.ROW_DATA[0].Amount;
                    pst.Folderid = AdDetails.ROW_DATA[0].Folderid;
                    pst.Countrycode = AdDetails.ROW_DATA[0].Countrycode;
                    pst.Ismobileverified = AdDetails.ROW_DATA[0].Ismobileverified;
                    pst.StateCode = AdDetails.ROW_DATA[0].Statecode;
                    pst.Metro = AdDetails.ROW_DATA[0].Metro;
                    pst.Metrourl = AdDetails.ROW_DATA[0].Metrourl;
                    pst.Supercategoryvalueurl = AdDetails.ROW_DATA[0].Supercategoryvalueurl;
                    pst.Primarycategoryvalueurl = AdDetails.ROW_DATA[0].Primarycategoryvalueurl;
                    pst.Secondarycategoryvalueurl = AdDetails.ROW_DATA[0].Secondarycategoryvalueurl;
                    pst.Orderid = AdDetails.ROW_DATA[0].Orderid;
                    pst.Expirydate = AdDetails.ROW_DATA[0].Expirydate;
                    pst.Noofadcnt = AdDetails.ROW_DATA[0].Noofadcnt;
                    pst.Adpostedcnt = AdDetails.ROW_DATA[0].Adpostedcnt;
                    pst.Responsemail = AdDetails.ROW_DATA[0].Responsemail;
                    pst.Gender = AdDetails.ROW_DATA[0].Gender;
                    pst.Disclosedbusiness = AdDetails.ROW_DATA[0].Disclosedbusiness;
                    pst.Couponcode = AdDetails.ROW_DATA[0].Couponcode;
                    pst.Salespersonemail = AdDetails.ROW_DATA[0].Salespersonemail;
                    pst.Companytype = AdDetails.ROW_DATA[0].Companytype;
                    pst.Bannerstatus = AdDetails.ROW_DATA[0].Bannerstatus;
                    pst.Banneramount = AdDetails.ROW_DATA[0].Banneramount;
                    pst.Categoryflag = AdDetails.ROW_DATA[0].Categoryflag;
                    pst.Freeadsflag = AdDetails.ROW_DATA[0].Freeadsflag;
                    pst.Islisting = AdDetails.ROW_DATA[0].Islisting;
                    pst.Addonamount = AdDetails.ROW_DATA[0].Addonamount;
                    pst.Emailblastamount = AdDetails.ROW_DATA[0].Emailblastamount;
                    pst.Bonusdays = AdDetails.ROW_DATA[0].Bonusdays;
                    pst.Bonusamount = AdDetails.ROW_DATA[0].Bonusamount;
                    pst.Pendingpaycouponcode = AdDetails.ROW_DATA[0].Pendingpaycouponcode;
                    pst.LocationAddress = AdDetails.ROW_DATA[0].Streetname;
                    pst.Benefits = AdDetails.ROW_DATA[0].Benefits;
                    pst.ismyneed = "1";
                    //if(addata.editvisible==true)
                    //{
                    //    await currentpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.offerwant(pst));
                    //}
                    //else
                    //{
                        await currentpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Package_RM(pst));
                    //}
                }
                else if (addata.services == "Rentals")
                {
                    rtpst.pricemode = AdDetails.ROW_DATA[0].pricemode;
                    rtpst.accomodate = AdDetails.ROW_DATA[0].accomodate;
                    rtpst.stayperiod = AdDetails.ROW_DATA[0].stayperiod;
                    rtpst.Rent = AdDetails.ROW_DATA[0].Rent;
                    rtpst.attachedbaths = AdDetails.ROW_DATA[0].attachedbaths;
                    rtpst.availablefrm = AdDetails.ROW_DATA[0].availablefrm;
                    rtpst.Movedate = AdDetails.ROW_DATA[0].Movedate;
                    rtpst.service = AdDetails.ROW_DATA[0].Service;
                    rtpst.adid = AdDetails.ROW_DATA[0].Adid;
                    //AdDetails.ROW_DATA[0].Adsid;
                    rtpst.agentid = AdDetails.ROW_DATA[0].Agentid;
                    rtpst.Isagent = AdDetails.ROW_DATA[0].Isagent;
                    rtpst.XmlPhotopath = AdDetails.ROW_DATA[0].XmlPhotopath;
                    rtpst.Imagepath = AdDetails.ROW_DATA[0].Imagepath;
                    //rtcatlist.Imagepath = AdDetails.ROW_DATA[0].HtmlPhotopath;
                    rtpst.Photoscnt = AdDetails.ROW_DATA[0].Photoscnt;
                    rtpst.Ispaid = AdDetails.ROW_DATA[0].Ispaid;
                    rtpst.Category= rtpst.maincategory = AdDetails.ROW_DATA[0].Category;
                    rtpst.Subcategory = AdDetails.ROW_DATA[0].Subcategory;
                    if (!string.IsNullOrEmpty(AdDetails.ROW_DATA[0].Adtype))
                    {
                        if (AdDetails.ROW_DATA[0].Adtype == "offered")
                        {
                            rtpst.adtype = 1;
                        }
                        else
                        {
                            rtpst.adtype = 0;
                        }
                    }
                    rtpst.Additionalcity = AdDetails.ROW_DATA[0].Additionalcity;
                    rtpst.Citycount = AdDetails.ROW_DATA[0].Citycount;
                    rtpst.supercategoryid = Convert.ToInt32(AdDetails.ROW_DATA[0].Supercategoryid);
                    if(rtpst.supercategoryid==1 || rtpst.supercategoryid == 3 || rtpst.supercategoryid == 36)
                    {
                        Commonsettings.CRTAdTypeID = 1;
                    }
                    rtpst.primarycategoryid = AdDetails.ROW_DATA[0].Primarycategoryid;
                    rtpst.secondarycategoryid = AdDetails.ROW_DATA[0].Secondarycategoryid;
                    rtpst.supercategoryvalue = AdDetails.ROW_DATA[0].Supercategoryvalue;
                    rtpst.primarycategoryvalue = AdDetails.ROW_DATA[0].Primarycategoryvalue;
                    rtpst.secondarycategoryvalue = AdDetails.ROW_DATA[0].Secondarycategoryvalue;
                    rtpst.tertiarycategoryid = Convert.ToInt32(AdDetails.ROW_DATA[0].Tertiarycategoryid);
                    rtpst.tertiarycategoryvalue = AdDetails.ROW_DATA[0].Tertiarycategoryvalue;
                    rtpst.Tags = AdDetails.ROW_DATA[0].Tags;
                    rtpst.businessname = AdDetails.ROW_DATA[0].Businessname;
                    rtpst.businessLicenseno = AdDetails.ROW_DATA[0].licenseno;
                    if (rtpst.businessLicenseno == "new id")
                    {
                        rtpst.businessLicenseno = "";
                    }
                    rtpst.url = AdDetails.ROW_DATA[0].Url;
                    rtpst.supercategoryid = Convert.ToInt32(AdDetails.ROW_DATA[0].Supertagid);
                    rtpst.Bestindustry = AdDetails.ROW_DATA[0].Bestindustry;
                    rtpst.Hideaddress = AdDetails.ROW_DATA[0].Hideaddress;
                    rtpst.Trimetros = AdDetails.ROW_DATA[0].Trimetros;
                    rtpst.Nationwide = AdDetails.ROW_DATA[0].Nationwide;
                    rtpst.Salarymode = AdDetails.ROW_DATA[0].Salarymode;
                    rtpst.Salarymodeid = AdDetails.ROW_DATA[0].Salarymodeid;
                    rtpst.Streetname = rtcatlist.Locationname = AdDetails.ROW_DATA[0].Streetname;
                    rtpst.Zipcode = AdDetails.ROW_DATA[0].Zipcode;
                    rtpst.Lat = AdDetails.ROW_DATA[0].Lat;
                    rtpst.Long = AdDetails.ROW_DATA[0].Long;
                    rtpst.Newcityurl = AdDetails.ROW_DATA[0].Newcityurl;
                    rtpst.City = AdDetails.ROW_DATA[0].City;
                    rtpst.State = rtcatlist.StateName = AdDetails.ROW_DATA[0].State;
                    rtpst.Country = AdDetails.ROW_DATA[0].Country;
                    rtpst.title = AdDetails.ROW_DATA[0].Title;
                    rtpst.Titleurl = AdDetails.ROW_DATA[0].Titleurl;
                    rtpst.description = AdDetails.ROW_DATA[0].Description;
                    rtpst.Contributor = rtcatlist.ContactName = AdDetails.ROW_DATA[0].Contributor;
                    rtpst.Status = AdDetails.ROW_DATA[0].Status;
                    rtpst.Displayphone = AdDetails.ROW_DATA[0].Displayphone;
                    rtpst.Mailerstatus = AdDetails.ROW_DATA[0].Mailerstatus;
                    rtpst.userpid = AdDetails.ROW_DATA[0].Userpid;
                    rtpst.Ordertype = AdDetails.ROW_DATA[0].Ordertype;
                    rtpst.Customerid = AdDetails.ROW_DATA[0].Customerid;
                    rtpst.Campaignid = AdDetails.ROW_DATA[0].Campaignid;
                    rtpst.Postedby = AdDetails.ROW_DATA[0].Postedby;
                    rtpst.Enddate = AdDetails.ROW_DATA[0].Enddate;
                    rtpst.Email = rtcatlist.ContactEmail = AdDetails.ROW_DATA[0].Email;
                    rtpst.Phone = rtcatlist.ContactPhone = AdDetails.ROW_DATA[0].Phone;
                    rtpst.Startdate = AdDetails.ROW_DATA[0].Startdate;
                    rtpst.Numberofdays = AdDetails.ROW_DATA[0].Numberofdays;
                    rtpst.Amount = AdDetails.ROW_DATA[0].Amount;
                    rtpst.Folderid = AdDetails.ROW_DATA[0].Folderid;
                    rtpst.Countrycode = AdDetails.ROW_DATA[0].Countrycode;
                    rtpst.Ismobileverified = AdDetails.ROW_DATA[0].Ismobileverified;
                    rtpst.StateCode = AdDetails.ROW_DATA[0].Statecode;
                    rtpst.Metro = AdDetails.ROW_DATA[0].Metro;
                    rtpst.Metrourl = AdDetails.ROW_DATA[0].Metrourl;
                    rtpst.Supercategoryvalueurl = AdDetails.ROW_DATA[0].Supercategoryvalueurl;
                    rtpst.Primarycategoryvalueurl = AdDetails.ROW_DATA[0].Primarycategoryvalueurl;
                    rtpst.Secondarycategoryvalueurl = AdDetails.ROW_DATA[0].Secondarycategoryvalueurl;
                    rtpst.Orderid = AdDetails.ROW_DATA[0].Orderid;
                    rtpst.Expirydate = AdDetails.ROW_DATA[0].Expirydate;
                    rtpst.Noofadcnt = AdDetails.ROW_DATA[0].Noofadcnt;
                    rtpst.Adpostedcnt = AdDetails.ROW_DATA[0].Adpostedcnt;
                    rtpst.Responsemail = AdDetails.ROW_DATA[0].Responsemail;
                    rtpst.Gender = AdDetails.ROW_DATA[0].Gender;
                    rtpst.Disclosedbusiness = AdDetails.ROW_DATA[0].Disclosedbusiness;
                    rtpst.Couponcode = AdDetails.ROW_DATA[0].Couponcode;
                    rtpst.Salespersonemail = AdDetails.ROW_DATA[0].Salespersonemail;
                    rtpst.Companytype = AdDetails.ROW_DATA[0].Companytype;
                    rtpst.Bannerstatus = AdDetails.ROW_DATA[0].Bannerstatus;
                    rtpst.Banneramount = AdDetails.ROW_DATA[0].Banneramount;
                    rtpst.Categoryflag = AdDetails.ROW_DATA[0].Categoryflag;
                    rtpst.Freeadsflag = AdDetails.ROW_DATA[0].Freeadsflag;
                    rtpst.Islisting = AdDetails.ROW_DATA[0].Islisting;
                    rtpst.Addonamount = AdDetails.ROW_DATA[0].Addonamount;
                    //rtcatlist.Emailblastamount = AdDetails.ROW_DATA[0].Emailblastamount;
                    rtpst.Bonusdays = AdDetails.ROW_DATA[0].Bonusdays;
                    rtpst.Bonusamount = AdDetails.ROW_DATA[0].Bonusamount;
                    rtpst.Pendingpaycouponcode = AdDetails.ROW_DATA[0].Pendingpaycouponcode;

                    rtpst.Benefits = AdDetails.ROW_DATA[0].Benefits;
                    rtpst.noofbaths = AdDetails.ROW_DATA[0].noofbaths;
                    rtpst.area = AdDetails.ROW_DATA[0].area;

                   
                    rtpst.ismyneed = "1";
                    await currentpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Package_RT(rtpst));
                }
                else if (addata.services == "Day Care")
                {
                    //public async void getpostfirstdata()
                   // {
                        try
                        {
                            dialog.ShowLoading("", null);
                            var DCapi = RestService.For<DayCare.Features.Post.Interface.IDaycareposting>(Commonsettings.DaycareAPI);

                            var list = await DCapi.getqandalist(addata.adid , "1");
                            //  var list = await DCapi.getqandalist("254000");

                            ListView qandalist = new ListView();

                            XmlDocument doc = new XmlDocument();
                            foreach (var data in list.ROW_DATA)
                            {
                                if (!string.IsNullOrEmpty(data.xml_answers))
                                {
                                    doc.LoadXml(data.xml_answers);
                                    string jsonText = JsonConvert.SerializeXmlNode(doc);
                                    XmlNode djkh = JsonConvert.DeserializeXmlNode(jsonText);
                                    string val = djkh.InnerXml.Replace("</option><option>", ",").Replace("<option>", "").Replace("</option>", "").Replace("<options>", "").Replace("</options>", "");
                                    string[] xmlval = System.Text.RegularExpressions.Regex.Split(val, ",");
                                    if (data.questionid == "6" || data.questionid == "20")
                                    {
                                        foreach (var j in xmlval)
                                        {
                                            string genderquestionID = "::" + data.questionid + "::" + data.questionorder; ;
                                            dcpst.gender = "<answers><answer>" + dcpst.gender + "</answer></answers>" + genderquestionID;
                                        }
                                    }
                                    else if (data.questionid == "3" || data.questionid == "9" || data.questionid == "14")
                                    {
                                        string paidexperiencequestionID = "::" + data.questionid + "::" + data.questionorder;
                                        dcpst.Experience = "<answers><answer>" + dcpst.Experience + "</answer></answers>" + paidexperiencequestionID;
                                    }
                                    else if (data.questionid == "8")
                                    {
                                       string certificationquestionID = "::" + data.questionid + "::" + data.questionorder;
                                    }
                                    else if (data.questionid == "42")
                                    {
                                        dcpst.responsetime = "<answers>" + dcpst.responsetime + "</answers>" + "::" + data.questionid + "::" + data.questionorder;
                                    }
                                    else if (data.questionid == "43")
                                    {
                                        dcpst.drive =  "<answers><answer>" + dcpst + "</answer></answers>::" + data.questionid + "::" + data.questionorder;
                                    }
                                    else if (data.questionid == "44")
                                    {
                                    dcpst.vehicle = "<answers><answer>" + dcpst.vehicle + "</answer></answers>::" + data.questionid + "::" + data.questionorder;
                                    }
                                    else if (data.questionid == "45")
                                    {
                                        dcpst.license = "<answers><answer>" + dcpst.license + "</answer></answers>::" + data.questionid + "::" + data.questionorder;
                                    }

                                    else if (data.questionid == "47")
                                    {
                                        dcpst.activityenrichments = "<answers>" + dcpst.activityenrichments + "</answers>" + "::" + data.questionid + "::" + data.questionorder;
                                    }
                                    else if (data.questionid == "35" || data.questionid == "13" || data.questionid == "11" || data.questionid == "12" || data.questionid == "14")
                                    {
                                        dcpst.careexperience = "<answers>" + "<answer>" + dcpst.careexperience + "</answer>" + "</answers>" + "::" + data.questionid + "::" + data.questionorder;
                                    }
                                    //wanted posting 
                                    else if (data.questionid == "18" || data.questionid == "22")
                                    {
                                        dcpst.preference = dcpst.preference + data.question;
                                    }
                                }
                            }
                            if ((AdDetails.ROW_DATA[0].Supercategoryid == 2 && AdDetails.ROW_DATA[0].Primarycategoryid == 25) || (AdDetails.ROW_DATA[0].Supercategoryid == 2 && AdDetails.ROW_DATA[0].Primarycategoryid == 26))
                            {
                                //var optDCapi = RestService.For<DayCare.Features.Post.Interface.IDaycareposting>(Commonsettings.DaycareAPI);
                                var optlist = await DCapi.getqandalist(addata.adid, "0");
                                ListView optqandalist = new ListView();
                                XmlDocument optdoc = new XmlDocument();
                                string languageqid = "";
                                foreach (var optdata in optlist.ROW_DATA)
                                {
                                    if (!string.IsNullOrEmpty(optdata.xml_answers))
                                    {
                                        doc.LoadXml(optdata.xml_answers);
                                        string jsonText = JsonConvert.SerializeXmlNode(doc);
                                        XmlNode djkh = JsonConvert.DeserializeXmlNode(jsonText);
                                        string val = djkh.InnerXml.Replace("</option><option>", ",").Replace("<option>", "").Replace("</option>", "").Replace("<options>", "").Replace("</options>", "");
                                        string[] xmlval = System.Text.RegularExpressions.Regex.Split(val, ",");
                                        if (optdata.questionid == "5" || optdata.questionid == "19")
                                        {
                                            languageqid = "::" + optdata.questionid + "::" + optdata.questionorder;
                                        }

                                    }
                                }
                                string languageval = "";
                                string[] languge = dcpst.languages.Split(',');
                                foreach(var lang in languge)
                                {
                                    languageval += "<answer>" + lang + "</answer>";
                                }
                                dcpst.languages = languageval + languageqid;
                            }
                            //getotherserviceprovider();
                        }
                        catch (Exception ex)
                        {
                            dialog.HideLoading();
                        }
                    //}




                    dcpst.businessid = addata.adid;
                    dcpst.businessname = AdDetails.ROW_DATA[0].Businessname;
                    dcpst.Businesstitleurl = AdDetails.ROW_DATA[0].Businesstitleurl;
                    dcpst.Profiletitle = AdDetails.ROW_DATA[0].Profiletitle;
                    dcpst.contactName = AdDetails.ROW_DATA[0].Contactname;
                    //dcpst.licenseno = AdDetails.ROW_DATA[0].licenseno;
                    dcpst.adtype = AdDetails.ROW_DATA[0].Adtype;
                    //dcpst.Businessname = AdDetails.ROW_DATA[0].Businessname1;
                    //dcpst.Businesstitleurl = AdDetails.ROW_DATA[0].Businesstitleurl1;
                    dcpst.Businessaddress = AdDetails.ROW_DATA[0].Businessaddress;
                    dcpst.locationAddress= AdDetails.ROW_DATA[0].Businessaddress;
                    dcpst.zipcode = AdDetails.ROW_DATA[0].Zipcode;
                    dcpst.city = AdDetails.ROW_DATA[0].City;
                    dcpst.state = AdDetails.ROW_DATA[0].State;
                    dcpst.Customerid = AdDetails.ROW_DATA[0].Customerid;
                    dcpst.Campaignid = AdDetails.ROW_DATA[0].Campaignid;
                    dcpst.Campaignstarts = AdDetails.ROW_DATA[0].Campaignstarts;
                    dcpst.Campaignend = AdDetails.ROW_DATA[0].Campaignend;
                    dcpst.country = AdDetails.ROW_DATA[0].Country;
                    //dcpst.pricemode = AdDetails.ROW_DATA[0].Crdate;
                    dcpst.emailid = AdDetails.ROW_DATA[0].Email;
                    dcpst.ContactNo = AdDetails.ROW_DATA[0].Phone;
                    //dcpst.Phone2 = AdDetails.ROW_DATA[0].Phone2;
                    dcpst.Education = AdDetails.ROW_DATA[0].Education;
                   // dcpst.Experience = AdDetails.ROW_DATA[0].Experience;
                    dcpst.Backgroundcheck = AdDetails.ROW_DATA[0].Backgroundcheck;
                    dcpst.Salarydescription = AdDetails.ROW_DATA[0].Salarydescription;
                    dcpst.Perkdescription = AdDetails.ROW_DATA[0].Perkdescription;
                    dcpst.Workingfrom = AdDetails.ROW_DATA[0].Workingfrom;
                    dcpst.Workingto = AdDetails.ROW_DATA[0].Workingto;
                    dcpst.description = AdDetails.ROW_DATA[0].Description;
                    dcpst.Details = AdDetails.ROW_DATA[0].Details;
                    dcpst.Genderid = AdDetails.ROW_DATA[0].Genderid;
                    dcpst.Preferredworktiming = AdDetails.ROW_DATA[0].Preferredworktiming;
                    dcpst.Staffratio = AdDetails.ROW_DATA[0].Staffratio;
                    dcpst.Showvn = AdDetails.ROW_DATA[0].Showvn;
                    dcpst.Showvs = AdDetails.ROW_DATA[0].Showvs;
                    dcpst.Milestotravel = AdDetails.ROW_DATA[0].Milestotravel;
                    dcpst.Noofkids = AdDetails.ROW_DATA[0].Noofkids;
                    dcpst.Noofadults = AdDetails.ROW_DATA[0].Noofadults;
                    dcpst.Noofspecialneeds = AdDetails.ROW_DATA[0].Noofspecialneeds;
                    dcpst.Salaryrate = AdDetails.ROW_DATA[0].Salaryrate;
                    dcpst.Perkscost = AdDetails.ROW_DATA[0].Perkscost;
                    dcpst.premiumad = AdDetails.ROW_DATA[0].Premiumad;
                    dcpst.availablefrom = AdDetails.ROW_DATA[0].Availablefrom;
                    dcpst.Availableto = AdDetails.ROW_DATA[0].Availableto;
                    dcpst.Neededdays = AdDetails.ROW_DATA[0].Neededdays;
                    dcpst.Salarymode = AdDetails.ROW_DATA[0].Salarymode;
                    dcpst.Salarymodeid = AdDetails.ROW_DATA[0].Salarymodeid;
                    dcpst.gender = AdDetails.ROW_DATA[0].Gender;
                    dcpst.userpid = AdDetails.ROW_DATA[0].Userpid;
                    dcpst.Startdate = AdDetails.ROW_DATA[0].Startdate;
                    dcpst.Enddate = AdDetails.ROW_DATA[0].Enddate;
                    dcpst.Verifieddate = AdDetails.ROW_DATA[0].Verifieddate;
                    dcpst.Status = AdDetails.ROW_DATA[0].Status;
                    dcpst.Agegroupid = AdDetails.ROW_DATA[0].Agegroupid.TrimStart(',');
                    dcpst.agegroups = AdDetails.ROW_DATA[0].Agegroup.TrimStart(',');

                    if (!string.IsNullOrEmpty(AdDetails.ROW_DATA[0].Agegroup))
                    {
                        string agegrpvalue = "";
                        string emptyagegrpvalue = "";
                        var dcapi = RestService.For<DayCare.Features.Post.Interface.IDaycareposting>(Commonsettings.DaycareAPI);
                        DayCare.Features.Post.Models.careservice agegroupdata = await dcapi.getagegroup();
                        agegrouplstdata = agegroupdata.ROW_DATA;
                        string[] agetypelst = AdDetails.ROW_DATA[0].Agegroup.Split(',');
                        foreach (var tdata in agetypelst)
                        {

                            foreach (var data in agegrouplstdata)
                            {
                                if (data.primarytag == tdata)
                                {
                                    data.primarytag = data.primarytag.Replace("&amp", "").Replace("&amp;", "").Replace("amp", "").Replace("amp;", "");
                                    emptyagegrpvalue = data.primarytagid.ToString() + "::" + data.primarytag + "::" + data.primarytagurl;
                                }
                            }
                            if(string.IsNullOrEmpty(agegrpvalue))
                            {
                                agegrpvalue = emptyagegrpvalue;
                            }
                            else
                            {
                                agegrpvalue += ","+ emptyagegrpvalue;
                            }
                            
                        }
                        dcpst.agegroups = agegrpvalue;

                    }
                    dcpst.Supertag = AdDetails.ROW_DATA[0].Supertag;
                    dcpst.supertagid = AdDetails.ROW_DATA[0].Supertagid;
                    dcpst.primarycategoryid = AdDetails.ROW_DATA[0].Primarytagid;
                    dcpst.Primarytag = AdDetails.ROW_DATA[0].Primarytag;
                    dcpst.Cityurl = AdDetails.ROW_DATA[0].Cityurl;
                    dcpst.Languageid = AdDetails.ROW_DATA[0].Languageid;
                    //dcpst.languages = AdDetails.ROW_DATA[0].Language;
                    dcpst.Trainingid = AdDetails.ROW_DATA[0].Trainingid;
                    dcpst.Training = AdDetails.ROW_DATA[0].Training;
                    dcpst.Airportcode = AdDetails.ROW_DATA[0].Airportcode;
                    dcpst.Statecode = AdDetails.ROW_DATA[0].Statecode;
                    dcpst.Noofpersons = AdDetails.ROW_DATA[0].Noofpersons;
                    dcpst.Neededtime = AdDetails.ROW_DATA[0].Neededtime;
                    dcpst.Category = AdDetails.ROW_DATA[0].Category;
                    dcpst.Statename = AdDetails.ROW_DATA[0].Statename;
                    dcpst.Metro = AdDetails.ROW_DATA[0].Metro;
                    dcpst.Nooflivefreeads = AdDetails.ROW_DATA[0].Nooflivefreeads;
                    dcpst.Freeadposted = AdDetails.ROW_DATA[0].Freeadposted;
                    dcpst.Freeliveadid = AdDetails.ROW_DATA[0].Freeliveadid;
                    dcpst.Freeliveadtitle = AdDetails.ROW_DATA[0].Freeliveadtitle;
                    dcpst.Freeliveadtitleurl = AdDetails.ROW_DATA[0].Freeliveadtitleurl;
                    dcpst.Freeliveadnewcityurl = AdDetails.ROW_DATA[0].Freeliveadnewcityurl;
                    dcpst.Metrourl = AdDetails.ROW_DATA[0].Metrourl;
                    dcpst.Radius = AdDetails.ROW_DATA[0].Radius;
                    dcpst.lat =Convert.ToDouble(AdDetails.ROW_DATA[0].Lat);
                    dcpst.longtitude = Convert.ToDouble(AdDetails.ROW_DATA[0].Long);
                    //dcpst.maincategory = AdDetails.ROW_DATA[0].mai;
                    dcpst.maincategoryid = AdDetails.ROW_DATA[0].Maincategoryid;
                    dcpst.Photoid = AdDetails.ROW_DATA[0].Photoid;
                    dcpst.photo = AdDetails.ROW_DATA[0].Photos;
                    dcpst.Videoid = AdDetails.ROW_DATA[0].Videoid;
                    dcpst.videourl = AdDetails.ROW_DATA[0].Videourl;
                    dcpst.Externalvideourl = AdDetails.ROW_DATA[0].Externalvideourl;
                    dcpst.Britecode = AdDetails.ROW_DATA[0].Britecode;
                    dcpst.Age = AdDetails.ROW_DATA[0].Age;
                    dcpst.Website = AdDetails.ROW_DATA[0].Website;
                    dcpst.Agentid = AdDetails.ROW_DATA[0].Agentid.ToString();
                    dcpst.newcityurl = AdDetails.ROW_DATA[0].Newcityurl;
                    dcpst.Folderid = AdDetails.ROW_DATA[0].Folderid;
                    dcpst.ordergroup = AdDetails.ROW_DATA[0].Ordergroup;
                    dcpst.ordertype = AdDetails.ROW_DATA[0].Ordertype;
                    dcpst.numberofdays = AdDetails.ROW_DATA[0].Numberofdays;
                    dcpst.Upgradeordergroup = AdDetails.ROW_DATA[0].Upgradeordergroup;
                    dcpst.Upgradeordertype = AdDetails.ROW_DATA[0].Upgradeordertype;
                    dcpst.Upgradenumberofdays = AdDetails.ROW_DATA[0].Upgradenumberofdays;
                    dcpst.Alladid = AdDetails.ROW_DATA[0].Alladid;
                    dcpst.Services = AdDetails.ROW_DATA[0].Services;
                    dcpst.Worktype = AdDetails.ROW_DATA[0].Worktype;
                    dcpst.Carespecialchild = AdDetails.ROW_DATA[0].Carespecialchild;
                    dcpst.Hideemail = AdDetails.ROW_DATA[0].Hideemail;
                    dcpst.Hideaddress = AdDetails.ROW_DATA[0].Hideaddress;
                    dcpst.Classtypeid = AdDetails.ROW_DATA[0].Classtypeid;
                    dcpst.Classtype = AdDetails.ROW_DATA[0].Classtype;
                    dcpst.Pettypeid = AdDetails.ROW_DATA[0].Pettypeid;
                    dcpst.Pettype = AdDetails.ROW_DATA[0].Pettype;
                    dcpst.supercategoryid = AdDetails.ROW_DATA[0].Supercategoryid.ToString();
                    dcpst.supercategoryvalue = AdDetails.ROW_DATA[0].Supercategoryvalue;
                    dcpst.primarycategoryid = AdDetails.ROW_DATA[0].Primarycategoryid.ToString();
                    dcpst.primarycategoryvalue = AdDetails.ROW_DATA[0].Primarycategoryvalue;
                    dcpst.secondarycategoryid = AdDetails.ROW_DATA[0].Secondarycategoryid.ToString();
                    dcpst.secondarycategoryvalue = AdDetails.ROW_DATA[0].Secondarycategoryvalue;
                    dcpst.tertiarycategoryid = AdDetails.ROW_DATA[0].Tertiarycategoryid.ToString();
                    dcpst.tertiarycategoryvalue = AdDetails.ROW_DATA[0].Tertiarycategoryvalue;
                    dcpst.countrycode = AdDetails.ROW_DATA[0].Countrycode;
                    dcpst.Ismobileverified = AdDetails.ROW_DATA[0].Ismobileverified;
                    dcpst.Contentid = AdDetails.ROW_DATA[0].Contentid;
                    dcpst.Primarycategoryvalueurl = AdDetails.ROW_DATA[0].Primarycategoryvalueurl;
                    dcpst.Secondarycategoryvalueurl = AdDetails.ROW_DATA[0].Secondarycategoryvalueurl;
                    dcpst.Agentid1 = AdDetails.ROW_DATA[0].Agentid1;
                    dcpst.Agentstatus = AdDetails.ROW_DATA[0].Agentstatus;
                    dcpst.Totalads = AdDetails.ROW_DATA[0].Totalads;
                    dcpst.Adsposted = AdDetails.ROW_DATA[0].Adsposted;
                    dcpst.Noofadslive = AdDetails.ROW_DATA[0].Noofadslive;
                    dcpst.Noofadsremain = AdDetails.ROW_DATA[0].Noofadsremain;
                    dcpst.Licenseno1 = AdDetails.ROW_DATA[0].Licenseno1;
                    dcpst.Agentdisplayphone = AdDetails.ROW_DATA[0].Agentdisplayphone;
                    dcpst.Agentcity = AdDetails.ROW_DATA[0].Agentcity;
                    dcpst.Agentzipcode = AdDetails.ROW_DATA[0].Agentzipcode;
                    dcpst.Agentstatecode = AdDetails.ROW_DATA[0].Agentstatecode;
                    dcpst.Agentcountry = AdDetails.ROW_DATA[0].Agentcountry;
                    dcpst.Agentbusinessname = AdDetails.ROW_DATA[0].Agentbusinessname;
                    dcpst.Agentaddress = AdDetails.ROW_DATA[0].Agentaddress;
                    dcpst.Agentphone = AdDetails.ROW_DATA[0].Agentphone;
                    dcpst.usertype = AdDetails.ROW_DATA[0].Usertype;
                    dcpst.Agentemail = AdDetails.ROW_DATA[0].Agentemail;
                    dcpst.Agentphotourl = AdDetails.ROW_DATA[0].Agentphotourl;
                    dcpst.Agentadordertype = AdDetails.ROW_DATA[0].Agentadordertype;
                    dcpst.Packageexpiredate = AdDetails.ROW_DATA[0].Packageexpiredate;
                    dcpst.Packageexpiredays = AdDetails.ROW_DATA[0].Packageexpiredays;
                    dcpst.Agentmetros = AdDetails.ROW_DATA[0].Agentmetros;
                    dcpst.availablitytable = AdDetails.ROW_DATA[0].Availability;
                    dcpst.Amount = AdDetails.ROW_DATA[0].Amount;
                    dcpst.Discountamt = AdDetails.ROW_DATA[0].Discountamt;
                    dcpst.Paymentamt = AdDetails.ROW_DATA[0].Paymentamt;
                    dcpst.Couponcode = AdDetails.ROW_DATA[0].Couponcode;
                    dcpst.addonamount = AdDetails.ROW_DATA[0].Addonamount;
                    dcpst.Addontype = AdDetails.ROW_DATA[0].Addontype;
                    dcpst.Categoryflag = AdDetails.ROW_DATA[0].Categoryflag;
                    dcpst.Businessorigin = AdDetails.ROW_DATA[0].Businessorigin;
                    dcpst.Adsourceurl = AdDetails.ROW_DATA[0].Adsourceurl;
                    dcpst.Packageid = AdDetails.ROW_DATA[0].Packageid;
                    dcpst.virtualnanny = AdDetails.ROW_DATA[0].Virtualnanny;
                    dcpst.ismyneed = "1";
                    
                    await currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Package(dcpst));
                }
                else if (addata.services == "Local Services")
                {
                    await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Posting.PrimePakage(addata.oldclpostid, addata.adid));
                }
                //else if (addata.services == "Jobs")
                //{
                //    ljpst.mode = "completepayment";
                //    jobsedit(addata);
                //}
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }

        }
        public async void jobsedit(MyNeedData addata)
        {
            try
            {
                dialog.ShowLoading("", null);
                var AdDetailsAPI = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                var AdDetails = await AdDetailsAPI.getadDetails(addata.oldclpostid, "1", Commonsettings.UserEmail, addata.services);
                ljpst.adid = AdDetails.ROW_DATA[0].Adid.ToString();
                ljpst.isagent = AdDetails.ROW_DATA[0].Isagent;
                ljpst.xml_photopath = AdDetails.ROW_DATA[0].XmlPhotopath;
                ljpst.imagepath = AdDetails.ROW_DATA[0].Imagepath;
                ljpst.html_photopath = AdDetails.ROW_DATA[0].HtmlPhotopath;
                ljpst.Photoscnt = AdDetails.ROW_DATA[0].Photoscnt;
                ljpst.Ispaid = AdDetails.ROW_DATA[0].Ispaid;
                ljpst.Category = AdDetails.ROW_DATA[0].Category;
                ljpst.Subcategory = AdDetails.ROW_DATA[0].Subcategory;
                ljpst.Adtype = AdDetails.ROW_DATA[0].Adtype;
                ljpst.Supercategoryid = AdDetails.ROW_DATA[0].Supercategoryid;
                ljpst.Primarycategoryid = AdDetails.ROW_DATA[0].Primarycategoryid;
                ljpst.Secondarycategoryid = AdDetails.ROW_DATA[0].Secondarycategoryid;
                ljpst.Tags = AdDetails.ROW_DATA[0].Tags;
                ljpst.Supercategoryvalue = AdDetails.ROW_DATA[0].Supercategoryvalue;
                ljpst.Primarycategoryvalue = AdDetails.ROW_DATA[0].Primarycategoryvalue;
                ljpst.Secondarycategoryvalue = AdDetails.ROW_DATA[0].Secondarycategoryvalue;

                ljpst.bizname = AdDetails.ROW_DATA[0].Businessname;
                ljpst.Companyweburl = AdDetails.ROW_DATA[0].Companyweburl;
                ljpst.Supertagid = AdDetails.ROW_DATA[0].Supertagid;
                ljpst.Bestindustry = AdDetails.ROW_DATA[0].Bestindustry;
                ljpst.minsalary = AdDetails.ROW_DATA[0].Minsalary;
                ljpst.maxsalary = AdDetails.ROW_DATA[0].Maxsalary;
                ljpst.Qualification = AdDetails.ROW_DATA[0].Qualification;
                ljpst.Visastatus = AdDetails.ROW_DATA[0].Visastatus;

                ljpst.Experience = AdDetails.ROW_DATA[0].Experience;
                ljpst.Experiencefrom = AdDetails.ROW_DATA[0].Experiencefrom;
                ljpst.Experienceto = AdDetails.ROW_DATA[0].Experienceto;
                ljpst.Noofopening = AdDetails.ROW_DATA[0].Noofopening;
                ljpst.employmenttype = AdDetails.ROW_DATA[0].Employmenttype;
                //if(!string.IsNullOrEmpty(AdDetails.ROW_DATA[0].Jobtype))
                //{
                //    ljpst.jobtype = Convert.ToInt32(AdDetails.ROW_DATA[0].Jobtype);//jobtype is same as primarycategoryval
                //}
                ljpst.roleid = AdDetails.ROW_DATA[0].Jobroleid;
                ljpst.rolename = AdDetails.ROW_DATA[0].Jobrole;
                ljpst.Isresumeavail = AdDetails.ROW_DATA[0].Isresumeavail;
                ljpst.Hideaddress = AdDetails.ROW_DATA[0].Hideaddress;
                ljpst.Linkedinurl = AdDetails.ROW_DATA[0].Linkedinurl;
                ljpst.salmode = AdDetails.ROW_DATA[0].Salarymode;
                ljpst.salmodeid = AdDetails.ROW_DATA[0].Salarymodeid;
                ljpst.bizaddress = AdDetails.ROW_DATA[0].Streetname;
                ljpst.zipcode = AdDetails.ROW_DATA[0].Zipcode;
                ljpst.lat = AdDetails.ROW_DATA[0].Lat;
                ljpst.longitude = AdDetails.ROW_DATA[0].Long;
                ljpst.newcityurl = AdDetails.ROW_DATA[0].Newcityurl;
                ljpst.city = AdDetails.ROW_DATA[0].City;
                ljpst.state = AdDetails.ROW_DATA[0].State;
                ljpst.country = AdDetails.ROW_DATA[0].Country;
                ljpst.title = AdDetails.ROW_DATA[0].Title;
                ljpst.Titleurl = AdDetails.ROW_DATA[0].Titleurl;
                ljpst.desc = AdDetails.ROW_DATA[0].Description.Replace("</p>", "").Replace("<p>", "");
                ljpst.username = AdDetails.ROW_DATA[0].Contributor;
                ljpst.Status = AdDetails.ROW_DATA[0].Status;
                ljpst.Displayphone = AdDetails.ROW_DATA[0].Displayphone;
                ljpst.Mailerstatus = AdDetails.ROW_DATA[0].Mailerstatus;
                ljpst.Ordertype = AdDetails.ROW_DATA[0].Ordertype;
                //ljpst.cus = AdDetails.ROW_DATA[0].Customerid;
                ljpst.Campaignend = AdDetails.ROW_DATA[0].Campaignend;
                ljpst.username = AdDetails.ROW_DATA[0].Postedby;
                ljpst.Enddate = AdDetails.ROW_DATA[0].Enddate;
                ljpst.Numberofdays = AdDetails.ROW_DATA[0].Numberofdays;
                ljpst.useremail = AdDetails.ROW_DATA[0].Email;
                ljpst.usermobileno = AdDetails.ROW_DATA[0].Phone;
                ljpst.Startdate = AdDetails.ROW_DATA[0].Startdate;
                ljpst.Amount = AdDetails.ROW_DATA[0].Amount;
                ljpst.Folderid = AdDetails.ROW_DATA[0].Folderid;

                ljpst.contrycode = AdDetails.ROW_DATA[0].Countrycode;
                ljpst.ismobileverified = AdDetails.ROW_DATA[0].Ismobileverified;
                ljpst.statecode = AdDetails.ROW_DATA[0].Statecode;
                ljpst.Metro = AdDetails.ROW_DATA[0].Metro;
                ljpst.Metrourl = AdDetails.ROW_DATA[0].Metrourl;
                ljpst.Supercategoryvalueurl = AdDetails.ROW_DATA[0].Supercategoryvalueurl;
                ljpst.Primarycategoryvalueurl = AdDetails.ROW_DATA[0].Primarycategoryvalueurl;
                ljpst.Secondarycategoryvalueurl = AdDetails.ROW_DATA[0].Secondarycategoryvalueurl;
                ljpst.longitude = AdDetails.ROW_DATA[0].Long;
                if (!string.IsNullOrEmpty(AdDetails.ROW_DATA[0].Industryid))
                {
                    ljpst.functionalareaid = Convert.ToInt32(AdDetails.ROW_DATA[0].Industryid);
                }
                ljpst.functionalarea = AdDetails.ROW_DATA[0].Industryname;
                ljpst.Orderid = AdDetails.ROW_DATA[0].Orderid;
                ljpst.Expirydate = AdDetails.ROW_DATA[0].Expirydate;
                ljpst.Noofadcnt = AdDetails.ROW_DATA[0].Noofadcnt;
                ljpst.Adpostedcnt = AdDetails.ROW_DATA[0].Adpostedcnt;
                ljpst.Responsemail = AdDetails.ROW_DATA[0].Responsemail;
                ljpst.Gender = AdDetails.ROW_DATA[0].Gender;
                ljpst.disclosedbusiness = AdDetails.ROW_DATA[0].Disclosedbusiness;
                ljpst.Couponcode = AdDetails.ROW_DATA[0].Couponcode;
                ljpst.Companytype = AdDetails.ROW_DATA[0].Companytype;
                ljpst.Bannerstatus = AdDetails.ROW_DATA[0].Bannerstatus;
                ljpst.Banneramount = AdDetails.ROW_DATA[0].Banneramount;
                ljpst.categoryflag = AdDetails.ROW_DATA[0].Categoryflag;
                ljpst.Freeadsflag = AdDetails.ROW_DATA[0].Freeadsflag;
                ljpst.Islisting = AdDetails.ROW_DATA[0].Islisting;
                ljpst.Numberofdays = AdDetails.ROW_DATA[0].Numberofdays;
                ljpst.Addonamount = AdDetails.ROW_DATA[0].Addonamount;
                ljpst.Emailblastamount = AdDetails.ROW_DATA[0].Emailblastamount;
                ljpst.Bonusdays = AdDetails.ROW_DATA[0].Bonusdays;
                ljpst.Bonusamount = AdDetails.ROW_DATA[0].Bonusamount;
                ljpst.Pendingpaycouponcode = AdDetails.ROW_DATA[0].Pendingpaycouponcode;
                ljpst.Benefits = AdDetails.ROW_DATA[0].Benefits;
                ljpst.ismyneed = "1";
                dialog.HideLoading();
                var currentpage = GetCurrentPage();
                if (ljpst.mode=="renew")//|| ljpst.mode== "completepayment"
                {
                    await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Packages(ljpst));
                }
                else if(ljpst.mode=="upgrade")
                {
                    await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Jobdetails(ljpst));
                }
                else
                {
                    await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Jobtype(ljpst));
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private List<DayCare.Features.Post.Models.careservicedata> _agegrouplstdata;
        public List<DayCare.Features.Post.Models.careservicedata> agegrouplstdata
        {
            get { return _agegrouplstdata; }
            set { _agegrouplstdata = value; OnPropertyChanged(nameof(agegrouplstdata)); }
        }
        public async void getagegroupdata(string agegroup)
        {
            try
            {
                //  dialog.ShowLoading("", null);
                await Task.Delay(500);
                var dcapi = RestService.For<DayCare.Features.Post.Interface.IDaycareposting>(Commonsettings.DaycareAPI);
                DayCare.Features.Post.Models.careservice agegroupdata = await dcapi.getagegroup();
                agegrouplstdata = agegroupdata.ROW_DATA;
                string[] agetypelst = agegroup.Split(',');
                string test = "";
                foreach (var data in agegrouplstdata)
                {
                    //foreach(var tdata in agetypelst)
                    //{
                    //    if(data.primarytag == tdata)
                    //    {

                    //    }
                    //}
                    if(data.primarytag==agegroup)
                    {
                        data.primarytag = data.primarytag.Replace("&amp", "").Replace("&amp;", "").Replace("amp", "").Replace("amp;", "");
                        test = data.primarytagid.ToString() + "::" + data.primarytag + "::" + data.primarytagurl;
                    }
                }
                dcpst.agegroups += test;
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }

        }
        public void Lsaddetails(MyNeedData data)
        {
           try
            {
                dialog.ShowLoading("", null);
                foreach (var lst in myneedLSlistdata)
                {
                    if (lst.adid == data.adid)
                    {
                        if (lst.LSAdimg == "PlusGrey.png")
                        {
                            lst.LSAdimg = "minusGrey.png";
                            lst.LSNeedData = true;
                            isexpanddatavisible = 1;
                        }
                        else
                        {
                            lst.LSAdimg = "PlusGrey.png";
                            lst.LSNeedData = false;
                            isexpanddatavisible = 0;
                        }
                    }
                    else
                    {
                        lst.LSAdimg = "PlusGrey.png";
                        lst.LSNeedData = false;
                    }
                }

                var currentpage = GetCurrentPage();
                HVScrollGridView LSlistdata = currentpage.FindByName<HVScrollGridView>("myneedLSlist");
                LSlistdata.ItemsSource = null;
                LSlistdata.ItemsSource = myneedLSlistdata;
                OnPropertyChanged(nameof(myneedLSlistdata));
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public static int isexpanddatavisible = 0;
        private bool _Isbusy = false;
        public bool Isbusy
        {
            get { return _Isbusy; }
            set { _Isbusy = value; OnPropertyChanged(nameof(Isbusy)); }
        }
        
        public  void RMadDetails(MyNeedData rmdata)
        {
          
            try
            {
                    dialog.ShowLoading("", null);
                    //Isbusy = true;


                    foreach (var lst in myneedRMlistdata)
                    {
                        if (lst.adid == rmdata.adid)
                        {
                            if (lst.LSAdimg == "PlusGrey.png")
                            {
                                lst.LSAdimg = "minusGrey.png";
                                lst.LSNeedData = true;
                                isexpanddatavisible = 1;
                            }
                            else
                            {
                                lst.LSAdimg = "PlusGrey.png";
                                lst.LSNeedData = false;
                                isexpanddatavisible = 0;
                            }
                        }
                        else
                        {
                            lst.LSAdimg = "PlusGrey.png";
                            lst.LSNeedData = false;
                        }
                    }
                    var currentpage = GetCurrentPage();
                    HVScrollGridView RMlistdata = currentpage.FindByName<HVScrollGridView>("myneedRMlist");
                    RMlistdata.ItemsSource = null;
                    RMlistdata.ItemsSource = myneedRMlistdata;
                    OnPropertyChanged(nameof(myneedRMlistdata));
                    // Isbusy = false;
                    dialog.HideLoading();
                
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
         //   dialog.HideLoading();
        }
        public void RTadDetails(MyNeedData rtdata)
        {
            try
            {
                dialog.ShowLoading("", null);
                foreach (var lst in myneedRTlistdata)
                {
                    if (lst.adid == rtdata.adid)
                    {
                        if (lst.LSAdimg == "PlusGrey.png")
                        {

                            lst.LSAdimg = "minusGrey.png";
                            lst.LSNeedData = true;
                            isexpanddatavisible = 1;
                        }
                        else
                        {
                            lst.LSAdimg = "PlusGrey.png";
                            lst.LSNeedData = false;
                            isexpanddatavisible = 0;
                        }
                    }
                    else
                    {
                        lst.LSAdimg = "PlusGrey.png";
                        lst.LSNeedData = false;
                    }
                }

                var currentpage = GetCurrentPage();
                HVScrollGridView RTlistdata = currentpage.FindByName<HVScrollGridView>("myneedRTlist");
                RTlistdata.ItemsSource = null;
                RTlistdata.ItemsSource = myneedRTlistdata;
                OnPropertyChanged(nameof(myneedRTlistdata));
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public void DCadDetails(MyNeedData rtdata)
        {
            try
            {
                dialog.ShowLoading("", null);
                foreach (var lst in myneedDClistdata)
                {
                    if (lst.adid == rtdata.adid)
                    {
                        if (lst.LSAdimg == "PlusGrey.png")
                        {
                            lst.LSAdimg = "minusGrey.png";
                            lst.LSNeedData = true;
                            isexpanddatavisible = 1;
                        }
                        else
                        {
                            lst.LSAdimg = "PlusGrey.png";
                            lst.LSNeedData = false;
                            isexpanddatavisible = 0;
                        }
                    }
                    else
                    {
                        lst.LSAdimg = "PlusGrey.png";
                        lst.LSNeedData = false;
                    }
                }

                var currentpage = GetCurrentPage();
                HVScrollGridView DClistdata = currentpage.FindByName<HVScrollGridView>("myneedDClist");
                DClistdata.ItemsSource = null;
                DClistdata.ItemsSource = myneedDClistdata;
                OnPropertyChanged(nameof(myneedDClistdata));
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public void JobsadDetails(MyNeedData rtdata)
        {
            try
            {
                dialog.ShowLoading("", null);
                foreach (var lst in myneedJobslistdata)
                {
                    if ((lst.adid == rtdata.adid && !string.IsNullOrEmpty(lst.LSAdimg) && lst.adid!="0" )|| lst.oldclpostid==rtdata.oldclpostid)
                    {
                        if (lst.LSAdimg == "PlusGrey.png")
                        {
                            lst.LSAdimg = "minusGrey.png";
                            lst.LSNeedData = true;
                            isexpanddatavisible = 1;
                        }
                        else
                        {
                            lst.LSAdimg = "PlusGrey.png";
                            lst.LSNeedData = false;
                            isexpanddatavisible = 0;
                        }
                    }
                    else
                    {
                        lst.LSAdimg = "PlusGrey.png";
                        lst.LSNeedData = false;
                    }
                }

                var currentpage = GetCurrentPage();
                HVScrollGridView Jobslistdata = currentpage.FindByName<HVScrollGridView>("myneedJobslist");
                Jobslistdata.ItemsSource = null;
                Jobslistdata.ItemsSource = myneedJobslistdata;
                OnPropertyChanged(nameof(myneedJobslistdata));
                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public void ExpandRM_Needs()
        {
            try
            {
                dialog.ShowLoading("",null) ;
                if(myneedRMlistdata!=null)
                {
                    if (myneedRMlistdata.Count > 0)
                    {
                        if (Roommatesimgnum == 0)
                        {
                            Roommatesimgnum = 1;
                            RMimg = "minusGrey.png";
                            RMblkvisible = true;
                        }
                        else
                        {
                            Roommatesimgnum = 0;
                            RMimg = "downarrowGrey.png";
                            RMblkvisible = false;
                        }
                    }
                    else
                    {
                        if (Roommatesimgnum == 0)
                        {
                            Roommatesimgnum = 1;
                            RMimg = "minusGrey.png";
                            NoAdsRM = true;
                        }
                        else
                        {
                            Roommatesimgnum = 0;
                            RMimg = "downarrowGrey.png";
                            NoAdsRM = false;
                        }
                    }
                }
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public void ExpandRT_Needs()
        {
            try
            {
                dialog.ShowLoading("", null);
                if(myneedRTlistdata!=null)
                {
                    if (myneedRTlistdata.Count > 0)
                    {
                        if (Rentalsimgnum == 0)
                        {
                            Rentalsimgnum = 1;
                            RTimg = "minusGrey.png";
                            Rentalsblkvisible = true;
                        }
                        else
                        {
                            Rentalsimgnum = 0;
                            RTimg = "downarrowGrey.png";
                            Rentalsblkvisible = false;
                        }
                    }
                    else
                    {
                        if (Rentalsimgnum == 0)
                        {
                            Rentalsimgnum = 1;
                            RTimg = "minusGrey.png";
                            NoAdsRT = true;
                        }
                        else
                        {
                            Rentalsimgnum = 0;
                            RTimg = "downarrowGrey.png";
                            NoAdsRT = false;
                        }
                    }
                }
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public void ExpandLS_Needs()
        {
            if(myneedLSlistdata.Count>0)
            {
                if (LSimgnum == 0)
                {
                    LSimgnum = 1;
                    LSimg = "minusGrey.png";
                    LSblkvisible = true;
                    // getlocalserviceaddetails();
                }
                else
                {
                    LSimgnum = 0;
                    LSimg = "downarrowGrey.png";
                    LSblkvisible = false;
                }
            }
            else
            {
                if (LSimgnum == 0)
                {
                    LSimgnum = 1;
                    LSimg = "minusGrey.png";
                    NoAdsLS = true;
                }
                else
                {
                    LSimgnum = 0;
                    LSimg = "downarrowGrey.png";
                    NoAdsLS = false;
                }
            }
        }
        public void ExpandDC_Needs()
        {
            try
            {
                if (myneedDClistdata.Count > 0)
                {
                    if (Daycareimgnum == 0)
                    {
                        Daycareimgnum = 1;
                        DCimg = "minusGrey.png";
                        Daycareblkvisible = true;
                    }
                    else
                    {
                        Daycareimgnum = 0;
                        DCimg = "downarrowGrey.png";
                        Daycareblkvisible = false;
                    }
                }
                else
                {
                    if (Daycareimgnum == 0)
                    {
                        Daycareimgnum = 1;
                        DCimg = "minusGrey.png";
                        NoAdsDC = true;
                    }
                    else
                    {
                        Daycareimgnum = 0;
                        DCimg = "downarrowGrey.png";
                        NoAdsDC = false;
                    }
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public void ExpandJobs_Needs()
        {
            try
            {

                if (myneedJobslistdata.Count > 0)
                {
                    if (Jobsimgnum == 0)
                    {
                        Jobsimgnum = 1;
                        Jobsimg = "minusGrey.png";
                        Jobsblkvisible = true;
                    }
                    else
                    {
                        Jobsimgnum = 0;
                        Jobsimg = "downarrowGrey.png";
                        Jobsblkvisible = false;
                    }
                }
                else
                {
                    if (Jobsimgnum == 0)
                    {
                        Jobsimgnum = 1;
                        Jobsimg = "minusGrey.png";
                        NoAdsJobs = true;
                    }
                    else
                    {
                        Jobsimgnum = 0;
                        Jobsimg = "downarrowGrey.png";
                        NoAdsJobs = false;
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
        public async void getlocalserviceaddetails(string adid)
        {
            dialog.ShowLoading("");
            var LSNeeds = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
            var lsadData = await LSNeeds.GetLSadDetails(Commonsettings.UserPid, "1"); //"41545357"
            if (lsadData !=null )
            {
                if(lsadData.ROW_DATA.Count>0)
                {
                    foreach (var list in lsadData.ROW_DATA)
                    {
                        if (adid == list.adid && isexpanddatavisible == 1)
                        {
                            list.LSNeedData = true;
                            list.LSAdimg = "minusGrey.png";
                        }
                        else
                        {
                            list.LSNeedData = false;
                            list.LSAdimg = "PlusGrey.png";
                        }
                        list.AdTitlevisible = true;
                        if (!string.IsNullOrEmpty(list.unreadcount) && list.responses != "0" && list.unreadcount != "0")
                        {
                            list.responses = list.responses + " " + "(" + list.unreadcount + " New)";
                        }
                        list.location = list.city + " ," + list.statecode;
                        if (list.adtype == "0")
                        {
                            list.adtype = "Offered";
                        }
                        else
                        {
                            list.adtype = "Wanted";
                        }

                        if (list.adstatus == "0" && list.premiumad == "1")
                        {
                            list.disablevisible = true;
                            list.enablevisible = false;
                            list.editvisible = true;
                            list.upgradevisible = false;
                            list.renewvisible = true;
                            list.paymentvisible = false;
                        }
                        else if (list.adstatus == "0" && list.premiumad == "0")
                        {
                            list.disablevisible = true;
                            list.enablevisible = false;
                            list.editvisible = true;
                            list.upgradevisible = false;
                            list.renewvisible = true;
                            list.paymentvisible = false;
                        }
                        else if (list.adstatus == "11" && list.premiumad == "1")
                        {
                            list.disablevisible = false;
                            list.enablevisible = false;
                            list.editvisible = false;
                            list.upgradevisible = false;
                            list.renewvisible = true;
                            list.paymentvisible = false;
                        }
                        else if (list.adstatus == "11" && list.premiumad == "0")
                        {
                            list.disablevisible = false;
                            list.enablevisible = false;
                            list.editvisible = false;
                            list.upgradevisible = true;
                            list.renewvisible = false;
                            list.paymentvisible = false;
                        }
                        else if (list.adstatus == "1")
                        {
                            list.disablevisible = false;
                            list.enablevisible = true;
                            list.editvisible = false;
                            list.upgradevisible = false;
                            list.renewvisible = false;
                            list.paymentvisible = false;
                        }
                        else if (list.adstatus == "6")
                        {
                            list.disablevisible = false;
                            list.enablevisible = false;
                            list.editvisible = false;
                            list.upgradevisible = false;
                            list.renewvisible = false;
                            list.paymentvisible = true;
                        }
                        else if (list.adstatus == "7")
                        {
                            list.nooptionvisible = true;
                        }
                    }
                    myneedLSlistdata = lsadData.ROW_DATA;
                    var currentpage = GetCurrentPage();
                    HVScrollGridView LSlistdata = currentpage.FindByName<HVScrollGridView>("myneedLSlist");
                    LSlistdata.ItemsSource = lsadData.ROW_DATA;
                    OnPropertyChanged(nameof(myneedLSlistdata));
                }
                else
                {
                    myneedLSlistdata = lsadData.ROW_DATA;
                }
            }
            if (adid != "1")
            {
                dialog.HideLoading();
            }

            // dialog.HideLoading();
        }
        public async void getroommatesaddetails(string adid)
        {
           // dialog.ShowLoading("");
          try
            {
                var RMNeeds = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                var rmadData = await RMNeeds.GetRMadDetails(Commonsettings.UserPid, "1");//"41545357"
                if (rmadData != null)
                {
                    if (rmadData.ROW_DATA.Count > 0)
                    {
                        foreach (var list in rmadData.ROW_DATA)
                        {
                            list.AdTitlevisible = true;
                            if (!string.IsNullOrEmpty(list.unreadcount) && list.responses != "0" && list.unreadcount!="0")
                            {
                                list.unreadcountvisible = true;
                                list.unreadcount = list.unreadcount + " New";
                            }
                            else
                            {
                                list.unreadcountvisible = false;
                                list.unreadcount = "";
                            }
                            if (adid == list.adid && isexpanddatavisible == 1)
                            {
                                list.LSNeedData = true;
                                list.LSAdimg = "minusGrey.png";
                            }
                            else
                            {
                                list.LSNeedData = false;
                                list.LSAdimg = "PlusGrey.png";
                            }

                            list.location = list.city + " ," + list.statecode;
                            if (list.adtype == "1")
                            {
                                list.adtype = "Offered";
                            }
                            else
                            {
                                list.adtype = "Wanted";
                            }
                            list.responses = list.responses;// + " Response";
                            list.responsevisible = true;
                            if (list.adstatus == "0" && list.premiumad == "1")
                            {
                                list.disablevisible = true;
                                list.enablevisible = false;
                                list.editvisible = true;
                                list.upgradevisible = false;
                                list.renewvisible = true;
                                list.paymentvisible = false;
                                list.sendverificationmailvisible = false;
                                list.viewadvisible = true;
                                list.premiumadreceiptvisible = true;
                                list.adstatusvisible = true;
                                list.adstatustxt = "Live";
                            }
                            else if (list.adstatus == "0" && list.premiumad == "0")
                            {
                                list.disablevisible = true;
                                list.enablevisible = false;
                                list.editvisible = true;
                                list.upgradevisible = true;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                                list.sendverificationmailvisible = false;
                                list.viewadvisible = true;
                                list.premiumadreceiptvisible = false;
                                list.adstatusvisible = true;
                                list.adstatustxt = "Live";
                            }
                            else if (list.adstatus == "11" && list.premiumad == "1")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = true;
                                list.paymentvisible = false;
                                list.sendverificationmailvisible = false;
                                list.viewadvisible = false;
                                list.premiumadreceiptvisible = true;
                                list.adstatusvisible = true;
                                list.adstatustxt = "Expired";
                            }
                            else if (list.adstatus == "11" && list.premiumad == "0")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = true;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                                list.sendverificationmailvisible = false;
                                list.viewadvisible = false;
                                list.premiumadreceiptvisible = false;
                                list.adstatusvisible = true;
                                list.adstatustxt = "Expired";
                            }
                            else if (list.adstatus == "1")
                            {
                                list.disablevisible = false;
                                list.enablevisible = true;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                                list.sendverificationmailvisible = false;
                                list.viewadvisible = false;
                                list.adstatusvisible = false;
                            }
                            else if (list.adstatus == "6")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = false;
                                list.paymentvisible = true;
                                list.sendverificationmailvisible = false;
                                list.viewadvisible = false;
                                list.premiumadreceiptvisible = false;
                                list.adstatusvisible = false;
                            }
                            else if (list.adstatus == "5")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                                list.sendverificationmailvisible = true;
                                list.viewadvisible = false;
                                list.premiumadreceiptvisible = false;
                                list.adstatusvisible = false;
                            }
                            else if (list.adstatus == "7")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = false;
                                list.nooptionvisible = true;
                                list.paymentvisible = false;
                                list.sendverificationmailvisible = false;
                                list.viewadvisible = false;
                                list.premiumadreceiptvisible = false;
                                list.responsevisible = false;
                                list.adstatusvisible = false;
                            }
                        }
                        myneedRMlistdata = rmadData.ROW_DATA;
                        var currentpage = GetCurrentPage();
                        HVScrollGridView RMlistdata = currentpage.FindByName<HVScrollGridView>("myneedRMlist");
                        RMlistdata.ItemsSource = rmadData.ROW_DATA;
                        OnPropertyChanged(nameof(myneedRMlistdata));
                    }
                    else
                    {
                        myneedRMlistdata = rmadData.ROW_DATA;
                    }
                }
                if (adid != "1")
                {
                    dialog.HideLoading();
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void getrentalsaddetails(string adid)
        {
          //  dialog.ShowLoading("");
            try
            {
                var RTNeeds = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                var rtadData = await RTNeeds.GetRTadDetails(Commonsettings.UserPid, "1");//"41545357"
                if (rtadData != null)
                {
                    if (rtadData.ROW_DATA.Count > 0)
                    {
                        foreach (var list in rtadData.ROW_DATA)
                        {
                            list.AdTitlevisible = true;
                            if (!string.IsNullOrEmpty(list.unreadcount) && list.responses != "0" && list.unreadcount != "0")
                            {
                                list.unreadcountvisible = true;
                                list.unreadcount = list.unreadcount + " New";
                            }
                            else
                            {
                                list.unreadcountvisible = false;
                                list.unreadcount = "";
                            }
                            if (adid == list.adid && isexpanddatavisible == 1)
                            {
                                list.LSNeedData = true;
                                list.LSAdimg = "minusGrey.png";
                            }
                            else
                            {
                                list.LSNeedData = false;
                                list.LSAdimg = "PlusGrey.png";
                            }
                            list.location = list.city + " ," + list.statecode;
                            if (list.adtype == "1")
                            {
                                list.adtype = "Offered";
                            }
                            else
                            {
                                list.adtype = "Wanted";
                            }
                            list.responsevisible = true;
                            if (list.adstatus == "0" && list.premiumad == "1")
                            {
                                list.disablevisible = true;
                                list.enablevisible = false;
                                list.editvisible = true;
                                list.upgradevisible = false;
                                list.renewvisible = true;
                                list.paymentvisible = false;
                                list.sendverificationmailvisible = false;
                                list.viewadvisible = true;
                                list.premiumadreceiptvisible = true;
                                list.adstatusvisible = true;
                                list.adstatustxt = "Live";
                            }
                            else if (list.adstatus == "0" && list.premiumad == "0")
                            {
                                list.disablevisible = true;
                                list.enablevisible = false;
                                list.editvisible = true;
                                list.upgradevisible = true;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                                list.sendverificationmailvisible = false;
                                list.viewadvisible = true;
                                list.premiumadreceiptvisible = false;
                                list.adstatusvisible = true;
                                list.adstatustxt = "Live";
                            }
                            else if (list.adstatus == "11" && list.premiumad == "1")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = true;
                                list.paymentvisible = false;
                                list.sendverificationmailvisible = false;
                                list.viewadvisible = false;
                                list.premiumadreceiptvisible = true;
                                list.adstatusvisible = true;
                                list.adstatustxt = "Expired";
                            }
                            else if (list.adstatus == "11" && list.premiumad == "0")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = true;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                                list.sendverificationmailvisible = false;
                                list.viewadvisible = false;
                                list.premiumadreceiptvisible = false;
                                list.adstatusvisible = true;
                                list.adstatustxt = "Expired";
                            }
                            else if (list.adstatus == "1")
                            {
                                list.disablevisible = false;
                                list.enablevisible = true;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                                list.sendverificationmailvisible = false;
                                list.viewadvisible = false;
                                list.adstatusvisible = false;
                            }
                            else if (list.adstatus == "6")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = false;
                                list.paymentvisible = true;
                                list.sendverificationmailvisible = false;
                                list.viewadvisible = false;
                                list.premiumadreceiptvisible = false;
                                list.adstatusvisible = false;
                            }
                            else if (list.adstatus == "5")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                                list.sendverificationmailvisible = true;
                                list.viewadvisible = false;
                                list.premiumadreceiptvisible = false;
                                list.adstatusvisible = false;
                            }
                            else if (list.adstatus == "7")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = false;
                                list.nooptionvisible = true;
                                list.paymentvisible = false;
                                list.sendverificationmailvisible = false;
                                list.viewadvisible = false;
                                list.premiumadreceiptvisible = false;
                                list.responsevisible = false;
                                list.adstatusvisible = false;
                            }
                        }
                        myneedRTlistdata = rtadData.ROW_DATA;
                        var currentpage = GetCurrentPage();
                        HVScrollGridView RTlistdata = currentpage.FindByName<HVScrollGridView>("myneedRTlist");
                        RTlistdata.ItemsSource = rtadData.ROW_DATA;
                        OnPropertyChanged(nameof(myneedRTlistdata));
                    }
                    else
                    {
                        myneedRTlistdata = rtadData.ROW_DATA;
                    }
                }
                if (adid != "1")
                {
                    dialog.HideLoading();
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void getdaycareaddetails(string adid)
        {
           // dialog.ShowLoading("");
           try
            {
                var DCNeeds = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                var dcadData = await DCNeeds.GetDCadDetails(Commonsettings.UserPid, "1");//"41545357"
                if (dcadData != null)
                {
                    if (dcadData.ROW_DATA.Count > 0)
                    {
                        foreach (var list in dcadData.ROW_DATA)
                        {
                            list.AdTitlevisible = true;
                            if (!string.IsNullOrEmpty(list.unreadcount) && list.responses != "0" && list.unreadcount != "0")
                            {
                                list.unreadcountvisible = true;
                                list.unreadcount = list.unreadcount + " New";
                            }
                            else
                            {
                                list.unreadcountvisible = false;
                                list.unreadcount = "";
                            }
                            if (adid == list.adid && isexpanddatavisible == 1)
                            {
                                list.LSNeedData = true;
                                list.LSAdimg = "minusGrey.png";
                            }
                            else
                            {
                                list.LSNeedData = false;
                                list.LSAdimg = "PlusGrey.png";
                            }

                            list.location = list.city + " ," + list.statecode;
                            if (list.adtype == "1" || list.adtype == "0" || list.adtype == "2")
                            {
                                list.adtype = "Offered";
                            }
                            else if(list.adtype == "3")
                            {
                                list.adtype = "Wanted";
                            }

                            if (list.adstatus == "0" && list.premiumad == "1")
                            {
                                list.disablevisible = true;
                                list.enablevisible = false;
                                list.editvisible = true;
                                list.upgradevisible = false;
                                list.renewvisible = true;
                                list.paymentvisible = false;
                                list.sendverificationmailvisible = false;
                                list.viewadvisible = true;
                                list.nooptionvisible = false;
                                list.premiumadreceiptvisible = true;
                                list.adstatusvisible = true;
                                list.adstatustxt = "Live";
                            }
                            else if (list.adstatus == "0" && list.premiumad == "0")
                            {
                                list.disablevisible = true;
                                list.enablevisible = false;
                                list.editvisible = true;
                                list.upgradevisible = true;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                                list.viewadvisible = true;
                                list.nooptionvisible = false;
                                list.premiumadreceiptvisible = false;
                                list.adstatusvisible = true;
                                list.adstatustxt = "Live";
                            }
                            else if (list.adstatus == "11" && list.premiumad == "1")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = true;
                                list.paymentvisible = false;
                                list.nooptionvisible = false;
                                list.premiumadreceiptvisible = true;
                                list.adstatusvisible = true;
                                list.adstatustxt = "Expired";
                            }
                            else if (list.adstatus == "11" && list.premiumad == "0")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = true;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                                list.nooptionvisible = false;
                                list.premiumadreceiptvisible = false;
                                list.adstatusvisible = true;
                                list.adstatustxt = "Expired";
                            }
                            else if (list.adstatus == "1")
                            {
                                list.disablevisible = false;
                                list.enablevisible = true;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                                list.nooptionvisible = false;
                                list.premiumadreceiptvisible = false;
                                list.adstatusvisible = false;
                            }
                            else if (list.adstatus == "6")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = true;
                                list.paymentvisible = false;
                                list.nooptionvisible = false;
                                list.premiumadreceiptvisible = false;
                                list.adstatusvisible = false;
                            }
                            else if (list.adstatus == "2"|| list.adstatus == "3")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                                list.nooptionvisible = false;
                                list.premiumadreceiptvisible = false;
                                list.adstatusvisible = false;
                            }
                            else if (list.adstatus == "7")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                                list.nooptionvisible = true;
                                list.premiumadreceiptvisible = false;
                                list.adstatusvisible = false;
                            }
                        }
                        myneedDClistdata = dcadData.ROW_DATA;
                        var currentpage = GetCurrentPage();
                        HVScrollGridView DClistdata = currentpage.FindByName<HVScrollGridView>("myneedDClist");
                        DClistdata.ItemsSource = dcadData.ROW_DATA;
                        OnPropertyChanged(nameof(myneedDClistdata));
                    }
                    else
                    {
                        myneedDClistdata = dcadData.ROW_DATA;
                    }
                }
                if (adid != "1")
                {
                    dialog.HideLoading();
                }
            }
            catch(Exception ex)
            {

            }
        }
        public async void getjobsdetails(string adid)
        {
            //dialog.ShowLoading("");
           try
            {
                var jobNeeds = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                var jobsadData = await jobNeeds.getjobsneeds(Commonsettings.UserPid, "1");//"41545357"
                if (jobsadData != null)
                {
                    if (jobsadData.ROW_DATA.Count > 0)
                    {
                        foreach (var list in jobsadData.ROW_DATA)
                        {
                            list.AdTitlevisible = true;
                            if (!string.IsNullOrEmpty(list.unreadcount) && list.responses != "0" && list.unreadcount != "0")
                            {
                                list.unreadcountvisible = true;
                                list.unreadcount = list.unreadcount + " New";
                            }
                            else
                            {
                                list.unreadcountvisible = false;
                                list.unreadcount = "";
                            }
                            if(list.adstatus=="6")
                            {
                                list.adidvisible = false;
                            }
                            else
                            {
                                list.adidvisible = true;
                            }
                            if (adid == list.adid && isexpanddatavisible == 1)
                            {
                                list.LSNeedData = true;
                                list.LSAdimg = "minusGrey.png";
                            }
                            else
                            {
                                list.LSNeedData = false;
                                list.LSAdimg = "PlusGrey.png";
                            }
                            list.location = list.city + " ," + list.statecode;
                            if (list.adtype == "1")
                            {
                                list.adtype = "Offered";
                            }
                            else
                            {
                                list.adtype = "Wanted";
                            }
                            list.responsevisible = true;
                            if (list.adstatus == "0" && list.premiumad == "1")
                            {
                                list.disablevisible = true;
                                list.enablevisible = false;
                                list.editvisible = true;
                                list.upgradevisible = true;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                                list.viewadvisible = true;
                                list.premiumadreceiptvisible = true;
                                list.nooptionvisible = false;
                                list.adstatusvisible = true;
                                list.adstatustxt = "Live";
                            }
                            else if (list.adstatus == "0" && list.premiumad == "0")
                            {
                                list.disablevisible = true;
                                list.enablevisible = false;
                                list.editvisible = true;
                                list.upgradevisible = false;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                                list.viewadvisible = true;
                                list.premiumadreceiptvisible = false;
                                list.nooptionvisible = false;
                                list.adstatusvisible = true;
                                list.adstatustxt = "Live";
                            }
                            else if (list.adstatus == "11" && list.premiumad == "1")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = true;
                                list.paymentvisible = false;
                                list.viewadvisible = true;
                                list.premiumadreceiptvisible = true;
                                list.nooptionvisible = false;
                                list.adstatusvisible = true;
                                list.adstatustxt = "Expired";
                            }
                            else if (list.adstatus == "11" && list.premiumad == "0")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = true;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                                list.viewadvisible = false;
                                list.premiumadreceiptvisible = false;
                                list.nooptionvisible = false;
                                list.adstatusvisible = false;
                            }
                            else if (list.adstatus == "1")
                            {
                                list.disablevisible = false;
                                list.enablevisible = true;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                                list.viewadvisible = false;
                                list.premiumadreceiptvisible = true;
                                list.nooptionvisible = false;
                                list.adstatusvisible = false;
                            }
                            else if (list.adstatus == "6")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = false;
                                list.paymentvisible = true;
                                //list.LSAdimg = "";
                                list.nooptionvisible = false;
                                list.adstatusvisible = false;
                            }
                            else if (list.adstatus == "7")
                            {
                                list.disablevisible = false;
                                list.enablevisible = false;
                                list.editvisible = false;
                                list.upgradevisible = false;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                                list.LSAdimg = "";
                                list.nooptionvisible = true;
                                list.responsevisible = false;
                                list.adstatusvisible = false;
                            }
                        }
                        myneedJobslistdata = jobsadData.ROW_DATA;
                        var currentpage = GetCurrentPage();
                        HVScrollGridView Jobslistdata = currentpage.FindByName<HVScrollGridView>("myneedJobslist");
                        Jobslistdata.ItemsSource = jobsadData.ROW_DATA;
                        OnPropertyChanged(nameof(myneedJobslistdata));
                    }
                    else
                    {
                        myneedJobslistdata = jobsadData.ROW_DATA;
                    }
                }
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }

        public async void Removepartialad(MyNeedpartialData data)
        {
            try
            {
                dialog.ShowLoading("");
                var Removepartial = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                var RemovepartialData = await Removepartial.RemoveLSpartialadDetails(Commonsettings.UserPid);
                if (RemovepartialData != null)
                {
                    try
                    {
                        var LSPartialclass = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                        var PartialclassdData = await LSPartialclass.GetLSpartialadDetails(Commonsettings.UserPid); //"41545357"
                        if (PartialclassdData != null && PartialclassdData.ROW_DATA != null)
                        {
                            foreach (var item in PartialclassdData.ROW_DATA)
                            {
                                if (item.stepno == "3")
                                {
                                    item.paymentvisible = true;
                                }
                                else
                                {
                                    item.finishlistvisible = true;
                                }
                            }

                            myneedLSpartiallistdata = PartialclassdData.ROW_DATA;
                            var currentpage = GetCurrentPage();
                            HVScrollGridView LSlistdata = currentpage.FindByName<HVScrollGridView>("myneedLSpartiallist");
                            LSlistdata.ItemsSource = PartialclassdData.ROW_DATA;
                            OnPropertyChanged(nameof(myneedLSpartiallistdata));
                            LSblkpartialadvisible = true;
                        }
                        else
                        {
                            myneedLSpartiallistdata = null;
                            var currentpage = GetCurrentPage();
                            HVScrollGridView LSlistdata = currentpage.FindByName<HVScrollGridView>("myneedLSpartiallist");
                            LSlistdata.ItemsSource = null;
                            LSblkpartialadvisible = false;
                        }
                    }
                    catch (Exception r)
                    {

                    }
                }
                dialog.HideLoading();
            }
            catch (Exception e)
            {

            }

        }

        public async void Selectpartialadpayment(MyNeedpartialData data)
        {
            try
            {
                if (data.stepno == "3")
                {
                    var currentpage = GetCurrentPage();
                    LocalService.Features.ViewModels.LS_ViewModel.isrenewclickble = 0;
                    await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Posting.PrimePakage(data.adid, ""));
                }
                else if (data.stepno == "2")
                {
                    var currentpage = GetCurrentPage();
                    var partialbusiness = RestService.For<LocalService.Features.Interfaces.IServiceAPI>(Commonsettings.TechjobsAPI);
                    var result = await partialbusiness.GetPartialaddetails(data.adid);
                    if (result != null && result.ROW_DATA.Count > 0)
                    {
                        //LS_Posting_VM(string stagid, string stag, string ptag, string ptagid, string checkvalue, string checktext,string oldclpostid="")
                        string stagid = result.ROW_DATA[0].supertagid;
                        string stag = result.ROW_DATA[0].supertag;
                        string ptag = result.ROW_DATA[0].primarytag;
                        string ptagid = result.ROW_DATA[0].primarytagid;
                        string checkvalue = result.ROW_DATA[0].tagids;
                        string checktext = result.ROW_DATA[0].tags;

                        await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Posting.LS_Posting(stagid, stag, ptag, ptagid, checkvalue, checktext, data.adid));
                    }
                }
                else if (data.stepno == "1")
                {
                    var currentpage = GetCurrentPage();

                    await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.LS_Primarytags(2, data.adid));
                }

            }
            catch (Exception e)
            {

            }

        }
        private void OnPropertyChanged([CallerMemberName] string propertyName=null)
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
