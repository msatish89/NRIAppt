using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Jobresume.Interfaces;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using Plugin.Connectivity;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NRIApp.LocalJobs.Features.Jobresume.ViewModels
{
    public class ContactJobseeker_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;
        string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        string Namepattern = "^[0-9A-Za-z ]+$";
        public Command Applynowcmd { get; set; }

        private string _ContactName = "";
        public string ContactName
        {
            get { return _ContactName; }
            set { _ContactName = value; OnPropertyChanged(nameof(ContactName)); }
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
        private string _Countrycode = "+1";
        public string Countrycode
        {
            get { return _Countrycode; }
            set { _Countrycode = value; OnPropertyChanged(nameof(Countrycode)); }
        }
        private string _contactdetail = "I am interested in your Job Profile.";
        public string ContactDetail
        {
            get { return _contactdetail; }
            set { _contactdetail = value; OnPropertyChanged(); }
        }
     
        public bool validation()
        {
            bool result = true;
            var currentpage = Getcurrentpage();
            if (string.IsNullOrEmpty(ContactName) || ContactName.Trim().Length == 0)
            {
                dialog.Toast("Please enter your name");
                return false;
            }
            if (!CheckAplhaOnly(ContactName))
            {
                dialog.Toast("Please enter a valid name");
                return false;
            }
            if (ContactEmail.Trim().Length == 0)
            {
                dialog.Toast("Please enter your email");
                return false;
            }
            if (!CheckValidEmail(ContactEmail))
            {
                dialog.Toast("Please enter a valid email id");
                return false;
            }
            if (!Regex.IsMatch(ContactEmail, Emailpattern))
            {
                dialog.Toast("Enter Proper Email");
                return false;
            }
            if (string.IsNullOrEmpty(ContactPhone) || ContactPhone.Trim().Length == 0)
            {
                dialog.Toast("Enter Contact Number");
                return false;
            }
            if (ContactPhone.Length < 10)
            {
                dialog.Toast("Minimum 10 Digits required");
                return false;
            }
            else if (!CheckValidPhone(ContactPhone))
            {
                dialog.Toast("Please enter valid mobile number");
                return false;
            }
           
            if (string.IsNullOrEmpty(ContactDetail))
            {
                dialog.Toast("Short Description field is required.");
                return false;
            }
            if (ContactDetail.Length > 500)
            {
                dialog.Toast("Short Description field is required.");
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
        bool CheckAplhaOnly(string inputString)
        {
            return Regex.IsMatch(inputString, "^[a-zA-Z \\.-]+$");
        }

        bool CheckValidEmail(string inputEmail)
        {
            return Regex.IsMatch(inputEmail, "[\\w\\.-]+@([\\w\\-]+\\.)+[A-Z]{2,4}$", RegexOptions.IgnoreCase);
        }
        
        public ContactJobseeker_VM(string adid,string premiumad)
        {
            try
            {
                if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                    ContactEmail = Commonsettings.UserEmail;
                if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                    ContactName = Commonsettings.UserName;
                if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                    ContactPhone = Commonsettings.UserMobileno;
             
                Applynowcmd = new Command(async () => await applysubmit(adid,premiumad));
               
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
            }
        }

        Detail.Models.postresponse postdata = new Detail.Models.postresponse();
        public async Task applysubmit(string adid,string premiumad)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            try
            {
                //var currentpage = Getcurrentpage();
                //await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Detail.Views.Responsetq());

                if (connected == true)
                {
                    if (validation())
                    {
                        dialog.ShowLoading("", null);

                        postdata.contentid = adid;
                        postdata.contributor = ContactName;
                        postdata.email = ContactEmail;
                        postdata.mobileno = ContactPhone;
                        postdata.countrycode = Countrycode;
                        postdata.shortdesc = ContactDetail;
                        postdata.ipaddress = ipaddress;
                        postdata.responsecategroy = "20";
                        postdata.ipuserlat = Commonsettings.UserLat;
                        postdata.ipuserlong = Commonsettings.UserLong;
                        postdata.zipcode = Commonsettings.Userzipcode;
                        postdata.city = Commonsettings.Usercity;
                        postdata.userpid = Commonsettings.UserPid;
                        postdata.country = Commonsettings.Usercountry;
                        if(premiumad!="0")
                        {
                            postdata.premiumad = Convert.ToInt32(premiumad);
                        }
                        else
                        {
                            postdata.premiumad = 0;
                        }
                        //postdata.relocate = relocatetype;
                        postdata.postedvia = Commonsettings.UserMobileOS;
                        if (Commonsettings.UserMobileOS == "iphone")
                        {
                            postdata.postedviaid = "188";
                        }
                        else
                        {
                            postdata.postedviaid = "191";
                        }
                        var postres = RestService.For<IJobresume>(Commonsettings.LocaljobsAPI);
                        var resresult = await postres.postjobresponse(postdata);
                        if (resresult != null)
                        {
                            if (resresult.resultinfo == "success")
                            {
                                var currentpage = Getcurrentpage();
                                await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Jobresume.Views.Seekerresponsetq());
                            }
                            else if (resresult.resultinfo == "exists" || resresult.resultinfo== "exist")
                            {
                                dialog.Toast("Already contacted this profile");
                            }
                            else
                            {
                                dialog.Toast("Some problem occurred,Please try again later...");
                            }
                        }
                        dialog.HideLoading();
                    }
                }
                else
                {
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
