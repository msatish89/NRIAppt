using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.LocalService.Features.Interfaces;
using NRIApp.LocalService.Features.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Plugin.Messaging;
using Plugin.Share;
using NRIApp.Controls;
using System.Threading.Tasks;
using NRIApp.Roommates.Features.Post.Models;

namespace NRIApp.LocalService.Features.ViewModels
{
    public class LS_Detail_VM : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs _Dialog = UserDialogs.Instance;
        private string _title { get; set; }
        public string title { get { return _title; } set { _title = value; } }
        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        private string _Pagedescription { get; set; }
        public string Pagedescription { get { return _Pagedescription; } set { _Pagedescription = value; } }

        private string _CloseorOpen { get; set; }
        public string CloseorOpen { get { return _CloseorOpen; } set { _CloseorOpen = value; } }
        public string miles { get; set; }
        private string _rating { get; set; }
        public string rating { get { return _rating; } set { _rating = value; } }
        private bool _isratingavil { get; set; }
        public bool isratingavil { get { return _isratingavil; } set { _isratingavil = value; } }
        private string _Primarytags { get; set; }
        public string Primarytags
        {
            get { return _Primarytags; }
            set
            {
                _Primarytags = value;

            }
        }
        private string _Remainphots { get; set; }
        public string Remainphots
        {
            get { return _Remainphots; }
            set
            {
                _Remainphots = value;

            }
        }
        private string _Contactname { get; set; }
        public string Contactname
        {
            get { return _Contactname; }
            set
            {
                _Contactname = value;

            }
        }
        private string _Citystatecode { get; set; }
        public string Citystatecode
        {
            get { return _Citystatecode; }
            set
            {
                _Citystatecode = value;

            }
        }
        private string _Adtimings { get; set; }
        public string Adtimings
        {
            get { return _Adtimings; }
            set
            {
                _Adtimings = value;

            }
        }
        private string _Adtimingsother { get; set; }
        public string Adtimingsother
        {
            get { return _Adtimingsother; }
            set
            {
                _Adtimingsother = value;

            }
        }
        private string _AdCloseoropen { get; set; }
        public string AdCloseoropen
        {
            get { return _AdCloseoropen; }
            set
            {
                _AdCloseoropen = value;

            }
        }
        private bool _AdCloseoropenother { get; set; }
        public bool AdCloseoropenother
        {
            get { return _AdCloseoropenother; }
            set
            {
                _AdCloseoropenother = value;

            }
        }
        string adlat = "", adlong = "", adcity = "";
        public List<string> Listservices { get; set; }
        public List<string> Listservicesmore { get; set; }

        public List<string> Listtoptags { get; set; }

        public List<LS_LTYPE_DATA> Listltype5 { get; set; }
        public List<LS_LTYPE_DATA> Listltype5more { get; set; }
        public List<LS_LTYPE_DATA> Listltype4 { get; set; }
        public List<LS_LTYPE_DATA> Listltype4more { get; set; }
        public List<LS_AD_CITIES_DATA> Listadcities1 { get; set; }
        public List<LS_AD_CITIES_DATA> Listadcities2 { get; set; }
        public List<LS_AD_PHOTOS_DATA> ListAdPhotos { get; set; }
        public List<LS_AD_VIDEOS_DATA> ListAdVideos { get; set; }

        public List<LS_ONLINE_DETAIL_DATA> Listonlineclass { get; set; }
        private bool _Listltype5visible = true;
        public bool Listltype5visible { get { return _Listltype5visible; } set { _Listltype5visible = value; } }

        private bool _Listltype4visible = true;
        public bool Listltype4visible { get { return _Listltype4visible; } set { _Listltype4visible = value; } }

        private bool _Accreditationsavail = true;
        public bool Accreditationsavail { get { return _Accreditationsavail; } set { _Accreditationsavail = value; } }


        private bool _Accreditationsnoavail = true;
        public bool Accreditationsnoavail { get { return _Accreditationsnoavail; } set { _Accreditationsnoavail = value; } }

        private bool _Admorecities = true;
        public bool Admorecities { get { return _Admorecities; } set { _Admorecities = value; } }

        private string _Adcountcities { get; set; }
        public string Adcountcities
        {
            get { return _Adcountcities; }
            set
            {
                _Adcountcities = value;

            }
        }
        private bool _Citiserved2 = true;
        public bool Citiserved2 { get { return _Citiserved2; } set { _Citiserved2 = value; } }

        private bool _IsVideoAvail = true;
        public bool IsVideoAvail { get { return _IsVideoAvail; } set { _IsVideoAvail = value; } }

        private bool _IsPhotoAvail = true;
        public bool IsPhotoAvail { get { return _IsPhotoAvail; } set { _IsPhotoAvail = value; } }

        private bool _IsClasssesAvail = true;
        public bool IsClasssesAvail { get { return _IsClasssesAvail; } set { _IsClasssesAvail = value; } }
        private string _MorecitiIcon = "plusIcon.png";
        public string MorecitiIcon
        {
            get { return _MorecitiIcon; }
            set
            {
                _MorecitiIcon = value;

            }
        }

