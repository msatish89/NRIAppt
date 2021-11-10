using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using Refit;
using NRIApp.Events.Features.Detail.Interface;
using NRIApp.Events.Features.Detail.Model;
using NRIApp.Helpers;
using NRIApp.Roommates.Features.Post.Interface;

namespace NRIApp.Events.Features.Detail.ViewModel
{
    public class Detail_ticket_VM : INotifyPropertyChanged
    {
        IUserDialogs _Dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;

        public static string tablerows = "";
        public static string tablerowsseats = "";
        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        string Namepattern = "^[0-9A-Za-z ]+$";
        private string _Name = "";

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {


                if (_Name.Length == 0)
                {
                    _Name = value.Trim();
                }
                else
                {
                    _Name = value;
                }



                OnPropertyChanged(Name);
            }
        }

        private string _LastName = "";
        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {


                if (_LastName.Length == 0)
                {
                    _LastName = value.Trim();
                }
                else
                {
                    _LastName = value;
                }



                OnPropertyChanged(LastName);
            }
        }
        private string _Email = "";
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value.Trim();
                OnPropertyChanged(Email);

            }
        }
        private string _Contactno = "";
        public string Contactno
        {
            get { return _Contactno; }
            set
            {

                _Contactno = value.Trim();
                OnPropertyChanged(Contactno);

            }
        }
        private string _Selectcontact = "+1";
        public string Selectcontact
        {
            get { return _Selectcontact; }
            set { _Selectcontact = value; }
        }
        public ICommand SubmitCommand { get; set; }
        string sEventid = "", sTicktingid = "", sTickettypeid = "";
        public Detail_ticket_VM(string eventid, string ticketingid, string tickettypeid)
        {
            sEventid = eventid; sTicktingid = ticketingid; sTickettypeid = tickettypeid;
            SubmitCommand = new Command(Oneventregister);
        }

        public async void Oneventregister()
        {
            if (Validations() && sTickettypeid != "")
            {
                try
                {
                    _Dialog.ShowLoading("");
                    string sUserpid = "", sUserguid = "";

                    var login = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
                    var logindata = await login.getuserpid(Name, Email);
                    //Commonsettings.UserPid = logindata.resultinformation;
                    //Commonsettings.UserName = Name;
                    //Commonsettings.UserEmail = Email;
                    //Commonsettings.UserMobileno = Contactno;


                    var userdetails = RestService.For<IEvent_Detail>(NRIApp.Helpers.Commonsettings.EventsAPI);
                    var details = await userdetails.getuserpidandguidbyemail(Email);

                    if (details.ROW_DATA.Count > 0)
                    {
                        sUserpid = details.ROW_DATA[0].pid;
                        sUserguid = details.ROW_DATA[0].guid;



                        EVENT_TICKET_DETAILS etd = new EVENT_TICKET_DETAILS
                        {
                            txtfirstname = Name,
                            txtlastname = LastName,
                            txtemail = Email,
                            txtworkphone = Contactno,
                            txtticketingtypeid = sTickettypeid,
                            hdn_objectid = sTicktingid,
                            Userpid = sUserpid,
                            Userguid = sUserguid

                        };

                        var register = RestService.For<IEvent_Detail>(NRIApp.Helpers.Commonsettings.EventsAPI);
                        var data = await register.rsvpregister(etd);
                        if (data != null && !string.IsNullOrEmpty(data.ordernumber) && data.ordernumber != "0")
                        {
                            var curpage = GetCurrentPage();

                            await curpage.Navigation.PushAsync(new NRIApp.Events.Features.Detail.Views.Register_Thankyou(data.orderguid, data.ordernumber, Email));

                        }
                    }
                    _Dialog.HideLoading();

                }
                catch (Exception e)
                {

                }
            }
        }

        public bool Validations()
        {
            var CurrentPage = GetCurrentPage();

            bool result = true;
            if (string.IsNullOrEmpty(Name) || !Regex.IsMatch(Name, Namepattern))
            {
                _Dialog.Toast("Enter Name");
                var entry = CurrentPage.FindByName<Entry>("uname");
                entry.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(Name) || !Regex.IsMatch(Name, Namepattern))
            {
                _Dialog.Toast("Enter Last Name");
                var entry = CurrentPage.FindByName<Entry>("ulastname");
                entry.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(Email))
            {
                _Dialog.Toast("Enter Email");
                var entry = CurrentPage.FindByName<Entry>("uemail");
                entry.Focus();
                return false;
            }
            if (!Regex.IsMatch(Email, Emailpattern))
            {
                _Dialog.Toast("Enter Proper Email");
                var entry = CurrentPage.FindByName<Entry>("uemail");
                entry.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(Contactno))
            {
                _Dialog.Toast("Enter Contact Number");
                var entry = CurrentPage.FindByName<Entry>("umobileno");
                entry.Focus();
                return false;
            }
            else if (Contactno.Length < 10)
            {

                Entry myEntry = CurrentPage.FindByName<Entry>("umobileno");
                myEntry.Focus();
                _Dialog.Toast("Please enter a 10-digit mobile number");
                return false;
            }
            else if (!CheckValidPhone(Contactno))
            {
                Entry myEntry = CurrentPage.FindByName<Entry>("umobileno");
                myEntry.Focus();
                _Dialog.Toast("Please enter valid mobile number");
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
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();

            return currentPage;
        }
    }
}
