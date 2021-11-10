using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using NRIApp.Rentals.Features.Post.Interface;
using NRIApp.Rentals.Features.Post.Models;
using NRIApp.Rentals.Features.Post.Views;
using Refit;
using NRIApp.Helpers;
using Acr.UserDialogs;
using NRIApp.Rentals.Features.List.Interface;
using NRIApp.Rentals.Features.List.Models;
using NRIApp.Rentals.Features.List.Views;
using NRIApp.Roommates.Features.List.Views;

namespace NRIApp.Rentals.Features.Post.ViewModels
{
    public class VMRTLCFSUC : INotifyPropertyChanged
    {
        IUserDialogs dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;

        public Command getrentalListdata { get; set; }
        public Command LCFSClose { get; set; }
        public Command Mailcommand { get; set; }
        public Command TapSimilarAds { get; set; }
        public Command<RTTop_Business_Data> TapOnTitle { get; set; }
        private List<RTTop_Business_Data> _Successlist;

        string rentalcategoryurl = "";
        string rentalcityurl ="";
        
        private bool _isvisiblerentalsdata = false;
        public bool IsVisiblerentalsdata
        {
            get { return _isvisiblerentalsdata; }
            set { _isvisiblerentalsdata = value; OnPropertyChanged(); }
        }
        public List<RTTop_Business_Data> Successlist
        {
            get { return _Successlist; }
            set{_Successlist = value;OnPropertyChanged(nameof(Successlist));}
        }
        private string _lbltitle;
        public string lbltitle
        {
            get { return _lbltitle; }
            set { _lbltitle = value; OnPropertyChanged(nameof(lbltitle)); }
        }
        private string _lblsuccess;
        public string lblsuccess { get { return _lblsuccess; } set { _lblsuccess = value; } }
        private string _imgsuccess;
        public string imgsuccess { get { return _imgsuccess; } set { _imgsuccess = value; } }
        bool _IsBusy = false;
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
        private bool _isvisible = false;
        public bool IsVisibledata
        {
            get { return _isvisible; }
            set { _isvisible = value;OnPropertyChanged(); }
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
        private string _topbusinesscategory;
        public string topbusinesscategory
        {
            get { return _topbusinesscategory; }
            set { _topbusinesscategory = value; OnPropertyChanged(); }
        }
        private bool _similaradsvisible = false;
        public bool similaradsvisible
        {
            get { return _similaradsvisible; }
            set { _similaradsvisible = value;OnPropertyChanged(nameof(similaradsvisible)); }
        }
        public string locationurl = "";
        public static int topbusinesscount;
        public VMRTLCFSUC(string result, int allrespid, RTResponse res, string cityurl)
        {
            try
            {
                res.ExpRent = VMRTLCF.RTpriceto;
                res.pricefrom = VMRTLCF.RTpricefrom;
                locationurl = cityurl;
                if (result == "success" && allrespid > 0)
                {
                    lblsuccess = result;
                    imgsuccess = "SuccessBg.png";
                    if (res.adid == "" || res.adid == null || res.adid=="0")
                    {
                        BindTopBusiness(res, cityurl, result);
                    }
                    else
                    {
                        IsLCFNoData = true;
                        lblsuccess = result;
                        imgsuccess = "SuccessBg.png";
                        IsLCFData = false;
                        IsVisiblerentalsdata = false;
                    }
                }
                else
                {
                    CountTopbusiness(res, cityurl, result);
                }
                
                //rentalcategoryurl = category.ToLower().Replace(" ", "-");
                rentalcategoryurl = res.primarycategoryvalue;
                rentalcityurl = cityurl;
                LCFSClose = new Command(async () => await ClickLCFSClose(res));
                getrentalListdata = new Command(async () => await ClickViewAllRentalList(res));
                TapOnTitle = new Command<RTTop_Business_Data>(TapTitle);
                Mailcommand = new Command(Taptomail);
                TapSimilarAds = new Command(async () =>await Taptogetsimilarads(res));
            }

            catch(Exception e)
            {
                dialog.HideLoading();
            }
        }
        
       public async void GotoRentalListings(RTResponse res)
        {
            var curpage = GetCurrentPage();
            await curpage.Navigation.PushAsync(new RentalList(res, locationurl));
        }
        public async void CountTopbusiness(RTResponse res, string cityurl, string result)
        {
            if (res.primarycategoryvalue == "1")
                topbusinesscategory = "Single Family Home";
            else if (res.primarycategoryvalue == "2")
                topbusinesscategory = "Apartment";
            else if (res.primarycategoryvalue == "3")
                topbusinesscategory = "Condo";
            else if (res.primarycategoryvalue == "4")
                topbusinesscategory = "Office Space";
            else if (res.primarycategoryvalue == "5")
                topbusinesscategory = "Retail Outlet";
            else if (res.primarycategoryvalue == "6")
                topbusinesscategory = "Town house";
            else if (res.primarycategoryvalue == "8")
                topbusinesscategory = "Homes";
            else if (res.primarycategoryvalue == "9")
                topbusinesscategory = "Houses";
            else if (res.primarycategoryvalue == "10")
                topbusinesscategory = "Hostels";
            else if (res.primarycategoryvalue == "11")
                topbusinesscategory = "Hotels";
            else if (res.primarycategoryvalue == "12")
                topbusinesscategory = "Basement Apartment";
            else if (res.primarycategoryvalue == "13")
                topbusinesscategory = "Parking Garage";
            else if (res.primarycategoryvalue == "7")
                topbusinesscategory = "Others";

            res.pageno = 1;

            var Topbusapi = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
            RTTop_Business responsetopbus = await Topbusapi.gettopbusiness(topbusinesscategory, res.Movedate, cityurl);
            topbusinesscount = responsetopbus.ROW_DATA.Count;
            if (topbusinesscount > 0 )
            {
                imgsuccess = "SuccessBg.png";
                //lblsuccess = "You have already posted your need";
                lblsuccess = "Already posted";
                lbltitle = "Find matching Ads";
                IsVisiblerentalsdata = true;
                IsLCFData = true;
                IsLCFNoData = false;
                Successlist = responsetopbus.ROW_DATA;
            }
            else
            {
                IsLCFData = false;
                IsLCFNoData = true;
                IsVisibledata = false;
                imgsuccess = "SuccessBg.png";
                lbltitle = "Find matching Ads";
                result = "You have already responded";
                //lblsuccess = result;
                lblsuccess = "Already posted";
                IsVisiblerentalsdata = false;
            }
        }
        public  async  Task Taptogetsimilarads(RTResponse categoryList)
        {
            Sortby_RM.orderby = "";
            categoryList.orderby = "date";
            Filter_RM.priceto = "";
            locationurl = categoryList.Newcityurl;
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new RentalList(categoryList, locationurl));
        }
        public void Taptomail()
        {
            var currentpage = GetCurrentPage();
            currentpage.Navigation.PushAsync(new Contact_RT(Passadid));
        }
        public async void TapTitle(RTTop_Business_Data data)
        {
            Passadid = data.adid.ToString();
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Rentals.Features.List.Views.DetailRentalList(Passadid, rentalcityurl));
        }