        public string _rating1 = "Star_inactive.png";
        public string Rating1
        {
            get { return _rating1; }
            set
            {
                _rating1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Rating1)));

            }
        }
        public string _rating2 = "Star_inactive.png";
        public string Rating2
        {
            get { return _rating2; }
            set
            {
                _rating2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Rating2)));

            }
        }
        public string _rating3 = "Star_inactive.png";
        public string Rating3
        {
            get { return _rating3; }
            set
            {
                _rating3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Rating3)));

            }
        }
        public string _rating4 = "Star_inactive.png";
        public string Rating4
        {
            get { return _rating4; }
            set
            {
                _rating4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Rating4)));

            }
        }
        public string _rating5 = "Star_inactive.png";
        public string Rating5
        {
            get { return _rating5; }
            set
            {
                _rating5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Rating5)));

            }
        }
        public string _ratingbar = "0/5";
        public string Ratingbar
        {
            get { return _ratingbar; }
            set
            {
                _ratingbar = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Ratingbar)));

            }
        }
        public string _revrating = "";
        public string Rating
        {
            get { return _revrating; }
            set
            {
                _revrating = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Rating)));

            }
        }
        //Quality rating
        public string _Qualityrating1 = "Star_inactive.png";
        public string QualityRating1
        {
            get { return _Qualityrating1; }
            set
            {
                _Qualityrating1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QualityRating1)));

            }
        }
        public string _Qualityrating2 = "Star_inactive.png";
        public string QualityRating2
        {
            get { return _Qualityrating2; }
            set
            {
                _Qualityrating2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QualityRating2)));

            }
        }
        public string _Qualityrating3 = "Star_inactive.png";
        public string QualityRating3
        {
            get { return _Qualityrating3; }
            set
            {
                _Qualityrating3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QualityRating3)));

            }
        }
        public string _Qualityrating4 = "Star_inactive.png";
        public string QualityRating4
        {
            get { return _Qualityrating4; }
            set
            {
                _Qualityrating4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QualityRating4)));

            }
        }
        public string _Qualityrating5 = "Star_inactive.png";
        public string QualityRating5
        {
            get { return _Qualityrating5; }
            set
            {
                _Qualityrating5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QualityRating5)));

            }
        }
        public string _revqualityrating = "";
        public string QualityRating
        {
            get { return _revqualityrating; }
            set
            {
                _revqualityrating = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QualityRating)));

            }
        }

        //quality rating

        //value rating
        public string _Valuerating1 = "Star_inactive.png";
        public string ValueRating1
        {
            get { return _Valuerating1; }
            set
            {
                _Valuerating1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ValueRating1)));

            }
        }
        public string _Valuerating2 = "Star_inactive.png";
        public string ValueRating2
        {
            get { return _Valuerating2; }
            set
            {
                _Valuerating2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ValueRating2)));

            }
        }
        public string _Valuerating3 = "Star_inactive.png";
        public string ValueRating3
        {
            get { return _Valuerating3; }
            set
            {
                _Valuerating3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ValueRating3)));

            }
        }
        public string _Valuerating4 = "Star_inactive.png";
        public string ValueRating4
        {
            get { return _Valuerating4; }
            set
            {
                _Valuerating4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ValueRating4)));

            }
        }
        public string _Valuerating5 = "Star_inactive.png";
        public string ValueRating5
        {
            get { return _Valuerating5; }
            set
            {
                _Valuerating5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ValueRating5)));

            }
        }
        public string _revvaluerating = "";
        public string ValueRating
        {
            get { return _revvaluerating; }
            set
            {
                _revvaluerating = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ValueRating)));

            }
        }
        //value  rating

        //customer rating
        public string _Customerrating1 = "Star_inactive.png";
        public string CustomerRating1
        {
            get { return _Customerrating1; }
            set
            {
                _Customerrating1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomerRating1)));

            }
        }
        public string _Customerrating2 = "Star_inactive.png";
        public string CustomerRating2
        {
            get { return _Customerrating2; }
            set
            {
                _Customerrating2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomerRating2)));

            }
        }
        public string _Customerrating3 = "Star_inactive.png";
        public string CustomerRating3
        {
            get { return _Customerrating3; }
            set
            {
                _Customerrating3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomerRating3)));

            }
        }
        public string _Customerrating4 = "Star_inactive.png";
        public string CustomerRating4
        {
            get { return _Customerrating4; }
            set
            {
                _Customerrating4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomerRating4)));

            }
        }
        public string _Customerrating5 = "Star_inactive.png";
        public string CustomerRating5
        {
            get { return _Customerrating5; }
            set
            {
                _Customerrating5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomerRating5)));

            }
        }
        public string _revcustomerrating = "";
        public string CustomerRating
        {
            get { return _revcustomerrating; }
            set
            {
                _revcustomerrating = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomerRating)));

            }
        }
        //customer rating

        private string _Reviewdescription = "";
        public string Reviewdescription
        {
            get { return _Reviewdescription; }
            set
            {
                _Reviewdescription = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Reviewdescription)));

            }
        }
        private bool _reviewvisible = false;
        public bool reviewvisible
        {
            get { return _reviewvisible; }
            set { _reviewvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(reviewvisible)));
            }
        }
        private bool _Reviewmoreblk = false;
        public bool Reviewmoreblk
        {
            get { return _Reviewmoreblk; }
            set
            {
                _Reviewmoreblk = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Reviewmoreblk)));
            }
        }
        private bool _Reviewloadblk = false;
        public bool Reviewloadblk
        {
            get { return _Reviewloadblk; }
            set
            {
                _Reviewloadblk = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Reviewloadblk)));
            }
        }
        
        private bool _Revimgnovisible = false;
        public bool Revimgnovisible
        {
            get { return _Revimgnovisible; }
            set
            {
                _Revimgnovisible = value;
               

            }
        }
        private bool _Revimgvisible = false;
        public bool Revimgvisible
        {
            get { return _Revimgvisible; }
            set
            {
                _Revimgvisible = value;


            }
        }
        private string _Revtext = "";
        public string Revtext
        {
            get { return _Revtext; }
            set
            {
                _Revtext = value;


            }
        }
        private string _Revimage = "";
        public string Revimage
        {
            get { return _Revimage; }
            set
            {
                _Revimage = value;


            }
        }
        private string _Revcontributor = "";
        public string Revcontributor
        {
            get { return _Revcontributor; }
            set
            {
                _Revcontributor = value;


            }
        }
        private string _Revpostedago = "";
        public string Revpostedago
        {
            get { return _Revpostedago; }
            set
            {
                _Revpostedago = value;


            }
        }
        private string _Revrating = "";
        public string Revrating
        {
            get { return _Revrating; }
            set
            {
                _Revrating = value;


            }
        }
        private string _Revdesc = "";
        public string Revdesc
        {
            get { return _Revdesc; }
            set
            {
                _Revdesc = value;


            }
        }
        private string _Loginname = "";
        public string Loginname
        {
            get { return _Loginname; }
            set
            {
                _Loginname = value;


            }
        }
        private string _Replydesc = "";
        public string Replydesc
        {
            get { return _Replydesc; }
            set
            {
                _Replydesc = value;


            }
        }
        private string _Revlogintext = "";
        public string Revlogintext
        {
            get { return _Revlogintext; }
            set
            {
                _Revlogintext = value;


            }
        }
        private string _Replycomment = "";
        public string Replycomment
        {
            get { return _Replycomment; }
            set
            {
                _Replycomment = value;


            }
        }
        public string _Isfavoriteimg;
        public string Isfavoriteimg
        {
            get { return _Isfavoriteimg; }
            set { _Isfavoriteimg = value; }
        }

        private bool _IsVisibleReportList = false;
        public bool IsVisibleReportList
        {
            get { return _IsVisibleReportList; }
            set
            {
                _IsVisibleReportList = value;


            }
        }

        private string _Reportlist = "";
        public string Reportlist
        {
            get { return _Reportlist; }
            set
            {
                _Reportlist = value;


            }
        }
        private string _Reportmail = "";
        public string Reportmail
        {
            get { return _Reportmail; }
            set
            {
                _Reportmail = value;


            }
        }

        private string _Reportdesc = "";
        public string Reportdesc
        {
            get { return _Reportdesc; }
            set
            {
                _Reportdesc = value;


            }
        }
        private bool _Isrecommendad = false;
        public bool Isrecommendad
        {
            get { return _Isrecommendad; }
            set
            {
                _Isrecommendad = value;


            }
        }
        private string _Recommendimg = "Recommend.png";
        public string Recommendimg
        {
            get { return _Recommendimg; }
            set
            {
                _Recommendimg = value;


            }
        }
        private bool _Isreport = false;
        public bool Isreport
        {
            get { return _Isreport; }
            set
            {
                _Isreport = value;


            }
        }

        private bool _Admoreservices = false;
        public bool Admoreservices
        {
            get { return _Admoreservices; }
            set
            {
                _Admoreservices = value;


            }
        }
        private string _Adcountservices = "";
        public string Adcountservices
        {
            get { return _Adcountservices; }
            set
            {
                _Adcountservices = value;


            }
        }
        private string _MoreservIcon = "plusIcon.png";
        public string MoreservIcon
        {
            get { return _MoreservIcon; }
            set
            {
                _MoreservIcon = value;

            }
        }
        private bool _Servicesvisbile = false;
        public bool Servicesvisbile
        {
            get { return _Servicesvisbile; }
            set
            {
                _Servicesvisbile = value;


            }
        }
        private bool _Admorehighlights = false;
        public bool Admorehighlights
        {
            get { return _Admorehighlights; }
            set
            {
                _Admorehighlights = value;


            }
        }
        private string _Adcounthighlits = "";
        public string Adcounthighlits
        {
            get { return _Adcounthighlits; }
            set
            {
                _Adcounthighlits = value;


            }
        }
        private string _MorehighltsIcon = "plusIcon.png";
        public string MorehighltsIcon
        {
            get { return _MorehighltsIcon; }
            set
            {
                _MorehighltsIcon = value;

            }
        }
        private bool _Highlightsvisible = false;
        public bool Highlightsvisible
        {
            get { return _Highlightsvisible; }
            set
            {
                _Highlightsvisible = value;


            }
        }
        private bool _Admoreotherservices = false;
        public bool Admoreotherservices
        {
            get { return _Admoreotherservices; }
            set
            {
                _Admoreotherservices = value;


            }
        }
        private string _Adcountotherservices = "";
        public string Adcountotherservices
        {
            get { return _Adcountotherservices; }
            set
            {
                _Adcountotherservices = value;


            }
        }
        private string _MoreothersIcon = "plusIcon.png";
        public string MoreothersIcon
        {
            get { return _MoreothersIcon; }
            set
            {
                _MoreothersIcon = value;

            }
        }
        private bool _Otherservicevisble = false;
        public bool Otherservicevisble
        {
            get { return _Otherservicevisble; }
            set
            {
                _Otherservicevisble = value;


            }
        }
        private bool _Israting = false;
        public bool Israting
        {
            get { return _Israting; }
            set
            {
                _Israting = value;


            }
        }
        private string _Closeoropencolor = "Red";
        public string Closeoropencolor
        {
            get { return _Closeoropencolor; }
            set
            {
                _Closeoropencolor = value;

            }
        }
        private bool _Adtimingavail = false;
        public bool Adtimingavail
        {
            get { return _Adtimingavail; }
            set
            {
                _Adtimingavail = value;

                OnPropertyChanged(nameof(Adtimingavail));
            }
        }
        private bool _Adtimingavail1 = false;
        public bool Adtimingavail1
        {
            get { return _Adtimingavail1; }
            set
            {
                _Adtimingavail1 = value;

                OnPropertyChanged(nameof(Adtimingavail1));
            }
        }
        private bool _Adtimingavail2 = false;
        public bool Adtimingavail2
        {
            get { return _Adtimingavail2; }
            set
            {
                _Adtimingavail2 = value;

                OnPropertyChanged(nameof(Adtimingavail2));
            }
        }
        private bool _Adtimingavailother = false;
        public bool Adtimingavailother
        {
            get { return _Adtimingavailother; }
            set
            {
                _Adtimingavailother = value;

                OnPropertyChanged(nameof(Adtimingavailother));
            }
        }

        private bool _Isvirualnoavail = false;
        public bool Isvirualnoavail
        {
            get { return _Isvirualnoavail; }
            set
            {
                _Isvirualnoavail = value;

                OnPropertyChanged(nameof(Isvirualnoavail));
            }
        }
        private bool _Isvirualnoavailother = false;
        public bool Isvirualnoavailother
        {
            get { return _Isvirualnoavailother; }
            set
            {
                _Isvirualnoavailother = value;

                OnPropertyChanged(nameof(Isvirualnoavailother));
            }
        }

       


        public List<LS_AD_SERVICES_DATA> Bizservices { get; set; }
        public List<LS_AD_SERVICES_DATA> Bizservicehead { get; set; }
        public List<LS_AD_GROUP_SERVICES> Bizservicesall { get; set; }
        public List<LS_AD_GROUP_SERVICES> Bizgrpservices { get; set; }

        public List<LS_REVIEWS_DATA> Reviews { get; set; }

        public List<LS_REVIEW_REPLY_DATA> Reviewreply { get; set; }
       public  List<LS_REPORT_LIST> listofreports { get; set; }

        public ICommand Clickmorecities { get; set; }
        public ICommand videoclickcommand { get; set; }
        public ICommand photoclickcommand { get; set; }
        public ICommand Clickmapview { get; set; }
        public ICommand Opendetailscommand { get; set; }
        public ICommand Bizmoduleleadcommand { get; set; }
        public ICommand clickratingcommand { get; set; }
        public ICommand Qualityclickratingcommand { get; set; }
        public ICommand Valueclickratingcommand { get; set; }
        public ICommand Customerclickratingcommand { get; set; }
        public ICommand Publishreview { get; set; }
        public ICommand Morereviews { get; set; }
        public ICommand Getdetailrespform { get; set; }
        public ICommand Getdetailpagecall { get; set; }
        public ICommand ShareLinkcommand { get; set; }
        public ICommand Reviewcomment { get; set; }
        public ICommand Replysubmit { get; set; }
        public ICommand Favoritecommand { get; set; }
        public ICommand ReportFlagcommand { get; set; }
        public ICommand selectreport { get; set; }
        public ICommand Clicksubmitreport { get; set; }
        public ICommand Recommendcommand { get; set; }
        public ICommand Clickmoreservices { get; set; }
        public ICommand Clickmorehighlights { get; set; }
        public ICommand Clickmoreotherservices { get; set; }
        public ICommand Clickonlinelistbuybatch { get; set; }
        public ICommand Clickdetailonlinemore { get; set; }

        string adid = "",sPremiumad="",mobileno="",Isfavorite="",addlink="",detailtitle="",ptagid="",ptagurl="",ptag="",stagid="",stagurl="",stag="",bizreviewid="",
            adtitle="",adtitleurl="",calltotwilio="",twilionumber="",twiliopin="",cityurl="";
        
        public LS_Detail_VM(LS_LISTINGMODEL_DATA LMD)
        {
            _Dialog.ShowLoading("");
            items = new ObservableCollection<GroupServices>();
            Items = new ObservableCollection<GroupServices>();
          
            Bindhighlights(LMD.adid, "5");
            Bindhighlights(LMD.adid, "4");
            Bindcities(LMD.adid);
            BindVideos(LMD.adid);
           
            adid = LMD.adid;
            sPremiumad = LMD.premiumad;
            mobileno = LMD.viewno;
            detailtitle = LMD.title;
            adtitle = LMD.title;
            adtitleurl = LMD.titleurl;
            cityurl = LMD.maincityurl;
            addlink = "http://localservices.sulekha.com/"+LMD.titleurl+ "_"+LMD.maincityurl + "_"+LMD.adid;
            BindAdServices();
            LoadHotelsCommand = new Command(async () => await BindAdServices());
            //LoadHotelsCommand = new Command<LS_LISTINGMODEL_DATA>(async (lmd) => await BindAdServices(lmd));
            RefreshItemsCommand = new Command<GroupServices>((Items2) => ExecuteRefreshItemsCommand(Items2));
            Clickmorecities = new Command(Openmorecities);
            videoclickcommand = new Command<LS_AD_VIDEOS_DATA>(Clickvideo);
            photoclickcommand = new Command<LS_AD_PHOTOS_DATA>(Clickphoto);
            Clickmapview = new Command(Clicktoopenmap);
            Opendetailscommand = new Command<LS_AD_SERVICES_DATA>(Opendetails);
            Bizmoduleleadcommand = new Command(Getrespform);
            clickratingcommand = new Command(clickrating);

            Qualityclickratingcommand = new Command(clickqualityrating);
            Valueclickratingcommand = new Command(clickvaluerating);
            Customerclickratingcommand = new Command(clickcustomerservicerating);

            Publishreview = new Command(ClickPublishreview);
           
            BindPhotos(LMD.adid);
            Bindreviews();
            Morereviews = new Command(ClickMorereviews);
            Getdetailrespform = new Command(Getrespform);
            Getdetailpagecall = new Command(ClickGetdetailpagecall);
            ShareLinkcommand = new Command(Sharelink);
            Reviewcomment = new Command<LS_REVIEWS_DATA>(ClickReviewcomment);
            Favoritecommand = new Command(ClickFavoritecommand);
            ReportFlagcommand = new Command(ClickReportFlagcommand);
            Recommendcommand = new Command(ClickRecommendcommand);
            Clickmoreservices = new Command(Openmoreservices);
            Clickmorehighlights = new Command(Openmorehighlights);
            Clickmoreotherservices = new Command(Openmoreotherservice);
            Clickonlinelistbuybatch = new Command<LS_ONLINE_DETAIL_DATA>(Clickonlinelistbuybatchdetail);
            Clickdetailonlinemore = new Command<LS_ONLINE_DETAIL_DATA>(Clickdetailonlinemoredetail);
            Bindonlineclasses(LMD.adid);
            Binddetailpagedata(LMD);

        }

       

        public LS_Detail_VM(string reviewid,string adid)
        {
            Binddetailreviews(reviewid);
            Bindreviewreply(reviewid);
            bizreviewid = reviewid;
            Replysubmit = new Command(Replyclicksubmit);
        }
        public LS_Detail_VM(string sadid,string sadtitle,string saddlink)
        {
            adid = sadid;adtitle = sadtitle;addlink = saddlink;
            List<LS_REPORT_LIST> listofreportsl = new List<LS_REPORT_LIST>();
            listofreportsl.Add(new LS_REPORT_LIST { reportlist = "Spam", reportid = 1 });
            listofreportsl.Add(new LS_REPORT_LIST { reportlist = "Duplicate", reportid = 8 });
            listofreportsl.Add(new LS_REPORT_LIST { reportlist = "Prohibited / fraud", reportid = 2 });
            listofreportsl.Add(new LS_REPORT_LIST { reportlist = "Miscategorized", reportid = 3 });
            listofreportsl.Add(new LS_REPORT_LIST { reportlist = "Invalid contact details", reportid = 6 });
            listofreportsl.Add(new LS_REPORT_LIST { reportlist = "No Reply", reportid = 7 });
            listofreports = listofreportsl;
            OnPropertyChanged(nameof(listofreports));
            selectreport = new Command<LS_REPORT_LIST>(Clicktoselectreport);
            Clicksubmitreport = new Command(Submitreport);


        }

        public async void Clickdetailonlinemoredetail(LS_ONLINE_DETAIL_DATA LOD)
        {
            try
            {
                var curpage = LS_ViewModel.GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Detail.LS_Online_Detail(LOD.classmasterid));
                
            }
            catch (Exception e)
            {

            }
        }

        public async void Clickonlinelistbuybatchdetail(LS_ONLINE_DETAIL_DATA LOD)
        {
            try
            {
                _Dialog.ShowLoading("");
                var curpage = LS_ViewModel.GetCurrentPage();
               
                await curpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Detail.OnlineClassPayment(LOD.guserid,LOD.classdetailid));
                _Dialog.HideLoading();
            }
            catch(Exception e)
            {

            }
        }
        public void Openmoreservices()
        {
            if (Servicesvisbile)
            {
                Servicesvisbile = false;
                Admoreservices = true;
                Adcountservices = Listservicesmore.Count + " less services";
            }
            else
            {
                Servicesvisbile = true;
                Admoreservices = false;
                Adcountservices = Listservicesmore.Count + " more services";
            }
            OnPropertyChanged(nameof(Servicesvisbile));
            OnPropertyChanged(nameof(Admoreservices));
        }
        public void Openmorehighlights()
        {
            if (Highlightsvisible)
            {
                Highlightsvisible = false;
                Admorehighlights = true;
                Adcounthighlits = Listltype5more.Count + " less highlights";
            }
            else
            {
                Highlightsvisible = true;
                Admorehighlights = false;
                Adcounthighlits = Listltype5more.Count + " more highlights";
            }
            OnPropertyChanged(nameof(Highlightsvisible));
            OnPropertyChanged(nameof(Admorehighlights));
        }
        public void Openmoreotherservice()
        {
            if (Otherservicevisble)
            {
                Otherservicevisble = false;
                Admoreotherservices = true;
            }
            else
            {
                Otherservicevisble = true;
                Admoreotherservices = false;
            }
            OnPropertyChanged(nameof(Otherservicevisble));
            OnPropertyChanged(nameof(Admoreotherservices));
        }
        
        public async void ClickRecommendcommand()
        {
            try
            {
                _Dialog.ShowLoading("");
                var recommend = RestService.For<ILS_Detailpage>(Commonsettings.TechjobsAPI);
                string pid = Commonsettings.UserPid;
                if (string.IsNullOrEmpty(pid))
                {
                    pid = "0";
                }
                var data =await  recommend.Submitrecommendads(adid, pid);
                if (data.ROW_DATA.Count>0)
                {
                    if (data.ROW_DATA[0].result=="1")
                    {
                        Recommendimg = "RecommendActive.png";
                    }
                    else
                    {
                        Recommendimg = "Recommend.png";
                    }
                    OnPropertyChanged(nameof(Recommendimg));
                }
                _Dialog.HideLoading();

            }
            catch(Exception e) { _Dialog.HideLoading(); }
        }
        public bool Reportvalidation()
        {
            bool result = true;
            if (string.IsNullOrEmpty(selectedreportid))
            {
                _Dialog.Toast("Please select the reson");
                return false;
            }
            if (string.IsNullOrEmpty(Reportdesc))
            {
                _Dialog.Toast("Please wirte a description");
                return false;
            }
            if (string.IsNullOrEmpty(Reportmail))
            {
                _Dialog.Toast("Please wirte a email");
                return false;
            }
            if (!Regex.IsMatch(Reportmail, Emailpattern))
            {
                _Dialog.Toast("Enter Proper Email");
                return false;
            }
          
            return result;
        }
        public async void Submitreport()
        {
            if (Reportvalidation())
            {
                try {
                    _Dialog.ShowLoading("");
                    var report = RestService.For<ILS_Detailpage>(Commonsettings.TechjobsAPI);
                    string pid = Commonsettings.UserPid;
                    if (string.IsNullOrEmpty(pid))
                    {
                        pid = "0";
                    }
                    var data = await report.Sendreport(selectedreportid, Reportmail, Reportdesc, adid, adtitle, addlink, pid, Commonsettings.UserEmail, "");
                    if (data.ROW_DATA.Count>0)
                    {
                        if(data.ROW_DATA[0].resultinfo== "inserted success")
                        {
                            _Dialog.Toast("Successfully reported.");
                        }
                        else
                        {
                            _Dialog.Toast("You are already reported!");
                        }
                    }
                    _Dialog.HideLoading();
                }catch(Exception e) { _Dialog.HideLoading(); }
                }
        }
        string selectedreportid = "";
        public void Clicktoselectreport(LS_REPORT_LIST LRT)
        {
            Reportlist = LRT.reportlist;
            selectedreportid = Convert.ToString(LRT.reportid);
            OnPropertyChanged(nameof(Reportlist));
            var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();
            var grid = CurrentPage.FindByName<HVScrollGridView>("Reportdata");
            grid.IsVisible = false;
        }



        public async void ClickReportFlagcommand()
        {
            var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();
            if (Commonsettings.UserPid != "" && Commonsettings.UserPid != "0" &&!string.IsNullOrEmpty(Commonsettings.UserPid))
            {
             
            await CurrentPage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Detail.LS_Report(adid,adtitle,addlink));
            }
            else
            {
                try
                {
                    await CurrentPage.Navigation.PushModalAsync(new Signin.Views.Login());
                }catch(Exception e)
                {

                }
            }
        }
        public async void ClickFavoritecommand()
        {
            try
            {
                var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();
                if (Commonsettings.UserPid != "" && Commonsettings.UserPid != "0"&&!string.IsNullOrEmpty(Commonsettings.UserPid))
                {
                    _Dialog.ShowLoading("");
                    var Issaved = RestService.For<ILS_Detailpage>(Commonsettings.TechjobsAPI);
                    var data = await Issaved.SaveandremoveBizfavorite(adid, Commonsettings.UserPid, Commonsettings.UserEmail, Isfavorite);
                    if (data.ROW_DATA.Count > 0)
                    {
                        if (Isfavorite == "0")
                        {
                            Isfavoriteimg = "FavoriteActive.png";
                            Isfavorite = "1";
                        }
                        else
                        {
                            Isfavoriteimg = "Favorite.png";
                            Isfavorite = "0";
                        }
                    }
                }
                else
                {
                    try
                    {
                        await CurrentPage.Navigation.PushModalAsync(new Signin.Views.Login());
                    }catch(Exception ee)
                    {

                    }
                }
                OnPropertyChanged(nameof(Isfavoriteimg));
                _Dialog.HideLoading();
            }
            catch(Exception e)
            {
                _Dialog.HideLoading();
            }
        }
        public async void Replyclicksubmit()
        {
            try
            {
                if (string.IsNullOrEmpty(Replycomment))
                {
                    _Dialog.Toast("Please enter comment");
                }
                else
                {
                    _Dialog.ShowLoading("");
                    var detailpagedata = RestService.For<ILS_Detailpage>(Commonsettings.TechjobsAPI);
                    var data = await detailpagedata.PostReviewReply(bizreviewid, adid, Replycomment, Commonsettings.UserName, Commonsettings.UserPid);
                    if (data.ROW_DATA.Count > 0)
                    {
                        Bindreviewreply(bizreviewid);
                    }
                }
                _Dialog.HideLoading();
            }
            catch(Exception re) { _Dialog.HideLoading(); }
         }
        public async void Bindreviewreply(string reviewid)
        {
            try
            {
                //reviewid = "32897";
                var detailpagedata = RestService.For<ILS_Detailpage>(Commonsettings.TechjobsAPI);
                var data = await detailpagedata.GetAdReviewReply(reviewid);
                if (data.ROW_DATA.Count>0)
                {
                    foreach (var item in data.ROW_DATA)
                    {
                       
                       item.replytext = item.contributor.Substring(0, 1).ToUpper();
                      
                    }
                    OnPropertyChanged(nameof(Reviewreply));
                    Reviewreply = data.ROW_DATA;
                    var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();
                    var listreviewreply = CurrentPage.FindByName<HVScrollGridView>("listreviewreply");
                    listreviewreply.ItemsSource = null;
                    listreviewreply.ItemsSource = Reviewreply;
                }

                _Dialog.HideLoading();
            }
            catch(Exception e) { _Dialog.HideLoading(); }
        }
        public async void Binddetailreviews(string reviewid)
        {
            try
            {
                _Dialog.ShowLoading("");
                var detailpagedata = RestService.For<ILS_Detailpage>(Commonsettings.TechjobsAPI);
                var data = await detailpagedata.GetAdDetailReview(reviewid);
                if (data.ROW_DATA.Count>0)
                {
                    if (!string.IsNullOrEmpty(data.ROW_DATA[0].photourl))
                    {
                        Revimage = data.ROW_DATA[0].photourl.ToString();
                        Revimgvisible = true;
                        Revimgnovisible = false;
                    }
                    else
                    {
                        Revtext = data.ROW_DATA[0].contributor.Substring(0, 1).ToUpper();
                        Revimgvisible = false;
                        Revimgnovisible = true;
                    }
                    Revcontributor = data.ROW_DATA[0].contributor.ToString();
                    Revpostedago = data.ROW_DATA[0].postedago.ToString();
                    Revrating = data.ROW_DATA[0].rating.ToString();
                    Revdesc = data.ROW_DATA[0].description.ToString();
                    Loginname = Commonsettings.UserName;
                    Revlogintext = Commonsettings.UserName.Substring(0, 1).ToUpper();
                    OnPropertyChanged(nameof(Revimage)); OnPropertyChanged(nameof(Revimgvisible));
                    OnPropertyChanged(nameof(Revimgnovisible)); OnPropertyChanged(nameof(Revtext));
                    OnPropertyChanged(nameof(Revcontributor)); OnPropertyChanged(nameof(Revpostedago));
                    OnPropertyChanged(nameof(Revrating)); OnPropertyChanged(nameof(Revdesc));
                    OnPropertyChanged(nameof(Loginname)); OnPropertyChanged(nameof(Revlogintext));
                }

            }
            catch(Exception e)
            {

            }
        }

        public async void ClickReviewcomment(LS_REVIEWS_DATA LR)
        {
            var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();
            if (Commonsettings.UserPid !="" && Commonsettings.UserPid != "0" &&!string.IsNullOrEmpty(Commonsettings.UserPid))
            {
                   
                    await CurrentPage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Detail.LS_Review_Detail(LR.reviewid, adid));
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
            
        }
        public async void ClickGetdetailpagecall()
        {
            try
            {
                if (calltotwilio == "1")
                {
                    _Dialog.ShowLoading("");

                    string dynamiccallpin = twiliopin;
                   
                    _Dialog.HideLoading();
                    var answer = await _Dialog.ConfirmAsync("To connect, please enter this pin in dial pad after dialing user phone number.", dynamiccallpin, "OK", "");
                    if (answer)
                    {
                        var phonecall = CrossMessaging.Current.PhoneDialer;
                        if (phonecall.CanMakePhoneCall)
                            phonecall.MakePhoneCall(twilionumber, null);

                    }
                }
                else
                {
                    var phonecall = CrossMessaging.Current.PhoneDialer;
                    if (phonecall.CanMakePhoneCall)
                        phonecall.MakePhoneCall(mobileno, null);
                }

            }catch(Exception e)
            {

            }
           
        }
        public void Sharelink()
        {
            CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
            {
                Text = "",
                Title = adtitle,
                Url = addlink.Trim()
            });
        }

        public async void ClickMorereviews()
        {
            reviewpageno = reviewpageno + 1;
            try
            {
                Reviewmoreblk = false;
                Reviewloadblk = true;
                var detailpagedata = RestService.For<ILS_Detailpage>(Commonsettings.TechjobsAPI);
                var data = await detailpagedata.GetAdReviews(adid, Convert.ToString(reviewpageno));
                if (data != null)
                {
                    reviewvisible = true;

                    foreach (var item in data.ROW_DATA)
                    {
                        if (string.IsNullOrEmpty(item.photourl))
                        {
                            item.imgtext = item.contributor.Substring(0, 1).ToUpper();
                            item.imgvisible = false;
                            item.imgnovisible = true;
                        }
                        else
                        {
                            item.imgnovisible = false;
                            item.imgvisible = true;
                        }

                    }
                    var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
                    var grid = currentpage.FindByName<HVScrollGridView>("listbizreview");

                    Reviews?.AddRange(data.ROW_DATA);
                    Reviewloadblk = false;
                    Reviewmoreblk = false;
                    grid.ItemsSource = null;
                    grid.ItemsSource = Reviews;
                   
                   
                    if (data.ROW_DATA.First().ovreviewcount != Reviews.Count)
                    {
                        Reviewmoreblk = true;
                    }
                    else
                    {
                        Reviewmoreblk = false;
                    }
                }
            }catch(Exception e) { }
        }
        int reviewpageno = 1;
        public async void Bindreviews()
        {
            try
            {
               
                var detailpagedata = RestService.For<ILS_Detailpage>(Commonsettings.TechjobsAPI);
                var data = await detailpagedata.GetAdReviews(adid, Convert.ToString(reviewpageno));
                if (data.ROW_DATA != null&& data.ROW_DATA.Count>0)
                {
                    reviewvisible = true;

                    foreach (var item in data.ROW_DATA)
                    {
                        if (string.IsNullOrEmpty(item.photourl))
                        {
                            item.imgtext = item.contributor.Substring(0, 1).ToUpper();
                            item.imgvisible = false;
                            item.imgnovisible = true;
                        }
                        else
                        {
                            item.imgnovisible = false;
                            item.imgvisible = true;
                        }
                    }
                    if (data.ROW_DATA.First().ovreviewcount > 10)
                    {
                        Reviewmoreblk = true;
                    }                    

                    Reviews = data.ROW_DATA;

                    OnPropertyChanged(nameof(Reviews));
                    OnPropertyChanged(nameof(Reviewmoreblk));
                    var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
                    var grid = currentpage.FindByName<HVScrollGridView>("listbizreview");
                    grid.ItemsSource = null;
                    grid.ItemsSource = Reviews;
                   // _Dialog.HideLoading();
                }
                else
                {
                    reviewvisible = false;
                  //  _Dialog.HideLoading();
                }
                OnPropertyChanged(nameof(reviewvisible));
            }catch(Exception e) {
                _Dialog.HideLoading();
            }

        }

        public bool Reviewvalidation()
        {
            var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();
            
            bool result = true;
            if (string.IsNullOrEmpty(Reviewdescription))
            {
                _Dialog.Toast("Enter Description");
                var entry = CurrentPage.FindByName<Editor>("reviewdesc");
                entry.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(Rating)||Rating=="0")
            {
                _Dialog.Toast("Please Select Rating");
                return false;
            }
            return result;
        }
        public async void ClickPublishreview()
        {
            try { 
            var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
            if (!string.IsNullOrEmpty(Commonsettings.UserPid)&&Commonsettings.UserPid !="0")
            {           
            if (Reviewvalidation())
            {
                        _Dialog.ShowLoading("");
                        var detailpagedata = RestService.For<ILS_Detailpage>(Commonsettings.TechjobsAPI);
                        var data = await detailpagedata.PublishReview(adid,Rating,Commonsettings.UserName,"",Commonsettings.UserPid, detailtitle, Reviewdescription, addlink,LS_ViewModel.primarytagid,LS_ViewModel.primarytag,LS_ViewModel.primarytagurl,LS_ViewModel.supertagid,LS_ViewModel.supertag,LS_ViewModel.supertagurl,Commonsettings.Usercity,Commonsettings.Userstatecode,Commonsettings.Userzipcode,Convert.ToString(Commonsettings.UserLat), Convert.ToString(Commonsettings.UserLong),Commonsettings.Usercountry);
                        if (data.ROW_DATA.Count>0)
                        {
                            if (data.ROW_DATA[0].resultinformation== "inserted success")
                            {
                                _Dialog.HideLoading();
                                _Dialog.Toast("Thank you! Your review will be published after verification");
                                reviewpageno = 1;
                                Bindreviews();
                                Reviewdescription = "";
                                OnPropertyChanged(nameof(Reviewdescription));
                                Rating = "0";
                                Rating1 = Rating2 = Rating3 = Rating4 = Rating5 = "Star_inactive.png";
                                QualityRating1 = QualityRating2 = QualityRating3 = QualityRating4 = QualityRating5 = "Star_inactive.png";
                                ValueRating1 = ValueRating2 = ValueRating3 = ValueRating4 = ValueRating5 = "Star_inactive.png";
                                CustomerRating1 = CustomerRating2 = CustomerRating3 = CustomerRating4 = CustomerRating5 = "Star_inactive.png";
                                Ratingbar = "0/5";
                                OnPropertyChanged(nameof(Rating)); OnPropertyChanged(nameof(Rating1));
                                OnPropertyChanged(nameof(Rating2)); OnPropertyChanged(nameof(Rating3));
                                OnPropertyChanged(nameof(Rating4)); OnPropertyChanged(nameof(Rating5));
                                OnPropertyChanged(nameof(Ratingbar));
                                OnPropertyChanged(nameof(QualityRating1)); OnPropertyChanged(nameof(QualityRating2));
                                OnPropertyChanged(nameof(QualityRating3)); OnPropertyChanged(nameof(QualityRating4));
                                OnPropertyChanged(nameof(QualityRating5));
                                OnPropertyChanged(nameof(ValueRating1)); OnPropertyChanged(nameof(ValueRating2));
                                OnPropertyChanged(nameof(ValueRating3)); OnPropertyChanged(nameof(ValueRating4));
                                OnPropertyChanged(nameof(ValueRating5));
                                OnPropertyChanged(nameof(CustomerRating1)); OnPropertyChanged(nameof(CustomerRating2));
                                OnPropertyChanged(nameof(CustomerRating3)); OnPropertyChanged(nameof(CustomerRating4));
                                OnPropertyChanged(nameof(CustomerRating5));

                            }
                        }

            }
            }
            else
            {
                    try { 
               await curpage.Navigation.PushModalAsync(new Signin.Views.Login());
                    }
                    catch (Exception ee)
                    {

                    }
                    // ClickPublishreview();
                }
            }
            catch (Exception e) { _Dialog.HideLoading(); }
        }

        private void clickcustomerservicerating(object sender)
        {
            var view = sender as Xamarin.Forms.Image;

            string id = view.ClassId.ToString();
            if (id == "1")
            {
                CustomerRating1 = "StarActive.png";
                CustomerRating2 = "Star_inactive.png";
                CustomerRating3 = "Star_inactive.png";
                CustomerRating4 = "Star_inactive.png";
                CustomerRating5 = "Star_inactive.png";
            }
            else if (id == "2")
            {
                CustomerRating1 = "StarActive.png";
                CustomerRating2 = "StarActive.png";
                CustomerRating3 = "Star_inactive.png";
                CustomerRating4 = "Star_inactive.png";
                CustomerRating5 = "Star_inactive.png";
            }
            else if (id == "3")
            {
                CustomerRating1 = "StarActive.png";
                CustomerRating2 = "StarActive.png";
                CustomerRating3 = "StarActive.png";
                CustomerRating4 = "Star_inactive.png";
                CustomerRating5 = "Star_inactive.png";
            }
            else if (id == "4")
            {
                CustomerRating1 = "StarActive.png";
                CustomerRating2 = "StarActive.png";
                CustomerRating3 = "StarActive.png";
                CustomerRating4 = "StarActive.png";
                CustomerRating5 = "Star_inactive.png";
            }
            else if (id == "5")
            {
                CustomerRating1 = "StarActive.png";
                CustomerRating2 = "StarActive.png";
                CustomerRating3 = "StarActive.png";
                CustomerRating4 = "StarActive.png";
                CustomerRating5 = "StarActive.png";
            }
            CustomerRating = id;
        }
        private void clickvaluerating(object sender)
        {
            var view = sender as Xamarin.Forms.Image;

            string id = view.ClassId.ToString();
            if (id == "1")
            {
                ValueRating1 = "StarActive.png";
                ValueRating2 = "Star_inactive.png";
                ValueRating3 = "Star_inactive.png";
                ValueRating4 = "Star_inactive.png";
                ValueRating5 = "Star_inactive.png";
               
            }
            else if (id == "2")
            {
                ValueRating1 = "StarActive.png";
                ValueRating2 = "StarActive.png";
                ValueRating3 = "Star_inactive.png";
                ValueRating4 = "Star_inactive.png";
                ValueRating5 = "Star_inactive.png";
               
            }
            else if (id == "3")
            {
                ValueRating1 = "StarActive.png";
                ValueRating2 = "StarActive.png";
                ValueRating3 = "StarActive.png";
                ValueRating4 = "Star_inactive.png";
                ValueRating5 = "Star_inactive.png";
               
            }
            else if (id == "4")
            {
                ValueRating1 = "StarActive.png";
                ValueRating2 = "StarActive.png";
                ValueRating3 = "StarActive.png";
                ValueRating4 = "StarActive.png";
                ValueRating5 = "Star_inactive.png";
               
            }
            else if (id == "5")
            {
                ValueRating1 = "StarActive.png";
                ValueRating2 = "StarActive.png";
                ValueRating3 = "StarActive.png";
                ValueRating4 = "StarActive.png";
                ValueRating5 = "StarActive.png";
               
            }
            ValueRating = id;
        }
        private void clickqualityrating(object sender)
        {
            var view = sender as Xamarin.Forms.Image;

            string id = view.ClassId.ToString();
            if (id == "1")
            {
                QualityRating1 = "StarActive.png";
                QualityRating2 = "Star_inactive.png";
                QualityRating3 = "Star_inactive.png";
                QualityRating4 = "Star_inactive.png";
                QualityRating5 = "Star_inactive.png";
            }
            else if (id == "2")
            {
                QualityRating1 = "StarActive.png";
                QualityRating2 = "StarActive.png";
                QualityRating3 = "Star_inactive.png";
                QualityRating4 = "Star_inactive.png";
                QualityRating5 = "Star_inactive.png";
            }
            else if (id == "3")
            {
                QualityRating1 = "StarActive.png";
                QualityRating2 = "StarActive.png";
                QualityRating3 = "StarActive.png";
                QualityRating4 = "Star_inactive.png";
                QualityRating5 = "Star_inactive.png";
            }
            else if (id == "4")
            {
                QualityRating1 = "StarActive.png";
                QualityRating2 = "StarActive.png";
                QualityRating3 = "StarActive.png";
                QualityRating4 = "StarActive.png";
                QualityRating5 = "Star_inactive.png";
            }
            else if (id == "5")
            {
                QualityRating1 = "StarActive.png";
                QualityRating2 = "StarActive.png";
                QualityRating3 = "StarActive.png";
                QualityRating4 = "StarActive.png";
                QualityRating5 = "StarActive.png";
            }
            QualityRating = id;
        }
        private void clickrating(object sender)
        {
            var view = sender as Xamarin.Forms.Image;

            string id = view.ClassId.ToString();
            if (id == "1")
            {
                Rating1 = "StarActive.png";
                Rating2 = "Star_inactive.png";
                Rating3 = "Star_inactive.png";
                Rating4 = "Star_inactive.png";
                Rating5 = "Star_inactive.png";
                Ratingbar = "1/5";
                Rating = "1";
            }
            else if (id == "2")
            {
                Rating1 = "StarActive.png";
                Rating2 = "StarActive.png";
                Rating3 = "Star_inactive.png";
                Rating4 = "Star_inactive.png";
                Rating5 = "Star_inactive.png";
                Ratingbar = "2/5";
                Rating = "2";
            }
            else if (id == "3")
            {
                Rating1 = "StarActive.png";
                Rating2 = "StarActive.png";
                Rating3 = "StarActive.png";
                Rating4 = "Star_inactive.png";
                Rating5 = "Star_inactive.png";
                Ratingbar = "3/5";
                Rating = "3";
            }
            else if (id == "4")
            {
                Rating1 = "StarActive.png";
                Rating2 = "StarActive.png";
                Rating3 = "StarActive.png";
                Rating4 = "StarActive.png";
                Rating5 = "Star_inactive.png";
                Ratingbar = "4/5";
                Rating = "4";
            }
            else if (id == "5")
            {
                Rating1 = "StarActive.png";
                Rating2 = "StarActive.png";
                Rating3 = "StarActive.png";
                Rating4 = "StarActive.png";
                Rating5 = "StarActive.png";
                Ratingbar = "5/5";
                Rating = "5";
            }
        }
        public async void Getrespform()
        {
            var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
            // await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Leadform.LS_RespForm(adid, sPremiumad));
            await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Leadform.LS_Prime_detail(adid, sPremiumad,adtitleurl,cityurl));
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async void Binddetailpagedata(LS_LISTINGMODEL_DATA LMD)
        {
            try
            {
               
                var detailpagedata = RestService.For<ILS_Detailpage>(Commonsettings.TechjobsAPI);
                string userpid = Commonsettings.UserPid;
                if (userpid==""|| userpid == null)
                {
                    userpid = "0";
                }
                var data = await detailpagedata.GetDetaildata(LMD.adid, LMD.newcityurl,LMD.titleurl,userpid);
                if (data.ROW_DATA.Count > 0)
                {
                    title = data.ROW_DATA[0].title.ToString();
                    if (!string.IsNullOrEmpty(data.ROW_DATA[0].ratingavg))
                    {
                        rating = data.ROW_DATA[0].ratingavg.ToString() + " / 5";
                        Israting = true;
                    }
                    else
                    {
                        Israting = false;
                    }
                    OnPropertyChanged(nameof(Israting));
                    if (!string.IsNullOrEmpty(data.ROW_DATA[0].primarytags))
                    {
                        Primarytags = data.ROW_DATA[0].primarytags.ToString();
                    }
                    calltotwilio=data.ROW_DATA[0].calltotwilio.ToString();
                    if (!string.IsNullOrEmpty(data.ROW_DATA[0].twilionumber))
                    {
                        twilionumber= data.ROW_DATA[0].twilionumber.ToString();
                    }
                    if (!string.IsNullOrEmpty(data.ROW_DATA[0].twiliopin))
                    {
                        twiliopin = data.ROW_DATA[0].twiliopin.ToString();
                    }
                    string services = data.ROW_DATA[0].servicetypes.ToString();
                    string[] ServicesArr = services.Split(',');
                    OnPropertyChanged(nameof(Listservices));
                    OnPropertyChanged(nameof(Listservicesmore));
                    // Listservices = ServicesArr.ToList();
                    if (ServicesArr.Length>10)
                    {
                        Listservices = ServicesArr.Take(10).ToList();
                        Listservicesmore = ServicesArr.Skip(10).ToList();
                        Adcountservices = Listservicesmore.Count + " more services";
                        Admoreservices = true;


                    }
                    else
                    {
                        Listservices = ServicesArr.Take(10).ToList();
                        Servicesvisbile = false;
                        Admoreservices = false;
                    }
                    OnPropertyChanged(nameof(Adcountservices));
                    OnPropertyChanged(nameof(Servicesvisbile));
                    OnPropertyChanged(nameof(Admoreservices));
                    if (!string.IsNullOrEmpty(data.ROW_DATA[0].description))
                    {
                        Pagedescription = data.ROW_DATA[0].shortdesc;
                    }
                    if (!string.IsNullOrEmpty(data.ROW_DATA[0].contactname))
                    {
                        Contactname = data.ROW_DATA[0].contactname.ToString();
                    }
                   
                    OnPropertyChanged(nameof(Contactname));
                    if (!string.IsNullOrEmpty(data.ROW_DATA[0].toptags))
                    {
                        string toptags = data.ROW_DATA[0].toptags.ToString();
                        string[] ToptagsArr = toptags.Split(',');
                        OnPropertyChanged(nameof(Listtoptags));
                        Listtoptags = ToptagsArr.ToList();
                    }
                   

                    Isfavorite = data.ROW_DATA[0].isadsaved;
                    if (Isfavorite == "0")
                    {
                        Isfavoriteimg = "Favorite.png";
                    }
                    else
                    {
                        Isfavoriteimg = "FavoriteActive.png";
                    }
                  //  string zip = data.ROW_DATA[0].zipcode;


                    if (!string.IsNullOrEmpty(data.ROW_DATA[0].zipcode))
                    {
                        Citystatecode = data.ROW_DATA[0].streetname + ", " + data.ROW_DATA[0].zipcode;
                    }
                    else
                    {
                        Citystatecode = data.ROW_DATA[0].streetname;
                    }

                    OnPropertyChanged(nameof(Isfavoriteimg));
                    OnPropertyChanged(nameof(Citystatecode));
                    if (string.IsNullOrEmpty(rating) || rating == "0")
                    {
                        isratingavil = false;
                    }
                    else
                    {
                        isratingavil = true;
                    }
                    if (data.ROW_DATA[0].premiumad == "1" && data.ROW_DATA[0].adstatus == "0")
                    {
                        Accreditationsavail = true;
                        Accreditationsnoavail = false;
                        Isreport = false;
                        Isrecommendad = true;
                    }
                    else
                    {
                        Accreditationsavail = false;
                        Accreditationsnoavail = true;
                        Isreport = true;
                        Isrecommendad = false;
                    }
                    OnPropertyChanged(nameof(Accreditationsavail));
                    OnPropertyChanged(nameof(Accreditationsnoavail));
                    OnPropertyChanged(nameof(Isreport));
                    OnPropertyChanged(nameof(Isrecommendad));
                    if (!string.IsNullOrEmpty(data.ROW_DATA[0].adstiming.ToString()))
                    {
                        Adtimings = "Today " + data.ROW_DATA[0].adstiming.ToString();
                        Adtimingavail = true;
                        Adtimingavail1 = true;
                        Adtimingavail2 = false;
                    }
                    else
                    {
                        Adtimingavail = false;
                        Adtimingavail1 = false;
                        Adtimingavail2 = true;
                    }
                    if (data.ROW_DATA[0].calltotwilio=="1"&& !string.IsNullOrEmpty(data.ROW_DATA[0].twilionumber)&& !string.IsNullOrEmpty(data.ROW_DATA[0].twiliopin))
                    {
                        Isvirualnoavail = true;
                        Isvirualnoavailother = false;
                    }

                   else if (string.IsNullOrEmpty(data.ROW_DATA[0].virtualno))
                    {
                        Isvirualnoavail = false;
                        Isvirualnoavailother = true;
                    }
                    else
                    {
                        Isvirualnoavail = true;
                        Isvirualnoavailother = false;
                    }
                    OnPropertyChanged(nameof(Isvirualnoavail));
                    OnPropertyChanged(nameof(Isvirualnoavailother));
                    if (!string.IsNullOrEmpty(data.ROW_DATA[0].lat))
                    {
                        adlat = data.ROW_DATA[0].lat.ToString();
                    }
                    if (!string.IsNullOrEmpty(data.ROW_DATA[0].longitude))
                    {
                        adlong = data.ROW_DATA[0].longitude.ToString();
                    }
                   
                    //adcity = data.ROW_DATA[0].city.ToString();

                    if (data.ROW_DATA[0].adavailability.ToString() == "0")
                    {
                        AdCloseoropen = "Closed now";
                        Closeoropencolor = "Red";
                        AdCloseoropenother = true;
                    }
                    else if (data.ROW_DATA[0].adavailability.ToString() == "1")
                    {
                        AdCloseoropen = "Open now";
                        Closeoropencolor ="Green";
                        AdCloseoropenother = true;
                    }
                    if (string.IsNullOrEmpty(data.ROW_DATA[0].adavailability))
                    {
                        Adtimingavail = true;
                        Adtimingavailother = true;
                        Adtimingsother = data.ROW_DATA[0].timingstatus.ToString();
                        AdCloseoropenother = false;
                    }
                    else
                    {
                        Adtimingavail = false;
                    }
                    if (string.IsNullOrEmpty(data.ROW_DATA[0].adavailability))
                    {
                        Adtimingavail = false;
                    }
                        OnPropertyChanged(nameof(Adtimingavail1));
                    OnPropertyChanged(nameof(Adtimingavail));
                    OnPropertyChanged(nameof(Adtimingavail2));
                    OnPropertyChanged(nameof(Adtimingsother));
                    OnPropertyChanged(nameof(Adtimingavailother));
                    OnPropertyChanged(nameof(AdCloseoropenother));
                    OnPropertyChanged(nameof(Isvirualnoavail));
                    if (data.ROW_DATA[0].Isrecomended=="0")
                    {
                        Recommendimg = "Recommend.png";
                    }
                    else
                    {
                        Recommendimg = "RecommendActive.png";
                    }
                    OnPropertyChanged(nameof(AdCloseoropen));
                    OnPropertyChanged(nameof(Closeoropencolor));
                    OnPropertyChanged(nameof(Adtimings));
                    OnPropertyChanged(nameof(rating));
                    OnPropertyChanged(nameof(title));
                    OnPropertyChanged(nameof(Primarytags));
                    OnPropertyChanged(nameof(Pagedescription));
                    OnPropertyChanged(nameof(Recommendimg));

                   
                }
                await Task.Delay(2000);
                _Dialog.HideLoading();
            }
            catch (Exception e)
            {
                _Dialog.HideLoading();
            }

        }

        public async void Bindhighlights(string adid, string ltype)
        {
            try
            {
                _Dialog.ShowLoading("", null);
                var ltypedata = RestService.For<ILS_Detailpage>(Commonsettings.TechjobsAPI);
                var data = await ltypedata.Getltypedata(adid, ltype);
               
                if (data.ROW_DATA.Count > 0)
                {
                    if (ltype == "4")
                    {
                        OnPropertyChanged(nameof(Listltype4));
                      //  Listltype4 = data.ROW_DATA;

                        if (data.ROW_DATA.Count > 10)
                        {
                            Listltype4 = data.ROW_DATA.Take(10).ToList();
                            Listltype4more = data.ROW_DATA.Skip(10).ToList();
                            Adcountotherservices = Listltype4more.Count + " more highlights";
                            Admoreotherservices = true;
                            OnPropertyChanged(nameof(Adcountotherservices));
                        }
                        else
                        {
                            Listltype4 = data.ROW_DATA;
                            Admoreotherservices = false;
                        }
                    }
                    if (ltype == "5")
                    {
                        OnPropertyChanged(nameof(Listltype5));
                        OnPropertyChanged(nameof(Listltype5more));
                        if (data.ROW_DATA.Count>10)
                        {
                            Listltype5 = data.ROW_DATA.Take(10).ToList();
                            Listltype5more = data.ROW_DATA.Skip(10).ToList();
                            Adcounthighlits = Listltype5more.Count + " more highlights";
                            Admorehighlights = true;
                            OnPropertyChanged(nameof(Adcounthighlits));
                        }
                        else
                        {
                            Listltype5 = data.ROW_DATA;
                            Admorehighlights = false;
                        }
                        OnPropertyChanged(nameof(Admorehighlights));
                    }

                }
                else
                {
                    if (ltype == "4")
                    {
                        OnPropertyChanged(nameof(Listltype4visible));
                        Listltype4visible = false;
                    }
                    if (ltype == "5")
                    {
                        OnPropertyChanged(nameof(Listltype5visible));
                        Listltype5visible = false;
                    }
                }
            }
            catch (Exception e)
            {
                _Dialog.HideLoading();
            }
        }

        public async void Bindcities(string adid)
        {
            try
            {

               
                var adcities = RestService.For<ILS_Detailpage>(Commonsettings.TechjobsAPI);
                var data = await adcities.Getadcities(adid);



                if (data.ROW_DATA.Count > 0)
                {
                    OnPropertyChanged(nameof(Listadcities1));
                    OnPropertyChanged(nameof(Listadcities2));
                    Listadcities1 = data.ROW_DATA.Take(10).ToList();
                    Listadcities2 = data.ROW_DATA.Skip(10).ToList();
                    if (data.ROW_DATA.Count > 10)
                    {
                        Admorecities = true;
                        Adcountcities = Listadcities2.Count.ToString() + " more cities";
                        Citiserved2 = false;
                    }
                    else { Admorecities = false;
                        
                    }
                    OnPropertyChanged(nameof(Admorecities));
                    OnPropertyChanged(nameof(Adcountcities));
                    OnPropertyChanged(nameof(Citiserved2));
                }
              
            }
            catch (Exception e)
            {
                _Dialog.HideLoading();
            }
        }

        public void Openmorecities()
        {
            OnPropertyChanged(nameof(Citiserved2));
          
            if (Citiserved2 == true)
            {
                Citiserved2 = false;
                MorecitiIcon = "plusIcon.png";
                Admorecities = false;
                Adcountcities = Listadcities2.Count.ToString() + " more cities";
            }
            else
            {
                Citiserved2 = true;
                MorecitiIcon = "minus.png";
                Admorecities = true;
                Adcountcities = Listadcities2.Count.ToString() + " less cities";
            }
            OnPropertyChanged(nameof(MorecitiIcon));
            OnPropertyChanged(nameof(Adcountcities));


        }

        public async void BindVideos(string adid)
        {
            try
            {
                
                var adcities = RestService.For<ILS_Detailpage>(Commonsettings.TechjobsAPI);
                var data = await adcities.GetVideos(adid);
                if (data.ROW_DATA.Count > 0)
                {
                    OnPropertyChanged(nameof(ListAdVideos));
                    ListAdVideos = data.ROW_DATA;
                    IsVideoAvail = true;
                }
                else { IsVideoAvail = false; }
                OnPropertyChanged(nameof(IsVideoAvail));
              
            }
            catch (Exception e)
            {
                _Dialog.HideLoading();
            }
        }

        public async void BindPhotos(string adid)
        {
            try
            {
               
                var adphotos = RestService.For<ILS_Detailpage>(Commonsettings.TechjobsAPI);
                var data = await adphotos.GetPhotos(adid);
                if (data.ROW_DATA.Count > 0)
                {
                    OnPropertyChanged(nameof(ListAdPhotos));
                    ListAdPhotos = data.ROW_DATA.Take(5).ToList();
                    int count = data.ROW_DATA.Count - 5;
                    Remainphots = "+ " + Convert.ToString(count);
                    IsPhotoAvail = true;
                    OnPropertyChanged(nameof(Remainphots));
                }
                else { IsPhotoAvail = false; }
                OnPropertyChanged(nameof(IsPhotoAvail));
               // _Dialog.HideLoading();
            }
            catch (Exception e)
            {
                _Dialog.HideLoading();
            }
        }

        public async void Bindonlineclasses(string adid)
        {
            try
            {

                var classes = RestService.For<ILS_Detailpage>(Commonsettings.Localservices);
                var data = await classes.GetOnlineclasses(adid);
                if (data.ROW_DATA.Count > 0)
                {
                    IsClasssesAvail = true;
                    foreach (var item in data.ROW_DATA)
                    {
                        if (!string.IsNullOrEmpty(item.batchdate)){
                            item.isbatchavail = true;
                        }
                        else
                        {
                            item.isbatchavail = false;
                        }
                        item.avgage = item.minage + "-" + item.maxage;

                        item.sizeallowtxt= "Avg.class size "+item.sizeallowed+" students";
                    }

                    Listonlineclass = data.ROW_DATA;
                    OnPropertyChanged(nameof(Listonlineclass));
                }
                else { IsClasssesAvail = false; }
                OnPropertyChanged(nameof(IsPhotoAvail));
                // _Dialog.HideLoading();
            }
            catch (Exception e)
            {
                _Dialog.HideLoading();
            }

        }

        public void Clickvideo(LS_AD_VIDEOS_DATA LV)
        {
            Device.OpenUri(new Uri(LV.videourl));
        }

        public async void Clickphoto(LS_AD_PHOTOS_DATA LP)
        {
            var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
            await currentpage.Navigation.PushModalAsync(new NRIApp.LocalService.Features.Views.Detail.LS_Detail_Image(LP.adid));
        }
        public void Clicktoopenmap()
        {
            string uri = "http://maps.google.com/?saddr=" + Commonsettings.UserLat + "," + Commonsettings.UserLong + "&daddr=" + adlat + "," + adlong;
            Device.OpenUri(new Uri(uri));
        }
        List<LS_AD_GROUP_SERVICES> listgrpdata = new List<LS_AD_GROUP_SERVICES>();

        private ObservableCollection<GroupServices> items;
        public ObservableCollection<GroupServices> Items
        {
            get { return items; }
            set { items = value; }
        }
       
        public Command LoadHotelsCommand
        {
            get;
            set;
        }
        public Command<GroupServices> RefreshItemsCommand
        {
            get;
            set;
        }

        private bool _Serviceavail = false;

        public bool Serviceavail
        {
            get { return _Serviceavail; }
            set { _Serviceavail = value; }
        }

        private GroupServices _oldHotel;
        public static string Listviewheight { get; set; }
        public async System.Threading.Tasks.Task BindAdServices()
        {
            //string adid = LMD.adid;
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                _Dialog.ShowLoading("", null);
                Items.Clear();
                //if ( listgrpdata.Count == 0)
                //{               
                var adservices = RestService.For<ILS_Detailpage>(Commonsettings.TechjobsAPI);
                var data = await adservices.GetAdServices(adid);
                if (data.ROW_DATA.Count>0)
                {

              
                Bizservices = data.ROW_DATA;
                var LS = Bizservices.GroupBy(m => m.mainmenu);
                var LSg = Bizservices.GroupBy(m => m.mainmenu).ToList();
                LS_AD_GROUP_SERVICES list;
                foreach (var item in LSg)
                {
                    list = new LS_AD_GROUP_SERVICES()
                    {
                        Heading = item.Key,
                        Services = item.ToList()

                    };
                    listgrpdata?.Add(list);




                 }
                //}
                if (listgrpdata != null && listgrpdata.Count > 0)
                {
                    Serviceavail = true;
                    OnPropertyChanged(nameof(Serviceavail));
                    Height = (listgrpdata.Count * 40) + (listgrpdata.Count * 10); ;
                    OnPropertyChanged(nameof(Height));

                    foreach (var h in listgrpdata)
                    {

                        Items.Add(new GroupServices(h));
                    }
                    
                }
                }
               
            }
            catch (Exception e)
            {
                IsBusy = false;
                _Dialog.HideLoading();
            }
            finally
            {
                IsBusy = false;
            }
        }
       
        public bool isExpanded = false;
        private void ExecuteRefreshItemsCommand(GroupServices HV)
        {
            if (_oldHotel == HV)
            {
                // click twice on the same item will hide it  
                HV.Expanded = !HV.Expanded;
                if (!HV.Expanded)
                {
                    Height = (listgrpdata.Count * 40) + (listgrpdata.Count * 10);
                    OnPropertyChanged(nameof(Height));
                }
                else
                {
                    Height = ((HV.Count + listgrpdata.Count) * 40) + ((HV.Count + listgrpdata.Count) * 20);
                    OnPropertyChanged(nameof(Height));
                }
               
            }
            else
            {
                if (_oldHotel != null)
                {
                    // hide previous selected item  
                    _oldHotel.Expanded = false;
                    Height = (listgrpdata.Count * 40) + (listgrpdata.Count * 10); ;
                    OnPropertyChanged(nameof(Height));
                }
                // show selected item  
                HV.Expanded = true;
                Height = ((HV.Count + listgrpdata.Count) * 40) + ((HV.Count + listgrpdata.Count) * 20);
                OnPropertyChanged(nameof(Height));
             
            }
            _oldHotel = HV;
            
        }
        public void Opendetails(LS_AD_SERVICES_DATA list)
        {

            var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
            HVScrollGridView myEntry = currentpage.FindByName<HVScrollGridView>("listbizdata");
            foreach (var p in Bizservices)
            {
                if (p.menucardid != list.menucardid)
                {
                    p.image = "plus.png";
                    p.detailblkvisible = false;
                }

                else
                {
                    p.image = "minus.png";
                    p.detailblkvisible = true;

                }

            }

            //if (item != null)
            //{
            //    item.image = "minus.png";
            //    item.detailblkvisible = true;
            //    bizmodlist3 = bizmodlist2;
            //    myEntry.ItemsSource = null;
            //    myEntry.ItemsSource = bizmodlist2;

            //}
            myEntry.ItemsSource = null;
            myEntry.ItemsSource = Bizservices;
        }
        public class Grouping<K, T> : ObservableCollection<T>
        {
            public K Key { get; private set; }

            public Grouping(K key, IEnumerable<T> items)
            {
                Key = key;
                foreach (var item in items)
                    this.Items.Add(item);
            }
        }
    }
}
