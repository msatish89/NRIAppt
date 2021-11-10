using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Listing.Interfaces;
using NRIApp.LocalJobs.Features.Listing.Models;
using NRIApp.LocalJobs.Features.Listing.Views;
using Plugin.Connectivity;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace NRIApp.LocalJobs.Features.Listing.ViewModels
{
    public class JoblistVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialogs = UserDialogs.Instance;
        public static int filtercnt = 0;
        public static int sortingcnt = 0;
        public InfiniteScrollCollection<LocalJobResponse> jobslists { get; set; }
        public ICommand sortbycommand { get; set; }
        public ICommand filtercommand { get; set; }
        public Command TapSearchPage { get; set; }
        public Command Retrycmd { get; set; }
        public Command savedjobcmd { get; set; }
        public Command Tapjobalert { get; set; }
        public Command recruiterpgcmd { get; set; }
        public Command gotopostpagecmd { get; set; }
        public ICommand refreshcmd { get; set; }

        public Command<LocalJobResponse> postresponsecmd { get; set; }
        public Command<LocalJobResponse> DetailjobsCommand { get; set; }

        private bool _IsBusy = false;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { _IsBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }
        private bool _nointernet = false;
        public bool nointernet
        {
            get { return _nointernet; }
            set { _nointernet = value; OnPropertyChanged(nameof(nointernet)); }
        }
        private bool _Nolisting = false;
        public bool Nolisting
        {
            get { return _Nolisting; }
            set { _Nolisting = value; OnPropertyChanged(nameof(Nolisting)); }
        }
        private bool _jobslistview;
        public bool jobslistview
        {
            get { return _jobslistview; }
            set { _jobslistview = value; OnPropertyChanged(nameof(jobslistview)); }
        }
        private bool _savedjobvisible=true;
        public bool savedjobvisible
        {
            get { return _savedjobvisible; }
            set { _savedjobvisible = value; OnPropertyChanged(nameof(savedjobvisible)); }
        }
        private bool _listpgjobalert = false;
        public bool listpgjobalert
        {
            get { return _listpgjobalert; }
            set { _listpgjobalert = value; OnPropertyChanged(nameof(listpgjobalert)); }
        }
        private int _filtercount = 0;
        public int filtercount
        {
            get { return _filtercount; }
            set { _filtercount = value;OnPropertyChanged(nameof(filtercount)); }
        }
        private bool _filtercntvisible = false;
        public bool filtercntvisible
        {
            get { return _filtercntvisible; }
            set { _filtercntvisible = value;OnPropertyChanged(nameof(filtercntvisible)); }
        }
       LocalJobResponse data = new LocalJobResponse();
        public JoblistVM(LocalJobSenddata listdata)
        {
            try
            {
                //if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                //{
                //    var currentpage = GetCurrentPage();
                //    currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                //}
                //else
                //{
               // filtercount = listdata.filtercount;
                //if(filtercount!=0)
                //{
                //    filtercntvisible = true;
                //}
                    listdata.searchtext = "";
                    jobslists = new InfiniteScrollCollection<LocalJobResponse>
                    {
                        OnLoadMore = async () =>
                        {
                            var list = new List<LocalJobResponse>();
                            if (jobslists.Last().totalrecs != jobslists.Count)
                            {
                                try
                                {
                                    var currentpage = GetCurrentPage();
                                    ActivityIndicator jobloader = currentpage.FindByName<ActivityIndicator>("listingloader");
                                    jobloader.IsRunning = true;
                                    jobloader.IsVisible = true;
                                    // listdata.pageno = 1;
                                    //  listdata.rowstofetch = 100;
                                    listdata.pageno = jobslists.Last().pageno + 1;
                                    IsBusy = true;
                                    var jobAPI = RestService.For<ILocalJob>(Commonsettings.LocaljobsAPI);
                                    var response = await jobAPI.Getjoblist(listdata);

                                    if (response.ROW_DATA.Count > 0)
                                    {
                                        Nolisting = false;
                                        jobslistview = true;

                                        foreach (var data in response.ROW_DATA)
                                        {
                                            data.Ddate = data.adposteddate.ToString("dd MMM yyyy");
                                            if (data.adtype == "1")
                                            {
                                                data.adtype = "Offered";
                                            }
                                            else
                                            {
                                                data.adtype = "Wanted";
                                            }
                                            if (data.hideaddress == 1)
                                            {
                                                data.addressvisible = false;
                                            }
                                            else
                                            {
                                                data.addressvisible = true;
                                            }
                                            if (!string.IsNullOrEmpty(data.businessname))
                                            {
                                                data.businessVisible = true;
                                            }
                                            if ((string.IsNullOrEmpty(data.experiencefrom) || data.experiencefrom == "0") && (string.IsNullOrEmpty(data.experienceto) || data.experienceto == "0"))
                                            {
                                                data.experience = "Freshers";
                                            }
                                            else
                                            {
                                                data.experience = data.experiencefrom + " - " + data.experienceto + " Years";
                                            }
                                            data.miles = Math.Round(data.miles, 1);
                                            if (data.miles != 0)
                                            {
                                                data.distVisible = true;
                                                //  data.distance = Math.Round(data.distance, 1);
                                                data.distancedata = "Distance :" + " " + data.miles + " " + "Miles";
                                            }
                                            else
                                            {
                                                data.distVisible = false;
                                            }
                                            if (Math.Round(data.minsal, 1) == 0 && Math.Round(data.maxsal, 1) == 0)
                                            {
                                                data.salary = "Best in Industry";
                                                data.salarymode = "";
                                            }
                                            else
                                            {
                                                data.minsal = Math.Round(data.minsal, 1);
                                                data.maxsal = Math.Round(data.maxsal, 1);
                                                data.salary = "$" + data.minsal + " - " + "$" + data.maxsal;
                                                if (data.salarymode.ToLower() == "hourly")
                                                    data.salarymode = "Hour";
                                                if (data.salarymode.ToLower() == "yearly")
                                                    data.salarymode = "Year";
                                                if (data.salarymode.ToLower() == "weekly")
                                                    data.salarymode = "Week";
                                                if (data.salarymode.ToLower() == "monthly")
                                                    data.salarymode = "Month";
                                                data.salarymode = "/ " + data.salarymode;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        Nolisting = true;
                                        jobslistview = false;
                                    }
                                    jobloader.IsRunning = false;
                                    jobloader.IsVisible = false;
                                    list = response.ROW_DATA;
                                    IsBusy = false;
                                }
                                catch (Exception e)
                                {
                                    string msg = e.Message;
                                }
                            }
                            return list;
                        }
                    };
                    sortbycommand = new Command(async () => await TapOnsortbycommand(listdata));
                    filtercommand = new Command(async () => await Taponfiltercommand(listdata));
                    DetailjobsCommand = new Command<LocalJobResponse>(getjobsdetail);
                    postresponsecmd = new Command<LocalJobResponse>(postresponse);
                    TapSearchPage = new Command(async () => await ClickSearchPage(listdata));
                    Retrycmd = new Command(() => { Taponretry(listdata); });
                    savedjobcmd = new Command(async () => await Taponsavedjobs(listdata));
                    Tapjobalert = new Command(gotojobalert);
                recruiterpgcmd = new Command(gotorecruiterpage);
                gotopostpagecmd = new Command(gotopostpage);
                // refreshcmd = new Command<LocalJobSenddata>(getjoblists);
                refreshcmd = new Command(async () => await getjoblists(listdata));
                    //TapSearchPage = new Command(async () => await ClickSearchPage(listdata.cityurl,listdata.userlat,listdata.userlong));
                    getjoblists(listdata);
                //}
            }
            catch (Exception e)
            {
                string msg = e.Message;
            }
        }
        Posting.Models.Postingdata jdata = new Posting.Models.Postingdata();
        public async void gotorecruiterpage()
        {
         try
            {
                var currentpage = GetCurrentPage();
                Xamarin.Forms.Page page1 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1];
              //  Xamarin.Forms.Page page2 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 2];
                currentpage.Navigation.RemovePage(page1);
                //currentpage.Navigation.RemovePage(page2);
                await currentpage.Navigation.PushAsync(new LocalJobs.Features.Posting.Views.Recruiter());
            }
            catch(Exception ex)
            {

            }
        }
        public async void gotopostpage()
        {
         try
            {
                var currentpage = GetCurrentPage();
                Xamarin.Forms.Page page1 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1];
                Xamarin.Forms.Page page2 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 2];
                Xamarin.Forms.Page page3 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 3];
                currentpage.Navigation.RemovePage(page1);
                currentpage.Navigation.RemovePage(page2);
                currentpage.Navigation.RemovePage(page3);
                await currentpage.Navigation.PushAsync(new LocalJobs.Features.Posting.Views.Jobtype(jdata));
            }
            catch (Exception ex)
            {

            }
        }
        public async void gotojobalert()
        {
            var currentpage = GetCurrentPage();

            await currentpage.Navigation.PushAsync(new Detail.Views.Jobalert());
        }
        public static int logincode = 0;
        public  async Task Taponsavedjobs(LocalJobSenddata listdata)
        {
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                var currentpage = GetCurrentPage();
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                logincode = 1;
            }
            else
            {
                logincode = 0;
                var currentpage = GetCurrentPage();
                await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Listing.Views.Savedjobs());
            }
            
        }
        public void Taponretry(LocalJobSenddata listdata)
        {
            getjoblists(listdata);
        }
       
        public async Task ClickSearchPage(LocalJobSenddata listres)
        {
            var curpage = GetCurrentPage();
            await curpage.Navigation.PushAsync(new Jobsearch(listres));
        }
        private bool _isrefresh = false;
        public bool isrefresh
        {
            get { return _isrefresh; }
            set { _isrefresh = value;OnPropertyChanged(nameof(isrefresh)); }
        }
        public async Task getjoblists(LocalJobSenddata listdata)
        {
           try
            {
                var connected = CrossConnectivity.Current.IsConnected;
                if (connected)
                {
                       dialogs.ShowLoading("");
                    isrefresh = false;
                        nointernet = false;
                        listdata.pageno = 1;
                   
                    var jobAPI = RestService.For<ILocalJob>(Commonsettings.LocaljobsAPI);
                        var response = await jobAPI.Getjoblist(listdata);

                        if (response.ROW_DATA.Count > 0)
                        {
                            foreach (var data in response.ROW_DATA)
                            {
                                data.Ddate = data.adposteddate.ToString("dd MMM yyyy");

                                if (data.adtype == "1")
                                {
                                    data.adtype = "Offered";
                                }
                                else
                                {
                                    data.adtype = "Wanted";
                                }
                                if (data.hideaddress == 1)
                                {
                                    data.addressvisible = false;
                                }
                                else
                                {
                                    data.addressvisible = true;
                                }
                                if (!string.IsNullOrEmpty(data.businessname))
                                {
                                    data.businessVisible = true;
                                }
                                if ((string.IsNullOrEmpty(data.experiencefrom) || data.experiencefrom == "0") && (string.IsNullOrEmpty(data.experienceto) || data.experienceto == "0"))
                                {
                                    data.experience = "Freshers";
                                }
                                else
                                {
                                    data.experience = data.experiencefrom + " - " + data.experienceto + " Years";
                                }
                                data.miles = Math.Round(data.miles, 1);
                                if (data.miles != 0)
                                {
                                    data.distVisible = true;
                                    //  data.distance = Math.Round(data.distance, 1);
                                    data.distancedata = "Distance :" + " " + data.miles + " " + "Miles";
                                }
                                else
                                {
                                    data.distVisible = false;
                                }
                                if (Math.Round(data.minsal, 1) == 0 && Math.Round(data.maxsal, 1) == 0)
                                {
                                    data.salary = "Best in Industry";
                                    data.salarymode = "";
                                }
                                else
                                {
                                    data.minsal = Math.Round(data.minsal, 1);
                                    data.maxsal = Math.Round(data.maxsal, 1);
                                    data.salary = "$" + data.minsal + " - " + "$" + data.maxsal;
                                    if (data.salarymode.ToLower() == "hourly")
                                        data.salarymode = "Hour";
                                    if (data.salarymode.ToLower() == "yearly")
                                        data.salarymode = "Year";
                                    if (data.salarymode.ToLower() == "weekly")
                                        data.salarymode = "Week";
                                    if (data.salarymode.ToLower() == "monthly")
                                        data.salarymode = "Month";
                                    data.salarymode = "/ " + data.salarymode;
                                }
                            }

                            var currentpage = GetCurrentPage();
                      
                        StackLayout nolistcnt = currentpage.FindByName<StackLayout>("stacknoblk");
                            nolistcnt.IsVisible = false;
                            ListView lstng = currentpage.FindByName<ListView>("JobssListview");
                            lstng.IsVisible = true;
                            ListView JobssListviewlst = currentpage.FindByName<ListView>("JobssListview");
                            // jobslists = response.ROW_DATA;
                            JobssListviewlst.ItemsSource = jobslists;
                            OnPropertyChanged(nameof(jobslists));
                            jobslists?.AddRange(response.ROW_DATA);
                      

                    }
                        else
                        {
                            var currentpage = GetCurrentPage();
                            StackLayout nolistcnt = currentpage.FindByName<StackLayout>("stacknoblk");
                            nolistcnt.IsVisible = true;
                            ListView lstng = currentpage.FindByName<ListView>("JobssListview");
                            lstng.IsVisible = false;
                            //Nolisting = true;
                            //jobslistview = false;
                        }
                    var currentpg = GetCurrentPage();
                    filtercount = listdata.filtercount;
                  
                        Frame filtercntframe = currentpg.FindByName<Frame>("filtercntframe");
                        filtercntvisible = true;
                    
                        Label filtrcnt = currentpg.FindByName<Label>("filtrcnt");
                        filtrcnt.Text = listdata.filtercount.ToString();
                  
                    dialogs.HideLoading();
                    isrefresh = false; 
                    //job alert label
                    //listpgjobalert = true;
                    //await Task.Delay(5000);
                    //listpgjobalert = false;
                }
            }
            catch(Exception ex)
            {
                isrefresh = false; 
                dialogs.HideLoading();
            }

        }
        
        public async void deleteadfromsavedlist()
        {
            try
            {
                if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                {
                    var currentpage = GetCurrentPage();
                    await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                    await Task.Delay(50);
                    if(Commonsettings.UserPid!="0" & Commonsettings.UserPid != null && Commonsettings.UserPid != "")
                    {
                        await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Listing.Views.Savedjobs());
                    }
                }
                else
                {
                    var deleteadsAPI = RestService.For<ILocalJob>(Commonsettings.LocaljobsAPI);
                    var deleteads = await deleteadsAPI.deletesavedadlist("", Commonsettings.UserPid);
                }
            }
            catch(Exception ex)
            {

            }
        }
        public void postresponse(LocalJobResponse pstresponse)
        {
            var currentpage = GetCurrentPage(); 
            currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Detail.Views.Apply_Form(pstresponse.adid,pstresponse.premiumad.ToString(), pstresponse.resumemandatory.ToString(),""));
        }
        public async void getjobsdetail(LocalJobResponse detailrmlist)
        {
            var currentpage = GetCurrentPage();
            string userpid = Commonsettings.UserPid;
            await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Detail.Views.JobDetails(detailrmlist.adid,detailrmlist.titleurl,userpid));
        }
        public async Task TapOnsortbycommand(LocalJobSenddata listdata)
        {
            var currentpage = GetCurrentPage();
            if(sortingcnt == 0)
            {
                sortingcnt = 1;
                await currentpage.Navigation.PushModalAsync(new SortbyJobs(listdata));
            }
           // await currentpage.Navigation.PushModalAsync(new SortbyJobs(listdata));
        }
        public async Task Taponfiltercommand(LocalJobSenddata listdata)
        {
            var currentpage = GetCurrentPage();
            if (filtercnt == 0 && Commonsettings.UserMobileOS == "android")
            {
                filtercnt = 1;
                await currentpage.Navigation.PushModalAsync(new FilterJobs(listdata));
            }
            else
            {
                await currentpage.Navigation.PushModalAsync(new FilterJobs(listdata));
            }
            
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page GetCurrentPage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}
