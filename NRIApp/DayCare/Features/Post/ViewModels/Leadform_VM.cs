using System;
using System.Collections.Generic;
using Acr.UserDialogs;
using System.Text;
using System.Text.RegularExpressions;
using NRIApp.Helpers;
using Xamarin.Forms;
using System.ComponentModel;
using NRIApp.DayCare.Features.Post.Models;
using System.Runtime.CompilerServices;
using System.Linq;
using Refit;
using NRIApp.DayCare.Features.Post.Interface;
using System.Threading.Tasks;

namespace NRIApp.DayCare.Features.Post.ViewModels
{
    public class Leadform_VM : INotifyPropertyChanged
    {
        IUserDialogs Dialogs = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;

        public Command GenderMaleCommand { get; set; }
        public Command GenderFemaleCommand { get; set; }
        public Command GenderAnyCommand { get; set; }
        public Command TapBackPage { get; set; }
        public Command TapSearchPage { get; set; }
        public Command<LocationData> LocationCmd { get; set; }
        public Command<DC_CategoryList> TapOnNext { get; set; }


        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";

        private DateTime _FrDate = DateTime.Today;
        private string _MaleImg;
        private string _FeMaleImg;
        private string _AnyImg;

        private string _expsalary;

        private string _LocationName = "";

        public string TempLocationName = "";

        private string _Newycityurl = "";

        bool _IsVisibleList = false;

        private string _ContactName = "";
        private string _ContactEmail = "";
        private string _ContactPhone = "";

        private Command _lcfcmdsubmit;

        private List<string> _Countrycode;

        private string _Selectcountry = "+1";


        bool _IsBusy = false;
       
       
        public DateTime FrDate
        {
            get { return _FrDate; }
            set { _FrDate = value; }
        }
        public string MaleImg
        {
            get { return _MaleImg; }
            set
            {
                _MaleImg = value;
                OnPropertyChanged(nameof(MaleImg));
            }
        }

        public string FeMaleImg
        {
            get { return _FeMaleImg; }
            set
            {
                _FeMaleImg = value;
                OnPropertyChanged(nameof(FeMaleImg));
            }
        }

        public string AnyImg
        {
            get { return _AnyImg; }
            set
            {
                _AnyImg = value;
                OnPropertyChanged(nameof(AnyImg));
            }
        }
        public string ExpSalary
        {
            get
            {
                return _expsalary;
            }
            set
            {
                _expsalary = value;
                OnPropertyChanged(nameof(ExpSalary));
            }
        }

       

        public string Gender = "any";

        public string LocationName
        {
            get { return _LocationName; }
            set
            {
                _LocationName = value;
                OnPropertyChanged(nameof(LocationName));
                if (!(string.IsNullOrEmpty(LocationName)) && (TempLocationName != LocationName))
                {
                    GetLocationname();
                }
            }
        }

        public string Newycityurl
        {
            get { return _Newycityurl; }
            set { _Newycityurl = value; }
        }
        private List<LocationData> _LocationList { get; set; }
        public List<LocationData> LocationList
        {
            get { return _LocationList; }
            set
            {
                _LocationList = value;
            }
        }

        public bool IsVisibleList
        {
            get { return _IsVisibleList; }
            set
            {
                _IsVisibleList = value;
                OnPropertyChanged();
            }
        }

        public string ContactName
        {
            get { return _ContactName; }
            set
            {
                _ContactName = value;
                OnPropertyChanged();
            }
        }
        public string ContactEmail
        {
            get { return _ContactEmail; }
            set
            {
                _ContactEmail = value;
                OnPropertyChanged();
            }
        }

        public string ContactPhone
        {
            get { return _ContactPhone; }
            set
            {
                _ContactPhone = value;
                OnPropertyChanged();
            }
        }

        
       

