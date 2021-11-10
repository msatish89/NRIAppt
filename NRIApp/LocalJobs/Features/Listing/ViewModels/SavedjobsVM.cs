using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Listing.Interfaces;
using NRIApp.LocalJobs.Features.Listing.Models;
using Plugin.Connectivity;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Extended;
using Acr.UserDialogs;
namespace NRIApp.LocalJobs.Features.Listing.ViewModels
{
    public class SavedjobsVM : INotifyPropertyChanged
    {
        IUserDialogs dialogs = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Savedadlist_DATA> jobslists { get; set; }
        public Command<Savedadlist_DATA> postresponsecmd { get; set; }
        public Command<Savedadlist_DATA> DetailjobsCommand { get; set; }
        public Command<Savedadlist_DATA> removesavedlist { get; set; }
        
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
        
        public SavedjobsVM()
        {
            getjoblists();
            DetailjobsCommand = new Command<Savedadlist_DATA>(getjobsdetail);
            postresponsecmd = new Command<Savedadlist_DATA>(postresponse);
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
                    var savedadsAPI = RestService.For<ILocalJob>(Commonsettings.LocaljobsAPI);
                    var savedadslist = await savedadsAPI.getsavedadlist(Commonsettings.UserPid, "100");
                    if (savedadslist.ROW_DATA.Count > 0)
                    {
                        foreach(var data in savedadslist.ROW_DATA)
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
                                if(data.salarymode!=null)
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
                catch(Exception ex)
                {
                    dialogs.HideLoading();
                }
            }
            else
            {
                nointernet = true;
            }
        }
        public async void getjobsdetail(Savedadlist_DATA detail)
        {
            var currentpage = GetCurrentPage();
            string userpid = Commonsettings.UserPid;
            await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Detail.Views.JobDetails(detail.adid, detail.titleurl, userpid));
        }
        public void postresponse(Savedadlist_DATA pstresponse)
        {
            var currentpage = GetCurrentPage();
            currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Detail.Views.Apply_Form(pstresponse.adid, pstresponse.premiumad.ToString(), pstresponse.resumemandatory.ToString(), ""));
        }
        public async void getsavedads()
        {
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                var currentpage = GetCurrentPage();
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
            }
            else
            {
                var savedadsAPI = RestService.For<ILocalJob>(Commonsettings.LocaljobsAPI);
                var savedadslist = await savedadsAPI.getsavedadlist(Commonsettings.UserPid, "100");
            }
        }
        public async void deleteadfromsavedlist(Savedadlist_DATA data)
        {
            dialogs.ShowLoading("", null);
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                var currentpage = GetCurrentPage();
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
            }
            else
            {
                var deleteadsAPI = RestService.For<ILocalJob>(Commonsettings.LocaljobsAPI);
                var deleteads = await deleteadsAPI.deletesavedadlist(data.adid, Commonsettings.UserPid);
                if(deleteads.issavedad =="0")
                {
                  //  getjoblists();
                    var currentpage = GetCurrentPage();
                    currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                    await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Listing.Views.Savedjobs());
                }
            }
            dialogs.HideLoading();
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
