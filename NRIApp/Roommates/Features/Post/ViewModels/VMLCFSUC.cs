using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using NRIApp.Roommates.Features.Post.Interface;
using NRIApp.Roommates.Features.Post.Models;
using NRIApp.Roommates.Features.Post.Views;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Refit;
using NRIApp.Helpers;
using Acr.UserDialogs;
using NRIApp.Roommates.Features.List.Interface;
using Plugin.Messaging;
using NRIApp.Roommates.Features.List.Views;

namespace NRIApp.Roommates.Features.Post.ViewModels
{
   public class VMLCFSUC : INotifyPropertyChanged
    {
        IUserDialogs dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public Command LCFSClose { get; set; }
        public Command Listofdata { get; set; }
        public Command<Top_Business_Data> Mailcommand { get; set; }
        public Command TapSimilarAds { get; set; }
        public Command<Top_Business_Data> TapOnTitle { get; set; }
        private List<Top_Business_Data> _Successlist;
        string passcategoryurl = "";
        string passcityurl = "";
      
        public List<Top_Business_Data> Successlist
        {
            get { return _Successlist; }
            set
            {
                _Successlist = value;
                OnPropertyChanged(nameof(Successlist));
            }
        }
        private bool _isvisiblermdata = false;
        public bool IsVisibleRMdata
        {
            get { return _isvisiblermdata; }
            set { _isvisiblermdata = value; OnPropertyChanged(); }
        }
        private string _lbltitle;
        public string lbltitle
        {
            get { return _lbltitle; }
            set
            {
                _lbltitle = value;
                OnPropertyChanged(nameof(lbltitle));
            }
        }
        private string _lblsuccess;
        public string lblsuccess
        {
            get { return _lblsuccess; }
            set { _lblsuccess = value;OnPropertyChanged(nameof(lblsuccess)); }
        }
        private string _imgsuccess;
        public string imgsuccess
        {
            get { return _imgsuccess; }
            set { _imgsuccess = value; OnPropertyChanged(nameof(imgsuccess)); }
        }
        private bool _IsBusy = false;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { _IsBusy = value; OnPropertyChanged(); }
        }
        private bool _IsLCFData = false;
        public bool IsLCFData
        {
            get { return _IsLCFData; }
            set { _IsLCFData = value; OnPropertyChanged(); }
        }
        bool _IsLCFNoData = false;
        public bool IsLCFNoData
        {
            get { return _IsLCFNoData; }
            set { _IsLCFNoData = value; OnPropertyChanged(); }
        }
        private string _passadid;
        public string Passadid
        {
            get { return _passadid; }
            set { _passadid = value;OnPropertyChanged(); }
        }
        private bool _postsuccessVisible=false;
        public bool postsuccessVisible
        {
            get { return _postsuccessVisible; }
            set { _postsuccessVisible = value;OnPropertyChanged(); }
        }
        private string _topbusinesscategory;
        public string topbusinesscategory
        {
            get { return _topbusinesscategory; }
            set { _topbusinesscategory = value;OnPropertyChanged(); }
        }
        private bool _similaradsvisible = false;
        public bool similaradsvisible
        {
            get { return _similaradsvisible; }
            set { _similaradsvisible = value; OnPropertyChanged(nameof(similaradsvisible)); }
        }
        public string locationurl = "";
        public static int topbusinesscount;
        public VMLCFSUC(string result, int allrespid, Response res, string cityurl )
        {
            Task.Delay(200);
            if (result == "success" && allrespid > 0)
            {
                lblsuccess = result;
                imgsuccess = "SuccessBg.png";
                postsuccessVisible = true;
                if (res.adid == "" || res.adid == null || res.adid == "0")
                {
                    BindTopBusiness(res, cityurl, result);
                }
                else
                {
                    IsLCFNoData = true;
                    lblsuccess = result;
                    imgsuccess = "SuccessBg.png";
                    IsLCFData = false;
                    IsVisibleRMdata = false;
                }
            }
            else
            {
                CountTopbusiness(res, cityurl, result);
            }
            
            LCFSClose = new Command(async()=>await ClickLCFSClose(res));
            //passcategoryurl = category.ToLower().Replace(" ","-");
            passcategoryurl =res.primarycategoryvalue;
            passcityurl = cityurl;
            Listofdata = new Command(async () => await ClickListofdata(res));
            TapOnTitle = new Command<Top_Business_Data>(TapTitle);
            Mailcommand = new Command<Top_Business_Data>(Taptomail);
            TapSimilarAds = new Command(async () => await Taptogetsimilarads(res));

        }
       public async void Gotoroommateslistings(Response res)
        {
            res.accomodate = "";
            res.attachedbaths = "";
            res.stayperiod = "";
            res.petpolicy = "999";
            res.furnishing = "999";
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new Category(res, locationurl));
        }
        public async void CountTopbusiness(Response res, string cityurl, string result)
        {
           try
            {
                    await Task.Delay(100);
                    var Topbusapi = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);

                    if (res.primarycategoryvalue == "2")
                        topbusinesscategory = "single room";
                    else if (res.primarycategoryvalue == "1")
                        topbusinesscategory = "shared room";
                    else if (res.primarycategoryvalue == "4")
                        topbusinesscategory = "paying guest";
                    else //added for myneeds page
                        topbusinesscategory = res.primarycategoryvalue;
                if (string.IsNullOrEmpty(res.gender))
                {
                    res.gender = "any";
                }
                if (string.IsNullOrEmpty(res.Movedate))
                    {
                        res.Movedate = DateTime.Today.ToString();
                    }
                    Top_Business responsetopbus = await Topbusapi.gettopbusiness(topbusinesscategory, res.Movedate, res.gender, cityurl);
                    topbusinesscount = responsetopbus.ROW_DATA.Count;
                    if (topbusinesscount > 0)
                    {
                        imgsuccess = "SuccessBg.png";
                    //lblsuccess = "You have already posted your need";
                    if(res.ismyneed=="1")
                    {
                        lblsuccess = "Ad Successfully Updated";
                    }
                    else
                    {
                        lblsuccess = "Already posted";
                    }
                        
                        lbltitle = "Find matching Ads";
                        IsVisibleRMdata = true;
                        IsLCFData = true;
                        IsLCFNoData = false;
                        Successlist = responsetopbus.ROW_DATA;
                    }
                    else
                    {
                        IsVisibleRMdata = false;
                        IsLCFData = false;
                        IsLCFNoData = true;
                        lblsuccess = "Already posted"; 
                        imgsuccess = "SuccessBg.png";
                    }
            }
            catch(Exception ex)
            {

            }
        }
        public async Task Taptogetsimilarads(Response categoryList)
        {
            Sortby_RM.orderby = "";
            categoryList.orderby = "date";
            Filter_RM.priceto = "";
            locationurl = categoryList.Newcityurl;
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new Category(categoryList, locationurl));
        }
        public async void TapTitle(Top_Business_Data data)
        {
            Passadid = data.adid.ToString();
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Roommates.Features.List.Views.DetailRMList(Passadid, passcityurl));
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
        public async Task  ClickLCFSClose(Response res)
        {
            res.accomodate = "";
            res.attachedbaths = "";
            res.stayperiod = "";
            res.petpolicy = "999";
            res.furnishing = "999";
            res.pricefrom = "";
            res.ExpRent = "";
            var curpage = GetCurrentPage();
            await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.List.Views.Category(res, passcityurl));
        }
       
        public async void BindTopBusiness(Response res, string cityurl,string result)
        {
           try
            {
                dialog.ShowLoading("");
                var Topbusapi = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);

                if (res.primarycategoryvalue == "2")
                    topbusinesscategory = "single room";
                else if (res.primarycategoryvalue == "1")
                    topbusinesscategory = "shared room";
                else if (res.primarycategoryvalue == "4")
                    topbusinesscategory = "paying guest";
                else //added for myneeds page
                    topbusinesscategory = res.primarycategoryvalue;
                if (string.IsNullOrEmpty(res.Movedate))
                {
                    res.Movedate = DateTime.Today.ToString();
                }
                if(string.IsNullOrEmpty(res.gender))
                {
                    res.gender = "any";
                }
                Top_Business responsetopbus = await Topbusapi.gettopbusiness(topbusinesscategory, res.Movedate, res.gender, cityurl);
               // Passadid = responsetopbus.ROW_DATA[0].adid.ToString();
                if (responsetopbus.ROW_DATA != null && responsetopbus.ROW_DATA.Count > 0)
                {
                    dialog.ShowLoading("");
                    res.pageno = 1;
                 
                    if (responsetopbus.ROW_DATA.Count > 0)
                    {
                        IsLCFData = true;
                        IsLCFNoData = false;
                        lbltitle = "Find matching Ads";
                        await Task.Delay(1000);
                        Successlist = responsetopbus.ROW_DATA;
                        IsVisibleRMdata = true;
                    }
                    else
                    {
                        IsLCFNoData = true;
                        lblsuccess = result;
                        imgsuccess = "SuccessBg.png";
                        IsLCFData = false;
                        IsVisibleRMdata = false;
                    }
                }
                else
                {
                    IsVisibleRMdata = false;
                    IsLCFData = false;
                    IsLCFNoData = true;
                    imgsuccess = "successBg.png";
                }
                dialog.HideLoading();
            }
            catch(Exception e)
            {
                dialog.HideLoading();
            }
        }
        public void Taptomail(Top_Business_Data data)
        {
            //var phonecall = CrossMessaging.Current.PhoneDialer;
            //if (phonecall.CanMakePhoneCall)
            //    phonecall.MakePhoneCall(lead.mobileno, null);
            Passadid = data.adid.ToString();
            var currentpage = GetCurrentPage();
            currentpage.Navigation.PushAsync(new Contact_RM(Passadid));
        }
        public async Task  ClickListofdata(Response res)
        {
            res.accomodate = "";
            res.attachedbaths = "";
            res.stayperiod = "";
            res.petpolicy = "999";
            res.furnishing = "999";
            res.pricefrom = "";
            res.ExpRent = "";
            var curpage = GetCurrentPage();
            await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.List.Views.Category(res, passcityurl));
        }
    }
}
