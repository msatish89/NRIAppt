using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Acr.UserDialogs;
using System.Linq;
using System.Text.RegularExpressions;
using NRIApp.Helpers;
using Refit;
using NRIApp.DayCare.Features.Post.Interface;
using NRIApp.DayCare.Features.Post.Models;
using NRIApp.DayCare.Features.Post.Views;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using System.Net.Http;
using Newtonsoft.Json;
using NRIApp.Roommates.Features.Post.Models;
using NRIApp.DayCare.Features.List.Models;

namespace NRIApp.DayCare.Features.Post.ViewModels
{
    public class PostThird_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;

        public Command Submitcmd { get; set; }
        public Command providenumberTap { get; set; }
        public Command listingcmd { get; set; }
        public Command postingcmd { get; set; }

        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        string Namepattern = "^[0-9A-Za-z ]+$";
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
        private string _providenumberImg = "CheckBoxUnChecked.png";
        public string providenumberImg
        {
            get { return _providenumberImg; }
            set { _providenumberImg = value; OnPropertyChanged(nameof(providenumberImg)); }
        }
        private int _providenumberID = 0;
        public int providenumberID
        {
            get { return _providenumberID; }
            set { _providenumberID = value; OnPropertyChanged(nameof(providenumberID)); }
        }
        public bool postThirdvalidation()
        {
            bool result = true;
            var currentpage = Getcurrentpage();
           
            if (string.IsNullOrEmpty(ContactName) || ContactName.Trim().Length == 0)
            {
                dialog.Toast("Enter Contact Name");
                result = false;
                return false;
            }
            if (!Regex.IsMatch(ContactName, Namepattern))
            {
                dialog.Toast("Enter Proper Name");
                result = false;
                return false;
            }
            if (string.IsNullOrEmpty(ContactEmail) || ContactEmail.Trim().Length==0)
            {
                dialog.Toast("Enter Contact Email");
                result = false;
                return false;
            }
            if (!Regex.IsMatch(ContactEmail, Emailpattern))
            {
                dialog.Toast("Enter Proper Email");
                result = false;
                return false;
            }

            if (string.IsNullOrEmpty(ContactPhone) || ContactPhone.Trim().Length==0)
            {
                dialog.Toast("Enter Contact Number");
                result = false;
                return false;
            }
            if (ContactPhone.Length < 10)
            {
                dialog.Toast("Minimum 10 Digits required");
                result = false;
                return false;
            }
            else if (!CheckValidPhone(ContactPhone))
            {
                dialog.Toast("Please enter valid mobile number");
                result = false;
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
        
        Daycareposting post = new Daycareposting();
        public PostThird_VM(Daycareposting postdata)
        {
            post = postdata;
            if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                ContactEmail = Commonsettings.UserEmail;
            if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                ContactName = Commonsettings.UserName;
            if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                ContactPhone = Commonsettings.UserMobileno;
           

            providenumberTap = new Command(checkprovidenumber);
            listingcmd = new Command(gotolisting);
            postingcmd = new Command(gotoposting);
            Submitcmd = new Command(postsubmit);
        }

        DClistinfo list = new DClistinfo();
        public void gotolisting()
        {
            //locationurl,categoryurl,latitude,longitude,distance
            if (postThirdvalidation())
            {
                list.newcategoryurl = post.newcategoryurl;
                list.careprovidertype = post.categoryurl;
                list.cityurl = post.newcityurl;
                list.userlat = post.lat.ToString();
                list.userlong = post.longtitude.ToString();
                list.city = post.city + ", " + post.state;
                if (!string.IsNullOrEmpty(post.tertiarycategoryid))
                {
                    list.worktype = post.tertiarycategoryid;
                    if(list.worktype=="42")
                    {
                        list.worktype = "19,42";
                    }
                }
                var currentpage = Getcurrentpage();
                currentpage.Navigation.PushAsync(new List.Views.DaycareListing(list));
            }
        }
        //PostFirst
        string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
        public async void gotoposting()
        {
            try
            {
                var currentpage = Getcurrentpage();
                if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                {
                    await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                }
                else
                {
                    // dialog.ShowLoading("", null);
                 if(postThirdvalidation())
                    {
                        post.ip = ipaddress;
                        post.contactName = ContactName;
                        post.ContactNo = ContactPhone;
                        post.countrycode = Countrycode;
                        post.hidephonenumber = providenumberID.ToString();
                       
                        post.userpid = Commonsettings.UserPid;


                       
                        if (post.ismyneed == "1")
                        {
                            dialog.ShowLoading("", null);
                            //var dcapi = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                            //var postfirstdata = await dcapi.postfirstsubmit(post);
                            if (post.mode == "edit")
                            {
                                if(post.ordertype.ToLower()!="free")
                                {
                                    post.premiumad = "1";
                                }
                                var DCpostAPI = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                                var postresponse = await DCpostAPI.postsecondsubmit(post);
                                dialog.HideLoading();
                                if (postresponse != null)
                                {
                                    await currentpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.EditThankyou());
                                }
                            }
                            else
                            {
                                //var DCpostAPI = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                                //var postresponse = await DCpostAPI.renewupdate(post);
                                //await Task.Delay(500);
                                dialog.HideLoading();
                                await currentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Package(post));
                            }
                        }
                        else
                        {
                            dialog.ShowLoading("", null);
                            post.contributor = Commonsettings.UserName;
                            post.emailid = Commonsettings.UserEmail;
                            post.country = Commonsettings.Usercountry;
                            if ((post.primarycategoryid == "3" || post.primarycategoryid == "4" || post.primarycategoryid == "5") && (post.secondarycategoryid == "8" || post.secondarycategoryid == "11" || post.secondarycategoryid == "13") && post.supercategoryid == "1")
                            {
                                post.maincategoryid = "1";
                            }
                            else if ((post.primarycategoryid == "4" || post.primarycategoryid == "3") && (post.secondarycategoryid == "12" || post.secondarycategoryid == "9") && post.supercategoryid == "1")
                            {
                                post.maincategoryid = "2";
                            }
                            else if (post.supercategoryid == "2" && post.primarycategoryid == "0" && post.secondarycategoryid == "0" && post.tertiarycategoryid == "0")
                            {
                                post.maincategoryid = "3";
                            }
                            //else
                            //{
                            //    post.maincategoryid = "0";
                            //}
                            var dcapi = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                            var postfirstdata = await dcapi.postfirstsubmit(post);
                            //  dialog.HideLoading();
                            if (!string.IsNullOrEmpty(postfirstdata.businessid))
                            {
                                post.businessid = postfirstdata.businessid;
                                post.premiumad = postfirstdata.premiumad;
                                post.usertype = postfirstdata.usertype;
                                //var currentpage = GetCurrentPage();
                                await currentpage.Navigation.PushAsync(new Post.Views.PostFirst(post));
                            }
                            else
                            {
                                dialog.HideLoading();
                                dialog.Toast("Try again later...");
                            }
                            //  dialog.HideLoading();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dialog.Toast("Some error occurred...try later...");
                dialog.HideLoading();
            }
        }
        public void checkprovidenumber()
        {
            if(providenumberID==0)
            {
                providenumberImg = "CheckBoxChecked.png";
                providenumberID = 1;
            }
            else
            {
                providenumberImg = "CheckBoxUnChecked.png";
                providenumberID = 0;
            }
        }
        
        public async void postsubmit()
        {
            try
            {
                var curentpage = Getcurrentpage();
                //if (post.ismyneed=="1")
                //{
                //    var DCpostAPI = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                //    var postresponse = await DCpostAPI.postsecondsubmit(post);
                //    if(postresponse!=null)
                //    {
                //        await curentpage.Navigation.PushAsync(new NRIApp.DayCare.Features.List.Views.Thankyou_DC());
                //    }
                //}
                //else
                //{
                    await curentpage.Navigation.PushAsync(new Package(post));
                //}
                //dialog.ShowLoading("", null);
                //if(postThirdvalidation())
                //{
                //    post.contactName =post.contributor= ContactName;
                //    post.emailid = ContactEmail;
                //    post.hidephonenumber = providenumberID.ToString();
                //    post.ContactNo = ContactPhone;
                //    post.countrycode = Countrycode;

                //    var DCpostAPI = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                //    //  var postresponse = await DCpostAPI.postsecondsubmit(post);
                //    var postresponse = await DCpostAPI.mobileverification(post.ContactNo, post.countrycode, post.emailid, post.usertype, Commonsettings.UserDeviceId, Commonsettings.UserPid, post.ip);

                //    var curentpage = Getcurrentpage();


                //    if (postresponse != null)
                //    {
                //        if (postresponse.blockstatus == "1")
                //        {
                //            dialog.HideLoading();
                //            await curentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Spammer());
                //        }
                //        else
                //        {
                //            if (postresponse.usertype == "2")
                //            {
                //                //  await curentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.JobSuccess(data.adid, details.usertype));
                //            }
                //            else
                //            {
                //                if (postresponse.otpsend == "otpsend")
                //                {
                //                    post.pinno = postresponse.pinno;
                //                    await curentpage.Navigation.PushAsync(new OTP_DC(post));
                //                }
                //                else
                //                {
                //                    //dialog.HideLoading();
                //                    if (postresponse.ispayment == "1")
                //                        await curentpage.Navigation.PushAsync(new Package(post));
                //                    else
                //                        await curentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Spammer());
                //                }
                //            }
                //        }
                //    }
                //}
                //dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
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
