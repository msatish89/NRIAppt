using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.Rentals.Features.Post.Interface;
using NRIApp.Rentals.Features.Post.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace NRIApp.Rentals.Features.Post.ViewModels
{
    public class Package_VMRT : INotifyPropertyChanged
    {
        // public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;
        public Command<string> TapOnElitePackage { get; set; }
        public Command<string> TapOnPremiumplusPackage { get; set; }
        public Command<string> TapOnPremiumPackage { get; set; }
        public Command<string> TapOnStandardPackage { get; set; }
        public Command TapOnFreePackage { get; set; }
        public Command TapOnMakeChanges { get; set; }

        public string _freepkgdays = "5";
        public string _standardpkgdays = "20";
        public string _standardpkgamnt = "$25";
        public string _premiumpkgdays = "35";
        public string _premiumpkgamnt = "$35";
        public string _premiumpluspkgdays = "40";
        public string _premiumpluspkgamnt = "$45";
        public string _elitepkgdays = "45";
        public string _elitepkgamnt = "$60";
        public string _premiumtotalads = "";
        public string _premiumplustotalads = "";
        public string _stdtotalads = "";
        public bool _freepkgbtn = true;
        public bool _selfreepkgbtn = false;
        public bool _standardpkgbtn = true;
        public bool _selstandardpkgbtn = false;
        public bool _premiumbtn = true;
        public bool _selpremiumbtn = false;
        public bool _premiumplusbtn = true;
        public bool _selpremiumplusbtn = false;
        public bool _elitebtn = true;
        public bool _selelitebtn = false;
        public bool _freepkg = false;
        public bool _standardpkg = false;
        public bool _premiumpkg = false;
        public bool _premiumpluspkg = false;
        public bool _elitepkg = false;
        public bool _featuretlevisible = false;
        public bool _agentvisible = false;
        public bool _nonagentvisible = false;
        public string _elitepkgads = "";

        public string elitepkgads
        {
            get { return _elitepkgads; }
            set
            {
                _elitepkgads = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(elitepkgads)));
            }
        }
        public bool agentvisible
        {
            get { return _agentvisible; }
            set
            {
                _agentvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(agentvisible)));

            }
        }
        public bool nonagentvisible
        {
            get { return _nonagentvisible; }
            set
            {
                _nonagentvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(nonagentvisible)));

            }
        }
        public bool freepkgvisible
        {
            get { return _freepkg; }
            set
            {
                _freepkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(freepkgvisible)));

            }
        }
        public bool standardpkgvisible
        {
            get { return _standardpkg; }
            set
            {
                _standardpkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgvisible)));

            }
        }
        public bool premiumpkgvisible
        {
            get { return _premiumpkg; }
            set
            {
                _premiumpkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpkgvisible)));

            }
        }
        public bool premiumpluspkgvisible
        {
            get { return _premiumpluspkg; }
            set
            {
                _premiumpluspkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpluspkgvisible)));

            }
        }
        public bool elitepkgvisible
        {
            get { return _elitepkg; }
            set
            {
                _elitepkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(elitepkgvisible)));

            }
        }
        public bool freepkgbtn
        {
            get { return _freepkgbtn; }
            set
            {
                _freepkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(freepkgbtn)));

            }
        }
        public bool Selfreepkgbtn
        {
            get { return _selfreepkgbtn; }
            set
            {
                _selfreepkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selfreepkgbtn)));

            }
        }
        public bool standardpkgbtn
        {
            get { return _standardpkgbtn; }
            set
            {
                _standardpkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgbtn)));

            }
        }
        public bool Selstandardpkgbtn
        {
            get { return _selstandardpkgbtn; }
            set
            {
                _selstandardpkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selstandardpkgbtn)));

            }
        }
        public bool premiumbtn
        {
            get { return _premiumbtn; }
            set
            {
                _premiumbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumbtn)));

            }
        }
        public bool Selpremiumbtn
        {
            get { return _selpremiumbtn; }
            set
            {
                _selpremiumbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selpremiumbtn)));

            }
        }
        public bool premiumplusbtn
        {
            get { return _premiumplusbtn; }
            set
            {
                _premiumplusbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumplusbtn)));

            }
        }
        public bool Selpremiumplusbtn
        {
            get { return _selpremiumplusbtn; }
            set
            {
                _selpremiumplusbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selpremiumplusbtn)));

            }
        }
        public bool elitebtn
        {
            get { return _elitebtn; }
            set
            {
                _elitebtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(elitebtn)));

            }
        }
        public bool Selelitebtn
        {
            get { return _selelitebtn; }
            set
            {
                _selelitebtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selelitebtn)));

            }
        }

        public string freepkgdays
        {
            get { return _freepkgdays; }
            set
            {
                _freepkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(freepkgdays)));

            }
        }
        public string standardpkgdays
        {
            get { return _standardpkgdays; }
            set
            {
                _standardpkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgdays)));

            }
        }
        public string standardpkgamnt
        {
            get { return _standardpkgamnt; }
            set
            {
                _standardpkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgamnt)));

            }
        }
        public string premiumpkgdays
        {
            get { return _premiumpkgdays; }
            set
            {
                _premiumpkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpkgdays)));

            }
        }
        public string premiumpkgamnt
        {
            get { return _premiumpkgamnt; }
            set
            {
                _premiumpkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpkgamnt)));

            }
        }
        public string premiumpluspkgdays
        {
            get { return _premiumpluspkgdays; }
            set
            {
                _premiumpluspkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpluspkgdays)));

            }
        }
        public string premiumpluspkgamnt
        {
            get { return _premiumpluspkgamnt; }
            set
            {
                _premiumpluspkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpluspkgamnt)));

            }
        }
        public string elitepkgdays
        {
            get { return _elitepkgdays; }
            set
            {
                _elitepkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(elitepkgdays)));

            }
        }
        public string elitepkgamnt
        {
            get { return _elitepkgamnt; }
            set
            {
                _elitepkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(elitepkgamnt)));

            }
        }
        public List<RTPackages> _pack { get; set; }
        public List<RTPackages> Pack
        {
            get
            {
                return _pack;
            }
            set
            {
                _pack = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pack)));
            }
        }
        public string premiumplustotalads
        {
            get { return _premiumplustotalads; }
            set
            {
                _premiumplustotalads = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumplustotalads)));
            }
        }
        public string premiumtotalads
        {
            get { return _premiumtotalads; }
            set
            {
                _premiumtotalads = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumtotalads)));
            }
        }
        public string stdtotalads
        {
            get { return _stdtotalads; }
            set
            {
                _stdtotalads = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(stdtotalads)));
            }
        }
        public bool featuretlevisible
        {
            get { return _featuretlevisible; }
            set
            {
                _featuretlevisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(featuretlevisible)));
            }
        }
        string adid = "",usertype="";
        static int agentid;
        RTPostFirst pft = new RTPostFirst();

        public Package_VMRT(RTPostFirst pst)
        {
           try
            {
                pft = pst;
                adid = pst.adid.ToString();
                agentid = pst.agentid;
                if(string.IsNullOrEmpty(pst.userpid))
                {
                    pst.userpid = pft.userpid = Commonsettings.UserPid;
                }
                if (string.IsNullOrEmpty(pst.usertype))
                {
                    usertype = "1";
                    pst.usertype = usertype;
                }
                else
                {
                    usertype = pst.usertype;
                }
                TapOnElitePackage = new Command<string>(SelectElitepkg);
                TapOnPremiumplusPackage = new Command<string>(SelectPremiumpluspkg);
                TapOnPremiumPackage = new Command<string>(SelectPremiumpkg);
                TapOnStandardPackage = new Command<string>(SelectStandpkg);
                TapOnFreePackage = new Command(SelectFreepkg);
                TapOnMakeChanges = new Command(Edit_Tap);
                Getpackages(pst.adid, pst.userpid, pst.usertype);
                //if (NRIApp.Rentals.Features.Post.Views.lcfsecond.postsuccessbackbtn == 1)
                if (NRIApp.Rentals.Features.Post.Views.lcfsecond.postsuccessbackbtn == 1 && Commonsettings.UserDeviceOSVersion == "android")
                {
                    var currentpage = GetCurrentPage();
                    currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                }
                //  Getpackages(9923650, "16487447");
            }
            catch (Exception ex)
            {

            }
        }
        private async void Edit_Tap()
        {
            try
            {
                Rentals.Features.Post.Models.RTCategoryList rtcatlist = new Rentals.Features.Post.Models.RTCategoryList();
                // var currentpage = GetCurrentPage();
                rtcatlist.pricemode = pft.pricemode;
                rtcatlist.accomodate = pft.accomodate;
                rtcatlist.stayperiod = pft.stayperiod;
                rtcatlist.Rent = pft.Rent;
                rtcatlist.attachedbaths = pft.attachedbaths;
                rtcatlist.availablefrm = pft.availablefrm;
                rtcatlist.Movedate = pft.Movedate;
                rtcatlist.service = pft.service;
                rtcatlist.adid = pft.adid;
                //myneedpost.Adsid;
                rtcatlist.agentid = pft.agentid;
                rtcatlist.Isagent = pft.Isagent;
                rtcatlist.XmlPhotopath = pft.XmlPhotopath;
                rtcatlist.Imagepath = pft.Imagepath;
                //rtcatlist.Imagepath = myneedpost.HtmlPhotopath;
                rtcatlist.Photoscnt = pft.Photoscnt;
                rtcatlist.Ispaid = pft.Ispaid;
                rtcatlist.Category =rtcatlist.maincategory= pft.Category;
                rtcatlist.Subcategory = pft.Subcategory;
                if (!string.IsNullOrEmpty(pft.Adtype))
                {
                    if (pft.Adtype == "offered")
                    {
                        rtcatlist.adtype = "1";
                    }
                    else
                    {
                        rtcatlist.adtype = "0";
                    }
                }
                rtcatlist.Additionalcity = pft.Additionalcity;
                rtcatlist.Citycount = pft.Citycount;
                rtcatlist.supercategoryid = Convert.ToInt32(pft.supercategoryid);
                rtcatlist.primarycategoryid = pft.primarycategoryid;
                rtcatlist.secondarycategoryid = pft.secondarycategoryid;
                rtcatlist.supercategoryvalue = pft.supercategoryvalue;
                rtcatlist.primarycategoryvalue = pft.primarycategoryvalue;
                rtcatlist.secondarycategoryvalue = pft.secondarycategoryvalue;
                rtcatlist.tertiarycategoryid = Convert.ToInt32(pft.tertiarycategoryid);
                rtcatlist.tertiarycategoryvalue = pft.tertiarycategoryvalue;
                rtcatlist.Tags = pft.Tags;
                rtcatlist.businessname = pft.businessname;
                rtcatlist.licenseno = pft.businessLicenseno;
                if (rtcatlist.licenseno == "new id")
                {
                    rtcatlist.licenseno = "";
                }
                rtcatlist.url = pft.url;
                //rtcatlist.supercategoryid = Convert.ToInt32(myneedpost.supercategoryid);
                rtcatlist.Bestindustry = pft.Bestindustry;
                rtcatlist.Hideaddress = pft.Hideaddress;
                rtcatlist.Trimetros = pft.Trimetros;
                rtcatlist.Nationwide = pft.Nationwide;
                rtcatlist.Salarymode = pft.Salarymode;
                rtcatlist.Salarymodeid = pft.Salarymodeid;
                rtcatlist.Streetname = rtcatlist.Locationname = pft.Streetname;
                rtcatlist.Zipcode = pft.Zipcode;
                rtcatlist.Lat = pft.Lat;
                rtcatlist.Long = pft.Long;
                rtcatlist.Newcityurl = pft.Newcityurl;
                rtcatlist.City = pft.City;
                rtcatlist.State = rtcatlist.StateName = pft.State;
                rtcatlist.Country = pft.Country;
                rtcatlist.title = pft.Title;
                rtcatlist.Titleurl = pft.Titleurl;
                rtcatlist.description = pft.Description;
                rtcatlist.Contributor = rtcatlist.ContactName = pft.Contributor;
                rtcatlist.Status = pft.Status;
                rtcatlist.Displayphone = pft.Displayphone;
                rtcatlist.Mailerstatus = pft.Mailerstatus;
                rtcatlist.Userpid = pft.userpid;
                rtcatlist.Ordertype = pft.Ordertype;
                rtcatlist.Customerid = pft.Customerid;
                rtcatlist.Campaignid = pft.Campaignid;
                rtcatlist.Postedby = pft.Postedby;
                rtcatlist.Enddate = pft.Enddate;
                rtcatlist.Email = rtcatlist.ContactEmail = pft.Email;
                rtcatlist.Phone = rtcatlist.ContactPhone = pft.Phone;
                rtcatlist.Startdate = pft.Startdate;
                rtcatlist.Numberofdays = pft.Numberofdays;
                rtcatlist.Amount = pft.Amount;
                rtcatlist.Folderid = pft.Folderid;
                rtcatlist.Countrycode = pft.Countrycode;
                rtcatlist.Ismobileverified = pft.Ismobileverified;
                rtcatlist.Statecode = pft.StateCode;
                rtcatlist.Metro = pft.Metro;
                rtcatlist.Metrourl = pft.Metrourl;
                rtcatlist.Supercategoryvalueurl = pft.Supercategoryvalueurl;
                rtcatlist.Primarycategoryvalueurl = pft.Primarycategoryvalueurl;
                rtcatlist.Secondarycategoryvalueurl = pft.Secondarycategoryvalueurl;
                rtcatlist.Orderid = pft.Orderid;
                rtcatlist.Expirydate = pft.Expirydate;
                rtcatlist.Noofadcnt = pft.Noofadcnt;
                rtcatlist.Adpostedcnt = pft.Adpostedcnt;
                rtcatlist.Responsemail = pft.Responsemail;
                rtcatlist.Gender = pft.Gender;
                rtcatlist.Disclosedbusiness = pft.Disclosedbusiness;
                rtcatlist.Couponcode = pft.Couponcode;
                rtcatlist.Salespersonemail = pft.Salespersonemail;
                rtcatlist.Companytype = pft.Companytype;
                rtcatlist.Bannerstatus = pft.Bannerstatus;
                rtcatlist.Banneramount = pft.Banneramount;
                rtcatlist.Categoryflag = pft.Categoryflag;
                rtcatlist.Freeadsflag = pft.Freeadsflag;
                rtcatlist.Islisting = pft.Islisting;
                rtcatlist.Addonamount = pft.Addonamount;
                //rtcatlist.Emailblastamount = myneedpost.Emailblastamount;
                rtcatlist.Bonusdays = pft.Bonusdays;
                rtcatlist.Bonusamount = pft.Bonusamount;
                rtcatlist.Pendingpaycouponcode = pft.Pendingpaycouponcode;
                rtcatlist.categoryname = pft.categoryname;
                rtcatlist.Benefits = pft.Benefits;
                rtcatlist.noofbaths = pft.noofbaths;
                rtcatlist.area = pft.area;
                rtcatlist.ismyneed = "1";
                rtcatlist.ismyneedpayment = "1";
                rtcatlist.usertype = pft.usertype;
                if(string.IsNullOrEmpty(pft.usertype)&&pft.agentid==0)
                {
                    pft.usertype = "1";
                }
                else
                {
                    pft.usertype = "2";
                }
                if (string.IsNullOrEmpty(rtcatlist.ContactName) || string.IsNullOrEmpty(rtcatlist.ContactPhone))
                {
                    rtcatlist.ContactName = rtcatlist.Contributor = pft.ContactName;
                    rtcatlist.ContactPhone = rtcatlist.Phone = pft.ContactPhone;
                    rtcatlist.Email = rtcatlist.ContactEmail = pft.ContactEmail;
                }
                if (string.IsNullOrEmpty(rtcatlist.Rent))
                {
                    rtcatlist.Rent = pft.ExpRent;
                }
                if (string.IsNullOrEmpty(rtcatlist.area))
                {
                    rtcatlist.sqrfeet = pft.sqrfeet;
                }
                if (string.IsNullOrEmpty(rtcatlist.Streetname))
                {
                    rtcatlist.Streetname = pft.LocationAddress;
                }
                rtcatlist.title = pft.title;
                rtcatlist.description = pft.description;
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.offerwant(rtcatlist));
            }
            catch (Exception ex)
            {
                  dialog.HideLoading();
            }
        }
        public async void SelectPremiumpluspkg(string amnnt)
        {
            var curpage = GetCurrentPage();
            dialog.ShowLoading("");
            premiumplusbtn = false;
            Selpremiumplusbtn = true;
            premiumbtn = true;
            Selpremiumbtn = false;
            elitebtn = true;
            Selelitebtn = false;
            standardpkgbtn = true;
            Selstandardpkgbtn = false;
            freepkgbtn = true;
            Selfreepkgbtn = false;
            var api = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
            Pkgsucess_RT list = await api.updatepackage(adid.ToString(), premiumpluspkgdays, "Premiumplus",premiumplustotalads,agentid);

            if (list.resultinformation == "updated successfully")
            {
                dialog.HideLoading();
                await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Payment_RT(amnnt, "Premiumplus", adid, pft));
            }
            else
            {
                dialog.HideLoading();
                await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Thankyou());
            }
        }
        public async void SelectPremiumpkg(string amnnt)
        {
           try
            {
                var curpage = GetCurrentPage();
                dialog.ShowLoading("");
                premiumplusbtn = true;
                Selpremiumplusbtn = false;
                premiumbtn = false;
                Selpremiumbtn = true;
                elitebtn = true;
                Selelitebtn = false;
                standardpkgbtn = true;
                Selstandardpkgbtn = false;
                freepkgbtn = true;
                Selfreepkgbtn = false;
                var api = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                Pkgsucess_RT list = await api.updatepackage(adid.ToString(), premiumpkgdays, "Premium", premiumtotalads, agentid);

                if (list.resultinformation == "updated successfully")
                {
                    dialog.HideLoading();
                    await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Payment_RT(amnnt, "Premium", adid, pft));
                }
                else
                {
                    dialog.HideLoading();
                    await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Thankyou());
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void SelectStandpkg(string amnnt)
        {
           try
            {
                var curpage = GetCurrentPage();
                dialog.ShowLoading("");
                premiumplusbtn = true;
                Selpremiumplusbtn = false;
                premiumbtn = true;
                Selpremiumbtn = false;
                elitebtn = true;
                Selelitebtn = false;
                standardpkgbtn = false;
                Selstandardpkgbtn = true;
                freepkgbtn = true;
                Selfreepkgbtn = false;
                var api = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                Pkgsucess_RT list = await api.updatepackage(adid.ToString(), standardpkgdays, "Standard", stdtotalads, agentid);

                if (list.resultinformation == "updated successfully")
                {
                    dialog.HideLoading();
                    await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Payment_RT(amnnt, "Standard", adid, pft));
                }
                else
                {
                    dialog.HideLoading();
                    await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Thankyou());
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void SelectFreepkg()
        {
            try
            {
                var curpage = GetCurrentPage();
                dialog.ShowLoading("");
                var nodays = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                Pkgsucess_RT FreeadDuration = await nodays.getnoofdays(usertype);
                var api = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                Pkgsucess_RT list = await api.updatepackage(adid.ToString(), FreeadDuration.resultinformation, "Free", "", 0);
                if (list.resultinformation == "updated successfully")
                {
                    dialog.HideLoading();
                    //await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Payment_RT("", adid, pft));
                    if (list.isfreeadpublish == "1")
                        await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.postsuccess(adid, usertype, "Free"));
                    else
                        await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Thankyou());
                }
                else
                {
                    dialog.HideLoading();
                    await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Thankyou());
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void Getpackages(int adid, string pid,string usertype)
        {
           try
            {
                dialog.ShowLoading("");
                var api = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                RTPackagelist list = await api.getpackages(adid.ToString(), pid, usertype);
                Pack = list.ROW_DATA;
                if (list.ROW_DATA.Count() > 0)
                {
                    foreach (var i in Pack)
                    {
                        if (i.ordertype == "Free")
                        {
                            freepkgdays = i.noofdays + " days";
                            freepkgvisible = true;
                            featuretlevisible = false;
                        }
                        else if (i.ordertype == "Standard")
                        {
                            standardpkgdays = i.noofdays + " days";
                            standardpkgamnt = "$" + i.amount;
                            standardpkgvisible = true;
                            if (usertype == "2")
                            {
                                stdtotalads = i.featuretitle + " ads";
                                featuretlevisible = true;
                            }
                        }
                        else if (i.ordertype == "Premium")
                        {
                            premiumpkgamnt = "$" + i.amount;
                            premiumpkgdays = i.noofdays + " days";
                            premiumpkgvisible = true;
                            if (usertype == "2")
                            {
                                premiumtotalads = i.featuretitle + " ads";
                                featuretlevisible = true;
                            }
                        }
                        else if (i.ordertype == "Premiumplus")
                        {
                            premiumpluspkgamnt = "$" + i.amount;
                            premiumpluspkgdays = i.noofdays + " days";
                            premiumpluspkgvisible = true;
                            if (usertype == "2")
                            {
                                premiumplustotalads = i.featuretitle + " ads";
                                featuretlevisible = true;
                            }
                        }
                        else if (i.ordertype == "Elite")
                        {
                            elitepkgamnt = "$" + i.amount.ToString();
                            elitepkgdays = i.noofdays + " days";
                            elitepkgvisible = true;
                            featuretlevisible = false;
                            if (usertype == "2")
                            {
                                elitepkgads = i.featuretitle + " ads";
                                featuretlevisible = true;
                            }
                        }
                    }
                    if (usertype == "2")
                    {
                        nonagentvisible = false;
                        agentvisible = true;
                    }
                    else
                    {
                        nonagentvisible = true;
                        agentvisible = false;
                    }
                    dialog.HideLoading();
                }
                else
                    dialog.HideLoading();

                if ((!string.IsNullOrEmpty(pft.Ordertype) && pft.ismyneed == "1" && pft.usertype == "1") || (!string.IsNullOrEmpty(pft.Ordertype) && pft.ismyneedpayment == "1" && pft.usertype == "1"))
                {

                    if (pft.Ordertype == "Standard")
                    {
                        // SelectStandpkg(standardpkgamnt);
                        premiumplusbtn = true;
                        Selpremiumplusbtn = false;
                        premiumbtn = true;
                        Selpremiumbtn = false;
                        elitebtn = true;
                        Selelitebtn = false;
                        standardpkgbtn = false;
                        Selstandardpkgbtn = true;
                        freepkgbtn = true;
                        Selfreepkgbtn = false;
                    }
                    else if (pft.Ordertype == "Premium")
                    {
                        premiumplusbtn = true;
                        Selpremiumplusbtn = false;
                        premiumbtn = false;
                        Selpremiumbtn = true;
                        elitebtn = true;
                        Selelitebtn = false;
                        standardpkgbtn = true;
                        Selstandardpkgbtn = false;
                        freepkgbtn = true;
                        Selfreepkgbtn = false;
                        standardpkgvisible = false;
                    }
                    else if (pft.Ordertype == "Premiumplus")
                    {
                        premiumplusbtn = false;
                        Selpremiumplusbtn = true;
                        premiumbtn = true;
                        Selpremiumbtn = false;
                        elitebtn = true;
                        Selelitebtn = false;
                        standardpkgbtn = true;
                        Selstandardpkgbtn = false;
                        freepkgbtn = true;
                        Selfreepkgbtn = false;
                        standardpkgvisible = false;
                        premiumpkgvisible = false;
                    }
                    else if (pft.Ordertype == "Elite")
                    {
                        premiumplusbtn = true;
                        Selpremiumplusbtn = false;
                        premiumbtn = true;
                        Selpremiumbtn = false;
                        elitebtn = false;
                        Selelitebtn = true;
                        standardpkgbtn = true;
                        Selstandardpkgbtn = false;
                        freepkgbtn = true;
                        Selfreepkgbtn = false;
                        standardpkgvisible = false;
                        premiumpkgvisible = false;
                        premiumpluspkgvisible = false;

                    }

                    if (!string.IsNullOrEmpty(pft.Ordertype) && pft.ismyneed == "1")
                    {
                        freepkgvisible = false;
                    }
                    else
                    {
                        freepkgvisible = true;
                    }
                }
                //if ((!string.IsNullOrEmpty(pft.Ordertype) && pft.ismyneed == "1") || (!string.IsNullOrEmpty(pft.Ordertype) && pft.ismyneedpayment == "1"))
                //{
                //    if (pft.Ordertype == "Standard")
                //    {
                //        // SelectStandpkg(standardpkgamnt);
                //        premiumplusbtn = true;
                //        Selpremiumplusbtn = false;
                //        premiumbtn = true;
                //        Selpremiumbtn = false;
                //        elitebtn = true;
                //        Selelitebtn = false;
                //        standardpkgbtn = false;
                //        Selstandardpkgbtn = true;
                //        freepkgbtn = true;
                //        Selfreepkgbtn = false;
                //    }
                //    else if (pft.Ordertype == "Premium")
                //    {
                //        premiumplusbtn = true;
                //        Selpremiumplusbtn = false;
                //        premiumbtn = false;
                //        Selpremiumbtn = true;
                //        elitebtn = true;
                //        Selelitebtn = false;
                //        standardpkgbtn = true;
                //        Selstandardpkgbtn = false;
                //        freepkgbtn = true;
                //        Selfreepkgbtn = false;
                //        standardpkgvisible = false;
                //    }
                //    else if (pft.Ordertype == "Premiumplus")
                //    {
                //        premiumplusbtn = false;
                //        Selpremiumplusbtn = true;
                //        premiumbtn = true;
                //        Selpremiumbtn = false;
                //        elitebtn = true;
                //        Selelitebtn = false;
                //        standardpkgbtn = true;
                //        Selstandardpkgbtn = false;
                //        freepkgbtn = true;
                //        Selfreepkgbtn = false;
                //        standardpkgvisible = false;
                //        premiumpkgvisible = false;
                //    }
                //    else if (pft.Ordertype == "Elite")
                //    {
                //        premiumplusbtn = true;
                //        Selpremiumplusbtn = false;
                //        premiumbtn = true;
                //        Selpremiumbtn = false;
                //        elitebtn = false;
                //        Selelitebtn = true;
                //        standardpkgbtn = true;
                //        Selstandardpkgbtn = false;
                //        freepkgbtn = true;
                //        Selfreepkgbtn = false;
                //        standardpkgvisible = false;
                //        premiumpkgvisible = false;
                //        premiumpluspkgvisible = false;
                //    }

                //    if (!string.IsNullOrEmpty(pft.Ordertype) && pft.ismyneed == "1" || pft.ismyneed == "1")
                //    {
                //        freepkgvisible = false;
                //    }
                //    else
                //    {
                //        freepkgvisible = true;
                //    }
                //}
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void SelectElitepkg(string amnnt)
        {
            var curpage = GetCurrentPage();
            try
            {
                dialog.ShowLoading("");
                premiumplusbtn = true;
                Selpremiumplusbtn = false;
                premiumbtn = true;
                Selpremiumbtn = false;
                elitebtn = false;
                Selelitebtn = true;
                standardpkgbtn = true;
                Selstandardpkgbtn = false;
                freepkgbtn = true;
                Selfreepkgbtn = false;
                var api = RestService.For<IPaymentRT>(Commonsettings.RoommatesAPI);
                Pkgsucess_RT list = await api.updatepackage(adid.ToString(), elitepkgdays, "Elite","",agentid);

                if (list.resultinformation == "updated successfully")
                {
                    dialog.HideLoading();
                    await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Payment_RT(amnnt, "Elite", adid, pft));
                }
                else
                {
                    dialog.HideLoading();
                    await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Thankyou());
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                dialog.HideLoading();
            }


        }
        public event PropertyChangedEventHandler PropertyChanged;

        public Page GetCurrentPage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}
