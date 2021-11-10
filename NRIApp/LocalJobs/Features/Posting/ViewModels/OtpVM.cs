using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Acr.UserDialogs;
using Plugin.Connectivity;
using NRIApp.Helpers;
using Refit;
using NRIApp.LocalJobs.Features.Posting.Interfaces;
using NRIApp.LocalJobs.Features.Posting.Models;
using Xamarin.Forms;
using System.Linq;
using System.Windows.Input;

namespace NRIApp.LocalJobs.Features.Posting.ViewModels
{
   public class OtpVM :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;
        public ICommand Otpcommand { get; set; }
        public ICommand ResendOtpcommand { get; set; }
        public ICommand Openmobilenocommand { get; set; }
        public ICommand Editphnocommand { get; set; }

        Postingdata pst = new Postingdata();
        public  OtpVM(Postingdata data)
        {
            pst = data;
            Otpcommand = new Command(otpsubmit);
            ResendOtpcommand = new Command(resendotp);
            Openmobilenocommand = new Command(Openmobileno);
            Editphnocommand = new Command(editphno);
        }

        public void Openmobileno()
        {
            StackVisible = false;
            Visible = true;
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
            set { _Selectcountry = value; }
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
        public async void resendotp()
        {
            try
            {
                var connected = CrossConnectivity.Current.IsConnected;
                if (connected == true)
                {
                    dialog.ShowLoading("", MaskType.Black);
                    var currentpage = GetCurrentPage();
                    var nsAPI = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                    postingsbmtdetails list = await nsAPI.Resendotp(Mobileno, Selectcountry);
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
                dialog.HideLoading();
            }

        }

        public async void editphno()
        {
           try
            {
                var connected = CrossConnectivity.Current.IsConnected;
                if (connected == true)
                {
                    dialog.ShowLoading("", MaskType.Black);
                    var currentpage = GetCurrentPage();
                    var nsAPI = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                    //  postingsbmtdetails list = await nsAPI.Editphno(Respid, Mobileno, Selectcountry);
                    postingsbmtdetails list = await nsAPI.Editphno(pst.respid, Mobileno, Selectcountry);
                    if (list.otpno != "")
                    {
                        OTP = list.otpno;
                        Respid = list.adid;
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
                else
                    dialog.Toast("Kindly check your internet connection");
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }


        }

        public async void otpsubmit()
        {
           try
            {
                var connected = CrossConnectivity.Current.IsConnected;
                if (connected == true)
                {
                    if (OTPText == OTP)
                    {
                        dialog.ShowLoading("", MaskType.Black);
                        var currentpage = GetCurrentPage();
                        var nsAPI = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                        postingsbmtdetails list = await nsAPI.otpsubmitForm(pst.respid, Commonsettings.UserEmail, Commonsettings.UserPid, Mobileno, Selectcountry, Commonsettings.UserDeviceId, Commonsettings.UserMobileOS, pst.ip);
                        if (list.ispayment == "1")
                            await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Packages(pst));
                        else
                            await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Spammer());

                    }

                    else
                    {
                        dialog.Toast("Enter valid OTP");
                    }
                }
                else
                    dialog.Toast("Kindly check your internet connection");
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
    }
}
