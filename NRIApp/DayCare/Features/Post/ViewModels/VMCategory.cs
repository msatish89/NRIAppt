using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Threading.Tasks;
using Refit;
using NRIApp.DayCare.Features.Post.Models;
using NRIApp.DayCare.Features.Post.Interface;
using NRIApp.DayCare.Features.Post.Views;
using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;

namespace NRIApp.DayCare.Features.Post.ViewModels
{
   public class VMCategory : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;

        private List<DC_CategoryList> _getTypes;
        public Command<DC_CategoryList> CategoryFirstTap { get; set; }
        public Command needGotoNext { get; set; }
        public Command myneedneedGotoNext { get; set; }
        public Command<DC_CategoryList> CareType { get; set; }
        public Command<DC_CategoryList> CareProviders { get; set; }
        public Command<DC_CategoryList> WorkType { get; set; }
        public Command listingcmd { get; set; }
        public Command postingcmd { get; set; }

        private bool _btnvisible = false;
        public bool btnvisible
        {
            get { return _btnvisible; }
            set { _btnvisible = value;OnPropertyChanged(nameof(btnvisible)); }
        }
        private bool _worktypebtnvisible = false;
        public bool worktypebtnvisible
        {
            get { return _worktypebtnvisible; }
            set { _worktypebtnvisible = value;OnPropertyChanged(nameof(worktypebtnvisible)); }
        }
        public List<DC_CategoryList> GetTypes
        {
            get { return _getTypes; }
            set
            {
                _getTypes = value;
                OnPropertyChanged(nameof(GetTypes));
            }
        }

