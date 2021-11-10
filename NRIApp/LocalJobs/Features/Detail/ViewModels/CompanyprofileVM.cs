using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using Xamarin.Forms;
using System.Linq;
using Refit;
using Newtonsoft.Json.Linq;
using NRIApp.LocalJobs.Features.Detail.Interfaces;
using NRIApp.Helpers;
using Plugin.Share;
using NRIApp.LocalJobs.Features.Detail.Models;
using Plugin.MapsPlugin;
using NRIApp.LocalJobs.Features.Listing.Models;
using Acr.UserDialogs;
using System.Text.RegularExpressions;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using System.Windows.Input;
using Xamarin.Forms.Extended;
using NRIApp.LocalJobs.Features.Detail.Views;

namespace NRIApp.LocalJobs.Features.Detail.ViewModels
{
    public class CompanyprofileVM : INotifyPropertyChanged
    {
        IUserDialogs dialogs = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
      //  string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        public InfiniteScrollCollection<Businessjobopeningdata> jobslists { get; set; }
        private List<Businessbenifitlist> _getbusinessbenifit;
        public List<Businessbenifitlist> getbusinessbenifit
        {
            get { return _getbusinessbenifit; }
            set { _getbusinessbenifit = value; OnPropertyChanged(nameof(getbusinessbenifit)); }
        }
        private List<Salarylistdata> _getsalarydata;
        public List<Salarylistdata> getsalarydata
        {
            get { return _getsalarydata; }
            set { _getsalarydata = value; OnPropertyChanged(nameof(getsalarydata)); }
        }
        private List<Rating_Details> _getreviewdetail;
        public List<Rating_Details> getreviewdetail
        {
            get { return _getreviewdetail; }
            set { _getreviewdetail = value; OnPropertyChanged(nameof(getreviewdetail)); }
        }
        public Command Taptoshare { get; set; }
        public Command Tapfollow { get; set; }
        public Command Tapmap { get; set; }
        public Command managecompanytap { get; set; }
        public Command publishreview { get; set; }
        public ICommand Customerclickratingcommand { get; set; }
        public ICommand Careerclickratingcommand { get; set; }
        public ICommand Workclickratingcommand { get; set; }
        public ICommand Managementclickratingcommand { get; set; }
        public Command<Businessjobopeningdata> DetailjobsCommandJO { get; set; }
        public Command<LocalJobResponse> DetailjobsCommand { get; set; }
        public ICommand applyforjobopeninglist { get; set; }
        public ICommand applyforcompanyjobopeninglist { get; set; }

        public ICommand clickjobalert { get; set; }
        public ICommand clickjobprofile { get; set; }
        public ICommand clickuploadresume { get; set; }

        public Command companyoverviewTap { get; set; }
        public Command jobopeningTap { get; set; }
        public Command salaryTap { get; set; }
        public Command sharereviewcmd { get; set; }
        public ICommand Morereviews { get; set; }
        public ICommand Morejobopenings { get; set; }
        public ICommand SMorejobopenings { get; set; }
        public Command<Rating_Details> commentviewcmd { get; set; }
        public List<Reviewreplydetaildata> Reviewreply { get; set; }
        public Command Moresalarylistings { get; set; }

        public Command Tapfacebook { get; set; }
        public Command Taptwitter { get; set; }
        public Command Taplinkedin { get; set; }

        public Command Replysubmit { get; set; }

