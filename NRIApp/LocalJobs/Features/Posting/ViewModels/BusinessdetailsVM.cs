using NRIApp.LocalJobs.Features.Posting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using NRIApp.LocalService.Features.Models;
using System.Xml.Linq;
using System.Linq;
using System.Net.Http;
using NRIApp.Helpers;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Plugin.Media;
using System.IO;
using Plugin.Media.Abstractions;
using Acr.UserDialogs;
using NRIApp.LocalJobs.Features.Posting.Interfaces;
using Refit;

namespace NRIApp.LocalJobs.Features.Posting.ViewModels
{
    public class BusinessdetailsVM : INotifyPropertyChanged
    {
        private const string GooglePlacesUrl = "https://maps.googleapis.com/maps/api/place/autocomplete/json";
        IUserDialogs _Dialog = UserDialogs.Instance;
        private const string GoogleCityAndZipcode = "https://maps.googleapis.com/maps/api/place/details/xml";
        public event PropertyChangedEventHandler PropertyChanged;
        private Plugin.Media.Abstractions.MediaFile _mediaFile;
        public Command Selectgoogleaddress { get; set; }
        public Command Choosefilecommand { get; set; }
        public Command Removeprofileimg { get; set; }
        public Command Bizdetailscmd { get; set; }
        public Command Bizselectcmd { get; set; }
        public Command Tapclosebiz { get; set; }
        public Command TCcheckcmd { get; set; }
        public Command PopupContentTap { get; set; }
        public Command takeprofilephoto { get; set; }
        public Command ContentViewTap { get; set; }
        string Latitude = string.Empty, Longitude = string.Empty, City = string.Empty, BizCountryCode = string.Empty,
             gCountry = string.Empty, State = string.Empty, ZipCode = string.Empty;
        Postingdata data = new Postingdata();

        private bool _Choosephotocontentviewvisible;
        public bool Choosephotocontentviewvisible
        {
            get { return _Choosephotocontentviewvisible; }
            set { _Choosephotocontentviewvisible = value; OnPropertyChanged(nameof(Choosephotocontentviewvisible)); }
        }

