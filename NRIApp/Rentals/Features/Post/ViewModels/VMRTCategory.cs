using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Refit;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NRIApp.Rentals.Features.Post.Models;
using NRIApp.Rentals.Features.Post.Views;
using NRIApp.Helpers;
using NRIApp.Rentals.Features.Post.Interface;
using Acr.UserDialogs;

namespace NRIApp.Rentals.Features.Post.ViewModels
{
	public class VMRTCategory : INotifyPropertyChanged
	{
        IUserDialogs dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public Command TapBackPage { get; set; }
        public Command offerwantTap { get; set; }
        public Command CategoryFirstTap { get; set; }
        public Command CategorySecondTap { get; set; }
        public Command CategoryThirdTap { get; set; }
        public Command Backbtncommand { get; set; }

        public Command GotoNext { get; set; }
        public Command GotoListings { get; set; }

        private List<adtype> _offerwantlist;
        public List<adtype> offerwantlist
        {
            get { return _offerwantlist; }
            set { _offerwantlist = value; OnPropertyChanged(nameof(offerwantlist)); }
        }
        private List<RTCategoryList> _CategoryFirstList;
        public List<RTCategoryList> CategoryFirstList
        {
            get { return _CategoryFirstList; }
            set { _CategoryFirstList = value;OnPropertyChanged(); }
        }
        private List<RTCategoryList> _CategorySecondList;
        public List<RTCategoryList> CategorySecondList
        {
            get { return _CategorySecondList; }
            set{ _CategorySecondList = value; OnPropertyChanged(nameof(CategorySecondList)); }
        }
        private List<RTCategoryList> _CategoryThirdList;
        public List<RTCategoryList> CategoryThirdList
        {
            get { return _CategoryThirdList; }
            set { _CategoryThirdList = value; OnPropertyChanged(nameof(CategoryThirdList)); }
        }
        private string _lblcategory;
        public string lblcategory
        {
            get { return _lblcategory; }
            set { _lblcategory = value; OnPropertyChanged(nameof(lblcategory)); }
        }
        public string Contentid = "";
        private List<Roommates.Features.Post.Models.Search> _searchlist { get; set; }

        public List<Roommates.Features.Post.Models.Search> SearchList
        {
            get { return _searchlist; }
            set { _searchlist = value; OnPropertyChanged(); }
        }
        public string _tempsearch;

        private bool _NxtVisible = false;
        public bool NxtVisible
        {
            get { return _NxtVisible; }
            set { _NxtVisible = value; OnPropertyChanged(nameof(NxtVisible)); }
        }

