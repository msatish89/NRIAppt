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

namespace NRIApp.MyNeeds.Features.ViewModel
{
    public class MyNeed_VM : INotifyPropertyChanged
    {
        IUserDialogs dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;

        public List<MyNeedData> myneedLSlistdata { get; set; }
        public List<MyNeedpartialData> myneedLSpartiallistdata { get; set; }
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


        public Command<MyNeedData> ExpandLSAdDetails { get; set; }
        public Command<MyNeedpartialData> ExpandLSPartialAdDetails { get; set; }
        public Command<MyNeedData> ExpandRMAdDetails { get; set; }
        public Command<MyNeedData> ExpandRTAdDetails { get; set; }
        public Command<MyNeedData> EnableAdTap { get; set; }
        public Command<MyNeedData> DisableAdTap { get; set; }
        public Command<MyNeedData> EditAdTap { get; set; }
        public Command<MyNeedData> RenewAdTap { get; set; }

        public Command<MyNeedData> RenewPaymentAdTap { get; set; }
        public Command<MyNeedData> UpgradeAdTap { get; set; }
        public Command<MyNeedData> CPaymentTap { get; set; }
        public Command<MyNeedData> ResponseAdTap { get; set; }
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
            set { _LSAdimg = value; OnPropertyChanged(nameof(LSAdimg)); }
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

