using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using Xamarin.Forms;
using Acr.UserDialogs;
using NRIApp.Helpers;
using Refit;
using NRIApp.DayCare.Features.Post.Interface;
using NRIApp.DayCare.Features.Post.Models;
using System.Windows.Input;
using Plugin.Connectivity;

namespace NRIApp.DayCare.Features.Post.ViewModels
{
    public class OTP_VM:INotifyPropertyChanged
    {
        IUserDialogs dialogs = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand Otpcommand { get; set; }
        public ICommand ResendOtpcommand { get; set; }
        public ICommand Openmobilenocommand { get; set; }
        public ICommand Editphnocommand { get; set; }

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

        public bool _visible = false;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Visible)));

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
            set { _Selectcountry = value;OnPropertyChanged(Selectcountry); }
        }

        public bool validation()
        {
            bool result = true;
            if(Selectcountry.Trim().Length==0)
            {
                dialogs.Toast("Please select country code");
                result = false;
                return false;
            }
            if (Mobileno.Trim().Length == 0)
            {
                dialogs.Toast("Please select country code");
                result = false;
                return false;
            }
            return result;
        }
        Daycareposting post = new Daycareposting();
        public OTP_VM(Daycareposting postdata)
        {
            post=postdata;
            Otpcommand = new Command(submitOTP);
            ResendOtpcommand = new Command(resendotp_dc);
            Openmobilenocommand = new Command(Openmobileno);
            Editphnocommand = new Command(editphno);
        }
        public void Openmobileno()
        {
            StackVisible = false;
            Visible = true;
        }
        public async void editphno()
        {
            try
            {
                var connected = CrossConnectivity.Current.IsConnected;
                if (connected == true)
                {
                    dialogs.ShowLoading("", MaskType.Black);
                    var currentpage = GetCurrentPage();
                    var nsAPI = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                    Daycareposting_Result list = await nsAPI.Editphno(post.businessid, Mobileno, Selectcountry);
                    if (list.otpno != "")
                    {
                        OTP = list.pinno;
                        Respid = list.businessid;
                        dialogs.HideLoading();
                        dialogs.Toast("OTP has been sent to your mobile number");
                    }
                    else
                    {
                        dialogs.HideLoading();
                        dialogs.Toast("Please try again");
                    }
                    StackVisible = true;
                    Visible = false;
                }
                else
                    dialogs.Toast("Kindly check your internet connection");
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        public async void resendotp_dc()
        {
            try
            {
                var connected = CrossConnectivity.Current.IsConnected;
                if (connected == true)
                {
                    dialogs.ShowLoading("", MaskType.Black);
                    var currentpage = GetCurrentPage();
                    var nsAPI = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                    Daycareposting_Result list = await nsAPI.Resendotp(Mobileno, Selectcountry);
                 
                    if (list.otpno != "")
                    {
                        Visible = false;
                        OTP = list.otpno;
                        dialogs.HideLoading();
                        dialogs.Toast("OTP has been sent to your mobile number");
                    }
                    else
                    {
                        Visible = false;
                        dialogs.HideLoading();
                        dialogs.Toast("Please try again");
                    }
                }
                else
                    dialogs.Toast("Kindly check your internet connection");
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        Features.List.Models.response_data response = new Features.List.Models.response_data();
        public async void submitOTP()
        {
            try
            {
                var connected = CrossConnectivity.Current.IsConnected;
                if (connected == true)
                {
                    if (OTPText == OTP)
                    {
                        dialogs.ShowLoading("", MaskType.Black);
                        var currentpage = GetCurrentPage();
                        var DCpostAPI = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                        var postresponse = await DCpostAPI.Verifyotp(post.emailid, post.ip, Mobileno, Selectcountry, Commonsettings.UserPid, post.businessid, Commonsettings.UserMobileOS, Commonsettings.UserDeviceId);
                        if(post.isresponse=="response")
                        {
                            //dialogs.HideLoading();
                            //await currentpage.Navigation.PopAsync();
                            response.contentid = post.businessid;
                            response.contactname = post.contactName;
                            response.emailid = post.emailid;
                            response.ip = post.ip;
                            response.phoneno = post.ContactNo;
                            response.city = Commonsettings.Usercity;
                            if (Commonsettings.UserMobileOS.ToLower().Contains("iphone"))
                            {
                                response.postedviaid = "194";
                                response.postedvia = "iphone";
                            }
                            else
                            {
                                response.postedviaid = "197";
                                response.postedvia = "android";
                            }
                            response.description = post.description;
                            response.responsecategorytype =post.responsecategorytype;
                            response.availablefrom = post.availablefrom;
                            response.premiumad = post.premiumad;
                            response.newclickurl = post.newclickurl;
                            var dcresponseapi = RestService.For<Features.List.Interface.IDClistings>(Commonsettings.DaycareAPI);
                            Features.List.Models.result_info list = await dcresponseapi.responsesubmit(response);
                            dialogs.HideLoading();
                            //if (list.resultinformation == "exist")
                            //{
                            //    dialogs.Toast("Already responded to this ad");
                            //}
                            //else
                            //{
                                var currntpage = GetCurrentPage();
                                await currntpage.Navigation.PushAsync(new NRIApp.DayCare.Features.List.Views.Thankyou_DC());
                            //}
                            //var vm = new NRIApp.DayCare.Features.List.ViewModels.ResponseForm_VM();
                        }
                        else
                        {
                            if (postresponse.ispayment == "1")
                                await currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Package(post));
                            else
                                await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Spammer());
                        }
                    }
                    else
                    {
                        dialogs.HideLoading();
                        dialogs.Toast("Enter valid OTP");
                    }
                }
                else
                    dialogs.Toast("Kindly check your internet connection");
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
            }

        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page GetCurrentPage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}
