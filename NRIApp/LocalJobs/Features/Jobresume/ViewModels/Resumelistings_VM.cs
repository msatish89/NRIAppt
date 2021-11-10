using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Jobresume.Interfaces;
using NRIApp.LocalJobs.Features.Jobresume.Models;
using NRIApp.LocalJobs.Features.Listing.Models;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using Plugin.Connectivity;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Extended;

namespace NRIApp.LocalJobs.Features.Jobresume.ViewModels
{
    public class Resumelistings_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialogs = UserDialogs.Instance;
        public static int filtercnt = 0;
        public static int sortingcnt = 0;
        public static int isdownloadresumecnt = 0;
        public InfiniteScrollCollection<LocalJobresumeResponse> jobslists { get; set; }
        public Command sortbycommand { get; set; }
        public Command filtercommand { get; set; }
        public Command TapSearchPage { get; set; }
        public Command Retrycmd { get; set; }
        public Command savedjobcmd { get; set; }
        public Command Tapjobalert { get; set; }
        public Command recruiterpgcmd { get; set; }
        public Command gotopostpagecmd { get; set; }
        public Command refreshcmd { get; set; }
        public Command Taptodownload { get; set; }

        public Command previewcmd { get; set; }
        public Command<LocalJobresumeResponse> postresponsecmd { get; set; }
        public Command<LocalJobresumeResponse> viewprofilecmd { get; set; }

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
        private bool _savedjobvisible = true;
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
            set { _filtercount = value; OnPropertyChanged(nameof(filtercount)); }
        }
        private bool _filtercntvisible = false;
        public bool filtercntvisible
        {
            get { return _filtercntvisible; }
            set { _filtercntvisible = value; OnPropertyChanged(nameof(filtercntvisible)); }
        }
        LocalJobresumeResponse data = new LocalJobresumeResponse();
        LocalJobSenddata copylistdata = new LocalJobSenddata();
        public Resumelistings_VM(LocalJobSenddata listdata)
        {
            try
            {
                listdata.searchtext = "";
                copylistdata = listdata;
                jobslists = new InfiniteScrollCollection<LocalJobresumeResponse>
                {
                    OnLoadMore = async () =>
                    {
                        var list = new List<LocalJobresumeResponse>();
                        if (jobslists.Last().Totalrecs != jobslists.Count)
                        {
                            try
                            {
                                var currentpage = GetCurrentPage();
                                ActivityIndicator jobloader = currentpage.FindByName<ActivityIndicator>("listingloader");
                                jobloader.IsRunning = true;
                                jobloader.IsVisible = true;
                                // listdata.pageno = 1;
                                //  listdata.rowstofetch = 100;
                                listdata.pageno = jobslists.Last().Pageno + 1;
                                IsBusy = true;
                                var jobAPI = RestService.For<IJobresume>(Commonsettings.LocaljobsAPI);
                                var response = await jobAPI.Getjoblist(listdata);

                                if (response.ROW_DATA.Count > 0)
                                {
                                    Nolisting = false;
                                    jobslistview = true;

                                    foreach (var data in response.ROW_DATA)
                                    {
                                        if (!string.IsNullOrEmpty(data.Experience))
                                        {
                                            data.experiencevisible = true;
                                            if (data.Experience == "1")
                                            {
                                                data.Experience = "Experience : " + data.Experience + " year";
                                            }
                                            else if (data.Experience == "0")
                                            {
                                                data.Experience = "Experience : Fresher";
                                            }
                                            else
                                            {
                                                data.Experience = "Experience : " + data.Experience + " years";
                                            }
                                        }
                                        else
                                        {
                                            data.experiencevisible = false;
                                        }

                                        if (!string.IsNullOrEmpty(data.Skills))
                                        {
                                            data.skillsvisible = true;
                                            data.Skills = data.Skills.Replace(",", " , ");
                                        }
                                        else
                                        {
                                            data.skillsvisible = false;
                                        }
                                        if (string.IsNullOrEmpty(data.Photo))
                                        {
                                            data.Photo = "ProfileUserIcon.png";
                                        }
                                        if (!string.IsNullOrEmpty(data.Industry))
                                        {
                                            data.industryvisible = true;
                                        }
                                        else
                                        {
                                            data.industryvisible = false;
                                        }

                                        if (string.IsNullOrEmpty(data.Education))
                                        {
                                            data.educationvisible = false;
                                        }
                                        else
                                        {
                                            data.educationvisible = true;
                                        }

                                        if (!string.IsNullOrEmpty(data.Jobprofileid) && data.Jobprofileid != "0")
                                        {
                                            data.profilevisible = true;
                                        }
                                        else
                                        {
                                            data.profilevisible = false;
                                        }

                                        if (!string.IsNullOrEmpty(data.Isdownloadedresume) && data.Isdownloadedresume == "1")
                                        {
                                            data.downloadvisible = false;
                                        }
                                        else
                                        {
                                            data.downloadvisible = true;
                                        }
                                        if (!string.IsNullOrEmpty(data.Statecode))
                                        {
                                            data.City = data.City + ", " + data.Statecode;
                                        }
                                        else
                                        {
                                            data.City = data.City;
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
                //sortbycommand = new Command(async () => await TapOnsortbycommand(listdata));
                filtercommand = new Command(async () => await Taponfiltercommand(listdata));
                viewprofilecmd = new Command<LocalJobresumeResponse>(getjobsdetail);
                postresponsecmd = new Command<LocalJobresumeResponse>(postresponse);
                TapSearchPage = new Command(async () => await ClickSearchPage(listdata));
                Retrycmd = new Command(() => { Taponretry(listdata); });
                savedjobcmd = new Command(async () => await Taponsavedjobs(listdata));
                Tapjobalert = new Command(gotojobalert);
                recruiterpgcmd = new Command(gotorecruiterpage);
                gotopostpagecmd = new Command(gotopostpage);
                // refreshcmd = new Command<LocalJobSenddata>(getjoblists);
                refreshcmd = new Command(async () => await getjoblists(listdata));
                //TapSearchPage = new Command(async () => await ClickSearchPage(listdata.cityurl,listdata.userlat,listdata.userlong));
                Taptodownload = new Command<LocalJobresumeResponse>(downloadresume);
                previewcmd = new Command<LocalJobresumeResponse>(previewresume);
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
               // Xamarin.Forms.Page page1 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1];
                //  Xamarin.Forms.Page page2 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 2];
               // currentpage.Navigation.RemovePage(page1);
                //currentpage.Navigation.RemovePage(page2);
                await currentpage.Navigation.PushAsync(new LocalJobs.Features.Posting.Views.Recruiter());
            }
            catch (Exception ex)
            {

            }
        }
        public async void gotopostpage()
        {
            try
            {
                var currentpage = GetCurrentPage();
                //Xamarin.Forms.Page page1 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1];
                //Xamarin.Forms.Page page2 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 2];
                //Xamarin.Forms.Page page3 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 3];
                //currentpage.Navigation.RemovePage(page1);
                //currentpage.Navigation.RemovePage(page2);
                //currentpage.Navigation.RemovePage(page3);
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
        public async Task Taponsavedjobs(LocalJobSenddata listdata)
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
                await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Jobresume.Views.Savedresumes());
            }

        }
        public void Taponretry(LocalJobSenddata listdata)
        {
            getjoblists(copylistdata);
        }

        public async Task ClickSearchPage(LocalJobSenddata listres)
        {
            var curpage = GetCurrentPage();
            await curpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Jobresume.Views.Jobrolesearch(listres));
        }
        private bool _isrefresh = false;
        public bool isrefresh
        {
            get { return _isrefresh; }
            set { _isrefresh = value; OnPropertyChanged(nameof(isrefresh)); }
        }
        public async Task getjoblists(LocalJobSenddata listdata)
        {
            try
            {
                isdownloadresumecnt = 0;
                var connected = CrossConnectivity.Current.IsConnected;
                if (connected)
                {
                    dialogs.ShowLoading("");
                    isrefresh = false;
                    nointernet = false;
                    listdata.pageno = 1;

                    var jobAPI = RestService.For<IJobresume>(Commonsettings.LocaljobsAPI);
                    var response = await jobAPI.Getjoblist(listdata);
                    if (response.ROW_DATA.Count > 0)
                    {
                        foreach (var data in response.ROW_DATA)
                        {
                            if(!string.IsNullOrEmpty(data.Experience))
                            {
                                data.experiencevisible = true;
                                if (data.Experience == "1")
                                {
                                    data.Experience = "Experience : " + data.Experience + " year";
                                }
                                else if(data.Experience == "0")
                                {
                                    data.Experience = "Experience : Fresher" ;
                                }
                                else
                                {
                                    data.Experience = "Experience : " + data.Experience + " years";
                                }
                            }
                            else
                            {
                                data.experiencevisible = false;
                            }

                            if(!string.IsNullOrEmpty(data.Skills))
                            {
                                data.skillsvisible = true;
                                data.Skills = data.Skills.Replace(",", " , ");
                            }
                            else
                            {
                                data.skillsvisible = false;
                            }
                            if(string.IsNullOrEmpty(data.Photo))
                            {
                                data.Photo = "ProfileUserIcon.png"; 
                            }
                            if(!string.IsNullOrEmpty(data.Industry))
                            {
                                data.industryvisible = true;
                            }
                            else
                            {
                                data.industryvisible = false;
                            }

                            if(string.IsNullOrEmpty(data.Education))
                            {
                                data.educationvisible = false;
                            }
                            else
                            {
                                data.educationvisible = true;
                            }

                            if(!string.IsNullOrEmpty(data.Jobprofileid)&&data.Jobprofileid!="0" )
                            {
                                data.profilevisible = true;
                            }
                            else
                            {
                                data.profilevisible = false;
                            }

                            if(!string.IsNullOrEmpty(data.Isdownloadedresume) && data.Isdownloadedresume=="1")
                            {
                                data.downloadvisible = false;
                            }
                            else
                            {
                                data.downloadvisible = true;
                            }
                            if(!string.IsNullOrEmpty(data.Statecode))
                            {
                                data.City = data.City + ", " + data.Statecode;
                            }
                            else
                            {
                                data.City = data.City;
                            }
                            
                        }

                        var currentpage = GetCurrentPage();
                        StackLayout nolistcnt = currentpage.FindByName<StackLayout>("stacknoblk");
                        nolistcnt.IsVisible = false;
                        ListView lstng = currentpage.FindByName<ListView>("JobsresumeListview");
                        lstng.IsVisible = true;
                        ListView JobssListviewlst = currentpage.FindByName<ListView>("JobsresumeListview");
                        //jobslists = response.ROW_DATA;
                        JobssListviewlst.ItemsSource = jobslists;
                        OnPropertyChanged(nameof(jobslists));
                        jobslists?.AddRange(response.ROW_DATA);
                    }
                    else
                    {
                        var currentpge = GetCurrentPage();
                        StackLayout nolitcnt = currentpge.FindByName<StackLayout>("stacknoblk");
                        nolitcnt.IsVisible = true;
                        ListView lstg = currentpge.FindByName<ListView>("JobsresumeListview");
                        lstg.IsVisible = false;
                        //Nolisting = true;
                        //jobslistview = false;
                    }
                }
                
                    var currentpg = GetCurrentPage();
                    filtercount = listdata.filtercount;
                    Frame filtercntframe = currentpg.FindByName<Frame>("filtercntframe");
                    filtercntvisible = true;
                    Label filtrcnt = currentpg.FindByName<Label>("filtrcnt");
                    filtrcnt.Text = listdata.filtercount.ToString();
                    dialogs.HideLoading();
                    isrefresh = false;
            }
            catch (Exception ex)
            {
                isrefresh = false;
                dialogs.HideLoading();
            }

        }

        public async void deleteadfromsavedlist()
        {
            try
            {
               // isdownloadresumecnt = 0;
                if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                {
                    var currentpage = GetCurrentPage();
                    await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                    await Task.Delay(50);
                    if (Commonsettings.UserPid != "0" & Commonsettings.UserPid != null && Commonsettings.UserPid != "")
                    {
                        await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Listing.Views.Savedjobs());
                    }
                }
                else
                {
                    var deleteadsAPI = RestService.For<IJobresume>(Commonsettings.LocaljobsAPI);
                    var deleteads = await deleteadsAPI.deletesavedad("", Commonsettings.UserPid);
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void postresponse(LocalJobresumeResponse pstresponse)
        {
            var currentpage = GetCurrentPage();
            //currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Detail.Views.Apply_Form(pstresponse.adid, pstresponse.premiumad.ToString(), pstresponse.resumemandatory.ToString(), ""));
        }
        public async void getjobsdetail(LocalJobresumeResponse detailrmlist)
        {
            //isdownloadresumecnt = 0;
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Jobresume.Views.ResumeDetail(detailrmlist.Jobprofileid,detailrmlist.Emailid));
        }

        public async Task Taponfiltercommand(LocalJobSenddata listdata)
        {
           // isdownloadresumecnt = 0;
            var currentpage = GetCurrentPage();
            if (filtercnt == 0 && Commonsettings.UserMobileOS == "android")
            {
                filtercnt = 1;
                await currentpage.Navigation.PushModalAsync(new NRIApp.LocalJobs.Features.Jobresume.Views.Resume_Filter(listdata));
            }
            else
            {
                await currentpage.Navigation.PushModalAsync(new NRIApp.LocalJobs.Features.Jobresume.Views.Resume_Filter(listdata));
            }
        }

        public async void previewresume(LocalJobresumeResponse data)
        {
            try
            {
                dialogs.ShowLoading("", null);
                // isdownloadresumecnt = 0;
                //http://localjobs.sulekha.com/classifiedshtmls/toolresumes/9786010259.html
                if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                {
                    var currentpage = GetCurrentPage();
                    await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                    NRIApp.LocalJobs.Features.Jobresume.Views.Previewresume.resumepreviewlogincode = 1;
                }
                else
                {
                    var resumedownloadapi = RestService.For<IJobresume>(Commonsettings.LocaljobsAPI);
                    var downloaddata = await resumedownloadapi.previewresume(Commonsettings.UserPid, Commonsettings.UserEmail, data.Contentid, Commonsettings.UserName);
                    var currentpage = GetCurrentPage();
                    if (downloaddata != null)
                    {
                        if (downloaddata.availiableresumes == "fail")
                        {
                            var alert = await Application.Current.MainPage.DisplayAlert("To preview the resume, Kindly subscribe to our ", "Resume Package Services.", "Yes", "Back");
                            if (alert)
                            {
                                await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Resumepkgbizdetails());
                            }
                            //else
                            //{
                            //    if (Commonsettings.UserDeviceOSVersion == "android")
                            //    {
                            //        //await currentpage.Navigation.PopAsync();
                            //    }
                            //    else
                            //    {
                            //        currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                            //    }
                            //}
                        }
                        else
                        {
                            await currentpage.Navigation.PushAsync(new LocalJobs.Features.Jobresume.Views.Previewresume(downloaddata.resumepath));
                        }
                    }
                }
                dialogs.HideLoading();
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
            }
        }

        public async void downloadresume(LocalJobresumeResponse data)
        {
            try
            {
                string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                //https://techjobs.sulekha.com/Mobileapp/localjobs
                if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                {
                    var currentpage = GetCurrentPage();
                    await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                    logincode = 1;
                }
                else
                {
                    dialogs.ShowLoading("", null);
                    var resumedownloadapi = RestService.For<IJobresume>("https://localjobs.sulekha.com/mobileappresume");
                    var downloaddata = await resumedownloadapi.downloadresumeurl(Commonsettings.UserPid, Commonsettings.UserMobileno, Commonsettings.UserEmail, data.Contentid, ipaddress);
                    dialogs.HideLoading();
                    //downloaddata.result = "expired";
                    if (downloaddata.result == "sucess" && !string.IsNullOrEmpty(downloaddata.resumepath))
                    {
                        // Device.OpenUri(new Uri(downloaddata.resumepath));
                        var alert = await Application.Current.MainPage.DisplayAlert("Available Resume Count  " + downloaddata.availiableresumes, "", "Ok", " ");
                        //await Task.Delay(500);
                        //alert = true;
                        Device.OpenUri(new Uri(downloaddata.resumepath));
                        //getjoblists(copylistdata);
                        isdownloadresumecnt = 1;
                    }
                    else if (downloaddata.result == "daylimit")
                    {
                        dialogs.Toast("You have downloaded maximum number of resumes (75) in a day!...");
                    }
                    else if (downloaddata.result == "expired")
                    {
                        var alert = await Application.Current.MainPage.DisplayAlert("To download the resume, Kindly subscribe to our ", "Resume Package Services.", "Yes", "Back");
                        if (alert)
                        {
                            var currentpage = GetCurrentPage();
                            await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Resumepkgbizdetails());
                        }
                    }
                    else if (downloaddata.result == "subscribe")
                    {
                        var alert = await Application.Current.MainPage.DisplayAlert("Your ad is expired to download the resume, Kindly subscribe to our ", "Resume Package Services.", "Yes", "Back");
                        if (alert)
                        {
                            var currentpage = GetCurrentPage();
                            await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Resumepkgbizdetails());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
                dialogs.Toast("Could not complete downloading..please try again...");
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