        private bool _MyneedsNxtVisible = false;
        public bool MyneedsNxtVisible
        {
            get { return _MyneedsNxtVisible; }
            set { _MyneedsNxtVisible = value;OnPropertyChanged(nameof(MyneedsNxtVisible)); }
        }
        NRIApp.DayCare.Features.Post.Models.DC_CategoryList needdccatlist = new DayCare.Features.Post.Models.DC_CategoryList();
        public VMCategory(Models.DC_CategoryList dccatlist)
        {
            try
            {
                needdccatlist = dccatlist;
                BindFirstCategoy();
                CategoryFirstTap = new Command<DC_CategoryList>(TaponCategoryFirst);
                needGotoNext = new Command(needgotonext);
                //needGotoNext = new Command(needgotonext);
            }
            catch (Exception e) { }
        }
        public async void needgotonext()
        {
            try
            {
                dialog.ShowLoading("", null);
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new Catsecond("2", needdccatlist));
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void BindFirstCategoy()
        {
            try
            {
                dialog.ShowLoading("",null);
                var CatFirstAPI = RestService.For<ICategory>(Commonsettings.DaycareAPI);
                Models.DCCategoryRowData responseSec = await CatFirstAPI.GetCareTypes("1", "0", "0", "0");
                if (needdccatlist.ismyneed == "1")
                {
                    MyneedsNxtVisible = true;
                }
                    foreach (var item in responseSec.ROW_DATA)
                    {
                        item.textcolor = "#2c2d2f";
                        item.categoryimgurl = item.categoryname + "List.png";
                        item.categoryimgurl = item.categoryimgurl.Replace(" ", "");
                        if (needdccatlist.ismyneed == "1")
                        {
                            if (needdccatlist.supercategoryvalue == item.categoryname)
                            {
                                item.textcolor = "#fbaa19";
                            }
                            else if (needdccatlist.primarycategoryvalue == item.categoryname)
                            {
                                item.textcolor = "#fbaa19";
                            }
                            else
                            {
                                item.textcolor = "#2c2d2f";
                            }
                        }
                    }
                //}
               
                GetTypes = responseSec.ROW_DATA;
                OnPropertyChanged(nameof(GetTypes));
                dialog.HideLoading();
            }
            catch (Exception e)
            {
            }
        }
        
        private async void TaponCategoryFirst(DC_CategoryList dctypes)
        {
            try
            {
                var curpage = GetCurrentPage();
                if (needdccatlist.ismyneed=="1")
                {
                    needdccatlist.supercategoryid = dctypes.supercategoryid;
                    needdccatlist.supercategoryvalue = dctypes.categoryname;
                    needdccatlist.maincategory = dctypes.maincategory;
                    needdccatlist.newcategoryurl = dctypes.newcategoryurl;
                    await curpage.Navigation.PushAsync(new Catsecond("2", needdccatlist));
                }
               else
                {
                    //await curpage.Navigation.PushAsync(new Catsecond("2", dctypes.supercategoryid, dctypes.primarycategoryid, ""));
                    await curpage.Navigation.PushAsync(new Catsecond("2", dctypes));
                }
            }
            catch (Exception e) { }
        }
        private int _Clevel ;
        public int Clevel
        {
            get { return _Clevel; }
            set { _Clevel = value;OnPropertyChanged(nameof(Clevel)); }
        }
        Daycareposting post = new Daycareposting();
        DC_CategoryList categorytype = new DC_CategoryList();
        string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
        public VMCategory(string Categorylevel, DC_CategoryList dctypes, int ctype)
        {
            try
            {
                // dialog.ShowLoading("", null);
                categorytype = dctypes;
                needdccatlist = dctypes;
                post.Categorylevel = Categorylevel;
                post.supercategoryid = dctypes.supercategoryid;
                post.supercategoryvalue = dctypes.supercategoryvalue;
                post.primarycategoryid = dctypes.primarycategoryid;
                post.secondarycategoryid = dctypes.secondarycategoryid;
                post.primarycategoryvalue = dctypes.primarycategoryvalue;
                post.secondarycategoryvalue = dctypes.secondarycategoryvalue;
                post.tertiarycategoryid = dctypes.tertiarycategoryid;
                post.tertiarycategoryvalue = dctypes.tertiarycategoryvalue;
                post.newcategoryurl = dctypes.newcategoryurl;
                post.categoryurl = dctypes.categoryurl;
                Clevel = ctype;
                if (ctype == 2)
                {
                    DCfirstpage(Categorylevel, dctypes);
                }
                else if (ctype == 3)
                {
                    if (dctypes.secondarycategoryid == "9")
                    {
                        btnvisible = true;
                    }
                    DCsecondpage(Categorylevel, dctypes);
                }
                else if (ctype == 4)
                {
                    if (ctype == 4 && dctypes.secondarycategoryid == "8")
                    {
                        DCthirdpage(Categorylevel, dctypes);
                    }
                    else if (ctype == 4 && dctypes.secondarycategoryid == "9" && dctypes.supercategoryid =="2")
                    {
                        //btnvisible = true;
                        //foreach (var lst in GetTypes)
                        //{
                        //    if (lst.secondarycategoryid == dctypes.secondarycategoryid)
                        //    {
                        //        lst.textcolor = "#fbaa19";
                        //    }
                        //    else
                        //    {
                        //        lst.textcolor = "#2c2d2f";
                        //    }
                        //}
                        //var curpage = GetCurrentPage();
                        //var catfirstlist = curpage.FindByName<ListView>("CategoryFirstView");
                        //catfirstlist.ItemsSource = null;
                        //catfirstlist.ItemsSource = GetTypes;
                        gotouserdtlpg(post);
                    }
                    else if(ctype == 4 && dctypes.secondarycategoryid == "9" && dctypes.supercategoryid == "1")
                    {
                        DCthirdpage(Categorylevel, dctypes);
                    }
                    else
                    {
                        DCthirdpage(Categorylevel, dctypes);
                    }
                }
                if(dctypes.ismyneed =="1" && dctypes.isprefill!="1")
                {
                    btnvisible = false;
                    prefillfirst(dctypes);
                }
                WorkType = new Command<DC_CategoryList>(TapWorkType);
                CareType = new Command<DC_CategoryList>(TapCaretype);
                CareProviders = new Command<DC_CategoryList>(TapCareProviderType);
                listingcmd = new Command(gotolisting);
                postingcmd = new Command(gotoposting);
                needGotoNext = new Command(gotonext);
                myneedneedGotoNext = new Command(gotoneedpost);
                //  dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void gotoneedpost()
        {
            try
            {
                var currentpage = GetCurrentPage();
                if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                {
                    await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                }
                else
                {
                    // dialog.ShowLoading("", null);

                    post.contributor = Commonsettings.UserName;
                    post.emailid = Commonsettings.UserEmail;
                    post.country = Commonsettings.Usercountry;
                    post.state = Commonsettings.Userstatecode;
                    post.zipcode = Commonsettings.Userzipcode;
                    post.city = Commonsettings.Usercity;
                    post.lat = Commonsettings.UserLat;
                    post.longtitude = Commonsettings.UserLong;
                    post.userpid = Commonsettings.UserPid;
                    post.ip = ipaddress;
                    var dcapi = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                    var postfirstdata = await dcapi.postfirstsubmit(post);
                    if (!string.IsNullOrEmpty(postfirstdata.businessid))
                    {
                        post.businessid = postfirstdata.businessid;
                        post.premiumad = postfirstdata.premiumad;
                        post.usertype = postfirstdata.usertype;

                        //var currentpage = GetCurrentPage();
                        await currentpage.Navigation.PushAsync(new Post.Views.PostFirst(post));
                    }
                    else
                    {
                        dialog.Toast("Try again later...");
                    }
                    //  dialog.HideLoading();
                }
            }
            catch (Exception ex)
            {
                dialog.Toast("Some error occurred...try later...");
                dialog.HideLoading();
            }
        }
        public void prefillfirst(DC_CategoryList dctype)
        {
            try
            {
                post.isprefill = "1";
                post.businessid = dctype.Businessid;
                post.businessname = dctype.Businessname;
                post.Businesstitleurl = dctype.Businesstitleurl;
                post.Profiletitle = dctype.Profiletitle;
                post.contactName = dctype.ContactName;
                post.licenseno = dctype.Licenseno;
                post.adtype = dctype.adtype;
                //post.Businessname = dctype.Businessname1;
                //post.Businesstitleurl = dctype.Businesstitleurl1;
                post.Businessaddress = dctype.Businessaddress;
                post.zipcode = dctype.Zipcode;
                post.city = dctype.City;
                post.state = dctype.State;
                post.Customerid = dctype.Customerid;
                post.Campaignid = dctype.Campaignid;
                post.Campaignstarts = dctype.Campaignstarts;
                post.Campaignend = dctype.Campaignend;
                post.country = dctype.Country;
                //post.pricemode = dctype.Crdate;
                post.emailid = dctype.Email;
                post.ContactNo = dctype.Phone;
                //post.Phone2 = dctype.Phone2;
                post.Education = dctype.Education;
                post.Experience = dctype.Experience;
                post.Backgroundcheck = dctype.Backgroundcheck;
                post.Salarydescription = dctype.Salarydescription;
                post.Perkdescription = dctype.Perkdescription;
                post.Workingfrom = dctype.Workingfrom;
                post.Workingto = dctype.Workingto;
                post.description = dctype.Description;
                post.Details = dctype.Details;
                post.Genderid = dctype.Genderid;
                post.Preferredworktiming = dctype.Preferredworktiming;
                post.Staffratio = dctype.Staffratio;
                post.Showvn = dctype.Showvn;
                post.Showvs = dctype.Showvs;
                post.Milestotravel = dctype.Milestotravel;
                post.Noofkids = dctype.Noofkids;
                post.Noofadults = dctype.Noofadults;
                post.Noofspecialneeds = dctype.Noofspecialneeds;
                post.Salaryrate = dctype.Salaryrate;
                post.Perkscost = dctype.Perkscost;
                post.premiumad = dctype.Premiumad;
                post.availablefrom = dctype.Availablefrom;
                post.Availableto = dctype.Availableto;
                post.Neededdays = dctype.Neededdays;
                post.Salarymode = dctype.Salarymode;
                post.Salarymodeid = dctype.Salarymodeid;
                post.gender = dctype.Gender;
                post.userpid = dctype.Userpid;
                post.Startdate = dctype.Startdate;
                post.Enddate = dctype.Enddate;
                post.Verifieddate = dctype.Verifieddate;
                post.Status = dctype.Status;
                post.Agegroupid = dctype.Agegroupid;
                post.agegroups = dctype.Agegroup;
                post.Supertag = dctype.Supertag;
                post.supertagid =dctype.supercategoryid;
                post.primarycategoryid = dctype.Primarytagid;
                post.Primarytag = dctype.Primarytag;
                post.Cityurl = dctype.Cityurl;
                post.Languageid = dctype.Languageid;
                post.languages = dctype.Language;
                post.Trainingid = dctype.Trainingid;
                post.Training = dctype.Training;
                post.Airportcode = dctype.Airportcode;
                post.Statecode = dctype.Statecode;
                post.Noofpersons = dctype.Noofpersons;
                post.Neededtime = dctype.Neededtime;
                post.Category = dctype.Category;
                post.Statename = dctype.Statename;
                post.Metro = dctype.Metro;
                post.Nooflivefreeads = dctype.Nooflivefreeads;
                post.Freeadposted = dctype.Freeadposted;
                post.Freeliveadid = dctype.Freeliveadid;
                post.Freeliveadtitle = dctype.Freeliveadtitle;
                post.Freeliveadtitleurl = dctype.Freeliveadtitleurl;
                post.Freeliveadnewcityurl = dctype.Freeliveadnewcityurl;
                post.Metrourl = dctype.Metrourl;
                post.Radius = dctype.Radius;
                post.lat = dctype.Lat;
                post.longtitude = dctype.Long;
                post.maincategoryid = dctype.maincategoryid;
                post.Photoid = dctype.Photoid;
                post.photo = dctype.Photos;
                post.Videoid = dctype.Videoid;
                post.videourl = dctype.Videourl;
                post.Externalvideourl = dctype.Externalvideourl;
                post.Britecode = dctype.Britecode;
                post.Age = dctype.Age;
                post.Website = dctype.Website;
                post.Agentid = dctype.Agentid;
                post.newcityurl = dctype.Newcityurl;
                post.Folderid = dctype.Folderid;
                post.ordergroup = dctype.Ordergroup;
                post.ordertype = dctype.Ordertype;
                post.numberofdays = dctype.Numberofdays;
                post.Upgradeordergroup = dctype.Upgradeordergroup;
                post.Upgradeordertype = dctype.Upgradeordertype;
                post.Upgradenumberofdays = dctype.Upgradenumberofdays;
                post.Alladid = dctype.Alladid;
                post.Services = dctype.Services;
                post.Worktype = dctype.Worktype;
                post.Carespecialchild = dctype.Carespecialchild;
                post.Hideemail = dctype.Hideemail;
                post.Hideaddress = dctype.Hideaddress;
                post.Classtypeid = dctype.Classtypeid;
                post.Classtype = dctype.Classtype;
                post.Pettypeid = dctype.Pettypeid;
                post.Pettype = dctype.Pettype;
                post.supercategoryid = dctype.supercategoryid;
                post.supercategoryvalue = dctype.supercategoryvalue;
                post.primarycategoryid = dctype.primarycategoryid;
                post.primarycategoryvalue = dctype.primarycategoryvalue;
                post.secondarycategoryid = dctype.secondarycategoryid;
                post.secondarycategoryvalue = dctype.secondarycategoryvalue;
                post.tertiarycategoryid = dctype.tertiarycategoryid;
                post.tertiarycategoryvalue = dctype.tertiarycategoryvalue;
                post.countrycode = dctype.Countrycode;
                post.Ismobileverified = dctype.Ismobileverified;
                post.Contentid = dctype.Contentid;
                post.Primarycategoryvalueurl = dctype.Primarycategoryvalueurl;
                post.Secondarycategoryvalueurl = dctype.Secondarycategoryvalueurl;
                post.Agentid1 = dctype.Agentid1;
                post.Agentstatus = dctype.Agentstatus;
                post.Totalads = dctype.Totalads;
                post.Adsposted = dctype.Adsposted;
                post.Noofadslive = dctype.Noofadslive;
                post.Noofadsremain = dctype.Noofadsremain;
                post.Licenseno1 = dctype.Licenseno1;
                post.Agentdisplayphone = dctype.Agentdisplayphone;
                post.Agentcity = dctype.Agentcity;
                post.Agentzipcode = dctype.Agentzipcode;
                post.Agentstatecode = dctype.Agentstatecode;
                post.Agentcountry = dctype.Agentcountry;
                post.Agentbusinessname = dctype.Agentbusinessname;
                post.Agentaddress = dctype.Agentaddress;
                post.Agentphone = dctype.Agentphone;
                post.usertype = dctype.Usertype;
                post.Agentemail = dctype.Agentemail;
                post.Agentphotourl = dctype.Agentphotourl;
                post.Agentadordertype = dctype.Agentadordertype;
                post.Packageexpiredate = dctype.Packageexpiredate;
                post.Packageexpiredays = dctype.Packageexpiredays;
                post.Agentmetros = dctype.Agentmetros;
                post.Availability = dctype.Availability;
                post.Amount = dctype.Amount;
                post.Discountamt = dctype.Discountamt;
                post.Paymentamt = dctype.Paymentamt;
                post.Couponcode = dctype.Couponcode;
                post.addonamount = dctype.Addonamount;
                post.Addontype = dctype.Addontype;
                post.Categoryflag = dctype.Categoryflag;
                post.Businessorigin = dctype.Businessorigin;
                post.Adsourceurl = dctype.Adsourceurl;
                post.Packageid = dctype.Packageid;
                post.virtualnanny = dctype.Virtualnanny;
                post.ismyneed = "1";
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public  void gotonext()
        {
            var curpage = GetCurrentPage();
            curpage.Navigation.PushAsync(new ProviderType("3", needdccatlist));
        }
        public async void gotouserdtlpg(Daycareposting postdta)
        {
            try
            {
                var currentpg = GetCurrentPage();
                //await currentpg.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.PostThird(postdta));
                await currentpg.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Location(postdta));
            }
            catch(Exception ex)
            {

            }
        }
        public void gotolisting()
        {
            //var currentpage = GetCurrentPage();
            //currentpage.Navigation.PushAsync(new List.Views.DaycareListing(list));
        }
        //PostFirst
        public async void gotoposting()
        {
           try
            {
                var currentpage = GetCurrentPage();
                if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                {
                    await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                }
                else
                {
                   // dialog.ShowLoading("", null);

                    post.contributor = Commonsettings.UserName;
                    post.emailid = Commonsettings.UserEmail;
                    post.country = Commonsettings.Usercountry;
                    post.state = Commonsettings.Userstatecode;
                    post.zipcode = Commonsettings.Userzipcode;
                    post.city = Commonsettings.Usercity;
                    post.lat = Commonsettings.UserLat;
                    post.longtitude = Commonsettings.UserLong;
                    post.userpid = Commonsettings.UserPid;
                    post.ip = ipaddress;
                    var dcapi = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                    var postfirstdata = await dcapi.postfirstsubmit(post);
                    if (!string.IsNullOrEmpty(postfirstdata.businessid))
                    {
                        post.businessid = postfirstdata.businessid;
                        post.premiumad = postfirstdata.premiumad;
                        post.usertype = postfirstdata.usertype;
                      
                        //var currentpage = GetCurrentPage();
                        await currentpage.Navigation.PushAsync(new Post.Views.PostFirst(post));
                    }
                    else
                    {
                        dialog.Toast("Try again later...");
                    }
                  //  dialog.HideLoading();
                }
            }
            catch(Exception ex)
            {
                dialog.Toast("Some error occurred...try later...");
                dialog.HideLoading();
            }
        }
        public async void DCfirstpage(string Categorylevel, DC_CategoryList dctypes)
        { 
            try
            {
                dialog.ShowLoading("", null);
                var ApiC = RestService.For<ICategory>(Commonsettings.DaycareAPI);
                DCCategoryRowData listoftypes = await ApiC.GetCareTypes(Categorylevel, dctypes.supercategoryid, dctypes.primarycategoryid, dctypes.secondarycategoryid);
                OnPropertyChanged(nameof(GetTypes));
                if (dctypes.ismyneed == "1")
                {
                    MyneedsNxtVisible = true;
                }
                    foreach (var type in listoftypes.ROW_DATA)
                    {
                        if(type.categoryname == dctypes.primarycategoryvalue)
                        {
                            type.textcolor = "#fbaa19";
                        }
                        else
                        {
                            type.textcolor = "#2c2d2f";
                        }
                    }
                //}
                GetTypes = listoftypes.ROW_DATA;
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }

        public async void DCsecondpage(string Categorylevel, DC_CategoryList dctypes)
        {
            try
            {
                dialog.ShowLoading("", null);
                var ApiC = RestService.For<ICategory>(Commonsettings.DaycareAPI);
                DCCategoryRowData listoftypes = await ApiC.GetCareTypes(Categorylevel, dctypes.supercategoryid, dctypes.primarycategoryid, dctypes.secondarycategoryid);
                OnPropertyChanged(nameof(GetTypes));
                foreach(var item in listoftypes.ROW_DATA)
                {
                    item.textcolor = "#2c2d2f";
                }
                if (dctypes.ismyneed == "1")
                {
                    MyneedsNxtVisible = true;
                }
                    foreach (var type in listoftypes.ROW_DATA)
                    {
                        if (type.categoryname == dctypes.secondarycategoryvalue)
                        {
                            type.textcolor = "#fbaa19";
                        }
                        else
                        {
                            type.textcolor = "#2c2d2f";
                        }
                    }
                //}
                GetTypes = listoftypes.ROW_DATA;
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private async void Tapontypes(DC_CategoryList dctypes)
        {
            try
            {
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new Catsecond("2", dctypes));
            }
            catch (Exception e) { }
        }

        public async void DCthirdpage(string Categorylevel, DC_CategoryList dctypes)
        {
            try
            {
                dialog.ShowLoading("", null);
                var ApiC = RestService.For<ICategory>(Commonsettings.DaycareAPI);
                DCCategoryRowData listoftypes = await ApiC.GetCareTypes(Categorylevel,dctypes.supercategoryid, dctypes.primarycategoryid, dctypes.secondarycategoryid);
                if (listoftypes.ROW_DATA.Count > 0)
                {
                    foreach (var lst in listoftypes.ROW_DATA)
                    {
                        //if (lst.secondarycategoryid == secondarycategoryid)
                        //{
                        //    lst.textcolor = "#fbaa19";
                        //}
                        //else
                        //{
                            lst.textcolor = "#2c2d2f";
                        //}
                        if (dctypes.ismyneed == "1")
                        {
                            MyneedsNxtVisible = true;
                        }
                            foreach (var type in listoftypes.ROW_DATA)
                            {
                                if (type.categoryname == dctypes.primarycategoryvalue)
                                {
                                    type.textcolor = "#fbaa19";
                                }
                                else
                                {
                                    type.textcolor = "#2c2d2f";
                                }
                            }
                       // }
                    }
                    var curpage = GetCurrentPage();
                    var catfirstlist = curpage.FindByName<ListView>("CategoryFirstView");
                    catfirstlist.ItemsSource = null;
                    catfirstlist.ItemsSource = listoftypes.ROW_DATA;
                   // OnPropertyChanged(nameof(GetTypes));
                    GetTypes = listoftypes.ROW_DATA;
                }
                else
                {
                    var curpage = GetCurrentPage();
                    if ((post.primarycategoryid == "3" || post.primarycategoryid == "4" || post.primarycategoryid == "5") && (post.secondarycategoryid == "8" || post.secondarycategoryid == "11" || post.secondarycategoryid == "13") && post.supercategoryid == "1")
                    {
                        post.maincategoryid = "1";
                    }
                    else if ((post.primarycategoryid == "4" || post.primarycategoryid == "3") && (post.secondarycategoryid == "12" || post.secondarycategoryid == "9") && post.supercategoryid == "1")
                    {
                        post.maincategoryid = "2";
                    }
                    else if (post.supercategoryid == "2" && post.primarycategoryid == "0" && post.secondarycategoryid == "0" && post.tertiarycategoryid == "0")
                    {
                        post.maincategoryid = "3";
                    }
                    else
                    {
                        post.maincategoryid = "0";
                    }
                    if(needdccatlist.ismyneed=="1")
                    {
                        await curpage.Navigation.PushAsync(new PostFirst(post));
                        curpage.Navigation.RemovePage(curpage.Navigation.NavigationStack[curpage.Navigation.NavigationStack.Count - 2]);
                    }
                    else
                    {
                        await curpage.Navigation.PushAsync(new PostFirst(post));
                        curpage.Navigation.RemovePage(curpage.Navigation.NavigationStack[curpage.Navigation.NavigationStack.Count - 2]);
                    }
                    

                }
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private async void TapCaretype(DC_CategoryList dctypes)
        {
            try
            {
                post.supercategoryid = dctypes.supercategoryid;
                post.supercategoryvalue = dctypes.supercategoryvalue;
                post.primarycategoryid = dctypes.primarycategoryid;
                post.secondarycategoryid = dctypes.secondarycategoryid;
                post.primarycategoryvalue = dctypes.primarycategoryvalue;
                post.secondarycategoryvalue = dctypes.secondarycategoryvalue;
                post.tertiarycategoryid = dctypes.tertiarycategoryid;
                post.tertiarycategoryvalue = dctypes.tertiarycategoryvalue;
                post.maincategoryid = dctypes.maincategoryid;
                //new
                post.categoryurl = dctypes.categoryurl;
                post.newcategoryurl = dctypes.newcategoryurl;
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new ProviderType("3", dctypes));
            }
            catch (Exception e)
            {
                dialog.HideLoading();
            }
        }

        private async void TapCareProviderType(DC_CategoryList types)
        {
            try
            {
                post.supercategoryid = types.supercategoryid;
                post.supercategoryvalue = types.supercategoryvalue;
                post.primarycategoryid = types.primarycategoryid;
                post.secondarycategoryid = types.secondarycategoryid;
                post.primarycategoryvalue = types.primarycategoryvalue;
                post.secondarycategoryvalue = types.secondarycategoryvalue;
                post.tertiarycategoryid = types.tertiarycategoryid;
                post.tertiarycategoryvalue = types.tertiarycategoryvalue;
                post.maincategoryid = types.maincategoryid;
                if(string.IsNullOrEmpty(types.newcategoryurl))
                {
                    types.newcategoryurl = post.newcategoryurl;
                }
                else
                {
                    post.newcategoryurl = types.newcategoryurl;
                }
                if(string.IsNullOrEmpty(types.secondarycategoryvalue))
                {
                    post.secondarycategoryvalue = types.categoryname;
                }
                post.categoryurl = types.categoryurl;

                //if (types.primarycategoryvalue== "Housekeeper" || types.primarycategoryvalue == "Cook" || types.categoryname== "Family Child Care" || post.primarycategoryid=="5")//13-petcare
                //{
                //    post.secondarycategoryvalue = types.categoryname;
                //}

                if (Clevel == 3)
                {
                    if ((Clevel == 3 && types.secondarycategoryid == "8")||(types.primarycategoryid=="4" &&types.secondarycategoryid=="12"))
                    {
                        var curpage = GetCurrentPage();
                        await curpage.Navigation.PushAsync(new WorkType("4", types));
                    }
                    else if (Clevel == 3 && types.secondarycategoryid == "9"&& types.supercategoryid == "2")
                    {
                        //btnvisible = true;
                        //foreach (var lst in GetTypes)
                        //{
                        //    if (lst.secondarycategoryid == types.secondarycategoryid)
                        //    {
                        //        lst.textcolor = "#fbaa19";
                        //    }
                        //    else
                        //    {
                        //        lst.textcolor = "#2c2d2f";
                        //    }
                        //}
                        //var curpage = GetCurrentPage();
                        //var catfirstlist = curpage.FindByName<ListView>("CategoryFirstView");
                        //catfirstlist.ItemsSource = null;
                        //catfirstlist.ItemsSource = GetTypes;
                        gotouserdtlpg(post);
                    }
                    else if((Clevel == 3 && types.secondarycategoryid == "9" && types.supercategoryid=="1")|| types.secondarycategoryid=="32"||types.secondarycategoryid == "11")
                    {
                        var curpage = GetCurrentPage();
                        await curpage.Navigation.PushAsync(new WorkType("4", types));
                    }
                    else 
                    {
                        ////var curpage = GetCurrentPage();
                        ////await curpage.Navigation.PushAsync(new WorkType("4", types.supercategoryid, types.primarycategoryid, types.secondarycategoryid));
                        //btnvisible = true;
                        //foreach (var lst in GetTypes)
                        //{
                        //    if (lst.secondarycategoryid == types.secondarycategoryid)
                        //    {
                        //        lst.textcolor = "#fbaa19";
                        //    }
                        //    else
                        //    {
                        //        lst.textcolor = "#2c2d2f";
                        //    }
                        //}
                        //var curpage = GetCurrentPage();
                        //var catfirstlist = curpage.FindByName<ListView>("CategoryFirstView");
                        //catfirstlist.ItemsSource = null;
                        //catfirstlist.ItemsSource = GetTypes;

                        gotouserdtlpg(post);
                    }
                }
            }
            catch (Exception e)
            {
            }
        }
        private void TapWorkType(DC_CategoryList data)
        {
            try
            {
                var curpage = GetCurrentPage();
                post.supercategoryid = data.supercategoryid;
                post.supercategoryvalue = data.supercategoryvalue;
                post.primarycategoryid = data.primarycategoryid;
                post.secondarycategoryid = data.secondarycategoryid;
                post.primarycategoryvalue = data.primarycategoryvalue;
                post.secondarycategoryvalue = data.secondarycategoryvalue;
                post.tertiarycategoryid = data.tertiarycategoryid;
                post.tertiarycategoryvalue = data.categoryname;
                //foreach (var lst in GetTypes)
                //{
                //    if (lst.tertiarycategoryid == data.tertiarycategoryid)
                //    {
                //        lst.textcolor = "#fbaa19";
                //    }
                //    else
                //    {
                //        lst.textcolor = "#2c2d2f";
                //    }
                //}

                //var catfirstlist = curpage.FindByName<ListView>("CategoryFirstView");
                //catfirstlist.ItemsSource = null;
                //catfirstlist.ItemsSource = GetTypes;
                //worktypebtnvisible = true;
                gotouserdtlpg(post);
            }
            catch (Exception e) 
            {
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
