using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Threading.Tasks;
using NRIApp.Roommates.Features.Post.Views;
using NRIApp.Roommates.Features.Post.Models;
using NRIApp.Roommates.Features.Post.Interface;
using NRIApp.Helpers;
using Refit;
using Acr.UserDialogs;
using Plugin.Connectivity;

namespace NRIApp.Roommates.Features.Post.ViewModels
{
    public class VMCategory : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;
        public Command ClickRooomatesBegin { get; set; }
        public Command ClickRentalBegin { get; set; }
        public Command lcffinalpage { get; set; }
        public Command postfinalpage { get; set; }
        public Command TapBackPage { get; set; }
        public Command offerwantTap { get; set; }
        public Command CategoryFirstTap { get; set; }
        public Command CategorySecondTap { get; set; }
        public Command Backbtncommand { get; set; }

        public string Contentid = "";
        public static string checkedcategoryname = "";
        private List<adtype> _offerwantlist;
        public List<adtype> offerwantlist
        {
            get { return _offerwantlist; }
            set { _offerwantlist = value;OnPropertyChanged(nameof(offerwantlist));}
        }
        
        private List<CategoryList> _CategoryFirstList;
        public List<CategoryList> CategoryFirstList
        {
            get { return _CategoryFirstList; }
            set {_CategoryFirstList = value; OnPropertyChanged(nameof(CategoryFirstList)); }
        }
        private List<CategoryList> _CategorySecondList;
        public List<CategoryList> CategorySecondList
        {
            get { return _CategorySecondList; }
            set { _CategorySecondList = value; OnPropertyChanged(nameof(CategorySecondList));}
        }
        
