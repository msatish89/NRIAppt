using NRIApp.Helpers;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using NRIApp.Techjobs.Features.LeadForm.Models;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using NRIApp.LocalJobs.Features.Posting.Interfaces;
using NRIApp.LocalJobs.Features.Posting.Models;
using System.Text.RegularExpressions;
using Acr.UserDialogs;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NRIApp.LocalJobs.Features.Posting.ViewModels
{
    public class JobsRecruiterVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand Cityajaxcommand { get; set; }
        public ICommand Uploadresume { get; set; }
        public ICommand Choosefilecommand { get; set; }
        public ICommand Jobrolecommand { get; set; }
        public FileData _filedata;
        IUserDialogs dialog = UserDialogs.Instance;
        public JobsRecruiterVM()
        {
            Jobrolecommand = new Command<Jobroles>(Selectjobrole);

            //  Cityajaxcommand = new Command<Cityajax>(Selectajaxcity);
            //Uploadresume = new Command(Upload);
            //Device.BeginInvokeOnMainThread(async () =>
            //{
            //    Uploadresume = new Command(Upload);
            //});

            //Uploadresume = new Command(() =>
            //{

            //    if (validateleadform())
            //    {

            //        Upload();

            //    }

            //});
            //  Uploadresume = new Command(Upload);
            Uploadresume = new Command(async () => await Upload());
            Choosefilecommand = new Command(Choosefile);
            if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                UEmail = Commonsettings.UserEmail;
            if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                UName = Commonsettings.UserName;
            if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                Mobileno = Commonsettings.UserMobileno;
            //if (Commonsettings.Usercity != null && Commonsettings.Usercity != "")
            //{
            //    SelTrainingcity = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
            //    Trainingcity = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
            //    Zipcode = Commonsettings.Userzipcode;
            //}
        }

        //public string _trcity { get; set; }
        //public string Trainingcity
        //{
        //    get { return _trcity; }
        //    set
        //    {

        //        if (_trcity != value)
        //        {
        //            _trcity = value;
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Trainingcity)));
        //            if (SelTrainingcity != _trcity)
        //                ajaxcity(_trcity);
        //        }

        //    }
        //}

        //public string _seltrcity { get; set; }
        //public string SelTrainingcity
        //{
        //    get { return _seltrcity; }
        //    set
        //    {

        //        if (_seltrcity != value)
        //        {
        //            _seltrcity = value;
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelTrainingcity)));
        //        }

        //    }
        //}

        //public int _Zipcode { get; set; }
        //public int Zipcode
        //{
        //    get { return _Zipcode; }
        //    set
        //    {

        //        if (_Zipcode != value)
        //        {
        //            _Zipcode = value;
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Zipcode)));
        //        }

        //    }
        //}

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
        public string _selectedfilename = "";
        public string selectedfilename
        {
            get { return _selectedfilename; }
            set
            {

                if (_selectedfilename != value)
                {
                    _selectedfilename = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selectedfilename)));
                }

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
        private string _Selectcountry = "+1";
        public string Selectcountry
        {
            get { return _Selectcountry; }
            set { _Selectcountry = value; }
        }
        //public void Selectajaxcity(Cityajax model)
        //{
        //    SelTrainingcity = model.city + ", " + model.statecode;
        //    Trainingcity = model.city + ", " + model.statecode;
        //    Zipcode = Convert.ToInt32(model.zipcode);
        //    CityVisible = false;
        //}

        //public async void ajaxcity(string city)
        //{
        //    Zipcode = 0;
        //    var nsAPI = RestService.For<ILeadformService>(Commonsettings.TechjobsAPI);
        //    Cityajaxlist list = await nsAPI.Getcityajax(city);
        //    CityAjax = list.ROW_DS;
        //    if (CityAjax != null)
        //    {
        //        foreach (var i in list.ROW_DS)
        //        {
        //            i.fullcity = i.city + ", " + i.statecode;
        //        }
        //        CityVisible = true;
        //    }
        //    else
        //        CityVisible = false;

        //}
        public string _jobrole { get; set; }
        public string Jobrole
        {
            get { return _jobrole; }
            set
            {

                if (_jobrole != value)
                {
                    _jobrole = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Jobrole)));
                    if (SelJobrole != _jobrole)
                        ajaxjobrole(_jobrole);
                }

            }
        }

        public string _seljobrole { get; set; }
        public string SelJobrole
        {
            get { return _seljobrole; }
            set
            {
                _seljobrole = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelJobrole)));

            }
        }

        public string _jobroleid { get; set; }
        public string Jobroleid
        {
            get { return _jobroleid; }
            set
            {
                _jobroleid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Jobroleid)));

            }
        }
        public string _jobroletagid = "";
        public string Jobroletagid
        {
            get { return _jobroletagid; }
            set
            {
                _jobroletagid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Jobroletagid)));

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
        public List<Jobroles> _jobroleajax { get; set; }
        public List<Jobroles> JobroleAjax
        {
            get
            {
                return _jobroleajax;
            }
            set
            {
                _jobroleajax = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(JobroleAjax)));
            }
        }
     
        public void Selectjobrole(Jobroles model)
        {
            SelJobrole = model.rolename;
            Jobroleid = model.contentid;
            Jobroletagid = model.tagid;
            Jobrole = model.rolename;
            Listviewvisible = false;
            var currentpage = GetCurrentPage();
            var txtmod = currentpage.FindByName<NRIApp.Controls.CustomEntry>("txtmod");
            txtmod.Unfocus();
        }
       
        public async void ajaxjobrole(string role)
        {
            Jobroleid = "";
            var nsAPI = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
            Jobroleslist list = await nsAPI.GetJobroleajax(role);
            JobroleAjax = list.ROW_DATA;
            if (JobroleAjax != null)
                Listviewvisible = true;
            else
                Listviewvisible = false;
        }

        string extension = "";
        public async void Choosefile()
        {
            try
            {
                
                _filedata = await CrossFilePicker.Current.PickFile();
                FileInfo fi = new FileInfo(_filedata.FilePath);
                
                if(fi!=null)
                {
                    var filename = _filedata.FileName;
                    var filepath = _filedata.FilePath;

                    extension = Path.GetExtension(_filedata.FileName);

                    //extension = fi.Extension;
                    if (extension.ToLower() == ".doc" || extension.ToLower() == ".docx" || extension.ToLower() == ".pdf")
                    {
                        selectedfilename = _filedata.FileName;
                    }
                    else
                    {
                        //  dialog.ShowLoading("");
                        selectedfilename = "";
                        await System.Threading.Tasks.Task.Delay(500);
                        var currentpage = GetCurrentPage();
                        await currentpage.DisplayAlert("Alert", "Select doc,docx,pdf files only", "", "Ok");
                        //dialog.HideLoading();
                        //dialog.Toast("Error");
                        // dialog.Alert("Select doc,docx,pdf files only","","ok");
                    }
                }
                else
                {
                    dialog.Toast("This is not supported in your device...");
                }
               
                
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
                
            }
        }

        public async Task Upload()
        {

            //   if (validateleadform())
            //  {

            //String URI = "https://techjobs.sulekha.com/mobileapp/localjobs/resume.aspx?type=resumeupload&name=" + UName + "&mobileno=" + Mobileno + "&code=" + Selectcountry + "&email=" + UEmail + "&jobrole=" + Jobrole;
            //using (WebClient wc = new WebClient())
            //{
            //    wc.Headers[HttpRequestHeader.ContentType] = "application/json";
            //    byte[] HtmlResult = wc.UploadFile(URI, "POST", _filedata.FilePath);
            //    string responsebody = Encoding.UTF8.GetString(HtmlResult);
            //}
            //Android.App.ProgressDialog.Show();

            //(this, "Please wait...", "Checking account info...", true); new Thread(new ThreadStart(delegate { //LOAD METHOD TO GET ACCOUNT INFO RunOnUiThread(() => Toast.MakeText(this, "Toast within progress dialog.", ToastLength.Long).Show()); //HIDE PROGRESS DIALOG RunOnUiThread(() => progressDialog.Hide()); })).Start();





            if (validateleadform())
            {
                dialog.ShowLoading("Uploading...", null);
                await Task.Delay(100);

                try
                {

                    String URI = "https://localjobs.sulekha.com/mobileappresume/mobileappresume.aspx?type=resumelistingupload&name=" + UName + "&mobileno=" + Mobileno + "&code=" + Selectcountry + "&email=" + UEmail + "&jobrole=" + Jobrole;

                    string FileName = "";
                    //  dialog.ShowLoading("", null);

                    if (Commonsettings.UserMobileOS.ToLower() == "android")
                    {
                        var path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
                        var filename = Path.Combine(path.ToString(), _filedata.FileName);
                        FileName = filename;
                    }
                    else
                    {
                        //To enable cloud Kit
                        //https://docs.microsoft.com/en-us/xamarin/ios/data-cloud/intro-to-cloudkit
                        //https://docs.microsoft.com/en-us/xamarin/ios/app-fundamentals/file-system
                        //var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        //Environment.SystemDirectory-- returns empty
                        var documents = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                        string tet = documents;
                        var filename = Path.Combine(documents, _filedata.FileName);
                        FileName = documents;

                        //var tt= Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), _filedata.FileName);
                        //FileName = tt;
                        //var tt1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), _filedata.FileName);
                        //FileName = tt1;
                    }





                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                        byte[] HtmlResult = wc.UploadFile(URI, "POST", FileName);
                        if (HtmlResult != null)
                        {
                            string responsebody = Encoding.UTF8.GetString(HtmlResult);
                            if (!string.IsNullOrEmpty(responsebody))
                            {
                                // dialog.Toast("Uploaded Successfully");
                                var currntpg = GetCurrentPage();
                                //SelJobrole = model.rolename;
                                //Jobroleid = model.contentid;
                                //Jobrole = model.rolename;
                                await currntpg.Navigation.PushAsync(new Views.UploadSuccess(Jobroletagid, SelJobrole));
                            }
                        }
                        else
                        {
                            //mention mb
                            dialog.Toast("Files may not be in a correct format/ Size issues...Select some other file and try again");
                        }

                    }
                    dialog.HideLoading();

                }
                catch (Exception ex)
                {
                    dialog.HideLoading();

                    dialog.Toast("Files may not be in a correct format/ Size issues...Select some other file and try again");

                }
                dialog.HideLoading();

            }
            else
            {
                dialog.HideLoading();
            }


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

        bool validate = true;
        public bool validateleadform()
        {
            var currentpage = GetCurrentPage();
            validate = true;

            if (string.IsNullOrEmpty(Jobroleid))
            {
                // validate = false;
                Entry myEntry = currentpage.FindByName<Entry>("txtmod");
                myEntry.Focus();
                dialog.Toast("Please select the role");
                return false;
            }

            if (UName.Trim().Length == 0)
            {
                // validate = false;
                // UNamecolor = "Red";
                Entry myEntry = currentpage.FindByName<Entry>("lblname");
                myEntry.Focus();
                dialog.Toast("Please enter your name");
                return false;
            }
            if (!CheckAplhaOnly(UName))
            {
                // validate = false;
                Entry myEntry = currentpage.FindByName<Entry>("lblname");
                myEntry.Focus();
                dialog.Toast("Please enter a valid name");
                return false;
            }
            if (UEmail.Trim().Length == 0)
            {
                validate = false;
                Entry myEntry = currentpage.FindByName<Entry>("lblemail");
                myEntry.Focus();
                dialog.Toast("Please enter your email");
                return false;
            }
            if (!CheckValidEmail(UEmail))
            {
                // validate = false;
                Entry myEntry = currentpage.FindByName<Entry>("lblemail");
                myEntry.Focus();
                dialog.Toast("Please enter valid email id");
                return false;
            }


            if (string.IsNullOrEmpty(Mobileno))
            {
                // validate = false;
                Entry myEntry = currentpage.FindByName<Entry>("lblph");
                myEntry.Focus();
                dialog.Toast("Enter your mobile number");
                return false;
            }
            if (Mobileno.Length < 10)
            {
                // validate = false;
                Entry myEntry = currentpage.FindByName<Entry>("lblph");
                myEntry.Focus();
                dialog.Toast("Please enter a 10-digit mobile number");
                return false;
            }
            else if (!CheckValidPhone(Mobileno))
            {
                Entry myEntry = currentpage.FindByName<Entry>("lblph");
                myEntry.Focus();
                // validate = false;
                dialog.Toast("Please enter valid mobile number");
                return false;
            }
            if (string.IsNullOrEmpty(selectedfilename))
            {
                dialog.Toast("Please choose file to upload");
                // validate = false;
                return false;
            }
            //if(extension.ToLower() != ".doc" || extension.ToLower() != ".docx" || extension.ToLower() != ".pdf") 
            //{
            //    dialog.Toast("Select doc,docx,pdf files only");
            //    return false;
            //}
            return validate;
        }

    }
}
