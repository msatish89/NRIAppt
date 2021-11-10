using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using System.Linq;
using Xamarin.Forms;
using System.ComponentModel;
using Acr.UserDialogs;
using NRIApp.Helpers;
using System.Text.RegularExpressions;
using NRIApp.DayCare.Features.List.Models;
using Refit;
using NRIApp.DayCare.Features.List.Interface;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;

namespace NRIApp.DayCare.Features.List.ViewModels
{
    public class ResponseForm_VM:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialogs = UserDialogs.Instance;
        public Command responsesumbitcmd { get; set; }
        public Command ViewnumberSubmitcmd { get; set; }
        string Namepattern = "^[0-9A-Za-z ]+$";
        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        private string _contactname;
        public string contactname
        {
            get { return _contactname; }
            set { _contactname = value; OnPropertyChanged(); }
        }
        private string _contactEmail;
        public string ContactEmail
        {
            get { return _contactEmail; }
            set { _contactEmail = value; OnPropertyChanged(); }
        }
        private string _phoneno;
        public string Phoneno
        {
            get { return _phoneno; }
            set { _phoneno = value; OnPropertyChanged(); }
        }
        private string _Countrycode = "+1";
        public string Countrycode
        {
            get { return _Countrycode; }
            set
            {
                _Countrycode = value;
            }
        }
        private DateTime _FrDate = DateTime.Today;
        public DateTime FrDate
        {
            get { return _FrDate; }
            set { _FrDate = value; }
        }
        private string _contactdetail = "";
        public string ContactDetail
        {
            get { return _contactdetail; }
            set { _contactdetail = value; OnPropertyChanged(nameof(ContactDetail)); }
        }
        private string _pageID = "";
        public string pageID
        {
            get { return _pageID; }
            set { _pageID = value; OnPropertyChanged(nameof(pageID)); }
        }
        DClistings listdata = new DClistings();
        public ResponseForm_VM(DClistings data, string pageid)
        {
            listdata = data;

            if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                ContactEmail = Commonsettings.UserEmail;
            if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                contactname = Commonsettings.UserName;
            if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                Phoneno = Commonsettings.UserMobileno;
            pageID = pageid;
            responsesumbitcmd = new Command(responsesubmit);
            ViewnumberSubmitcmd = new Command(responsesubmit);
        }
        public bool validation()
        {
            bool result = true;
            if (string.IsNullOrEmpty(contactname) || !Regex.IsMatch(contactname, Namepattern) || contactname.Trim().Length == 0)
            {
                dialogs.Toast("Please enter your name");
                result = false;
                return false;
            }
            if (string.IsNullOrEmpty(ContactEmail) || ContactEmail.Trim().Length==0)
            {
                dialogs.Toast("Please enter your email id");
                result = false;
                return false;
            }
            if (!Regex.IsMatch(ContactEmail, Emailpattern))
            {
                dialogs.Toast("Please enter a valid email id");
                result = false;
                return false;
            }
            if (string.IsNullOrEmpty(Phoneno) || Phoneno.Trim().Length == 0)
            {
                dialogs.Toast("Please enter your mobile number");
                result = false;
                return false;
            }
            if (Phoneno.Length < 10)
            {
                dialogs.Toast("Minimum 10 Digits required");
                result = false;
                return false;
            }
            if (!CheckValidPhone(Phoneno))
            {
                dialogs.Toast("Please enter valid mobile number");
                result = false;
                return false;
            }
            if(pageID=="response")
            {
                if (string.IsNullOrEmpty(ContactDetail) || ContactDetail.Trim().Length == 0)
                {
                    dialogs.Toast("Details field is required");
                    result = false;
                    return false;
                }
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
        response_data response = new response_data();
        string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
        public async void responsesubmit()
        {
            try
            {
                if (validation())
                {
                    //OTP
                    dialogs.ShowLoading("", null);
                    var DCpostAPI = RestService.For<DayCare.Features.Post.Interface.IDaycareposting>(Commonsettings.DaycareAPI);
                    var postresponse = await DCpostAPI.mobileverification(listdata.businessid, Phoneno, Countrycode, ContactEmail, "1", Commonsettings.UserDeviceId, Commonsettings.UserPid, ipaddress);

                    if (postresponse.blockstatus == "1")
                    {
                        dialogs.HideLoading();
                        var currentpage = GetCurrentpage();
                        await currentpage.Navigation.PushModalAsync(new NRIApp.DayCare.Features.List.Views.Thankyou_DC());
                    }
                    else
                    {
                        Post.Models.Daycareposting post = new Post.Models.Daycareposting();
                        if (postresponse.otpsend == "otpsend")
                        {
                            post.businessid = listdata.businessid;
                            post.contactName = contactname;
                            post.contributor = contactname;
                            post.emailid = ContactEmail;
                            post.ip = ipaddress;
                            post.ContactNo = Phoneno;
                            post.city = Commonsettings.Usercity;
                            post.pinno = postresponse.otpno;
                            post.countrycode = Countrycode;
                            post.isresponse = "response";
                            //post.freeadstatus = postresponse.freeadstatus;

                            //response
                            if (Commonsettings.UserMobileOS.ToLower().Contains("iphone"))
                            {
                                post.postedviaid = "194";
                                post.postedvia = "iphone";
                            }
                            else
                            {
                                post.postedviaid = "197";
                                post.postedvia = "android";
                            }

                            if (pageID == "response")
                            {
                                post.description = ContactDetail;
                                post.responsecategorytype = "";
                            }
                            else
                            {
                                post.description = "";
                                post.responsecategorytype = "10";
                            }

                            post.availablefrom = FrDate.ToString("yyyy-MM-dd");
                            post.premiumad = listdata.premiumad;
                            post.newclickurl = listdata.newclickurl;
                            dialogs.HideLoading();
                            var currentpage = GetCurrentpage();
                            await currentpage.Navigation.PushAsync(new DayCare.Features.Post.Views.OTP_DC(post));
                        }
                        else
                        {
                            //dialogs.ShowLoading("", null);
                            response.contentid = listdata.businessid;
                            response.contactname = contactname;
                            response.emailid = ContactEmail;
                            response.ip = ipaddress;
                            response.phoneno = Phoneno;
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

                            if (pageID=="response")
                            {
                                response.description = ContactDetail;
                                response.responsecategorytype = "";
                            }
                            else
                            {
                                response.description = "";
                                response.responsecategorytype = "10";
                            }
                            
                            response.availablefrom = FrDate.ToString("yyyy-MM-dd");
                            response.premiumad = listdata.premiumad;
                            response.newclickurl = listdata.newclickurl;
                            var dcresponseapi = RestService.For<IDClistings>(Commonsettings.DaycareAPI);
                            result_info list = await dcresponseapi.responsesubmit(response);
                            dialogs.HideLoading();
                            if (list.resultinformation == "exist")
                            {
                                dialogs.Toast("Already responded to this ad");
                            }
                            else
                            {
                                var currentpage = GetCurrentpage();
                                await currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.List.Views.Thankyou_DC());
                            }
                        }
                    } 
                }
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        //public void viewnumbersumbit()
        //{
        //    try
        //    {
        //        if(validation())
        //        {

        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        dialogs.HideLoading();
        //    }
        //}
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
