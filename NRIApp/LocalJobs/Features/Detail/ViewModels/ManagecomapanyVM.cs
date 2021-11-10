using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using System.Text.RegularExpressions;
using Refit;
using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Detail.Interfaces;
using NRIApp.LocalJobs.Features.Jobseeker.Models;
using NRIApp.LocalJobs.Features.Listing.Models;
using Plugin.Connectivity;

namespace NRIApp.LocalJobs.Features.Detail.ViewModels
{
    public class ManagecomapanyVM:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialogs = UserDialogs.Instance;
        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        public Command validatephoneno { get; set; }
        public Command verifyphoneno { get; set; }
        public Command resend { get; set; }
        public Command changenumber { get; set; }
        public Command managecompanysubmit { get; set; }

        private string _Countrycode = "+1";
        public string Countrycode
        {
            get { return _Countrycode; }
            set { _Countrycode = value; OnPropertyChanged(nameof(Countrycode)); }
        }
        private string _ContactEmail = "";
        public string ContactEmail
        {
            get { return _ContactEmail; }
            set { _ContactEmail = value; OnPropertyChanged(nameof(ContactEmail)); }
        }
        private string _ContactPhone = "";
        public string ContactPhone
        {
            get { return _ContactPhone; }
            set { _ContactPhone = value; OnPropertyChanged(nameof(ContactPhone)); }
        }
        private string _verifycode = "";
        public string verifycode
        {
            get { return _verifycode; }
            set { _verifycode = value; OnPropertyChanged(nameof(verifycode)); }
        }
        private bool _phvalidatetxtvisible=false;
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

        private string _mobileverified = "0";
        public string mobileverified
        {
            get { return _mobileverified; }
            set { _mobileverified = value; OnPropertyChanged(nameof(mobileverified)); }
        }
        public bool validation()
        {
            bool result = true;
            var currentpage = GetCurrentpage();
            if (string.IsNullOrEmpty(ContactEmail) || ContactEmail.Trim().Length==0)
            {
                dialogs.Toast("Enter Contact Email");
                return false;
            }
            if (!Regex.IsMatch(ContactEmail, Emailpattern))
            {
                dialogs.Toast("Enter Proper Email");
                return false;
            }
            if (string.IsNullOrEmpty(ContactPhone))
            {
                dialogs.Toast("Enter Contact Number");
                return false;
            }
            if (ContactPhone.Length < 10)
            {
                dialogs.Toast("Minimum 10 Digits required");
                return false;
            }
            else if (!CheckValidPhone(ContactPhone))
            {
                dialogs.Toast("Please enter valid mobile number");
                return false;
            }
            return result;
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
       
        public string companyname_url = "";
        Search Sdata = new Search();
        public ManagecomapanyVM(Search cdata)
        {
            try
            {
                dialogs.ShowLoading("", null);
                Sdata = cdata;
                companyname_url = cdata.titleurl;
                if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                    ContactEmail = Commonsettings.UserEmail;
                if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                    ContactPhone = Commonsettings.UserMobileno;

                validatephoneno = new Command(async () => await validatenumber(""));
                verifyphoneno = new Command(async () => await validatenumber("1"));
                resend = new Command(async () => await validatenumber(""));
                changenumber = new Command(changenum);
                managecompanysubmit = new Command(async () => await submit(cdata));
                dialogs.HideLoading();
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        private string _pinno="";
        public string pinno
        {
            get { return _pinno; }
            set { _pinno = value;OnPropertyChanged(nameof(pinno)); }
        }
        public async Task validatenumber(string verify)
        {
            try
            {
                validatephnovisible = false;


                if (validation())
                {
                    dialogs.ShowLoading("", null);
                    
                    var validate = RestService.For<IComapanydata>(Commonsettings.LocaljobsAPI);
                    var validateno = await validate.verifyphone(Countrycode, ContactPhone, verify, verifycode , pinno);
                    //validateno.mode == "0" ||
                    if (!string.IsNullOrEmpty(validateno.pinno))
                    {
                        pinno = validateno.pinno;
                        phvalidatetxt = "Please verify your phone number You will receive a message containing 4 digit code on your Phone No(" + Countrycode + ") -" + ContactPhone + ".Enter the 4 digit code in the space provided.";
                        phvalidatetxtvisible = true;
                    }
                    else if (validateno.result == "true")
                    {
                       // pinno = "";
                        mobileverified = "1";
                        validatephnovisible = false;
                        phvalidatetxtvisible = false;
                        await submit(Sdata);
                    }
                    else if (validateno.result == "false")
                    {
                     //   pinno = "";
                        dialogs.Toast("Enter a valid code");
                    }
                    else
                    {
                      //  pinno = "";
                        dialogs.Toast("Couldn't verify...try later");
                    }
                    dialogs.HideLoading();
                }
                
            }
            catch(Exception ex)
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
        public async Task submit(Search cdata)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            try
            {
                if(connected==true)
                {
                    if (mobileverified == "1")
                    {
                        string businessurl = cdata.titleurl;
                        // businessurl = "iam-business";
                        if (validation())
                        {
                            var mcsubmit = RestService.For<IComapanydata>(Commonsettings.LocaljobsAPI);
                            var mcsubmitdata = await mcsubmit.claimbusiness(ContactEmail, Countrycode.Replace("+", ""), ContactPhone, mobileverified, businessurl);
                            if (mcsubmitdata.dataupdation == "exists")
                            {
                                dialogs.Toast("You have already claimed this business...");
                                validatephnovisible = true;
                                phvalidatetxtvisible = false;
                            }
                            else if (mcsubmitdata.dataupdation == "insert")
                            {
                                var currentpage = GetCurrentpage();
                                currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                                currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                                await currentpage.Navigation.PushAsync(new Views.CompanyProfile(cdata));
                            }
                            else
                            {
                                dialogs.Toast("Some problem occurred...try later");
                                validatephnovisible = true;
                                phvalidatetxtvisible = false;
                            }
                        }

                    }
                    else
                    {
                        dialogs.Toast("validate your mobile number and try again...");
                    }
                }
               else
                {
                        dialogs.Toast("Kindly check your internet connection!");
                }
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
                dialogs.HideLoading();
                if (connected == false)
                    dialogs.Toast("Kindly check your internet connection!");
            }
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page GetCurrentpage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}
