using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using NRIApp.Helpers;
using NRIApp.Techjobs.Features.Listing.Interfaces;
using NRIApp.Techjobs.Features.Listing.Models;
using Acr.UserDialogs;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Connectivity;

namespace NRIApp.Techjobs.Features.Listing.ViewModels
{
   public class ModulelistVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;
        public static List<Modulelisting> modlistings { get; set; }
        public ICommand Contactcommand { get; set; }
        public ICommand Moddetailcommand { get; set; }
        public ICommand Searchbarcommand { get; set; }
        public ICommand Filtercommand { get; set; }
        public ICommand Sortcommand { get; set; }
        public ICommand Refreshcommand { get; set; }
        string moduleid = "";
        static string trmode = "", facilty = "",sorting="",pagetype="";
        public ModulelistVM(string id,string modes, string facility,string page,string sort)
        {
          //  checkinternet();
            Checkinternetcontinuosly();
            getmoduleslist(id, modes, facility,page,sort);
            moduleid = id;
            trmode = modes;
            facilty = facility;
            sorting = sort;
            pagetype = page;
            Contactcommand = new Command<Modulelisting>(GetLeadform);
            Moddetailcommand = new Command<Modulelisting>(Moddetailclick);
            Searchbarcommand = new Command(searchbar);
           // Filtercommand = new Command(async () => await Openfilter(modes,facility));
            Filtercommand = new Command(Openfilter);
            Sortcommand = new Command(OpenSort);
            Refreshcommand = new Command(Refresh);
        }

        private bool _conn;
        public bool conn
        {
            get { return _conn; }
            set
            {
                _conn = value;
                OnPropertyChanged();
            }
        }