        //public async void getRentaldataList()
        //{
        //    var Listofrentaldata = RestService.For<IListRentalCategory>(Commonsettings.RoommatesAPI);
        //    ListRentalCategory responsedata =await Listofrentaldata.GetRentalList(rentalcategoryurl, rentalcityurl);
        //    if (responsedata.ROW_DATA.Count==0)
        //    {
        //        IsVisibledata = false;
        //    }
        //    else
        //    {
        //        IsVisibledata = true;
        //    }

        //}


        public async Task ClickViewAllRentalList(RTResponse res)
        {
            var currentpage = GetCurrentPage();
           await currentpage.Navigation.PushAsync(new List.Views.RentalList(res, rentalcityurl));
            
        }
        public async Task ClickLCFSClose(RTResponse res)
        {
            var curpage = GetCurrentPage();

            //if (IsVisiblerentalsdata == true)
            //{
                //var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new RentalList(res, locationurl));
            //}
            //else
            //{
            //    await curpage.Navigation.PopToRootAsync();
            //}
           
            
            // await curpage.Navigation.PushAsync(new offerwant());
            //  await curpage.Navigation.PushAsync(new NRIApp.USHome.Views.Homemaster());
        }

        public async void BindTopBusiness(RTResponse res, string cityurl, string result)
        {
            
           try
            {
                dialog.ShowLoading("");
                if (res.primarycategoryvalue == "1")
                    topbusinesscategory = "Single Family Home";
                else if (res.primarycategoryvalue == "2")
                    topbusinesscategory = "Apartment";
                else if (res.primarycategoryvalue == "3")
                    topbusinesscategory = "Condo";
                else if (res.primarycategoryvalue == "4")
                    topbusinesscategory = "Office Space";
                else if (res.primarycategoryvalue == "5")
                    topbusinesscategory = "Retail Outlet";
                else if (res.primarycategoryvalue == "6")
                    topbusinesscategory = "Town house";
                else if (res.primarycategoryvalue == "8")
                    topbusinesscategory = "Homes";
                else if (res.primarycategoryvalue == "9")
                    topbusinesscategory = "Houses";
                else if (res.primarycategoryvalue == "10")
                    topbusinesscategory = "Hostels";
                else if (res.primarycategoryvalue == "11")
                    topbusinesscategory = "Hotels";
                else if (res.primarycategoryvalue == "12")
                    topbusinesscategory = "Basement Apartment";
                else if (res.primarycategoryvalue == "13")
                    topbusinesscategory = "Parking Garage";
                else if (res.primarycategoryvalue == "7")
                    topbusinesscategory = "Others";

                res.pageno = 1;

                var Topbusapi = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                //topbusinesscategory = res.primarycategoryvalue.Replace("-", " ");
                RTTop_Business responsetopbus = await Topbusapi.gettopbusiness(topbusinesscategory, res.Movedate, cityurl);
                if (responsetopbus.ROW_DATA != null && responsetopbus.ROW_DATA.Count > 0)
                {
                    dialog.ShowLoading("");
                    res.pageno = 1;
                    //var rentalapi = RestService.For<IListRentalCategory>(Commonsettings.RoommatesAPI);
                    //ListRentalCategory response = await rentalapi.GetRentalList(res, cityurl);
                    if (responsetopbus.ROW_DATA.Count > 0)
                    {
                        IsLCFData = true;
                        IsLCFNoData = false; 
                        lbltitle = "Find matching Ads";
                        await Task.Delay(1000);
                        Successlist = responsetopbus.ROW_DATA;
                        IsVisiblerentalsdata = true;
                    }
                    else
                    {
                        IsLCFNoData = true;
                        lblsuccess = result;
                        imgsuccess = "SuccessBg.png";
                        IsLCFData = false;
                        IsVisiblerentalsdata = false;
                    }
                    //lbltitle = "Find matching Ads";
                    //getRentaldataList();
                    //Successlist = responsetopbus.ROW_DATA;
                    //IsVisiblerentalsdata = true;
                }

                else
                {
                    IsLCFData = false;
                    IsLCFNoData = true;
                    IsVisiblerentalsdata = false;
                    // getRentaldataList();
                    imgsuccess = "successBg.png";
                }
                dialog.HideLoading();
            }
            catch(Exception e)
            {
                dialog.HideLoading();
              //  dialog.Toast("Kindly check your internet connection");
            }
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
    }
}