        public BusinessdetailsVM(Postingdata pd)
        {
            data = pd;
            Selectgoogleaddress = new Command<Predictions>(OnSelectgoogleaddress);
            Choosefilecommand = new Command(chooseprofile);
            takeprofilephoto = new Command(takephoto);
            Removeprofileimg = new Command(Removeprofimg);
            Bizdetailscmd = new Command(Sbmtbizdetails);
            Tapclosebiz = new Command(Closebiz);
            Bizselectcmd = new Command<Businessdetails>(Selectbusiness);
            TCcheckcmd = new Command(contactallowpermission);

            ContentViewTap = new Command(Closecontentview);
            PopupContentTap = new Command(showcontentview);
            if(pd.ismyneed=="1")
            {
                businessdetailsprefill();
            }
        }
        public void businessdetailsprefill()
        {
            try
            {
                //data.bizname = BusinessName;
                //data.bizaddress = BusinessAddress;
                //data.bizlogo = Profileimg;
                //data.postedvia = Commonsettings.UserMobileOS;
                //data.deviceid = Commonsettings.UserDeviceId;

                //data.discloseid = discloseID;
                //https://az827626.vo.msecnd.net/cdn/job/images/thumbnail/job_2021-03-10-02-27-52-147_10323033.jpeg
                if(!string.IsNullOrEmpty(data.imagepath))
                {
                    string[] bizimag = data.imagepath.Split(';');
                    foreach(var imgpth in bizimag)
                    {
                        Profileimg = "https://az827626.vo.msecnd.net/cdn/job/images/thumbnail/" + data.imagepath;
                    }
                }
                //Profileimg = data.html_photopath;
                BusinessAddress = _tempBusinessAddress = data.bizaddress;
                BusinessName = _tempbusinessname = data.bizname;
                if(data.disclosedbusiness == "1")
                {
                    discloseID = 0;
                    contactallowpermission();
                }
            }
            catch(Exception ex)
            {
                
            }
        }
        public string _businessname = "";
        public string _tempbusinessname = "";
        public string BusinessName
        {
            get { return _businessname; }
            set
            {
                _businessname = value;
                OnPropertyChanged(nameof(BusinessName));
                if(!string.IsNullOrEmpty(_businessname))
                {
                    if (_tempbusinessname != _businessname)
                        ajaxbusiness(_businessname);
                }
                //else
                //{
                //    Closebiz();
                //}
                
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
        private bool _isbiz = false;
        public bool Isbiz
        {
            get { return _isbiz; }
            set
            {
                _isbiz = value;
                OnPropertyChanged(nameof(Isbiz));
            }
        }
        private List<Predictions> _googleadd;

        public List<Predictions> googleadd
        {
            get { return _googleadd; }
            set { _googleadd = value; }
        }
        public string _tempBusinessAddress = "";
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
        private int _discloseID = 0;
        public int discloseID
        {
            get { return _discloseID; }
            set { _discloseID = value; OnPropertyChanged(nameof(discloseID)); }
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        public void contactallowpermission()
        {
            var currentpg = GetCurrentPage();
            if (discloseID == 0)
            {
                var checkbximg = currentpg.FindByName<Image>("checkbximg");
                checkbximg.Source = "CheckBoxChecked.png";
                discloseID = 1;
            }
            else
            {
                var checkbximg = currentpg.FindByName<Image>("checkbximg");
                checkbximg.Source = "CheckBoxUnChecked.png";
                discloseID = 0;
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
                    if (gCountry == "United States") { gCountry = "USA"; }
                }
                if (type.ToLower().Trim() == "postal_code") //if type is locality the get the locality  
                {
                    ZipCode = element.Elements().Where(e => e.Name.LocalName == "long_name").Single().Value;
                }
            }

        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
        string profileimgurl = "";


        public async void takephoto()
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

                    // //var mediaOption = new PickMediaOptions()
                    // //{
                    // //    PhotoSize = PhotoSize.Medium
                    // //};
                    //// _mediaFile = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions()).ConfigureAwait(true);
                    // if (_mediaFile == null) return;
                    // var CurrentPage = GetCurrentPage();
                    // //  var imgpath = CurrentPage.FindByName<Label>("Localpath");
                    // //imgpath.Text=  _mediaFile.Path;
                    // var memoryStream = new MemoryStream();
                    // Stream stream = null;
                    // stream = _mediaFile.GetStream();
                    // stream.CopyTo(memoryStream);
                    // var base64 = Convert.ToBase64String(memoryStream.ToArray());
                    // byte[] myBynary = memoryStream.ToArray();
                    // Random rnd = new Random(); int Randomnumber = rnd.Next(1, 1000);
                    // string integerValue = Randomnumber.ToString();
                    // var name = "job" + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-ss-mm-fff") + "_" + integerValue + ".jpg";




                    // MemoryStream defaultimg = new MemoryStream(myBynary);
                    // profileimgurl = await CommonMethods.Uploadimagestoblob("cdn/job/images", name, defaultimg);
                    // defaultimg.Dispose();

                    // //using(var ms =new MemoryStream())
                    // //{
                    // //smallimage image
                    // byte[] smallimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 150, 150);
                    // MemoryStream smallimagestrm = new MemoryStream(smallimage);
                    // // ms.Seek(0, SeekOrigin.Begin);
                    // // ms.Write(smallimage, 0, smallimage.Length);
                    // await CommonMethods.Uploadimagestoblob("cdn/job/images/smallimage", name, smallimagestrm);
                    // smallimagestrm.Dispose();

                    // Profileimg = profileimgurl;

                    // var path = CurrentPage.FindByName<StackLayout>("stackprfimg");
                    // path.IsVisible = true;
                    // //large image
                    // byte[] large = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 346, 250);
                    // MemoryStream largeimagestrm = new MemoryStream(large);
                    // await CommonMethods.Uploadimagestoblob("cdn/job/images/large", name, largeimagestrm);
                    // largeimagestrm.Dispose();

                    // //small image
                    // byte[] verysmallimg = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 106, 80);
                    // MemoryStream verysmallimgstrm = new MemoryStream(verysmallimg);
                    // await CommonMethods.Uploadimagestoblob("cdn/job/images/small", name, verysmallimgstrm);
                    // verysmallimgstrm.Dispose();

                    // //thumbnail image
                    // byte[] thumbnailimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 226, 170);
                    // MemoryStream thumbnailimagestrm = new MemoryStream(thumbnailimage);
                    // await CommonMethods.Uploadimagestoblob("cdn/job/images/thumbnail", name, thumbnailimagestrm);
                    // thumbnailimagestrm.Dispose();

                    // //thumbnailfull image
                    // byte[] thumbnailfullimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 770, 577);
                    // MemoryStream thumbnailfullimagestrm = new MemoryStream(thumbnailfullimage);
                    // await CommonMethods.Uploadimagestoblob("cdn/job/images/thumbnailfull", name, thumbnailfullimagestrm);
                    // thumbnailfullimagestrm.Dispose();

                    // //thumbnailmedium image
                    // byte[] thumbnailmediumimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 400, 260);
                    // MemoryStream thumbnailmediumimagestrm = new MemoryStream(thumbnailmediumimage);
                    // await CommonMethods.Uploadimagestoblob("cdn/job/images/thumbnailmedium", name, thumbnailmediumimagestrm);
                    // thumbnailmediumimagestrm.Dispose();

                    // //thumbs image
                    // byte[] thumbsimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 80, 60);
                    // MemoryStream thumbsimagestrm = new MemoryStream(thumbsimage);
                    // await CommonMethods.Uploadimagestoblob("cdn/job/images/thumbs", name, thumbsimagestrm);
                    // thumbsimagestrm.Dispose();
                    // //}
                    Choosephotocontentviewvisible = false;
                    photosize(_mediaFile);
                    _mediaFile.Dispose();
                  //  _Dialog.HideLoading();

                }
            }
            catch (Exception ee)
            {
                _Dialog.HideLoading();
            }

        }

