using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Jobresume.Interfaces;
using NRIApp.LocalJobs.Features.Jobresume.Models;
using NRIApp.LocalJobs.Features.Listing.Models;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using Plugin.Connectivity;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace NRIApp.LocalJobs.Features.Jobresume.ViewModels
{
   
    public class SavedresumeVM : INotifyPropertyChanged
    {
        IUserDialogs dialogs = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Savedadlist_DATA> jobslists { get; set; }
        public Command<Savedadlist_DATA> postresponsecmd { get; set; }
        public Command<Savedadlist_DATA> DetailjobsCommand { get; set; }
        public Command<Savedadlist_DATA> removesavedlist { get; set; }

        public Command Taptodownload { get; set; }

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
        private bool _postedago = false;
        public bool postedago
        {
            get { return _postedago; }
            set { _postedago = value; OnPropertyChanged(nameof(postedago)); }
        }
        private bool _nojobssaved = false;
        public bool nojobssaved
        {
            get { return _nojobssaved; }
            set { _nojobssaved = value; OnPropertyChanged(nameof(nojobssaved)); }
        }

        public SavedresumeVM()
        {
            getjoblists();
            DetailjobsCommand = new Command<Savedadlist_DATA>(getjobsdetail);
            postresponsecmd = new Command<Savedadlist_DATA>(postresponse);
            Taptodownload = new Command<Savedadlist_DATA>(downloadresume);
            removesavedlist = new Command<Savedadlist_DATA>(deleteadfromsavedlist);
        }
        public async void getjoblists()
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected)
            {
                try
                {
                    dialogs.ShowLoading("");
                    nointernet = false;
                    //listdata.pageno = 1;
                    var savedadsAPI = RestService.For<IJobresume>(Commonsettings.LocaljobsAPI);
                    var savedadslist = await savedadsAPI.getsavedadlist(Commonsettings.UserPid, "100");
                    if (savedadslist.ROW_DATA.Count > 0)
                    {
                        foreach (var data in savedadslist.ROW_DATA)
                        {
                            if (Math.Round(data.MINSAL, 1) == 0 && Math.Round(data.MAXSAL, 1) == 0)
                            {
                                data.salary = "Best in Industry";
                                data.salarymode = "";
                            }
                            else
                            {
                                data.MINSAL = Math.Round(data.MINSAL, 1);
                                data.MAXSAL = Math.Round(data.MAXSAL, 1);
                                data.salary = "$" + data.MINSAL + " - " + "$" + data.MAXSAL;
                                if (data.salarymode != null)
                                {
                                    if (data.salarymode.ToLower() == "hourly")
                                        data.salarymode = "Hour";
                                    if (data.salarymode.ToLower() == "yearly")
                                        data.salarymode = "Year";
                                    if (data.salarymode.ToLower() == "weekly")
                                        data.salarymode = "Week";
                                    if (data.salarymode.ToLower() == "monthly")
                                        data.salarymode = "Month";
                                    data.salarymode = "/ " + data.salarymode;
                                    data.salary = "$" + data.MINSAL + " - " + "$" + data.MAXSAL + data.salarymode;
                                }

                            }
                            data.crdate = "Posted " + data.crdate;
                        }
                        OnPropertyChanged(nameof(jobslists));

                    }
                    else
                    {
                        nojobssaved = true;
                        nointernet = false;
                        var currentpage = GetCurrentPage();
                        ListView lstng = currentpage.FindByName<ListView>("JobssListview");
                        lstng.IsVisible = false;
                    }
                    jobslists = savedadslist.ROW_DATA;
                    dialogs.HideLoading();
                }
                catch (Exception ex)
                {
                    dialogs.HideLoading();
                }
            }
            else
            {
                nointernet = true;
            }
        }
        public static int saveresumelogincode = 0;
        public async void downloadresume(Savedadlist_DATA data)
        {
            try
            {
                string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                //https://techjobs.sulekha.com/Mobileapp/localjobs
                if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                {
                    var currentpage = GetCurrentPage();
                    await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                    saveresumelogincode = 1;
                }
                else
                {
                    dialogs.ShowLoading("", null);
                    var resumedownloadapi = RestService.For<IJobresume>("http://localjobs.sulekha.com/mobileappresume");
                    var downloaddata = await resumedownloadapi.downloadresumeurl(Commonsettings.UserPid, Commonsettings.UserMobileno, Commonsettings.UserEmail, data.Jobresumeid, ipaddress);
                    dialogs.HideLoading();
                    downloaddata.result = "expired";
                    if (downloaddata.result == "sucess" && !string.IsNullOrEmpty(downloaddata.resumepath))
                    {
                        var alert = await Application.Current.MainPage.DisplayAlert("Available Resume Count  " + downloaddata.availiableresumes, "", "Ok", " ");
                        // Device.OpenUri(new Uri(downloaddata.resumepath));
                        Device.OpenUri(new Uri(downloaddata.resumepath));
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
            }
        }
        public async void getjobsdetail(Savedadlist_DATA detail)
        {
            var currentpage = GetCurrentPage();
            string userpid = Commonsettings.UserPid;
            //await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Jobresume.Views.ResumeDetail(detail.adid, detail.titleurl, userpid));
        }
        public void postresponse(Savedadlist_DATA pstresponse)
        {
            var currentpage = GetCurrentPage();
            currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Detail.Views.Apply_Form(pstresponse.adid, pstresponse.premiumad.ToString(), pstresponse.resumemandatory.ToString(), ""));
        }
        public async void getsavedads()
        {
            try
            {
                if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                {
                    var currentpage = GetCurrentPage();
                    await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                }
                else
                {
                    var savedadsAPI = RestService.For<IJobresume>(Commonsettings.LocaljobsAPI);
                    var savedadslist = await savedadsAPI.getsavedadlist(Commonsettings.UserPid, "100");
                }
            }
            catch(Exception ex)
            {


            }
        }
        public async void deleteadfromsavedlist(Savedadlist_DATA data)
        {
           try
            {
                dialogs.ShowLoading("", null);
                if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                {
                    var currentpage = GetCurrentPage();
                    await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                }
                else
                {
                    var deleteadsAPI = RestService.For<IJobresume>(Commonsettings.LocaljobsAPI);
                    var deleteads = await deleteadsAPI.deletesavedad(data.Jobresumeid, Commonsettings.UserPid);
                    if (deleteads.result == "deleted successfully")
                    {
                        //  getjoblists();
                        var currentpage = GetCurrentPage();
                        currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                        await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Jobresume.Views.Savedresumes());
                    }
                }
                dialogs.HideLoading();
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
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
