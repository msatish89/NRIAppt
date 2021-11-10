using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Linq;
using Acr.UserDialogs;
using NRIApp.LocalJobs.Features.Listing.Models;
using NRIApp.Helpers;
using Refit;
using NRIApp.DayCare.Features.List.Interface;
using NRIApp.DayCare.Features.Detail.Interface;
using NRIApp.DayCare.Features.List.Models;
using Plugin.Connectivity;

namespace NRIApp.DayCare.Features.List.ViewModels
{
    public class Savedads_VM:INotifyPropertyChanged
    {
        IUserDialogs dialogs = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Saved_Ads_DATA> daycarelists { get; set; }
        public Command<Saved_Ads_DATA> postresponsecmd { get; set; }
        public Command<Saved_Ads_DATA> DetailcareCommand { get; set; }
        public Command<Saved_Ads_DATA> removesavedlist { get; set; }

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
        DClistings listdata = new DClistings();
        public Savedads_VM()
        {
            getsavedadlists();
            DetailcareCommand = new Command<Saved_Ads_DATA>(getjobsdetail);
            postresponsecmd = new Command<Saved_Ads_DATA>(postresponse);
            removesavedlist = new Command<Saved_Ads_DATA>(deleteadfromsavedlist);
        }
        public async void getsavedadlists()
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected)
            {
                try
                {
                    dialogs.ShowLoading("");
                    nointernet = false;
                    //listdata.pageno = 1;
                    var savedadsAPI = RestService.For<IDClistings>(Commonsettings.DaycareAPI);
                    var savedadslist = await savedadsAPI.getsavedadlist(Commonsettings.UserPid);
                    if (savedadslist.RowData.Count > 0)
                    {
                        foreach (var data in savedadslist.RowData)
                        {
                            if(data.Supercategoryvalue== "I offer Care")
                            {
                                data.Availablefrom = "Available From : " + data.Availablefrom;
                            }
                            else
                            {
                                data.Availablefrom = "Needed From : " + data.Availablefrom;
                            }
                           
                            data.Postedago = "Posted " + data.Postedago;
                        }
                        OnPropertyChanged(nameof(daycarelists));
                    }
                    else
                    {
                        nojobssaved = true;
                        nointernet = false;
                        var currentpage = GetCurrentPage();
                        ListView lstng = currentpage.FindByName<ListView>("careListview");
                        lstng.IsVisible = false;
                    }
                    daycarelists = savedadslist.RowData;
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
        public async void getjobsdetail(Saved_Ads_DATA detail)
        {
            var currentpage = GetCurrentPage();
            string userpid = Commonsettings.UserPid;
            await currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Detail.Views.CareDetail(detail.Businessid, detail.Titleurl,""));
        }
        public void postresponse(Saved_Ads_DATA pstresponse)
        {
            var currentpage = GetCurrentPage();
            currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.List.Views.ResponseForm(listdata, "response"));
        }
       
        public async void deleteadfromsavedlist(Saved_Ads_DATA data)
        {
            dialogs.ShowLoading("", null);
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                var currentpage = GetCurrentPage();
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
            }
            else
            {
                var deleteadsAPI = RestService.For<ICareDetail>(Commonsettings.DaycareAPI);
                var deleteads = await deleteadsAPI.DeleteAdData(data.Businessid, Commonsettings.UserPid);
                if (deleteads.ROW_DATA[0].issavedad == 0)
                {
                    //  getjoblists();
                    var currentpage = GetCurrentPage();
                    currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                    await currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.List.Views.Savedads());
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
