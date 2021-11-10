using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.USHome.Interfaces;
using NRIApp.USHome.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NRIApp.USHome.ViewModels
{
   public class Favorite : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;

        private bool _Servicesavail = false;
        public bool Servicesavail
        {
            get { return _Servicesavail; }
            set { _Servicesavail = value; OnPropertyChanged(nameof(Servicesavail)); }
        }
        private string _Servicesicon = "downarrowGrey.png";
        public string Servicesicon
        {
            get { return _Servicesicon; }
            set { _Servicesicon = value; }
        }
        private bool _Servicedata = false;
        public bool Servicedata
        {
            get { return _Servicedata; }
            set { _Servicedata = value; }
        }


        private bool _Techjobsavail = false;
        public bool Techjobsavail
        {
            get { return _Techjobsavail; }
            set { _Techjobsavail = value; OnPropertyChanged(nameof(Techjobsavail)); }
        }
        private bool _Techjobsdata = false;
        public bool Techjobsdata
        {
            get { return _Techjobsdata; }
            set { _Techjobsdata = value; }
        }

        private string _Techjobsicon = "downarrowGrey.png";
        public string Techjobsicon
        {
            get { return _Techjobsicon; }
            set { _Techjobsicon = value; }
        }
        private bool _Roommatesavail = false;
        public bool Roommatesavail
        {
            get { return _Roommatesavail; }
            set { _Roommatesavail = value; OnPropertyChanged(nameof(Roommatesavail)); }
        }
        private bool _Roommatesdata = false;
        public bool Roommatesdata
        {
            get { return _Roommatesdata; }
            set { _Roommatesdata = value; }
        }
        private string _Roommatesicon = "downarrowGrey.png";
        public string Roommatesicon
        {
            get { return _Roommatesicon; }
            set { _Roommatesicon = value; }
        }
        private bool _Rentalsavail = false;
        public bool Rentalsavail
        {
            get { return _Rentalsavail; }
            set { _Rentalsavail = value; OnPropertyChanged(nameof(Rentalsavail)); }
        }
        private bool _Rentalsdata = false;
        public bool Rentalsdata
        {
            get { return _Rentalsdata; }
            set { _Rentalsdata = value; }
        }
        private string _Rentalsicon = "downarrowGrey.png";
        public string Rentalsicon
        {
            get { return _Rentalsicon; }
            set { _Rentalsicon = value; }
        }
        private bool _Eventsavail = false;
        public bool Eventsavail
        {
            get { return _Eventsavail; }
            set { _Eventsavail = value; OnPropertyChanged(nameof(Eventsavail)); }
        }
        private bool _Eventsdata = false;
        public bool Eventsdata
        {
            get { return _Eventsdata; }
            set { _Eventsdata = value; }
        }
        private string _Eventsicon = "downarrowGrey.png";
        public string Eventsicon
        {
            get { return _Eventsicon; }
            set { _Eventsicon = value; }
        }
        private string _Eventcount = "0";
        public string Eventcount
        {
            get { return _Eventcount; }
            set { _Eventcount = value; }
        }
        private string _Roommatecount = "0";
        public string Roommatecount
        {
            get { return _Roommatecount; }
            set { _Roommatecount = value; }
        }
        private string _Rentalcount = "0";
        public string Rentalcount
        {
            get { return _Rentalcount; }
            set { _Rentalcount = value; }
        }
        private string _Servicecount = "0";
        public string Servicecount
        {
            get { return _Servicecount; }
            set { _Servicecount = value; }
        }
        private string _Techjobscount = "0";
        public string Techjobscount
        {
            get { return _Techjobscount; }
            set { _Techjobscount = value; }
        }

        public List<EVENT_FAVORITE_DATA> Eventsfavorite { get; set; }
        public List<RM_FAVORITE_DATA> Roommatefavorite { get; set; }
        public List<RM_FAVORITE_DATA> Rentalfavorite { get; set; }
        public List<SERVICES_FAVORITE_DATA> Servicesfavorite { get; set; }
        public List<TECHJOBS_FAVORITE_DATA> Techjobsfavorite { get; set; }
        public ICommand Servicesopenclose { get; set; }
        public ICommand Techjobsopenclose { get; set; }
        public ICommand Roommatesopenclose { get; set; }
        public ICommand Rentalsopenclose { get; set; }
        public ICommand Eventsopenclose { get; set; }
        public ICommand Favoritetolsdetail { get; set; }
        public ICommand Favoritetolsrespform { get; set; }
        public ICommand Favoritetolsdelete { get; set; }
        public ICommand Favoritetoeventdetail { get; set; }
        public ICommand Favoritetoeventdelete { get; set; }
        public ICommand Favoritetorentaldetail { get; set; }
        public ICommand Favoritetorentalrespform { get; set; }
        public ICommand Favoritetorentaldelete { get; set; }
        public ICommand Favoritetorommatedetail { get; set; }
        public ICommand Favoritetorommaterespform { get; set; }
        public ICommand Favoritetoroommatedelete { get; set; }
        public ICommand Favoritetotrainingdetail { get; set; }
        public ICommand Favoritetotrainingrespform { get; set; }
        public ICommand Favoritetotrainingdelete { get; set; }
        public Favorite()
        {
            Bindeventfavorites();
            Servicesopenclose = new Command(ClickServicesopenclose);
            Techjobsopenclose = new Command(ClickTechjobsopenclose);
            Roommatesopenclose = new Command(ClickRoommatesopenclose);
            Rentalsopenclose = new Command(ClickRentalsopenclose);
            Eventsopenclose = new Command(ClickEventsopenclose);
            Favoritetolsdetail = new Command<SERVICES_FAVORITE_DATA>(Clicktolsdetail);
            Favoritetolsrespform = new Command<SERVICES_FAVORITE_DATA>(Clicktolsrespform);
            Favoritetolsdelete = new Command<SERVICES_FAVORITE_DATA>(deletelsfavorite);
            Favoritetoeventdetail = new Command<EVENT_FAVORITE_DATA>(clicktoeventdetail);
            Favoritetoeventdelete=new Command<EVENT_FAVORITE_DATA>(deleteeventfavorite);
            Favoritetorentaldetail = new Command<RM_FAVORITE_DATA>(clicktorentaldetail);
            Favoritetorentalrespform = new Command<RM_FAVORITE_DATA>(Clicktorentalrespform);
            Favoritetorentaldelete = new Command<RM_FAVORITE_DATA>(deleterentalFavorite);
            Favoritetorommatedetail= new Command<RM_FAVORITE_DATA>(clicktorommatedetail);
            Favoritetorommaterespform= new Command<RM_FAVORITE_DATA>(Clicktorommaterespform);
            Favoritetoroommatedelete = new Command<RM_FAVORITE_DATA>(deleteroommateFavorite);
            Favoritetotrainingdetail = new Command<TECHJOBS_FAVORITE_DATA>(clicktotechjobsdetail);
            Favoritetotrainingrespform = new Command<TECHJOBS_FAVORITE_DATA>(Clicktotechjobsrespform);
            Favoritetotrainingdelete = new Command<TECHJOBS_FAVORITE_DATA>(deletetechjobsFavorite);
        }

        public async void Bindeventfavorites()
        {
            try
            {

                    dialog.ShowLoading("");

                var Home = RestService.For<IUSHome>(Commonsettings.USHomeAPI);
                var eventsdata = await Home.Geteventsfavorite(Commonsettings.UserPid);
                var roommatesdata = await Home.Getroommateandrentalfavorite(Commonsettings.UserPid,"1");
                var rentaldata = await Home.Getroommateandrentalfavorite(Commonsettings.UserPid, "2");
                var servicedata = await Home.Getservicefavorite(Commonsettings.UserPid);
                var techjobsdata = await Home.Gettechjobsfavorite(Commonsettings.UserPid);
                if (eventsdata.ROW_DATA.Count>0)
                {
                    Eventsavail = true;
                    OnPropertyChanged(nameof(Eventsicon));
                    OnPropertyChanged(nameof(Eventsavail));
                    Eventcount = eventsdata.ROW_DATA.Count.ToString();
                    OnPropertyChanged(nameof(Eventcount));
                    foreach (var item in eventsdata.ROW_DATA)
                    {
                        item.citystatecode = item.city + ", " + item.statecode;
                    }
                    Eventsfavorite = eventsdata.ROW_DATA;
                    OnPropertyChanged(nameof(Eventsfavorite));
                }
                if (roommatesdata.ROW_DATA.Count>0)
                {
                    Roommatesavail = true;
                    Roommatecount = roommatesdata.ROW_DATA.Count.ToString();
                    OnPropertyChanged(nameof(Roommatecount));
                    OnPropertyChanged(nameof(Roommatesavail));
                    OnPropertyChanged(nameof(Roommatesicon));
                    foreach (var item in roommatesdata.ROW_DATA)
                    {
                        item.citystatecode = item.cityname + ", " + item.statecode;
                    }
                    Roommatefavorite = roommatesdata.ROW_DATA;
                    OnPropertyChanged(nameof(Roommatefavorite));
                }
                if (rentaldata.ROW_DATA.Count > 0)
                {
                    Rentalsavail = true;
                    Rentalcount = rentaldata.ROW_DATA.Count.ToString();
                    OnPropertyChanged(nameof(Rentalcount));
                    OnPropertyChanged(nameof(Rentalsavail));
                    OnPropertyChanged(nameof(Rentalsicon));
                    foreach (var item in rentaldata.ROW_DATA)
                    {
                        item.citystatecode = item.cityname + ", " + item.statecode;
                    }
                    Rentalfavorite = rentaldata.ROW_DATA;
                    OnPropertyChanged(nameof(Rentalfavorite));
                }
                if (servicedata.ROW_DATA.Count > 0)
                {
                    Servicesavail = true;
                    Servicecount = servicedata.ROW_DATA.Count.ToString();
                    OnPropertyChanged(nameof(Servicecount));
                    OnPropertyChanged(nameof(Servicesavail));
                    OnPropertyChanged(nameof(Servicesicon));
                    foreach (var item in servicedata.ROW_DATA)
                    {
                        item.citystatecode = item.city + ", " + item.statecode;
                    }
                    Servicesfavorite = servicedata.ROW_DATA;
                    OnPropertyChanged(nameof(Servicesfavorite));
                }
                if (techjobsdata.ROW_DATA.Count>0)
                {
                    Techjobsavail = true;
                    Techjobscount = techjobsdata.ROW_DATA.Count.ToString();
                    OnPropertyChanged(nameof(Techjobscount));
                    OnPropertyChanged(nameof(Techjobsavail));
                    OnPropertyChanged(nameof(Techjobsicon));
                    foreach (var item in techjobsdata.ROW_DATA)
                    {
                        if (item.trainingmode== "classroom")
                        {
                            item.mode = "Classroom";
                        }
                        else if (item.trainingmode== "classroom,online")
                        {
                            item.mode = "Classroom,Online";
                        }
                        else
                        {
                            item.mode = "Online";
                        }
                        
                    }
                    Techjobsfavorite = techjobsdata.ROW_DATA;
                    OnPropertyChanged(nameof(Techjobsfavorite));
                }

                if (Roommatecount == "0" && Rentalcount == "0" && Servicecount == "0" && Eventcount == "0" && Techjobscount == "0")
                {
                    var currentpage = GetCurrentPage();
                    var stackLayout = currentpage.FindByName<StackLayout>("Nolistblk");
                    var listblk = currentpage.FindByName<StackLayout>("favoritelist");
                    stackLayout.IsVisible = true;
                    listblk.IsVisible = false;

                }

                dialog.HideLoading();

            }
            catch (Exception e)
            {

            }
        }

        public void ClickServicesopenclose()
        {
            if (Servicesicon== "downarrowGrey.png")
            {
                Servicesicon = "minusGrey.png";
                Servicedata = true;
            }
            else
            {
                Servicesicon = "downarrowGrey.png";
                Servicedata = false;
            }
            OnPropertyChanged(nameof(Servicesicon));
            OnPropertyChanged(nameof(Servicedata));
        }
        public void ClickTechjobsopenclose()
        {
            if (Techjobsicon == "downarrowGrey.png")
            {
                Techjobsicon = "minusGrey.png";
                Techjobsdata = true;
            }
            else
            {
                Techjobsicon = "downarrowGrey.png";
                Techjobsdata = false;
            }
            OnPropertyChanged(nameof(Techjobsicon));
            OnPropertyChanged(nameof(Techjobsdata));
        }
        public void ClickRoommatesopenclose()
        {
            if (Roommatesicon == "downarrowGrey.png")
            {
                Roommatesicon = "minusGrey.png";
                Roommatesdata = true;
            }
            else
            {
                Roommatesicon = "downarrowGrey.png";
                Roommatesdata = false;
            }
            OnPropertyChanged(nameof(Roommatesicon));
            OnPropertyChanged(nameof(Roommatesdata));
        }
        public void ClickRentalsopenclose()
        {
            if (Rentalsicon == "downarrowGrey.png")
            {
                Rentalsicon = "minusGrey.png";
                Rentalsdata = true;
            }
            else
            {
                Rentalsicon = "downarrowGrey.png";
                Rentalsdata = false;
            }
            OnPropertyChanged(nameof(Rentalsicon));
            OnPropertyChanged(nameof(Rentalsdata));
        }
        public void ClickEventsopenclose()
        {
            if (Eventsicon == "downarrowGrey.png")
            {
                Eventsicon = "minusGrey.png";
                Eventsdata = true;
            }
            else
            {
                Eventsicon = "downarrowGrey.png";
                Eventsdata = false;
            }
            OnPropertyChanged(nameof(Eventsicon));
            OnPropertyChanged(nameof(Eventsdata));
        }
        public async void Clicktolsdetail(SERVICES_FAVORITE_DATA LDB)
        {
            dialog.ShowLoading("");
            NRIApp.LocalService.Features.Models.LS_LISTINGMODEL_DATA LMD = new NRIApp.LocalService.Features.Models.LS_LISTINGMODEL_DATA()
            {
                adid = LDB.adid,
                title = LDB.title,
                titleurl = LDB.titleurl,
                newcityurl = Commonsettings.Usercityurl
            };
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Detail.LS_Detail(LMD));

        }

        public async void Clicktolsrespform(SERVICES_FAVORITE_DATA LDB)
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Leadform.LS_RespForm(LDB.adid,LDB.premiumad));
        }

        public async void deletelsfavorite(SERVICES_FAVORITE_DATA SFA)
        {
            try
            {
                dialog.ShowLoading("");
                var Issaved = RestService.For<LocalService.Features.Interfaces.ILS_Detailpage>(Commonsettings.TechjobsAPI);
                var data = await Issaved.SaveandremoveBizfavorite(SFA.adid, Commonsettings.UserPid, Commonsettings.UserEmail, "1");
                if (data.ROW_DATA.Count > 0)
                {
                    Servicesfavorite= Servicesfavorite.Where(i => i.adid != SFA.adid).ToList();
                    Servicecount = Servicesfavorite.Count.ToString();
                    if (Servicecount == "0")
                    {
                        Servicesavail = false;
                        OnPropertyChanged(nameof(Servicesavail));
                    }
                    if (Roommatecount == "0" && Rentalcount == "0" && Servicecount == "0" && Eventcount == "0" && Techjobscount == "0")
                    {
                        var currentpage = GetCurrentPage();
                        var stackLayout = currentpage.FindByName<StackLayout>("Nolistblk");
                        var listblk = currentpage.FindByName<StackLayout>("favoritelist");
                        stackLayout.IsVisible = true;
                        listblk.IsVisible = false;

                    }
                    OnPropertyChanged(nameof(Servicesfavorite)); OnPropertyChanged(nameof(Servicecount));
                }
                dialog.HideLoading();
            }
            catch(Exception e)
            {

            }
        }

        public async void clicktoeventdetail(EVENT_FAVORITE_DATA EFA)
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Events.Features.Detail.Views.Detail(EFA.eventid,EFA.title,EFA.ticketingid));
        }

        public async void deleteeventfavorite(EVENT_FAVORITE_DATA EFA)
        {
            try
            {
                dialog.ShowLoading("");
                var Issaved = RestService.For<Events.Features.Detail.Interface.IEvent_Detail>(Commonsettings.EventsAPI);
                var data = await Issaved.SaveandremoveBizfavorite(EFA.eventid, Commonsettings.UserPid,"0",EFA.ticketingid);
                if (data.ROW_DATA.Count > 0)
                {
                    Eventsfavorite = Eventsfavorite.Where(i => i.eventid != EFA.eventid).ToList();
                    Eventcount = Eventsfavorite.Count.ToString();
                    if (Eventcount=="0")
                    {
                        Eventsavail = false;
                        OnPropertyChanged(nameof(Eventsavail));
                    }
                    if (Roommatecount == "0" && Rentalcount == "0" && Servicecount == "0" && Eventcount == "0" && Techjobscount == "0")
                    {
                        var currentpage = GetCurrentPage();
                        var stackLayout = currentpage.FindByName<StackLayout>("Nolistblk");
                        var listblk = currentpage.FindByName<StackLayout>("favoritelist");
                        stackLayout.IsVisible = true;
                        listblk.IsVisible = false;

                    }
                    OnPropertyChanged(nameof(Eventsfavorite)); OnPropertyChanged(nameof(Eventcount));
                }
                dialog.HideLoading();
            }
            catch (Exception e)
            {

            }
        }

        public async void clicktorentaldetail(RM_FAVORITE_DATA RFA)
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Rentals.Features.List.Views.DetailRentalList(RFA.adid, RFA.cityurl));
        }
        public async void Clicktorentalrespform(RM_FAVORITE_DATA RFA)
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Rentals.Features.List.Views.Contact_RT(RFA.adid));

        }
        public async void deleterentalFavorite(RM_FAVORITE_DATA RFA)
        {
            try
            {
                dialog.ShowLoading("");
                var deletead = RestService.For<NRIApp.Rentals.Features.List.Interface.IListRentalCategory>(Commonsettings.RoommatesAPI);
                NRIApp.Rentals.Features.List.Models.RTDeleteAD response = await deletead.DeleteAdData(RFA.adid, Commonsettings.UserPid);
                if (response.ROW_DATA.Count > 0)
                {
                    Rentalfavorite = Rentalfavorite.Where(i => i.adid != RFA.adid).ToList();
                    Rentalcount = Rentalfavorite.Count.ToString();
                    if (Rentalcount == "0")
                    {
                        Rentalsavail = false;
                        OnPropertyChanged(nameof(Rentalsavail));
                    }
                    if (Roommatecount == "0" && Rentalcount == "0" && Servicecount == "0" && Eventcount == "0" && Techjobscount == "0")
                    {
                        var currentpage = GetCurrentPage();
                        var stackLayout = currentpage.FindByName<StackLayout>("Nolistblk");
                        var listblk = currentpage.FindByName<StackLayout>("favoritelist");
                        stackLayout.IsVisible = true;
                        listblk.IsVisible = false;

                    }
                    OnPropertyChanged(nameof(Rentalfavorite)); OnPropertyChanged(nameof(Rentalcount));
                }
                dialog.HideLoading();
            }
            catch (Exception e)
            {

            }

        }


        public async void clicktorommatedetail(RM_FAVORITE_DATA RFA)
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Roommates.Features.List.Views.DetailRMList(RFA.adid,RFA.adtype));
        }

        public async void Clicktorommaterespform(RM_FAVORITE_DATA RFA)
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Roommates.Features.List.Views.Contact_RM(RFA.adid));
        }
        public async void deleteroommateFavorite(RM_FAVORITE_DATA RFA)
        {
            try
            {
                dialog.ShowLoading("");
                var deletead = RestService.For<NRIApp.Roommates.Features.List.Interface.ILisCategory>(Commonsettings.RoommatesAPI);
                NRIApp.Roommates.Features.List.Models.RMDeleteAD data = await deletead.DeleteAdData(RFA.adid, Commonsettings.UserPid);
                if (data.ROW_DATA.Count > 0)
                {
                    Roommatefavorite = Roommatefavorite.Where(i => i.adid != RFA.adid).ToList();
                    Roommatecount = Roommatefavorite.Count.ToString();
                    if (Roommatecount=="0")
                    {
                        Roommatesavail = false;
                        OnPropertyChanged(nameof(Roommatesavail));
                    }
                    if (Roommatecount=="0"&&Rentalcount=="0"&&Servicecount=="0"&&Eventcount=="0"&& Techjobscount=="0")
                    {
                        var currentpage = GetCurrentPage();
                        var stackLayout = currentpage.FindByName<StackLayout>("Nolistblk");
                        var listblk = currentpage.FindByName<StackLayout>("favoritelist");
                        stackLayout.IsVisible = true;
                        listblk.IsVisible = false;

                    }
                    OnPropertyChanged(nameof(Roommatefavorite)); OnPropertyChanged(nameof(Roommatecount));
                }
                dialog.HideLoading();
            }
            catch (Exception e)
            {

            }
        }

        public async void clicktotechjobsdetail(TECHJOBS_FAVORITE_DATA TFA)
        {
            dialog.ShowLoading("");
            var currentpage = GetCurrentPage();
            if(TFA.businessid!="0")
            await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.Detail.Moduledetail(TFA.modulename,TFA.moduleid,TFA.businessid,TFA.businessname));
            else
                await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.Listing.Views.Modulelisting(TFA.moduleid, TFA.modulename,"","","",""));
        }
        public async void Clicktotechjobsrespform(TECHJOBS_FAVORITE_DATA TFA)
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.LeadForm.Views.Respform(TFA.modulename,TFA.moduleid,TFA.businessid));
        }

        public async void deletetechjobsFavorite(TECHJOBS_FAVORITE_DATA TFA)
        {
            try
            {
                dialog.ShowLoading("");
                string pageurl = "/"+TFA.moduleurl+ "_training_in_" +TFA.businesstitleurl+"_"+TFA.moduleid+"_"+TFA.businessid;

                var deletead = RestService.For<NRIApp.Techjobs.Features.Detail.Interfaces.IDetailservice>(Commonsettings.TechjobsAPI);
                NRIApp.Techjobs.Features.Detail.Models.Result data = await deletead.Deletefavorite(Commonsettings.UserPid,pageurl);
                if (data.ROW_DATA.Count >0)
                {
                    if (data.ROW_DATA[0].resulinformation == "deleted success")
                    {


                        Techjobsfavorite = Techjobsfavorite.Where(i => i.pageurl != pageurl).ToList();
                        Techjobscount = Techjobsfavorite.Count.ToString();
                        if (Techjobscount == "0")
                        {
                            Techjobsavail = false;
                            OnPropertyChanged(nameof(Techjobsavail));
                        }
                        if (Roommatecount == "0" && Rentalcount == "0" && Servicecount == "0" && Eventcount == "0" && Techjobscount == "0")
                        {
                            var currentpage = GetCurrentPage();
                            var stackLayout = currentpage.FindByName<StackLayout>("Nolistblk");
                            var listblk = currentpage.FindByName<StackLayout>("favoritelist");
                            stackLayout.IsVisible = true;
                            listblk.IsVisible = false;

                        }
                        OnPropertyChanged(nameof(Techjobsfavorite)); OnPropertyChanged(nameof(Techjobscount));
                    }
                }
                dialog.HideLoading();
            }
            catch (Exception e)
            {

            }
        }

        public static Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();

            return currentPage;
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
