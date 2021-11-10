using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Acr.UserDialogs;
using Plugin.Connectivity;
using Refit;
using NRIApp.LocalService.Features.Interfaces;
using NRIApp.LocalService.Features.Models;
using NRIApp.Helpers;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Plugin.Share;
using Plugin.Messaging;
namespace NRIApp.LocalService.Features.ViewModels
{
    public class LS_OnlineDetail_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs _Dialog = UserDialogs.Instance;
        private string _Classtitle { get; set; }
        public string Classtitle
        {
            get { return _Classtitle; }
            set
            {
                _Classtitle = value;
                OnPropertyChanged(nameof(Classtitle));

            }
        }
        private string _Contactname { get; set; }
        public string Contactname
        {
            get { return _Contactname; }
            set
            {
                _Contactname = value;
                OnPropertyChanged(nameof(Contactname));

            }
        }
        private string _Classtime { get; set; }
        public string Classtime
        {
            get { return _Classtime; }
            set
            {
                _Classtime = value;
                OnPropertyChanged(nameof(Classtime));

            }
        }

        private string _Sizeallowed { get; set; }
        public string Sizeallowed
        {
            get { return _Sizeallowed; }
            set
            {
                _Sizeallowed = value;
                OnPropertyChanged(nameof(Sizeallowed));

            }
        }
        private string _totalamnt { get; set; }
        public string totalamnt
        {
            get { return _totalamnt; }
            set
            {
                _totalamnt = value;
                OnPropertyChanged(nameof(totalamnt));

            }
        }
        private string _langauges { get; set; }
        public string langauges
        {
            get { return _langauges; }
            set
            {
                _langauges = value;
                OnPropertyChanged(nameof(langauges));

            }
        }
        private string _Classmodes { get; set; }
        public string Classmodes
        {
            get { return _Classmodes; }
            set
            {
                _Classmodes = value;
                OnPropertyChanged(nameof(Classmodes));
            }
        }
        private string _Classlevels { get; set; }
        public string Classlevels
        {
            get { return _Classlevels; }
            set
            {
                _Classlevels = value;
                OnPropertyChanged(nameof(Classlevels));
            }
        }
        private string _Classlength { get; set; }
        public string Classlength
        {
            get { return _Classlength; }
            set
            {
                _Classlength = value;
                OnPropertyChanged(nameof(Classlength));
            }
        }
        private string _Age { get; set; }
        public string Age
        {
            get { return _Age; }
            set
            {
                _Age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        private string _Expe { get; set; }
        public string Expe
        {
            get { return _Expe; }
            set
            {
                _Expe = value;
                OnPropertyChanged(nameof(Expe));
            }
        }

        private string _Tag { get; set; }
        public string Tag
        {
            get { return _Tag; }
            set
            {
                _Tag = value;
                OnPropertyChanged(nameof(Tag));
            }
        }

        private string _Instructorlang { get; set; }
        public string Instructorlang
        {
            get { return _Instructorlang; }
            set
            {
                _Instructorlang = value;
                OnPropertyChanged(nameof(Instructorlang));
            }
        }
        private string _Taught{ get; set; }
        public string Taught
        {
            get { return _Taught; }
            set
            {
                _Taught = value;
                OnPropertyChanged(nameof(Taught));
            }
        }
        private bool _Isreq = false;
        public bool Isreq
        {
            get { return _Isreq; }
            set
            {
                _Isreq = value;
                OnPropertyChanged(nameof(Isreq));
            }
        }
        private bool _Islearning = false;
        public bool Islearning
        {
            get { return _Islearning; }
            set
            {
                _Islearning = value;
                OnPropertyChanged(nameof(Islearning));
            }
        }
        private bool _Isfaq = false;
        public bool Isfaq
        {
            get { return _Isfaq; }
            set
            {
                _Isfaq = value;
                OnPropertyChanged(nameof(Isfaq));
            }
        }
        private bool _isexp = false;
        public bool isexp
        {
            get { return _isexp; }
            set { _isexp = value; OnPropertyChanged(nameof(isexp)); }
        }
        private bool _contentviewvisible = false;
        public bool contentviewvisible
        {
            get { return _contentviewvisible; }
            set { _contentviewvisible = value; OnPropertyChanged(nameof(contentviewvisible)); }
        }
        public string _Isfavoriteimg;
        public string Isfavoriteimg
        {
            get { return _Isfavoriteimg; }
            set { _Isfavoriteimg = value; }
        }
        public string _profileimg;
        public string Profileimg
        {
            get { return _profileimg; }
            set { _profileimg = value; }
        }
        public Command ContentViewTap { get; set; }
        public Command PopupContentTap { get; set; }
        public Command Favoritecommand { get; set; }
        public Command ShareLinkcommand { get; set; }
        public Command Bookbatchcommand { get; set; }
        public Command Booklistbatchcommand { get; set; }
        public static List<Classtimings> classtimings { get; set; }
        public static List<requirements> requiremnts { get; set; }
        public static List<learns> learnslist { get; set; }
        public static List<faqs> faqslist { get; set; }
        public void Closecontentview()
        {
            contentviewvisible = false;
        }
        public void showcontentview(Classtimings timings)
        {
            OnPropertyChanged(nameof(contentviewvisible));
            string[] times = Regex.Split(timings.timezones, "</li>");
            var currentpage = GetCurrentPage();
            ListviewScrollbar myEntry = currentpage.FindByName<ListviewScrollbar>("listtimezones");
            List<timingzones> srch = new List<timingzones>();
            foreach (var i in times)
            {
                srch.Add(new timingzones() { time = i.Replace("<li>", "") });
            }
            myEntry.ItemsSource = srch;
            contentviewvisible = true;
        }
       static string classid = "", Isfavorite = "",linkurl="",title="",guserid="";
        public LS_OnlineDetail_VM(string contentid)
        {
            classid = contentid;
            Getsingledata(contentid);
            Getinstructordetails(contentid);
            Getclasstimings(contentid);
            Getrequirements(contentid);
            Getlearnings(contentid);
            Getfaq(contentid);
            ContentViewTap = new Command(Closecontentview);
            PopupContentTap = new Command<Classtimings>(showcontentview);
            Favoritecommand = new Command(addbookmark);
            ShareLinkcommand = new Command(Sharelink);
            Bookbatchcommand = new Command(Bookbatch);
            Booklistbatchcommand = new Command<Classtimings>(Booktiminglistbatch);
        }
        public async void Bookbatch()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Detail.OnlineClassPayment(guserid, classid));
        }
        public async void Booktiminglistbatch(Classtimings data)
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Detail.OnlineClassPayment(guserid, classid));
        }
        public void Sharelink()
        {
            CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
            {
                Text = "",
                Title = title,
                Url = linkurl.Trim()
            });
        }
        public async void Getlearnings(string contentid)
        {
            try
            {
                await Task.Delay(2000);
                var currentpage = GetCurrentPage();
                HVScrollGridView myEntry = currentpage.FindByName<HVScrollGridView>("listlearn");
                var nsAPI = RestService.For<ILS_Listing>(Commonsettings.Localservices);
                learnsdata list = await nsAPI.Getlearnings(contentid);
                if(list.ROW_DATA.Count>0)
                {
                    
                    learnslist = list.ROW_DATA;
                    OnPropertyChanged(nameof(Islearning));
                    Islearning = true;
                    myEntry.ItemsSource = learnslist;
                }
               
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
        }
        public async void Getrequirements(string contentid)
        {
            try
            {
                await Task.Delay(2000);
                var currentpage = GetCurrentPage();
                HVScrollGridView myEntry = currentpage.FindByName<HVScrollGridView>("listreq");
                var nsAPI = RestService.For<ILS_Listing>(Commonsettings.Localservices);
                requirementsdata list = await nsAPI.Getrequirements(contentid);
                if(list.ROW_DATA.Count>0)
                {
                  
                    requiremnts = list.ROW_DATA;
                    OnPropertyChanged(nameof(Isreq));
                    Isreq = true;
                    myEntry.ItemsSource = requiremnts;
                }
                
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
        }
        public async void Getfaq(string contentid)
        {
            try
            {
                await Task.Delay(2000);
                var currentpage = GetCurrentPage();
                var nsAPI = RestService.For<ILS_Listing>(Commonsettings.Localservices);
                HVScrollGridView myEntry = currentpage.FindByName<HVScrollGridView>("listfaq");
                faqsdata list = await nsAPI.Getfaq(contentid);
                if(list.ROW_DATA.Count>0)
                {
                    
                    faqslist = list.ROW_DATA;
                    OnPropertyChanged(nameof(Isfaq));
                    Isfaq = true;
                    myEntry.ItemsSource = faqslist;
                }
               
                _Dialog.HideLoading();
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
        }
       
        public async void addbookmark()
        {
          

            try
            {
                var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();
                if (Commonsettings.UserPid != "" && Commonsettings.UserPid != "0" && !string.IsNullOrEmpty(Commonsettings.UserPid))
                {
                    _Dialog.ShowLoading("");
                    var nsAPI = RestService.For<ILS_Listing>(Commonsettings.Localservices);
                    var data = await nsAPI.Addbookmark(classid, Commonsettings.UserPid, Commonsettings.UserEmail, Isfavorite);
                    if (data.ROW_DATA.Count > 0)
                    {

                        if (Isfavorite == "0")
                        {
                            Isfavoriteimg = "HeartGrayActive.png";
                            Isfavorite = "1";
                        }
                        else
                        {
                            Isfavoriteimg = "HeartGray.png";
                            Isfavorite = "0";
                        }

                    }
                   
                }
                else
                {
                    try { 
                    await CurrentPage.Navigation.PushModalAsync(new Signin.Views.Login());
                    }
                    catch (Exception ee)
                    {

                    }
                }
                OnPropertyChanged(nameof(Isfavoriteimg));
                _Dialog.HideLoading();
            }
            catch (Exception e)
            {
                Isfavoriteimg = "HeartGray.png";
                Isfavorite = "0";
                OnPropertyChanged(nameof(Isfavoriteimg));
                _Dialog.HideLoading();

               
            }
        }
        public async void Getclasstimings (string contentid)
        {
            try
            {
                await Task.Delay(2000);
                var currentpage = GetCurrentPage();
                HVScrollGridView myEntry = currentpage.FindByName<HVScrollGridView>("listtimings");
                var nsAPI = RestService.For<ILS_Listing>(Commonsettings.Localservices);
                Classtimingsdata list = await nsAPI.Getclasstimings(contentid);
                classtimings = list.ROW_DATA;
                foreach(var i in classtimings)
                {
                    i.classdate = i.availablefrom + " to " + i.availableto;
                    i.classremainingsize = i.classremainingsize + " seats";
                    i.amount = "$"+i.amount;
                }
                myEntry.ItemsSource = classtimings;


            }
            catch(Exception ex)
            {
                string msg = ex.Message.ToString();
            }
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
        public async void Getsingledata(string contentid)
        {
            try
            {
                var connected = CrossConnectivity.Current.IsConnected;
                if (connected == true)
                {
                    _Dialog.ShowLoading("");
                    var nsAPI = RestService.For<ILS_Listing>(Commonsettings.Localservices);
                    var data = await nsAPI.Getonlineclassessingleinfo(contentid,Commonsettings.UserPid);
                    if(data.ROW_DATA.Count>0)
                    {
                        Classtitle = data.ROW_DATA[0].classmastertitle;
                        Contactname = data.ROW_DATA[0].contributor;
                        Classtime = data.ROW_DATA[0].classtimings;
                        if (data.ROW_DATA[0].sizeallowed != null)
                            Sizeallowed = data.ROW_DATA[0].sizeallowed + " students";
                        else
                            Sizeallowed = "50 students";
                        totalamnt = "$"+data.ROW_DATA[0].amount;
                        langauges = data.ROW_DATA[0].languages;
                        Classmodes = data.ROW_DATA[0].classmodes;
                        Classlevels = data.ROW_DATA[0].classlevels;
                        Classlength = data.ROW_DATA[0].classlengthtime;
                        Isfavorite = data.ROW_DATA[0].issavedclass;
                        title = data.ROW_DATA[0].classmastertitle;
                        guserid = data.ROW_DATA[0].guserid;
                        linkurl = "https://us.sulekha.com/volive/" + data.ROW_DATA[0].tagurl + "/" + data.ROW_DATA[0].classmasertitleurl + "_" + contentid;
                        if (Isfavorite == "0")
                        {
                            Isfavoriteimg = "HeartGray.png";
                        }
                        else
                        {
                            Isfavoriteimg = "HeartGrayActive.png";
                        }
                        OnPropertyChanged(nameof(Isfavoriteimg));
                        if (data.ROW_DATA[0].minage != null && data.ROW_DATA[0].maxage != null)
                            Age = data.ROW_DATA[0].minage + " - " + data.ROW_DATA[0].maxage;
                        else
                            Age = "-";
                    }
                }
                else
                {
                    _Dialog.HideLoading();
                    _Dialog.Toast("Kindly check your internet connection");
                }
                }
            catch(Exception ex)
            {
                string msg = ex.Message.ToString();
            }
        }

        public async void Getinstructordetails(string contentid)
        {
            try
            {
                var nsAPI = RestService.For<ILS_Listing>(Commonsettings.Localservices);
                var data = await nsAPI.Getinstructordetails(contentid);
                if(data.ROW_DATA.Count>0)
                {
                    Expe = data.ROW_DATA[0].instructorexp;
                    Tag = data.ROW_DATA[0].instructorskills;
                    Instructorlang = data.ROW_DATA[0].instructorlang;
                    Taught = data.ROW_DATA[0].instructoraudience;
                    if(Expe!= "0yrs")
                    {
                        isexp = true;
                    }
                    if (data.ROW_DATA[0].instructorprofileimg != "" && data.ROW_DATA[0].instructorprofileimg != null)
                        Profileimg = data.ROW_DATA[0].instructorprofileimg;
                   
                    else
                        Profileimg = "ProfileUserIcon.png";
                    OnPropertyChanged(nameof(Profileimg));
                }
            }
            catch(Exception ex)
            {
                string msg = ex.Message.ToString();
            }
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
