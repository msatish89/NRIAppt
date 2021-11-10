using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NRIApp.LocalService.Features.Interfaces;
using NRIApp.LocalService.Features.Models;
using Xamarin.Forms.Extended;
using Refit;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using System.Windows.Input ;
using NRIApp.Helpers;
using Acr.UserDialogs;
using System.Collections.ObjectModel;
using Plugin.Connectivity;

namespace NRIApp.LocalService.Features.ViewModels
{
    public class LS_ViewModel : INotifyPropertyChanged
    {
        IUserDialogs _Dialog = UserDialogs.Instance;
        //added 
      //  public static string Flexchoosemonths = "";
        public event PropertyChangedEventHandler PropertyChanged;
        public static int Selectedservicetype;
        public static string primarytagid; public static string primarytag; public static string primarytagurl;
        public static string supertagid; public static string supertag; public static string supertagurl;
        public static string searchtagid; public static string searchtag;
        public static string stagid = "",ordertype=""; public static string filterprimarytagid; public static string filterprimarytag;
        public static int selectedtagtype = 0;public static string sortselected = "date";public static int distancefrom = 0;
        public static int distanceto = 10; public static int openorclose = 0; public static int isfiltered = 0;
        public static int ispartialclass = 0;
        public static string page = ""; public static int valueadded = 0,isOTPpage=0; public static string Adid = "",Valueaddedcontentid="",Bizcity="",Flexchoosemonths="";

        public static int isrenewclickble = 0; public static int islseditable = 0;
        public static IDictionary<string, string> addionalcities = new Dictionary<string, string>();
        public static IDictionary<string, string> packagevalueamountdetails = new Dictionary<string, string>();
        public static IDictionary<string, string> packageamount = new Dictionary<string, string>();
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
        public static Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();

            return currentPage;
        }
        public static Page GetCurrentModalPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.ModalStack.LastOrDefault();

