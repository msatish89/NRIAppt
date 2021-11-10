using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Linq;
using NRIApp.Helpers;
using System.Text.RegularExpressions;
using System.Windows.Input;
using NRIApp.Roommates.Features.List.Models;
using Acr.UserDialogs;
using Refit;
using System.ComponentModel;
using NRIApp.DayCare.Features.Detail.Interface;
using NRIApp.DayCare.Features.Detail.Models;

namespace NRIApp.DayCare.Features.Detail.ViewModels
{
    public class Report_DC_VM:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialogs = UserDialogs.Instance;
        public ICommand selectreport { protected set; get; }
        public ICommand Reportsubmitcmd { protected set; get; }
        public Command CloseFlagpage { get; set; }
        public Command TapOnReportEntry { get; set; }
        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        private string _reportlist;
        public string Reportlist
        {
            get { return _reportlist; }
            set { _reportlist = value; OnPropertyChanged(); }
        }
        private string _reportemail;
        public string ReportEmail
        {
            get { return _reportemail; }
            set { _reportemail = value; OnPropertyChanged(); }
        }
        private string _flagreason = "";
        public string FlagReason
        {
            get { return _flagreason; }
            set { _flagreason = value; OnPropertyChanged(); }
        }
        private string _contactEmail;
        public string ContactEmail
        {
            get { return _contactEmail; }
            set { _contactEmail = value; OnPropertyChanged(); }
        }
        private List<ReportFlag> _listofreports;
        public List<ReportFlag> Listofreports
        {
            get { return _listofreports; }
            set { _listofreports = value; OnPropertyChanged(); }
        }
        private bool _isvisiblereportlist = false;
        public bool IsVisibleReportList
        {
            get { return _isvisiblereportlist; }
            set { _isvisiblereportlist = value; OnPropertyChanged(); }
        }
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }
        string PassContentid = string.Empty;
        string Reportid = string.Empty,AdUrl=string.Empty;
        public Report_DC_VM(string businessid,string adurl,string adtitle)
        {
            try
            {
                PassContentid = businessid;
                Title = adtitle;
                AdUrl = adurl;
                ContactEmail = Commonsettings.UserEmail;
                if (string.IsNullOrEmpty(ReportEmail))
                {
                    ReportEmail = Commonsettings.UserEmail;
                }
                if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                    ContactEmail = Commonsettings.UserEmail;
                selectreport = new Command<ReportFlag>(Taponreportflaglist);
                TapOnReportEntry = new Command(taponreportentry);
                Reportsubmitcmd = new Command(Taponreportsubmitcmd);
                CloseFlagpage = new Command(TaponClose);
                List<ReportFlag> listofreports = new List<ReportFlag>();
                listofreports.Add(new ReportFlag { reportlist = "Spam", reportid = 1 });
                listofreports.Add(new ReportFlag { reportlist = "Duplicate", reportid = 8 });
                listofreports.Add(new ReportFlag { reportlist = "Prohibited / fraud", reportid = 2 });
                listofreports.Add(new ReportFlag { reportlist = "Miscategorized", reportid = 3 });
                listofreports.Add(new ReportFlag { reportlist = "Invalid contact details", reportid = 6 });
                listofreports.Add(new ReportFlag { reportlist = "No Reply", reportid = 7 });
                Listofreports = listofreports;
                Reportlist = listofreports.First().reportlist;

            }
            catch (Exception e)
            {
            }
        }
        public bool validate()
        {
            bool result = true;
            var currentpage = GetCurrentPage();
            if (string.IsNullOrEmpty(Reportlist))
            {
                dialogs.Toast("Select anyone");
                return false;
            }
            if (string.IsNullOrEmpty(ReportEmail))
            {
                dialogs.Toast("Please enter your email id");
                return false;
            }
            if (!Regex.IsMatch(ReportEmail, Emailpattern))
            {
                dialogs.Toast("Please enter a valid email id");
                return false;
            }
            if (string.IsNullOrEmpty(FlagReason))
            {
                dialogs.Toast("Please write a description");
                return false;
            }
            return result;
        }
        public void taponreportentry()
        {
            IsVisibleReportList = true;
        }
        public void Taponreportflaglist(ReportFlag reportFlag)
        {
            Reportlist = reportFlag.reportlist;
            Reportid = reportFlag.reportid.ToString();
            IsVisibleReportList = false;
        }
        public async void Taponreportsubmitcmd()
        {
            if (validate())
            {
                try
                {
                    string reportid = Reportid;
                    if (string.IsNullOrEmpty(reportid))
                    {
                        reportid = "1";
                    }
                    string adid = PassContentid;
                    string email = ContactEmail;
                    if (Title == null)
                        Title = "";
                    var report = RestService.For<ICareDetail>(Commonsettings.DaycareAPI);
                    CareReportflagresult_DATA reportsubmit = await report.GetReport(adid,Title,reportid, AdUrl, ReportEmail,FlagReason);
                    if(reportsubmit.resultinfo == "ad exists")
                    {
                        dialogs.Toast("You are already reported!");
                    }
                    else
                    {
                        dialogs.Toast("Successfully reported.");
                    }
                    var currentpage = GetCurrentPage();
                    //Device.BeginInvokeOnMainThread(async () => 
                    await currentpage.Navigation.PopModalAsync();
                }
                catch (Exception e)
                {

                }
            }
        }
        public void TaponClose()
        {
            var currentpage = GetCurrentPage();
            Device.BeginInvokeOnMainThread(async () => await currentpage.Navigation.PopModalAsync());
        }
        public async void getdetailpage()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PopAsync();
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
