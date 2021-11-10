using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Linq;
using Acr.UserDialogs;
using NRIApp.Helpers;
using Refit;
using NRIApp.LocalJobs.Features.Listing.Models;
using NRIApp.DayCare.Features.Post.Models;
using System.Net.Http;
using Newtonsoft.Json;
using NRIApp.Roommates.Features.Post.Models;
using NRIApp.DayCare.Features.Post.Views;
using Plugin.Media;
using NRIApp.DayCare.Features.Post.Interface;
using Plugin.Media.Abstractions;
using System.IO;

namespace NRIApp.DayCare.Features.Post.ViewModels
{
    public class PostSecond_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Acr.UserDialogs.IUserDialogs dialog = UserDialogs.Instance;
        public static string Adid_DC = "";
        public Command nxtcmd { get; set; }
        public Command otherlocationyesCommand { get; set; }
        public Command RyesCommand { get; set; }
        public Command RnoCommand { get; set; }
        public Command showmapcmd { get; set; }
        public Command choosephototap { get; set; }
        public Command ContentViewTap { get; set; }
        public Command PopupContentTap { get; set; }
        public Command Choosefilecommand { get; set; }
        public Command takeprofilephoto { get; set; }
        public Command Tapclosebiz { get; set; }
        public Command Removeprofileimg { get; set; }
        public Command Removeimg { get; set; }

        public Command<Roommates.Features.Post.Models.Location> SelectLocation { get; set; }
        public Command<NRIApp.Roommates.Features.Post.Models.Predictions> SelectAddressLocation { get; set; }

        private List<NRIApp.Roommates.Features.Post.Models.Location> locationlist;
        public List<NRIApp.Roommates.Features.Post.Models.Location> _locationlist
        {
            get { return _locationlist; }
            set { _locationlist = value;OnPropertyChanged(nameof(locationlist)); }
        }
        private string _adtitle = "";
        public string adtitle
        {
            get { return _adtitle; }
            set { _adtitle = value; OnPropertyChanged(nameof(adtitle)); }
        }
        private string _addescription = "";
        public string addescription
        {
            get { return _addescription; }
            set { _addescription = value; OnPropertyChanged(nameof(addescription)); }
        }
        private string _cityzipcode = "";
        public string cityzipcode
        {
            get { return _cityzipcode; }
            set { _cityzipcode = value; OnPropertyChanged(nameof(cityzipcode)); }
        }
        private string _addresszipcode = "";
        public string addresszipcode
        {
            get { return _addresszipcode; }
            set { _addresszipcode = value; OnPropertyChanged(nameof(addresszipcode)); }
        }
        private bool _citylistbvisble = false;
        public bool citylistbvisble
        {
            get { return _citylistbvisble; }
            set { _citylistbvisble = value;OnPropertyChanged(nameof(citylistbvisble)); }
        }
        private bool _addresslistvisible = false;
        public bool addresslistvisible
        {
            get { return _addresslistvisible; }
            set { _addresslistvisible = value; OnPropertyChanged(nameof(addresslistvisible)); }
        }
        private string _YesImg = "RadioButtonUnChecked.png";
        public string YesImg
        {
            get { return _YesImg; }
            set { _YesImg = value;OnPropertyChanged(nameof(YesImg)); }
        }
        private string _NoImg = "RadioButtonUnChecked.png";
        public string NoImg
        {
            get { return _NoImg; }
            set { _NoImg = value; OnPropertyChanged(nameof(NoImg)); }
        }
        private int _worklocationID = 0;
        public int worklocationID
        {
            get { return _worklocationID; }
            set { _worklocationID = value; OnPropertyChanged(nameof(worklocationID)); }
        }
        private string _workotherlocationImg = "CheckBoxChecked.png";
        public string workotherlocationImg
        {
            get { return _workotherlocationImg; }
            set { _workotherlocationImg = value; OnPropertyChanged(nameof(workotherlocationImg)); }
        }
        private string _zipcode = "";
        public string zipcode
        {
            get { return _zipcode; }
            set { _zipcode = value; OnPropertyChanged(nameof(zipcode)); }
        }
        private string _Zipcode = "";
        public string Zipcode
        {
            get { return _Zipcode; }
            set { _Zipcode = value;OnPropertyChanged(nameof(Zipcode)); }
        }
        private string _showmapImg = "CheckBoxUnChecked.png";
        public string showmapImg
        {
            get { return _showmapImg; }
            set { _showmapImg = value;OnPropertyChanged(nameof(showmapImg)); }
        }
        private int _showmapID =0;
        public int showmapID
        {
            get { return _showmapID; }
            set { _showmapID = value; OnPropertyChanged(nameof(showmapID)); }
        }
       
