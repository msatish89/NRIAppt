using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using NRIApp.LocalService.Features.Interfaces;
using NRIApp.LocalService.Features.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using NRIApp.Helpers;
using Acr.UserDialogs;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net.Http.Headers;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;
using System.Net;
using Microsoft.WindowsAzure.Storage;
using System.Xml;
using System.Xml.Linq;
using NRIApp.Roommates.Features.Post.Interface;
using NRIApp.Controls;

namespace NRIApp.LocalService.Features.ViewModels
{
    public class LS_Posting_VM : INotifyPropertyChanged
    {
        IUserDialogs _Dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public string Postptagid;
        public string Postptag;
        public string Postsupertagid;
        public string Postsupertag;
        public string Postleaftagid;
        private string _Postleaftags { get; set; }
        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        string Namepattern = "^[0-9A-Za-z ]+$";
        public static string _Pinno;
        public static string _Businessid;
        public static string Contactnumber;
        public static string sCountrcode;string oLdclpostid = "0";
        private const string GooglePlacesUrl = "https://maps.googleapis.com/maps/api/place/autocomplete/json";

        private const string GoogleCityAndZipcode = "https://maps.googleapis.com/maps/api/place/details/xml";

        private string _NewContactnumber;
        public string NewContactnumber
        {
            get { return _NewContactnumber; }
            set
            {
                if (_NewContactnumber == null)
                {
                    _NewContactnumber = value.Trim();
                }
                else { _NewContactnumber = value; }
                OnPropertyChanged(nameof(NewContactnumber));
            }
        }
        private string _Newselectcontact = "+1";
        public string Newselectcontact
        {
            get { return _Newselectcontact; }
            set { _Newselectcontact = value; }
        }
        private string _Selectcontact = "+1";
        public string Selectcontact
        {
            get { return _Selectcontact; }
            set { _Selectcontact = value; }
        }
        public string Postleaftags
        {
            get { return _Postleaftags; }
            set
            {
                _Postleaftags = value;

                OnPropertyChanged(Postleaftags);
            }
        }
        public string _BusinessName = "";
        public string BusinessName
        {
            get { return _BusinessName; }
            set
            {
                if (_BusinessName == null)
                {
                    _BusinessName = value.Trim();
                }
                else { _BusinessName = value; }
                OnPropertyChanged(nameof(BusinessName));
            }
        }
        private string _BusinessEmail;
        public string BusinessEmail
        {
            get { return _BusinessEmail; }
            set
            {
                if (_BusinessEmail == null)
                {
                    _BusinessEmail = value.Trim();
                }
                else { _BusinessEmail = value; }
                OnPropertyChanged(nameof(BusinessEmail));
            }
        }
        public string _tempBusinessAddress = "";
        public string _tempBusinesscity = "";
        private string _BusinessAddress;
        public string BusinessAddress
        {
            get { return _BusinessAddress; }
            set
            {
                if (_BusinessAddress == null)
                {
                    _BusinessAddress = value.Trim();
                }
                else { _BusinessAddress = value; }




                OnPropertyChanged(nameof(BusinessAddress));
                if (string.IsNullOrEmpty(BusinessAddress))
                {
                    _tempBusinessAddress = "";
                }
                if (BusinessAddress != null && _tempBusinessAddress != BusinessAddress)
                {
                    GetgoogleAddress();
                }
            }
        }
        private string _ContactName;
        public string ContactName
        {
            get { return _ContactName; }
            set
            {
                if (_ContactName == null)
                {
                    _ContactName = value.Trim();
                }
                else { _ContactName = value; }
                OnPropertyChanged(nameof(ContactName));
            }
        }
        private string _Countrycode;
        public string Countrycode
        {
            get { return _Countrycode; }
            set { _Countrycode = value; }
        }
        private string _ContactNumber;
        public string ContactNumber
        {
            get { return _ContactNumber; }
            set
            {
                if (_ContactNumber == null)
                {
                    _ContactNumber = value.Trim();
                }
                else { _ContactNumber = value; }
                OnPropertyChanged(nameof(ContactNumber));
            }
        }
        public string _PostOTP { get; set; }

        public string PostOTP
        {
            get { return _PostOTP; }
            set { _PostOTP = value.Trim(); }
        }
        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }
        private bool _Otpblock = true;
        public bool Otpblock
        {
            get { return _Otpblock; }
            set
            {
                _Otpblock = value;
                OnPropertyChanged(nameof(Otpblock));
            }
        }
        private List<string> _PostCountrycode;
        public List<string> PostCountrycode
        {
            get { return _PostCountrycode; }
            set { _PostCountrycode = value; }
        }
        private List<Predictions> _googleadd;

        public List<Predictions> googleadd
        {
            get { return _googleadd; }
            set { _googleadd = value; }
        }

        List<string> Country = new List<string>() {

            "+1","+91"
        };