        private bool _MyneedsNxtVisible = false;
        public bool MyneedsNxtVisible
        {
            get { return _MyneedsNxtVisible; }
            set { _MyneedsNxtVisible = value; OnPropertyChanged(nameof(MyneedsNxtVisible)); }
        }
        Rentals.Features.Post.Models.RTCategoryList needsrtcatlist = new RTCategoryList();
        public VMRTCategory (RTCategoryList list)
		{
            try
            {
                needsrtcatlist = list;
                TapBackPage = new Command(ClickBackPage);
                BindAdTypeList();
                GotoNext = new Command(propertypeprefillnext);
                offerwantTap = new Command<adtype>(async (adtypels) => await TapOnOfferWant(adtypels));
            }
            catch (Exception e) { }
        }
        public async void propertypeprefillnext()
        {
            try
            {
                //dialog.ShowLoading("", null);
                //if (needsrtcatlist.supercategoryvalue == "I have a room to rent")
                //{
                //    var curpage = GetCurrentPage();
                //    Commonsettings.CAdTypeID = 1;
                //    await curpage.Navigation.PushAsync(new Catfirst(2, needscatlist.supercategoryvalue, needscatlist));
                //}
                //else //if (adtypels.stagid == 2)
                //{
                //    var curpage = GetCurrentPage();
                //    int sparam = 0;
                //    Commonsettings.CAdTypeID = 0;
                //    await curpage.Navigation.PushAsync(new Catfirstpost(5, needscatlist.supercategoryvalue, sparam, needscatlist));
                //}
                if(!string.IsNullOrEmpty(needsrtcatlist.adtype))
                {
                    Commonsettings.CRTAdTypeID = Convert.ToInt32(needsrtcatlist.adtype);
                }
                
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new Catfirst(needsrtcatlist.supercategoryid, needsrtcatlist.supercategoryvalue,needsrtcatlist));
                // dialog.HideLoading();
            }
            catch (Exception e)
            {
                dialog.HideLoading();
            }
        }
        public VMRTCategory(string value1, string value2, string value3)
        {
            Backbtncommand = new Command(OnBackbtnTap);
        }
        public async void OnBackbtnTap()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PopAsync();
        }
      //catfirst-Property type
        public VMRTCategory(int suptagid, string stype, RTCategoryList needlist)
        {
            try
            {
                needsrtcatlist = needlist;
                TapBackPage = new Command(ClickBackPage);
                BindFirstCategoy(suptagid, stype);
                CategoryFirstTap = new Command<RTCategoryList>(async (catlist) => await TaponCategoryFirst(catlist));
                GotoNext = new Command(gotonexttap);
                GotoListings = new Command(gotolistingstap);
            }
            catch (Exception e) { }
        }
        
        //catsecond-no of vehicles/beds
        public VMRTCategory(RTCategoryList clist, string sPage)
        {
            try
            {
                needsrtcatlist = clist;
                TapBackPage = new Command(ClickBackPage);
                //catsecond-no of vehicles/beds-spage2
                if (sPage == "2") 
                { 
                BindSecondCategoy(clist);
                CategorySecondTap = new Command<RTCategoryList>(async (catlist) => await TaponCategorySecond(catlist));
                GotoNext=new Command(TaponnxtCategorySecond);
                }
                else if (sPage == "3") //catthird-about you -spage3
                {
                BindThirdCategoy(clist);
                CategoryThirdTap = new Command<RTCategoryList>(async (catlist) => await TaponCategoryThird(catlist));
                GotoNext = new Command(TaponnxtCategoryThird);
                }
            }
            catch (Exception e) 
            {
            }
        }

       
        public async void ClickBackPage()
        {
            var curpage = GetCurrentPage();
            await curpage.Navigation.PopAsync();
        }
      
        public void BindAdTypeList()
        {
          try
            {
                List<adtype> lsow = new List<adtype>();
                lsow.Add(new adtype { typeid = 1, textcolor = "#2c2d2f", stagid = 1, type = "I have a property to rent" });
                lsow.Add(new adtype { typeid = 1, textcolor = "#2c2d2f", stagid = 3, type = "I have a commercial space to rent" });
                lsow.Add(new adtype { typeid = 1, textcolor = "#2c2d2f", stagid = 36, type = "I offer short stay accommodation" });
                lsow.Add(new adtype { typeid = 0, textcolor = "#2c2d2f", stagid = 4, type = "I need a property for rent" });
                lsow.Add(new adtype { typeid = 0, textcolor = "#2c2d2f", stagid = 37, type = "I need short stay accommodation" });
                offerwantlist = lsow;
                if (needsrtcatlist.ismyneed == "1")
                {
                    NxtVisible = true;
                    foreach (var data in lsow)
                    {
                        if (data.type == needsrtcatlist.supercategoryvalue)
                        {
                            data.textcolor = "#fbaa19";
                            needsrtcatlist.supercategoryid = data.stagid;
                        }
                        else
                        {
                            data.textcolor = "#2c2d2f";
                        }
                    }
                    offerwantlist = null;
                    offerwantlist = lsow;
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public static int sCategoryID ;
        async Task TapOnOfferWant(adtype adtypels)
        {
            try
            {
                if (needsrtcatlist.ismyneed == "1")
                {
                    //lst = needsrtcatlist;
                    //lst.ismyneed = needsrtcatlist.ismyneed;
                    needsrtcatlist.supercategoryid = adtypels.stagid;
                    needsrtcatlist.supercategoryvalue = adtypels.type;
                    if (needsrtcatlist.ismyneed == "1")
                    {
                        NxtVisible = true;
                        foreach (var data in offerwantlist)
                        {
                            if (data.type == needsrtcatlist.supercategoryvalue)
                            {
                                data.textcolor = "#fbaa19";
                            }
                            else
                            {
                                data.textcolor = "#2c2d2f";
                            }
                        }
                    }
                }
                sCategoryID = adtypels.stagid;
                var curpage = GetCurrentPage();
                Commonsettings.CRTAdTypeID = adtypels.typeid;
                
                await curpage.Navigation.PushAsync(new Catfirst(adtypels.stagid, adtypels.type,needsrtcatlist));
            }
            catch (Exception e) { }
        }

        public async void BindFirstCategoy(int stagid, string stype)
        {
            try
            {
                dialog.ShowLoading("");
                lblcategory = stype;
                var CatFIRSTAPI = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                Models.RTCategoryRowData response = await CatFIRSTAPI.getcategory(2, stagid, 0, 0);
                foreach (var item in response.ROW_DATA)
                {
                    item.textcolor = "#2c2d2f";
                    item.categoryimgurl = item.categoryname + "List.png";
                    item.categoryimgurl = item.categoryimgurl.Replace(" ", "");
                    if(needsrtcatlist.ismyneed=="1"&& item.categoryname == needsrtcatlist.primarycategoryvalue)
                    {
                        item.textcolor = "#fbaa19";
                    }
                }
                if(needsrtcatlist.ismyneed=="1")
                {
                    NxtVisible = true;
                    MyneedsNxtVisible = false;
                }
                else
                {
                    NxtVisible = false;
                    MyneedsNxtVisible = false;
                }
                dialog.HideLoading();
                var curpage = GetCurrentPage();
                CategoryFirstList = response.ROW_DATA;
                var catfirstlist = curpage.FindByName<ListView>("CategoryFirstView");
                catfirstlist.ItemsSource = response.ROW_DATA;
            }
            catch (Exception e)
            {
                dialog.Toast("Kindly check your internet connection");
            }
        }
        RTCategoryList listingdata = new RTCategoryList();
        async Task TaponCategoryFirst(RTCategoryList catlist)
        {
            try
            {
                listingdata = catlist;
                var curpage = GetCurrentPage();
                if(needsrtcatlist.ismyneed=="1")
                { 
                    needsrtcatlist.primarycategoryid = catlist.primarycategoryid;
                    needsrtcatlist.primarycategoryvalue = catlist.categoryname;
                    needsrtcatlist.categoryname = catlist.categoryname;
                }
                Commonsettings.CRTCategory = catlist.categoryname;
                if (needsrtcatlist.ismyneed == "1")
                {
                    NxtVisible = true;
                    MyneedsNxtVisible = false;
                }
                else
                {
                    if(catlist.supercategoryid==4|| catlist.supercategoryid == 37)
                    {
                        NxtVisible = false;
                        MyneedsNxtVisible = true;
                    }
                    else
                    {
                        NxtVisible = false;
                        MyneedsNxtVisible = false;
                    }
                }
                if (catlist.supercategoryid==4 || catlist.supercategoryid==37)
                {
                  
                    foreach (var lst in CategoryFirstList)
                    {
                        if (lst.categoryname == catlist.categoryname)
                        {
                            lst.textcolor = "#fbaa19";
                        }
                        else
                        {
                            lst.textcolor = "#2c2d2f";
                        }
                    }
                    var catfirstlist = curpage.FindByName<ListView>("CategoryFirstView");
                    catfirstlist.ItemsSource = null;
                    catfirstlist.ItemsSource = CategoryFirstList;
                }
                else
                {
                    if (needsrtcatlist.ismyneed=="1")
                    {
                        if (catlist.supercategoryid == 3)
                        {
                            await curpage.Navigation.PushAsync(new Catthird(needsrtcatlist));
                        }
                        else
                        {
                            await curpage.Navigation.PushAsync(new Catsec(needsrtcatlist));
                        }
                    }
                    else
                    {
                        if (catlist.supercategoryid == 3)
                        {
                            await curpage.Navigation.PushAsync(new Catthird(catlist));
                        }
                        else
                        {
                            await curpage.Navigation.PushAsync(new Catsec(catlist));
                        }
                    }
                    
                }
            }
            catch (Exception e) { }
        }
        public async void gotonexttap()
        {
            var curpage = GetCurrentPage();
            string clrchange = "0";
          
            foreach (var lst in CategoryFirstList)
            {
                if (lst.categoryname == needsrtcatlist.primarycategoryvalue)
                {
                    lst.textcolor = "#fbaa19";
                    clrchange = "1";
                }
                else if (lst.categoryname == needsrtcatlist.categoryname)
                {
                    lst.textcolor = "#fbaa19";
                    clrchange = "1";
                }
                else
                {
                    lst.textcolor = "#2c2d2f";
                }
            }
            if (clrchange == "0" && needsrtcatlist.ismyneed == "1")
            {
                dialog.Toast("Select category...");
            }
            else
            {
                //if (listingdata.supercategoryid == 3)
                //{
                //    if (needsrtcatlist.ismyneed == "1")
                //    {
                //        await curpage.Navigation.PushAsync(new Catthird(needsrtcatlist));
                //    }
                //    else
                //    {
                //        await curpage.Navigation.PushAsync(new Catthird(listingdata));
                //    }
                //}
                //else
                //{
                //    if (needsrtcatlist.ismyneed == "1")
                //    {
                //        await curpage.Navigation.PushAsync(new Catsec(needsrtcatlist));
                //    }
                //    else
                //    {
                //        await curpage.Navigation.PushAsync(new Catsec(listingdata));
                //    }
                //}
                if (needsrtcatlist.ismyneed == "1")
                {
                    if (needsrtcatlist.supercategoryid == 3)
                    {
                        await curpage.Navigation.PushAsync(new Catthird(needsrtcatlist));
                    }
                    else
                    {
                        await curpage.Navigation.PushAsync(new Catsec(needsrtcatlist));
                    }
                }
                else
                {
                    if (needsrtcatlist.supercategoryid == 3)
                    {
                        await curpage.Navigation.PushAsync(new Catthird(listingdata));
                    }
                    else
                    {
                        await curpage.Navigation.PushAsync(new Catsec(listingdata));
                    }
                }
            }
            
        }
        RTResponse response = new RTResponse();
        public async void gotolistingstap()
        {
            response.adid = listingdata.adid.ToString();
            response.primarycategoryid = listingdata.primarycategoryid;
            response.supercategoryid = listingdata.supercategoryid;
            response.tertiarycategoryid = listingdata.tertiarycategoryid;
            response.maincategory = listingdata.maincategory;
            response.categoryname = listingdata.categoryname;
            if (response.categoryname == "Single Family Home")
                response.primarycategoryvalue = "1";
            if (response.categoryname == "Apartment")
                response.primarycategoryvalue = "2";
            if (response.categoryname == "Condo")
                response.primarycategoryvalue = "3";
            if (response.categoryname == "Office Space")
                response.primarycategoryvalue = "4";
            if (response.categoryname == "Retail Outlet")
                response.primarycategoryvalue = "5";
            if (response.categoryname == "Town house")
                response.primarycategoryvalue = "6";
            if (response.categoryname == "Homes")
                response.primarycategoryvalue = "8";
            if (response.categoryname == "Houses")
                response.primarycategoryvalue = "9";
            if (response.categoryname == "Hostels")
                response.primarycategoryvalue = "10";
            if (response.categoryname == "Hotels")
                response.primarycategoryvalue = "11";
            if (response.categoryname == "Basement Apartment")
                response.primarycategoryvalue = "12";
            if (response.categoryname == "Parking Garage")
                response.primarycategoryvalue = "13";
            if (response.categoryname == "Others")
                response.primarycategoryvalue = "7";

            var curpage = GetCurrentPage();
            Features.List.Views.Filter_RT.filtercity = "";
            Features.List.Views.Filter_RT.filtercityurl = "";
            Features.List.Views.Filter_RT.filterlatitude = "";
            Features.List.Views.Filter_RT.filterlongitude = "";
            response.City = Commonsettings.Usercity + " ," + Commonsettings.Userstatecode;
            response.Newcityurl = Commonsettings.Usercityurl;
            response.userlat = Commonsettings.UserLat.ToString();
            response.userlong = Commonsettings.UserLong.ToString();
            response.orderby = "ordergroup";
            Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName= Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
            await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.List.Views.RentalList(response, Commonsettings.Usercityurl));
        }
        public async void BindSecondCategoy(RTCategoryList clist)
        {
            try
            {
                dialog.ShowLoading("");
                lblcategory = clist.categoryname;
                var CatSecondAPI = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                Models.RTCategoryRowData responseSec = await CatSecondAPI.getcategory(3, clist.supercategoryid, clist.primarycategoryid, clist.secondarycategoryid);
                if(needsrtcatlist.ismyneed=="1")
                {
                    NxtVisible = true;
                    foreach (var data in responseSec.ROW_DATA)
                    {
                        if (needsrtcatlist.secondarycategoryvalue == data.categoryname|| needsrtcatlist.secondarycategoryid == data.secondarycategoryid || needsrtcatlist.categoryname == data.categoryname)
                        {
                            data.textcolor = "#fbaa19";
                        }
                        else
                        {
                            data.textcolor = "#2c2d2f";
                        }
                    }
                }
                else
                {
                    foreach (var data in responseSec.ROW_DATA)
                    {
                        if (clist.secondarycategoryvalue == data.categoryname)
                        {
                            data.textcolor = "#fbaa19";
                        }
                        else
                        {
                            data.textcolor = "#2c2d2f";
                        }
                    }
                }
                dialog.HideLoading();
                CategorySecondList = responseSec.ROW_DATA;
            }
            catch (Exception e)
            { 
            }
        }

        async Task TaponCategorySecond(RTCategoryList catlist)
        {
            try
            {
               if(needsrtcatlist.ismyneed=="1")
                {
                    var curpage = GetCurrentPage();
                    await curpage.Navigation.PushAsync(new Catthird(needsrtcatlist));
                }
               else
                {
                    var curpage = GetCurrentPage();
                    await curpage.Navigation.PushAsync(new Catthird(catlist));
                }
            }
            catch (Exception e)
            {
                dialog.HideLoading();
            }
        }
        async void TaponnxtCategorySecond()
        {
            try
            {
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new Catthird(needsrtcatlist));
            }
            catch (Exception e)
            {
                dialog.HideLoading();
            }
        }

        public async void BindThirdCategoy(RTCategoryList clist)
        {
            try
            {
                dialog.ShowLoading("");
                lblcategory = clist.categoryname;
                //if (Commonsettings.CRTAdTypeID == 1)
                //{
                //    clist.supercategoryid = 1;
                //}
                int catlevel = 4;
                if (clist.supercategoryid == 3) {
                    catlevel = 3;
                }
                var CatSecondAPI = RestService.For<IRTCategory>(Commonsettings.RoommatesAPI);
                Models.RTCategoryRowData responseSec = await CatSecondAPI.getcategory(catlevel, clist.supercategoryid, clist.primarycategoryid, clist.secondarycategoryid);
                if (needsrtcatlist.ismyneed == "1")
                {
                    NxtVisible = true;
                    foreach (var data in responseSec.ROW_DATA)
                    {
                        if (needsrtcatlist.tertiarycategoryvalue == data.categoryname || needsrtcatlist.categoryname == data.categoryname || needsrtcatlist.secondarycategoryvalue == data.categoryname)
                        {
                            data.textcolor = "#fbaa19";
                        }
                        else
                        {
                            data.textcolor = "#2c2d2f";
                        }
                    }
                }
                else
                {
                    foreach (var data in responseSec.ROW_DATA)
                    {
                        if (clist.tertiarycategoryvalue == data.categoryname)
                        {
                            data.textcolor = "#fbaa19";
                        }
                        else
                        {
                            data.textcolor = "#2c2d2f";
                        }
                    }
                }
                dialog.HideLoading();
                CategoryThirdList = responseSec.ROW_DATA;
            }
            catch (Exception e) 
            {
                dialog.HideLoading();
            }
        }

        async Task TaponCategoryThird(RTCategoryList catlist)
        {
            try
            {
                string clrchange = "0";
                var curpage = GetCurrentPage();
                if (needsrtcatlist.ismyneed=="1")
                {
                    foreach (var lst in CategoryThirdList)
                    {
                        if (lst.categoryname == needsrtcatlist.primarycategoryvalue)
                        {
                            lst.textcolor = "#fbaa19";
                            clrchange = "1";
                            needsrtcatlist.categoryname = lst.categoryname;
                        }
                        else if (lst.categoryname == needsrtcatlist.categoryname)
                        {
                            lst.textcolor = "#fbaa19";
                            clrchange = "1";
                            needsrtcatlist.categoryname = lst.categoryname;
                            needsrtcatlist.tertiarycategoryid = lst.tertiarycategoryid;
                        }
                        else
                        {
                            lst.textcolor = "#2c2d2f";
                        }
                    }
                    needsrtcatlist.tertiarycategoryid = catlist.tertiarycategoryid;
                    needsrtcatlist.tertiarycategoryvalue = catlist.categoryname;
                    needsrtcatlist.categoryname = catlist.categoryname;
                    needsrtcatlist.maincategory = catlist.maincategory;
                    if (needsrtcatlist.supercategoryid == 1 || needsrtcatlist.supercategoryid == 3 || needsrtcatlist.supercategoryid == 36)
                    {
                        Commonsettings.CRTAdTypeID = 1;
                    }
                    if (Commonsettings.CRTAdTypeID == 0)
                    {
                        await curpage.Navigation.PushAsync(new lcf(needsrtcatlist));
                    }
                    else
                    {
                        await curpage.Navigation.PushAsync(new postfirst(needsrtcatlist));
                    }
                }
                else
                {
                    if (Commonsettings.CRTAdTypeID == 0)
                    {
                        await curpage.Navigation.PushAsync(new lcf(catlist));
                    }
                    else
                    {
                        await curpage.Navigation.PushAsync(new postfirst(catlist));
                    }
                }

                
                //if (clrchange == "0"&&needsrtcatlist.ismyneed=="1")
                //{
                //    dialog.Toast("Select category...");
                //}

                
                
            }
            catch (Exception e)
            {
                string msg = e.Message;
                dialog.HideLoading();
            }
        }
        async void TaponnxtCategoryThird()
        {

            try
            {
                //if(needsrtcatlist.ismyneed=="1")
                //{
                //    foreach(var dta in CategoryThirdList)
                //    {

                //    }
                //}
                var curpage = GetCurrentPage();
                if(string.IsNullOrEmpty(needsrtcatlist.maincategory))
                {
                    needsrtcatlist.maincategory = needsrtcatlist.Category;
                }
                if (needsrtcatlist.supercategoryid == 1 || needsrtcatlist.supercategoryid == 3 || needsrtcatlist.supercategoryid == 36)
                {
                    Commonsettings.CRTAdTypeID = 1;
                }
                if (string.IsNullOrEmpty(needsrtcatlist.categoryname))
                {
                    needsrtcatlist.categoryname = needsrtcatlist.tertiarycategoryvalue;
                }
                if (Commonsettings.CRTAdTypeID == 0)
                {
                    await curpage.Navigation.PushAsync(new lcf(needsrtcatlist));
                }
                else
                {
                    await curpage.Navigation.PushAsync(new postfirst(needsrtcatlist));
                }
            }
            catch (Exception e)
            {
                string msg = e.Message;
                dialog.HideLoading();
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