        public string TempLocationName = "";
        private string _LocationName = "";
        public string LocationName
        {
            get { return _LocationName; }
            set
            {
                _LocationName = value;
                OnPropertyChanged(nameof(LocationName));
                if(string.IsNullOrEmpty(LocationName))
                {
                    TempLocationName = "";
                }
                if(!(string.IsNullOrEmpty(LocationName)) &&(TempLocationName!=LocationName))
                {
                    GetLocation();
                }
            }
        }
        public string TempaddresslocationName = "";
        private string _addresslocationname = "";
        public string addresslocationname
        {
            get { return _addresslocationname; }
            set
            {
                _addresslocationname = value;
                OnPropertyChanged(nameof(addresslocationname));
                if (string.IsNullOrEmpty(addresslocationname))
                {
                    TempaddresslocationName = "";
                }
                if (!(string.IsNullOrEmpty(addresslocationname)) && addresslocationname.Trim().Length!=0 && (TempaddresslocationName != addresslocationname))
                {
                    //  GetaddressLocation();
                    GetgoogleAddress();
                }
            }
        }

        private string _placeid = "";
        public string placeid
        {
            get { return _placeid; }
            set { _placeid = value; OnPropertyChanged(nameof(placeid)); }
        }
        private string _authorizationtxt = "";
        public string authorizationtxt
        {
            get { return _authorizationtxt; }
            set { _authorizationtxt = value; OnPropertyChanged(nameof(authorizationtxt)); }
        }
        
        private bool _offertypevisible = false;
        public bool offertypevisible
        {
            get { return _offertypevisible; }
            set { _offertypevisible = value;OnPropertyChanged(nameof(offertypevisible)); }
        }
        private bool _ishousekeepingtype = false;
        public bool ishousekeepingtype
        {
            get { return _ishousekeepingtype; }
            set { _ishousekeepingtype = value; OnPropertyChanged(nameof(ishousekeepingtype)); }
        }
        private bool _photondvedioblkvisiblity = false;
        public bool photondvedioblkvisiblity
        {
            get { return _photondvedioblkvisiblity; }
            set { _photondvedioblkvisiblity = value; OnPropertyChanged(nameof(photondvedioblkvisiblity)); }
        }
        private string _businessname = "";
        public string businessname
        {
            get { return _businessname; }
            set { _businessname = value;OnPropertyChanged(nameof(businessname)); }
        }
        private string _licenseno = "";
        public string licenseno
        {
            get { return _licenseno; }
            set { _licenseno = value; OnPropertyChanged(nameof(licenseno)); }
        }
      