        private string _Bizdescription = "";
        public string Bizdescription
        {
            get { return _Bizdescription; }
            set
            {
                if (_Bizdescription == null)
                {
                    _Bizdescription = value.Trim();
                }
                else { _Bizdescription = value; }
                OnPropertyChanged(nameof(Bizdescription));
            }
        }
        private string _Businessexp = "Experience";
        public string Businessexp
        {
            get { return _Businessexp; }
            set
            {
                if (_Businessexp == null)
                {
                    _Businessexp = value.Trim();
                }
                else { _Businessexp = value; }
                OnPropertyChanged(nameof(Businessexp));
            }
        }
        private string _Businessexptxtcolor = "#c0c0c0";
        public string Businessexptxtcolor
        {
            get { return _Businessexptxtcolor; }
            set
            {
                if (_Businessexptxtcolor == null)
                {
                    _Businessexptxtcolor = value.Trim();
                }
                else { _Businessexptxtcolor = value; }
                OnPropertyChanged(nameof(Businessexptxtcolor));
            }
        }
        private string _Profileimg = "";
        public string Profileimg
        {
            get { return _Profileimg; }
            set
            {
                if (_Profileimg == null)
                {
                    _Profileimg = value.Trim();
                }
                else { _Profileimg = value; }
                OnPropertyChanged(nameof(Profileimg));
            }
        }
        private bool _Choosephotocontentviewvisible = false;
        public bool Choosephotocontentviewvisible
        {
            get { return _Choosephotocontentviewvisible; }
            set
            {
                _Choosephotocontentviewvisible = value;
                OnPropertyChanged(nameof(Choosephotocontentviewvisible));
            }
        }



        private Command _Backbtncommand;
        public Command Backbtncommand
        {
            get { return _Backbtncommand; }
            set { _Backbtncommand = value; }
        }

        List<EXPERIENCE_DATA> experincelist = new List<EXPERIENCE_DATA>();
        private Plugin.Media.Abstractions.MediaFile _mediaFile;
        public Command PostSubmit { get; set; }
        public Command SubmitPostotp { get; set; }
        public Command ResendOTP { get; set; }
        public Command ChangemobileNumber { get; set; }
        public Command SubmitChangeMobile { get; set; }
        public Command Selectgoogleaddress { get; set; }
        public Command Choosefilecommand { get; set; }
        public Command Chooseexperince { get; set; }
        public Command Removeprofileimg { get; set; }
        public Command Opentypepicture { get; set; }
        public Command CloseContentViewTap { get; set; }
        public Command Callotp { get; set; }
        public Command takeprofilephoto { get; set; }



        public async Task BackbtncommandClick()
        {
            try { 
            await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ee)
            {

            }
        }
        Business lsp = new Business();
        public LS_Posting_VM(string stagid, string stag, string ptag, string ptagid, string checkvalue, string checktext,string oldclpostid="")
        {
            string ltags = "", ltagid = "";
            Postsupertagid = stagid;
            Postsupertag = stag;
            Postptagid = ptagid;
            Postptag = ptag;
            ltags = checktext;
            ltagid = checkvalue;
            if (!string.IsNullOrEmpty(oldclpostid))
            {
                oLdclpostid = oldclpostid;
            }
            
            if (ltags != "")
            {
                ltags = ltags.Remove(ltags.Length - 1, 1);
            }
            if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                BusinessEmail = Commonsettings.UserEmail;
            if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                ContactName = Commonsettings.UserName;
            if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                ContactNumber = Commonsettings.UserMobileno;

            Postleaftags = ltags;
            Postleaftagid = ltagid;
            PostSubmit = new Command(onPostSubmit);
            Backbtncommand = new Command(async () => await BackbtncommandClick());
            Selectgoogleaddress = new Command<Predictions>(OnSelectgoogleaddress);
            _PostCountrycode = Country;
            OnPropertyChanged(nameof(PostCountrycode));
            Choosefilecommand = new Command(chooseprofile);
            Chooseexperince = new Command<EXPERIENCE_DATA>(Selectexperince);
            Removeprofileimg = new Command(Removeprofimg);
            Opentypepicture = new Command(Openselectpictype);
            CloseContentViewTap = new Command(Closecontentview);
            takeprofilephoto = new Command(takephoto);
            Bindexperince();
            if (LS_ViewModel.ispartialclass == 1 && !string.IsNullOrEmpty(oldclpostid))
            {

                Bindpartialaddetails(oldclpostid);

                Addpartialadcategory(oldclpostid);
            }
            if (LS_ViewModel.islseditable == 1)
            {
                Bindpartialaddetails(oldclpostid);
                Bindeditabledata();
               
            }


        }


