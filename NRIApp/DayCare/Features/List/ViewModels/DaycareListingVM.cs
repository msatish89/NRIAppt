using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.DayCare.Features.List.Models;
using NRIApp.DayCare.Features.List.Interface;
using Refit;
using Plugin.Connectivity;
using Xamarin.Forms.Extended;
using System.Windows.Input;
using System.Threading.Tasks;

namespace NRIApp.DayCare.Features.List.ViewModels
{
    public class DaycareListingVM:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialogs = UserDialogs.Instance;
        public InfiniteScrollCollection<DClistings> listings { get; set; }
        public ICommand refreshcmd { get; set; }
        public Command sortbycommand { get; set; }
        public ICommand filtercommand { get; set; }
        public Command mileslistngcmd { get; set; }
        public Command responsecmd { get; set; }
        public Command viewnumbercmd { get; set; }
        public Command TapSearchPage { get; set; }
        public Command CareDetailCommand { get; set; }
        public Command savedadcmd { get; set; }
        public static int filtercnt = 0;
        public static int sortingcnt = 0;
        private string _title = "";
        public string title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(nameof(title)); }
        }
        private bool _isrefresh = false;
        public bool isrefresh
        {
            get { return _isrefresh; }
            set { _isrefresh = value; OnPropertyChanged(nameof(isrefresh)); }
        }
        private bool _mileslistingvisible = false;
        public bool mileslistingvisible
        {
            get { return _mileslistingvisible; }
            set { _mileslistingvisible = value; OnPropertyChanged(nameof(mileslistingvisible)); }
        }
        
        public DaycareListingVM(DClistinfo list)
        {
            if (!string.IsNullOrEmpty(list.careprovidertype) && (list.careprovidertype != "cyber-security"))
            {
                string titlename = char.ToUpper(list.careprovidertype[0]) + list.careprovidertype.Substring(1);//list.careprovidertype.Substring(0, 1).ToUpper();
                title = titlename.Replace("-", " ") + " in " + list.city;
                OnPropertyChanged(nameof(title));
            }
            list.isrefreshed = 0;
           if(list.worktype=="19")
            {
                list.worktype = "19,42";
            }
           else if(list.worktype=="20")
            {
                list.worktype = "20,42";
            }
           
            listings = new InfiniteScrollCollection<DClistings>
            {
                OnLoadMore = async () =>
                {
                    var listdta = new List<DClistings>();
                    if (listings.Last().totalrecs!= listings.Count)
                    {
                        try
                        {
                            var currentpage = GetCurrentpage();
                            ActivityIndicator loader = currentpage.FindByName<ActivityIndicator>("listingloader");
                            loader.IsRunning = true;
                            loader.IsVisible = true;
                            list.pageno = listings.Last().pageno + 1;
                            var dclistAPI = RestService.For<IDClistings>(Commonsettings.DaycareAPI);
                            DClistingdata listoftypes = await dclistAPI.getcarelistings(list);
                            if(list.miles=="1")
                            {
                                listoftypes = await dclistAPI.getcaresublistings(list);
                            }
                            if (listoftypes.ROW_DATA != null && listoftypes.ROW_DATA.Count > 0)
                            {
                                foreach (var data in listoftypes.ROW_DATA)
                                {
                                    if (data.secondarycategoryvalue == "Care Center" || data.secondarycategoryvalueurl == "elder-care" || data.secondarycategoryvalue == "Child Care Center" || data.secondarycategoryvalue == "Pet Care Center" || data.secondarycategoryvalue == "Elder Care Center" || data.secondarycategoryvalue == "Pet Care Provider")
                                    {
                                        data.daycarecentervisible = true;
                                        data.otherservicevisible = false;
                                        if (!string.IsNullOrEmpty(data.xml_eligibility))
                                        {
                                            data.xml_eligibility = data.xml_eligibility.Replace("<answers>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("</answer>", "");
                                            if (data.xml_eligibility == "Yes")
                                            {
                                                data.xml_eligibility = "License provided";
                                            }
                                            else
                                            {
                                                data.xml_eligibility = "License not provided";
                                            }
                                        }


                                        if (!string.IsNullOrEmpty(data.availability))
                                        {
                                            data.availabilityvisible = true;
                                            data.availability = data.availability.Replace("::", "-");
                                        }
                                        else
                                        {
                                            data.availabilityvisible = false;
                                        }
                                        if (!string.IsNullOrEmpty(data.xml_experience))
                                        {
                                            data.xml_experience = data.xml_experience.Replace("<answers>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("</answer>", "");
                                            if (!string.IsNullOrEmpty(data.xml_experience))
                                            {
                                                data.yearvisible = true;
                                            }
                                            else
                                            {
                                                data.worktypevisible = true;
                                            }
                                        }
                                        else
                                        {
                                            data.worktypevisible = true;
                                        }
                                        if (!string.IsNullOrEmpty(data.tertiarycategoryvalue))
                                        {
                                            data.worktypevisible = true;
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(data.secondarycategoryvalue))
                                            {
                                                data.tertiarycategoryvalue = data.secondarycategoryvalue;
                                                data.worktypevisible = true;
                                            }
                                            else
                                            {
                                                data.worktypevisible = false;
                                            }
                                        }
                                        if (!string.IsNullOrEmpty(data.dynamiccallpin))
                                        {
                                            data.callpinvisible = true;
                                            data.dynamiccallpin = "(Pin: " + data.dynamiccallpin + ")";
                                        }
                                        if (!string.IsNullOrEmpty(data.xml_languages))
                                        {
                                            data.xml_languages = data.xml_languages.Replace("</answer><answer>", ",").Replace("<answers>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("</answer>", "");
                                            if (!string.IsNullOrEmpty(data.xml_languages))
                                            {
                                                data.languagevisible = true;
                                            }
                                            else
                                            {
                                                data.languagevisible = false;
                                            }
                                        }
                                        else
                                        {
                                            data.languagevisible = false;
                                        }
                                        if (data.avgrating == "1")
                                        {
                                            data.ratingimg = "Star1.png";
                                        }
                                        else if (data.avgrating == "2")
                                        {
                                            data.ratingimg = "Star2.png";
                                        }
                                        else if (data.avgrating == "3")
                                        {
                                            data.ratingimg = "Star3.png";
                                        }
                                        else if (data.avgrating == "4")
                                        {
                                            data.ratingimg = "Star4.png";
                                        }
                                        else if (data.avgrating == "5")
                                        {
                                            data.ratingimg = "Star5.png";
                                        }
                                        else
                                        {
                                            data.ratingimg = "Star0.png";
                                        }
                                        if (data.secondarycategoryvalue == "Pet Care Center")
                                        {
                                            data.worktypetxt = "Services Offered : " + data.servicetags;
                                            data.tertiarycategoryvalue = data.servicetags;
                                            data.nopetcarelayout = false;
                                            data.petcarelayout = true;
                                            data.phone = "Contact : " + data.phone;

                                            data.availabilityvisible = false;
                                            data.languagevisible = false;
                                            data.ageservedvisible = false;
                                        }
                                        else
                                        {
                                            data.worktypetxt = "Work Type";
                                            data.nopetcarelayout = true;
                                            data.petcarelayout = false;
                                        }
                                        if (data.secondarycategoryvalue == "Elder Care Center")
                                        {
                                            data.petcarelayout = true;
                                            data.worktypetxt = "Services Offered : " + data.servicetags;
                                        }
                                    }
                                    else
                                    {
                                        data.daycarecentervisible = false;
                                        data.otherservicevisible = true;
                                        if (!string.IsNullOrEmpty(data.xml_response))
                                        {
                                            data.xml_response = "Average response : " + data.xml_response.Replace("<answers>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("</answer>", "");
                                        }
                                        if (!string.IsNullOrEmpty(data.xml_experience))
                                        {
                                            data.xml_experience = data.xml_experience.Replace("<answers>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("</answer>", "");
                                            if (data.xml_experience != "" && data.xml_experience != "Beginner")
                                            {
                                                if (data.xml_experience == "1")
                                                {
                                                    data.xml_experience = data.xml_experience + " year";
                                                }
                                                else
                                                {
                                                    data.xml_experience = data.xml_experience + " years";
                                                }
                                            }

                                        }
                                        else
                                        {
                                            data.xml_experience = "Beginner";
                                        }
                                        if (!string.IsNullOrEmpty(data.xml_gender))
                                        {
                                            data.xml_gender = data.xml_gender.Replace("<answers>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("</answer>", "");
                                            if (!string.IsNullOrEmpty(data.xml_gender))
                                            {
                                                data.gendervisible = true;
                                            }
                                            else
                                            {
                                                data.gendervisible = false;
                                            }
                                        }
                                        else
                                        {
                                            data.gendervisible = false;
                                        }
                                        if (!string.IsNullOrEmpty(data.xml_certificates))
                                        {
                                            data.xml_certificates = data.xml_certificates.Replace(",", "").Replace(",<answer><answer>", "").Replace("</answer><answer>", ",").Replace("<answers>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("</answer>", "");
                                            if (!string.IsNullOrEmpty(data.xml_certificates) && data.xml_certificates != "No Certification")
                                            {
                                                data.certificationvisible = true;
                                            }
                                            else
                                            {
                                                data.certificationvisible = false;
                                            }
                                        }
                                        else
                                        {
                                            data.certificationvisible = false;
                                        }
                                        if (!string.IsNullOrEmpty(data.xml_languages))
                                        {
                                            data.xml_languages = data.xml_languages.Replace("</answer><answer>", ",").Replace("<answers>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("</answer>", "").Replace("addmore-", "");

                                            if (!string.IsNullOrEmpty(data.xml_languages))
                                            {
                                                data.languagevisible = true;
                                            }
                                            else
                                            {
                                                data.languagevisible = false;
                                            }
                                        }
                                        else
                                        {
                                            data.languagevisible = false;
                                        }
                                        if (!string.IsNullOrEmpty(data.salarymode) && !string.IsNullOrEmpty(data.salaryrate))
                                        {
                                            data.salaryvisible = true;
                                            data.salarymode = "/" + data.salarymode;
                                            data.salaryrate = "$" + data.salaryrate.Replace(".0000", "").Replace(".00", "");
                                        }
                                        if (data.supercategoryvalue == "I offer Care")
                                        {
                                            data.fromtext = "Available from: ";
                                        }
                                        else
                                        {
                                            data.fromtext = "Needed from: ";
                                        }
                                        if (!string.IsNullOrEmpty(data.tertiarycategoryvalue))
                                        {
                                            data.worktypevisible = true;
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(data.secondarycategoryvalue))
                                            {
                                                data.worktypevisible = true;
                                                data.tertiarycategoryvalue = data.secondarycategoryvalue;
                                            }
                                            else
                                            {
                                                data.worktypevisible = false;
                                            }
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(data.agegroupid))
                                    {
                                        string[] ageid = data.agegroupid.Split(',');
                                        List<string> ageyrtxt = new List<string>();
                                        foreach (var id in ageid)
                                        {
                                            if (id == "58")
                                            {
                                                ageyrtxt.Add("0-6-months");
                                            }
                                            if (id == "62")
                                            {
                                                ageyrtxt.Add("12+ Years");
                                            }
                                            if (id == "209")
                                            {
                                                ageyrtxt.Add("5 to 7 Years");
                                            }
                                            if (id == "59")
                                            {
                                                ageyrtxt.Add("6 Months-3 Years");
                                            }
                                            if (id == "60")
                                            {
                                                ageyrtxt.Add("3 to 5 Years");
                                            }
                                            if (id == "61")
                                            {
                                                ageyrtxt.Add("7 to 11 Years");
                                            }

                                        }
                                        data.agegrouplistvisible = true;
                                        data.agegrouptxt = string.Join(" / ", ageyrtxt);
                                    }
                                    else
                                    {
                                        data.agegrouplistvisible = false;
                                    }
                                    if (string.IsNullOrEmpty(data.xml_response))
                                    {
                                        data.xml_response = data.postedago;
                                    }
                                    if (list.orderby == "distance" && !string.IsNullOrEmpty(data.distance) && data.distance != "0")
                                    {
                                        data.distVisible = true;
                                        data.distance = Math.Round(Convert.ToDecimal(data.distance)).ToString() + " Miles";
                                    }

                                    if (!string.IsNullOrEmpty(data.virtualnanny))
                                    {
                                        if (data.virtualnanny != "0")
                                        {
                                            data.virtualnannyvisible = true;
                                        }
                                        else
                                        {
                                            data.virtualnannyvisible = false;
                                        }
                                    }
                                    else
                                    {
                                        data.virtualnannyvisible = false;
                                    }
                                }
                            }
                            listdta = listoftypes.ROW_DATA;
                            loader.IsRunning = false;
                            loader.IsVisible = false;
                            //IsBusy = false;
                        }
                        catch(Exception ex)
                        {
                            dialogs.HideLoading();
                            var currentpage = GetCurrentpage();
                            ActivityIndicator loader = currentpage.FindByName<ActivityIndicator>("listingloader");
                            loader.IsRunning = false;
                            loader.IsVisible = false;
                            //IsBusy = false;
                        }
                    }
                    return listdta;
                }
             };
            //var currentpge = GetCurrentpage();
            //ActivityIndicator ldr = currentpge.FindByName<ActivityIndicator>("listingloader");
            //ldr.IsRunning = false;
            //ldr.IsVisible = false;
            getDClistings(list);
            sortbycommand = new Command(async () => await gotosortbypg(list));
            filtercommand =new Command(async () => await gotofilterpg(list));
           // filtercommand = new Command(() =>{ gotofilterpg(list);});
            //refreshcmd = new Command(async () => await getDClistings(list));
            //refreshcmd = new Command(async () => await refresh(list));
            refreshcmd = new Command(async () => await refresh());
            responsecmd = new Command<DClistings>(gotoresposeform);
            viewnumbercmd = new Command<DClistings>(gotoviewnumberform);
            TapSearchPage = new Command(async () => await ClickSearchPage(list));
            CareDetailCommand=new Command<DClistings>(gotocaredetail);
            savedadcmd = new Command(async () => await Taponsavedjobs(list));
            mileslistngcmd = new Command(async () => await gotomileslistingpg(list));
        }
        //public async Task refresh(DClistinfo list)

        public async Task refresh()
        {
            //list.isrefreshed = 1;
            //await getDClistings(list);
            dialogs.ShowLoading("",null);
            await Task.Delay(300);
            isrefresh = false;
            dialogs.HideLoading();
        }
        public static int logincode = 0;
        public async Task Taponsavedjobs(DClistinfo listdata)
        {
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                var currentpage = GetCurrentpage();
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                logincode = 1;
            }
            else
            {
                logincode = 0;
                var currentpage = GetCurrentpage();
                await currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.List.Views.Savedads());
            }
        }
        public async void gotocaredetail(DClistings dtl)
        {
            try
            {
                var currentpage = GetCurrentpage();
                if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                {
                     await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                }
                else
                {
                    await currentpage.Navigation.PushAsync(new DayCare.Features.Detail.Views.CareDetail(dtl.businessid,dtl.newclickurl,dtl.premiumad));
                }
               
            }
            catch(Exception ex)
            {

            }
        }
        public async Task gotofilterpg(DClistinfo list)
        {
            var currentpage = GetCurrentpage();
            if (filtercnt == 0 && Commonsettings.UserMobileOS == "android")
            {
                filtercnt = 1;
                await currentpage.Navigation.PushModalAsync(new DayCare.Features.List.Views.Filter_dc(list));
            }
            else
            {
                await currentpage.Navigation.PushModalAsync(new DayCare.Features.List.Views.Filter_dc(list));
            }
        }
        public async Task gotomileslistingpg(DClistinfo list)
        {
            var currentpage = GetCurrentpage();
            await currentpage.Navigation.PushAsync(new DayCare.Features.List.Views.DaycareListing_Miles(list));
        }
        public async Task ClickSearchPage(DClistinfo listres)
        {
            var curpage = GetCurrentpage();
            await curpage.Navigation.PushAsync(new Views.Search_DC(listres));
        }
        public async void gotoresposeform(DClistings data)
        {
            dialogs.ShowLoading("", null);
            string email = Commonsettings.UserEmail;
            var currentpage = GetCurrentpage();
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
            }
            else
            {
                await currentpage.Navigation.PushAsync(new Views.ResponseForm(data, "response"));
            }
            dialogs.HideLoading();
        }
        public async void gotoviewnumberform(DClistings data)
        {
            var currentpage = GetCurrentpage();
            dialogs.ShowLoading("", null);
            string email = Commonsettings.UserEmail;
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
            }
            else
            {
                await currentpage.Navigation.PushAsync(new Views.ViewNumberForm(data, "viewnumber"));
            }
            dialogs.HideLoading();
        }
        private int _filterlistcount = 0;
        public int filterlistcount
        {
            get { return _filterlistcount; }
            set { _filterlistcount = value;OnPropertyChanged(nameof(filterlistcount)); }
        }
        public async Task getDClistings(DClistinfo list)
        {
           try
            {
                var connected = CrossConnectivity.Current.IsConnected;
                await Task.Delay(300);
                var currentpage = GetCurrentpage();
                
                if (connected)
                {
                    dialogs.ShowLoading("", null);
                    var dclistAPI = RestService.For<IDClistings>(Commonsettings.DaycareAPI);
                    if (string.IsNullOrEmpty(list.cityurl))
                    {
                        list.cityurl = Commonsettings.Usercityurl;
                        list.userlat = Commonsettings.UserLat.ToString();
                        list.userlong = Commonsettings.UserLong.ToString();
                    }
                    if(string.IsNullOrEmpty(list.nearby))
                    {
                        list.nearby = "city";
                    }
                    if (string.IsNullOrEmpty(list.orderby))
                        list.orderby = "bizrunrate";
                    //if (string.IsNullOrEmpty(list.pageno.ToString())|| list.pageno==0)
                        list.pageno = 1;
                 
                    DClistingdata listoftypes = await dclistAPI.getcarelistings(list);
                    if(listoftypes!=null)
                    {
                        if (list.miles == "1" || listoftypes.ROW_DATA.Count < 10)
                        {
                            DClistingdata listoftypesmiles = await dclistAPI.getcaresublistings(list);
                            if(listoftypesmiles != null && listoftypesmiles.ROW_DATA.Count!=0)
                            {
                                mileslistingvisible = true;
                            }
                            if (list.miles == "1" && listoftypesmiles.ROW_DATA.Count != 0)
                            {
                                mileslistingvisible = false;
                                listoftypes = await dclistAPI.getcaresublistings(list);
                            }
                        }
                    }
                    OnPropertyChanged(nameof(listings));
                    if(listoftypes.ROW_DATA!=null && listoftypes.ROW_DATA.Count>0)
                    {
                        foreach (var data in listoftypes.ROW_DATA)
                        {
                            if (data.secondarycategoryvalue == "Care Center"|| data.secondarycategoryvalueurl == "elder-care" || data.secondarycategoryvalue == "Child Care Center" || data.secondarycategoryvalue== "Pet Care Center" || data.secondarycategoryvalue == "Elder Care Center"||data.secondarycategoryvalue== "Pet Care Provider")
                            {
                                data.daycarecentervisible = true;
                                data.otherservicevisible = false;
                                if(!string.IsNullOrEmpty(data.xml_eligibility))
                                {
                                    data.xml_eligibility = data.xml_eligibility.Replace("<answers>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("</answer>", "");
                                    if (data.xml_eligibility == "Yes")
                                    {
                                        data.xml_eligibility = "License provided";
                                    }
                                    else
                                    {
                                        data.xml_eligibility = "License not provided";
                                    }
                                }
                                
                               
                                if(!string.IsNullOrEmpty(data.availability))
                                {
                                    data.availabilityvisible = true;
                                    data.availability = data.availability.Replace("::", "-");
                                }
                                else
                                {
                                    data.availabilityvisible = false;
                                }
                                if(!string.IsNullOrEmpty(data.xml_experience))
                                {
                                    data.xml_experience = data.xml_experience.Replace("<answers>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("</answer>", "");
                                    if (!string.IsNullOrEmpty(data.xml_experience))
                                    {
                                        data.yearvisible = true;
                                    }
                                    else
                                    {
                                        data.worktypevisible = true;
                                    }
                                }
                                else
                                {
                                    data.worktypevisible = true;
                                }
                                if(!string.IsNullOrEmpty(data.tertiarycategoryvalue))
                                {
                                    data.worktypevisible = true;
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(data.secondarycategoryvalue))
                                    {
                                        data.tertiarycategoryvalue = data.secondarycategoryvalue;
                                        data.worktypevisible = true;
                                    }
                                    else
                                    {
                                        data.worktypevisible = false;
                                    }
                                }
                                if (!string.IsNullOrEmpty(data.dynamiccallpin))
                                {
                                    data.callpinvisible = true;
                                    data.dynamiccallpin = "(Pin: " + data.dynamiccallpin + ")";
                                }
                                if(!string.IsNullOrEmpty(data.xml_languages))
                                {
                                    data.xml_languages = data.xml_languages.Replace("</answer><answer>", ",").Replace("<answers>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("</answer>", "");
                                    if (!string.IsNullOrEmpty(data.xml_languages))
                                    {
                                        data.languagevisible = true;
                                    }
                                    else
                                    {
                                        data.languagevisible = false;
                                    }
                                }
                                else
                                {
                                    data.languagevisible = false;
                                }
                                if (data.avgrating=="1")
                                {
                                    data.ratingimg = "Star1.png";
                                }
                                else if (data.avgrating == "2")
                                {
                                    data.ratingimg = "Star2.png";
                                }
                                else if (data.avgrating == "3")
                                {
                                    data.ratingimg = "Star3.png";
                                }
                                else if (data.avgrating == "4")
                                {
                                    data.ratingimg = "Star4.png";
                                }
                                else if (data.avgrating == "5")
                                {
                                    data.ratingimg = "Star5.png";
                                }
                                else 
                                {
                                    data.ratingimg = "Star0.png";
                                }
                                if(data.secondarycategoryvalue == "Pet Care Center")
                                {
                                    data.worktypetxt = "Services Offered : "+data.servicetags;
                                    data.tertiarycategoryvalue = data.servicetags;
                                    data.nopetcarelayout = false;
                                    data.petcarelayout = true;
                                    data.phone = "Contact : "+data.phone;

                                    data.availabilityvisible = false;
                                    data.languagevisible = false;
                                    data.ageservedvisible = false;
                                }
                                else
                                {
                                    data.worktypetxt = "Work Type";
                                    data.nopetcarelayout = true;
                                    data.petcarelayout = false;
                                }
                                if(data.secondarycategoryvalue == "Elder Care Center")
                                {
                                    data.petcarelayout = true;
                                    data.worktypetxt = "Services Offered : " + data.servicetags;
                                }
                            }
                            else
                            {
                                data.daycarecentervisible = false;
                                data.otherservicevisible = true;
                                if (!string.IsNullOrEmpty(data.xml_response))
                                {
                                    data.xml_response = "Average response : " + data.xml_response.Replace("<answers>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("</answer>", "");
                                }
                                if (!string.IsNullOrEmpty(data.xml_experience))
                                {
                                    data.xml_experience = data.xml_experience.Replace("<answers>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("</answer>", "");
                                    if (data.xml_experience != "" && data.xml_experience != "Beginner")
                                    {
                                        if (data.xml_experience == "1")
                                        {
                                            data.xml_experience = data.xml_experience + " year";
                                        }
                                        else
                                        {
                                            data.xml_experience = data.xml_experience + " years";
                                        }
                                    }
                                    
                                }
                                else
                                {
                                    data.xml_experience = "Beginner";
                                }
                                if (!string.IsNullOrEmpty(data.xml_gender))
                                {
                                    data.xml_gender = data.xml_gender.Replace("<answers>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("</answer>", "");
                                    if(!string.IsNullOrEmpty(data.xml_gender))
                                    {
                                        data.gendervisible = true;
                                    }
                                    else
                                    {
                                        data.gendervisible = false;
                                    }
                                }
                                else
                                {
                                    data.gendervisible = false;
                                }
                                if (!string.IsNullOrEmpty(data.xml_certificates))
                                {
                                    data.xml_certificates = data.xml_certificates.Replace(",","").Replace(",<answer><answer>", "").Replace("</answer><answer>", ",").Replace("<answers>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("</answer>", "");
                                    if(!string.IsNullOrEmpty(data.xml_certificates) && data.xml_certificates!= "No Certification")
                                    {
                                        data.certificationvisible = true;
                                    }
                                    else
                                    {
                                        data.certificationvisible = false;
                                    }
                                }
                                else
                                {
                                    data.certificationvisible = false;
                                }
                                if (!string.IsNullOrEmpty(data.xml_languages))
                                {
                                    data.xml_languages = data.xml_languages.Replace("</answer><answer>", ",").Replace("<answers>", "").Replace("<answer>", "").Replace("</answers>", "").Replace("</answer>", "").Replace("addmore-","");

                                    if(!string.IsNullOrEmpty(data.xml_languages))
                                    {
                                        data.languagevisible = true;
                                    }
                                    else
                                    {
                                        data.languagevisible = false;
                                    }
                                }
                                else
                                {
                                    data.languagevisible = false;
                                }
                                if(!string.IsNullOrEmpty(data.salarymode) && !string.IsNullOrEmpty(data.salaryrate))
                                {
                                    data.salaryvisible = true;
                                    data.salarymode = "/" + data.salarymode;
                                    data.salaryrate = "$" + data.salaryrate.Replace(".0000", "").Replace(".00", "");
                                }
                                if (data.supercategoryvalue == "I offer Care")
                                {
                                    data.fromtext = "Available from: ";
                                }
                                else
                                {
                                    data.fromtext = "Needed from: ";
                                }
                                if (!string.IsNullOrEmpty(data.tertiarycategoryvalue))
                                {
                                    data.worktypevisible = true;
                                }
                                else
                                {
                                    if(!string.IsNullOrEmpty(data.secondarycategoryvalue))
                                    {
                                        data.worktypevisible = true;
                                        data.tertiarycategoryvalue = data.secondarycategoryvalue;
                                    }
                                    else
                                    {
                                        data.worktypevisible = false;
                                    }
                                }
                            }
                            if(!string.IsNullOrEmpty(data.agegroupid))
                            {
                                string[] ageid = data.agegroupid.Split(',');
                                List<string> ageyrtxt=new List<string>();
                                foreach(var id in ageid)
                                {
                                    if(id== "58")
                                    {
                                        ageyrtxt.Add("0-6-months");
                                    }
                                    if (id == "62")
                                    {
                                        ageyrtxt.Add("12+ Years");
                                    }
                                    if (id == "209")
                                    {
                                        ageyrtxt.Add("5 to 7 Years");
                                    }
                                    if (id == "59")
                                    {
                                        ageyrtxt.Add("6 Months-3 Years");
                                    }
                                    if(id=="60")
                                    {
                                        ageyrtxt.Add("3 to 5 Years");
                                    }
                                    if (id == "61")
                                    {
                                        ageyrtxt.Add("7 to 11 Years");
                                    }

                                }
                                data.agegrouplistvisible = true;
                                data.agegrouptxt = string.Join(" / ", ageyrtxt);
                            }
                            else
                            {
                                data.agegrouplistvisible = false;
                            }
                           if(string.IsNullOrEmpty(data.xml_response))
                            {
                                data.xml_response = data.postedago;
                            }
                            if(list.orderby=="distance" && !string.IsNullOrEmpty(data.distance)&&data.distance!="0")
                            {
                                data.distVisible = true;
                                data.distance = Math.Round(Convert.ToDecimal(data.distance)).ToString()+ " Miles";
                            }

                            if(!string.IsNullOrEmpty(data.virtualnanny))
                            {
                                if(data.virtualnanny !="0")
                                {
                                    data.virtualnannyvisible = true;
                                }
                                else
                                {
                                    data.virtualnannyvisible = false;
                                }
                            }
                            else
                            {
                                data.virtualnannyvisible = false;
                            }
                        }
                        Label filtcnt = currentpage.FindByName<Label>("filtrcnt");
                        filtcnt.Text = list.filtercount.ToString();
                        StackLayout nolistcnt = currentpage.FindByName<StackLayout>("nolistblk");
                        nolistcnt.IsVisible = false;
                        ListView lstng = currentpage.FindByName<ListView>("listdata");
                        lstng.IsVisible = true;
                        lstng.ItemsSource = null;
                        lstng.ItemsSource = listings;
                        OnPropertyChanged(nameof(listings));
                        //if(list.isrefreshed!=1)
                        //{
                            listings?.AddRange(listoftypes.ROW_DATA);
                        //}
                      
                    }
                    else
                    {
                        Label filtcnt = currentpage.FindByName<Label>("filtrcnt");
                        filtcnt.Text = list.filtercount.ToString();
                        StackLayout nointernet = currentpage.FindByName<StackLayout>("nointernet");
                        nointernet.IsVisible = false;
                        StackLayout nolistcnt = currentpage.FindByName<StackLayout>("nolistblk");
                        nolistcnt.IsVisible = true;
                        ListView lstng = currentpage.FindByName<ListView>("listdata");
                        lstng.IsVisible = false;
                    }
                    dialogs.HideLoading();
                    isrefresh = false;
                }
                else
                {
                    if (connected == false)
                    {
                        StackLayout nointernet = currentpage.FindByName<StackLayout>("nointernet");
                        nointernet.IsVisible = true;
                        StackLayout nolistcnt = currentpage.FindByName<StackLayout>("nolistblk");
                        nolistcnt.IsVisible = false;
                        ListView lstng = currentpage.FindByName<ListView>("listdata");
                        lstng.IsVisible = false;
                    }
                    else
                    {
                        await getDClistings(list);
                    }
                }
                isrefresh = false;
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
                isrefresh = false;
            }
                
        }
        public async Task gotosortbypg(DClistinfo list)
        {
           try
            {
                var currentpage = GetCurrentpage();
                if (sortingcnt == 0)
                {
                    sortingcnt = 1;
                    await currentpage.Navigation.PushModalAsync(new DayCare.Features.List.Views.Sortby_dc(list));
                }
            }
            catch(Exception ex)
            {

            }
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page GetCurrentpage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}
