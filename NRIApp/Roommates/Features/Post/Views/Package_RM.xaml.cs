using NRIApp.Roommates.Features.Post.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Roommates.Features.Post.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Package_RM : ContentPage
    {
        Postfirst myneedpost = new Postfirst();
        public Package_RM(Postfirst pst)
        {
            InitializeComponent();
            myneedpost = pst;
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Choose your package";
            BindingContext = new ViewModels.Package_VM(pst);
        }
        protected override bool OnBackButtonPressed()
        {
            //var currentpage = (NRIApp.Helpers.CoolContentPage)Xamarin.Forms.Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            //if (currentpage?.CustomBackButtonAction != null)
            //{
            //    currentpage?.CustomBackButtonAction.Invoke();
            //}
            //else
            //{
            //    base.OnBackButtonPressed();
            //}

            //return true;

            var currentpage = GetCurrentPage();
            if (lcfsecond.postscuccesbackbtn == 0)
            {
                currentpage.Navigation.PopAsync();
            }
            else if (lcfsecond.postscuccesbackbtn == 1)
            {
                currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                //currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            }
            return true;
        }
        private void Backbutton_Tapped(object obj, EventArgs e)
        {
            var currentpage = GetCurrentPage();
            if (lcfsecond.postscuccesbackbtn == 0)
            {
                currentpage.Navigation.PopAsync();
            }
            else if (lcfsecond.postscuccesbackbtn == 1)
            {
                currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            }
        }
        public Page GetCurrentPage()
        {
            var currentpg = App.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpg;
        }

        private void Edit_Tap(object sender, EventArgs e)
        {
                try
                {
                Roommates.Features.Post.Models.CategoryList catlist = new Roommates.Features.Post.Models.CategoryList();
                var currentpage = GetCurrentPage();
                catlist.pricemode = myneedpost.pricemode;
                catlist.accomodate = myneedpost.accomodate;
                catlist.stayperiod = myneedpost.stayperiod;
                catlist.Rent = myneedpost.rent;
                if (string.IsNullOrEmpty(catlist.Rent))
                {
                    catlist.Rent = myneedpost.ExpRent;
                }
                catlist.attachedbaths = myneedpost.attachedbaths;
                if (string.IsNullOrEmpty(catlist.attachedbaths))
                {
                    catlist.attachedbaths = myneedpost.attachedbathtype;
                }
                catlist.availablefrm = myneedpost.availablefrm;
                if (string.IsNullOrEmpty(catlist.availablefrm))
                {
                    catlist.availablefrm = myneedpost.Movedate;
                }
                catlist.service = myneedpost.service;
                catlist.adid = myneedpost.adid;
                //myneedpost.Adsid;
                catlist.agentid = myneedpost.agentid;
                catlist.Isagent = myneedpost.Isagent;
                catlist.XmlPhotopath = myneedpost.XmlPhotopath;
                catlist.Imagepath = myneedpost.Imagepath;
                //catlist.Imagepath = myneedpost.HtmlPhotopath;
                catlist.Photoscnt = myneedpost.Photoscnt;
                catlist.Ispaid = myneedpost.Ispaid;
                catlist.Category = myneedpost.Category;
                if (string.IsNullOrEmpty(catlist.Category))
                {
                    catlist.Category = myneedpost.RMcategory;
                }
                catlist.Subcategory = myneedpost.Subcategory;
                if (!string.IsNullOrEmpty(myneedpost.Adtype))
                {
                    if (myneedpost.Adtype == "offered")
                    {
                        catlist.adtype = "1";
                    }
                    else
                    {
                        catlist.adtype = "0";
                    }
                }
                else
                {
                    catlist.adtype = myneedpost.adtype;
                }
                //catlist.adtype = myneedpost.Adtype;

                catlist.Additionalcity = myneedpost.Additionalcity;
                catlist.Citycount = myneedpost.Citycount;
                //pst.me myneedpost.Metrototalamount;
                catlist.Citytotalamount = myneedpost.Citytotalamount;
                catlist.supercategoryid = myneedpost.supercategoryid;
                catlist.primarycategoryid = myneedpost.primarycategoryid.ToString();
                catlist.secondarycategoryid = myneedpost.secondarycategoryid;
                catlist.supercategoryvalue = myneedpost.supercategoryvalue;
                catlist.primarycategoryvalue = myneedpost.primarycategoryvalue;
                catlist.secondarycategoryvalue = myneedpost.secondarycategoryvalue;
                catlist.Tags = myneedpost.Tags;
                catlist.businessname = myneedpost.businessname;
                catlist.licenseno = myneedpost.licenseno;
                if (catlist.licenseno == "new id")
                {
                    catlist.licenseno = "";
                }
                catlist.url = myneedpost.url;
                //catlist.supercategoryid = myneedpost.supertagid;
                catlist.Bestindustry = myneedpost.Bestindustry;
                catlist.Hideaddress = myneedpost.Hideaddress;
                catlist.Linkedinurl = myneedpost.Linkedinurl;
                catlist.Trimetros = myneedpost.Trimetros;
                catlist.Nationwide = myneedpost.Nationwide;
                catlist.Salarymode = myneedpost.Salarymode;
                catlist.Salarymodeid = myneedpost.Salarymodeid;
                if (string.IsNullOrEmpty(catlist.Category))
                {
                    catlist.Category = myneedpost.RMcategory;
                }
                catlist.Streetname = catlist.Locationname = myneedpost.Streetname;
                if (string.IsNullOrEmpty(catlist.Streetname))
                {
                    catlist.Streetname = myneedpost.LocationAddress;
                }
                catlist.Zipcode = myneedpost.Zipcode;
                catlist.userlat = myneedpost.Lat;
                catlist.userlong = myneedpost.Long;
                catlist.Newcityurl = myneedpost.Newcityurl;
                catlist.City = myneedpost.City;
                catlist.State = catlist.StateName = myneedpost.State;
                catlist.Country = myneedpost.Country;
                catlist.title = myneedpost.title;
                catlist.Titleurl = myneedpost.Titleurl;
                catlist.description = myneedpost.description;
                catlist.Contributor = catlist.ContactName = myneedpost.Contributor;
                catlist.Status = myneedpost.Status;
                catlist.Displayphone = myneedpost.Displayphone;
                catlist.Mailerstatus = myneedpost.Mailerstatus;
                catlist.Userpid = myneedpost.userpid;
                catlist.Ordertype = myneedpost.Ordertype;
                catlist.Customerid = myneedpost.Customerid;
                catlist.Campaignid = myneedpost.Campaignid;
                catlist.Postedby = myneedpost.Postedby;
                catlist.Enddate = myneedpost.Enddate;
                catlist.Email = catlist.ContactEmail = myneedpost.Email;
                catlist.Phone = catlist.ContactPhone = myneedpost.Phone;
                catlist.Startdate = myneedpost.Startdate;
                catlist.Numberofdays = myneedpost.Numberofdays;
                catlist.Amount = myneedpost.Amount;
                catlist.Folderid = myneedpost.Folderid;
                catlist.Countrycode = myneedpost.Countrycode;
                catlist.Ismobileverified = myneedpost.Ismobileverified;
                catlist.Statecode = myneedpost.StateCode;
                catlist.Metro = myneedpost.Metro;
                catlist.Metrourl = myneedpost.Metrourl;
                catlist.Supercategoryvalueurl = myneedpost.Supercategoryvalueurl;
                catlist.Primarycategoryvalueurl = myneedpost.Primarycategoryvalueurl;
                catlist.Secondarycategoryvalueurl = myneedpost.Secondarycategoryvalueurl;
                catlist.Orderid = myneedpost.Orderid;
                catlist.Expirydate = myneedpost.Expirydate;
                catlist.Noofadcnt = myneedpost.Noofadcnt;
                catlist.Adpostedcnt = myneedpost.Adpostedcnt;
                catlist.Responsemail = myneedpost.Responsemail;
                catlist.Gender = myneedpost.Gender;
                catlist.Disclosedbusiness = myneedpost.Disclosedbusiness;
                catlist.Couponcode = myneedpost.Couponcode;
                catlist.Salespersonemail = myneedpost.Salespersonemail;
                catlist.Companytype = myneedpost.Companytype;
                catlist.Bannerstatus = myneedpost.Bannerstatus;
                catlist.Banneramount = myneedpost.Banneramount;
                catlist.Categoryflag = myneedpost.Categoryflag;
                catlist.Freeadsflag = myneedpost.Freeadsflag;
                catlist.Islisting = myneedpost.Islisting;
                catlist.Addonamount = myneedpost.Addonamount;
                catlist.Emailblastamount = myneedpost.Emailblastamount;
                catlist.Bonusdays = myneedpost.Bonusdays;
                catlist.Bonusamount = myneedpost.Bonusamount;
                catlist.Pendingpaycouponcode = myneedpost.Pendingpaycouponcode;
                catlist.categoryname = myneedpost.categoryname;
                catlist.Benefits = myneedpost.Benefits;
                catlist.userlat = myneedpost.userlat;
                catlist.userlong = myneedpost.userlat;
                catlist.HideRent = myneedpost.HideRent;
                catlist.ismyneed = "1";
                catlist.ismyneedpayment = "1";
                catlist.usertype = myneedpost.usertype;
                if (string.IsNullOrEmpty(catlist.usertype) && catlist.agentid == 0)
                {
                    catlist.usertype = "1";
                }
                else
                {
                    catlist.usertype = "2";
                }
                //Paymentedit = 1;
                if (string.IsNullOrEmpty(catlist.ContactName) || string.IsNullOrEmpty(catlist.ContactPhone))
                {
                    catlist.ContactName = catlist.Contributor = myneedpost.ContactName;
                    catlist.ContactPhone = catlist.Phone = myneedpost.ContactPhone;
                    catlist.Email = catlist.ContactEmail = myneedpost.ContactEmail;
                }
                //Paymentedit = 1;
                Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.offerwant(catlist));
                }
                catch (Exception ex)
                {
                   // dialog.HideLoading();
                }
        }
    }
}