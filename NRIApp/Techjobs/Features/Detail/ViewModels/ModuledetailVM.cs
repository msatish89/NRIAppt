using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Acr.UserDialogs;
using System.Windows.Input;
using NRIApp.Techjobs.Features.Detail.Models;
using Refit;
using NRIApp.Helpers;
using NRIApp.Techjobs.Features.Detail.Interfaces;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Linq;
using System.Net;
using System.IO;
using NRIApp.Techjobs.Features.LeadForm.Models;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Plugin.Share;

namespace NRIApp.Techjobs.Features.Detail.ViewModels
{
  
    public class ModuledetailVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;
        public List<Serviceslist> servicelist { get; set; }
        public List<Trainingdateslist> trainingdate { get; set; }
        public List<Videolist> videos { get; set; }
        public List<Bizmoduleslist> bizmodlist { get; set; }
        public List<Reviews> reviews { get; set; }

        public ICommand videoclickcommand { get; set; }
        public ICommand Contactcommand { get; set; }
        public ICommand Cityajaxcommand { get; set; }
        public ICommand Publishreviewcommand { get; set; }
        public ICommand clickratingcommand { get; set; }
        public ICommand Bizmoduleleadcommand { get; set; }
        public ICommand Opendetailscommand { get; set; }
        public ICommand Loadmoremodulescommand { get; set; }
        public ICommand ShareLinkcommand { get; set; }
        public ICommand Commentcommand { get; set; }
        public ICommand Verifyphonecommand { get; set; }
        public ICommand Verifyotpcommand { get; set; }
        string modid = "", businessid = "",module="",bizmoduleid="";
        public List<Bizmoduleslist> bizmodlist2 { get; set; }
        //  public List<Bizmoduleslist> bizmodlist3 { get; set; }