        private string _lblcategory;
        public string lblcategory
        {
            get { return _lblcategory; }
            set { _lblcategory = value; OnPropertyChanged(nameof(lblcategory)); }
        }
        private string _Categoryurl;
        public string Categoryurl
        {
            get { return _Categoryurl; }
            set { _Categoryurl = value;OnPropertyChanged(); }
        }
        private string _cityurl;
        public string Cityurl
        {
            get { return _cityurl; }
            set { _cityurl = value;OnPropertyChanged(); }
        }
        private bool _NxtVisible = false;
        public bool NxtVisible
        {
            get { return _NxtVisible; }
            set { _NxtVisible = value;OnPropertyChanged(nameof(NxtVisible)); }
        }
        //offerwant(watisyourneed)
        Roommates.Features.Post.Models.CategoryList needscatlist = new CategoryList();
        public VMCategory(Roommates.Features.Post.Models.CategoryList catlist ,string ismyneed)
        {
            try
            {
                needscatlist = catlist;
                ClickRooomatesBegin = new Command(ClickRoommatespage);
                ClickRentalBegin = new Command(ClickRentalspage);
                TapBackPage = new Command(ClickBackPage);
                BindAdTypeList();
                //NxtVisible
                GotoNext = new Command(propertypeprefillnext);
                offerwantTap = new Command<adtype>(async (adtypels) => await TapOnOfferWant(adtypels));
            }
            catch (Exception e) 
            {

            }
        }
        //offerwant from myneeds page Nextclick
        public async void propertypeprefillnext()
        {
            try
            {
                //dialog.ShowLoading("", null);
                if (needscatlist.supercategoryvalue == "I have a room to rent")
                {
                    var curpage = GetCurrentPage();
                    Commonsettings.CAdTypeID = 1;
                    await curpage.Navigation.PushAsync(new Catfirst(2, needscatlist.supercategoryvalue,needscatlist));
                }
                else //if (adtypels.stagid == 2)
                {
                    var curpage = GetCurrentPage();
                    int sparam = 0;
                    Commonsettings.CAdTypeID = 0;
                    await curpage.Navigation.PushAsync(new Catfirstpost(5, needscatlist.supercategoryvalue, sparam, needscatlist));
                }
               // dialog.HideLoading();
            }
            catch (Exception e)
            {
                dialog.HideLoading();
            }
        }
        //catfirstpost-single|shared|paying listvew
        public VMCategory(int stagid, string type,int param, Roommates.Features.Post.Models.CategoryList needslist)
        {
            needscatlist = needslist;
            Bindpostfirstcategory(stagid,type);
            CategoryFirstTap = new Command<CategoryList>(async (catlist) => await TaponCategoryFirst(catlist));
            needGotoNext = new Command(needgotonext);
        }
        public async void Bindpostfirstcategory(int stagid,string type)
        {
            dialog.ShowLoading("");
            await Task.Delay(300);
            var currentpg = GetCurrentPage();
            var categorylist = currentpg.FindByName<StackLayout>("categorylist");
            var CatFIRSTAPI = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
            Models.CategoryRowData response = await CatFIRSTAPI.getcategory(2, stagid, "0", 0);
            foreach (var item in response.ROW_DATA)
            {
                item.categoryimgurl = item.categoryname + "List.png";
                item.categoryimgurl = item.categoryimgurl.Replace(" ", "");
                item.textcolor = "#2c2d2f";
            }
            CategoryFirstList = response.ROW_DATA;
            if(needscatlist.ismyneed=="1")
            {
                MyneedsNxtVisible = true;
                foreach (var item in response.ROW_DATA)
                {
                    item.textcolor = "#2c2d2f";
                    item.categoryimgurl = item.categoryname + "List.png";
                    item.categoryimgurl = item.categoryimgurl.Replace(" ", "");
                    if (needscatlist.ismyneed == "1")
                    {
                        if (needscatlist.primarycategoryvalue == item.categoryname)
                        {
                            item.textcolor = "#fbaa19";
                        }
                        else if (needscatlist.secondarycategoryvalue == item.categoryname)
                        {
                            item.textcolor = "#fbaa19";
                        }
                        else if (needscatlist.categoryname == item.categoryname)
                        {
                            item.textcolor = "#fbaa19";
                        }
                        else
                        {
                            item.textcolor = "#2c2d2f";
                        }
                    }
                }

                CategoryFirstList = response.ROW_DATA;
                var currentpage = GetCurrentPage();
                var catfirstlst = currentpage.FindByName<ListView>("CategoryFirstView");
                catfirstlst.ItemsSource = response.ROW_DATA;
            }

            dialog.HideLoading();
        }
        public VMCategory(string cityurl,string value2,string value3)
        {
            try
            {
                Cityurl = cityurl;
                Backbtncommand = new Command(OnBackbtnTap);
            }
            catch(Exception e)
            {
                dialog.HideLoading();
            }
          
        }
    
