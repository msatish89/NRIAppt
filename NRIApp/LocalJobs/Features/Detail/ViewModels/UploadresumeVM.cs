using NRIApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using Acr.UserDialogs;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Net;
using Plugin.FilePicker.Abstractions;
using Plugin.FilePicker;
using System.IO;
using Refit;
using NRIApp.LocalJobs.Features.Detail.Interfaces;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NRIApp.LocalJobs.Features.Detail.ViewModels
{
    public class UploadresumeVM : INotifyPropertyChanged
    {
        IUserDialogs dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;

        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        string Namepattern = "^[0-9A-Za-z ]+$";

        public Command Choosefilecommand { get; set; }
        public Command uploadfilecommand { get; set; }
        public Command validatephoneno { get; set; }
        public Command verifyphoneno { get; set; }
        public Command resend { get; set; }
        public Command changenumber { get; set; }
        public Command managecompanysubmit { get; set; }
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
        private int _uploadsuccess;
        public int uploadsuccess
        {
            get { return _uploadsuccess; }
            set { _uploadsuccess = value; OnPropertyChanged(nameof(uploadsuccess)); }
        }
        private string _selectedfilename = "";
        public string selectedfilename
        {
            get { return _selectedfilename; }
            set { _selectedfilename = value; OnPropertyChanged(nameof(selectedfilename)); }
        }
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

        private string _mobileverified = "";
        public string mobileverified
        {
            get { return _mobileverified; }
            set { _mobileverified = value; OnPropertyChanged(nameof(mobileverified)); }
        }
        public UploadresumeVM()
        {
            try
            {
                dialog.ShowLoading("", null);
                if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                    ContactEmail = Commonsettings.UserEmail;
                if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                    ContactName = Commonsettings.UserName;
                if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                    ContactPhone = Commonsettings.UserMobileno;

                Choosefilecommand = new Command(chooseresume);
                uploadfilecommand = new Command(upload);
                //validatephoneno = new Command(async () => await validatenumber(""));
                //verifyphoneno = new Command(async () => await validatenumber("1"));
                //resend = new Command(async () => await validatenumber(""));
                //changenumber = new Command(changenum);
                Applynowcmd = new Command(upload);
                dialog.HideLoading();
            }
            catch(Exception ex)
            {

            }
        }
        public FileData _filedata;
    
        public async void chooseresume()
        {
            try
            {
                _filedata = await CrossFilePicker.Current.PickFile();
                FileInfo fi = new FileInfo(_filedata.FilePath);
                var filename = _filedata.FileName;
                var filepath = _filedata.FilePath;
                selectedfilename = _filedata.FileName;
                //string extension = fi.Extension;

            }
            catch (Exception e)
            {
                string msg = e.Message;
                await App.Current.MainPage.DisplayAlert("Alert", "Permission Denied. Can not continue, try again.", "Ok");
            }

        }

      
        public async void upload()
        {
            try
            {
                uploadsuccess = 0;
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Storage))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Need Storage permission to access to your photos.", "Ok");
                    }
                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Plugin.Permissions.Abstractions.Permission.Storage });
                    status = results[Plugin.Permissions.Abstractions.Permission.Storage];
                }
                if (status == PermissionStatus.Granted)
                {
                    String URI = "http://localjobs.sulekha.com/mobileappresume/mobileappresume.aspx?type=resumelistingupload&name=" + ContactName + "&mobileno=" + ContactPhone + "&code=" + Countrycode + "&email=" + ContactEmail;// + "&jobrole=" + jobrole;
                    if(string.IsNullOrEmpty(_filedata.FilePath))
                    {
                        dialog.Toast("Select file then upload");
                    }
                    else
                    {
                        string FileName = "";
                        if (Commonsettings.UserMobileOS.ToLower() == "android")
                        {
                            var path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
                            var filename = Path.Combine(path.ToString(), _filedata.FileName);
                            FileName = filename;
                        }
                        else
                        {
                            FileName = _filedata.FilePath;
                        }
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                            byte[] HtmlResult = wc.UploadFile(URI, "POST", _filedata.FilePath);
                            string responsebody = Encoding.UTF8.GetString(HtmlResult);
                            //if (!string.IsNullOrEmpty(responsebody))
                            //{
                            //    dialog.Toast("Uploaded Successfully");
                            //}
                        }
                        uploadsuccess = 1;
                    }
                   
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Permission Denied. Can not continue, try again.", "Ok");
                }

                if(uploadsuccess==1)
                {
                    var suc = await dialog.ConfirmAsync("Success", "Uploaded Successfully", "Ok","");
                    if (suc)
                    {
                        var currentpage = Getcurrentpage();
                        await currentpage.Navigation.PopAsync();
                    }
                }
            }
            catch (Exception mg)
            {
                string msg = mg.Message;
                //await App.Current.MainPage.DisplayAlert("Alert", "Select from File Manager , try again.", "Ok");
            }
        }
        public async Task validatenumber(string verify)
        {
            try
            {

                if (validation())
                {
                    dialog.ShowLoading("", null);
                    var validate = RestService.For<IComapanydata>(Commonsettings.LocaljobsAPI);
                    var validateno = await validate.verifyphone(Countrycode, ContactPhone, verify, verifycode,"");
                    if (validateno.mode == "0")
                    {
                        phvalidatetxt = "Please verify your phone number You will receive a message containing 4 digit code on your Phone No(" + Countrycode + ") -" + ContactPhone + ".Enter the 4 digit code in the space provided.";
                        phvalidatetxtvisible = true;
                    }
                    else if (validateno.result == "true")
                    {
                        mobileverified = "validated";
                        validatephnovisible = false;
                        phvalidatetxtvisible = false;
                        applysubmit();
                    }
                    else if (validateno.result == "false")
                    {
                        dialog.Toast("Enter a valid code");
                    }
                    else
                    {
                        dialog.Toast("Couldn't verify...try later");
                    }
                    dialog.HideLoading();
                }

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                dialog.HideLoading();
            }
        }
        public void changenum()
        {
            phvalidatetxtvisible = false;
        }
        public bool validation()
        {
            bool result = true;
            var currentpage = Getcurrentpage();
            if (string.IsNullOrEmpty(ContactName.Trim()))
            {
                dialog.Toast("Enter Contact Name");
                return false;
            }
            if (!Regex.IsMatch(ContactName, Namepattern))
            {
                dialog.Toast("Enter Proper Name");
                return false;
            }
            if (string.IsNullOrEmpty(ContactEmail))
            {
                dialog.Toast("Enter Contact Email");
                return false;
            }
            if (!Regex.IsMatch(ContactEmail, Emailpattern))
            {
                dialog.Toast("Enter Proper Email");
                return false;
            }
            if (uploadsuccess == 0)
            {
                dialog.Toast("Upload resume");
                return false;
            }
            if (string.IsNullOrEmpty(ContactPhone))
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
        public async void applysubmit()
        {
            //try
            //{
            //    if (validation())
            //    {
            //        dialog.ShowLoading("", null);
            //        postdata.contentid = adid;
            //        postdata.contributor = ContactName;
            //        postdata.email = ContactEmail;
            //        postdata.mobileno = ContactPhone;
            //        postdata.countrycode = Countrycode;
                   
            //        postdata.ipaddress = ipaddress;
            //        postdata.responsecategroy = "10";
            //        postdata.ipuserlat = Commonsettings.UserLat;
            //        postdata.ipuserlong = Commonsettings.UserLong;
            //        postdata.zipcode = Commonsettings.Userzipcode;
            //        postdata.city = Commonsettings.Usercity;
            //        postdata.userpid = Commonsettings.UserPid;
            //        postdata.country = Commonsettings.Usercountry;
            //        postdata.postedvia = Commonsettings.UserMobileOS;
            //        if (Commonsettings.UserMobileOS == "iphone")
            //        {
            //            postdata.postedviaid = "188";
            //        }
            //        else
            //        {
            //            postdata.postedviaid = "191";
            //        }
            //        var postres = RestService.For<IDetaildata>(Commonsettings.LocaljobsAPI);
            //        var resresult = await postres.uploadresume(postdata);
            //        if (resresult != null)
            //        {
            //            if (resresult.resultinfo == "success")
            //            {
            //                var currentpage = Getcurrentpage();
            //                await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Detail.Views.Responsetq());
            //            }
            //            else if (resresult.resultinfo == "exists")
            //            {
            //                dialog.Toast("Already responded to this ad");
            //            }
            //            else
            //            {
            //                dialog.Toast("Some problem occurred,Please try again later...");
            //            }
            //        }
            //        dialog.HideLoading();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    dialog.HideLoading();
            //}
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
