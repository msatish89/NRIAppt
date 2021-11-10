using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Linq;
using System.Runtime.CompilerServices;
using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Listing.Models;
using Acr.UserDialogs;
using System.Text.RegularExpressions;
using Refit;
using NRIApp.LocalJobs.Features.Detail.Interfaces;
using Plugin.FilePicker.Abstractions;
using System.IO;
using Plugin.FilePicker;
using Plugin.Permissions.Abstractions;
using System.Net;
using Plugin.Permissions;
using NRIApp.LocalJobs.Features.Detail.Models;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using System.Windows.Input;
using Plugin.Connectivity;

namespace NRIApp.LocalJobs.Features.Detail.ViewModels
{
    public class ApplyformVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;
        string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        string Namepattern = "^[0-9A-Za-z ]+$";
        public Command Choosefilecommand { get; set; }
        public Command uploadfilecommand { get; set; }
        public Command Applynowcmd { get; set; }
        public Command Viewnumbercmd { get; set; }
        public Command RyesCommand { get; set; }
        public Command RnoCommand { get; set; }
        public Command JobofferTapopen { get; set; }
        public ICommand Selectjoboffers { get; set; }
        public Command ContentViewTap { get; set; }
        public Command PopupContentTap { get; set; }
        public List<joboffersdata> jobofferlist { get; set; }

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
        private string _contactdetail = "I am interested .";
        public string ContactDetail
        {
            get { return _contactdetail; }
            set { _contactdetail = value; OnPropertyChanged(); }
        }
        private string _YesImg = "";
        public string YesImg
        {
            get { return _YesImg; }
            set { _YesImg = value; OnPropertyChanged(nameof(YesImg)); }
        }
        private string _NoImg = "";
        public string NoImg
        {
            get { return _NoImg; }
            set { _NoImg = value; OnPropertyChanged(nameof(NoImg)); }
        }
        private string _relocatetype = "";
        public string relocatetype
        {
            get { return _relocatetype; }
            set { _relocatetype = value; OnPropertyChanged(nameof(relocatetype)); }
        }
        private bool _viewnumbervisible = false;
        public bool viewnumbervisible
        {
            get { return _viewnumbervisible; }
            set { _viewnumbervisible = value; OnPropertyChanged(nameof(viewnumbervisible)); }
        }
        private string _selectedfilename = "";
        public string selectedfilename
        {
            get { return _selectedfilename; }
            set { _selectedfilename = value; OnPropertyChanged(nameof(selectedfilename)); }
        }
        private string _resummandatory;
        public string resummandatory
        {
            get { return _resummandatory; }
            set { _resummandatory = value; OnPropertyChanged(nameof(resummandatory)); }
        }
        private int _uploadsuccess;
        public int uploadsuccess
        {
            get { return _uploadsuccess; }
            set { _uploadsuccess = value; OnPropertyChanged(nameof(uploadsuccess)); }
        }
        private bool _joboffervisible;
        public bool joboffervisible
        {
            get { return _joboffervisible; }
            set { _joboffervisible = value; OnPropertyChanged(nameof(joboffervisible)); }
        }
        private bool _contentviewvisible;
        public bool contentviewvisible
        {
            get { return _contentviewvisible; }
            set { _contentviewvisible = value; OnPropertyChanged(nameof(contentviewvisible)); }
        }
        private string _selectedjoboffer = "";
        public string selectedjoboffer
        {
            get { return _selectedjoboffer; }
            set { _selectedjoboffer = value; OnPropertyChanged(nameof(selectedjoboffer)); }
        }
        private string _selectedjobofferADID = "";
        public string selectedjobofferADID
        {
            get { return _selectedjobofferADID; }
            set { _selectedjobofferADID = value; OnPropertyChanged(nameof(selectedjobofferADID)); }
        }
        private string _jobofferxt = "Select the Job offer";
        public string jobofferxt
        {
            get { return _jobofferxt; }
            set { _jobofferxt = value; OnPropertyChanged(nameof(jobofferxt)); }
        }
        private string _cprofilevalidate = "";
        public string cprofilevalidate
        {
            get { return _cprofilevalidate; }
            set { _cprofilevalidate = value; OnPropertyChanged(nameof(cprofilevalidate)); }
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
            //if (!Regex.IsMatch(ContactName, Namepattern))
            //{
            //    dialog.Toast("Enter Proper Name");
            //    return false;
            //}
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
            if (string.IsNullOrEmpty(relocatetype))
            {
                dialog.Toast("Please select Relocate");
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
            if (resummandatory == "1" || !string.IsNullOrEmpty(selectedfilename))
            {
                if (uploadsuccess == 0)
                {
                    dialog.Toast("Upload resume");
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(cprofilevalidate))
            {
                if (jobofferxt == "Select the Job offer")
                {
                    dialog.Toast("Select the job applying for");
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
        bool CheckAplhaOnly(string inputString)
        {
            return Regex.IsMatch(inputString, "^[a-zA-Z \\.-]+$");
        }

        bool CheckValidEmail(string inputEmail)
        {
            return Regex.IsMatch(inputEmail, "[\\w\\.-]+@([\\w\\-]+\\.)+[A-Z]{2,4}$", RegexOptions.IgnoreCase);
        }
        // LocalJobResponse data = new LocalJobResponse();
        public ApplyformVM(string adid, string premiumad, string resumemandatry, string cprofile)
        {
            try
            {
                resummandatory = resumemandatry;
                if (!string.IsNullOrEmpty(cprofile))
                {
                    cprofilevalidate = cprofile;
                    getjoboffers(cprofile);
                }

                
                //data = postresponse;
                if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                    ContactEmail = Commonsettings.UserEmail;
                if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                    ContactName = Commonsettings.UserName;
                if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                    ContactPhone = Commonsettings.UserMobileno;
                //if (Commonsettings.Usercity != null && Commonsettings.Usercity != "")
                //{
                //    LocationName = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                //    TempLocationName = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                //    Newycityurl = Commonsettings.Usercityurl;
                //    Zipcode = Commonsettings.Userzipcode;
                //}
                NoImg = "RadioButtonUnChecked.png";
                YesImg = "RadioButtonUnChecked.png";
                Choosefilecommand = new Command(chooseresume);
                uploadfilecommand = new Command(upload);
                ContentViewTap = new Command(Closecontentview);
                PopupContentTap = new Command(showcontentview);
                Applynowcmd = new Command(async () => await applysubmit(adid, premiumad, resumemandatry));
                Viewnumbercmd = new Command(async () => await viewnumber());
                RyesCommand = new Command(Ryescmd);
                RnoCommand = new Command(Rnocmd);
                JobofferTapopen = new Command(showapplyforlist);
                Selectjoboffers = new Command<joboffersdata>(selectoffer);
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
            }
        }
        public void showapplyforlist()
        {
            var curentpage = Getcurrentpage();
            var joffers = curentpage.FindByName<ListView>("joffers");
            foreach (var data in jobofferlist)
            {
                if (selectedjobofferADID == data.adid)
                {
                    data.checkimage = "RadioButtonChecked.png";
                    selectedjoboffer = data.jobrole;
                    selectedjobofferADID = data.adid;
                    jobofferxt = data.applyingfor;
                }
                else
                {
                    data.checkimage = "RadioButtonUnChecked.png";
                }

            }
            joffers.ItemsSource = null;
            joffers.ItemsSource = jobofferlist;
            contentviewvisible = true;
            joboffervisible = true;
        }
        public void selectoffer(joboffersdata data)
        {
            var curntpg = Getcurrentpage();
            Label jobofferlist = curntpg.FindByName<Label>("jobofferlist");
            jobofferlist.TextColor = Color.FromHex("#212121");
            selectedjoboffer = data.jobrole;
            selectedjobofferADID = data.adid;
            jobofferxt = data.applyingfor;
            contentviewvisible = false;
        }
        public void Closecontentview()
        {
            contentviewvisible = false;
        }
        public void showcontentview()
        {
            contentviewvisible = true;
        }
        public async void getjoboffers(string businesurl)
        {
            try
            {
                var joboffers = RestService.For<IDetaildata>(Commonsettings.LocaljobsAPI);
                var joboffersdata = await joboffers.getjoboffers(businesurl, Commonsettings.UserPid);
                var currentpage = Getcurrentpage();

                if (joboffersdata != null && joboffersdata.ROW_DATA.Count > 0)
                {
                    joboffervisible = true;
                    jobofferlist = joboffersdata.ROW_DATA;
                }
                else
                {
                    joboffervisible = false;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public FileData _filedata;
        string extension = "";
        public async void chooseresume()
        {
            try
            {
                _filedata = await CrossFilePicker.Current.PickFile();
                FileInfo fi = new FileInfo(_filedata.FilePath);
                var filename = _filedata.FileName;
                var filepath = _filedata.FilePath;
                selectedfilename = _filedata.FileName;
                extension = Path.GetExtension(_filedata.FileName);
                if (extension.ToLower() == ".doc" || extension.ToLower() == ".docx" || extension.ToLower() == ".pdf")
                {
                    selectedfilename = _filedata.FileName;
                }
                else
                {
                    dialog.ShowLoading("");
                    selectedfilename = "";
                    await System.Threading.Tasks.Task.Delay(500);
                    var currentpage = Getcurrentpage();
                    await currentpage.DisplayAlert("Alert", "Select doc,docx,pdf files only", "", "Ok");
                    dialog.HideLoading();
                    //dialog.Toast("Error");
                    // dialog.Alert("Select doc,docx,pdf files only","","ok");
                }

            }
            catch (Exception e)
            {
                dialog.HideLoading();
                string msg = e.Message;
               // await App.Current.MainPage.DisplayAlert("Alert", "Some error occurred, try again.", "Ok");
                //  await App.Current.MainPage.DisplayAlert("Alert", "Permission Denied. Can not continue, try again.", "Ok");
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
                    dialog.ShowLoading("", null);
                    await Task.Delay(100);

                    if (!string.IsNullOrEmpty(ContactName) && !string.IsNullOrEmpty(ContactEmail) && !string.IsNullOrEmpty(ContactPhone))
                    {
                        if (_filedata == null)
                        {
                            dialog.Toast("Select file then upload");
                            dialog.HideLoading();
                        }
                        else
                        {
                            string FileName = "";
                            String URI = "http://localjobs.sulekha.com/mobileappresume/mobileappresume.aspx?type=resumelistingupload&name=" + ContactName + "&mobileno=" + ContactPhone + "&code=" + Countrycode + "&email=" + ContactEmail;// + "&jobrole=" + jobrole;
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
                                byte[] HtmlResult = wc.UploadFile(URI, "POST", FileName);
                                string responsebody = Encoding.UTF8.GetString(HtmlResult);
                                if (!string.IsNullOrEmpty(responsebody))
                                {
                                    dialog.Toast("Uploaded Successfully");
                                }
                            }
                        }

                        uploadsuccess = 1;

                    }
                    else
                    {
                        dialog.HideLoading();
                        dialog.Toast("Please fill the form...");
                    }

                    dialog.HideLoading();
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Permission Denied. Can not continue, try again.", "Ok");
                    dialog.HideLoading();
                }
            }
            catch (Exception mg)
            {
                dialog.HideLoading();
                string msg = mg.Message;
                //await App.Current.MainPage.DisplayAlert("Alert", "Select file then upload", "Ok");
                dialog.Toast("Please Select file and try again later...");
            }
        }
        postresponse postdata = new postresponse();
        public async Task applysubmit(string adid, string premiumad, string resumemandatory)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            try
            {
                //var currentpage = Getcurrentpage();
                //await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Detail.Views.Responsetq());

                if(connected==true)
                {
                    if (validation())
                    {
                        dialog.ShowLoading("", null);
                        if (!string.IsNullOrEmpty(cprofilevalidate) && !string.IsNullOrEmpty(selectedjobofferADID))
                        {
                            adid = selectedjobofferADID;
                        }
                        postdata.contentid = adid;
                        postdata.contributor = ContactName;
                        postdata.email = ContactEmail;
                        postdata.mobileno = ContactPhone;
                        postdata.countrycode = Countrycode;
                        postdata.shortdesc = ContactDetail;
                        postdata.ipaddress = ipaddress;
                        postdata.responsecategroy = "10";
                        postdata.ipuserlat = Commonsettings.UserLat;
                        postdata.ipuserlong = Commonsettings.UserLong;
                        postdata.zipcode = Commonsettings.Userzipcode;
                        postdata.city = Commonsettings.Usercity;
                        postdata.userpid = Commonsettings.UserPid;
                        postdata.country = Commonsettings.Usercountry;
                        postdata.premiumad = Convert.ToInt32(premiumad);
                        postdata.relocate = relocatetype;
                        postdata.postedvia = Commonsettings.UserMobileOS;
                        if (Commonsettings.UserMobileOS == "iphone")
                        {
                            postdata.postedviaid = "188";
                        }
                        else
                        {
                            postdata.postedviaid = "191";
                        }
                        var postres = RestService.For<IDetaildata>(Commonsettings.LocaljobsAPI);
                        var resresult = await postres.postjobresponse(postdata);
                        if (resresult != null)
                        {
                            if (resresult.resultinfo == "success")
                            {
                                var currentpage = Getcurrentpage();
                                await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Detail.Views.Responsetq());
                            }
                            else if (resresult.resultinfo == "exists")
                            {
                                dialog.Toast("Already responded to this ad");
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
        public void Ryescmd()
        {
            NoImg = "RadioButtonUnChecked.png";
            YesImg = "RadioButtonChecked.png";
            relocatetype = "1";
        }
        public void Rnocmd()
        {
            YesImg = "RadioButtonUnChecked.png";
            NoImg = "RadioButtonChecked.png";
            relocatetype = "0";
        }
        public async Task viewnumber()
        {
            var currentpage = Getcurrentpage();
            await currentpage.Navigation.PushAsync(new LocalJobs.Features.Detail.Views.View_Number());
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