        public string _mobilenum = "";
        public string Mobilenum
        {
            get { return _mobilenum; }
            set
            {
                _mobilenum = value;
                    OnPropertyChanged();

            }
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
        private bool _isshow = false;
        public bool IsShow
        {
            get { return _isshow; }
            set
            {
                _isshow = value;
                OnPropertyChanged();
            }
        }

        private bool _phverify = false;
        public bool Phverify
        {
            get { return _phverify; }
            set
            {
                _phverify = value;
                OnPropertyChanged();
            }
        }
        private bool _otpblk = false;
        public bool Otpblk
        {
            get { return _otpblk; }
            set
            {
                _otpblk = value;
                OnPropertyChanged();
            }
        }

        private bool _reviewblk = true;
        public bool Reviewblk
        {
            get { return _reviewblk; }
            set
            {
                _reviewblk = value;
                OnPropertyChanged();
            }
        }
        private bool _sucessblk = false;
        public bool Sucessblk
        {
            get { return _sucessblk; }
            set
            {
                _sucessblk = value;
                OnPropertyChanged();
            }
        }
        private string _Selectcountry = "+1";
        public string Selectcountry
        {
            get { return _Selectcountry; }
            set { _Selectcountry = value; }
        }
        private bool _reviewvisible = false;
        public bool reviewvisible
        {
            get { return _reviewvisible; }
            set
            {
                _reviewvisible = value;
                OnPropertyChanged();
            }
        }
        public string _respid = "";
        public string Respid
        {
            get { return _respid; }
            set
            {
                _respid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Respid)));
            }
        }

        public string _otptext { get; set; }
        public string OTPText
        {
            get { return _otptext; }
            set
            {
                _otptext = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OTPText)));
            }
        }
        public string _otpno { get; set; }
        public string OTPNO
        {
            get { return _otpno; }
            set
            {
                _otpno = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OTPNO)));
            }
        }
        public string _Isfavoriteimg;
        public string Isfavoriteimg
        {
            get { return _Isfavoriteimg; }
            set { _Isfavoriteimg = value; }
        }
        public ICommand LoadHotelsCommand { get; set; }
        public ICommand RefreshItemsCommand { get; set; }
        public ICommand Favoritecommand { get; set; }
        public ModuledetailVM(string moduleid, string bizid)
        {
            dialog.ShowLoading("", null);
            modid = moduleid;
                businessid = bizid;
                Getservices(moduleid, bizid);
                Gettrainingdates(moduleid, bizid);
                Getmoduledetails(moduleid, bizid);
                Gettrainingvideo(moduleid, bizid);
               // Getbizmodules(bizid, "1");
                Getmodreviews(moduleid, bizid);
            Contactcommand = new Command(GetLeadform);
            videoclickcommand = new Command<Videolist>(videoclick);
            Cityajaxcommand = new Command<Cityajax>(Selectajaxcity);
            Publishreviewcommand = new Command(publishreview);
            clickratingcommand = new Command(clickrating);
            Bizmoduleleadcommand = new Command<BizModule>(GetbizLeadform);
            Opendetailscommand = new Command<Bizmoduleslist>(Opendetails);
            Loadmoremodulescommand = new Command(Getmorebizmodules);
            ShareLinkcommand = new Command(Sharelink);
            Commentcommand = new Command<Reviews>(Gotocomment);
            Verifyphonecommand = new Command(Verifymobile);
            Verifyotpcommand = new Command(Verifyotp);
            Favoritecommand = new Command(ClickFavoritecommand);
            items = new ObservableCollection<GroupModules>();
            Items = new ObservableCollection<GroupModules>();
            BindAdServices();
            LoadHotelsCommand = new Command(async () => await BindAdServices());
            RefreshItemsCommand = new Command<GroupModules>((Items2) => ExecuteRefreshItemsCommand(Items2));
        }
        private string _pagedesc = "";
        public string Pagedesc
        {
            get { return _pagedesc; }
            set { _pagedesc = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pagedesc)));
            }
        }

        private HtmlWebViewSource _questHtml;
        public HtmlWebViewSource QuestHtml
        {
            get
            {
                return _questHtml;
            }
            set
            {
                _questHtml = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuestHtml)));
            }
        }

        public bool _stackvideos =false;
        public bool Stackvideos
        {
            get { return _stackvideos; }
            set
            {
                _stackvideos = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Stackvideos)));
            }
        }
        public bool _trdatevisible = false;
        public bool trdatevisible
        {
            get { return _trdatevisible; }
            set
            {
                _trdatevisible = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(trdatevisible)));
            }
        }

        


        private string _pagetitle = "";
        public string Pagetitle
        {
            get { return _pagetitle; }
            set { _pagetitle = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pagetitle)));
            }
        }
        private string _trainingmode = "";
        public string Trainingmode
        {
            get { return _trainingmode; }
            set
            {
                _trainingmode = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Trainingmode)));
            }
        }
        private string _trainingdatetitle = "";
        public string Trainingdatetitle
        {
            get { return _trainingdatetitle; }
            set
            {
                _trainingdatetitle = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Trainingdatetitle)));
            }
        }

        private string _bizlink = "";
        public string Bizlink
        {
            get { return _bizlink; }
            set
            {
                _bizlink = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bizlink)));
            }
        }

        private string _videotitle = "";
        public string Videotitle
        {
            get { return _videotitle; }
            set
            {
                _videotitle = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Videotitle)));
            }
        }

        public void videoclick(Videolist vs)
        {
            Device.OpenUri(new Uri(vs.videourl));
        }
        string Isfavorite = "",Pageurl="",Courseid="";
        public async void Getmoduledetails(string moduleid, string bizid)
        {
            dialog.ShowLoading("", null);
            var nsAPI = RestService.For<IDetailservice>(Commonsettings.TechjobsAPI);
            ModuleDetaildata list = await nsAPI.Getmoduledetails(moduleid, bizid,Commonsettings.UserPid);
            Bizlink = "https://techjobs.sulekha.com/" + list.ROW_DATA.Last().moduleurl + "_training_in_" + list.ROW_DATA.Last().businesstitleurl + "_" + moduleid + "_" + bizid;
            Pageurl= "/"+list.ROW_DATA.Last().moduleurl + "_training_in_" + list.ROW_DATA.Last().businesstitleurl + "_" + moduleid + "_" + bizid;
            Pagedesc = list.ROW_DATA.Last().pagedescription;
            module = list.ROW_DATA.Last().module;
            Pagedesc = Pagedesc.Replace("&nbsp;", " ");
            System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex("<[^>]*>");
            Pagedesc = rx.Replace(Pagedesc, "");
            Pagetitle = list.ROW_DATA.Last().title;
            Courseid = list.ROW_DATA.Last().supertagid;
            Trainingdatetitle = "Upcoming " + list.ROW_DATA.Last().module + " training dates by " + list.ROW_DATA.Last().businessname;
            Videotitle = "Videos of " + list.ROW_DATA.Last().businessname;
            bizmoduleid = list.ROW_DATA.Last().businessmoduleid;
            if (list.ROW_DATA.Last().trainingmode == "1")
                Trainingmode = "Inclass - " + list.ROW_DATA.Last().businesscity;
            else if (list.ROW_DATA.Last().trainingmode == "3")
                Trainingmode = "Online, Inclass";
            else
                Trainingmode = "Online";

            Isfavorite = list.ROW_DATA.Last().isbookmarked;
            if (Isfavorite == "0")
            {
                Isfavoriteimg = "Favorite.png";
            }
            else
            {
                Isfavoriteimg = "FavoriteActive.png";
            }
            OnPropertyChanged(nameof(Isfavoriteimg));
            // QuestHtml= Getpagedesc(Pagedesc);
            //dialog.HideLoading();
        }
        public async void ClickFavoritecommand()
        {
            try
            {
                var CurrentPage = GetCurrentPage();
                if (Commonsettings.UserPid != "" && Commonsettings.UserPid != "0" && !string.IsNullOrEmpty(Commonsettings.UserPid))
                {
                    dialog.ShowLoading("");
                   
                        if (Isfavorite == "0")
                        {
                        var saved = RestService.For<NRIApp.Techjobs.Features.Detail.Interfaces.IDetailservice>(Commonsettings.TechjobsAPI);
                        NRIApp.Techjobs.Features.Detail.Models.Result data = await saved.Savefavorite(Commonsettings.UserPid, Pageurl,businessid,Courseid,modid, Pagetitle, "0");
                        if (data.ROW_DATA.Count > 0)
                        {
                            if (data.ROW_DATA[0].resulinformation == "inserted success")
                            {
                                Isfavoriteimg = "FavoriteActive.png";
                                Isfavorite = "1";
                            }
                        }
                        }
                        else
                        {
                        var deletead = RestService.For<NRIApp.Techjobs.Features.Detail.Interfaces.IDetailservice>(Commonsettings.TechjobsAPI);
                        NRIApp.Techjobs.Features.Detail.Models.Result data = await deletead.Deletefavorite(Commonsettings.UserPid, Pageurl);
                        if (data.ROW_DATA.Count > 0)
                        {
                            if (data.ROW_DATA[0].resulinformation == "deleted success")
                            {
                                Isfavoriteimg = "Favorite.png";
                                Isfavorite = "0";
                            }
                        }
                               
                        }
                  
                }
                else
                {
                    await CurrentPage.Navigation.PushModalAsync(new Signin.Views.Login());
                }
                OnPropertyChanged(nameof(Isfavoriteimg));
                dialog.HideLoading();
            }
            catch (Exception e)
            {
                dialog.HideLoading();
            }
        }
        public HtmlWebViewSource Getpagedesc(string Pagedesc)
        {
            var customCss = @"
        <style>
       body {
       font-size: 14px;
       color: #818080;
       height: auto;
    } 
 </style> ";
            var htmlSource = new HtmlWebViewSource();
            
            htmlSource.Html = customCss+Pagedesc;
         
            return htmlSource;
        }
        public async void Gettrainingvideo(string moduleid, string bizid)
        {
            dialog.ShowLoading("", null);
            var nsAPI = RestService.For<IDetailservice>(Commonsettings.TechjobsAPI);
            Videos list = await nsAPI.Gettrainingvideos(moduleid, bizid);
            OnPropertyChanged(nameof(videos));
            videos = list.ROW_DATA;
            if (videos.Count() >0)
                Stackvideos = true;
          //  dialog.HideLoading();
        }
        public async void Gettrainingdates(string moduleid, string bizid)
        {
            dialog.ShowLoading("", null);
            var nsAPI = RestService.For<IDetailservice>(Commonsettings.TechjobsAPI);
            Trainingdates list = await nsAPI.Gettrainingdates(moduleid,bizid);
            if(list.ROW_DATA.Count()>0 && list!=null)
            {
                OnPropertyChanged(nameof(trainingdate));
                trainingdate = list.ROW_DATA;
                trdatevisible = true;
            }
          
           // dialog.HideLoading();
        }
        public async void Getservices(string moduleid, string bizid)
        {
            dialog.ShowLoading("", null);
            var nsAPI = RestService.For<IDetailservice>(Commonsettings.TechjobsAPI);
            Services list = await nsAPI.Getservices(moduleid, bizid);
            OnPropertyChanged(nameof(servicelist));
            servicelist = list.ROW_DATA;
           // dialog.HideLoading();
        }
        public async void GetLeadform()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.LeadForm.Views.Respform(module, modid,businessid));
        }

        public void Display(string moduleid)
        {
            string url = "https://az827626.vo.msecnd.net/cdn/techjobshtml/module/"+bizmoduleid+".html";
            // htmlweb.Source = GetHtmlSource(url);
        }
        public List<Cityajax> _cityajax { get; set; }
        public List<Cityajax> CityAjax
        {
            get
            {
                return _cityajax;
            }
            set
            {
                _cityajax = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CityAjax)));
            }
        }
        public bool _cityvisible = false;
        public bool CityVisible
        {
            get { return _cityvisible; }
            set
            {
                    _cityvisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CityVisible)));

            }
        }
        public string _trcity { get; set; }
        public string Trainingcity
        {
            get { return _trcity; }
            set
            {

                if (_trcity != value)
                {
                    _trcity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Trainingcity)));
                    if (SelTrainingcity != _trcity && _trcity!="")
                        ajaxcity(_trcity);
                }

            }
        }

        public string _seltrcity { get; set; }
        public string SelTrainingcity
        {
            get { return _seltrcity; }
            set
            {

                if (_seltrcity != value)
                {
                    _seltrcity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelTrainingcity)));
                }

            }
        }


        public string _selcity { get; set; }
        public string Selcity
        {
            get { return _selcity; }
            set
            {

                if (_selcity != value)
                {
                    _selcity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selcity)));
                }

            }
        }

        public string _desc = "";       
        public string _descval = "Briefly explain your requirement";
        public string Descval
        {
            get { return _descval; }
            set
            {

                if (_descval != value)
                {
                    _descval = value;
                    _desc = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Descval)));
                }

            }
        }
        public async void ajaxcity(string city)
        {
            Selcity = "";
            var nsAPI = RestService.For<ILeadformService>(Commonsettings.TechjobsAPI);
            Cityajaxlist list = await nsAPI.Getcityajax(city,"");
            CityAjax = list.ROW_DS;
            if (CityAjax != null)
            {
                foreach (var i in list.ROW_DS)
                {
                    i.fullcity = i.city + ", " + i.statecode;
                }
                CityVisible = true;
            }
            else
                CityVisible = false;

        }

        public void Selectajaxcity(Cityajax model)
        {
            SelTrainingcity = model.city + ", " + model.statecode;
            Trainingcity = model.city + ", " + model.statecode;
           
            Selcity = model.citystatecodeurl;
            CityVisible = false;
        }
        public HtmlWebViewSource GetHtmlSource(string url)
        {
            var htmlSource = new HtmlWebViewSource();
            string htmlCode;
            string da = "";
            WebRequest request = HttpWebRequest.Create(url);
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    da = reader.ReadToEnd();
                }
            }


            // instruct the server to return headers only
            //request.Method = "HEAD";

            //// make the connection
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //// get the status code
            //HttpStatusCode status = response.StatusCode;
            using (WebClient client = new WebClient())
            {
                Stream stream = client.OpenRead(url);
                if (stream != null)
                    da = "kk";
                htmlCode = client.DownloadString(url);
            }
            var customCss = @"
              <body>
              <div>
        
        <style>
       .mywrapper, .wrapper, .mycss {
        width: 100% !important;
        height:100% !important;
        zoom:45%;
        padding:0,0,0,0;
        overflow-y: scroll;
    } 
    </div>
    </body>
        </style> ";
            htmlCode = htmlCode.Replace("<body>", customCss);

            htmlSource.Html = htmlCode;
            return htmlSource;
        }

        public async void publishreview()
        {
            var currentpage = GetCurrentPage();
            if(Commonsettings.UserPid=="0"|| Commonsettings.UserPid == null|| Commonsettings.UserPid == "")
            {
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
            }
            else
            {
                validatereview();
                Mobilenum = Commonsettings.UserMobileno;
                if (validate == true)
                {
                    dialog.ShowLoading(null);
                    var baseApi = RestService.For<IDetailservice>(Commonsettings.TechjobsAPI);
                    Addreview review = new Addreview()
                    {
                        name = Commonsettings.UserName,
                        emailid = Commonsettings.UserEmail,
                        userpid = Commonsettings.UserPid,
                        businessid = businessid,
                        moduleid = modid,
                        desc = _desc,
                        rating = Rating,
                        mobileno = Commonsettings.UserMobileno,
                        location = Trainingcity

                    };
                    Reviewresult res = await baseApi.Postreview(review);
                    dialog.HideLoading();
                    if (res.resultinformation != "already exists")
                    {
                        Reviewblk = false;
                        if(res.otpsend== "otpsend")
                        {
                            Phverify = true;
                            Respid = res.reviewid;
                        }
                        
                        else
                            Sucessblk = true;
                        //_desc = "";
                        //Rating = "";
                        //Selcity = "";
                        //Descval = "";
                        //Trainingcity = "";
                        //SelTrainingcity = "";
                        //_trcity = "";
                        //Ratingbar = "0/5";
                        //Rating1 = "Star_inactive.png";
                        //Rating2 = "Star_inactive.png";
                        //Rating3 = "Star_inactive.png";
                        //Rating4 = "Star_inactive.png";
                        //Rating5 = "Star_inactive.png";
                    }
                    else
                    {
                        Reviewblk = false;
                        Sucessblk = true;
                    }
                }
            }
        }
        public bool validate = true;
        public void validatereview()
        {
            validate = true;
               if (Rating == "")
            {
                validate = false;
                dialog.Toast("Please select your rating");
            }
            else if (string.IsNullOrEmpty(Selcity))
            {
                validate = false;
                dialog.Toast("Please select the city");
            }
            else if (_desc=="")
            {
                validate = false;
                dialog.Toast("Please enter the description");
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
        public string _rating = "";
        public string Rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Rating)));

            }
        }
        private void clickrating(object sender)
        {
            var view = sender as Xamarin.Forms.Image;
            
            string id = view.ClassId.ToString();
            if(id=="1")
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


        int page = 1;
        public async void Getbizmodules(string bizid, string pageno)
        {
            dialog.ShowLoading("", null);
            var nsAPI = RestService.For<IDetailservice>(Commonsettings.TechjobsAPI);
            Bizmodules list = await nsAPI.Getbizmodules(bizid, pageno);

            // OnPropertyChanged(nameof(bizmodlist));
           // bizmodlist2?.AddRange(list.ROW_DATA);
            bizmodlist2 = list.ROW_DATA;
            if (Convert.ToInt32(list.ROW_DATA.First().totalrecs) != list.ROW_DATA.Count)
            {
                page = bizmodlist2.Last().pageno + 1;
                IsBusy = true;
            }
            foreach (var p in list.ROW_DATA)
            {
                p.detailblkvisible = false;
                p.image = "plus.png";

                if (p.trainingmode == "3")
                {
                    p.onlinemode = "Online";

                    p.inclassmode = "Inclass - " + p.trainingcity;
                    p.Classroommodestack = true;
                    p.Onlinemodestack = true;
                }
                else if (p.trainingmode == "1")
                {
                    p.inclassmode = "Inclass - " + p.trainingcity;

                    p.Classroommodestack = true;
                    p.Onlinemodestack = false;
                }
                else
                {
                    p.onlinemode = "Online";
                    p.Classroommodestack = false;
                    p.Onlinemodestack = true;
                }


            }

            // bizmodlist2 = new ObservableCollection<Bizmoduleslist>(bizmodlist);
            //bizmodlist2 = new ObservableCollection<Bizmoduleslist>(bizmodlist as List<Bizmoduleslist>);
            var currentpage = GetCurrentPage();
            HVScrollGridView myEntry = currentpage.FindByName<HVScrollGridView>("listbizdata");
            myEntry.ItemsSource = bizmodlist2;
            // OnPropertyChanged(nameof(bizmodlist2));

            dialog.HideLoading();
        }

        public async void Getmorebizmodules()
        {
            IsShow = true;
            IsBusy = false;
            var nsAPI = RestService.For<IDetailservice>(Commonsettings.TechjobsAPI);
            Bizmodules list = await nsAPI.Getbizmodules(businessid, page.ToString());
           
                
            // OnPropertyChanged(nameof(bizmodlist));
            // bizmodlist2?.AddRange(list.ROW_DATA);
            // bizmodlist3 = list.ROW_DATA;
            foreach (var p in list.ROW_DATA)
            {
                p.detailblkvisible = false;
                p.image = "plus.png";

                if (p.trainingmode == "3")
                {
                    p.onlinemode = "Online";

                    p.inclassmode = "Inclass - " + p.trainingcity;
                    p.Classroommodestack = true;
                    p.Onlinemodestack = true;
                }
                else if (p.trainingmode == "1")
                {
                    p.inclassmode = "Inclass - " + p.trainingcity;

                    p.Classroommodestack = true;
                    p.Onlinemodestack = false;
                }
                else
                {
                    p.onlinemode = "Online";
                    p.Classroommodestack = false;
                    p.Onlinemodestack = true;
                }
                bizmodlist2?.Add(p);

            }
            IsShow = false;
            if (Convert.ToInt32(list.ROW_DATA.First().totalrecs) != bizmodlist2.Count)
            {
                page = list.ROW_DATA.Last().pageno + 1;
                IsBusy = true;
            }
            else
                IsBusy = false;
            // bizmodlist2 = new ObservableCollection<Bizmoduleslist>(bizmodlist);
            //bizmodlist2 = new ObservableCollection<Bizmoduleslist>(bizmodlist as List<Bizmoduleslist>);
            var currentpage = GetCurrentPage();
            HVScrollGridView myEntry = currentpage.FindByName<HVScrollGridView>("listbizdata");
            myEntry.ItemsSource = null;
            myEntry.ItemsSource = bizmodlist2;
            // OnPropertyChanged(nameof(bizmodlist2));
        }
        public void Opendetails(Bizmoduleslist list)
        {
          
             var currentpage = GetCurrentPage();
            HVScrollGridView myEntry = currentpage.FindByName<HVScrollGridView>("listbizdata");
           // var item = bizmodlist2.FirstOrDefault(i => i.moduleid == list.moduleid);
            foreach (var p in bizmodlist2)
            {
                if(p.moduleid!=list.moduleid)
                {
                    p.image = "plus.png";
                    p.detailblkvisible = false;
                }

                else
                {
                    if(p.detailblkvisible == true)
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
            myEntry.ItemsSource = bizmodlist2;
        }

        public async void Getmodreviews(string moduleid, string bizid)
        {
            dialog.ShowLoading("", null);
            var nsAPI = RestService.For<IDetailservice>(Commonsettings.TechjobsAPI);
            Getreviews list = await nsAPI.Getmodulereviews(moduleid, bizid);
            OnPropertyChanged(nameof(reviews));
            reviews = list.ROW_DATA;
            foreach(var i in reviews)
            {
               
                if (i.profilepicurl != "" && i.profilepicurl!=null)
                {
                    i.Profileimg = true;
                    i.Noimg = false;
                }
                    
                else
                {
                    i.profilename = i.name.Substring(0, 1).ToUpper();
                    i.Noimg = true;
                    i.Profileimg = false;
                }

            }
           // dialog.HideLoading();
            if (list.ROW_DATA.Count > 0)
                reviewvisible = true;
        }

        public void Sharelink()
        {
            CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
            {
                Text = "",
                Title = "",
                Url = Bizlink
            });
        }
        public async void Verifymobile()
        {
            if(Mobilenum!="" && Mobilenum!=null)
            {
                var nsAPI = RestService.For<IDetailservice>(Commonsettings.TechjobsAPI);
                OTPdetails details = await nsAPI.Verifyphone(Selectcountry, Mobilenum);
                OTPNO = details.otpno;
                Otpblk = true;
            }
            else
                dialog.Toast("Enter mobile number");

        }
        public async void Verifyotp()
        {
            if (OTPNO == OTPText)
            {
                var nsAPI = RestService.For<IDetailservice>(Commonsettings.TechjobsAPI);
                OTPdetails details = await nsAPI.Verification(Selectcountry, Mobilenum, Respid);
                if (details.resultinformation != "")
                {
                    Sucessblk = true;
                    Phverify = false;
                    Otpblk = false;
                }
            }
            else
                dialog.Toast("Enter valid otp");
           

        }
        public async void GetbizLeadform(BizModule list)
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.LeadForm.Views.Respform(list.Module, list.Moduleid, businessid));
        }
        public async void Gotocomment(Reviews rev)
        {
            var currentpage = GetCurrentPage();
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
            }
            else
            await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.Detail.Views.Comment(rev.testimonialid, businessid));
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


        private GroupModules _oldHotel;
       public int listviewheight;
        public int Listviewheight
        {
            get
            {
                return listviewheight;
            }
            set
            {
                listviewheight = value;
                OnPropertyChanged(nameof(Listviewheight));
            }
        }
        //public int Listviewheight { get; set; }
        List<GROUP_Bizmodules> listgrpdata = new List<GROUP_Bizmodules>();

        private ObservableCollection<GroupModules> items;
        public ObservableCollection<GroupModules> Items
        {
            get { return items; }
            set { items = value; }
        }
        public async System.Threading.Tasks.Task BindAdServices()
        {
            //string adid = LMD.adid;
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;
                dialog.ShowLoading("", null);
                Items.Clear();
                var nsAPI = RestService.For<IDetailservice>(Commonsettings.TechjobsAPI);
                Bizmodules lists = await nsAPI.Getbizmodules(businessid, "");
                var LSg = lists.ROW_DATA.GroupBy(m => m.course).ToList();
                GROUP_Bizmodules list;
                foreach (var item in LSg)
                {
                    list = new GROUP_Bizmodules()
                    {
                        Heading = item.Key,
                        Modules = item.ToList()

                    };
                    listgrpdata?.Add(list);




                }
                //}
                if (listgrpdata != null && listgrpdata.Count > 0)
                {
                    Listviewheight = (listgrpdata.Count * 40);
                    OnPropertyChanged(nameof(Listviewheight));

                    foreach (var hotel in listgrpdata)
                    {

                        Items.Add(new GroupModules(hotel));
                    }

                }
                dialog.HideLoading();

            }
            catch (Exception e)
            {
                dialog.HideLoading();
            }
        }

        public bool isExpanded = false;
        private void ExecuteRefreshItemsCommand(GroupModules HV)
        {
            if (_oldHotel == HV)
            {
                // click twice on the same item will hide it  
                HV.Expanded = !HV.Expanded;
                if (!HV.Expanded)
                {
                    if(listgrpdata.Count()<5)
                    Listviewheight = (listgrpdata.Count * 40) + (listgrpdata.Count * 25);
                    else
                        Listviewheight = (listgrpdata.Count * 40);
                    OnPropertyChanged(nameof(Listviewheight));
                }
                else
                {
                    if (listgrpdata.Count() < 5)
                        Listviewheight = ((HV.Count + listgrpdata.Count) * 40) + ((HV.Count + listgrpdata.Count) * 25);
                    else
                        Listviewheight = ((HV.Count + listgrpdata.Count) * 40)+ ((HV.Count + listgrpdata.Count)*2 );
                    OnPropertyChanged(nameof(Listviewheight));
                }

            }
            else
            {
                if (_oldHotel != null)
                {
                    // hide previous selected item  
                    _oldHotel.Expanded = false;
                    if (listgrpdata.Count() < 5)
                        Listviewheight = (listgrpdata.Count * 40) + (listgrpdata.Count * 25);
                    else
                        Listviewheight = (listgrpdata.Count * 40);
                    OnPropertyChanged(nameof(Listviewheight));
                }
                // show selected item  
                HV.Expanded = true;
                if (listgrpdata.Count() < 5)
                    Listviewheight = ((HV.Count + listgrpdata.Count) * 40) + ((HV.Count + listgrpdata.Count) * 25);
                  else
                        Listviewheight = ((HV.Count + listgrpdata.Count) * 40)+((HV.Count + listgrpdata.Count)*2);
                OnPropertyChanged(nameof(Listviewheight));

            }
            _oldHotel = HV;

        }

    }
}