        private bool _companydescvisible = false;
        public bool companydescvisible
        {
            get { return _companydescvisible; }
            set { _companydescvisible = value; OnPropertyChanged(nameof(companydescvisible)); }
        }
        private bool _jobopeningvisible = false;
        public bool jobopeningvisible
        {
            get { return _jobopeningvisible; }
            set { _jobopeningvisible = value; OnPropertyChanged(nameof(jobopeningvisible)); }
        }
        private bool _empsalaryvisible = false;
        public bool empsalaryvisible
        {
            get { return _empsalaryvisible; }
            set { _empsalaryvisible = value; OnPropertyChanged(nameof(empsalaryvisible)); }
        }
        private bool _benefitsvisible = false;
        public bool benefitsvisible
        {
            get { return _benefitsvisible; }
            set { _benefitsvisible = value; OnPropertyChanged(nameof(benefitsvisible)); }
        }
        private string _companydesctxtclr;
        public string companydesctxtclr
        {
            get { return _companydesctxtclr; }
            set { _companydesctxtclr = value; OnPropertyChanged(nameof(companydesctxtclr)); }
        }
        private string _companydescboxclr;
        public string companydescboxclr
        {
            get { return _companydescboxclr; }
            set { _companydescboxclr = value; OnPropertyChanged(nameof(companydescboxclr)); }
        }
        private string _jobopeningtxtclr= "#212121";
        public string jobopeningtxtclr
        {
            get { return _jobopeningtxtclr; }
            set { _jobopeningtxtclr = value; OnPropertyChanged(nameof(jobopeningtxtclr)); }
        }
        private string _jobopeningboxclr= "#ffffff";
        public string jobopeningboxclr
        {
            get { return _jobopeningboxclr; }
            set { _jobopeningboxclr = value; OnPropertyChanged(nameof(jobopeningboxclr)); }
        }
        private string _salarytxtclr= "#212121";
        public string salarytxtclr
        {
            get { return _salarytxtclr; }
            set { _salarytxtclr = value; OnPropertyChanged(nameof(salarytxtclr)); }
        }
        private string _salaryboxclr= "#ffffff";
        public string salaryboxclr
        {
            get { return _salaryboxclr; }
            set { _salaryboxclr = value; OnPropertyChanged(nameof(salaryboxclr)); }
        }
        private string _title;
        public string title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(nameof(title)); }
        }
        private string _address;
        public string address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(nameof(address)); }
        }
        private string _founded;
        public string founded
        {
            get { return _founded; }
            set { _founded = value; OnPropertyChanged(nameof(founded)); }
        }
        private string _companysize;
        public string companysize
        {
            get { return _companysize; }
            set { _companysize = value; OnPropertyChanged(nameof(companysize)); }
        }
        private string _industry;
        public string industry
        {
            get { return _industry; }
            set { _industry = value; OnPropertyChanged(nameof(industry)); }
        }
        private string _businesstype;
        public string businesstype
        {
            get { return _businesstype; }
            set { _businesstype = value; OnPropertyChanged(nameof(businesstype)); }
        }
        private string _verifytxt;
        public string verifytxt
        {
            get { return _verifytxt; }
            set { _verifytxt = value; OnPropertyChanged(nameof(verifytxt)); }
        }
        private bool _verifyvisible=false;
        public bool verifyvisible
        {
            get { return _verifyvisible; }
            set { _verifyvisible = value; OnPropertyChanged(nameof(verifyvisible)); }
        }
        private string _ratingbarvalue;
        public string ratingbarvalue
        {
            get { return _ratingbarvalue; }
            set { _ratingbarvalue = value; OnPropertyChanged(nameof(ratingbarvalue)); }
        }
        private string _cityurl;
        public string cityurl
        {
            get { return _cityurl; }
            set { _cityurl = value;OnPropertyChanged(nameof(cityurl)); }
        }
        private string _businessid;
        public string businessid
        {
            get { return _businessid; }
            set { _businessid = value; OnPropertyChanged(nameof(businessid)); }
        }
        private string _followflag;
        public string followflag
        {
            get { return _followflag; }
            set { _followflag = value; OnPropertyChanged(nameof(followflag)); }
        }
        private string _newcityurl;
        public string newcityurl
        {
            get { return _newcityurl; }
            set { _newcityurl = value; OnPropertyChanged(nameof(newcityurl)); }
        }
        private string _shortdesc;
        public string shortdesc
        {
            get { return _shortdesc; }
            set { _shortdesc = value; OnPropertyChanged(nameof(shortdesc)); }
        }
        private string _reviewdesc;
        public string reviewdesc
        {
            get { return _reviewdesc; }
            set { _reviewdesc = value; OnPropertyChanged(nameof(reviewdesc)); }
        }
        private string _careerrating;
        public string careerrating
        {
            get { return _careerrating; }
            set { _careerrating = value; OnPropertyChanged(nameof(careerrating)); }
        }
        private string _workrating;
        public string workrating
        {
            get { return _workrating; }
            set { _workrating = value; OnPropertyChanged(nameof(workrating)); }
        }
        private string _managementrating;
        public string managementrating
        {
            get { return _managementrating; }
            set { _managementrating = value; OnPropertyChanged(nameof(managementrating)); }
        }
        private string _rating;
        public string rating
        {
            get { return _rating; }
            set { _rating = value; OnPropertyChanged(nameof(rating)); }
        }
        private bool _reviewvisible;
        public bool reviewvisible
        {
            get { return _reviewvisible; }
            set { _reviewvisible = value; OnPropertyChanged(nameof(reviewvisible)); }
        }
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
        public string _Grating1 = "Star_inactiveWt.png";
        public string Grating1
        {
            get { return _Grating1; }
            set
            {
                _Grating1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Grating1)));
            }
        }
        public string _Grating2 = "Star_inactiveWt.png";
        public string Grating2
        {
            get { return _Grating2; }
            set
            {
                _Grating2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Grating2)));
            }
        }
        public string _Grating3 = "Star_inactiveWt.png";
        public string Grating3
        {
            get { return _Grating3; }
            set
            {
                _Grating3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Grating3)));
            }
        }
        public string _Grating4 = "Star_inactiveWt.png";
        public string Grating4
        {
            get { return _Grating4; }
            set
            {
                _Grating4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Grating4)));
            }
        }
        public string _Grating5 = "Star_inactiveWt.png";
        public string Grating5
        {
            get { return _Grating5; }
            set
            {
                _Grating5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Grating5)));
            }
        }
        //careerrating
        public string _Careerrating1 = "Star_inactive.png";
        public string Careerrating1
        {
            get { return _Careerrating1; }
            set
            {
                _Careerrating1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Careerrating1)));

            }
        }
        public string _Careerrating2 = "Star_inactive.png";
        public string Careerrating2
        {
            get { return _Careerrating2; }
            set
            {
                _Careerrating2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Careerrating2)));

            }
        }
        public string _Careerrating3 = "Star_inactive.png";
        public string Careerrating3
        {
            get { return _Careerrating3; }
            set
            {
                _Careerrating3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Careerrating3)));

            }
        }
        public string _Careerrating4 = "Star_inactive.png";
        public string Careerrating4
        {
            get { return _Careerrating4; }
            set
            {
                _Careerrating4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Careerrating4)));

            }
        }
        public string _Careerrating5 = "Star_inactive.png";
        public string Careerrating5
        {
            get { return _Careerrating5; }
            set
            {
                _Careerrating5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Careerrating5)));

            }
        }
        //workrating
        public string _workrating1 = "Star_inactive.png";
        public string workrating1
        {
            get { return _workrating1; }
            set
            {
                _workrating1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(workrating1)));

            }
        }
        public string _workrating2 = "Star_inactive.png";
        public string workrating2
        {
            get { return _workrating2; }
            set
            {
                _workrating2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(workrating2)));

            }
        }
        public string _workrating3 = "Star_inactive.png";
        public string workrating3
        {
            get { return _workrating3; }
            set
            {
                _workrating3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(workrating3)));

            }
        }
        public string _workrating4 = "Star_inactive.png";
        public string workrating4
        {
            get { return _workrating4; }
            set
            {
                _workrating4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(workrating4)));

            }
        }
        public string _workrating5 = "Star_inactive.png";
        public string workrating5
        {
            get { return _workrating5; }
            set
            {
                _workrating5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(workrating5)));

            }
        }
        //managementrating
        public string _managementrating1 = "Star_inactive.png";
        public string managementrating1
        {
            get { return _managementrating1; }
            set
            {
                _managementrating1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(managementrating1)));

            }
        }
        public string _managementrating2 = "Star_inactive.png";
        public string managementrating2
        {
            get { return _managementrating2; }
            set
            {
                _managementrating2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(managementrating2)));

            }
        }
        public string _managementrating3 = "Star_inactive.png";
        public string managementrating3
        {
            get { return _managementrating3; }
            set
            {
                _managementrating3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(managementrating3)));

            }
        }
        public string _managementrating4 = "Star_inactive.png";
        public string managementrating4
        {
            get { return _managementrating4; }
            set
            {
                _managementrating4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(managementrating4)));

            }
        }
        public string _managementrating5 = "Star_inactive.png";
        public string managementrating5
        {
            get { return _managementrating5; }
            set
            {
                _managementrating5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(managementrating5)));

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
        private string _followflagimg = "Reportflag.png";
        public string followflagimg
        {
            get { return _followflagimg; }
            set { _followflagimg = value;OnPropertyChanged(nameof(followflagimg)); }
        }
        private string _businessurl = "";
        public string businessurl
        {
            get { return _businessurl; }
            set { _businessurl = value;OnPropertyChanged(nameof(businessurl)); }
        }
        private bool _IsBusy = false;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { _IsBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }
        private bool _jovisible = false;
        public bool jovisible
        {
            get { return _jovisible; }
            set { _jovisible = value; OnPropertyChanged(nameof(jovisible)); }
        }
        private bool _covvisible = false;
        public bool covvisible
        {
            get { return _covvisible; }
            set { _covvisible = value; OnPropertyChanged(nameof(covvisible)); }
        }
        
        private bool _salaryheadvisible = false;
        public bool salaryheadvisible
        {
            get { return _salaryheadvisible; }
            set { _salaryheadvisible = value; OnPropertyChanged(nameof(salaryheadvisible)); }
        }
        private string _replycomment = "";
        public string replycomment
        {
            get { return _replycomment; }
            set { _replycomment = value; OnPropertyChanged(nameof(replycomment)); }
        }
        private string _thumbimgurl = "";
        public string thumbimgurl
        {
            get { return _thumbimgurl; }
            set { _thumbimgurl = value; OnPropertyChanged(nameof(thumbimgurl)); }
        }
        private string _aboutcompanyoverviewlbl = "";
        public string aboutcompanyoverviewlbl
        {
            get { return _aboutcompanyoverviewlbl; }
            set { _aboutcompanyoverviewlbl = value; OnPropertyChanged(nameof(aboutcompanyoverviewlbl)); }
        }
        private string _aboutjobopeninglbl = "";
        public string aboutjobopeninglbl
        {
            get { return _aboutjobopeninglbl; }
            set { _aboutjobopeninglbl = value; OnPropertyChanged(nameof(aboutjobopeninglbl)); }
        }
        private string _aboutbenefitslbl = "";
        public string aboutbenefitslbl
        {
            get { return _aboutbenefitslbl; }
            set { _aboutbenefitslbl = value; OnPropertyChanged(nameof(aboutbenefitslbl)); }
        }
        private string _aboutsalarylbl = "";
        public string aboutsalarylbl
        {
            get { return _aboutsalarylbl; }
            set { _aboutsalarylbl = value; OnPropertyChanged(nameof(aboutsalarylbl)); }
        }
        private bool _reviewcommentsvisible;
        public bool reviewcommentsvisible
        {
            get { return _reviewcommentsvisible; }
            set { _reviewcommentsvisible = value; OnPropertyChanged(nameof(reviewcommentsvisible)); }
        }
        private bool _foundedvisible=false;
        public bool foundedvisible
        {
            get { return _foundedvisible; }
            set { _foundedvisible = value; OnPropertyChanged(nameof(foundedvisible)); }
        }
        private bool _nooempvisible=false;
        public bool nooempvisible
        {
            get { return _nooempvisible; }
            set { _nooempvisible = value; OnPropertyChanged(nameof(nooempvisible)); }
        }
        private bool _industryvisible=false;
        public bool industryvisible
        {
            get { return _industryvisible; }
            set { _industryvisible = value; OnPropertyChanged(nameof(industryvisible)); }
        }
        private bool _businesstypevisble=false;
        public bool businesstypevisble
        {
            get { return _businesstypevisble; }
            set { _businesstypevisble = value; OnPropertyChanged(nameof(businesstypevisble)); }
        }
        private string _Revimage = "";
        public string Revimage
        {
            get { return _Revimage; }
            set { _Revimage = value; }
        }

        private bool _Revimgvisible = false;
        public bool Revimgvisible
        {
            get { return _Revimgvisible; }
            set { _Revimgvisible = value; }
        }

        private bool _Revimgnovisible = false;
        public bool Revimgnovisible
        {
            get { return _Revimgnovisible; }
            set { _Revimgnovisible = value; }
        }

        private string _Revcontributor = "";
        public string Revcontributor
        {
            get { return _Revcontributor; }
            set { _Revcontributor = value; }
        }
        private string _Revpostedago = "";
        public string Revpostedago
        {
            get { return _Revpostedago; }
            set { _Revpostedago = value; }
        }
        private string _Revrating = "";
        public string Revrating
        {
            get { return _Revrating; }
            set { _Revrating = value; }
        }
        private string _Revdesc = "";
        public string Revdesc
        {
            get { return _Revdesc; }
            set { _Revdesc = value; }
        }
        private string _Loginname = "";
        public string Loginname
        {
            get { return _Loginname; }
            set { _Loginname = value; }
        }
        private string _Revtext = "";
        public string Revtext
        {
            get { return _Revtext; }
            set { _Revtext = value; }
        }
        private string _Revlogintext = "";
        public string Revlogintext
        {
            get { return _Revlogintext; }
            set { _Revlogintext = value; }
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
        private bool _Salarymoreblk = false;
        public bool Salarymoreblk
        {
            get { return _Salarymoreblk; }
            set
            {
                _Salarymoreblk = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Salarymoreblk)));
            }
        }
        
        private bool _Jobopeningmoreblk = false;
        public bool Jobopeningmoreblk
        {
            get { return _Jobopeningmoreblk; }
            set
            {
                _Jobopeningmoreblk = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Jobopeningmoreblk)));
            }
        }
        private bool _thumbimgvisible = false;
        public bool thumbimgvisible
        {
            get { return _thumbimgvisible; }
            set
            {
                _thumbimgvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(thumbimgvisible)));
            }
        }
    
        private bool _socialmediavisible = false;
        public bool socialmediavisible
        {
            get { return _socialmediavisible; }
            set { _socialmediavisible = value; OnPropertyChanged(nameof(socialmediavisible)); }
        }
        private bool _fbvisible = false;
        public bool fbvisible
        {
            get { return _fbvisible; }
            set { _fbvisible = value; OnPropertyChanged(nameof(fbvisible)); }
        }
        private bool _twittervisible = false;
        public bool twittervisible
        {
            get { return _twittervisible; }
            set { _twittervisible = value; OnPropertyChanged(nameof(twittervisible)); }
        }
        private bool _linkedinvisible = false;
        public bool linkedinvisible
        {
            get { return _linkedinvisible; }
            set { _linkedinvisible = value; OnPropertyChanged(nameof(linkedinvisible)); }
        }
        private bool _applyjobvisible = false;
        public bool applyjobvisible
        {
            get { return _applyjobvisible; }
            set { _applyjobvisible = value; OnPropertyChanged(nameof(applyjobvisible)); }
        }
       
        private string _jobopeningcnttxt = "";
        public string jobopeningcnttxt
        {
            get { return _jobopeningcnttxt; }
            set { _jobopeningcnttxt = value; OnPropertyChanged(nameof(jobopeningcnttxt)); }
        }

        public CompanyprofileVM(string cdata,string type)
        {
            try
            {
                jobslists = new InfiniteScrollCollection<Businessjobopeningdata>
                {
                    OnLoadMore = async () =>
                    {
                        var list = new List<Businessjobopeningdata>();
                        if (jobslists.Last().totalrecs != jobslists.Count)
                        {
                            try
                            {
                                var currentpage = GetCurrentpage();
                                ActivityIndicator jobloader = currentpage.FindByName<ActivityIndicator>("listingloader");
                                jobloader.IsRunning = true;
                                jobloader.IsVisible = true;
                                jopageno = jopageno + 1;
                                
                                IsBusy = true;
                                var jobopeningapi = RestService.For<IComapanydata>(Commonsettings.LocaljobsAPI);
                                var jobopeningdata = await jobopeningapi.jobopeninglist(cdata, jopageno, 10);
                                if (jobopeningdata.ROW_DATA.Count > 0)
                                {
                                    jobopeningvisible = true;
                                    foreach (var data in jobopeningdata.ROW_DATA)
                                    {
                                        if ((string.IsNullOrEmpty(data.experiencefrom) || data.experiencefrom == "0") && (string.IsNullOrEmpty(data.experienceto) || data.experienceto == "0"))
                                        {
                                            data.experience = "Freshers";
                                        }
                                        else
                                        {
                                            data.experience = data.experiencefrom + " - " + data.experienceto + " Years";
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
                                jobloader.IsRunning = false;
                                jobloader.IsVisible = false;
                                list = jobopeningdata.ROW_DATA;
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
                Jobopeninglist(cdata);
                DetailjobsCommandJO = new Command<Businessjobopeningdata>(gotodetailpageJO);
                applyforjobopeninglist = new Command<Businessjobopeningdata>(applyforjob);
            }
            catch(Exception ex)
            {
                
            }
        }
       
        public int jopageno;
        public async void Jobopeninglist(string titleurl)
        {
            try
            {
                dialogs.ShowLoading("", null);
                
                var jobopeningapi = RestService.For<IComapanydata>(Commonsettings.LocaljobsAPI);
                jopageno = 1;
                var jobopeningdata = await jobopeningapi.jobopeninglist(titleurl,jopageno,20);
                if (jobopeningdata.ROW_DATA.Last().totalrecs > 0 && jobopeningdata != null)
                {
                    jovisible = true;
                    jobopeningvisible = true;
                    foreach (var data in jobopeningdata.ROW_DATA)
                    {
                        if ((string.IsNullOrEmpty(data.experiencefrom) || data.experiencefrom == "0") && (string.IsNullOrEmpty(data.experienceto) || data.experienceto == "0"))
                        {
                            data.experience = "Freshers";
                        }
                        else
                        {
                            data.experience = data.experiencefrom + " - " + data.experienceto + " Years";
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
                    var currentpage = GetCurrentpage();
                    ListView JobssListviewlst = currentpage.FindByName<ListView>("JobopeningListview");
                    JobssListviewlst.ItemsSource = jobslists;
                    OnPropertyChanged(nameof(jobslists));
                    jobslists?.AddRange(jobopeningdata.ROW_DATA);
                }
                else
                {
                    jobopeningvisible = false;
                    jovisible = false;
                }
                dialogs.HideLoading();
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
                string msg = ex.Message;
            }
        }
        //when u click from search 
        public string twitterlink = "";
        public string facebooklink = "";
        public string linkedinlink = "";
        public CompanyprofileVM(Search cdata)
        {
           try
            {
               
                businessurl = cdata.titleurl;
                businessid = cdata.contentid.ToString();
                getcompanyprofile(businessurl);
                
                Taptoshare = new Command(Sharecompanyprofile);
                sharereviewcmd = new Command(Sharecompanyprofile);
                Tapfollow = new Command(taptofollow);
                Tapmap = new Command(TapOnMapView);

                managecompanytap = new Command(async () => await taponmanagecompany(cdata));
                publishreview = new Command(publishreviewdetails);
                Customerclickratingcommand = new Command(clickoverallrating);
                Careerclickratingcommand = new Command(clickcareerrating);
                Workclickratingcommand = new Command(clickworkrating);
                Managementclickratingcommand = new Command(clickmanagementrating);
                companyoverviewTap = new Command(companytap);
                jobopeningTap = new Command(joblisttap);
                salaryTap = new Command(salarylisttap);
                commentviewcmd = new Command<Rating_Details>(addreply);
               
                Moresalarylistings = new Command(getmoresalarylist);
                Morejobopenings = new Command(async () => await ClickMorejobopenings(businessurl));
                
                DetailjobsCommand = new Command<LocalJobResponse>(gotodetailpage);
                DetailjobsCommandJO = new Command<Businessjobopeningdata>(gotodetailpageJO);
                applyforcompanyjobopeninglist = new Command<Businessjobopeningdata>(applyforjob);

                clickjobalert = new Command(gotojobalert);
                clickjobprofile = new Command(gotojobseekerprofile);
                clickuploadresume = new Command(gotouploadresume);

                Taptwitter = new Command(gototwitterlink);
                Tapfacebook = new Command(gotofacebooklink);
                Taplinkedin = new Command(gotolinkedinlink);
            

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                dialogs.HideLoading();
            }
        }
        public void gotouploadresume()
        {
            var currentpage = GetCurrentpage();
            currentpage.Navigation.PushAsync(new Features.Detail.Views.UploadResume());
        }
        public void gotofacebooklink()
        {
            try
            {
                Device.OpenUri(new Uri(facebooklink));
            }
            catch(Exception ex)
            {

            }
        }
        public void gototwitterlink()
        {
            try
            {
                Device.OpenUri(new Uri(twitterlink));
            }
            catch (Exception ex)
            {

            }
            
        }
        public void gotolinkedinlink()
        {
            try
            {
                Device.OpenUri(new Uri(linkedinlink));
            }
            catch (Exception ex)
            {

            }
            
        }
        public void gotojobalert()
        {
            var currentpage = GetCurrentpage();
            currentpage.Navigation.PushAsync(new Features.Detail.Views.Jobalert());
        }
        
        public async void gotojobseekerprofile()
        {
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                var currentpage = GetCurrentpage();
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                NRIApp.USHome.Views.HomePage.seekerlogincode = 1;
            }
            else
            {
                NRIApp.USHome.Views.HomePage.seekerlogincode = 0;
                Jobseeker.Models.Jobseekers_DATA data = new Jobseeker.Models.Jobseekers_DATA();
                var currentpage = GetCurrentpage();
                await currentpage.Navigation.PushAsync(new Features.Jobseeker.View.Jobseekerprofile(data));
            }
            
        }
        public async void gotodetailpage (LocalJobResponse data)
        {
            var currentpage = GetCurrentpage();
            await currentpage.Navigation.PushAsync(new Features.Detail.Views.JobDetails(data.adid,data.titleurl, Commonsettings.UserPid));
        }
        public async void gotodetailpageJO(Businessjobopeningdata data)
        {
            var currentpage = GetCurrentpage();
            await currentpage.Navigation.PushAsync(new Features.Detail.Views.JobDetails(data.adid, data.titleurl, Commonsettings.UserPid));
        }
        public async void applyforjobopening(Businessjobopeningdata data)
        {
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                var currentpage = GetCurrentpage();
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
            }
            else
            {
                var currentpage = GetCurrentpage();
                await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Detail.Views.Apply_Form(data.adid, "", data.resumemandatory, businessurl));
            }
        }
        public async void applyforjob(Businessjobopeningdata data)
        {
                var currentpage = GetCurrentpage();
                await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Detail.Views.Apply_Form("", "", "", businessurl));
        }
        public async void addreply(Rating_Details reviewsdtl)
        {
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                var currentpage = GetCurrentpage();
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
            }
            else
            {
                var currentpage = GetCurrentpage();
                await currentpage.Navigation.PushAsync(new Replytoreview(reviewsdtl));
            }
        }
       
        public void companytap()
        {
            companydescvisible = true;
            jobopeningvisible = false;
            empsalaryvisible = false;
            var currentpage = GetCurrentpage();
            var companydesctxtclr = currentpage.FindByName<Label>("companydesctxtclr");
            var companydescboxclr = currentpage.FindByName<BoxView>("companydescboxclr");
            companydesctxtclr.TextColor = Color.FromHex("#2e74f0");
            companydescboxclr.BackgroundColor = Color.FromHex("#2e74f0");
            var jobopeningtxtclr = currentpage.FindByName<Label>("jobopeningtxtclr");
            var jobopeningboxclr = currentpage.FindByName<BoxView>("jobopeningboxclr");
            jobopeningtxtclr.TextColor = Color.FromHex("#212121");
            jobopeningboxclr.BackgroundColor = Color.FromHex("#ffffff");
            var salarytxtclr = currentpage.FindByName<Label>("salarytxtclr");
            var salaryboxclr = currentpage.FindByName<BoxView>("salaryboxclr");
            salarytxtclr.TextColor =Color.FromHex("#212121") ;
            salaryboxclr.BackgroundColor = Color.FromHex("#ffffff");

        }
        public void joblisttap()
        {
            companydescvisible = false;
            jobopeningvisible = true;
            empsalaryvisible = false;
            var currentpage = GetCurrentpage();
            var companydesctxtclr = currentpage.FindByName<Label>("companydesctxtclr");
            var companydescboxclr = currentpage.FindByName<BoxView>("companydescboxclr");
           
            var jobopeningtxtclr = currentpage.FindByName<Label>("jobopeningtxtclr");
            var jobopeningboxclr = currentpage.FindByName<BoxView>("jobopeningboxclr");
           
            var salarytxtclr = currentpage.FindByName<Label>("salarytxtclr");
            var salaryboxclr = currentpage.FindByName<BoxView>("salaryboxclr");

            companydesctxtclr.TextColor = Color.FromHex("#212121");
            companydescboxclr.BackgroundColor = Color.FromHex("#ffffff");
            jobopeningtxtclr.TextColor = Color.FromHex("#2e74f0");
            jobopeningboxclr.BackgroundColor = Color.FromHex("#2e74f0");
            salarytxtclr.TextColor = Color.FromHex("#212121");
            salaryboxclr.BackgroundColor = Color.FromHex("#ffffff");
            getjobopeninglist(businessurl);

        }
        public void salarylisttap()
        {
            companydescvisible = false;
            jobopeningvisible = false;
            empsalaryvisible = true;

            var currentpage = GetCurrentpage();
            var companydesctxtclr = currentpage.FindByName<Label>("companydesctxtclr");
            var companydescboxclr = currentpage.FindByName<BoxView>("companydescboxclr");

            var jobopeningtxtclr = currentpage.FindByName<Label>("jobopeningtxtclr");
            var jobopeningboxclr = currentpage.FindByName<BoxView>("jobopeningboxclr");

            var salarytxtclr = currentpage.FindByName<Label>("salarytxtclr");
            var salaryboxclr = currentpage.FindByName<BoxView>("salaryboxclr");

            companydesctxtclr.TextColor = Color.FromHex("#212121");
            companydescboxclr.BackgroundColor = Color.FromHex("#ffffff");
            jobopeningtxtclr.TextColor = Color.FromHex("#212121");
            jobopeningboxclr.BackgroundColor = Color.FromHex("#ffffff");
            salarytxtclr.TextColor = Color.FromHex("#2e74f0");
            salaryboxclr.BackgroundColor = Color.FromHex("#2e74f0");
            getsalarylist(businessurl);
        }
        public async void getjobopeninglist(string businessurl)
        {
            try
            {
                dialogs.ShowLoading("", null);
                var jobopeningapi = RestService.For<IComapanydata>(Commonsettings.LocaljobsAPI);
                var jobopeningdata = await jobopeningapi.jobopeninglist(businessurl,1,1);
                if (jobopeningdata.ROW_DATA.Last().totalrecs > 0 && jobopeningdata != null)
                {
                    jobopeningvisible = true;
                    jovisible = true;
                    foreach (var data in jobopeningdata.ROW_DATA)
                    {
                        if ((string.IsNullOrEmpty(data.experiencefrom) || data.experiencefrom == "0") && (string.IsNullOrEmpty(data.experienceto) || data.experienceto == "0"))
                        {
                            data.experience = "Freshers";
                        }
                        else
                        {
                            data.experience = data.experiencefrom + " - " + data.experienceto + " Years";
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
                    var currentpage = GetCurrentpage();
                    if (jobopeningdata.ROW_DATA.First().totalrecs > 1)
                    {
                        Jobopeningmoreblk = true;
                    }
                    //NRIApp.Helpers.HVScrollGridView JobssListviewlst = currentpage.FindByName<NRIApp.Helpers.HVScrollGridView>("JobopeningListview");
                    ListView JobssListviewlst = currentpage.FindByName<ListView>("JobopeningListview");
                    JobssListviewlst.ItemsSource = jobopeningdata.ROW_DATA;
                   // jobslists?.AddRange(jobopeningdata.ROW_DATA);
                    dialogs.HideLoading();
                }
                else
                {
                    jobopeningvisible = false;
                    jovisible = false;
                }
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public async void getbusinessbenifitslist()
        {
            try
            {
                dialogs.ShowLoading("", null);
                var businessbenefiteapi = RestService.For<IComapanydata>(Commonsettings.LocaljobsAPI);
                var benefitsdata = await businessbenefiteapi.businessbenifitlist(businessurl, "10");
                getbusinessbenifit = benefitsdata.ROW_DATA;
                if (getbusinessbenifit != null && getbusinessbenifit.Count > 0)
                {
                    benefitsvisible = true;
                }
                else
                {
                    benefitsvisible = false;
                }
                dialogs.HideLoading();
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
                string msg = ex.Message;
            }
        }
        public async void getsalarylist(string businesurl)
        {
            try
            {
                dialogs.ShowLoading("", null);
                salarypageno = 1;
                var salaryapi = RestService.For<IComapanydata>(Commonsettings.LocaljobsAPI);
                var salarydata = await salaryapi.salarylist(businesurl, salarypageno);
                if (salarydata != null && salarydata.ROW_DATA.Count > 0)
                {
                    salaryheadvisible = true;
                   // empsalaryvisible = true;

                    foreach (var data in salarydata.ROW_DATA)
                    {
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
                                data.salarymode = "Per Hour";
                            if (data.salarymode.ToLower() == "yearly")
                                data.salarymode = "Per Year";
                            if (data.salarymode.ToLower() == "weekly")
                                data.salarymode = "Per Week";
                            if (data.salarymode.ToLower() == "monthly")
                                data.salarymode = "Per Month";
                        }
                    }
                    if(salarydata.ROW_DATA.Count>10)
                    {
                        Salarymoreblk = true;
                    }
                    getsalarydata = salarydata.ROW_DATA;
                    dialogs.HideLoading();
                }
                else
                {
                    salaryheadvisible = false;
                  //  empsalaryvisible = false;
                }
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public int salarypageno;
        public async void getmoresalarylist()
        {
            var list = new List<Salarylistdata>();
            salarypageno = salarypageno + 1;
            try
            {
                Salarymoreblk = false;
                var currentpage = GetCurrentpage();
                ActivityIndicator jobloader = currentpage.FindByName<ActivityIndicator>("salaryloader");
                jobloader.IsRunning = true;
                jobloader.IsVisible = true;
                IsBusy = true;
                var salaryapi = RestService.For<IComapanydata>(Commonsettings.LocaljobsAPI);
                var salarydata = await salaryapi.salarylist(businessurl,salarypageno);
                if (salarydata != null && salarydata.ROW_DATA.Count > 0)
                {
                    salaryheadvisible = true;

                    foreach (var data in salarydata.ROW_DATA)
                    {
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
                                data.salarymode = "Per Hour";
                            if (data.salarymode.ToLower() == "yearly")
                                data.salarymode = "Per Year";
                            if (data.salarymode.ToLower() == "weekly")
                                data.salarymode = "Per Week";
                            if (data.salarymode.ToLower() == "monthly")
                                data.salarymode = "Per Month";
                        }
                    }
                    jobloader.IsRunning = false;
                    jobloader.IsVisible = false;
                    list = salarydata.ROW_DATA;
                    IsBusy = false;
                    getsalarydata?.AddRange(salarydata.ROW_DATA);
                    Salarymoreblk = false;
                    Helpers.HVScrollGridView Jobssalarylst = currentpage.FindByName<HVScrollGridView>("salarylistview");
                    Jobssalarylst.ItemsSource = null;
                    Jobssalarylst.ItemsSource = getsalarydata;
                }
                if (salarydata.ROW_DATA.First().totalrecs != getsalarydata.Count)
                {
                    Salarymoreblk = true;
                }
                else
                {
                    Salarymoreblk = false;
                }
            }
            catch (Exception e)
            {
                dialogs.HideLoading();
            }
        }
        public async void getreviewdetails()
        {
            try
            {
                dialogs.ShowLoading("", null);
                var getreviewapi = RestService.For<IComapanydata>(Commonsettings.LocaljobsAPI);
                var getreviewdetails = await getreviewapi.getreviewdetails(businessid);
                if(getreviewdetails!=null && getreviewdetails.ROW_DATA.Count>0)
                {
                    reviewcommentsvisible = true;
                    foreach (var item in getreviewdetails.ROW_DATA)
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
                    //if (getreviewdetails.ROW_DATA.First().ovreviewcount > 10)
                    //{
                    //    Reviewmoreblk = true;
                    //}
                    getreviewdetail =getreviewdetails.ROW_DATA;
                    dialogs.HideLoading();
                }
                else
                {
                    reviewcommentsvisible = false;
                }
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
        }
       
        public bool Reviewvalidation()
        {
            bool result = true;
            var currentpage = GetCurrentpage();
            if (string.IsNullOrEmpty(rating))
            {
                dialogs.Toast("Please select your rating");
                return false;
            }
            if (string.IsNullOrEmpty(reviewdesc) || reviewdesc.Trim().Length==0)
            {
                dialogs.Toast("Description cannot be empty");
                return false;
            }
            if(!string.IsNullOrEmpty(reviewdesc))
            {
                if (reviewdesc.Length > 1500)
                {
                    dialogs.Toast("Description must be between 1500 characters");
                    return false;
                }
            }
           
            return result;
        }
        Add_Rating_Details ratingdata = new Add_Rating_Details();
        public async void publishreviewdetails()
        {
            try
            {
                dialogs.ShowLoading("",null);
                if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                {
                    var currentpage = GetCurrentpage();
                    await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                }
                else
                {
                    if (Reviewvalidation())
                    {
                        ratingdata.pid = Commonsettings.UserPid;
                        ratingdata.contributor = Commonsettings.UserName;
                        ratingdata.shortdesc = reviewdesc;
                        ratingdata.objectid = businessid;
                        ratingdata.ipaddress = ipaddress;
                        ratingdata.city = Commonsettings.Usercity;
                        ratingdata.statename = Commonsettings.Userstate;
                        ratingdata.lat = Commonsettings.UserLat.ToString();
                        ratingdata.@long = Commonsettings.UserLong.ToString();
                        ratingdata.zipcode = Commonsettings.Userzipcode;
                        ratingdata.careerrating = careerrating;
                        ratingdata.workrating = workrating;
                        ratingdata.rating = rating;
                        ratingdata.managementrating = managementrating;
                        var addreview = RestService.For<IComapanydata>(Commonsettings.LocaljobsAPI);
                        var addreviewdetails = await addreview.addreview(ratingdata);
                        if(addreviewdetails.resultinformation == "inserted success")//success
                        {
                            
                            CustomerRating1 = "Star_inactive.png";
                            CustomerRating2 = "Star_inactive.png";
                            CustomerRating3 = "Star_inactive.png";
                            CustomerRating4 = "Star_inactive.png";
                            CustomerRating5 = "Star_inactive.png";


                            Careerrating1 = "Star_inactive.png";
                            Careerrating2 = "Star_inactive.png";
                            Careerrating3 = "Star_inactive.png";
                            Careerrating4 = "Star_inactive.png";
                            Careerrating5 = "Star_inactive.png";

                            workrating1 = "Star_inactive.png";
                            workrating2 = "Star_inactive.png";
                            workrating3 = "Star_inactive.png";
                            workrating4 = "Star_inactive.png";
                            workrating5 = "Star_inactive.png";

                            managementrating1 = "Star_inactive.png";
                            managementrating2 = "Star_inactive.png";
                            managementrating3 = "Star_inactive.png";
                            managementrating4 = "Star_inactive.png";
                            managementrating5 = "Star_inactive.png";

                            Ratingbar = "0/5";

                            dialogs.Toast("Review published successfully");
                            reviewdesc = "";
                            getreviewdetails();
                        }
                    }
                }
                dialogs.HideLoading();
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
                string msg = ex.Message;
            }
        }
        public async Task taponmanagecompany(Search cdata)
        {
            if(verifytxt== "Manage this company now!" || verifyvisible==true)
            {
                var currentpage = GetCurrentpage();
                await currentpage.Navigation.PushAsync(new Features.Detail.Views.Managecompany(cdata));
            }
        }
        Companyprofiledata companypdata = new Companyprofiledata();
        public async void getcompanyprofile(string businessurl)
        {
            try
            {
                dialogs.ShowLoading("", null);
              //  businessurl = "iam-business";
                string userpid = Commonsettings.UserPid;
                var companyprofileapi = RestService.For<IComapanydata>(Commonsettings.LocaljobsAPI);
                if (string.IsNullOrEmpty(userpid))
                {
                    userpid = "";
                }
                var companydatas = await companyprofileapi.Getcompanyprofile(businessurl, userpid);
                //check review availablity
                if(companydatas.ROW_DATA.Last().reviewcount==0 || string.IsNullOrEmpty(companydatas.ROW_DATA.Last().ratingavg)) //|| companydatas.ROW_DATA.Last().ratingavg=="0"
                {
                    reviewvisible = false;
                    ratingbarvalue = "(0) Be the first to review";
                }
                else
                {
                    reviewvisible = true;
                    ratingbarvalue = companydatas.ROW_DATA.Last().ratingavg + "/ 5";
                }
                if(companydatas.ROW_DATA.Last().claimflag==0)
                {
                    verifytxt = "Verified";
                    verifyvisible = false;
                }
                else
                {
                    verifytxt = "Manage this company now!";
                    verifyvisible = true;
                }
                if(companydatas.ROW_DATA.Last().adscnt != 0)
                {
                    applyjobvisible = true;
                }
                if(!string.IsNullOrEmpty(companydatas.ROW_DATA.Last().thumbimgurl))
                {
                    thumbimgvisible = true;
                    thumbimgurl = companydatas.ROW_DATA.Last().thumbimgurl;
                }
                if (companydatas.ROW_DATA.Last().ratingavg == "1")
                {
                    Grating1 = "StarActiveWt.png";
                    Grating2 = "Star_inactiveWt.png";
                    Grating3 = "Star_inactiveWt.png";
                    Grating4 = "Star_inactiveWt.png";
                    Grating5 = "Star_inactiveWt.png";
                    rating = "1";
                    ratingbarvalue = "1/5";
                }
                else if (companydatas.ROW_DATA.Last().ratingavg == "2")
                {
                    Grating1 = "StarActiveWt.png";
                    Grating2 = "StarActiveWt.png";
                    Grating3 = "Star_inactiveWt.png";
                    Grating4 = "Star_inactiveWt.png";
                    Grating5 = "Star_inactiveWt.png";
                    rating = "2";
                    ratingbarvalue = "2/5";
                }
                else if (companydatas.ROW_DATA.Last().ratingavg == "3")
                {
                    Grating1 = "StarActiveWt.png";
                    Grating2 = "StarActiveWt.png";
                    Grating3 = "StarActiveWt.png";
                    Grating4 = "Star_inactiveWt.png";
                    Grating5 = "Star_inactiveWt.png";
                    rating = "3";
                    ratingbarvalue = "3/5";
                }
                else if (companydatas.ROW_DATA.Last().ratingavg == "4")
                {
                    Grating1 = "StarActiveWt.png";
                    Grating2 = "StarActiveWt.png";
                    Grating3 = "StarActiveWt.png";
                    Grating4 = "StarActiveWt.png";
                    Grating5 = "Star_inactiveWt.png";
                    rating = "4";
                    ratingbarvalue = "4/5";
                }
                else if (companydatas.ROW_DATA.Last().ratingavg == "5")
                {
                    Grating1 = "StarActiveWt.png";
                    Grating2 = "StarActiveWt.png";
                    Grating3 = "StarActiveWt.png";
                    Grating4 = "StarActiveWt.png";
                    Grating5 = "StarActiveWt.png";
                    rating = "5";
                    ratingbarvalue = "5/5";
                }
                if(!string.IsNullOrEmpty(companydatas.ROW_DATA.Last().followflag) && companydatas.ROW_DATA.Last().followflag=="1")
                {
                    followflagimg = "ReportflagActive.png";
                    followflag = "1";
                }
                else
                {
                    followflagimg = "Reportflag.png";
                    followflag = "0";
                }
                title = companydatas.ROW_DATA.Last().businessname+" Careers in "+companydatas.ROW_DATA.Last().city+", "+ companydatas.ROW_DATA.Last().statecode;
                address = "Headquartered in: "+companydatas.ROW_DATA.Last().address;

                founded = companydatas.ROW_DATA.Last().founded;
                if(!string.IsNullOrEmpty(founded))
                {
                    foundedvisible = true;
                }
                //companysize = companydatas.ROW_DATA.Last().companysize +" employees";
                
                if (!string.IsNullOrEmpty(companydatas.ROW_DATA.Last().companysize))
                {
                    companysize = companydatas.ROW_DATA.Last().companysize.Replace("employees", "");
                    companysize = companysize+" employees";
                    nooempvisible = true;
                }
                else
                {
                    nooempvisible = false;
                }
                industry = companydatas.ROW_DATA.Last().industry;
                if (!string.IsNullOrEmpty(industry))
                {
                    industryvisible = true;
                }
                businesstype = companydatas.ROW_DATA.Last().businesstype;
                if (!string.IsNullOrEmpty(businesstype))
                {
                    businesstypevisble = true;
                }
                if (!string.IsNullOrEmpty(companydatas.ROW_DATA.Last().shortdesc))
                {
                    shortdesc = companydatas.ROW_DATA.Last().shortdesc;
                    companydescvisible = true;
                    var currentpage = GetCurrentpage();
                    var companydesctxtclr = currentpage.FindByName<Label>("companydesctxtclr");
                    var companydescboxclr = currentpage.FindByName<BoxView>("companydescboxclr");
                    companydesctxtclr.TextColor = Color.FromHex("#2e74f0");
                    companydescboxclr.BackgroundColor = Color.FromHex("#2e74f0");
                    covvisible = true;
                }
                else if(companydatas.ROW_DATA.Last().jobopeninglist==1)
                {
                    var currentpage = GetCurrentpage();
                    var jobopeningtxtclr = currentpage.FindByName<Label>("jobopeningtxtclr");
                    var jobopeningboxclr = currentpage.FindByName<BoxView>("jobopeningboxclr");
                    jobopeningtxtclr.TextColor = Color.FromHex("#2e74f0");
                    jobopeningboxclr.BackgroundColor = Color.FromHex("#2e74f0");
                    jovisible = true;
                    getjobopeninglist(businessurl);
                }
                else if (companydatas.ROW_DATA.Last().salarylist == 1)
                {
                    var currentpage = GetCurrentpage();
                    var salarytxtclr = currentpage.FindByName<Label>("salarytxtclr");
                    var salaryboxclr = currentpage.FindByName<BoxView>("salaryboxclr");
                    salarytxtclr.TextColor = Color.FromHex("#2e74f0");
                    salaryboxclr.BackgroundColor = Color.FromHex("#2e74f0");
                    salaryheadvisible = true;
                    getsalarylist(businessurl);
                }
                if (!string.IsNullOrEmpty(companydatas.ROW_DATA.Last().facebook))
                {
                    fbvisible = true;
                    socialmediavisible = true;
                    facebooklink = companydatas.ROW_DATA.Last().facebook;
                  
                }
                if(!string.IsNullOrEmpty(companydatas.ROW_DATA.Last().twitter))
                {
                    twittervisible = true;
                    socialmediavisible = true;
                    twitterlink = companydatas.ROW_DATA.Last().twitter;
                
                }
                if(!string.IsNullOrEmpty(companydatas.ROW_DATA.Last().linkedin))
                {
                    socialmediavisible = true;
                    linkedinvisible = true;
                    linkedinlink = companydatas.ROW_DATA.Last().linkedin;
                }
                cityurl = companydatas.ROW_DATA.Last().cityurl;
                businessid = companydatas.ROW_DATA.Last().businessid.ToString();
                followflag = companydatas.ROW_DATA.Last().followflag;
                newcityurl = companydatas.ROW_DATA.Last().newcityurl;
                aboutcompanyoverviewlbl = "About " + companydatas.ROW_DATA.Last().businessname;
                aboutjobopeninglbl = companydatas.ROW_DATA.Last().businessname+" Job Openings";
                aboutsalarylbl = companydatas.ROW_DATA.Last().businessname+" Salaries";
                aboutbenefitslbl = companydatas.ROW_DATA.Last().businessname + " Benefits";
                companypdata.businesstitleurl = companydatas.ROW_DATA.Last().businesstitleurl;
                companypdata.address = companydatas.ROW_DATA.Last().address;
                companypdata.latitude = companydatas.ROW_DATA.Last().latitude;
                companypdata.longitude = companydatas.ROW_DATA.Last().longitude;

                //getjobopeninglist(businessurl);
                //getbusinessbenifitslist();
                //getsalarylist(businessurl);
                //getreviewdetails();

                if (companydatas.ROW_DATA.Last().benefits == 1)
                {
                    getbusinessbenifitslist();
                    benefitsvisible = true;
                }
                else
                {
                    benefitsvisible = false;
                }
                if (companydatas.ROW_DATA.Last().reviews == 1)
                {
                    getreviewdetails();
                    //reviewcommentsvisible = true;
                }
                else
                {
                    reviewcommentsvisible = false;
                }
                if (companydatas.ROW_DATA.Last().salarylist == 1)
                {
                   // getsalarylist(businessurl);
                    salaryheadvisible = true;
                }
                else
                {
                    salaryheadvisible = false;
                }
                if (companydatas.ROW_DATA.Last().jobopeninglist == 1)
                {
                   // getjobopeninglist(businessurl);
                    jovisible = true;
                }
                else
                {
                    jovisible = false;
                }
                if(companydatas.ROW_DATA.Last().totalrecs!=0)
                {
                    jobopeningcnttxt = "Jobs(" + companydatas.ROW_DATA.Last().totalrecs +")";
                }
                else
                {
                    jobopeningcnttxt = "Job Openings";
                }
                await Task.Delay(2000);
                dialogs.HideLoading();
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
                string msg = ex.Message;
            }
        }
        public void Sharecompanyprofile()
        {
            //Hi, I found this Job Ad in Sulekha.com and would like to share it with you.Have a look at it and provide me your feedback. http://localjobs.sulekha.com/job-openings-in-streamlink-software-inc-at-clev
            //http://localjobs.sulekha.com/job-openings-in-streamlink-software-inc-at-clev
          try
            {
                string url = "http://localjobs.sulekha.com/job-openings-in-" + companypdata.businesstitleurl + "-at-" + newcityurl;
                CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
                {
                    Url = url
                });

            }
            catch(Exception ex)
            {

            }
        }
        public async void taptofollow()
        {
            try
            {
                dialogs.ShowLoading("", null);
                if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                {
                    var currentpage = GetCurrentpage();
                    await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                }
                else
                {
                    var companyprofileapi = RestService.For<IComapanydata>(Commonsettings.LocaljobsAPI);
                    if (string.IsNullOrEmpty(followflag) || followflag == "0")
                    {
                        followflag = "1";
                        followflagimg = "ReportflagActive.png";
                    }
                    else if (followflag == "1")
                    {
                        followflag = "0";
                        followflagimg = "Reportflag.png";
                    }
                    var companydatas = await companyprofileapi.Businessfollow(Commonsettings.UserPid, businessid, Commonsettings.UserEmail, Commonsettings.UserName, followflag);
                }
                dialogs.HideLoading();
                
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
                string msg = ex.Message;
            }
        }
        
        public void scrolldown()
        {
           try
            {
                var currentpage = GetCurrentpage();
                var scrollview = currentpage.FindByName<ScrollView>("cmscroll");
                var stacklayout = currentpage.FindByName<StackLayout>("cmlayout");
                Device.BeginInvokeOnMainThread(() =>
                {
                    scrollview.ScrollToAsync(stacklayout, ScrollToPosition.End, false);
                    //  await ScrollView. ScrollToAsync((Grid)grid, ScrollToPosition.End, false);
                });
            }
            catch(Exception ex)
            {

            }

        }
        private void clickoverallrating(object sender)
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
                rating = "1";
                Ratingbar = "1/5";
                
                
            }
            else if (id == "2")
            {
                if (CustomerRating2 == "StarActive.png")
                {
                    CustomerRating1 = "StarActive.png";
                    CustomerRating2 = "Star_inactive.png";
                    CustomerRating3 = "Star_inactive.png";
                    CustomerRating4 = "Star_inactive.png";
                    CustomerRating5 = "Star_inactive.png";
                    rating = "1";
                    Ratingbar = "1/5";
                }
                else
                {
                    CustomerRating1 = "StarActive.png";
                    CustomerRating2 = "StarActive.png";
                    CustomerRating3 = "Star_inactive.png";
                    CustomerRating4 = "Star_inactive.png";
                    CustomerRating5 = "Star_inactive.png";
                    rating = "2";
                    Ratingbar = "2/5";
                }
                
            }
            else if (id == "3")
            {
                if (CustomerRating3 == "StarActive.png")
                {
                    CustomerRating1 = "StarActive.png";
                    CustomerRating2 = "StarActive.png";
                    CustomerRating3 = "Star_inactive.png";
                    CustomerRating4 = "Star_inactive.png";
                    CustomerRating5 = "Star_inactive.png";
                    rating = "2";
                    Ratingbar = "2/5";
                }
                else
                {
                    CustomerRating1 = "StarActive.png";
                    CustomerRating2 = "StarActive.png";
                    CustomerRating3 = "StarActive.png";
                    CustomerRating4 = "Star_inactive.png";
                    CustomerRating5 = "Star_inactive.png";
                    rating = "3";
                    Ratingbar = "3/5";
                }
                    
            }
            else if (id == "4")
            {
                if (CustomerRating4 == "StarActive.png")
                {
                    CustomerRating1 = "StarActive.png";
                    CustomerRating2 = "StarActive.png";
                    CustomerRating3 = "StarActive.png";
                    CustomerRating4 = "Star_inactive.png";
                    CustomerRating5 = "Star_inactive.png";
                    rating = "3";
                    Ratingbar = "3/5";
                }
                else
                {
                    CustomerRating1 = "StarActive.png";
                    CustomerRating2 = "StarActive.png";
                    CustomerRating3 = "StarActive.png";
                    CustomerRating4 = "StarActive.png";
                    CustomerRating5 = "Star_inactive.png";
                    rating = "4";
                    Ratingbar = "4/5";
                }
                   
            }
            else if (id == "5")
            {
                if (CustomerRating5 == "StarActive.png")
                {

                    CustomerRating1 = "StarActive.png";
                    CustomerRating2 = "StarActive.png";
                    CustomerRating3 = "StarActive.png";
                    CustomerRating4 = "StarActive.png";
                    CustomerRating5 = "Star_inactive.png";
                    rating = "4";
                    Ratingbar = "4/5";
                }
                else
                {

                    CustomerRating1 = "StarActive.png";
                    CustomerRating2 = "StarActive.png";
                    CustomerRating3 = "StarActive.png";
                    CustomerRating4 = "StarActive.png";
                    CustomerRating5 = "StarActive.png";
                    rating = "5";
                    Ratingbar = "5/5";
                }
            }
            rating = id;
        }
        private void clickcareerrating(object sender)
        {
            var view = sender as Xamarin.Forms.Image;

            string id = view.ClassId.ToString();
            if (id == "1")
            {
                //if(Careerrating1== "StarActive.png")
                //{
                //    Careerrating1 = "Star_inactive.png";
                //    Careerrating2 = "Star_inactive.png";
                //    Careerrating3 = "Star_inactive.png";
                //    Careerrating4 = "Star_inactive.png";
                //    Careerrating5 = "Star_inactive.png";
                //}
                //else
                //{
                    Careerrating1 = "StarActive.png";
                    Careerrating2 = "Star_inactive.png";
                    Careerrating3 = "Star_inactive.png";
                    Careerrating4 = "Star_inactive.png";
                    Careerrating5 = "Star_inactive.png";
                //}
                
            }
            else if (id == "2")
            {
                if(Careerrating2== "StarActive.png")
                {
                    Careerrating1 = "StarActive.png";
                    Careerrating2 = "Star_inactive.png";
                    Careerrating3 = "Star_inactive.png";
                    Careerrating4 = "Star_inactive.png";
                    Careerrating5 = "Star_inactive.png";
                }
                else
                {
                    Careerrating1 = "StarActive.png";
                    Careerrating2 = "StarActive.png";
                    Careerrating3 = "Star_inactive.png";
                    Careerrating4 = "Star_inactive.png";
                    Careerrating5 = "Star_inactive.png";
                }
                
            }
            else if (id == "3")
            {
                if(Careerrating3== "StarActive.png")
                {
                    Careerrating1 = "StarActive.png";
                    Careerrating2 = "StarActive.png";
                    Careerrating3 = "Star_inactive.png";
                    Careerrating4 = "Star_inactive.png";
                    Careerrating5 = "Star_inactive.png";
                }
                else
                {
                    Careerrating1 = "StarActive.png";
                    Careerrating2 = "StarActive.png";
                    Careerrating3 = "StarActive.png";
                    Careerrating4 = "Star_inactive.png";
                    Careerrating5 = "Star_inactive.png";
                }
               
            }
            else if (id == "4")
            {
                if (Careerrating4 == "StarActive.png")
                {
                    Careerrating1 = "StarActive.png";
                    Careerrating2 = "StarActive.png";
                    Careerrating3 = "StarActive.png";
                    Careerrating4 = "Star_inactive.png";
                    Careerrating5 = "Star_inactive.png";
                }
                else
                {
                    Careerrating1 = "StarActive.png";
                    Careerrating2 = "StarActive.png";
                    Careerrating3 = "StarActive.png";
                    Careerrating4 = "StarActive.png";
                    Careerrating5 = "Star_inactive.png";
                }
                    
            }
            else if (id == "5")
            {
                if (Careerrating5 == "StarActive.png")
                {
                    Careerrating1 = "StarActive.png";
                    Careerrating2 = "StarActive.png";
                    Careerrating3 = "StarActive.png";
                    Careerrating4 = "StarActive.png";
                    Careerrating5 = "Star_inactive.png";
                }
                else
                {
                    Careerrating1 = "StarActive.png";
                    Careerrating2 = "StarActive.png";
                    Careerrating3 = "StarActive.png";
                    Careerrating4 = "StarActive.png";
                    Careerrating5 = "StarActive.png";
                }
                   
            }
            careerrating = id;
        }
        private void clickworkrating(object sender)
        {
            var view = sender as Xamarin.Forms.Image;

            string id = view.ClassId.ToString();
            if (id == "1")
            {
                //if(workrating1== "StarActive.png")
                //{
                //    workrating1 = "StarActive.png";
                //    workrating2 = "Star_inactive.png";
                //    workrating3 = "Star_inactive.png";
                //    workrating4 = "Star_inactive.png";
                //    workrating5 = "Star_inactive.png";
                //}
                //else
                //{
                    workrating1 = "StarActive.png";
                    workrating2 = "Star_inactive.png";
                    workrating3 = "Star_inactive.png";
                    workrating4 = "Star_inactive.png";
                    workrating5 = "Star_inactive.png";
                //}
                
            }
            else if (id == "2")
            {
                if (workrating2 == "StarActive.png")
                {
                    workrating1 = "StarActive.png";
                    workrating2 = "Star_inactive.png";
                    workrating3 = "Star_inactive.png";
                    workrating4 = "Star_inactive.png";
                    workrating5 = "Star_inactive.png";
                }
                else
                {
                    workrating1 = "StarActive.png";
                    workrating2 = "StarActive.png";
                    workrating3 = "Star_inactive.png";
                    workrating4 = "Star_inactive.png";
                    workrating5 = "Star_inactive.png";
                }
                    
            }
            else if (id == "3")
            {
                if (workrating3 == "StarActive.png")
                {
                    workrating1 = "StarActive.png";
                    workrating2 = "StarActive.png";
                    workrating3 = "Star_inactive.png";
                    workrating4 = "Star_inactive.png";
                    workrating5 = "Star_inactive.png";
                }
                else
                {
                    workrating1 = "StarActive.png";
                    workrating2 = "StarActive.png";
                    workrating3 = "StarActive.png";
                    workrating4 = "Star_inactive.png";
                    workrating5 = "Star_inactive.png";
                }
                   
            }
            else if (id == "4")
            {
                if (workrating4 == "StarActive.png")
                {
                    workrating1 = "StarActive.png";
                    workrating2 = "StarActive.png";
                    workrating3 = "StarActive.png";
                    workrating4 = "Star_inactive.png";
                    workrating5 = "Star_inactive.png";
                }
                else
                {
                    workrating1 = "StarActive.png";
                    workrating2 = "StarActive.png";
                    workrating3 = "StarActive.png";
                    workrating4 = "StarActive.png";
                    workrating5 = "Star_inactive.png";
                }
                    
            }
            else if (id == "5")
            {
                if (workrating5 == "StarActive.png")
                {
                    workrating1 = "StarActive.png";
                    workrating2 = "StarActive.png";
                    workrating3 = "StarActive.png";
                    workrating4 = "StarActive.png";
                    workrating5 = "Star_inactive.png";
                }
                else
                {
                    workrating1 = "StarActive.png";
                    workrating2 = "StarActive.png";
                    workrating3 = "StarActive.png";
                    workrating4 = "StarActive.png";
                    workrating5 = "StarActive.png";
                }
                    
            }
            workrating = id;
        }
        private void clickmanagementrating(object sender)
        {
            var view = sender as Xamarin.Forms.Image;

            string id = view.ClassId.ToString();
            if (id == "1")
            {
                managementrating1 = "StarActive.png";
                managementrating2 = "Star_inactive.png";
                managementrating3 = "Star_inactive.png";
                managementrating4 = "Star_inactive.png";
                managementrating5 = "Star_inactive.png";
            }
            else if (id == "2")
            {
                if(managementrating1 == "StarActive.png")
                {
                    managementrating1 = "StarActive.png";
                    managementrating2 = "Star_inactive.png";
                    managementrating3 = "Star_inactive.png";
                    managementrating4 = "Star_inactive.png";
                    managementrating5 = "Star_inactive.png";
                }
                else
                {
                    managementrating1 = "StarActive.png";
                    managementrating2 = "StarActive.png";
                    managementrating3 = "Star_inactive.png";
                    managementrating4 = "Star_inactive.png";
                    managementrating5 = "Star_inactive.png";
                }
                
            }
            else if (id == "3")
            {
                if (managementrating3 == "StarActive.png")
                {
                    managementrating1 = "StarActive.png";
                    managementrating2 = "StarActive.png";
                    managementrating3 = "Star_inactive.png";
                    managementrating4 = "Star_inactive.png";
                    managementrating5 = "Star_inactive.png";
                }
                else
                {
                    managementrating1 = "StarActive.png";
                    managementrating2 = "StarActive.png";
                    managementrating3 = "StarActive.png";
                    managementrating4 = "Star_inactive.png";
                    managementrating5 = "Star_inactive.png";
                }
                    
            }
            else if (id == "4")
            {
                if (managementrating4 == "StarActive.png")
                {
                    managementrating1 = "StarActive.png";
                    managementrating2 = "StarActive.png";
                    managementrating3 = "StarActive.png";
                    managementrating4 = "Star_inactive.png";
                    managementrating5 = "Star_inactive.png";
                }
                else
                {
                    managementrating1 = "StarActive.png";
                    managementrating2 = "StarActive.png";
                    managementrating3 = "StarActive.png";
                    managementrating4 = "StarActive.png";
                    managementrating5 = "Star_inactive.png";
                }
                    
            }
            else if (id == "5")
            {
                if (managementrating5 == "StarActive.png")
                {
                    managementrating1 = "StarActive.png";
                    managementrating2 = "StarActive.png";
                    managementrating3 = "StarActive.png";
                    managementrating4 = "StarActive.png";
                    managementrating5 = "Star_inactive.png";
                }
                else
                {
                    managementrating1 = "StarActive.png";
                    managementrating2 = "StarActive.png";
                    managementrating3 = "StarActive.png";
                    managementrating4 = "StarActive.png";
                    managementrating5 = "StarActive.png";
                }
                    
            }
            managementrating = id;
        }
        public async void TapOnMapView()
        {
            await CrossMapsPlugin.Current.PinTo(companypdata.address,companypdata.latitude ,companypdata.longitude, 8);
        }
        public CompanyprofileVM(Rating_Details reviewdtl)
        {
            businessid = reviewdtl.objectid.ToString();
            Binddetailreviews(reviewdtl.reviewid.ToString(), businessid);
            Bindreviewreply(reviewdtl.reviewid.ToString(), businessid);
            Replysubmit = new Command(async () => await replysubmit(reviewdtl.reviewid.ToString(), businessid));
           //   Replysubmit = new Command(replysubmit);
        }
        public async void Binddetailreviews(string reviewid,string businesid)
        {
            try
            {
                dialogs.ShowLoading("");
                var detailreviewdata = RestService.For<IComapanydata>(Commonsettings.LocaljobsAPI);
                var data = await detailreviewdata.GetheaderReview(reviewid, businesid);
                //var getreviewapi = RestService.For<IComapanydata>(Commonsettings.LocaljobsAPI);
                //var data = await getreviewapi.getreviewdetails(businessid);
                if (data.ROW_DATA.Count > 0)
                {
                    if (!string.IsNullOrEmpty(data.ROW_DATA[0].photourl))
                    {
                        Revimage = data.ROW_DATA[0].photourl.ToString();
                        Revimgvisible = true;
                        Revimgnovisible = false;
                    }
                    else
                    {
                        if(!string.IsNullOrEmpty(data.ROW_DATA[0].contributor))
                        {
                            Revtext = data.ROW_DATA[0].contributor.Substring(0, 1).ToUpper();
                            Revimgvisible = false;
                            Revimgnovisible = true;
                        }
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
            catch (Exception e)
            {

            }
        }
        public async void Bindreviewreply(string reviewid,string businesid)
        {
            try
            {
                //reviewid = "124"; sulekha us llc
                dialogs.ShowLoading("", null);
                var detailpagedata = RestService.For<IComapanydata>(Commonsettings.LocaljobsAPI);
                var data = await detailpagedata.GetAdReviewReplydtl(reviewid, businesid);
                if (data.ROW_DATA.Count > 0)
                {
                    foreach (var item in data.ROW_DATA)
                    {
                        if(!string.IsNullOrEmpty(item.contributor))
                        {
                            item.replytext = item.contributor.Substring(0, 1).ToUpper();
                        }
                    }
                    OnPropertyChanged(nameof(Reviewreply));
                    Reviewreply = data.ROW_DATA;
                    var CurrentPage = GetCurrentpage();
                    var listreviewreply = CurrentPage.FindByName<HVScrollGridView>("listreviewreply");
                    listreviewreply.ItemsSource = null;
                    listreviewreply.ItemsSource = Reviewreply;
                }

                dialogs.HideLoading();
            }
            catch (Exception e)
            {
                dialogs.HideLoading();
            }
        }

        public async Task replysubmit(string reviewid,string businessd)
        {
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                var currentpage = GetCurrentpage();
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
            }
            else
            {
                if(string.IsNullOrEmpty(replycomment))
                {
                    dialogs.Toast("Please enter your description...");
                }
                else
                {
                    var replyapi = RestService.For<IComapanydata>(Commonsettings.LocaljobsAPI);
                    var replyapidata = await replyapi.replytoreview(reviewid, businessid, Commonsettings.UserPid, replycomment, Commonsettings.UserName);
                    //returns 1 or 0
                    if (replyapidata != null)
                    {
                        var currentpage = GetCurrentpage();
                        //var editorlayout = currentpage.FindByName<Frame>("replyeditorlayout");
                        //editorlayout.IsVisible = false;
                        replycomment = "";
                        Bindreviewreply(reviewid, businessd);
                    }
                }
                
            }
        }
        public async Task ClickMorejobopenings(string data)
        {
            var currentpage = GetCurrentpage();
            await currentpage.Navigation.PushAsync(new Jobopeninglist(data));
        }
      
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page GetCurrentpage()
        {
            var Currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return Currentpage;
        }
    }
}
