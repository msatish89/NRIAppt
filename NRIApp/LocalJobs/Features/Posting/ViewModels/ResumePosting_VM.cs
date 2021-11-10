using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using NRIApp.LocalJobs.Features.Posting.Models;
using NRIApp.LocalJobs.Features.Posting.Interfaces;
using Refit;
using NRIApp.LocalService.Features.Models;
using Acr.UserDialogs;
using Xamarin.Forms;
using System.Linq;
using System.Xml.Linq;
using NRIApp.Helpers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using NRIApp.Techjobs.Features.LeadForm.Models;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NRIApp.LocalJobs.Features.Posting.ViewModels
{
    public class ResumePosting_VM : INotifyPropertyChanged
    {
        private const string GooglePlacesUrl = "https://maps.googleapis.com/maps/api/place/autocomplete/json";
        IUserDialogs _Dialog = UserDialogs.Instance;
        private const string GoogleCityAndZipcode = "https://maps.googleapis.com/maps/api/place/details/xml";
        public event PropertyChangedEventHandler PropertyChanged;
        public Command Selectgoogleaddress { get; set; }
        public Command Bizdetailscmd { get; set; }
        public Command Bizselectcmd { get; set; }
        public Command Industryselectcmd { get; set; }
        public Command Cityajaxcommand { get; set; }
        public Command TapBizclose { get; set; }
        public ICommand Sbmtuserdetailscommand { get; set; }
        public Command TapRecruiter { get; set; }
        public Command TapConsultant { get; set; }
        string Latitude = string.Empty, Longitude = string.Empty, City = string.Empty, BizCountryCode = string.Empty,
            gCountry = string.Empty, State = string.Empty, ZipCode = string.Empty;
        private string _RecImg;
        private string _ConsuImg;

        public string RecImg
        {
            get { return _RecImg; }
            set
            {
                _RecImg = value;
                OnPropertyChanged(nameof(RecImg));
            }
        }

        public string ConsuImg
        {
            get { return _ConsuImg; }
            set
            {
                _ConsuImg = value;
                OnPropertyChanged(nameof(ConsuImg));
            }
        }
        Resumepkgdetails data = new Resumepkgdetails();
        public ResumePosting_VM()
        {
            data.usertypeid = "2";
            RecImg = "RadioButtonUnChecked.png";
            ConsuImg = "RadioButtonChecked.png";
            Selectgoogleaddress = new Command<Predictions>(OnSelectgoogleaddress);
            Bizdetailscmd = new Command(Sbmtbizdetails);
            Bizselectcmd = new Command<Businessdetails>(Selectbusiness);
            Industryselectcmd = new Command<Industries>(Selectindustry);
            TapBizclose = new Command(Closebiz);
            Cityajaxcommand = new Command<Cityajax>(Selectajaxcity);
            TapRecruiter = new Command(ChangeRecruiter);
            TapConsultant = new Command(ChangeConsultant);
        }

        public Command validatephoneno { get; set; }
        public Command verifyphoneno { get; set; }
        public Command resend { get; set; }
        public Command changenumber { get; set; }
        private string _verifycode = "";
        public string verifycode
        {
            get { return _verifycode; }
            set { _verifycode = value; OnPropertyChanged(nameof(verifycode)); }
        }
        private bool _phvalidatetxtvisible = false;
        public bool phvalidatetxtvisible
        {
            get { return _phvalidatetxtvisible; }
            set { _phvalidatetxtvisible = value; OnPropertyChanged(nameof(phvalidatetxtvisible)); }
        }
        private string _phvalidatetxt = "";
        public string phvalidatetxt
        {
            get { return _phvalidatetxt; }
            set { _phvalidatetxt = value; OnPropertyChanged(nameof(phvalidatetxt)); }
        }
        private bool _validatephnovisible = true;
        public bool validatephnovisible
        {
            get { return _validatephnovisible; }
            set { _validatephnovisible = value; OnPropertyChanged(nameof(validatephnovisible)); }
        }
        private string _pinno = "";
        public string pinno
        {
            get { return _pinno; }
            set { _pinno = value; OnPropertyChanged(nameof(pinno)); }
        }
        private string _mobileverified = "0";
        public string mobileverified
        {
            get { return _mobileverified; }
            set { _mobileverified = value; OnPropertyChanged(nameof(mobileverified)); }
        }
        IUserDialogs dialogs = UserDialogs.Instance;

        //contact details
        public ResumePosting_VM(Resumepkgdetails pkgdata)
        {
            data = pkgdata;
            Sbmtuserdetailscommand = new Command(Sbmtuserdetails);
            if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                UEmail = Commonsettings.UserEmail;
            if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                UName = Commonsettings.UserName;
            if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                Mobileno = Commonsettings.UserMobileno;

            validatephoneno = new Command(async () => await validatenumber(""));
            verifyphoneno = new Command(async () => await validatenumber("1"));
            resend = new Command(async () => await validatenumber(""));
            changenumber = new Command(changenum);
        }
        public async Task validatenumber(string verify)
        {
            try
            {
                validatephnovisible = false;


                validateleadform();
                if (validate == true)
                {
                    dialogs.ShowLoading("", null);

                    var validate = RestService.For<Detail.Interfaces.IComapanydata>(Commonsettings.LocaljobsAPI);
                    var validateno = await validate.verifyphone(Selectcountry, Mobileno, verify, verifycode, pinno);
                    //validateno.mode == "0" ||
                    if (!string.IsNullOrEmpty(validateno.pinno))
                    {
                        mobileverified = "0";
                        pinno = validateno.pinno;
                        phvalidatetxt = "Please verify your phone number You will receive a message containing 4 digit code on your Phone No(" + Selectcountry + ") -" + Mobileno + ".Enter the 4 digit code in the space provided.";
                        phvalidatetxtvisible = true;
                    }
                    else if (validateno.result == "true")
                    {
                        // pinno = "";
                        mobileverified = "1";
                        validatephnovisible = false;
                        phvalidatetxtvisible = false;
                        Sbmtuserdetails();
                    }
                    else if (validateno.result == "false")
                    {
                        //   pinno = "";
                        dialogs.Toast("Enter a valid code");
                    }
                    else
                    {
                        //  pinno = "";
                        mobileverified = "0";
                        dialogs.Toast("Couldn't verify...try later");
                    }
                    dialogs.HideLoading();
                }

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                dialogs.HideLoading();
            }
        }
        public void changenum()
        {
            validatephnovisible = true;
            phvalidatetxtvisible = false;
        }
        public async void Sbmtuserdetails()
        {
            try
            {
                var baseApi = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                var currentpage = GetCurrentPage();
                validateleadform();
                if (validate == true)
                {
                    _Dialog.ShowLoading("");
                    string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                    data.userpid = Commonsettings.UserPid;
                    data.name = UName;
                    data.email = UEmail;
                    data.phoneno = Mobileno;
                    data.countrycode = Selectcountry;
                    data.ip = ipaddress;
                    data.ismobileverified = mobileverified;
                    data.postedvia = Commonsettings.UserMobileOS;
                    _Dialog.HideLoading();
                    await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Resumepkgs(data));
                    // data.deviceid = Commonsettings.UserDeviceId;
                }
            }
            catch(Exception ex)
            {
                _Dialog.HideLoading();
            }
        }

        bool validate = true;
        public bool validateleadform()
        {
            var currentpage = GetCurrentPage();
            validate = true;

            if (UName.Trim().Length == 0)
            {
                validate = false;
                Entry myEntry = currentpage.FindByName<Entry>("lblname");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Please enter your name";
                myEntry.PlaceholderColor = Color.Red;


            }
            else if (!CheckAplhaOnly(UName))
            {
                validate = false;
                Entry myEntry = currentpage.FindByName<Entry>("lblname");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Please enter a valid name";
                myEntry.PlaceholderColor = Color.Red;


            }
            else if (UEmail.Trim().Length == 0)
            {
                validate = false;
                Entry myEntry = currentpage.FindByName<Entry>("lblemail");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Please enter your email";
                myEntry.PlaceholderColor = Color.Red;

            }
            else if (!CheckValidEmail(UEmail))
            {
                validate = false;
                Entry myEntry = currentpage.FindByName<Entry>("lblemail");
                myEntry.Text = "";
                myEntry.Focus();
                myEntry.Placeholder = "Please enter valid email id";
                myEntry.PlaceholderColor = Color.Red;

            }
            else if (string.IsNullOrEmpty(Mobileno))
            {
                validate = false;
                Entry myEntry = currentpage.FindByName<Entry>("lblph");
                myEntry.Focus();
                myEntry.Placeholder = "Enter your mobile number";
                myEntry.PlaceholderColor = Color.Red;
            }
            else if (Mobileno.Length < 10)
            {
                validate = false;
                Entry myEntry = currentpage.FindByName<Entry>("lblph");
                myEntry.Text = "";
                myEntry.Focus();
                myEntry.Placeholder = "Please enter a 10-digit mobile number";
                myEntry.PlaceholderColor = Color.Red;
            }
            else if (!CheckValidPhone(Mobileno))
            {
                Entry myEntry = currentpage.FindByName<Entry>("lblph");
                myEntry.Text = "";
                myEntry.Focus();
                validate = false;
                myEntry.Placeholder = "Please enter valid mobile number";
                myEntry.PlaceholderColor = Color.Red;
            }


            return validate;
        }

        bool CheckAplhaOnly(string inputString)
        {
            return Regex.IsMatch(inputString, "^[a-zA-Z \\.-]+$");
        }

        bool CheckValidEmail(string inputEmail)
        {
            return Regex.IsMatch(inputEmail, "[\\w\\.-]+@([\\w\\-]+\\.)+[A-Z]{2,4}$", RegexOptions.IgnoreCase);
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

        public string _name = "";
        public string UName
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UName)));

            }
        }

        public string _email = "";
        public string UEmail
        {
            get { return _email; }
            set
            {
                _email = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UEmail)));

            }
        }

        public string _mobileno = "";
        public string Mobileno
        {
            get { return _mobileno; }
            set
            {
                _mobileno = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Mobileno)));

            }
        }
        private string _Selectcountry = "+1";
        public string Selectcountry
        {
            get { return _Selectcountry; }
            set { _Selectcountry = value; }
        }
        public string _otp = "";
        public string OTP
        {
            get { return _otp; }
            set
            {
                _otp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OTP)));
            }
        }

        private string zipcode = "", statecode = "", state = "", city = "", latitude = "", longitude = "", country = "";
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
                    if (SelTrainingcity != _trcity)
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
        public bool _cityvisible = false;
        public bool CityVisible
        {
            get { return _cityvisible; }
            set
            {
                if (_cityvisible != value)
                {
                    _cityvisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CityVisible)));
                }

            }
        }
        public async void ajaxcity(string city)
        {
            Selcity = "";
            zipcode = "";
            var nsAPI = RestService.For<ILeadformService>(Commonsettings.TechjobsAPI);
            Cityajaxlist list = await nsAPI.Getcityajax(city, "all");
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
            zipcode = model.zipcode;
            statecode = model.statecode;
            state = model.statename;
            city = model.city;
            country = model.country;
            latitude = model.latitude;
            longitude = model.longitude;
            CityVisible = false;
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
                if (_tempbusinessname != _businessname)
                    ajaxbusiness(_businessname);
            }
        }

        public string _indname = "";
        public string _tempindname = "";
        public string IndName
        {
            get { return _indname; }
            set
            {
                _indname = value;
                OnPropertyChanged(nameof(IndName));
                if (_tempindname != _indname)
                    ajaxindustry(_indname);
            }
        }
        private string _industryid = "";
        public string industryid
        {
            get { return _industryid; }
            set { _industryid = value;OnPropertyChanged(nameof(industryid)); }
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
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async void GetgoogleAddress()
        {
            try
            {
                ZipCode = "";
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
            ZipCode = pt.Description;
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
        public void Sbmtbizdetails()
        {
            if (Validations())
            {
                data.businessname = BusinessName;
                data.address = BusinessAddress;
                data.industry = IndName;
                data.zipcode = zipcode;
                data.city = city;
                data.statecode = statecode;
                data.country = country;
                data.postedvia = Commonsettings.UserMobileOS;
                //data.deviceid = Commonsettings.UserDeviceId;
                var currentpage = GetCurrentPage();
                currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Resumepkgcontactdetails(data));
            }

        }
        public bool Validations()
        {
            bool result = true;
            if (string.IsNullOrEmpty(BusinessName) || BusinessName.Trim().Length==0)
            {
                _Dialog.Toast("Enter business name");
                //var entry = CurrentPage.FindByName<Entry>("ubizname");
                //entry.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(IndName))
            {
                _Dialog.Toast("Enter industry name");
                //var entry = CurrentPage.FindByName<Entry>("ubizname");
                //entry.Focus();
                return false;
            }
            if (!string.IsNullOrEmpty(IndName))
            {
                if(string.IsNullOrEmpty(industryid))
                {
                    _Dialog.Toast("Please Select from industry list only.");
                    return false;
                }
            }
            if (string.IsNullOrEmpty(Selcity) || Selcity.Trim().Length == 0 || string.IsNullOrEmpty(zipcode))
            {
                _Dialog.Toast("Select city name");
                return false;
            }
            if (string.IsNullOrEmpty(_tempBusinessAddress) || string.IsNullOrEmpty(BusinessAddress)||string.IsNullOrEmpty(ZipCode))
            {
                _Dialog.Toast("Select business address");
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

        public List<Industries> _indajax { get; set; }
        public List<Industries> Indajax
        {
            get
            {
                return _indajax;
            }
            set
            {
                _indajax = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Indajax)));
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

        public async void ajaxindustry(string srch)
        {
            try
            {
                industryid = "";
                var baseApi = RestService.For<IJobPayment>(Commonsettings.LocaljobsAPI);
                Industrylist list = await baseApi.Getindustries(srch);
                Indajax = list.ROW_DATA;
                if (list.ROW_DATA.Count() > 0 && list.ROW_DATA != null)
                {
                    var CurrentPage = GetCurrentPage();
                    var indstack = CurrentPage.FindByName<StackLayout>("stackind");
                    indstack.IsVisible = true;
                }
            }
            catch(Exception ex)
            {

            }

        }

        public void Selectindustry(Industries biz)
        {
            var CurrentPage = GetCurrentPage();
            var indstack = CurrentPage.FindByName<StackLayout>("stackind");
            indstack.IsVisible = false;
            industryid = biz.featurename;
            _tempindname = biz.featurename;
            _indname = biz.featurename;
            IndName = biz.featurename;
        }

        public void Closebiz()
        {
            var CurrentPage = GetCurrentPage();
            var bizstack = CurrentPage.FindByName<StackLayout>("stackbiz");
            var indstack = CurrentPage.FindByName<StackLayout>("stackind");
            indstack.IsVisible = false;
            bizstack.IsVisible = false;
        }

        public void ChangeRecruiter()
        {
            RecImg = "RadioButtonChecked.png";
            ConsuImg = "RadioButtonUnChecked.png";
            data.usertypeid = "1";
        }
        public void ChangeConsultant()
        {
            RecImg = "RadioButtonUnChecked.png";
            ConsuImg = "RadioButtonChecked.png";
            data.usertypeid = "2";
        }
    }
}
