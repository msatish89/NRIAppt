using System;
using System.Collections.Generic;
using System.Text;
using Refit;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using System.Runtime.CompilerServices;
using NRIApp.Helpers;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using NRIApp.Techjobs.Features.LeadForm.Models;
using System.Text.RegularExpressions;
using System.Net;
using Acr.UserDialogs;
using Plugin.Connectivity;

namespace NRIApp.Techjobs.Features.LeadForm.ViewModels
{
    public class Leadformviewmodel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand MDatacommand { get; set; }
        public ICommand Leadcommand { get; set; }
        public ICommand Cityajaxcommand { get; set; }
        public ICommand Otpcommand { get; set; }
        public ICommand ResendOtpcommand { get; set; }
        public ICommand Trainingmodecommand { get; set; }
        public ICommand Openmobilenocommand { get; set; }
        public ICommand Editphnocommand { get; set; }
        public ICommand SkipLeadformcommand { get; set; }
        //private static void Toast()
        //{
        //    ToastConfig toastConfig = new ToastConfig("Toast");
        //    toastConfig.SetDuration(3000);
        //    toastConfig.SetPosition(ToastPosition.Top);
        //    UserDialogs.Instance.Toast(toastConfig);
        //}
        IUserDialogs dialog = UserDialogs.Instance;
        string Trainingmode = "2";
        public Leadformviewmodel()
        {
            
            MDatacommand = new Command<Modules>(Selectmodule);
            Cityajaxcommand = new Command<Cityajax>(Selectajaxcity);
            Leadcommand = new Command(Leadsubmit);
            Otpcommand = new Command(otpsubmit);
            ResendOtpcommand = new Command(resendotp);
            Trainingmodecommand = new Command(ChangeTrainingmode);
            Openmobilenocommand = new Command(Openmobileno);
            Editphnocommand = new Command(editphno);
            SkipLeadformcommand = new Command(SkipLeadform);
            if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                UEmail = Commonsettings.UserEmail;
            if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                UName = Commonsettings.UserName;
            if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                Mobileno = Commonsettings.UserMobileno;
            if (Commonsettings.Usercity != null && Commonsettings.Usercity != "")
            {
                SelTrainingcity = Commonsettings.Usercity +", "+Commonsettings.Userstatecode;
                Trainingcity = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                Selcity = Commonsettings.Usercityurl;
            }
        }

