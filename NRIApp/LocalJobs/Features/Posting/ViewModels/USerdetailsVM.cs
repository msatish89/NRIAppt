using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using NRIApp.Helpers;
using System.Windows.Input;
using NRIApp.LocalJobs.Features.Posting.Models;
using Xamarin.Forms;
using System.Linq;
using System.Text.RegularExpressions;
using Refit;
using NRIApp.LocalJobs.Features.Posting.Interfaces;
using Acr.UserDialogs;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using System.Runtime.CompilerServices;
using Plugin.Connectivity;

namespace NRIApp.LocalJobs.Features.Posting.ViewModels
{
   public class USerdetailsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand Sbmtuserdetailscommand { get; set; }
        IUserDialogs dialog = UserDialogs.Instance;
        Postingdata data = new Postingdata();
        public USerdetailsVM(Postingdata pd)
        {
            data = pd;
            Sbmtuserdetailscommand = new Command(Sbmtuserdetails);
            if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                UEmail = Commonsettings.UserEmail;
            if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                UName = Commonsettings.UserName;
            if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                Mobileno = Commonsettings.UserMobileno;      
            
            if(pd.ismyneed=="1")
            {
                Selectcountry = data.contrycode;
                if (!Selectcountry.Contains("+"))
                {
                    Selectcountry = "+"+data.contrycode;
                }
            }
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
            set { _Selectcountry = value; OnPropertyChanged(nameof(Selectcountry)); }
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
        public async void Sbmtuserdetails()
        {
            var connected = CrossConnectivity.Current.IsConnected;
            try
            {
                if(connected==true)
                {
                    var baseApi = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                    var currentpage = GetCurrentPage();
                    validateleadform();
                    if (validate == true)
                    {
                        dialog.ShowLoading("");
                        string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                        data.userpid = Commonsettings.UserPid;
                        data.username = UName;
                        data.useremail = UEmail;
                        data.usermobileno = Mobileno;
                        data.contrycode = Selectcountry;
                        data.ip = ipaddress;
                        data.postedvia = Commonsettings.UserMobileOS;
                        data.deviceid = Commonsettings.UserDeviceId;
                        postingsbmtdetails details = await baseApi.Jobspostingsecondstep(data);
                        data.usertype = details.usertype;
                        Respid = details.adid = data.adid;
                        dialog.HideLoading();
                        if (details.blockstatus == "1")
                        {
                            
                            await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Spammer());
                        }
                        else
                        {
                            if (details.usertype == "2")
                            {
                                await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.JobSuccess(data.adid, details.usertype));
                                //await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Spammer());
                            }
                            else
                            {
                                if(data.mode=="edit")
                                {
                                    await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Spammer());
                                }
                                else
                                {
                                    if (details.otpsend == "otpsend")
                                    {
                                        //details.adid returns null selectcountrycode - ++
                                        OTP = details.otpno;
                                        // Respid = details.adid;
                                        Respid = data.adid;
                                        Mobileno = details.mobileno;
                                        //Selectcountry = "+" + details.countrycode;
                                        Selectcountry = details.countrycode;
                                        dialog.HideLoading();
                                        await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.OTPform(OTP, Respid, details.mobileno, Selectcountry, data));
                                    }
                                    else
                                    {
                                        //dialog.HideLoading();
                                        if (details.ispayment == "1")
                                            await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Packages(data));
                                        else
                                            await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Spammer());
                                    }
                                }
                            }
                        }

                    }
                }
                else
                {
                    dialog.HideLoading();
                    dialog.Toast("Kindly check your internet connection!");
                }
             

            }
            catch (Exception ex)
            {
                dialog.HideLoading();
                if (connected == false)
                    dialog.Toast("Kindly check your internet connection!");
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
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
    }
}