        public void checkinternet()
        {
            conn = CrossConnectivity.Current.IsConnected;
        }
        public void Checkinternetcontinuosly()
        {
            CrossConnectivity.Current.ConnectivityChanged += (sender, args) =>
            {
                //conn = args.IsConnected;
                if (CrossConnectivity.Current.IsConnected)
                    getmoduleslist(moduleid, trmode, facilty, pagetype, sorting);
                else
                   dialog.Toast("Kindly check your internet connection");
            };
        }
        public void Refresh ()
        {
            getmoduleslist(moduleid, trmode, facilty, pagetype, sorting);
        }
        public async void OpenSort()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushModalAsync(new NRIApp.Techjobs.Features.Listing.Views.Sort(moduleid,trmode, facilty, sorting));
        }
        public async void Openfilter()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushModalAsync(new NRIApp.Techjobs.Features.Listing.Views.Filters(moduleid,"", trmode, facilty));
        }
        public async void searchbar()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.Home.Views.Search());
        }
        public bool _nolist = false;
        public bool Nolist
        {
            get { return _nolist; }
            set
            {
                _nolist = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nolist)));
            }
        }
        public bool _listblk = true;
        public bool Listblk
        {
            get { return _listblk; }
            set
            {
                _listblk = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Listblk)));
            }
        }
        public bool _filterblk = true;
        public bool Filterblk
        {
            get { return _filterblk; }
            set
            {
                _filterblk = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filterblk)));
            }
        }
        public bool _nofilterblk = false;
        public bool NoFilterblk
        {
            get { return _nofilterblk; }
            set
            {
                _nofilterblk = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NoFilterblk)));
            }
        }
        async void getmoduleslist(string moduleid,string mode, string facilities, string page,string sort)
        {
            var connected = CrossConnectivity.Current.IsConnected;
           // await Task.Delay(2000);
           
            try
            {
                if (connected==true)
                {
                    dialog.ShowLoading("", null);
                    var nsAPI = RestService.For<IModulelisting>(Commonsettings.TechjobsAPI);
                    Moduledata list = await nsAPI.Getmodulelistings(moduleid, mode, facilities, Commonsettings.UserLat.ToString(), Commonsettings.UserLong.ToString(), sort);
                    OnPropertyChanged(nameof(modlistings));
                    modlistings = list.ROW_DATA;
                    var currentpage = GetCurrentPage();
                    ListView listvew = currentpage.FindByName<ListView>("listdata");
                    StackLayout stack = currentpage.FindByName<StackLayout>("nolistblk");
                    StackLayout stacknointernet = currentpage.FindByName<StackLayout>("nointernet");
                    if (list.ROW_DATA.Count > 0)
                    {
                        if (list.ROW_DATA.Count < 2 && page != "filter")
                        {
                            Filterblk = false;
                            NoFilterblk = true;
                        }

                        foreach (var i in modlistings)
                        {
                            if (i.rating == "0")
                                i.israting = false;
                            i.ratingimg = "Star" + i.rating + ".png";
                            i.address = i.city + "," + i.statecode;
                            i.nobizimg = i.businessname.Substring(0, 1).ToUpper();
                            i.Isfaclity1 = false;
                            i.Isfaclity2 = false;
                            i.Isfaclity3 = false;
                            i.Isfaclity4 = false;
                            i.Isstackfac1 = false;
                            i.Isstackfac2 = false;
                            if (i.businessimageurl == "")
                            {
                                i.NoimgVisible = true;
                                i.ImgVisible = false;
                            }
                            else
                            {
                                i.NoimgVisible = false;
                                i.ImgVisible = true;
                            }
                            if (i.trainingmode == "classroom,online")
                                i.trainmode = "Online, Inclass";
                            else if (i.trainingmode == "classroom")
                                i.trainmode = "Inclass";
                            else
                                i.trainmode = "Online";
                            if (i.facilityname != "")
                            {
                                string[] names = i.facilityname.Split(',');
                                if (names.Length > 3)
                                {
                                    i.faclity1 = names[0].ToString();
                                    i.faclity2 = names[1].ToString();
                                    i.faclity3 = names[2].ToString();
                                    i.faclity4 = names[3].ToString();
                                    i.Isfaclity1 = true;
                                    i.Isfaclity2 = true;
                                    i.Isfaclity3 = true;
                                    i.Isfaclity4 = true;
                                    i.Isstackfac1 = true;
                                    i.Isstackfac2 = true;
                                }
                                else if (names.Length == 3)
                                {
                                    i.faclity1 = names[0].ToString();
                                    i.faclity2 = names[1].ToString();
                                    i.faclity3 = names[2].ToString();
                                    i.Isfaclity1 = true;
                                    i.Isfaclity2 = true;
                                    i.Isfaclity3 = true;
                                    i.Isstackfac1 = true;
                                    i.Isstackfac2 = true;
                                }
                                else if (names.Length == 2)
                                {
                                    i.faclity1 = names[0].ToString();
                                    i.faclity2 = names[1].ToString();
                                    i.Isfaclity1 = true;
                                    i.Isfaclity2 = true;
                                    i.Isstackfac1 = true;
                                    i.Isstackfac2 = false;
                                }
                                else if (names.Length == 1)
                                {
                                    i.faclity1 = names[0].ToString();
                                    i.Isfaclity1 = true;
                                    i.Isstackfac1 = true;
                                    i.Isstackfac2 = false;
                                }
                            }

                        }

                        //Nolist = false;
                        //Listblk = true;
                        //OnPropertyChanged(nameof(Nolist));
                        //OnPropertyChanged(nameof(Listblk));
                        listvew.IsVisible = true;
                        stack.IsVisible = false;
                        stacknointernet.IsVisible = false;
                    }
                    else
                    {
                        listvew.IsVisible = false;
                        stack.IsVisible = true;

                        stacknointernet.IsVisible = false;
                        //Nolist = true;
                        //Listblk = false;
                        //OnPropertyChanged(nameof(Nolist));
                        //OnPropertyChanged(nameof(Listblk));

                    }

                    listvew.ItemsSource = modlistings;
                    dialog.HideLoading();
                }
                else
                {
                    var currentpage = GetCurrentPage();
                    ListView listvew = currentpage.FindByName<ListView>("listdata");
                    StackLayout stack = currentpage.FindByName<StackLayout>("nolistblk");
                    StackLayout stacknointernet = currentpage.FindByName<StackLayout>("nointernet");
                    listvew.IsVisible = false;
                    stack.IsVisible = false;
                    stacknointernet.IsVisible = true;
                    // dialog.Toast("Kindly check your internet connection");
                }


            }
            catch (Exception e)
            {
              
                string Error = e.Message + e.StackTrace;
                dialog.HideLoading();
                var conne = CrossConnectivity.Current.IsConnected;
                if (conne==false)
                {
                    var currentpage = GetCurrentPage();
                    ListView listvew = currentpage.FindByName<ListView>("listdata");
                    StackLayout stack = currentpage.FindByName<StackLayout>("nolistblk");
                    StackLayout stacknointernet = currentpage.FindByName<StackLayout>("nointernet");
                    listvew.IsVisible = false;
                    stack.IsVisible = false;
                    stacknointernet.IsVisible = true;
                }
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
        public async void GetLeadform(Modulelisting list)
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.LeadForm.Views.Respform(list.module, list.moduleid.ToString(), list.businessid.ToString()));
        }

        public async void Moddetailclick(Modulelisting list)
        {
            
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.Detail.Moduledetail(list.module, list.moduleid.ToString(), list.businessid.ToString(),list.businessname));
        }
    }
}