        public string _bizid = "0";
        public string bizid
        {
            get { return _bizid; }
            set
            {
                _bizid = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(bizid)));

            }
        }
        public string _onlineimg = "RadioButtonChecked.png";
        public string Onlineimg
        {
            get { return _onlineimg; }
            set
            {
                _onlineimg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Onlineimg)));

            }
        }
        private string _Selectcountry = "+1";
        public string Selectcountry
        {
            get { return _Selectcountry; }
            set { _Selectcountry = value; }
        }

        public string _inclassimg = "RadioButtonUnChecked.png";
        public string Inclassimg
        {
            get { return _inclassimg; }
            set
            {
                _inclassimg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Inclassimg)));

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

        public bool _visible = false;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Visible)));
                }

            }
        }

        public bool _listviewvisible = false;
        public bool Listviewvisible
        {
            get { return _listviewvisible; }
            set
            {
                _listviewvisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Listviewvisible)));

            }
        }

        public bool _stackvisible = false;
        public bool StackVisible
        {
            get { return _stackvisible; }
            set
            {
                    _stackvisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StackVisible)));
            }
        }


        public List<Modules> _moduleajax { get; set; }
        public List<Modules> ModulesAjax
        {
            get
            {
                return _moduleajax;
            }
            set
            {
                _moduleajax = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ModulesAjax)));
            }
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


        public string _module { get; set; }
        public string Module
        {
            get { return _module; }
            set
            {

                if (_module != value)
                {
                    _module = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Module)));
                    if (SelModule != _module)
                        ajaxmodule(_module);
                }

            }
        }

        public string _selmodule { get; set; }
        public string SelModule
        {
            get { return _selmodule; }
            set
            {

                if (_selmodule != value)
                {
                    _selmodule = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelModule)));
                }

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

        public string _name = "";
        public string UName
        {
            get { return _name; }
            set
            {

                if (_name != value)
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UName)));
                }

            }
        }

        public string _email = "";
        public string UEmail
        {
            get { return _email; }
            set
            {

                if (_email != value)
                {
                    _email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UEmail)));
                }

            }
        }

        public string _mobileno = "";
        public string Mobileno
        {
            get { return _mobileno; }
            set
            {

                if (_mobileno != value)
                {
                    _mobileno = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Mobileno)));
                }

            }
        }

        public string _moduleid = "";
        public string Moduleid
        {
            get { return _moduleid; }
            set
            {

                if (_moduleid != value)
                {
                    _moduleid = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Moduleid)));
                }

            }
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

        public string _namecolor = "Gray";
        public string UNamecolor
        {
            get { return _namecolor; }
            set
            {

                if (_namecolor != value)
                {
                    _namecolor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UNamecolor)));
                }

            }
        }

        public void Selectmodule(Modules model)
        {
            SelModule = model.module;
            Module = model.module;
            Moduleid = model.contentid;
            Listviewvisible = false;
        }

        public async void ajaxmodule(string mod)
        {

                Moduleid = "";
                var nsAPI = RestService.For<ILeadformService>(Commonsettings.TechjobsAPI);
                Moduleajax list = await nsAPI.Getmoduleajax(mod);
                ModulesAjax = list.ROW_DATA;
              if(ModulesAjax !=null)
                Listviewvisible = true;
             else
                Listviewvisible = false;
        }

        public async void ajaxcity(string city)
        {
            Selcity = "";
            var nsAPI = RestService.For<ILeadformService>(Commonsettings.TechjobsAPI);
            Cityajaxlist list = await nsAPI.Getcityajax(city,"all");
            CityAjax = list.ROW_DS;
            if(CityAjax!=null)
            {
                DependencyService.Get<IKeyboardHelper>().HideKeyboard();
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

        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }


        bool CheckAplhaOnly(string inputString)
        {
            return Regex.IsMatch(inputString, "^[a-zA-Z ]+$");
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
        
        public async void Leadsubmit()
        {
            var currentpage = GetCurrentPage();
            validateleadform();
            var connected = CrossConnectivity.Current.IsConnected;
            try
            {
                if (connected == true)
                {
                    if (validate == true)
                    {
                        dialog.ShowLoading("", MaskType.Black);
                        string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                        var baseApi = RestService.For<ILeadformService>(Commonsettings.TechjobsAPI);
                        Leaddetails lead = new Leaddetails()
                        {
                            name = UName,
                            email = UEmail,
                            mobileno = Mobileno,
                            businessid = bizid,
                            shortdesc = "I am looking for " + Module + " training",
                            trainingcity = Selcity,
                            userpid = Commonsettings.UserPid,
                            city = Commonsettings.Usercity,
                            zipcode = Commonsettings.Userzipcode.ToString(),
                            moduleid = Moduleid,
                            trainingmode = Trainingmode,
                            countrycode = Selectcountry,
                            jobmodule = "",
                            postedvia = Commonsettings.UserMobileOS,
                            deviceid = Commonsettings.UserDeviceId,
                            ip = ipaddress
                        };
                        leadsbmtdetails details = await baseApi.SubmitLeadForm(lead);
                        if (details.otpsend == "otpsend")
                        {
                            OTP = details.otpno;
                            Respid = details.respid;
                            Mobileno = details.mobileno;
                            Selectcountry = "+" + details.countrycode;
                            dialog.HideLoading();
                            await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.LeadForm.Views.OTPform(OTP, Respid, details.mobileno, Selectcountry));
                        }
                        else
                        {
                            Respid = details.respid;
                            dialog.HideLoading();
                            await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.LeadForm.Views.Thankyou(Respid));
                        }

                    }
                }
                else
                    dialog.Toast("Kindly check your internet connection");
            }
          catch(Exception ex)
            {
                string exe = ex.Message.ToString();
                dialog.HideLoading();
                var connect = CrossConnectivity.Current.IsConnected;
                if (connect == false)
                    dialog.Toast("Kindly check your internet connection");
            }



        }

        public void ChangeTrainingmode()
        {
            if(Trainingmode=="2")
            {
                Inclassimg = "RadioButtonChecked.png";
                Onlineimg = "RadioButtonUnChecked.png";
                Trainingmode = "1";
                StackVisible = true;
            }
            else
            {
                Inclassimg = "RadioButtonUnChecked.png";
                Onlineimg = "RadioButtonChecked.png";
                Trainingmode = "2";
                CityVisible = false;
                StackVisible = false;
            }
        }

        public async void resendotp()
        {
            var connected = CrossConnectivity.Current.IsConnected;
            try
            {
            if (connected == true)
            {
                dialog.ShowLoading("", MaskType.Black);
                var currentpage = GetCurrentPage();
                var nsAPI = RestService.For<ILeadformService>(Commonsettings.TechjobsAPI);
                leadsbmtdetails list = await nsAPI.Resendotp(Respid);
                if (list.otpno != "")
                {
                    Visible = false;
                    OTP = list.otpno;
                    dialog.HideLoading();
                    dialog.Toast("OTP has been sent to your mobile number");

                }
                else
                {
                    Visible = false;
                    dialog.HideLoading();
                    dialog.Toast("Please try again");
                }
            }
            else
                dialog.Toast("Kindly check your internet connection");
            }
            catch (Exception ex)
            {
                string exe = ex.Message.ToString();
                dialog.HideLoading();
            }

        }

        public async void editphno()
        {
            var connected = CrossConnectivity.Current.IsConnected;
            bool phvalidate = true;
            if (connected == true)
            {
                var currentpage = GetCurrentPage();
                 if (string.IsNullOrEmpty(Mobileno))
                {
                    phvalidate = false;
                    Entry myEntry = currentpage.FindByName<Entry>("lblph");
                    myEntry.Focus();
                    myEntry.Placeholder = "Enter your mobile number";
                    myEntry.PlaceholderColor = Color.Red;
                    // dialog.Toast("Enter your mobile number");
                }
                else if (Mobileno.Length < 10)
                {
                    phvalidate = false;
                    Entry myEntry = currentpage.FindByName<Entry>("lblph");
                    myEntry.Text = "";
                    myEntry.Focus();
                    myEntry.Placeholder = "Enter a 10-digit mobile number";
                    myEntry.PlaceholderColor = Color.Red;
                    // dialog.Toast("Please enter a 10-digit mobile number");
                }
                else if (!CheckValidPhone(Mobileno))
                {
                    Entry myEntry = currentpage.FindByName<Entry>("lblph");
                    myEntry.Text = "";
                    myEntry.Focus();
                    phvalidate = false;
                    myEntry.Placeholder = "Enter valid mobile number";
                    myEntry.PlaceholderColor = Color.Red;
                    // dialog.Toast("Please enter valid mobile number");
                }
                 if(phvalidate == true)
                {
                    dialog.ShowLoading("", MaskType.Black);
                    var nsAPI = RestService.For<ILeadformService>(Commonsettings.TechjobsAPI);
                    leadsbmtdetails list = await nsAPI.Editphno(Respid, Mobileno, Selectcountry);
                    if (list.otpno != "")
                    {
                        OTP = list.otpno;
                        Respid = list.respid;
                        dialog.HideLoading();
                        dialog.Toast("OTP has been sent to your mobile number");

                    }
                    else
                    {
                        dialog.HideLoading();
                        dialog.Toast("Please try again");
                    }
                    StackVisible = true;
                    Visible = false;
                }
             
            }
            else
                dialog.Toast("Kindly check your internet connection");


        }

        public async void otpsubmit()
        {
            var connected = CrossConnectivity.Current.IsConnected;
            try
            {
            if (connected == true)
            {
                if (OTPText == OTP)
                {
                    dialog.ShowLoading("", MaskType.Black);
                    var currentpage = GetCurrentPage();
                    var nsAPI = RestService.For<ILeadformService>(Commonsettings.TechjobsAPI);
                    leadsbmtdetails list = await nsAPI.otpsubmitForm(Respid,Commonsettings.UserDeviceId,Commonsettings.UserMobileOS);
                    await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.LeadForm.Views.Thankyou(list.respid));

                }

                else
                {
                    dialog.Toast("Enter valid OTP");
                }
            }
            else
                dialog.Toast("Kindly check your internet connection");
            }
            catch (Exception ex)
            {
                string exe = ex.Message.ToString();
                dialog.HideLoading();
                if (connected == false)
                    dialog.Toast("Kindly check your internet connection");
            }

        }
        bool validate = true;
        public bool validateleadform()
        {
            var currentpage = GetCurrentPage();
            validate = true;
              if (Moduleid == "")
            {
                validate = false;
                Entry myEntry = currentpage.FindByName<Entry>("txtmod");
                myEntry.Text = "";
                myEntry.Placeholder = "Please select the course";
                myEntry.PlaceholderColor = Color.Red;
                myEntry.Focus();
                dialog.Toast("Please select the course");
            }
            else if (Trainingmode == "1" && string.IsNullOrEmpty(Selcity))
            {
                    Entry myEntry = currentpage.FindByName<Entry>("txtajxcity");
                myEntry.Text = "";
                myEntry.Placeholder = "Select city from the list";
                myEntry.PlaceholderColor = Color.Red;
                myEntry.Focus();
                    validate = false;
                  //  dialog.Toast("Please select city from the list");

            }
            else if (UName.Trim().Length == 0)
            {
                validate = false;
               // UNamecolor = "Red";
                Entry myEntry = currentpage.FindByName<Entry>("lblname");
                myEntry.Focus();
                myEntry.Placeholder = "Enter your name";
                myEntry.PlaceholderColor = Color.Red;
                dialog.Toast("Please enter your name");

            }
            else if (!CheckAplhaOnly(UName))
            {
                validate = false;
                Entry myEntry = currentpage.FindByName<Entry>("lblname");
                myEntry.Focus();
                myEntry.Text = "";
                myEntry.Placeholder = "Enter a valid name";
                myEntry.PlaceholderColor = Color.Red;
                dialog.Toast("Please enter a valid name");

            }
            else if (UEmail.Trim().Length == 0)
            {
                validate = false;
                Entry myEntry = currentpage.FindByName<Entry>("lblemail");
                myEntry.Focus();
                myEntry.Placeholder = "Enter your email";
                myEntry.PlaceholderColor = Color.Red;
                dialog.Toast("Please enter your email");
            }
            else if (!CheckValidEmail(UEmail))
            {
                validate = false;
                Entry myEntry = currentpage.FindByName<Entry>("lblemail");
                myEntry.Text = "";
                myEntry.Focus();
                myEntry.Placeholder = "Enter valid email id";
                myEntry.PlaceholderColor = Color.Red;
                dialog.Toast("Please enter valid email id");
            }
           

            else if (string.IsNullOrEmpty(Mobileno))
            {
                validate = false;
                Entry myEntry = currentpage.FindByName<Entry>("lblph");
                myEntry.Focus();
                myEntry.Placeholder = "Enter your mobile number";
                myEntry.PlaceholderColor = Color.Red;
                dialog.Toast("Enter your mobile number");
            }
            else if (Mobileno.Length < 10)
            {
                validate = false;
                Entry myEntry = currentpage.FindByName<Entry>("lblph");
                myEntry.Text = "";
                myEntry.Focus();
                myEntry.Placeholder = "Enter a 10-digit mobile number";
                myEntry.PlaceholderColor = Color.Red;
                dialog.Toast("Please enter a 10-digit mobile number");
            }
            else if (!CheckValidPhone(Mobileno))
            {
                Entry myEntry = currentpage.FindByName<Entry>("lblph");
                myEntry.Text = "";
                myEntry.Focus();
                validate = false;
                myEntry.Placeholder = "Enter valid mobile number";
                myEntry.PlaceholderColor = Color.Red;
                dialog.Toast("Please enter valid mobile number");
            }
        

            return validate;
        }

        public void Openmobileno()
        {
            StackVisible = false;
            Visible = true;
        }
        public async void SkipLeadform()
        {
            var currentpage = GetCurrentPage();
            if (Moduleid == "")
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtmod");
                myEntry.Focus();
                dialog.Toast("Please select the course");
            }
           else
            await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.Listing.Views.Modulelisting(Moduleid, Module,"","","",""));
        }
    }
}