        public LS_Posting_VM(Business llf)
        {
            SubmitPostotp = new Command(Submitingpostotp1);
            ResendOTP = new Command(PostingresndOTP);
            ChangemobileNumber = new Command(OnChangeMObile);
            SubmitChangeMobile = new Command(OnSubmitChangeMobile);
            Callotp = new Command(Sendcallotp);
            lsp.businessAddress = llf.businessAddress;
            lsp.businessEmail = llf.businessEmail;
            lsp.businessname = llf.businessname;
            lsp.contactName = llf.contactName;
            lsp.contactNumber = llf.contactNumber;
            lsp.countrycode = llf.countrycode;
            lsp.userpid = llf.userpid;
            lsp.useremail = llf.useremail;
            lsp.username = llf.username;
            lsp.leaftags = llf.leaftags;
            lsp.ptagid = llf.ptagid;
            lsp.ptag = llf.ptag;
            lsp.supertag = llf.supertag;
            lsp.supertagid = llf.supertagid;
            lsp.bizlatitude = llf.bizlatitude;
            lsp.bizlongitude = llf.bizlongitude;
            lsp.bizcity = llf.bizcity;
            lsp.bizcountry = llf.bizcountry;
            lsp.bizstate = llf.bizstate;
            lsp.bizzipcode = llf.bizzipcode;
            lsp.bizcountrycode = llf.bizcountrycode;
            lsp.profimgurl = llf.profimgurl;
            lsp.businessexp = llf.businessexp;
            lsp.businessdesc = llf.businessdesc;
        }