        public Command lcfcmdsubmit
        {
            get { return _lcfcmdsubmit; }
            set { _lcfcmdsubmit = value; }
        }

        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
            }
        }
        public List<string> Countrycode
        {
            get { return _Countrycode; }
            set{_Countrycode = value;}
        }
        private List<string> _paytype;
        public List<string> PayType
        {
            get { return _paytype; }
            set { _paytype = value; }
        }
        private List<string> _listofpaytypes;
        public List<string> Listofpaytypes
        {
            get { return _listofpaytypes; }
            set { _listofpaytypes = value;OnPropertyChanged();}
        }
        List<string> Countryitem = new List<string>() { "+1","+91" };
        List<string> ListofPayType = new List<string>()
        {
            "Hourly","Daily","Weekly","Monthly"
        };

        public string Selectcountry
        {
            get { return _Selectcountry; }
            set
            {
                _Selectcountry = value;
                OnPropertyChanged(Selectcountry);
            }
        }
        private string _selectpaytype= "Hourly";
        public string SelectPaytype
        {
            get { return _selectpaytype; }
            set { _selectpaytype = value; OnPropertyChanged(SelectPaytype); }
        }

        public bool Validations()
        {
            bool result = true;
            var currentpage = GetCurrentPage();
            if (string.IsNullOrEmpty(ExpSalary))
            {
                Entry myEntry = currentpage.FindByName<Entry>("ExpSalary");
                myEntry.Focus();
                Dialogs.Toast("Enter Rent");
                return false;
            }

            if (ExpSalary.Length > 6)
            {
                Entry myEntry = currentpage.FindByName<Entry>("ExpSalary");
                myEntry.Focus();
                Dialogs.Toast("Maximum 6 Digits only");
                return false;
            }
            return result;
        }
        public bool Validationssecond()
        {
            bool result = true;
            var currentpage = GetCurrentPage();
            if (string.IsNullOrEmpty(LocationName))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtlocation");
                myEntry.Focus();
                Dialogs.Toast("Enter Location");
                return false;
            }
            if (string.IsNullOrEmpty(ContactName))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactname");
                myEntry.Focus();
                Dialogs.Toast("Enter Contact Name");
                return false;
            }
            if (string.IsNullOrEmpty(ContactEmail))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactemail");
                myEntry.Focus();
                Dialogs.Toast("Enter Contact Email");
                return false;
            }
            if (!Regex.IsMatch(ContactEmail, Emailpattern))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactemail");
                myEntry.Focus();
                Dialogs.Toast("Enter Proper Email");
                return false;
            }
            if (string.IsNullOrEmpty(ContactPhone))
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactphone");
                myEntry.Focus();
                Dialogs.Toast("Enter Contact Number");
                return false;
            }
            if (ContactPhone.Length < 10)
            {
                Entry myEntry = currentpage.FindByName<Entry>("txtcontactphone");
                myEntry.Focus();
                Dialogs.Toast("Minimum 10 Digits required");
                return false;
            }

            return result;
        }

        DC_CategoryList ctl = new DC_CategoryList();
        public Leadform_VM()
        {

        }
        public Leadform_VM(DC_CategoryList clist)
        {
            try
            {
                ctl.supercategoryid = clist.supercategoryid;
                ctl.primarycategoryid = clist.primarycategoryid;
                ctl.secondarycategoryid = clist.secondarycategoryid;
                ctl.categoryname = clist.categoryname;
                ctl.maincategory = clist.maincategory;

                TapBackPage = new Command(ClickBackPage);
                TapSearchPage = new Command(ClickSearchPage);

                MaleImg = "RadioButtonUnChecked.png";
                FeMaleImg = "RadioButtonUnChecked.png";
                AnyImg = "RadioButtonChecked.png";
                GenderMaleCommand = new Command(ChangeGenderMale);
                GenderFemaleCommand = new Command(ChangeGenderFemale);
                GenderAnyCommand = new Command(ChangeGenderAny);
                TapOnNext = new Command<DC_CategoryList>(clicklcfcmdnext);
                Countrycode = Countryitem;
                PayType = ListofPayType;
                OnPropertyChanged(nameof(PayType));
            }
            catch (Exception e) { }
            LocationCmd = new Command<LocationData>(OnLocationClick);
        }

        public Leadform_VM(DC_CategoryList clist, DateTime frdate, string gender, string ExpSalary)
        {
            try
            {

                ctl.supercategoryid = clist.supercategoryid;
                ctl.primarycategoryid = clist.primarycategoryid;
                ctl.secondarycategoryid = clist.secondarycategoryid;
                ctl.categoryname = clist.categoryname;
                ctl.maincategory = clist.maincategory;

                TapBackPage = new Command(ClickBackPage);
                TapSearchPage = new Command(ClickSearchPage);

                if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                    ContactEmail = Commonsettings.UserEmail;
                if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                    ContactName = Commonsettings.UserName;
                if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                    ContactPhone = Commonsettings.UserMobileno;
                if (Commonsettings.Usercity != null && Commonsettings.Usercity != "")
                {
                    LocationName = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                    TempLocationName = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                    Newycityurl = Commonsettings.Usercityurl;
                }
                Countrycode = Countryitem;
                OnPropertyChanged(nameof(Countrycode));

              
                LocationCmd = new Command<LocationData>(OnLocationClick);

                //  lcfcmdsubmit = new Command(async () => await clicklcfcmdsubmit(frdate, gender, ExpSalary));
            }
            catch (Exception e) { }

        }
        public async void ClickBackPage()
        {
            var curpage = GetCurrentPage();
            await curpage.Navigation.PopAsync();
        }
        public async void ClickSearchPage()
        {
            var curpage = GetCurrentPage();
            //await curpage.Navigation.PushAsync(new DCsearch());
        }

        public void ChangeGenderMale()
        {
            AnyImg = "RadioButtonUnChecked.png";
            MaleImg = "RadioButtonChecked.png";
            FeMaleImg = "RadioButtonUnChecked.png";
            Gender = "male";
        }
        public void ChangeGenderFemale()
        {
            AnyImg = "RadioButtonUnChecked.png";
            MaleImg = "RadioButtonUnChecked.png";
            FeMaleImg = "RadioButtonChecked.png";
            Gender = "female";
        }
        public void ChangeGenderAny()
        {
            AnyImg = "RadioButtonChecked.png";
            MaleImg = "RadioButtonUnChecked.png";
            FeMaleImg = "RadioButtonUnChecked.png";
            Gender = "any";
        }
        public async void clicklcfcmdnext(DC_CategoryList catlist)
        {
            try
            {
                if (Validations() && Validationssecond())
                {
                    // Dialogs.ShowLoading("");
                    //var curpage = GetCurrentPage();
                    // await curpage.Navigation.PushAsync(new Leadform_Two(ctl, FrDate, Gender, ExpSalary));
                    // await DisplayAlert("response", "ok", "Exit");
                    //Dialogs.HideLoading();
                    Dialogs.Toast("Validated");

                }
                else
                {
                    Dialogs.Toast("Not validataed");
                }
            }
            catch (Exception e) { }
        }

        public async void GetLocationname()
        {
            var LocationAPI = RestService.For<ICategory>(Commonsettings.Localservices);
            Models.ListOfLocationData response = await LocationAPI.getlocation(LocationName);
            foreach (var item in response.ROW_DATA)
            {
                item.citystatecode = item.city + ", " + item.statecode;
            }
            LocationList = response.ROW_DATA;
            OnPropertyChanged(nameof(LocationList));
            IsVisibleList = true;
        }

        public  void OnLocationClick(LocationData obj)
        {
            var city = obj.citystatecode;
            LocationName = TempLocationName = city;
            Newycityurl = obj.newcityurl;
            IsVisibleList = false;
        }

        public async Task clicklcfcmdsubmit(DateTime dt, string gender, string expSalary)
        {
            try
            {
                if (Validationssecond())
                {
                    //IsBusy = true;
                    Dialogs.ShowLoading("");
                    // Response res = new Response();
                    DC_CategoryList loc = new DC_CategoryList();
                    loc.adtype = Commonsettings.CAdTypeID.ToString();
                    //loc.RMcategory = Commonsettings.CRMCategory;
                    loc.supercategoryid = ctl.supercategoryid;
                    loc.primarycategoryid = ctl.primarycategoryid;
                    loc.secondarycategoryid = ctl.secondarycategoryid;
                    loc.categoryname = ctl.categoryname;
                    loc.maincategory = ctl.maincategory;
                    loc.gender = gender;
                    string sdate = dt.ToString("MM/dd/yyyy");
                    loc.NeedFromDate  = sdate;
                    loc.NeedToDate = sdate;
                    loc.ExpSalary = expSalary;
                    loc.Locationname = LocationName;
                    loc.Newcityurl = Newycityurl;
                    loc.ContactName = ContactName;
                    loc.ContactEmail = ContactEmail;
                    loc.Countrycode = Selectcountry;
                    loc.txtcontactphone = ContactPhone;

                  //  var LCFSubmit = RestService.For<ILCF>("https://www.sulekha.ae/");
                   // var data = await LCFSubmit.Responselcfsubmit(loc);
                   // var result = data.ROW_DATA;

                  //  if (result != null)
                  //  {
                     //   var curpage = GetCurrentPage();
                        // await curpage.Navigation.PushAsync(new lcfsuccess(result.First().result, result.First().allrespid, res.RMcategory, res.Movedate, res.gender, res.Newcityurl));
                   // }
                    // IsBusy = false;
                  //  dialog.HideLoading();
                }
            }
            catch (Exception e) { }
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