            return currentPage;
        }
        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        private Command _Backbtncommand;

        public Command Backbtncommand
        {
            get { return _Backbtncommand; }
            set { _Backbtncommand = value; }
        }

        
        public InfiniteScrollCollection<LS_PRIMARYTAG_LIST> Primarytags { get; set; }
       // public ObservableCollection<LS_PRIMARYTAG_LIST> Primarytags1 { get; set; }
        
        public Command Datacommand { get; set; }
        public ICommand SubmitCommand { protected set; get; }
        public ICommand Clickservicesearch { get; set; }
        public Command Searchtapcommand { get; set; }
        public ICommand Retrylisting { get; set; }
        public string leadlcfptagid;
        public string leadlcfleaftagid;
        private string _leadlcfleaftags { get; set; }
        public string leadlcfleaftags
        {
            get { return _leadlcfleaftags; }
            set
            {
                _leadlcfleaftags = value;

                OnPropertyChanged(leadlcfleaftags);
            }
        }
        private Color emailbackground;
        public Color EmailErrorColor
        {
            get { return emailbackground; }

            set { emailbackground = value; }
        }
        private string emailerror;
        public string Emailerror
        {
            get { return emailerror; }
            set
            {
                emailerror = value;

                OnPropertyChanged(nameof(Emailerror));
            }
        }
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged(Email); OnPropertyChanged(nameof(EmailErrorColor));

            }
        }
        private string _Countrycode = "+1";
        public string Countrycode
        {
            get { return _Countrycode; }
            set
            {

                _Countrycode = value;
                OnPropertyChanged(Countrycode);

            }
        }
        private DateTime _Date = DateTime.Now.Date;

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        private string contactno;
        public string Contactno
        {
            get { return contactno; }
            set
            {

                contactno = value;
                OnPropertyChanged(Contactno);

            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {

                name = value;
                OnPropertyChanged(Name);
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {

                description = value;
                OnPropertyChanged(Description);
            }
        }
        public string _tempservicesearch;

        private string _servicesearch { get; set; }
        public string servicesearch
        {
            get { return _servicesearch; }
            set
            {
                _servicesearch = value;
                if (string.IsNullOrEmpty(servicesearch))
                {
                    _getserachdata = null;
                    OnPropertyChanged(nameof(getserachdata));
                }
                if (_tempservicesearch != servicesearch && servicesearch != "")
                {
                    getServicetags();
                }
            }
        }

        private string _Searchtxt="";
        public string Searchtxt
        {
            get { return _Searchtxt; }
            set
            {

                _Searchtxt = value;
                OnPropertyChanged(Searchtxt);
            }
        }
        private bool _Searchtxtavail = false;
        public bool Searchtxtavail
        {
            get { return _Searchtxtavail; }
            set { _Searchtxtavail = value; OnPropertyChanged(nameof(_Searchtxtavail)); }
        }

        private List<LS_SEARCH_DATA> _getserachdata { get; set; }

        public List<LS_SEARCH_DATA> getserachdata
        {
            get { return _getserachdata; }
            set
            {
                _getserachdata = value;
               

            }
        }
        string Oldclpostid = "";
        public LS_ViewModel(string oldclpostid,string type)
        {
            Oldclpostid = oldclpostid;
            Datacommand = new Command<LS_PRIMARYTAG_LIST>(async (ds) => await OnTagstapped(ds));
            Clickservicesearch = new Command(ClicksearchButton);
            Primarytags = new InfiniteScrollCollection<LS_PRIMARYTAG_LIST>
            {
                OnLoadMore = async () =>
                {

                    var items = new List<LS_PRIMARYTAG_LIST>();
                    if (Convert.ToInt32(Primarytags.First().totalrecs) != Primarytags.Count)
                    {


                        try
                        {
                            var page = Primarytags.Last().pageno + 1;
                            IsBusy = true;
                            var primarytags = RestService.For<IServiceAPI>(Commonsettings.Localservices);
                            var list = await primarytags.GetPrimaryData(page);
                            items = list.ROW_DATA;
                            IsBusy = false;

                        }
                        catch (Exception ex) { }
                    }
                    return items;
                }
            };
            getprimarydata();
            Retrylisting = new Command(Retrylistingmethod);
            

        }
        public void Retrylistingmethod()
        {
            getprimarydata();
        }
        public LS_ViewModel(string type)
        {
            Oldclpostid = LocalService.Features.Views.LS_Primarytags._Oldclpostid;
            Searchtapcommand = new Command<LS_SEARCH_DATA>(Searchitemtapped);
            Backbtncommand = new Command(async () => await BackbtncommandClick());
            Binddefaultdata();
        }

        public LS_ViewModel(string ptagid, List<LEAF_TAGS_DATA> leaf)
        {
            string ltags = "", ltagid = "";
            leadlcfptagid = ptagid;
            foreach (var item in leaf)
            {
                ltags += item.tag + ",";
                ltagid += item.tagid + ",";
            }
            if (ltags != "")
            {
                ltags = ltags.Remove(ltags.Length - 1, 1);
            }

            leadlcfleaftags = ltags;
            leadlcfleaftagid = ltagid;
            Backbtncommand = new Command(async () => await BackbtncommandClick());
        }
        public async void getprimarydata()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    _Dialog.ShowLoading("", null);
                    var primarydata = RestService.For<IServiceAPI>(Commonsettings.Localservices);
                    var data = await primarydata.GetPrimaryData(1);
                    Primarytags.Clear();
                    var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
                    var layout = currentpage.FindByName<StackLayout>("nointernet");
                    var layoutlist = currentpage.FindByName<ListView>("listdata");
                    layoutlist.IsVisible = true;
                    layout.IsVisible = false;
                    Primarytags?.AddRange(data.ROW_DATA);
                    _Dialog.HideLoading();
                }
                else
                {
                    _Dialog.HideLoading();
                    await Task.Delay(2000);
                    var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
                    var layout = currentpage.FindByName<StackLayout>("nointernet");
                    var layoutlist = currentpage.FindByName<ListView>("listdata");
                    layoutlist.IsVisible = false;
                    layout.IsVisible = true;
                    

                }

            }
            catch (Exception e)
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await Task.Delay(2000);
                    var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
                    var layout = currentpage.FindByName<StackLayout>("nointernet");
                    layout.IsVisible = true;
                    var layoutlist = currentpage.FindByName<ListView>("listdata");
                    layoutlist.IsVisible = false;
                }
            }
        }

        async Task OnTagstapped(LS_PRIMARYTAG_LIST lt)
        {

            try
            {
                _Dialog.ShowLoading("", null);
                var curpage = GetCurrentPage();
                ViewModels.LS_Leaftypes.Checkedvalues = ViewModels.LS_Leaftypes.Checkedtext = "";
                int servicetype = Selectedservicetype;
                ViewModels.LS_ViewModel.primarytagid = ViewModels.LS_ViewModel.searchtagid = ViewModels.LS_ViewModel.stagid = Convert.ToString(lt.tagid);
                ViewModels.LS_ViewModel.primarytag = ViewModels.LS_ViewModel.searchtag = lt.primarytag;
                ViewModels.LS_ViewModel.filterprimarytagid = ViewModels.LS_Leaftypes.Checkedvalues;
                ViewModels.LS_ViewModel.filterprimarytag = ViewModels.LS_Leaftypes.Checkedtext;
                ViewModels.LS_ViewModel.primarytagurl = lt.primarytagurl;
                if (lt.tagcnt != "0")
                {
                    if (servicetype == 2)
                    {
                        await curpage.Navigation.PushAsync(new Views.LS_Leaftypes(lt.primarytag, lt.tagid, servicetype, 2, lt.supertagid, lt.supertag,Oldclpostid));
                    }
                    else
                    {
                        await curpage.Navigation.PushAsync(new Views.Leadform.LS_Prime(lt.supertagid, lt.supertag, lt.primarytag, lt.tagid.ToString(), "", ""));
                    }
                        

                }
                else
                {

                    string val = ViewModels.LS_Leaftypes.Checkedvalues, text = ViewModels.LS_Leaftypes.Checkedtext;
                    if (servicetype == 2)
                    {
                        await curpage.Navigation.PushAsync(new Views.Posting.LS_Posting(Convert.ToString(lt.supertagid), lt.supertag, lt.primarytag, lt.tagid.ToString(), val, text,Oldclpostid));

                    }
                    else
                    {
                        // await curpage.Navigation.PushAsync(new Views.Leadform.LS_LeadForm(lt.supertagid, lt.supertag, lt.primarytag, lt.tagid.ToString(), val, text));
                        await curpage.Navigation.PushAsync(new Views.Leadform.LS_Prime(lt.supertagid, lt.supertag, lt.primarytag, lt.tagid.ToString(), val, text));


                    }
                    _Dialog.HideLoading();
                }
                ViewModels.LS_ViewModel.primarytagid = Convert.ToString(lt.tagid);
                primarytag = lt.primarytag; primarytagurl = lt.primarytagurl;
                supertagid = Convert.ToString(lt.supertagid); supertag = lt.supertag;
                supertagurl = lt.supertagurl;
                await Task.Delay(2000);
                _Dialog.HideLoading();
            }
            catch (Exception e)
            {

            }
        }
       
       
        public async Task BackbtncommandClick()
        {
            try { 
            await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ee)
            {

            }
        }
        public async void ClicksearchButton()
        {
            var curpage = GetCurrentPage();
            await curpage.Navigation.PushAsync(new Views.LS_Search());
        }
        public async void getServicetags()
        {
            try
            {
                Searchtxtavail = false;
                Searchtxt = "";
                var search = RestService.For<IServiceAPI>(Commonsettings.Localservices);
                var list = await search.Getsearchajax(servicesearch);
                OnPropertyChanged(nameof(getserachdata));
                _getserachdata = list.ROW_DATA;
            }
            catch (Exception e) { }
        }
        public async void Searchitemtapped(LS_SEARCH_DATA ld)
        {
            try
            {

                _Dialog.ShowLoading("");
              
                var curpage = GetCurrentPage();
                ViewModels.LS_Leaftypes.Checkedvalues = ViewModels.LS_Leaftypes.Checkedtext = "";
                ViewModels.LS_ViewModel.primarytagid = ViewModels.LS_ViewModel.searchtagid =  Convert.ToString(ld.ptagid);
                ViewModels.LS_ViewModel.primarytag = ViewModels.LS_ViewModel.searchtag = ld.ptag;
                ViewModels.LS_ViewModel.primarytagid = Convert.ToString(ld.ptagid);
                LS_ViewModel.primarytagurl = ld.ptagurl;
                primarytag = ld.ptag; primarytagurl = ld.ptagurl;
                supertagid = Convert.ToString(ld.stagid); supertag = ld.stag;
                supertagurl = ld.stagurl;
                if (ld.ltype==2)
                {
                    ViewModels.LS_ViewModel.stagid = Convert.ToString(ld.ptagid);
                }
                else
                {
                    ViewModels.LS_ViewModel.stagid = Convert.ToString(ld.stagid);
                }
               
                ViewModels.LS_ViewModel.filterprimarytagid = ViewModels.LS_Leaftypes.Checkedvalues;
                ViewModels.LS_ViewModel.filterprimarytag = ViewModels.LS_Leaftypes.Checkedtext;
                if (ld.ltype ==2 && ld.tagcnt==0)
                {
                    if (Selectedservicetype == 2)
                    {
                        await curpage.Navigation.PushAsync(new Views.LS_Leaftypes(ld.ptag, Convert.ToInt32(ld.ptagid), Selectedservicetype, ld.ltype, ld.stagid, ld.stag, Oldclpostid));
                    }
                    else
                    {
                        await curpage.Navigation.PushAsync(new Views.Leadform.LS_Prime(ld.stagid, ld.stag, ld.ptag, ld.ptagid, "", ""));
                    }
                }
                else if (ld.ltype == 3)
                {
                    if (Selectedservicetype == 2)
                    {
                        await curpage.Navigation.PushAsync(new Views.LS_Leaftypes(ld.ptag, Convert.ToInt32(ld.ptagid), Selectedservicetype, ld.ltype, ld.stagid, ld.stag, Oldclpostid, ld.ptagid));
                    }
                    else
                    {
                        await curpage.Navigation.PushAsync(new Views.Leadform.LS_Prime(ld.stagid, ld.stag, ld.ptag, ld.ptagid, "", ""));
                    }
                }
                else
                {
                    if (Selectedservicetype == 2)
                    {
                        await curpage.Navigation.PushAsync(new Views.LS_Leaftypes(ld.ptag, Convert.ToInt32(ld.ptagid), Selectedservicetype, ld.ltype, ld.stagid, ld.stag, Oldclpostid));
                    }
                    else
                    {
                        await curpage.Navigation.PushAsync(new Views.Leadform.LS_Prime(ld.stagid, ld.stag, ld.ptag, ld.ptagid, "", ""));
                    }

                    
                }
               
                _Dialog.HideLoading();
              
            }catch(Exception e) {

            }

        }

        public async void Binddefaultdata()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                _Dialog.ShowLoading("", null);
                var primarydata = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);
                var data = await primarydata.GetSearchcategories(Commonsettings.Usercityurl);
                OnPropertyChanged(nameof(getserachdata));
                getserachdata = data.ROW_DATA;
              
                Searchtxtavail = true;
                Searchtxt = "Top search categories";
                OnPropertyChanged(nameof(Searchtxtavail));
                _Dialog.HideLoading();
                }
                else
                {
                    _Dialog.HideLoading();
                    _Dialog.Toast("Check your internet connection");
                }

            }
            catch (Exception e)
            {
                
            }
        }
    }


}