        public async void OnBackbtnTap()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PopAsync();
        }
        public static int supercategoryid ;
        public static string supercatvalue = "";
        public Command CheckCatogoryTap { get; set; }
        public Command GotoNext { get; set; }
        public Command needGotoNext { get; set; }
        public Command GotoListings { get; set; }
        CategoryList res = new CategoryList();
        //catfirst - property type
        private bool _MyneedsNxtVisible = false;
        public bool MyneedsNxtVisible
        {
            get { return _MyneedsNxtVisible; }
            set { _MyneedsNxtVisible = value;OnPropertyChanged(nameof(MyneedsNxtVisible)); }
        }
        public VMCategory(int suptagid, string stype, Roommates.Features.Post.Models.CategoryList needlist)
        {
            needscatlist = needlist;
            supercategoryid = suptagid;
            supercatvalue = stype;
            res.supercategoryid = supercategoryid;
            res.supercategoryvalue = supercatvalue;
            var connected = CrossConnectivity.Current.IsConnected;
            if(connected==true)
            {
                try
                {
                    TapBackPage = new Command(ClickBackPage);
                    BindFirstCategoy(suptagid, stype);
                    CategoryFirstTap = new Command<CategoryList>(async (catlist) => await TaponCategoryFirst(catlist));
                }
                catch (Exception e)
                {
                    dialog.HideLoading();
                    dialog.Toast("Kindly check your internet connection");
                }
            }
            else
            {
                dialog.Toast("Kindly check your internet connection");
            }
            if(needlist.ismyneed=="1")
            {
                MyneedsNxtVisible = true;
            }

            CheckCatogoryTap = new Command(async () => await Gotocategorylist(res));
            GotoNext = new Command(gotonexttap);
            needGotoNext = new Command(needgotonext);
            GotoListings = new Command(gotolistingstap);
        }
        public async void needgotonext()
        {
            try
            {
                await Task.Delay(300);

                var curpage = GetCurrentPage();
                if (needscatlist.primarycategoryid == "6")
                {
                    dbprimcatval = "6";
                    needscatlist.primarycategoryid = "1";
                }
                else if (needscatlist.primarycategoryid == "7")
                {
                    dbprimcatval = "7";
                    needscatlist.primarycategoryid = "2";
                }
                else if (needscatlist.primarycategoryid == "8")
                {
                    dbprimcatval = "8";
                    needscatlist.primarycategoryid = "4";
                }
                listingdata = needscatlist;


                //if(needscatlist.ismyneed=="1")
                if (needscatlist.supercategoryid == 5)
                {
                    NxtVisible = false;
                    MyneedsNxtVisible = true;
                    string clrchange = "0";
                    foreach (var lst in CategoryFirstList)
                    {
                        if (lst.categoryname == needscatlist.primarycategoryvalue)
                        {
                            lst.textcolor = "#fbaa19";
                            clrchange = "1";
                        }
                        if (lst.categoryname == needscatlist.secondarycategoryvalue)
                        {
                            lst.textcolor = "#fbaa19";
                            clrchange = "1";
                        }
                        else if(lst.categoryname == needscatlist.categoryname)
                        {
                            lst.textcolor = "#fbaa19";
                            clrchange = "1";
                        }
                        else
                        {
                            lst.textcolor = "#2c2d2f";
                        }
                    }

                    needscatlist.categoryname = lst.categoryname;
                    var catfirstlist = curpage.FindByName<ListView>("CategoryFirstView");
                    catfirstlist.ItemsSource = null;
                    catfirstlist.ItemsSource = CategoryFirstList;
                    
                    //if (needscatlist.ismyneed == "1")
                    //{
                    //    foreach (var dt in CategoryFirstList)
                    //    {
                    //        if (dt.textcolor == "#fbaa19")
                    //        {
                    //            clrchange = "1";
                    //        }
                    //    }
                    //}
                    if (clrchange == "0")
                    {
                        dialog.Toast("Select category...");
                    }
                    else
                    {
                        await curpage.Navigation.PushAsync(new Catsec(needscatlist));
                    }
                }
                else
                {
                    string clrchange = "0";
                    if(needscatlist.ismyneed=="1")
                    {
                        foreach(var dt in CategoryFirstList)
                        {
                            if(dt.textcolor== "#fbaa19")
                            {
                                clrchange = "1";
                            }
                        }
                    }
                    if(clrchange=="0")
                    {
                        dialog.Toast("Select category...");
                    }
                    else
                    {
                        await curpage.Navigation.PushAsync(new Catsec(needscatlist));
                    }
                }
            }
            catch (Exception e)
            {
                dialog.HideLoading();
            }
        }
        public async Task Gotocategorylist(CategoryList catlist)
        {
            try
            {
                await Task.Delay(300);
                if(string.IsNullOrEmpty(checkedcategoryname))
                {
                    dialog.Alert("Kindly select your need");
                }
                else
                {
                    // Commonsettings.CRMCategory = catlist.categoryname;
                    var curpage = GetCurrentPage();
                    await curpage.Navigation.PushAsync(new Catsec(catlist));
                }
                
            }
            catch (Exception e) { }
        }
        //catsec - Aboutyou(indi/group/cf)
        public VMCategory(CategoryList clist)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if(connected==true)
            {
                try
                {
                    needscatlist = clist;
                    TapBackPage = new Command(ClickBackPage);
                    BindSecondCategoy(clist);
                    CategorySecondTap = new Command<CategoryList>(async (catlist) => await TaponCategorySecond(catlist));
                    lblcategory = clist.categoryname;
                    needGotoNext = new Command(needtaponcategorysecond);

                }
                catch (Exception e)
                {
                    dialog.HideLoading();
                }
            }
            else
            {
                dialog.Toast("Kindly check your internet connection");
            }
        }
        //catsec gotonext method
        public async void needtaponcategorysecond()
        {
            try
            {
                Scategoryid = needscatlist.secondarycategoryid;
                dialog.ShowLoading("");
                await Task.Delay(300);
                var curpage = GetCurrentPage();
                //myneed
                string clrchange = "0";
                
                if(needscatlist.ismyneed=="1")
                {
                    if (CategorySecondList != null)
                    {
                        foreach (var dta in CategorySecondList)
                        {
                            if (dta.textcolor == "#fbaa19")
                            {
                                clrchange = "1";
                            }
                        }
                    }
                }
                
                if(clrchange=="0" && needscatlist.ismyneed=="1")
                {
                    dialog.Toast("Please select the category");
                }
                else
                {
                    if (Commonsettings.CAdTypeID == 0)
                    {
                        await curpage.Navigation.PushAsync(new lcf(needscatlist));
                    }
                    else
                    {
                        await curpage.Navigation.PushAsync(new postfirst(needscatlist));
                    }
                }
                dialog.HideLoading();
            }
            catch (Exception e)
            {
                dialog.HideLoading();
            }
        }
        CategoryList clst = new CategoryList();
        NRIApp.Rentals.Features.Post.Models.RTCategoryList rtclst = new NRIApp.Rentals.Features.Post.Models.RTCategoryList();
        public async void ClickRoommatespage()
        {
            var curpage = GetCurrentPage();
            await curpage.Navigation.PushAsync(new offerwant(clst));
        }
        public async void ClickRentalspage()
        {
            var curpage = GetCurrentPage();
            await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.offerwant(rtclst));
        }
        public async void ClickBackPage()
        {
            var curpage = GetCurrentPage();
            await curpage.Navigation.PopAsync();
        }
      
        public void BindAdTypeList() 
        {

            List<adtype> lsow = new List<adtype>();
            lsow.Add(new adtype { typeid = 1,textcolor= "#2c2d2f", stagid = 2, type = "I have a room to rent" });
            lsow.Add(new adtype { typeid = 0, textcolor = "#2c2d2f", stagid = 5, type = "I need a room for rent" });
            offerwantlist = lsow;
            if (needscatlist.ismyneed == "1")
            {
                NxtVisible = true;
                foreach(var data in lsow)
                {
                    if (data.type == needscatlist.supercategoryvalue)
                    {
                        data.textcolor = "#fbaa19";
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
        Roommates.Features.Post.Models.CategoryList lst = new CategoryList();
        async Task TapOnOfferWant(adtype adtypels)
        {
            try
            {
                if (needscatlist.ismyneed == "1")
                {
                    lst = needscatlist;
                    lst.ismyneed = needscatlist.ismyneed;
                    lst.supercategoryid = adtypels.stagid;
                    lst.supercategoryvalue = adtypels.type;
                    if (needscatlist.ismyneed == "1")
                    {
                        NxtVisible = true;
                        foreach (var data in offerwantlist)
                        {
                            if (data.type == needscatlist.supercategoryvalue)
                            {
                                data.textcolor = "#fbaa19";
                            }
                            else
                            {
                                data.textcolor = "#2c2d2f";
                            }
                        }
                        //offerwanted`
                        var currentpage = GetCurrentPage();
                        var offerwantedlst = currentpage.FindByName<ListView>("offerwanted");
                        offerwantedlst.ItemsSource = null;
                        offerwantedlst.ItemsSource = offerwantlist;
                    }
                }
                    if (adtypels.stagid == 5)
                    {
                        var curpage = GetCurrentPage();
                        Commonsettings.CAdTypeID = adtypels.typeid;
                    //add condition
                        await curpage.Navigation.PushAsync(new Catfirst(adtypels.stagid, adtypels.type, lst));
                    }
                    else if (adtypels.stagid == 2)
                    {
                        var curpage = GetCurrentPage();
                        int sparam = 0;
                        Commonsettings.CAdTypeID = adtypels.typeid;
                        await curpage.Navigation.PushAsync(new Catfirstpost(adtypels.stagid, adtypels.type, sparam, lst));
                    }
            }
            catch (Exception e)
            {
                dialog.HideLoading();
            }
        }
        //678
        public static string dbprimcatval { get; set; }
        
        public async void BindFirstCategoy(int stagid, string stype) {
            try
            {
                dialog.ShowLoading("",null);
                lblcategory = stype;
               // await Task.Delay(300);
                // var currentpg = GetCurrentPage();
                // var categorylist = currentpg.FindByName<StackLayout>("categorylist");
                var CatFIRSTAPI = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                Models.CategoryRowData response = await CatFIRSTAPI.getcategory(2, stagid, "0", 0);
                //// var chbx = currentpg.FindByName<Plugin.InputKit.Shared.Controls.CheckBox>("categorychk");
                // foreach (var data in response.ROW_DATA)
                // {
                //     BoxView boxx = new BoxView();
                //     boxx.BackgroundColor = Color.FromHex("e0e0e0");
                //     boxx.HeightRequest = 1;
                //     Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                //     chbx.OnImg = "CheckBoxChecked.png";
                //     chbx.OffImg = "CheckBoxUnChecked.png";
                //     chbx.Text = data.categoryname;
                //     chbx.ShowLabel = true;
                //     chbx.CheckChanged += Chb_CategoryCheckChanged;

                //     if(data.primarycategoryid=="6")
                //     {
                //         dbprimcatval = "6";
                //         data.primarycategoryid = "1";
                //         chbx.ClassId = "1";
                //     }
                //     else if(data.primarycategoryid == "7")
                //     {
                //         dbprimcatval = "7";
                //         data.primarycategoryid = "2";
                //         chbx.ClassId = "2";
                //     }
                //     else if (data.primarycategoryid == "8")
                //     {
                //         dbprimcatval = "8";
                //         data.primarycategoryid = "4";
                //         chbx.ClassId = "4";
                //     }
                //     chbx.Padding = 5;
                //     categorylist.Children.Add(chbx);
                // }

                foreach (var item in response.ROW_DATA)
                {
                    item.textcolor = "#2c2d2f";
                    item.categoryimgurl = item.categoryname + "List.png";
                    item.categoryimgurl = item.categoryimgurl.Replace(" ", "");
                    if(needscatlist.ismyneed=="1")
                    {
                        if (needscatlist.primarycategoryvalue == item.categoryname)
                        {
                            item.textcolor = "#fbaa19";
                        }
                        else if(needscatlist.secondarycategoryvalue == item.categoryname)
                        {
                            item.textcolor = "#fbaa19";
                        }
                        else
                        {
                            item.textcolor = "#2c2d2f";
                        }
                    }
                }
               
                CategoryFirstList = response.ROW_DATA;
                var currentpage = GetCurrentPage();
                var catfirstlst = currentpage.FindByName<ListView>("CategoryFirstView");
                catfirstlst.ItemsSource = response.ROW_DATA;

                dialog.HideLoading();
            }
            catch (Exception e)
            {
                dialog.HideLoading();
                dialog.Toast("Kindly check your internet connection");
            }
        }
        private void Chb_CategoryCheckChanged(object sender, EventArgs e)
        {
            
            var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
            if (category.IsChecked==true)
            {
                if (checkedcategoryname != null && checkedcategoryname != "")
                {
                    if (!checkedcategoryname.Contains(category.ClassId))
                    {
                        checkedcategoryname += "," + category.ClassId;
                    }
                }
                else
                {

                    checkedcategoryname = category.ClassId;
                }
            }
            else
            {
                string[] indexvalue = checkedcategoryname.Split(',');
                int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                checkedcategoryname = string.Join(",", indexvalue);
            }

        }
        CategoryList listingdata = new CategoryList();
        async Task TaponCategoryFirst(CategoryList catlist)
        {
            try
            {
                    //await Task.Delay(300);
                //myneed
                if(needscatlist.ismyneed=="1")
                {
                    needscatlist.primarycategoryid = catlist.primarycategoryid;
                    needscatlist.primarycategoryvalue= catlist.primarycategoryvalue;
                    needscatlist.categoryname = catlist.categoryname;
                    catlist = needscatlist;

                    catlist.ismyneed = needscatlist.ismyneed;
                }

                var curpage = GetCurrentPage();
                if (catlist.primarycategoryid == "6")
                {
                    dbprimcatval = "6";
                    catlist.primarycategoryid = "1";
                }
                else if (catlist.primarycategoryid == "7")
                {
                    dbprimcatval = "7";
                    catlist.primarycategoryid = "2";
                }
                else if (catlist.primarycategoryid == "8")
                {
                    dbprimcatval = "8";
                    catlist.primarycategoryid = "4";
                }
                listingdata = catlist;
                //if(needscatlist.ismyneed=="1")
                //if(catlist.supercategoryid==5)
                if (catlist.supercategoryid == 5 && catlist.ismyneed == "1")
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
                    if (catlist.ismyneed == "1")
                    {
                        NxtVisible = false;
                        MyneedsNxtVisible = true;
                    }
                    else
                    {
                        NxtVisible = true;
                        MyneedsNxtVisible = false;
                    }
                }
                else
                {
                    //if(catlist.ismyneed=="1")
                    // {
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

                    if (catlist.ismyneed == "1")
                    {

                        await curpage.Navigation.PushAsync(new Catsec(needscatlist));
                    }
                    else
                    {
                        if(catlist.supercategoryid == 5)
                        {
                            NxtVisible = true;
                            MyneedsNxtVisible = false;
                        }
                        else
                        {
                            await curpage.Navigation.PushAsync(new Catsec(catlist));
                        }
                    }

                    // }
                    //else
                    // {
                    //     await curpage.Navigation.PushAsync(new Catsec(catlist));
                    // }

                }
            }
            catch (Exception e)
            {
            }
        }
        public async void gotonexttap()
        {
            if(needscatlist.ismyneed=="1")
            {
                listingdata = needscatlist;
            }
            var curpage = GetCurrentPage();
            await curpage.Navigation.PushAsync(new Catsec(listingdata));
        }
        Response response = new Response();
        public async void gotolistingstap()
        {
            response.adid = listingdata.adid.ToString();
            response.primarycategoryid = listingdata.primarycategoryid;
            response.supercategoryid = listingdata.supercategoryid;
            response.tertiarycategoryid = listingdata.tertiarycategoryid.ToString();
            response.maincategory = listingdata.maincategory;
            response.categoryname = listingdata.categoryname;
            response.primarycategoryvalue = listingdata.primarycategoryid;

            var curpage = GetCurrentPage();
            Features.List.Views.Filter_RM.filtercity = "";
            Features.List.Views.Filter_RM.filtercityurl = "";
            Features.List.Views.Filter_RM.filterlatitude = "";
            Features.List.Views.Filter_RM.filterlongitude = "";
            response.City = Commonsettings.Usercity + " ," + Commonsettings.Userstatecode;
            response.Newcityurl = Commonsettings.Usercityurl;
            response.userlat = Commonsettings.UserLat.ToString();
            response.userlong = Commonsettings.UserLong.ToString();
            response.orderby = "ordergroup";
            Roommates.Features.Post.ViewModels.VMLCF.SelectedCityName = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
            await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.List.Views.Category(response,Commonsettings.Usercityurl));
        }
        public async void BindSecondCategoy(CategoryList clist)
        {
            try
            {
                dialog.ShowLoading("");
                lblcategory = clist.Category;
                
               if(string.IsNullOrEmpty(dbprimcatval))
                {
                    if (Commonsettings.CAdTypeID == 0)
                    {
                        clist.supercategoryid = 5;
                    }
                    if (clist.primarycategoryid == "6")
                    {
                        dbprimcatval = "6";
                        clist.primarycategoryid = "1";
                    }
                    else if (clist.primarycategoryid == "7")
                    {
                        dbprimcatval = "7";
                        clist.primarycategoryid = "2";
                    }
                    else if (clist.primarycategoryid == "8")
                    {
                        dbprimcatval = "8";
                        clist.primarycategoryid = "4";
                    }
                    //myneed
                    if(clist.primarycategoryid=="1")
                    {
                        dbprimcatval = "6";
                    }
                    else if(clist.primarycategoryid == "2")
                    {
                        dbprimcatval = "7";
                    }
                    else if(clist.primarycategoryid == "4")
                    {
                        dbprimcatval = "8";
                    }
                }
                if ((clist.supercategoryvalue == "I have a room to rent") && clist.ismyneed == "1")
                {
                    clist.supercategoryid = 2;
                    clist.supercategoryvalue = "I have a room to rent";
                }
                else if ((clist.supercategoryvalue == "I need a room for rent") && clist.ismyneed == "1")
                {
                    clist.supercategoryid = 5;
                    clist.supercategoryvalue = "I need a room for rent";
                }
                else if(clist.supercategoryid == 2)
                {
                    clist.supercategoryid = 2;
                    clist.supercategoryvalue = "I have a room to rent";
                }
                else if (clist.supercategoryid == 5)
                {
                    clist.supercategoryid = 5;
                    clist.supercategoryvalue = "I need a room for rent";
                }
                var CatSecondAPI = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                Models.CategoryRowData responseSec = await CatSecondAPI.getcategory(3, clist.supercategoryid, dbprimcatval, clist.secondarycategoryid);

                foreach (var dta in responseSec.ROW_DATA)
                {
                        dta.textcolor = "#2c2d2f";
                }
                CategorySecondList = responseSec.ROW_DATA;
                if(clist.ismyneed=="1")
                {
                    MyneedsNxtVisible = true;
                    foreach (var dta in CategorySecondList)
                    {
                        if(clist.secondarycategoryvalue== dta.categoryname || clist.categoryname == dta.categoryname)
                        {
                            dta.textcolor = "#fbaa19";
                        }
                        else
                        {
                            dta.textcolor = "#2c2d2f";
                        }
                    }
                    CategorySecondList = null;
                    CategorySecondList = responseSec.ROW_DATA;
                }
                dialog.HideLoading();
            }
            catch (Exception e)
            {
                dialog.HideLoading();
            }
        }
        public static int Scategoryid ;
        async Task TaponCategorySecond(CategoryList catlist)
        {
            try
            {
                dialog.ShowLoading("");
                //myneed
                Scategoryid = catlist.secondarycategoryid;
                if (needscatlist.ismyneed=="1")
                {
                    
                    needscatlist.secondarycategoryvalue = catlist.categoryname;
                    
                    needscatlist.secondarycategoryid = catlist.secondarycategoryid;
                    //_CategorymyneedSecondList = CategorySecondList;
                    foreach (var dta in CategorySecondList)
                    {
                        if (catlist.categoryname == dta.categoryname)
                        {
                            dta.textcolor = "#fbaa19";
                        }
                        else
                        {
                            dta.textcolor = "#2c2d2f";
                        }
                    }
                    var currentpage = GetCurrentPage();
                    var catsecondlst = currentpage.FindByName<ListView>("CategorySecondList");
                    catsecondlst.ItemsSource = null;
                    catsecondlst.ItemsSource = CategorySecondList;
                    var curpage = GetCurrentPage();
                    if (Commonsettings.CAdTypeID == 0)
                    {
                        await curpage.Navigation.PushAsync(new lcf(needscatlist));
                    }
                    else
                    {
                        await curpage.Navigation.PushAsync(new postfirst(needscatlist));
                    }
                }
               else
                {
                    Scategoryid = catlist.secondarycategoryid;
                    var curpage = GetCurrentPage();
                    if (Commonsettings.CAdTypeID == 0)
                    {
                        await curpage.Navigation.PushAsync(new lcf(catlist));
                    }
                    else
                    {
                        await curpage.Navigation.PushAsync(new postfirst(catlist));
                    }
                }
                
                //await Task.Delay(300);
                
                dialog.HideLoading();
            }
            catch (Exception e)
            {
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