        //public void removeimgfromlist(string imglist)
        //{
        //    if (AdmultiImage.Contains(imglist))
        //    {
        //        AdmultiImage.Remove(imglist);
        //    }
        //    string dbimg = imglist.Replace("http://cdnnrisulekhalive.blob.core.windows.net:80/cdn/roommates/images/smallimage/", "");
        //    if (AdmultiImagetoDB.Contains(dbimg))
        //    {
        //        AdmultiImagetoDB.Remove(dbimg);
        //    }
        //    var currentpage = GetCurrentPage();
        //    var lst = currentpage.FindByName<NRIApp.Helpers.HVScrollGridView>("multiimglist");
        //    lst.ItemsSource = null;
        //    lst.ItemsSource = AdmultiImage;
        //}


        //public static List<string> AdmultiImage = new List<string>();
        //public static List<string> AdmultiImagetoDB = new List<string>();

        //public static void Adimgurl(string imageurls, string photoname)
        //{
        //    try
        //    {
        //        AdmultiImagetoDB.Add(photoname);
        //        AdmultiImage.Add(imageurls);
        //        var currentpage = getcurrentpage();
        //        var findimglist = currentpage.FindByName<NRIApp.Helpers.HVScrollGridView>("multiimglist");
        //        findimglist.ItemsSource = null;
        //        findimglist.ItemsSource = AdmultiImage;
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        public void Closecontentview()
        {
            Choosephotocontentviewvisible = false;
        }
        public void showcontentview()
        {
            Choosephotocontentviewvisible = true;
        }


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
                   // _Dialog.ShowLoading("", null);
                    var mediaOption = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };
                    _mediaFile = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions()).ConfigureAwait(true);
                    Choosephotocontentviewvisible = false;
                    if(_mediaFile!=null)
                    {
                        photosize(_mediaFile);
                    }
                    else
                    {
                        _Dialog.HideLoading();
                    }
                    
                    

                }
            }
            catch (Exception ee)
            {
                _Dialog.HideLoading();
            }

        }
        public async void photosize(Plugin.Media.Abstractions.MediaFile _mediaFile)
        {
           try
            {
                
                _Dialog.ShowLoading("", null);
                if (_mediaFile == null) return;
                var CurrentPage = GetCurrentPage();
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
                var name = "job" + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-ss-mm-fff") + "_" + integerValue + ".jpg";




                MemoryStream defaultimg = new MemoryStream(myBynary);
                profileimgurl = await CommonMethods.Uploadimagestoblob("cdn/job/images", name, defaultimg);
                defaultimg.Dispose();

                //using(var ms =new MemoryStream())
                //{
                //smallimage image
                byte[] smallimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 150, 150);
                MemoryStream smallimagestrm = new MemoryStream(smallimage);
                // ms.Seek(0, SeekOrigin.Begin);
                // ms.Write(smallimage, 0, smallimage.Length);
                await CommonMethods.Uploadimagestoblob("cdn/job/images/smallimage", name, smallimagestrm);
                smallimagestrm.Dispose();

                Profileimg = profileimgurl;

                var path = CurrentPage.FindByName<StackLayout>("stackprfimg");
                path.IsVisible = true;
                //large image
                byte[] large = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 346, 250);
                MemoryStream largeimagestrm = new MemoryStream(large);
                await CommonMethods.Uploadimagestoblob("cdn/job/images/large", name, largeimagestrm);
                largeimagestrm.Dispose();

                //small image
                byte[] verysmallimg = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 106, 80);
                MemoryStream verysmallimgstrm = new MemoryStream(verysmallimg);
                await CommonMethods.Uploadimagestoblob("cdn/job/images/small", name, verysmallimgstrm);
                verysmallimgstrm.Dispose();

                //thumbnail image
                byte[] thumbnailimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 226, 170);
                MemoryStream thumbnailimagestrm = new MemoryStream(thumbnailimage);
                await CommonMethods.Uploadimagestoblob("cdn/job/images/thumbnail", name, thumbnailimagestrm);
                thumbnailimagestrm.Dispose();

                //thumbnailfull image
                byte[] thumbnailfullimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 770, 577);
                MemoryStream thumbnailfullimagestrm = new MemoryStream(thumbnailfullimage);
                await CommonMethods.Uploadimagestoblob("cdn/job/images/thumbnailfull", name, thumbnailfullimagestrm);
                thumbnailfullimagestrm.Dispose();

                //thumbnailmedium image
                byte[] thumbnailmediumimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 400, 260);
                MemoryStream thumbnailmediumimagestrm = new MemoryStream(thumbnailmediumimage);
                await CommonMethods.Uploadimagestoblob("cdn/job/images/thumbnailmedium", name, thumbnailmediumimagestrm);
                thumbnailmediumimagestrm.Dispose();

                //thumbs image
                byte[] thumbsimage = DependencyService.Get<IimageResize>().ResizeImage(myBynary, 80, 60);
                MemoryStream thumbsimagestrm = new MemoryStream(thumbsimage);
                await CommonMethods.Uploadimagestoblob("cdn/job/images/thumbs", name, thumbsimagestrm);
                thumbsimagestrm.Dispose();
                //}
                
                _Dialog.HideLoading();
            }
            catch(Exception ex)
            {
                _Dialog.HideLoading();
            }
        }
        public void Removeprofimg()
        {
            Profileimg = "";
            profileimgurl = "";
            var CurrentPage = GetCurrentPage();
            var path = CurrentPage.FindByName<StackLayout>("stackprfimg");
            path.IsVisible = false;
        }
        public void Sbmtbizdetails()
        {
            if(Validations())
            {
                data.bizname = BusinessName;
                data.bizaddress = BusinessAddress;
                data.bizlogo = Profileimg;
                data.postedvia = Commonsettings.UserMobileOS;
                data.deviceid = Commonsettings.UserDeviceId;
                data.discloseid = discloseID;
                var currentpage = GetCurrentPage();
                currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Userdetails(data));
            }
           
        }
        public bool Validations()
        {
            bool result = true;
            if(discloseID==0)
            {
                if (string.IsNullOrEmpty(BusinessName) || BusinessName.Trim().Length==0)
                {
                    _Dialog.Toast("Enter BusinessName");
                    //var entry = CurrentPage.FindByName<Entry>("ubizname");
                    //entry.Focus();
                    return false;
                }
            }
            
            if (string.IsNullOrEmpty(_tempBusinessAddress) || string.IsNullOrEmpty(BusinessAddress))
            {
                _Dialog.Toast("Enter Business Address");
                //var entry = CurrentPage.FindByName<Entry>("uaddress");
                //entry.Focus();
                return false;
            }
            return result;
        }

        public List<Businessdetails> _bizajax { get; set; }
        public List<Businessdetails> Bizajax
        {
            get
            {
                return _bizajax;
            }
            set
            {
                _bizajax = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bizajax)));
            }
        }
        public async void ajaxbusiness(string srch)
        {
            try
            {
                var baseApi = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                Businessdata list = await baseApi.Businessajax(srch);
                Bizajax = list.ROW_DATA;
                if (list.ROW_DATA.Count() > 0 && list.ROW_DATA != null)
                {
                    var CurrentPage = GetCurrentPage();
                    var bizstack = CurrentPage.FindByName<StackLayout>("stackbiz");
                    bizstack.IsVisible = true;
                }
            }
            catch(Exception ex)
            {

            }

        }
       
        public void Selectbusiness(Businessdetails biz)
        {
            var CurrentPage = GetCurrentPage();
            var bizstack = CurrentPage.FindByName<StackLayout>("stackbiz");
            bizstack.IsVisible = false;
            _tempbusinessname = biz.businessname;
            _businessname = biz.businessname;
              BusinessName = biz.businessname;
        }
        public void Closebiz()
        {
            var CurrentPage = GetCurrentPage();
            var bizstack = CurrentPage.FindByName<StackLayout>("stackbiz");
            bizstack.IsVisible = false;
        }
    }
}