        private bool _LSblkpartialadvisible = false;
        public bool LSblkpartialadvisible
        {
            get { return _LSblkpartialadvisible; }
            set { _LSblkpartialadvisible = value; OnPropertyChanged(nameof(LSblkpartialadvisible)); }
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
            set { _LSNeedData = value; OnPropertyChanged(nameof(LSNeedData)); }
        }
        private bool _NoAdsLS = false;
        public bool NoAdsLS
        {
            get { return _NoAdsLS; }
            set { _NoAdsLS = value; OnPropertyChanged(nameof(NoAdsLS)); }
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
        public MyNeed_VM()
        {
            if (!string.IsNullOrEmpty(Commonsettings.UserPid))
            {
                LSimg = "downarrowGrey.png";
                RMimg = "downarrowGrey.png";
                RTimg = "downarrowGrey.png";
                DCimg = "downarrowGrey.png";
                Jobsimg = "downarrowGrey.png";
                LSAdimg = "PlusGrey.png";
                RMAdimg = "downarrowGrey.png";
                RTAdimg = "downarrowGrey.png";
                RM_Tap = new Command(ExpandRM_Needs);
                RT_Tap = new Command(ExpandRT_Needs);
                LS_Tap = new Command(ExpandLS_Needs);
                DC_Tap = new Command(ExpandDC_Needs);
                Jobs_Tap = new Command(ExpandJobs_Needs);

                ExpandLSAdDetails = new Command<MyNeedData>(Lsaddetails);
                ExpandLSPartialAdDetails = new Command<MyNeedpartialData>(Lspartialaddetails);
                ExpandRMAdDetails = new Command<MyNeedData>(RMadDetails);
                ExpandRTAdDetails = new Command<MyNeedData>(RTadDetails);

                EnableAdTap = new Command<MyNeedData>(Enablead);
                DisableAdTap = new Command<MyNeedData>(DisableAd);
                EditAdTap = new Command<MyNeedData>(EditAd);
                RenewAdTap = new Command<MyNeedData>(completepayment);
                RenewPaymentAdTap = new Command<MyNeedData>(RenewAd);
                UpgradeAdTap = new Command<MyNeedData>(UpgradeAd);
                CPaymentTap = new Command<MyNeedData>(completepayment);
                ResponseAdTap = new Command<MyNeedData>(sendresponsepage);
                PartialadremoveTap = new Command<MyNeedpartialData>(Removepartialad);
                PartialPaymentTap = new Command<MyNeedpartialData>(Selectpartialadpayment);
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
        public async void sendresponsepage(MyNeedData addata)
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new MyNeeds.Features.Views.Adresponselist(addata));
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
                else if (addata.services == "Daycare")
                {
                    var EnableAdLS = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                    var EnableAdLSData = await EnableAdLS.EnableDCAd(addata.adid, Commonsettings.UserPid, addata.contributor, addata.oldclpostid, "0");
                    if (EnableAdLSData.resultinformation == "inserted")
                    {
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
                dialog.HideLoading();
            }
            catch (Exception ex)
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
                else if (addata.services == "Daycare")
                {
                    var DisableAdLS = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                    var DisableAdLSData = await DisableAdLS.DisableDCAd(addata.adid, Commonsettings.UserPid, addata.contributor, addata.oldclpostid, "1");
                    if (DisableAdLSData.resultinformation == "updated")
                    {
                        getdaycareaddetails(addata.adid);
                        Daycareblkvisible = true;
                    }
                }
                else if (addata.services == "Jobs")
                {
                    var DisableAdLS = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                    var DisableAdLSData = await DisableAdLS.DisableJobsAd(addata.adid, Commonsettings.UserPid, addata.contributor, addata.oldclpostid, "1");
                    if (DisableAdLSData.resultinformation == "updated")
                    {
                        getjobsdetails(addata.adid);
                        Jobsblkvisible = true;
                    }
                }
                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        Roommates.Features.Post.Models.CategoryList catlist = new Roommates.Features.Post.Models.CategoryList();
        public async void EditAd(MyNeedData addata)
        {
            try
            {
                dialog.ShowLoading("", null);
                var currentpage = GetCurrentPage();
                var AdDetailsAPI = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                var AdDetails = await AdDetailsAPI.getadDetails(addata.oldclpostid, "1", Commonsettings.UserEmail, addata.services);
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
                    if (!string.IsNullOrEmpty(AdDetails.ROW_DATA[0].Adtype))
                    {
                        if (AdDetails.ROW_DATA[0].Adtype == "offered")
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
                    if (catlist.licenseno == "new id")
                    {
                        catlist.licenseno = "";
                    }
                    catlist.Companyweburl = AdDetails.ROW_DATA[0].Companyweburl;
                    catlist.url = AdDetails.ROW_DATA[0].Url;
                    catlist.supercategoryid = AdDetails.ROW_DATA[0].Supertagid;
                    catlist.Bestindustry = AdDetails.ROW_DATA[0].Bestindustry;
                    catlist.Minsalary = AdDetails.ROW_DATA[0].Minsalary;
                    //catlist.Qualification = AdDetails.ROW_DATA[0].Maxsalary;
                    catlist.Qualification = AdDetails.ROW_DATA[0].Qualification;
                    catlist.Visastatus = AdDetails.ROW_DATA[0].Visastatus;
                    catlist.Experience = AdDetails.ROW_DATA[0].Experience;
                    catlist.Experiencefrom = AdDetails.ROW_DATA[0].Experiencefrom;
                    catlist.Experienceto = AdDetails.ROW_DATA[0].Experienceto;
                    catlist.Experienceto = AdDetails.ROW_DATA[0].Noofopening;
                    catlist.Employmenttype = AdDetails.ROW_DATA[0].Employmenttype;
                    catlist.Jobtype = AdDetails.ROW_DATA[0].Jobtype;
                    catlist.Jobroleid = AdDetails.ROW_DATA[0].Jobroleid;
                    catlist.Jobrole = AdDetails.ROW_DATA[0].Jobrole;
                    catlist.Isresumeavail = AdDetails.ROW_DATA[0].Isresumeavail;
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
                    catlist.State = catlist.StateName = AdDetails.ROW_DATA[0].State;
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
                    catlist.Industryid = AdDetails.ROW_DATA[0].Industryid;
                    catlist.Industryname = AdDetails.ROW_DATA[0].Industryname;
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
                else if (addata.services == "Daycare")
                {

                }
                else if (addata.services == "Jobs")
                {

                }
                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void RenewAd(MyNeedData addata)
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

        }
        Roommates.Features.Post.Models.Postfirst pst = new Roommates.Features.Post.Models.Postfirst();
        Rentals.Features.Post.Models.RTPostFirst rtpst = new Rentals.Features.Post.Models.RTPostFirst();
        DayCare.Features.Post.Models.Daycareposting dcpst = new DayCare.Features.Post.Models.Daycareposting();
        LocalJobs.Features.Posting.Models.Postingdata ljpst = new LocalJobs.Features.Posting.Models.Postingdata();
        Roommates.Features.Post.Models.CategoryList catlst = new Roommates.Features.Post.Models.CategoryList();
        public async void completepayment(MyNeedData addata)
        {
            //gotopackage page 
            try
            {

                dialog.ShowLoading("");
                var currentpage = GetCurrentPage();
                var AdDetailsAPI = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                var AdDetails = await AdDetailsAPI.getadDetails(addata.oldclpostid, "1", Commonsettings.UserEmail, addata.services);

                if (addata.services == "Roommates")
                {
                    //Package_RM
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
                    pst.Adtypetxt = AdDetails.ROW_DATA[0].Adtype.ToString();
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
                    pst.Companyweburl = AdDetails.ROW_DATA[0].Companyweburl;
                    pst.url = AdDetails.ROW_DATA[0].Url;
                    pst.supercategoryid = AdDetails.ROW_DATA[0].Supertagid;
                    pst.Bestindustry = AdDetails.ROW_DATA[0].Bestindustry;
                    pst.Minsalary = AdDetails.ROW_DATA[0].Minsalary;
                    pst.Qualification = AdDetails.ROW_DATA[0].Maxsalary;
                    pst.Qualification = AdDetails.ROW_DATA[0].Qualification;
                    pst.Visastatus = AdDetails.ROW_DATA[0].Visastatus;
                    pst.Experience = AdDetails.ROW_DATA[0].Experience;
                    pst.Experiencefrom = AdDetails.ROW_DATA[0].Experiencefrom;
                    pst.Experienceto = AdDetails.ROW_DATA[0].Experienceto;
                    pst.Experienceto = AdDetails.ROW_DATA[0].Noofopening;
                    pst.Employmenttype = AdDetails.ROW_DATA[0].Employmenttype;
                    pst.Jobtype = AdDetails.ROW_DATA[0].Jobtype;
                    pst.Jobroleid = AdDetails.ROW_DATA[0].Jobroleid;
                    pst.Jobrole = AdDetails.ROW_DATA[0].Jobrole;
                    pst.Isresumeavail = AdDetails.ROW_DATA[0].Isresumeavail;
                    pst.Hideaddress = AdDetails.ROW_DATA[0].Hideaddress;
                    pst.Linkedinurl = AdDetails.ROW_DATA[0].Linkedinurl;
                    pst.Trimetros = AdDetails.ROW_DATA[0].Trimetros;
                    pst.Nationwide = AdDetails.ROW_DATA[0].Nationwide;
                    pst.Salarymode = AdDetails.ROW_DATA[0].Salarymode;
                    pst.Salarymodeid = AdDetails.ROW_DATA[0].Salarymodeid;
                    pst.Streetname = pst.LocationAddress = AdDetails.ROW_DATA[0].Streetname;
                    pst.Zipcode = AdDetails.ROW_DATA[0].Zipcode;
                    pst.Lat = AdDetails.ROW_DATA[0].Lat;
                    pst.Long = AdDetails.ROW_DATA[0].Long;
                    pst.Newcityurl = AdDetails.ROW_DATA[0].Newcityurl;
                    pst.City = AdDetails.ROW_DATA[0].City;
                    pst.State = AdDetails.ROW_DATA[0].State;
                    pst.Country = AdDetails.ROW_DATA[0].Country;
                    pst.title = AdDetails.ROW_DATA[0].Title;
                    pst.Titleurl = AdDetails.ROW_DATA[0].Titleurl;
                    pst.description = AdDetails.ROW_DATA[0].Description;
                    pst.Contributor = pst.ContactName = AdDetails.ROW_DATA[0].Contributor;
                    pst.Status = AdDetails.ROW_DATA[0].Status;
                    pst.Displayphone = AdDetails.ROW_DATA[0].Displayphone;
                    pst.Mailerstatus = AdDetails.ROW_DATA[0].Mailerstatus;
                    //pst.Userpid = AdDetails.ROW_DATA[0].Userpid;
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
                    pst.Industryid = AdDetails.ROW_DATA[0].Industryid;
                    pst.Industryname = AdDetails.ROW_DATA[0].Industryname;
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
                    rtpst.service = AdDetails.ROW_DATA[0].Service;
                    rtpst.adid = AdDetails.ROW_DATA[0].Adid;
                    //AdDetails.ROW_DATA[0].Adsid;
                    rtpst.agentid = AdDetails.ROW_DATA[0].Agentid;
                    rtpst.Isagent = AdDetails.ROW_DATA[0].Isagent;
                    rtpst.XmlPhotopath = AdDetails.ROW_DATA[0].XmlPhotopath;
                    rtpst.Imagepath = AdDetails.ROW_DATA[0].Imagepath;
                    rtpst.Imagepath = AdDetails.ROW_DATA[0].HtmlPhotopath;
                    rtpst.Photoscnt = AdDetails.ROW_DATA[0].Photoscnt;
                    rtpst.Ispaid = AdDetails.ROW_DATA[0].Ispaid;
                    rtpst.Category = AdDetails.ROW_DATA[0].Category;
                    rtpst.Subcategory = AdDetails.ROW_DATA[0].Subcategory;
                    rtpst.Adtypetxt = AdDetails.ROW_DATA[0].Adtype.ToString();
                    rtpst.Additionalcity = AdDetails.ROW_DATA[0].Additionalcity;
                    rtpst.Citycount = AdDetails.ROW_DATA[0].Citycount;
                    //rtpst.me AdDetails.ROW_DATA[0].Metrototalamount;
                    rtpst.Citytotalamount = AdDetails.ROW_DATA[0].Citytotalamount;
                    rtpst.supercategoryid = AdDetails.ROW_DATA[0].Supercategoryid;
                    rtpst.primarycategoryid = AdDetails.ROW_DATA[0].Primarycategoryid;
                    rtpst.secondarycategoryid = AdDetails.ROW_DATA[0].Secondarycategoryid;
                    rtpst.supercategoryvalue = AdDetails.ROW_DATA[0].Supercategoryvalue;
                    rtpst.primarycategoryvalue = AdDetails.ROW_DATA[0].Primarycategoryvalue;
                    rtpst.secondarycategoryvalue = AdDetails.ROW_DATA[0].Secondarycategoryvalue;
                    rtpst.Tags = AdDetails.ROW_DATA[0].Tags;
                    rtpst.businessname = AdDetails.ROW_DATA[0].Businessname;
                    rtpst.Companyweburl = AdDetails.ROW_DATA[0].Companyweburl;
                    rtpst.url = AdDetails.ROW_DATA[0].Url;
                    rtpst.supercategoryid = AdDetails.ROW_DATA[0].Supertagid;
                    rtpst.Bestindustry = AdDetails.ROW_DATA[0].Bestindustry;
                    rtpst.Minsalary = AdDetails.ROW_DATA[0].Minsalary;
                    rtpst.Qualification = AdDetails.ROW_DATA[0].Maxsalary;
                    rtpst.Qualification = AdDetails.ROW_DATA[0].Qualification;
                    rtpst.Visastatus = AdDetails.ROW_DATA[0].Visastatus;
                    rtpst.Experience = AdDetails.ROW_DATA[0].Experience;
                    rtpst.Experiencefrom = AdDetails.ROW_DATA[0].Experiencefrom;
                    rtpst.Experienceto = AdDetails.ROW_DATA[0].Experienceto;
                    rtpst.Experienceto = AdDetails.ROW_DATA[0].Noofopening;
                    rtpst.Employmenttype = AdDetails.ROW_DATA[0].Employmenttype;
                    rtpst.Jobtype = AdDetails.ROW_DATA[0].Jobtype;
                    rtpst.Jobroleid = AdDetails.ROW_DATA[0].Jobroleid;
                    rtpst.Jobrole = AdDetails.ROW_DATA[0].Jobrole;
                    rtpst.Isresumeavail = AdDetails.ROW_DATA[0].Isresumeavail;
                    rtpst.Hideaddress = AdDetails.ROW_DATA[0].Hideaddress;
                    rtpst.Linkedinurl = AdDetails.ROW_DATA[0].Linkedinurl;
                    rtpst.Trimetros = AdDetails.ROW_DATA[0].Trimetros;
                    rtpst.Nationwide = AdDetails.ROW_DATA[0].Nationwide;
                    rtpst.Salarymode = AdDetails.ROW_DATA[0].Salarymode;
                    rtpst.Salarymodeid = AdDetails.ROW_DATA[0].Salarymodeid;
                    rtpst.Streetname = rtpst.LocationAddress = AdDetails.ROW_DATA[0].Streetname;
                    rtpst.Zipcode = AdDetails.ROW_DATA[0].Zipcode;
                    rtpst.Lat = AdDetails.ROW_DATA[0].Lat;
                    rtpst.Long = AdDetails.ROW_DATA[0].Long;
                    rtpst.Newcityurl = AdDetails.ROW_DATA[0].Newcityurl;
                    rtpst.City = AdDetails.ROW_DATA[0].City;
                    rtpst.State = AdDetails.ROW_DATA[0].State;
                    rtpst.Country = AdDetails.ROW_DATA[0].Country;
                    rtpst.Title = AdDetails.ROW_DATA[0].Title;
                    rtpst.Titleurl = AdDetails.ROW_DATA[0].Titleurl;
                    rtpst.Description = AdDetails.ROW_DATA[0].Description;
                    rtpst.Contributor = rtpst.ContactName = AdDetails.ROW_DATA[0].Contributor;
                    rtpst.Status = AdDetails.ROW_DATA[0].Status;
                    rtpst.Displayphone = AdDetails.ROW_DATA[0].Displayphone;
                    rtpst.Mailerstatus = AdDetails.ROW_DATA[0].Mailerstatus;
                    //rtpst.Userpid = AdDetails.ROW_DATA[0].Userpid;
                    rtpst.Ordertype = AdDetails.ROW_DATA[0].Ordertype;
                    rtpst.Customerid = AdDetails.ROW_DATA[0].Customerid;
                    rtpst.Campaignid = AdDetails.ROW_DATA[0].Campaignid;
                    rtpst.Postedby = AdDetails.ROW_DATA[0].Postedby;
                    rtpst.Enddate = AdDetails.ROW_DATA[0].Enddate;
                    rtpst.Email = rtpst.ContactEmail = AdDetails.ROW_DATA[0].Email;
                    rtpst.Phone = rtpst.ContactPhone = AdDetails.ROW_DATA[0].Phone;
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
                    rtpst.Industryid = AdDetails.ROW_DATA[0].Industryid;
                    rtpst.Industryname = AdDetails.ROW_DATA[0].Industryname;
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
                    rtpst.Emailblastamount = AdDetails.ROW_DATA[0].Emailblastamount;
                    rtpst.Bonusdays = AdDetails.ROW_DATA[0].Bonusdays;
                    rtpst.Bonusamount = AdDetails.ROW_DATA[0].Bonusamount;
                    rtpst.Pendingpaycouponcode = AdDetails.ROW_DATA[0].Pendingpaycouponcode;
                    rtpst.Benefits = AdDetails.ROW_DATA[0].Benefits;
                    rtpst.ismyneed = "1";
                    await currentpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Package_RT(rtpst));
                }
                else if (addata.services == "Local Services")
                {

                    await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Posting.PrimePakage(addata.oldclpostid, addata.adid));
                }
                else if (addata.services == "Daycare")
                {
                    await currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Package(dcpst));
                }
                else if (addata.services == "Jobs")
                {
                    await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Packages(ljpst));
                }
                NRIApp.LocalService.Features.ViewModels.LS_ViewModel.isrenewclickble = 0;
                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }

        }
        public void Lsaddetails(MyNeedData data)
        {
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


        public static int isexpanddatavisible = 0;
        public void RMadDetails(MyNeedData rmdata)
        {
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
        }
        public void RTadDetails(MyNeedData rtdata)
        {
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
        }
        public void ExpandRM_Needs()
        {
            try
            {
                if (myneedRMlistdata != null)
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
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public void ExpandRT_Needs()
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
        public void ExpandLS_Needs()
        {
            if (myneedLSlistdata.Count > 0)
            {
                if (LSimgnum == 0)
                {
                    LSimgnum = 1;
                    LSimg = "minusGrey.png";
                    LSblkpartialadvisible= LSblkvisible = true;
                    
                    // getlocalserviceaddetails();
                }
                else
                {
                    LSimgnum = 0;
                    LSimg = "downarrowGrey.png";
                    LSblkpartialadvisible= LSblkvisible = false;
                    
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
            catch (Exception ex)
            {

            }
        }
        public async void getlocalserviceaddetails(string adid)
        {
            try
            {
                dialog.ShowLoading("");
                string ispartialclass = "";
                var LSNeeds = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                var lsadData = await LSNeeds.GetLSadDetails(Commonsettings.UserPid, "1"); //"41545357"
                if (lsadData != null)
                {
                    if (lsadData.ROW_DATA.Count > 0)
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
                                list.upgradevisible = true;
                                list.renewvisible = false;
                                list.paymentvisible = false;
                            }
                            else if (list.adstatus == "0" && list.premiumad == "0")
                            {
                                list.disablevisible = true;
                                list.enablevisible = false;
                                list.editvisible = false;

                                list.upgradevisible = true;
                                list.renewvisible = false;
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

                            if (list.partialstepads == "1")
                            {
                                ispartialclass = "1";
                                NRIApp.LocalService.Features.ViewModels.LS_ViewModel.ispartialclass = 1;
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

                    if (!string.IsNullOrEmpty(ispartialclass))
                    {
                        try
                        {
                            var LSPartialclass = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                            var PartialclassdData = await LSNeeds.GetLSpartialadDetails(Commonsettings.UserPid); //"41545357"
                            if (PartialclassdData != null && PartialclassdData.ROW_DATA.Count > 0)
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
                                LSblkpartialadvisible = false;
                            }
                        }
                        catch (Exception r)
                        {

                        }
                    }
                    else
                    {
                        LSblkpartialadvisible = false;
                    }
                }
            }catch(Exception e)
            {

            }
            // dialog.HideLoading();
        }
        public async void getroommatesaddetails(string adid)
        {
            // dialog.ShowLoading("");
            var RMNeeds = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
            var rmadData = await RMNeeds.GetRMadDetails(Commonsettings.UserPid, "1");//"41545357"
            if (rmadData != null)
            {
                if (rmadData.ROW_DATA.Count > 0)
                {
                    foreach (var list in rmadData.ROW_DATA)
                    {
                        list.AdTitlevisible = true;
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

                        if (list.adstatus == "0" && list.premiumad == "1")
                        {
                            list.disablevisible = true;
                            list.enablevisible = false;
                            list.editvisible = true;
                            list.upgradevisible = false;
                            list.renewvisible = false;
                            list.paymentvisible = false;
                        }
                        else if (list.adstatus == "0" && list.premiumad == "0")
                        {
                            list.disablevisible = true;
                            list.enablevisible = false;
                            list.editvisible = true;
                            list.upgradevisible = false;
                            list.renewvisible = false;
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
        public async void getrentalsaddetails(string adid)
        {
            //  dialog.ShowLoading("");
            var RTNeeds = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
            var rtadData = await RTNeeds.GetRTadDetails(Commonsettings.UserPid, "1");//"41545357"
            if (rtadData != null)
            {
                if (rtadData.ROW_DATA.Count > 0)
                {
                    foreach (var list in rtadData.ROW_DATA)
                    {
                        list.AdTitlevisible = true;
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

                        if (list.adstatus == "0" && list.premiumad == "1")
                        {
                            list.disablevisible = true;
                            list.enablevisible = false;
                            list.editvisible = true;
                            list.upgradevisible = false;
                            list.renewvisible = false;
                            list.paymentvisible = false;
                        }
                        else if (list.adstatus == "0" && list.premiumad == "0")
                        {
                            list.disablevisible = true;
                            list.enablevisible = false;
                            list.editvisible = true;
                            list.upgradevisible = false;
                            list.renewvisible = false;
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
        public async void getdaycareaddetails(string adid)
        {
            // dialog.ShowLoading("");
            var DCNeeds = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
            var dcadData = await DCNeeds.GetRMadDetails(Commonsettings.UserPid, "1");//"41545357"
            if (dcadData != null)
            {
                if (dcadData.ROW_DATA.Count > 0)
                {
                    foreach (var list in dcadData.ROW_DATA)
                    {
                        list.AdTitlevisible = true;
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
                            list.renewvisible = false;
                            list.paymentvisible = false;
                        }
                        else if (list.adstatus == "0" && list.premiumad == "0")
                        {
                            list.disablevisible = true;
                            list.enablevisible = false;
                            list.editvisible = true;
                            list.upgradevisible = false;
                            list.renewvisible = false;
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
        public async void getjobsdetails(string adid)
        {
            //dialog.ShowLoading("");
            var jobNeeds = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
            var jobsadData = await jobNeeds.getjobsneeds(Commonsettings.UserPid, "1");//"41545357"
            if (jobsadData != null)
            {
                if (jobsadData.ROW_DATA.Count > 0)
                {
                    foreach (var list in jobsadData.ROW_DATA)
                    {
                        list.AdTitlevisible = true;
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
                            list.renewvisible = false;
                            list.paymentvisible = false;
                        }
                        else if (list.adstatus == "0" && list.premiumad == "0")
                        {
                            list.disablevisible = true;
                            list.enablevisible = false;
                            list.editvisible = true;
                            list.upgradevisible = false;
                            list.renewvisible = false;
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