        public async void Bindeditabledata()
        {
            try
            {
                _Dialog.ShowLoading("");
                await Task.Delay(2000);
                var CurrentPage = LS_ViewModel.GetCurrentPage();
                var email = CurrentPage.FindByName<CustomEntry>("ubizemail");
                email.IsEnabled = false;

                var mobile = CurrentPage.FindByName<CustomEntry>("umobile");
                mobile.IsEnabled = false;
                _Dialog.HideLoading();
            }
            catch(Exception e)
            {

            }
        }
        public async void Addpartialadcategory(string oldclpostid)
        {
            try
            {
                Business lsp = new Business();
                lsp.oldclpostid = oldclpostid;
                lsp.supertagid = Postsupertagid;
                lsp.supertag = Postsupertag;
                lsp.ptag = Postptag;
                lsp.ptagid = Postptagid;
                lsp.leaftags = Postleaftags;
                lsp.userpid = Commonsettings.UserPid;
                
                var partialbusiness = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);
                var result = await partialbusiness.Addpartialadcategory(lsp);
                if (result != null &&!string.IsNullOrEmpty(result.resultinformation ))
                { 
                
                }
                }catch(Exception e)
            {

            }
        }
        public async void Bindpartialaddetails(string oldclpostid)
        {
            try
            {
                var partialbusiness = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);
                var result = await partialbusiness.GetPartialaddetails(oldclpostid);
                if(result != null &&result.ROW_DATA.Count>0)
                {
                    var CurrentPage = LS_ViewModel.GetCurrentPage();
                    var bizname = CurrentPage.FindByName<Entry>("ubizname");
                    bizname.Text = result.ROW_DATA[0].title;

                    var streetname = CurrentPage.FindByName<Entry>("uaddress");
                    BusinessAddress = _tempBusinessAddress =  streetname.Text = result.ROW_DATA[0].streetname;

                    Businessexp = "";

                  string  countryvalue = result.ROW_DATA[0].countrycode;
                    var selcountrycode = CurrentPage.FindByName<CustomPicker>("selcountrycode");
                    if(countryvalue=="91" || countryvalue == "+91")
                    {
                        selcountrycode.SelectedIndex = 1;
                    }
                    else
                    {
                        selcountrycode.SelectedIndex = 0;
                    }
                    if (result.ROW_DATA[0].mobileno != Commonsettings.UserMobileno)
                    {
                        ContactNumber = result.ROW_DATA[0].mobileno;
                    }
                    if (result.ROW_DATA[0].email != Commonsettings.UserEmail)
                    {
                        BusinessEmail = result.ROW_DATA[0].email;
                    }

                    Businessexp = result.ROW_DATA[0].industryexp;
                    OnPropertyChanged(nameof(Businessexp));
                    Businessexptxtcolor = "#000000";
                    var description = CurrentPage.FindByName<CustomEditor>("udescription");
                    description.Text = result.ROW_DATA[0].shortdesc;

                    var profileimg = result.ROW_DATA[0].profileimgurl;
                    if (!string.IsNullOrEmpty(profileimg))
                    {
                        var imgstack = CurrentPage.FindByName<StackLayout>("stackprfimg");
                        imgstack.IsVisible = true;
                        Profileimg = profileimg;
                        OnPropertyChanged(nameof(profileimg));
                    }
                    var email = CurrentPage.FindByName<CustomEntry>("ubizemail");
                    email.IsEnabled = false;


                    await Task.Delay(1000);
                    OnPropertyChanged(nameof(IsBusy));
                    _isBusy = false;


                    OnPropertyChanged(nameof(googleadd));
                    _googleadd = null;
                }

            }
            catch(Exception e)
            {

            }
        }

        public async void Sendcallotp()
        {
            try
            {
                _Dialog.ShowLoading("");

                var resendotp = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);

                var data = await resendotp.Getcallotp(ViewModels.LS_Posting_VM.sCountrcode, ViewModels.LS_Posting_VM.Contactnumber);

                if (!string.IsNullOrEmpty(data.result)&&data.pinno !="0000" && data.pinno !=null)
                {
                    _Pinno = data.pinno;

                    _Dialog.Toast("OTP has been sent to your mobile number");
                }
                else
                {
                    _Dialog.Toast("Please try again later!");
                }


                _Dialog.HideLoading();
            }
            catch (Exception e)
            {

            }
        }

        public void Openselectpictype()
        {
            Choosephotocontentviewvisible = true;
        }
        public void Closecontentview()
        {
            Choosephotocontentviewvisible = false;
        }
        public void Removeprofimg()
        {
            Profileimg = "";
            profileimgurl = "";
            var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();
            var path = CurrentPage.FindByName<StackLayout>("stackprfimg");
            path.IsVisible = false;
        }
        public void Selectexperince(EXPERIENCE_DATA ED)
        {
            foreach (var item in experincelist)
            {
                if (item.id == ED.id)
                {
                    item.Expimg = "RadioButtonChecked.png";
                    Businessexp = sBusinessexp = item.content;
                    Businessexptxtcolor = "#000000";
                }
                else
                {
                    item.Expimg = "RadioButtonUnChecked.png";
                }
            }
            var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();
            var expdata = CurrentPage.FindByName<ListView>("explistdata");
            expdata.ItemsSource = null;
            expdata.ItemsSource = experincelist;
            var contentview = CurrentPage.FindByName<StackLayout>("popupexperience");
            contentview.IsVisible = false;
        }
        public async void Bindexperince()
        {
            experincelist.Add(new EXPERIENCE_DATA { id = "1", content = "0-5 years", Expimg = "RadioButtonUnChecked.png" });
            experincelist.Add(new EXPERIENCE_DATA { id = "2", content = "5-15 years", Expimg = "RadioButtonUnChecked.png" });
            experincelist.Add(new EXPERIENCE_DATA { id = "3", content = "Above 15 years", Expimg = "RadioButtonUnChecked.png" });
            await Task.Delay(2000);
            var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();
            var expdata = CurrentPage.FindByName<ListView>("explistdata");
            expdata.ItemsSource = experincelist;

        }
        string extension = "", profileimgurl = "";
        public async void chooseprofile()
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    _Dialog.Toast("This is not support on your device.");
                    return;
                }
                else
                {
                    string readerdata = ""; Stream imgstream = null;
                    var mediaOption = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };
                    _mediaFile = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions()).ConfigureAwait(true);
                    if (_mediaFile == null) return;
                    var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();
                    //  var imgpath = CurrentPage.FindByName<Label>("Localpath");
                    //imgpath.Text=  _mediaFile.Path;
                    var memoryStream = new MemoryStream();
                    Stream stream = null;
                    stream = _mediaFile.GetStream();
                    stream.CopyTo(memoryStream);
                    var base64 = Convert.ToBase64String(memoryStream.ToArray());
                    byte[] myBynary = memoryStream.ToArray();
                    Random rnd = new Random(); int Randomnumber = rnd.Next(1, 1000);
                    string integerValue = Randomnumber.ToString();
                    var name = "localservices" + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-ss-mm-fff") + "_" + integerValue + ".jpg";

                    Choosephotocontentviewvisible = false;



                    //smallimage image
                    byte[] smallimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 150, 150);
                    MemoryStream smallimagestrm = new MemoryStream(smallimage);

                    profileimgurl = await CommonMethods.Uploadimagestoblob("cdn/localservices/images/smallimage", name, smallimagestrm);
                    smallimagestrm.Dispose();

                    Profileimg = profileimgurl;

                    var path = CurrentPage.FindByName<StackLayout>("stackprfimg");
                    path.IsVisible = true;

                    //large image
                    byte[] large = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 346, 250);
                    MemoryStream largeimagestrm = new MemoryStream(large);
                    await CommonMethods.Uploadimagestoblob("cdn/localservices/images/large", name, largeimagestrm);
                    largeimagestrm.Dispose();

                    //small image
                    byte[] verysmallimg = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 106, 80);
                    MemoryStream verysmallimgstrm = new MemoryStream(verysmallimg);
                    await CommonMethods.Uploadimagestoblob("cdn/localservices/images/small", name, verysmallimgstrm);
                    verysmallimgstrm.Dispose();

                    //thumbnail image
                    byte[] thumbnailimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 226, 170);
                    MemoryStream thumbnailimagestrm = new MemoryStream(thumbnailimage);
                    await CommonMethods.Uploadimagestoblob("cdn/localservices/images/thumbnail", name, thumbnailimagestrm);
                    thumbnailimagestrm.Dispose();

                    //thumbnailfull image
                    byte[] thumbnailfullimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 770, 577);
                    MemoryStream thumbnailfullimagestrm = new MemoryStream(thumbnailfullimage);
                    await CommonMethods.Uploadimagestoblob("cdn/localservices/images/thumbnailfull", name, thumbnailfullimagestrm);
                    thumbnailfullimagestrm.Dispose();

                    //thumbnailmedium image
                    byte[] thumbnailmediumimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 400, 260);
                    MemoryStream thumbnailmediumimagestrm = new MemoryStream(thumbnailmediumimage);
                    await CommonMethods.Uploadimagestoblob("cdn/localservices/images/thumbnailmedium", name, thumbnailmediumimagestrm);
                    thumbnailmediumimagestrm.Dispose();

                    //thumbs image
                    byte[] thumbsimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 80, 60);
                    MemoryStream thumbsimagestrm = new MemoryStream(thumbsimage);
                    await CommonMethods.Uploadimagestoblob("cdn/localservices/images/thumbs", name, thumbsimagestrm);
                    thumbsimagestrm.Dispose();
                    //}



                }
            }
            catch (Exception ee)
            {

            }

        }
        public async void takephoto()
        {
            //reference https://github.com/jamesmontemagno/MediaPlugin
            try
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    var answer = await _Dialog.ConfirmAsync("No Camera", " No camera avaialble.", "OK", "");
                   
                    return;
                }
                else
                {

                    string readerdata = ""; Stream imgstream = null;
                    var mediaOption = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };
                    //_mediaFile = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions()).ConfigureAwait(true);

                    _mediaFile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                        Directory = "Sample",
                        Name = "test.jpg",
                        AllowCropping = true,


                    });
                    if (_mediaFile == null) return;
                    var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();
                    //  var imgpath = CurrentPage.FindByName<Label>("Localpath");
                    //imgpath.Text=  _mediaFile.Path;
                    var memoryStream = new MemoryStream();
                    Stream stream = null;
                    stream = _mediaFile.GetStream();
                    stream.CopyTo(memoryStream);
                    var base64 = Convert.ToBase64String(memoryStream.ToArray());
                    byte[] myBynary = memoryStream.ToArray();
                    Random rnd = new Random(); int Randomnumber = rnd.Next(1, 1000);
                    string integerValue = Randomnumber.ToString();
                    var name = "localservices" + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-ss-mm-fff") + "_" + integerValue + ".jpg";


                    Choosephotocontentviewvisible = false;

                   
                    //smallimage image
                    byte[] smallimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 150, 150);
                    MemoryStream smallimagestrm = new MemoryStream(smallimage);
                    // ms.Seek(0, SeekOrigin.Begin);
                    // ms.Write(smallimage, 0, smallimage.Length);
                    profileimgurl = await CommonMethods.Uploadimagestoblob("cdn/localservices/images/smallimage", name, smallimagestrm);
                    smallimagestrm.Dispose();

                    Profileimg = profileimgurl;

                    var path = CurrentPage.FindByName<StackLayout>("stackprfimg");
                    path.IsVisible = true;

                    //large image
                    byte[] large = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 346, 250);
                    MemoryStream largeimagestrm = new MemoryStream(large);
                    await CommonMethods.Uploadimagestoblob("cdn/localservices/images/large", name, largeimagestrm);
                    largeimagestrm.Dispose();

                    //small image
                    byte[] verysmallimg = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 106, 80);
                    MemoryStream verysmallimgstrm = new MemoryStream(verysmallimg);
                    await CommonMethods.Uploadimagestoblob("cdn/localservices/images/small", name, verysmallimgstrm);
                    verysmallimgstrm.Dispose();

                    //thumbnail image
                    byte[] thumbnailimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 226, 170);
                    MemoryStream thumbnailimagestrm = new MemoryStream(thumbnailimage);
                    await CommonMethods.Uploadimagestoblob("cdn/localservices/images/thumbnail", name, thumbnailimagestrm);
                    thumbnailimagestrm.Dispose();

                    //thumbnailfull image
                    byte[] thumbnailfullimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 770, 577);
                    MemoryStream thumbnailfullimagestrm = new MemoryStream(thumbnailfullimage);
                    await CommonMethods.Uploadimagestoblob("cdn/localservices/images/thumbnailfull", name, thumbnailfullimagestrm);
                    thumbnailfullimagestrm.Dispose();

                    //thumbnailmedium image
                    byte[] thumbnailmediumimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 400, 260);
                    MemoryStream thumbnailmediumimagestrm = new MemoryStream(thumbnailmediumimage);
                    await CommonMethods.Uploadimagestoblob("cdn/localservices/images/thumbnailmedium", name, thumbnailmediumimagestrm);
                    thumbnailmediumimagestrm.Dispose();

                    //thumbs image
                    byte[] thumbsimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 80, 60);
                    MemoryStream thumbsimagestrm = new MemoryStream(thumbsimage);
                    await CommonMethods.Uploadimagestoblob("cdn/localservices/images/thumbs", name, thumbsimagestrm);
                    thumbsimagestrm.Dispose();
                    //}


                }
            }
            catch (Exception ex)
            {
                _Dialog.HideLoading();
                _Dialog.Toast("Unable to save the file...Please try again later...");
            }
        }
        public async void Submitingpostotp()
        {
            if (!string.IsNullOrEmpty(PostOTP))
            {
                _Dialog.ShowLoading("", null);
                var postotp = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);

                var data = await postotp.Verifyotp(PostOTP);

                if (data.result == "success")
                {
                    string devicetypeid = "1";
                    if (Commonsettings.UserMobileOS.ToLower().Contains("android"))
                    {
                        devicetypeid = "2";
                    }
                    lsp.mobileverify = "0";
                    var postbusiness = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);
                    var result = await postbusiness.SubmitBusiness(lsp, Commonsettings.UserDeviceId, devicetypeid, Commonsettings.UserMobileOS);

                    var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
                    try
                    {
                        if (result != null)
                        {

                            Postadid = result.contentid;
                            if (result.ispayment == "1")
                            {
                                await curpage.Navigation.PushAsync(new Views.Posting.PrimePakage(Postadid, ""));
                                LS_ViewModel.Adid = "";
                                LS_ViewModel.Adid = Postadid;
                            }
                            else
                            {
                                await curpage.Navigation.PushAsync(new Views.Posting.LS_POST_Thankyou());
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }

                }
                else
                {
                    _Dialog.Toast("Enter valid OTP");
                }

                _Dialog.HideLoading();
            }
            else
            {
                _Dialog.Toast("Enter OTP");
            }



        }


        public async void Submitingpostotp1()
        {
            if (PostOTP == _Pinno)
            {
                try
                {
                    _Dialog.ShowLoading("", null);
                    //var postbusiness = RestService.For<IServiceAPI>(Commonsettings.Localservices);
                    //await postbusiness.UpdatePostMobileverified(_Businessid, "1");
                    //var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
                    //await curpage.Navigation.PushAsync(new Views.Posting.LS_POST_Thankyou());

                    string devicetypeid = "1";
                    if (Commonsettings.UserMobileOS.ToLower().Contains("android"))
                    {
                        devicetypeid = "2";
                    }
                    lsp.mobileverify = "0";
                    var postbusiness = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);
                    var result = await postbusiness.SubmitBusiness(lsp, Commonsettings.UserDeviceId, devicetypeid, Commonsettings.UserMobileOS);

                    var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
                    try
                    {
                        if (result != null)
                        {

                            Postadid = result.contentid;
                            if (result.ispayment == "1")
                            {
                                await curpage.Navigation.PushAsync(new Views.Posting.PrimePakage(Postadid, ""));
                                LS_ViewModel.Adid = "";
                                LS_ViewModel.Adid = Postadid;
                            }
                            else
                            {
                                await curpage.Navigation.PushAsync(new Views.Posting.LS_POST_Thankyou());
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }
                    _Dialog.HideLoading();
                }
                catch (Exception e)
                {
                    _Dialog.HideLoading();
                }
            }
            else
            {
                _Dialog.Toast("Enter valid OTP");
            }
        }
        public async void PostingresndOTP()
        {
            try
            {
                _Dialog.ShowLoading();

                var resendotp = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);

                var data = await resendotp.OTPResnd(Contactnumber, Selectcontact);

                _Pinno = data.pinno;
                _Dialog.Toast("OTP has been sent to your mobile number");
                _Dialog.HideLoading();

            }
            catch (Exception e)
            {

            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        bool CheckValidPhone(string inputPhone)
        {
            bool bPhnumValid = false;
            string firstchar = inputPhone.Substring(0, 1);
            if (inputPhone.Length > 6)
            {
                for (var iphnum = 0; iphnum < 6; iphnum++)
                {
                    string chara = inputPhone[iphnum].ToString();
                    if (chara != firstchar)
                        bPhnumValid = true;

                }
            }
            return bPhnumValid;
        }
        public bool Validations()
        {
            var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();
            bool result = true;
            if (string.IsNullOrEmpty(ContactName) || !Regex.IsMatch(ContactName, Namepattern))
            {
                _Dialog.Toast("Enter ContactName");
                var entry = CurrentPage.FindByName<Entry>("ucontactname");
                entry.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(BusinessName))
            {
                _Dialog.Toast("Enter BusinessName");
                var entry = CurrentPage.FindByName<Entry>("ubizname");
                entry.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(BusinessEmail))
            {
                _Dialog.Toast("Enter BusinessEmail");
                var entry = CurrentPage.FindByName<Entry>("ubizemail");
                entry.Focus();
                return false;
            }
            if (!Regex.IsMatch(BusinessEmail, Emailpattern))
            {
                _Dialog.Toast("Enter Proper Email");
                var entry = CurrentPage.FindByName<Entry>("ubizemail");
                entry.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(ContactNumber))
            {
                _Dialog.Toast("Enter Contact Number");
                var entry = CurrentPage.FindByName<Entry>("umobile");
                entry.Focus();
                return false;
            }
            else if (ContactNumber.Length < 10)
            {

                Entry myEntry = CurrentPage.FindByName<Entry>("umobile");
                myEntry.Focus();
                _Dialog.Toast("Please enter a 10-digit mobile number");
                return false;
            }
            else if (!CheckValidPhone(ContactNumber))
            {
                Entry myEntry = CurrentPage.FindByName<Entry>("umobile");
                myEntry.Focus();
                _Dialog.Toast("Please enter valid mobile number");
                return false;
            }
            if (string.IsNullOrEmpty(_tempBusinessAddress) || string.IsNullOrEmpty(BusinessAddress))
            {
                _Dialog.Toast("Enter Business Address");
                var entry = CurrentPage.FindByName<Entry>("uaddress");
                entry.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(Bizdescription) || Bizdescription.Length < 90)
            {
                _Dialog.Toast("Enter Business Description minimum 90 charcters");
                var entry = CurrentPage.FindByName<Editor>("udescription");
                entry.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(sBusinessexp) || string.IsNullOrEmpty(Businessexp))
            {
                _Dialog.Toast("Please Choose Business Experience");
                var entry = CurrentPage.FindByName<Editor>("uexperince");
                entry.Focus();
                return false;
            }

            return result;
        }


        string Latitude = string.Empty, Longitude = string.Empty, City = string.Empty,
               gCountry = string.Empty, State = string.Empty, BizCountryCode = string.Empty, ZipCode = string.Empty,
            Postadid = string.Empty, sBusinessexp = string.Empty;
        public static string sBusinessname = "";
        public async void onPostSubmit()
        {
            //  var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
            // sBusinessname = BusinessName;
            //  await curpage.Navigation.PushAsync(new Views.Posting.PrimePakage("10240994", ""));
            string bname = BusinessName, bemail = BusinessEmail, baddress = BusinessAddress, bcontactname = ContactName,
                   bcontact = ContactNumber, bcountry = Countrycode, _bcountry = Selectcontact;
            try
            {
                if (Validations())
                {


                    _Dialog.ShowLoading("");
                    if (string.IsNullOrEmpty(Commonsettings.UserPid) || Commonsettings.UserPid == "0")
                    {
                        var login = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                        var logindata = await login.getuserpid(ContactName, BusinessEmail);
                        Commonsettings.UserPid = logindata.resultinformation;
                        Commonsettings.UserName = ContactName;
                        Commonsettings.UserEmail = BusinessEmail;
                        Commonsettings.UserMobileno = ContactNumber;
                    }
                    string devicetypeid = "1";
                    if (Commonsettings.UserMobileOS.ToLower().Contains("android"))
                    {
                        devicetypeid = "2";
                    }
                    string sPostedviaid = "";
                    if (Commonsettings.UserMobileOS.ToLower().Contains("iphone"))
                    {
                        sPostedviaid = "194";
                    }
                    else
                    {
                        sPostedviaid = "197";
                    }
                    sBusinessname = BusinessName;

                    var postbusiness = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);
                    Business bz = new Business()
                    {
                        businessAddress = BusinessAddress,
                        businessEmail = BusinessEmail,
                        businessname = BusinessName,
                        contactName = ContactName,
                        contactNumber = ContactNumber,
                        countrycode = Selectcontact,
                        leaftags = Postleaftags,
                        ptagid = Postptagid,
                        ptag = Postptag,
                        supertag = Postsupertag,
                        supertagid = Postsupertagid,
                        bizlatitude = Latitude,
                        bizlongitude = Longitude,
                        bizcity = City,
                        bizcountry = gCountry,
                        bizstate = State,
                        bizzipcode = ZipCode,
                        bizcountrycode = BizCountryCode,
                        profimgurl = profileimgurl,
                        mobileverify = "1",
                        businessexp = sBusinessexp,
                        userpid = Commonsettings.UserPid,
                        useremail = Commonsettings.UserEmail,
                        username = Commonsettings.UserName,
                        businessdesc = Bizdescription,
                        postedvia = Commonsettings.UserMobileOS.ToLower(),
                        postedviaid = sPostedviaid,
                        oldclpostid=oLdclpostid

                    };


                    BUSINESS_RESULT_DATA result = await postbusiness.SubmitBusiness(bz, Commonsettings.UserDeviceId, devicetypeid, Commonsettings.UserMobileOS);

                    var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
                    if (result != null)
                    {

                        LS_ViewModel.packageamount.Clear();
                        LS_ViewModel.packagevalueamountdetails.Clear();
                        LS_ViewModel.addionalcities.Clear();
                        LS_ViewModel.valueadded = 0;
                        LS_ViewModel.Valueaddedcontentid = "";
                        LS_ViewModel.Bizcity = City + "," + State;

                        if (LS_ViewModel.islseditable == 1)
                        {
                            string primeid = "";

                            LS_GET_PRIME_ID lt = await postbusiness.Getprimeid(oLdclpostid);
                            if (lt.ROW_DATA.Count > 0 && lt.ROW_DATA != null)
                            {
                                primeid = lt.ROW_DATA.First().primeid;
                                await curpage.Navigation.PushAsync(new Views.Posting.LS_POST_Thankyou(oLdclpostid, primeid));
                            }
                        }
                        else
                        {

                            if (result.type == "0" && ContactNumber != "9043674236")
                            {

                                await curpage.Navigation.PushAsync(new Views.Posting.LS_POST_OTP(bz, result.contentid, result.pinno, lsp.contactNumber));
                            }
                            else if (result.ispayment == "1" && result.contentid != "")
                            // else if (ContactNumber == "9043674238")
                            {
                                //  LS_ViewModel.Bizcity = City + "," + State;
                                await curpage.Navigation.PushAsync(new Views.Posting.PrimePakage(result.contentid, ""));

                                LS_ViewModel.Adid = "";
                                LS_ViewModel.Adid = result.contentid;
                            }
                            else
                            {
                                await curpage.Navigation.PushAsync(new Views.Posting.LS_POST_Thankyou());
                            }
                        }
                    }
                }
                _Dialog.HideLoading();

            }
            catch (Exception e)
            {

            }


        }

        public async void OnChangeMObile()
        {
            _isBusy = true;
            OnPropertyChanged(nameof(IsBusy));
            _Otpblock = false; OnPropertyChanged(nameof(Otpblock));

        }

        public async void OnSubmitChangeMobile()
        {
            try
            {
                if (NewContactnumber != null && NewContactnumber != "" && NewContactnumber.Length >= 10)
                {


                    _Dialog.ShowLoading("", null);
                    var resendotp = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);
                    ViewModels.LS_Posting_VM.sCountrcode = Newselectcontact;
                    ViewModels.LS_Posting_VM.Contactnumber = NewContactnumber;
                    var data = await resendotp.OTPResnd(NewContactnumber, Newselectcontact);
                   // var updatecontact = RestService.For<IServiceAPI>(Commonsettings.Localservices);
                   // await updatecontact.UpdateMobilenumber(_Businessid, NewContactnumber, Newselectcontact.Replace("+", ""), Commonsettings.UserDeviceId);
                    _Pinno = data.pinno;
                    _Dialog.Toast("OTP has been sent to your mobile number");
                    _isBusy = false;
                    OnPropertyChanged(nameof(IsBusy));
                    _Otpblock = true; OnPropertyChanged(nameof(Otpblock));
                    _Dialog.HideLoading();
                }
                else
                {
                    _Dialog.Toast("Enter valid contact number");
                }
            }
            catch (Exception e) { _Dialog.HideLoading(); }


        }
        public async void GetgoogleAddress()
        {
            try
            {
                string content = GooglePlacesUrl + "?input=" + BusinessAddress + "&components=country:us|country:ca&key=" + Commonsettings.GoogleAPIkey;
                var client = new HttpClient();

                //  client.BaseAddress = baseAddress;
                //  client.DefaultRequestHeaders.Referrer = baseAddress;

                var response = await client.PostAsync(content, new StringContent("")).ConfigureAwait(false);
                var data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<Googlecity>(data);

                OnPropertyChanged(nameof(googleadd));
                _googleadd = result.Predictions;
                OnPropertyChanged(nameof(IsBusy));
                _isBusy = true;
            }
            catch (Exception e)
            {
            }
        }
        public async void OnSelectgoogleaddress(Predictions pt)
        {
            BusinessAddress = _tempBusinessAddress = pt.Description;

            OnPropertyChanged(nameof(IsBusy));
            _isBusy = false;
            string content = GoogleCityAndZipcode + "?placeid=" + pt.Place_id + "&key=" + Commonsettings.GoogleAPIkey;
            var client = new HttpClient();
            var response = await client.PostAsync(content, new StringContent("")).ConfigureAwait(false);
            var data = response.Content.ReadAsStringAsync().Result;
            var xmlElm = XElement.Parse(data);
            var addressResult = (from elm in xmlElm.Descendants()
                                 where
                                     elm.Name == "address_component"
                                 select elm);
            //get the location element  
            var locationResult = (from elm in xmlElm.Descendants()
                                  where
                                      elm.Name == "location"
                                  select elm);
            foreach (XElement item in locationResult)
            {
                Latitude = Convert.ToString(item.Elements().Where(e => e.Name.LocalName == "lat").FirstOrDefault().Value);
                Longitude = Convert.ToString(item.Elements().Where(e => e.Name.LocalName == "lng").FirstOrDefault().Value);
            }
            foreach (XElement element in addressResult)
            {
                string type = element.Elements().Where(e => e.Name.LocalName == "type").FirstOrDefault().Value;

                if (type.ToLower().Trim() == "locality") //if type is locality the get the locality  
                {
                    City = element.Elements().Where(e => e.Name.LocalName == "long_name").Single().Value;
                }
                else
                {
                    if (type.ToLower().Trim() == "administrative_area_level_2" && string.IsNullOrEmpty(City))
                    {
                        City = element.Elements().Where(e => e.Name.LocalName == "long_name").Single().Value;
                    }
                }
                if (type.ToLower().Trim() == "administrative_area_level_1") //if type is locality the get the locality  
                {
                    State = element.Elements().Where(e => e.Name.LocalName == "long_name").Single().Value;
                }
                if (type.ToLower().Trim() == "country") //if type is locality the get the locality  
                {
                    gCountry = element.Elements().Where(e => e.Name.LocalName == "long_name").Single().Value;
                    BizCountryCode = element.Elements().Where(e => e.Name.LocalName == "short_name").Single().Value;
                    if (gCountry == "United States") { gCountry = "US"; }
                }
                if (type.ToLower().Trim() == "postal_code") //if type is locality the get the locality  
                {
                    ZipCode = element.Elements().Where(e => e.Name.LocalName == "long_name").Single().Value;
                }
            }

        }
    }
}
