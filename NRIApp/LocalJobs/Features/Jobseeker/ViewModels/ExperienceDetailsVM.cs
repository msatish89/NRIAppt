using NRIApp.LocalJobs.Features.Jobseeker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using Acr.UserDialogs;
using Refit;
using NRIApp.LocalJobs.Features.Jobseeker.Interface;
using NRIApp.Helpers;
using System.Text.RegularExpressions;

namespace NRIApp.LocalJobs.Features.Jobseeker.ViewModels
{
    public class ExperienceDetailsVM: INotifyPropertyChanged
    {
        IUserDialogs dialogs = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;

        public Command ContentViewTap { get; set; }
        public Command PopupContentTap { get; set; }
        public Command savedetails { get; set; }
        public Command canceldtl { get; set; }
        public Command Taponfrommonth { get; set; }
        public Command Taponfromyear { get; set; }
        public Command Tapontomonth { get; set; }
        public Command Tapontoyear { get; set; }
        public Command<Experience_Params> selectfrommonth { get; set; }
        public Command<Experience_Params> selectfromyear { get; set; }
        public Command<Experience_Params> selecttomonth { get; set; }
        public Command<Experience_Params> selecttoyear { get; set; }
        
            
        private bool _contentviewvisible = false;
        public bool contentviewvisible
        {
            get { return _contentviewvisible; }
            set { _contentviewvisible = value; OnPropertyChanged(nameof(contentviewvisible)); }
        }
        private bool _frommonthlistviewvisble = false;
        public bool frommonthlistviewvisble
        {
            get { return _frommonthlistviewvisble; }
            set { _frommonthlistviewvisble = value; OnPropertyChanged(nameof(frommonthlistviewvisble)); }
        }
        private bool _fromyearlistviewvisble = false;
        public bool fromyearlistviewvisble
        {
            get { return _fromyearlistviewvisble; }
            set { _fromyearlistviewvisble = value; OnPropertyChanged(nameof(fromyearlistviewvisble)); }
        }
        private bool _tomonthlistviewvisble = false;
        public bool tomonthlistviewvisble
        {
            get { return _tomonthlistviewvisble; }
            set { _tomonthlistviewvisble = value; OnPropertyChanged(nameof(tomonthlistviewvisble)); }
        }
        private bool _toyearlistviewvisble = false;
        public bool toyearlistviewvisble
        {
            get { return _toyearlistviewvisble; }
            set { _toyearlistviewvisble = value; OnPropertyChanged(nameof(toyearlistviewvisble)); }
        }
        private string _companyname = "";
        public string companyname
        {
            get { return _companyname; }
            set { _companyname = value; OnPropertyChanged(nameof(companyname)); }
        }
        private string _jobrole = "";
        public string jobrole
        {
            get { return _jobrole; }
            set { _jobrole = value; OnPropertyChanged(nameof(jobrole)); }
        }
        private string _expdesctxt = "";
        public string expdesctxt
        {
            get { return _expdesctxt; }
            set { _expdesctxt = value; OnPropertyChanged(nameof(expdesctxt)); }
        }
        private string _frommonth = "Month";
        public string frommonth
        {
            get { return _frommonth; }
            set { _frommonth = value; OnPropertyChanged(nameof(frommonth)); }
        }
        private string _frommonthID ;
        public string frommonthID
        {
            get { return _frommonthID; }
            set { _frommonthID = value; OnPropertyChanged(nameof(frommonthID)); }
        }
        private string _fromyear = "Year";
        public string fromyear
        {
            get { return _fromyear; }
            set { _fromyear = value; OnPropertyChanged(nameof(fromyear)); }
        }
        private string _tomonth = "Month";
        public string tomonth
        {
            get { return _tomonth; }
            set { _tomonth = value; OnPropertyChanged(nameof(tomonth)); }
        }
        private string _tomonthID;
        public string tomonthID
        {
            get { return _tomonthID; }
            set { _tomonthID = value; OnPropertyChanged(nameof(tomonthID)); }
        }
        private string _toyear = "Year";
        public string toyear
        {
            get { return _toyear; }
            set { _toyear = value; OnPropertyChanged(nameof(toyear)); }
        }
        public List<Experience_Params> frmskpmonthlist = new List<Experience_Params>();
        public List<Experience_Params> monthlist = new List<Experience_Params>()
        {
            new Experience_Params {months="0",monthname="Till date",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params {months="1",monthname="January",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params {months="2",monthname="February",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params {months="3",monthname="March",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params {months="4",monthname="April",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params {months="5",monthname="May",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params {months="6",monthname="June",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params {months="7",monthname="July",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params {months="8",monthname="August",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params {months="9",monthname="September",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params {months="10",monthname="October",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params {months="11",monthname="November",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params {months="12",monthname="December",checkimage="RadioButtonUnChecked.png"},
        };
        public List<Experience_Params> frnyearsskplist = new List<Experience_Params>();
        public List<Experience_Params> yearslist = new List<Experience_Params>()
        {
            new Experience_Params{years="Till date", checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2020",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2019",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2018",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2017",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2016",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2015",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2014",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2013",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2012",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2011",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2010",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2009",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2008",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2007",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2006",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2005",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2004",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2003",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2002",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2001",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="2000",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1999",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1998",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1997",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1996",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1995",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1994",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1993",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1992",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1991",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1990",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1989",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1988",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1987",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1986",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1985",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1984",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1983",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1982",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1981",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1980",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1979",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1978",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1977",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1976",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1975",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1974",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1973",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1972",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1971",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1970",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1969",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1968",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1967",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1966",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1965",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1964",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1963",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1962",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1961",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1960",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1959",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1958",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1957",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1956",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1955",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1954",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1953",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1952",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1951",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1950",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1949",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1948",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1947",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1946",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1945",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1944",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1943",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1942",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1941",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1940",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1939",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1938",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1937",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1936",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1935",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1934",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1933",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1932",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1931",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1930",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1929",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1928",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1927",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1926",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1925",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1924",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1923",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1922",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1921",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1920",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1919",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1918",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1917",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1916",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1915",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1914",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1913",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1912",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1911",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1910",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1909",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1908",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1907",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1906",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1905",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1904",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1903",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1902",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1901",checkimage="RadioButtonUnChecked.png"},
            new Experience_Params{years="1900",checkimage="RadioButtonUnChecked.png"},

        };
        Jobseekers_DATA data = new Jobseekers_DATA();
        public ExperienceDetailsVM(Jobseekers_DATA Sdata)
        {
            try
            {
                dialogs.ShowLoading("",null);
                data = Sdata;
                getexperiencedata(data);
                ContentViewTap = new Command(Closecontentview);
                PopupContentTap = new Command(showcontentview);
                savedetails = new Command(saveexperiencedetails);
                canceldtl = new Command(cancel);
                Taponfrommonth = new Command(showfrommonthlist);
                Taponfromyear = new Command(showfromyearlist);
                Tapontomonth = new Command(showtomonthlist);
                Tapontoyear = new Command(showtoyearlist);
                selectfrommonth = new Command<Experience_Params>(selectfommonthID);
                selectfromyear = new Command<Experience_Params>(selectfromyearID);
                selecttomonth = new Command<Experience_Params>(selecttomonthID);
                selecttoyear = new Command<Experience_Params>(selecttoyearID);
                dialogs.HideLoading();
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        public void getexperiencedata(Jobseekers_DATA Sdata)
        {
            try
            {
                dialogs.ShowLoading("", null);
                if (!string.IsNullOrEmpty(Sdata.txtcmyname))
                {
                    // data.txtcmyname = Sdata.txtcmyname;
                    companyname = Sdata.txtcmyname;
                }
                if (!string.IsNullOrEmpty(Sdata.txtrole))
                {
                    // data.txtrole = Sdata.txtrole;
                    jobrole = Sdata.txtrole;
                }
                if (!string.IsNullOrEmpty(Sdata.frmmonth))
                {
                    //  data.frmmonth = Sdata.frmmonth;
                    frommonth =  Sdata.frmmonthnametxt;
                    frommonthID = Sdata.frmmonth;
                    //var curntpg = GetCurrentpage();
                    //Label frommonthtxtclr = curntpg.FindByName<Label>("frommonth");
                    //frommonthtxtclr.TextColor = Color.FromHex("#212121");
                }
                if (!string.IsNullOrEmpty(Sdata.tomonth))
                {
                    //data.txtcmyname = Sdata.tomonth;
                    tomonth = Sdata.tomonthname;
                    tomonthID = Sdata.tomonth;
                    //var curntpg = GetCurrentpage();
                    //Label tomonthtxtclr = curntpg.FindByName<Label>("tomonth");
                    //tomonthtxtclr.TextColor = Color.FromHex("#212121");
                }
                if (!string.IsNullOrEmpty(Sdata.frmyear))
                {
                    // data.txtcmyname = Sdata.tomonth;
                    fromyear = Sdata.frmyear;
                    //var curntpg = GetCurrentpage();
                    //Label fromyeartxtclr = curntpg.FindByName<Label>("fromyear");
                    //fromyeartxtclr.TextColor = Color.FromHex("#212121");
                }
                if (!string.IsNullOrEmpty(Sdata.toyear))
                {
                    //  data.txtcmyname = Sdata.tomonth;
                    toyear = Sdata.toyear;
                    //var curntpg = GetCurrentpage();
                    //Label toyeartxtclr = curntpg.FindByName<Label>("toyear");
                    //toyeartxtclr.TextColor = Color.FromHex("#212121");
                }
                if (!string.IsNullOrEmpty(Sdata.txtareadescrip))
                {
                    // data.txtareadescrip = Sdata.txtareadescrip;
                    expdesctxt = Sdata.txtareadescrip;
                }
                dialogs.HideLoading();

            }
            catch(Exception ex)
            {

            }
        }
        public void showfrommonthlist()
        {
            var curentpage = GetCurrentpage();
            var frommonthlist = curentpage.FindByName<ListView>("frommonthlst");
            frmskpmonthlist = monthlist.Skip(1).ToList();
            foreach (var data in frmskpmonthlist)
            {
                if (frommonth == data.monthname)
                {
                    data.checkimage = "RadioButtonChecked.png";
                    frommonth = data.monthname;
                }
                else
                {
                    data.checkimage = "RadioButtonUnChecked.png";
                }
            }
            frommonthlist.ItemsSource = null;
            frommonthlist.ItemsSource = frmskpmonthlist;
            contentviewvisible = true;
            frommonthlistviewvisble = true;
            fromyearlistviewvisble = false;
            tomonthlistviewvisble = false;
            toyearlistviewvisble = false;
        }
        public void selectfommonthID(Experience_Params c_param)
        {
            var curntpg = GetCurrentpage();
            Label frommonthtxtclr = curntpg.FindByName<Label>("frommonth");
            frommonthtxtclr.TextColor = Color.FromHex("#212121");
            frommonth = c_param.monthname;
            frommonthID = c_param.months;
            contentviewvisible = false;
        }
        public void showfromyearlist()
        {
            var curentpage = GetCurrentpage();
            var fromyearlist = curentpage.FindByName<ListView>("fromyearlst");
            frnyearsskplist = yearslist.Skip(1).ToList();
            foreach (var data in frnyearsskplist)
            {
                if (fromyear == data.years)
                {
                    data.checkimage = "RadioButtonChecked.png";
                    fromyear = data.years;
                }
                else
                {
                    data.checkimage = "RadioButtonUnChecked.png";
                }
            }
            fromyearlist.ItemsSource = null;
            fromyearlist.ItemsSource = frnyearsskplist;
            contentviewvisible = true;
            frommonthlistviewvisble = false;
            fromyearlistviewvisble = true;
            tomonthlistviewvisble = false;
            toyearlistviewvisble = false;
        }
        public void selectfromyearID(Experience_Params c_param)
        {
            var curntpg = GetCurrentpage();
            Label fromyeartxtclr = curntpg.FindByName<Label>("fromyear");
            fromyeartxtclr.TextColor = Color.FromHex("#212121");
            fromyear = c_param.years;
            contentviewvisible = false;
        }
        public void showtomonthlist()
        {
            var curentpage = GetCurrentpage();
            var tomonthlist = curentpage.FindByName<ListView>("tomonthlst");

            foreach (var data in monthlist)
            {
                if (tomonth == data.monthname)
                {
                    data.checkimage = "RadioButtonChecked.png";
                    tomonth = data.monthname;
                }
                else
                {
                    data.checkimage = "RadioButtonUnChecked.png";
                }
            }
            tomonthlist.ItemsSource = null;
            tomonthlist.ItemsSource = monthlist;
            contentviewvisible = true;
            frommonthlistviewvisble = false;
            fromyearlistviewvisble = false;
            tomonthlistviewvisble = true;
            toyearlistviewvisble = false;
        }
        public void selecttomonthID(Experience_Params c_param)
        {
            var curntpg = GetCurrentpage();
            Label tomonthtxtclr = curntpg.FindByName<Label>("tomonth");
            tomonthtxtclr.TextColor = Color.FromHex("#212121");
            tomonth = c_param.monthname;
            
            if (c_param.monthname == "Till date")
            {
               // c_param.monthname= DateTime.Today.Month.ToString();
                tomonthID = DateTime.Today.Month.ToString();
            }
            else
            {
                tomonthID = c_param.months;
            }
            
            contentviewvisible = false;
        }
        public void showtoyearlist()
        {
            var curentpage = GetCurrentpage();
            var toyearlist = curentpage.FindByName<ListView>("toyearlst");
            foreach (var data in yearslist)
            {
                if (toyear == data.years)
                {
                    data.checkimage = "RadioButtonChecked.png";
                    toyear = data.years;
                }
                else
                {
                    data.checkimage = "RadioButtonUnChecked.png";
                }
            }
            toyearlist.ItemsSource = null;
            toyearlist.ItemsSource = yearslist;
            contentviewvisible = true;
            frommonthlistviewvisble = false;
            fromyearlistviewvisble = false;
            tomonthlistviewvisble = false;
            toyearlistviewvisble = true;
        }
        public void selecttoyearID(Experience_Params c_param)
        {
            var curntpg = GetCurrentpage();
            Label toyeartxtclr = curntpg.FindByName<Label>("toyear");
            toyeartxtclr.TextColor = Color.FromHex("#212121");
            if (c_param.years == "Till date")
            {
                toyear = DateTime.Today.Year.ToString();
            }
            else
            {
                toyear = c_param.years;
            }
            
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
        public bool validation()
        {
            bool result = true;
            if(string.IsNullOrEmpty(companyname) || companyname.Trim().Length == 0)
            {
                dialogs.Toast("Please enter company name");
                return false;
            }
            if(companyname.Trim().Length!=0)
            {
                if (!CheckAplhaOnly(companyname))   //|| expdesctxt.Trim().Length==0
                {
                    dialogs.Toast("Please enter company name");
                    return false;
                }
            }
            
            if(jobrole.Trim().Length!=0)
            {
                    if (!CheckAplhaOnly(jobrole))   //|| expdesctxt.Trim().Length==0
                    {
                        dialogs.Toast("Please enter your designation...");
                        return false;
                    }
            }
            if (string.IsNullOrEmpty(frommonthID))
            {
                dialogs.Toast("Please enter experience in month");
                return false;
            }
            if (string.IsNullOrEmpty(fromyear))
            {
                dialogs.Toast("Please enter experience in years");
                return false;
            }
            if (string.IsNullOrEmpty(tomonthID))
            {
                dialogs.Toast("Please enter experience in month");
                return false;
            }
            if (string.IsNullOrEmpty(toyear))
            {
                dialogs.Toast("Please enter experience in years");
                return false;
            }
            if (!string.IsNullOrEmpty(fromyear) && !string.IsNullOrEmpty(toyear))
            {
                if(fromyear==toyear)
                {
                    if (Convert.ToInt32(frommonthID) > Convert.ToInt32(tomonthID))
                        {
                        dialogs.Toast("To month should be greater than From month");
                        return false;
                    }
                }
               if (Convert.ToInt32(fromyear) > Convert.ToInt32(toyear))
                    {
                    //dialogs.Toast("Current experience should be greater than from experience");
                    dialogs.Toast("To year should be greater than From year");
                        return false;
                    }
                
            }
           if(expdesctxt.Trim().Length!=0)
            {
                if (!CheckAplhaOnly(expdesctxt))   //|| expdesctxt.Trim().Length==0
                {
                    dialogs.Toast("Description should have some letters...");
                    return false;
                }
            }
           
            return result;
        }
        bool CheckAplhaOnly(string inputString)
        {
            return Regex.IsMatch(inputString, "^[a-zA-Z ]+$");
        }
        public async void saveexperiencedetails()
        {
            try
            {
                if (validation())
                {
                    data.txtcmyname = companyname;
                    data.txtrole = jobrole;
                    data.frmmonth = frommonthID;
                    data.frmyear = fromyear;
                    data.tomonth = tomonthID;
                    data.toyear = toyear;
                    data.txtareadescrip = expdesctxt;
                    data.block = "company";
                    if(data.toyear== "Till date")
                    {
                        data.toyear = DateTime.Today.Year.ToString();
                    }
                    //if (data.tomonth == "0")
                    //{
                    //    data.toyear = DateTime.Today.Month.ToString();
                    //}
                    var profileupdata = RestService.For<IJobseeker>(Commonsettings.LocaljobsAPI);
                    var profiledatas = await profileupdata.profilepagecreate(data);
                   // if (profiledatas != null)
                    //{
                        var currentpage = GetCurrentpage();
                    //currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                    //currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                    Xamarin.Forms.Page page1 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1];
                    Xamarin.Forms.Page page2 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 2];
                    currentpage.Navigation.RemovePage(page1);
                    currentpage.Navigation.RemovePage(page2);
                    await currentpage.Navigation.PushAsync(new Jobseeker.View.Jobseekerprofile(data));
                    //}
                   // else
                    //{
                        //dialogs.Toast("Try again later...");
                    //}
                }
            }
            catch (Exception ex)
            {
                dialogs.Toast("Try again later...");
            }
        }
        public void cancel()
        {
            var currentpage = GetCurrentpage();
            currentpage.Navigation.PopAsync();
        }

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