        private double _Latitude;
        public double Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; OnPropertyChanged(nameof(Latitude)); }
        }
        private double _Longitude;
        public double Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; OnPropertyChanged(nameof(Longitude)); }
        }
        private string _Country = "";
        public string Country
        {
            get { return _Country; }
            set { _Country = value; OnPropertyChanged(nameof(Country)); }
        }
        private string _Statecode = "";
        public string Statecode
        {
            get { return _Statecode; }
            set { _Statecode = value; OnPropertyChanged(nameof(Statecode)); }
        }
        private string _newcityurl = "";
        public string newcityurl
        {
            get { return _newcityurl; }
            set { _newcityurl = value; OnPropertyChanged(nameof(newcityurl)); }
        }
        private bool _Choosephotocontentviewvisible = false;
        public bool Choosephotocontentviewvisible
        {
            get { return _Choosephotocontentviewvisible; }
            set { _Choosephotocontentviewvisible = value;OnPropertyChanged(nameof(Choosephotocontentviewvisible)); }
        }
        public bool postSecondvalidtion()
        {
            bool result = true;
            var currentpage = Getcurrentpage();
            if (string.IsNullOrEmpty(adtitle) || adtitle.Trim().Length==0)
            {
                dialog.Toast("Please enter your Ad title");
                result = false;
                return false;
            }
            if (string.IsNullOrEmpty(addescription) || addescription.Trim().Length==0)
            {
                dialog.Toast("Please enter your Ad Description");
                result = false;
                return false;
            }
            if(!string.IsNullOrEmpty(adtitle))
            {
                if(adtitle.Trim().Length>120)
                {
                    dialog.Toast("Please enter between 0-120 characters.");
                    result = false;
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(addescription))
            {
                if (addescription.Trim().Length > 900)
                {
                    dialog.Toast("Enter a detailed description within 900 characters.");
                    result = false;
                    return false;
                }
            }
            //if (string.IsNullOrEmpty(cityzipcode))
            //{
            //    dialog.Toast("Please select your City");
            //    result = false;
            //}
            //if (addresslocationname.Trim().Length==0)
            //{
            //    dialog.Toast("Please select your Address");
            //    result = false;
            //    return false;
            //}
            //if (string.IsNullOrEmpty(zipcode) || zipcode.Trim().Length==0)
            //{
            //    dialog.Toast("Please enter your Zipcode");
            //    result = false;
            //    return false;
            //}
            if (offertypevisible==true)
            {
                if(string.IsNullOrEmpty(businessname) || businessname.Trim().Length==0)
                {
                    dialog.Toast("Please enter your business name");
                    result = false;
                    return false;
                }
                //if (string.IsNullOrEmpty(licenseno) || licenseno.Trim().Length == 0)
                //{
                //    dialog.Toast("Please enter your license number");
                //    result = false;
                //    return false;
                //}
            }
            return result;
        }
        private bool _contentviewvisible = false;
        public bool contentviewvisible
        {
            get { return _contentviewvisible; }
            set { _contentviewvisible = value; OnPropertyChanged(nameof(contentviewvisible)); }
        }
        private bool _imgvediolinksblkvisible = false;
        public bool imgvediolinksblkvisible
        {
            get { return _imgvediolinksblkvisible; }
            set { _imgvediolinksblkvisible = value; OnPropertyChanged(nameof(imgvediolinksblkvisible)); }
        }
        
        Daycareposting post = new Daycareposting();
        public PostSecond_VM(Daycareposting postdata)
        {
            if(postdata.ismyneed!="1")
            {
                AdmultiImage = new List<string>();
                AdmultiImagetoDB = new List<string>();
            }
           // profilepicurl = "";
            Commonsettings.CategoryType = "Daycare";
            post = postdata;
            Adid_DC = postdata.businessid;
            if (postdata.supercategoryid=="1")
            {
                if(postdata.secondarycategoryid=="9" || postdata.secondarycategoryid =="49" || postdata.secondarycategoryid == "12" || postdata.secondarycategoryid == "14")
                {
                    offertypevisible = true;
                }
                imgvediolinksblkvisible = true;
            }
            else
            {
                imgvediolinksblkvisible = false;
            }
            //if(postdata.primarycategoryid=="44" || postdata.primarycategoryid == "45") //Housekeeper and cook
            //{
            //    ishousekeepingtype = true;
            //    authorizationtxt = "I am legally eligible to work in this location";
            //}
            //else
            //{
            //    authorizationtxt = "This care center is legally authorized to work in this location";
            //}
            if(postdata.ismyneed=="1")
            {
                prefill();
            }
            nxtcmd = new Command(gotopostthird);
            RyesCommand = new Command(clickyescmd);
            RnoCommand = new Command(clicknocmd);
            otherlocationyesCommand = new Command(clickotherlocation);
            showmapcmd = new Command(checkshowmapdir);
            SelectLocation = new Command<Roommates.Features.Post.Models.Location>(selectlocation);
            //SelectAddressLocation = new Command<Roommates.Features.Post.Models.Location>(selectaddresslocation);
            SelectAddressLocation = new Command<NRIApp.Roommates.Features.Post.Models.Predictions>(OnSelectgoogleaddress);
            choosephototap = new Command(choosephotos);

            ContentViewTap = new Command(Closecontentview);
            PopupContentTap = new Command(showcontentview);
            Choosefilecommand = new Command(chooseprofile);
            takeprofilephoto = new Command(takephoto);
            //Tapclosebiz = new Command(Closebiz);
            Removeprofileimg = new Command(Removeprofimg);
            Removeimg = new Command<string>(removeimgfromlist);
    }
        public void prefill()
        {
            try
            {
                if(string.IsNullOrEmpty(post.adtitle))
                {
                    adtitle = post.businessname;
                }
                else
                {
                    adtitle = post.adtitle;
                }
                addescription = post.description;
                if (offertypevisible == true)
                {
                    businessname = post.businessname;
                }
                licenseno = post.licenseno;
                if (post.Secondarycategoryvalueurl == "nanny-babysitter")
                {
                    Profileimg = post.photo;
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
    public void Closebiz()
    {
        var CurrentPage = Getcurrentpage();
        var bizstack = CurrentPage.FindByName<StackLayout>("stackprfimg");
        bizstack.IsVisible = false;
    }
        public void Closecontentview()
        {
            contentviewvisible = false;
        }
        public void showcontentview()
        {
            contentviewvisible = true;
        }
        public async void choosephotos()
        {
            try
            {
               
                if (post.secondarycategoryid == "9")
                {
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        dialog.Toast("This is not support on your device.");
                        return;
                    }
                    else
                    {
                        await DependencyService.Get<IMultiMediaPickerService>().PickPhotosAsync(Adid_DC);
                    }
                }
                else
                {
                    Choosephotocontentviewvisible = true;
                }

            }
            catch (Exception e)
            {
                dialog.HideLoading();
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
        public static string profileimgurl = "";
        public async void takephoto()
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    dialog.Toast("This is not support on your device.");
                    return;
                }
                else
                {
                    //  _Dialog.ShowLoading("", null);

                    if (Commonsettings.UserMobileOS.ToLower() == "android")
                    {
                        _mediaFile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                        {
                            PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                            Directory = "Sample",
                            Name = "test.jpg",
                        });
                    }
                    else if (Commonsettings.UserMobileOS.ToLower() == "ios")
                    {
                        Func<object> img = DependencyService.Get<ICameraUploading>().GetImage();
                        _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                        {
                            OverlayViewProvider = img
                        });
                    }


                    Choosephotocontentviewvisible = false;
                    photosize(_mediaFile);
                    _mediaFile.Dispose();
                    //  _Dialog.HideLoading();

                }
            }
            catch (Exception ee)
            {
                dialog.HideLoading();
            }

        }

        private Plugin.Media.Abstractions.MediaFile _mediaFile;
        public async void chooseprofile()
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    dialog.Toast("This is not support on your device.");
                    return;
                }
                else
                {
                    // _Dialog.ShowLoading("", null);
                    var mediaOption = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };
                    _mediaFile = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions()).ConfigureAwait(true);
                    Choosephotocontentviewvisible = false;
                    if (_mediaFile != null)
                    {
                        photosize(_mediaFile);
                    }
                    else
                    {
                        dialog.HideLoading();
                    }



                }
            }
            catch (Exception ee)
            {
                dialog.HideLoading();
            }

        }
        public async void photosize(Plugin.Media.Abstractions.MediaFile _mediaFile)
        {
            try
            {

                dialog.ShowLoading("", null);
                if (_mediaFile == null) return;
                var CurrentPage = Getcurrentpage();
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
                var name = "daycare" + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-ss-mm-fff") + "_" + integerValue + ".jpg";




                MemoryStream defaultimg = new MemoryStream(myBynary);
                profileimgurl = await CommonMethods.Uploadimagestoblob("cdn/daycare/images", name, defaultimg);
                defaultimg.Dispose();

                //using(var ms =new MemoryStream())
                //{
                //smallimage image
                byte[] smallimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 150, 150);
                MemoryStream smallimagestrm = new MemoryStream(smallimage);
                // ms.Seek(0, SeekOrigin.Begin);
                // ms.Write(smallimage, 0, smallimage.Length);
                await CommonMethods.Uploadimagestoblob("cdn/daycare/images/smallimage", name, smallimagestrm);
                smallimagestrm.Dispose();

                Profileimg = profileimgurl;

                var path = CurrentPage.FindByName<StackLayout>("stackprfimg");
                path.IsVisible = true;
                //large image
                byte[] large = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 346, 250);
                MemoryStream largeimagestrm = new MemoryStream(large);
                await CommonMethods.Uploadimagestoblob("cdn/daycare/images/large", name, largeimagestrm);
                largeimagestrm.Dispose();

                //small image
                byte[] verysmallimg = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 106, 80);
                MemoryStream verysmallimgstrm = new MemoryStream(verysmallimg);
                await CommonMethods.Uploadimagestoblob("cdn/daycare/images/small", name, verysmallimgstrm);
                verysmallimgstrm.Dispose();

                //thumbnail image
                byte[] thumbnailimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 226, 170);
                MemoryStream thumbnailimagestrm = new MemoryStream(thumbnailimage);
                await CommonMethods.Uploadimagestoblob("cdn/daycare/images/thumbnail", name, thumbnailimagestrm);
                thumbnailimagestrm.Dispose();

                //thumbnailfull image
                byte[] thumbnailfullimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 770, 577);
                MemoryStream thumbnailfullimagestrm = new MemoryStream(thumbnailfullimage);
                await CommonMethods.Uploadimagestoblob("cdn/daycare/images/thumbnailfull", name, thumbnailfullimagestrm);
                thumbnailfullimagestrm.Dispose();

                //thumbnailmedium image
                byte[] thumbnailmediumimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 400, 260);
                MemoryStream thumbnailmediumimagestrm = new MemoryStream(thumbnailmediumimage);
                await CommonMethods.Uploadimagestoblob("cdn/daycare/images/thumbnailmedium", name, thumbnailmediumimagestrm);
                thumbnailmediumimagestrm.Dispose();

                //thumbs image
                byte[] thumbsimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 80, 60);
                MemoryStream thumbsimagestrm = new MemoryStream(thumbsimage);
                await CommonMethods.Uploadimagestoblob("cdn/daycare/images/thumbs", name, thumbsimagestrm);
                thumbsimagestrm.Dispose();
                //}

                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public void Removeprofimg()
        {
            Profileimg = "";
            profileimgurl = "";
            var CurrentPage = Getcurrentpage();
            var path = CurrentPage.FindByName<StackLayout>("stackprfimg");
            path.IsVisible = false;
        }
        public void removeimgfromlist(string imglist)
        {
            if (AdmultiImage.Contains(imglist))
            {
                AdmultiImage.Remove(imglist);
            }
            string dbimg = imglist.Replace("http://cdnnrisulekhalive.blob.core.windows.net:80/cdn/roommates/images/smallimage/", "");
            if (AdmultiImagetoDB.Contains(dbimg))
            {
                AdmultiImagetoDB.Remove(dbimg);
            }
            var currentpage = Getcurrentpage();
            var lst = currentpage.FindByName<NRIApp.Helpers.HVScrollGridView>("multiimglist");
            lst.ItemsSource = null;
            lst.ItemsSource = AdmultiImage;
        }
        private string _cityID = "";
        public string cityID
        {
            get { return _cityID; }
            set { _cityID = value;OnPropertyChanged(nameof(cityID)); }
        }
        private string _addresscityID = "";
        public string addresscityID
        {
            get { return _addresscityID; }
            set { _addresscityID = value; OnPropertyChanged(nameof(addresscityID)); }
        }
        public void selectlocation(Roommates.Features.Post.Models.Location location)
        {
            var city = location.citystatecode;
            LocationName = TempLocationName = city;
            cityID = location.city;
            Latitude = location.latitude;
            Longitude = location.longitude;
            Statecode = location.statecode;
            newcityurl = location.cityurl;
            cityzipcode = zipcode = location.zipcode.ToString();
            citylistbvisble = false;
        }
        //public void selectaddresslocation(Roommates.Features.Post.Models.Location location)
        //{
        //    var city = location.citystatecode;
        //    addresslocationname = TempaddresslocationName = city;
        //    addresscityID = location.city;
        //    addresszipcode = location.zipcode.ToString();
        //    addresslistvisible = false;
        //}
        public void checkshowmapdir()
        {
            if (showmapID == 0)
            {
                showmapImg = "CheckBoxChecked.png";
                showmapID = 1;
            }
            else
            {
                showmapImg = "CheckBoxUnChecked.png";
                showmapID = 0;
            }
        }
      
        public async void GetLocation()
        {
            try
            {
                cityzipcode = "";
                //Tlocationtxt = "";
                var LocationAPI = RestService.For<Roommates.Features.Post.Interface.IRMCategory>(Commonsettings.Localservices);
                Roommates.Features.Post.Models.LocationRowData response = await LocationAPI.getlocation(LocationName);
                
                if (response != null && response.ROW_DATA.Count > 0)
                {
                    citylistbvisble = true;
                    locationlist = response.ROW_DATA;
                    var currentpage = Getcurrentpage();
                    var locationdata = currentpage.FindByName<ListviewScrollbar>("locationlistdata");

                    foreach (var item in response.ROW_DATA)
                    {
                        item.citystatecode = item.city + ", " + item.statecode;
                    }
                    locationdata.ItemsSource= response.ROW_DATA;
                }
                
            }
            catch (Exception e)
            {
            }
        }
        public async void GetaddressLocation()
        {
            try
            {
                addresszipcode = "";
                //Tlocationtxt = "";
                var LocationAPI = RestService.For<Roommates.Features.Post.Interface.IRMCategory>(Commonsettings.Localservices);
                Roommates.Features.Post.Models.LocationRowData response = await LocationAPI.getlocation(addresslocationname);

                if (response != null && response.ROW_DATA.Count > 0)
                {
                    addresslistvisible = true;
                   // locationlist = response.ROW_DATA;
                    var currentpage = Getcurrentpage();
                    var locationdata = currentpage.FindByName<ListviewScrollbar>("addresslistdata");

                    foreach (var item in response.ROW_DATA)
                    {
                        item.citystatecode = item.city + ", " + item.statecode;
                    }
                    locationdata.ItemsSource = response.ROW_DATA;
                }

            }
            catch (Exception e)
            {
            }
        }
        private const string GooglePlacesUrl = "https://maps.googleapis.com/maps/api/place/autocomplete/json";
        private List<Predictions> _googleadd;
        public List<Predictions> googleadd
        {
            get { return _googleadd; }
            set { _googleadd = value; }
        }
        public async void GetgoogleAddress()
        {
            try
            {
                placeid = "";
                string content = GooglePlacesUrl + "?input=" + addresslocationname + "&components=country:us&key=" + Commonsettings.GoogleAPIkey;
                var client = new HttpClient();
                var response = await client.PostAsync(content, new StringContent("")).ConfigureAwait(false);
                var data = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<Googlecity>(data);
                var currentpage = Getcurrentpage();
                //var locationdata = currentpage.FindByName<ListviewScrollbar>("addresslistdata");
                //locationdata.ItemsSource = result.Predictions;
                
                OnPropertyChanged(nameof(googleadd));
                _googleadd = result.Predictions;
                OnPropertyChanged(nameof(addresslistvisible));
                addresslistvisible = true;
            }
            catch (Exception e)
            {
                dialog.HideLoading();
            }
        }
        public void OnSelectgoogleaddress(Predictions pt)
        {
            placeid = pt.Place_id;
            addresslocationname = TempaddresslocationName = pt.Description;
            addresslistvisible = false;
            OnPropertyChanged(nameof(addresslistvisible));
            addresslistvisible = false;
        }

        //public void OnaddresslocationClick(Roommates.Features.Post.Models.Location obj)
        //{
        //    try
        //    {
        //        var city = obj.citystatecode;
        //        addresslocationname = TempaddresslocationName = city;
        //        addresszipcode = obj.zipcode.ToString();
        //        addresslistvisible = false;
        //        //citytxt = LocationName;
        //        //Newycityurl = obj.newcityurl;
        //        //StateName = obj.statename;
        //        //StateCode = obj.statecode;

        //        //Lat = obj.latitude.ToString();
        //        //Long = obj.longitude.ToString();

        //    }
        //    catch (Exception e) { }
        //}
        public void clickotherlocation()
        {
            YesImg = "RadioButtonUnChecked.png";
            NoImg = "RadioButtonUnChecked.png";
            workotherlocationImg = "RadioButtonChecked.png";
            worklocationID = 1;
        }
        public void clickyescmd()
        {
            YesImg = "RadioButtonChecked.png";
            NoImg = "RadioButtonUnChecked.png";
            workotherlocationImg = "RadioButtonUnChecked.png";
            worklocationID = 1;
        }
        public void clicknocmd()
        {
            YesImg = "RadioButtonUnChecked.png";
            NoImg = "RadioButtonChecked.png";
            workotherlocationImg = "RadioButtonUnChecked.png";
            worklocationID = 0;
        }
        private string _videolink = "";
        public string videolink
        {
            get { return _videolink; }
            set { _videolink = value;OnPropertyChanged(nameof(videolink)); }
        }
        public static List<string> AdmultiImage = new List<string>();
        public static List<string> AdmultiImagetoDB = new List<string>();
        public async void gotopostthird()
        {
            try
            {
                
                if (postSecondvalidtion())
                {
                    dialog.ShowLoading("", null);
                    post.adtitle = adtitle;
                    post.description = addescription;
                    post.businessname = adtitle;
                    if (offertypevisible == true)
                    {
                        post.businessname = businessname;
                        post.licenseno = licenseno;
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
                    post.postedviaid = sPostedviaid;
                   
                    if ((AdmultiImage != null && !string.IsNullOrEmpty(AdmultiImage.ToString()) && AdmultiImage.Count!=0 )|| !string.IsNullOrEmpty(profileimgurl)|| !string.IsNullOrEmpty(videolink))
                    {
                        string imgs = string.Join(";", AdmultiImage.ToArray());
                        if(!string.IsNullOrEmpty(profileimgurl))
                        {
                            imgs = profileimgurl;
                        }
                        var DCpostAPIt = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                        var postresponset = await DCpostAPIt.addimgvlinks(post.businessid, videolink, imgs, Commonsettings.UserPid);
                    }

                    string ipaddress = DependencyService.Get<Techjobs.Features.LeadForm.Interfaces.IIPAddressManager>().GetIPAddress();
                    var curentpage = Getcurrentpage();
                   
                       if(post.ismyneed=="1")
                        {
                        //await curentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.PostThird(post));
                        await curentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Location(post));
                        }
                       else
                        {
                        var DCpostAPI = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                        //  var postresponse = await DCpostAPI.postsecondsubmit(post);
                        var postresponse = await DCpostAPI.mobileverification(post.businessid, post.ContactNo, post.countrycode, post.emailid, post.usertype, Commonsettings.UserDeviceId, Commonsettings.UserPid, ipaddress);

                        if (postresponse != null)
                        {
                            if (postresponse.blockstatus == "1")
                            {
                                dialog.HideLoading();
                                await curentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Spammer());
                            }
                            else
                            {
                                if (postresponse.otpsend == "otpsend")
                                {
                                    post.pinno = postresponse.otpno;
                                    post.freeadstatus = postresponse.freeadstatus;
                                    await curentpage.Navigation.PushAsync(new OTP_DC(post));
                                }
                                else
                                {
                                    if (postresponse.ispayment == "1")
                                    {
                                        post.freeadstatus = postresponse.freeadstatus;
                                        await curentpage.Navigation.PushAsync(new Package(post));
                                    }
                                    else
                                    {
                                        await curentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Spammer());
                                    }
                                }
                            }
                        }
                    }
                    dialog.HideLoading();

                }
                
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
            //try
            //{
            //    //dialog.ShowLoading("", null);
            //    //if(postSecondvalidtion())
            //    //{
            //        var currentpage = Getcurrentpage();
            //    //    post.adtitle = adtitle;
            //    //    post.description = addescription;
            //    //    if(offertypevisible==true)
            //    //    {
            //    //        post.businessname = businessname;
            //    //        post.license = licenseno;
            //    //    }

            //    //    post.city = cityID;
            //    //    post.lat = Latitude;
            //    //    post.longtitude = Longitude;
            //    //    post.lat = Latitude;
            //    //    post.state = Statecode;
            //    //    post.locationAddress = addresslocationname;
            //    //    post.zipcode = zipcode;
            //    //    post.hidemap = showmapID.ToString();
            //    //    post.newcityurl = newcityurl;
            //        currentpage.Navigation.PushAsync(new Views.PostThird(post));
            //  //  }
            //    dialog.HideLoading();
            //}
            //catch(Exception ex)
            //{
            //    dialog.HideLoading();
            //}
        }
        public static void Adimgurl(string imageurls, string photoname)
        {
           try
            {
                AdmultiImagetoDB.Add(photoname);
                AdmultiImage.Add(imageurls);
                var currentpage = getcurrentpage();
                var findimglist = currentpage.FindByName<NRIApp.Helpers.HVScrollGridView>("multiimglist");
                findimglist.ItemsSource = null;
                findimglist.ItemsSource = AdmultiImage;
            }
            catch(Exception ex)
            {

            }
        }
        private static Page getcurrentpage()
        {
            var currentpage = App.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page Getcurrentpage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}
