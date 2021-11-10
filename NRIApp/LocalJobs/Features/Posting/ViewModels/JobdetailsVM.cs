using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Posting.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs;
using NRIApp.LocalJobs.Features.Posting.Interfaces;
using NRIApp.Techjobs.Features.LeadForm.Models;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using System.Runtime.CompilerServices;
using Plugin.Connectivity;

namespace NRIApp.LocalJobs.Features.Posting.ViewModels
{
   public class JobdetailsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand Cityajaxcommand { get; set; }
        public ICommand Sbmtjobdetailscommand { get; set; }
        public ICommand Taponsalmode { get; set; }
        public ICommand TapSelectsalmode { get; set; }
        public ICommand ContentViewTap { get; set; }
        public Command TCcheckcmd { get; set; }
        public Command TCfreshercheckcmd { get; set; }

        public List<LocalJobs.Features.Listing.Models.Filterdata> getemployementtypelist { get; set; }
        public List<LocalJobs.Features.Listing.Models.Filterdata> getemployementtypelist1 { get; set; }

        private int _BestinIndustryID = 0;
        public int BestinIndustryID
        {
            get { return _BestinIndustryID; }
            set { _BestinIndustryID = value;}
        }
        private int _FresherID = 0;
        public int FresherID
        {
            get { return _FresherID; }
            set { _FresherID = value; }
        }

        private string _checkedemployementtypevalue = "";
        public string checkedemployementtypevalue
        {
            get { return _checkedemployementtypevalue; }
            set { _checkedemployementtypevalue = value; OnPropertyChanged(nameof(checkedemployementtypevalue)); }
        }
        Postingdata data = new Postingdata();
        IUserDialogs dialog = UserDialogs.Instance;
        public List<Salarymodes> salmodedata { get; set; }
        public List<Salarymodes> salmodelist = new List<Salarymodes>()
        {
            new Salarymodes { salmode = "Per Hour", salmodeid = "182", salmodeval="Hourly", checkimage = "RadioButtonUnChecked.png" },
            new Salarymodes { salmode = "Per Day", salmodeid = "183", salmodeval="Daily", checkimage = "RadioButtonUnChecked.png" },
            new Salarymodes { salmode = "Per Week", salmodeid = "184", salmodeval="Weekly", checkimage = "RadioButtonUnChecked.png" },
             new Salarymodes { salmode = "Per Month", salmodeid = "185", salmodeval="Monthly", checkimage = "RadioButtonChecked.png" },
             new Salarymodes { salmode = "Per Year", salmodeid = "192", salmodeval="Yearly", checkimage = "RadioButtonUnChecked.png" }
        };
        private string _RecImg;
        private string _ConsuImg;

        public string RecImg
        {
            get { return _RecImg; }
            set
            {
                _RecImg = value;
                OnPropertyChanged(nameof(RecImg));
            }
        }

        public string ConsuImg
        {
            get { return _ConsuImg; }
            set
            {
                _ConsuImg = value;
                OnPropertyChanged(nameof(ConsuImg));
            }
        }
        public Command TapRecruiter { get; set; }
        public Command TapConsultant { get; set; }
        public JobdetailsVM(Postingdata pd)
        {
            RecImg = "RadioButtonUnChecked.png";
            ConsuImg = "RadioButtonChecked.png";
            data = pd;
            data.Companytype = "Consultancy";
            //data.functionalareaid = pd.functionalareaid;
            //data.jobtype = pd.jobtype;
            //data.roleid = pd.roleid;
            //data.rolename = pd.rolename;
            //data.skillids = pd.skillids;
            //data.skills = pd.skills;

            salmodedata = salmodelist;
            Cityajaxcommand = new Command<Cityajax>(Selectajaxcity);
            Sbmtjobdetailscommand = new Command(sbmtjobdetails);
            Taponsalmode = new Command<Salarymodes>(showsalmodelist);
            TapSelectsalmode = new Command<Salarymodes>(Selectsalmode);
            ContentViewTap = new Command(SelectContentview);

            TCcheckcmd = new Command(contactallowpermission);
            TCfreshercheckcmd = new Command(selectfresher);
            TapRecruiter = new Command(ChangeRecruiter);
            TapConsultant = new Command(ChangeConsultant);
            getemployementtype("");

        }
        public void ChangeRecruiter()
        {
            RecImg = "RadioButtonChecked.png";
            ConsuImg = "RadioButtonUnChecked.png";
            data.Companytype = "Recruiter";
        }
        public void ChangeConsultant()
        {
            RecImg = "RadioButtonUnChecked.png";
            ConsuImg = "RadioButtonChecked.png";
            data.Companytype = "Consultancy";
        }
        public async void getemployementtype(string employementtype)
        {
            try
            {

                dialog.ShowLoading("", null);
                if(!string.IsNullOrEmpty(data.employmenttype))
                {
                    employementtype = data.employmenttype;
                }
                var LJAPI = RestService.For<Listing.Interfaces.ILocalJob>(Commonsettings.LocaljobsAPI);
                Listing.Models.FilterListings list = await LJAPI.Getfilterdata("5");

                double lstcnt = list.ROW_DATA.Count;
                double halfcnt = Math.Round(lstcnt / 2, 1);
                getemployementtypelist = list.ROW_DATA.Take(Convert.ToInt32(halfcnt)).ToList();
                getemployementtypelist1 = list.ROW_DATA.Skip(Convert.ToInt32(halfcnt)).ToList();
                foreach (var data in getemployementtypelist)
                {
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    chbx.Text = data.tag;
                    chbx.ClassId = data.tag;
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_employementypeCheckChanged;
                    chbx.Padding = 5;
                    if (!string.IsNullOrEmpty(employementtype))
                    {
                        string[] emptypelst = employementtype.Split(',');
                        foreach (var i in emptypelst)
                        {
                            if (data.tag == i)
                            {
                                chbx.IsChecked = true;
                                if (string.IsNullOrEmpty(checkedemployementtypevalue))
                                {
                                    checkedemployementtypevalue += chbx.ClassId;
                                }
                                else
                                {
                                    checkedemployementtypevalue += "," + chbx.ClassId;
                                }
                            }
                        }
                    }
                    var currentpage = GetCurrentPage();
                    var Employmenttypelayout = currentpage.FindByName<StackLayout>("Employmenttypelayout");
                    Employmenttypelayout.Children.Add(chbx);
                }
                foreach (var data in getemployementtypelist1)
                {
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                   // chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    chbx.Text = data.tag;
                    chbx.ClassId = data.tag;
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_employementypeCheckChanged;
                    chbx.Padding = 5;
                    if (!string.IsNullOrEmpty(employementtype))
                    {
                        string[] emptypelst = employementtype.Split(',');
                        foreach (var i in emptypelst)
                        {
                            if (data.tag == i)
                            {
                                chbx.IsChecked = true;
                                if(string.IsNullOrEmpty(checkedemployementtypevalue))
                                {
                                    checkedemployementtypevalue += chbx.ClassId;
                                }
                                else
                                {
                                    checkedemployementtypevalue += "," + chbx.ClassId;
                                }
                            }
                        }
                    }
                    var currentpage = GetCurrentPage();
                    var Employmenttypelayout = currentpage.FindByName<StackLayout>("Employmenttypelayout1");
                    Employmenttypelayout.Children.Add(chbx);
                }
                if(data.ismyneed=="1")
                {
                    prefilldata();
                }
                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
                string msg = ex.Message;
            }

        }
        public void prefilldata()
        {
            try
            {
                Trainingcity = Selcity = SelTrainingcity = data.city + ", " + data.statecode;
                AdTitle = data.title;
                Desc = data.desc.Replace("<span>","").Replace("</span>","").Replace("<p>", "").Replace("</p>", "");
                Minexp = data.Experiencefrom;
                Maxexp = data.Experienceto;
                Minsal = data.minsalary;
                Maxsal = data.maxsalary;
                //is fresher check  checkbxfresherimg
                Selsalmodetxt = data.salmode;
                zipcode = data.zipcode;
                statecode = data.statecode;
                state = data.state;
                country = data.country;
                city = data.city;
                latitude = data.lat;
                longitude = data.longitude;
                Selsalmodeval = data.salmode;
                Selsalmodeid = data.salmodeid;
                
                //best in industry check checkbximg
                if (!string.IsNullOrEmpty(data.Bestindustry))
                {
                    if(data.Bestindustry=="1")
                    {
                        BestinIndustryID = 0;
                    }
                    contactallowpermission();
                }
                if ((data.minexp=="0"&& data.maxexp == "0")||( (string.IsNullOrEmpty(data.minexp)|| string.IsNullOrEmpty(data.maxexp))))
                {
                    FresherID = 0;
                    selectfresher();
                }

            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private void Chb_employementypeCheckChanged(object sender, EventArgs e)
        {
            try
            {
                var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
                if (category.IsChecked==true)
                {
                    if (checkedemployementtypevalue != null && checkedemployementtypevalue != "")
                    {
                        if (!checkedemployementtypevalue.Contains(category.ClassId))
                        {
                            checkedemployementtypevalue += "," + category.ClassId;
                        }
                    }
                    else
                    {
                        checkedemployementtypevalue += category.ClassId;
                    }
                }
                else
                {
                    string[] indexvalue = checkedemployementtypevalue.Split(',');
                    int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                    indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                    checkedemployementtypevalue = string.Join(",", indexvalue);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        private bool _expvisible = true;
        public bool expvisible
        {
            get { return _expvisible; }
            set { _expvisible = value;OnPropertyChanged(nameof(expvisible)); }
        }
        private bool _salaryvisible = true;
        public bool salaryvisible
        {
            get { return _salaryvisible; }
            set { _salaryvisible = value; OnPropertyChanged(nameof(salaryvisible)); }
        }

        public void selectfresher()
        {
            var currentpg = GetCurrentPage();
            if (FresherID == 0)
            {
                expvisible = false;
                var checkbximg = currentpg.FindByName<Image>("checkbxfresherimg");
                checkbximg.Source = "CheckBoxChecked.png";
                FresherID = 1;
            }
            else
            {
                var checkbximg = currentpg.FindByName<Image>("checkbxfresherimg");
                checkbximg.Source = "CheckBoxUnChecked.png";
                FresherID = 0;
                expvisible = true;
            }
        }
        public void contactallowpermission()
        {
            var currentpg = GetCurrentPage();
            if (BestinIndustryID == 0)
            {
                var checkbximg = currentpg.FindByName<Image>("checkbximg");
                checkbximg.Source = "CheckBoxChecked.png";
                BestinIndustryID = 1;
                salaryvisible = false;
            }
            else
            {
                var checkbximg = currentpg.FindByName<Image>("checkbximg");
                checkbximg.Source = "CheckBoxUnChecked.png";
                BestinIndustryID = 0;
                salaryvisible = true;
            }
        }
        public void SelectContentview()
        {
            salmodetypevisible = false;
        }
        public void showsalmodelist(Salarymodes sal)
        {
            var curentpage = GetCurrentPage();
            var salmodetype = curentpage.FindByName<ListView>("salmodetype");
            foreach (var amt in salmodelist)
            {
                if (Selsalmodeid == amt.salmodeid)
                {
                    amt.checkimage = "RadioButtonChecked.png";
                    Selsalmodetxt = amt.salmode;
                    Selsalmodeid = amt.salmodeid;
                    Selsalmodeval = amt.salmodeval;
                }
                else
                {
                    amt.checkimage = "RadioButtonUnChecked.png";
                }

            }

            salmodetype.ItemsSource = null;
            salmodetype.ItemsSource = salmodelist;
            //contentviewvisible = true;
            salmodetypevisible = true;
        }

        public void Selectsalmode(Salarymodes sal)
        {
            Selsalmodetxt = sal.salmode;
            Selsalmodeid = sal.salmodeid;
            Selsalmodeval = sal.salmodeval;
            salmodetypevisible = false;
            var curntpg = GetCurrentPage();
            Label salmodetxt = curntpg.FindByName<Label>("salmodetxt");
            salmodetxt.TextColor = Color.FromHex("#212121");
        }
        public string _selsalmodetxt = "Per Month";
        public string Selsalmodetxt
        {
            get { return _selsalmodetxt; }
            set
            {
                _selsalmodetxt = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selsalmodetxt)));

            }
        }
        public string _selsalmodeval = "Monthly";
        public string Selsalmodeval
        {
            get { return _selsalmodeval; }
            set
            {
                _selsalmodeval = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selsalmodeval)));

            }
        }
        public string _selsalmodeid = "185";
        public string Selsalmodeid
        {
            get { return _selsalmodeid; }
            set
            {
                    _selsalmodeid = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selsalmodeid)));

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

        public bool _stackvisible = false;
        public bool StackVisible
        {
            get { return _stackvisible; }
            set
            {
                _stackvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StackVisible)));
            }
        }

        public bool _salmodetypevisible = false;
        public bool salmodetypevisible
        {
            get { return _salmodetypevisible; }
            set
            {
                _salmodetypevisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(salmodetypevisible)));
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
        public string _trcity { get; set; }
        public string Trainingcity
        {
            get { return _trcity; }
            set
            {

                if (_trcity != value)
                {
                    _trcity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Trainingcity)));
                    if (SelTrainingcity != _trcity)
                        ajaxcity(_trcity);
                }

            }
        }

        public string _seltrcity { get; set; }
        public string SelTrainingcity
        {
            get { return _seltrcity; }
            set
            {

                if (_seltrcity != value)
                {
                    _seltrcity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelTrainingcity)));
                }

            }
        }

        public string _selcity { get; set; }
        public string Selcity
        {
            get { return _selcity; }
            set
            {

                if (_selcity != value)
                {
                    _selcity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selcity)));
                }

            }
        }

        public string _minsal = "";
        public string Minsal
        {
            get { return _minsal; }
            set
            {
                _minsal = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Minsal)));
            }
        }
        public string _maxsal = "";
        public string Maxsal
        {
            get { return _maxsal; }
            set
            {
                _maxsal = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Maxsal)));
            }
        }

        public string _maxexp ;
        public string Maxexp
        {
            get { return _maxexp; }
            set
            {
                _maxexp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Maxexp)));
            }
        }
        public string _minexp ;
        public string Minexp
        {
            get { return _minexp; }
            set
            {
                _minexp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Minexp)));
            }
        }

        public string _adtitle = "";
        public string AdTitle
        {
            get { return _adtitle; }
            set
            {
                _adtitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AdTitle)));
            }
        }
        public string _desc = "";
        public string Desc
        {
            get { return _desc; }
            set
            {
                _desc = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Desc)));
            }
        }
        private string zipcode = "", statecode = "", state = "", city = "",latitude="",longitude="", country = "";

        public async void ajaxcity(string city)
        {
            Selcity = "";
            var nsAPI = RestService.For<ILeadformService>(Commonsettings.TechjobsAPI);
            Cityajaxlist list = await nsAPI.Getcityajax(city,"all");
            CityAjax = list.ROW_DS;
            if (CityAjax != null)
            {
                foreach (var i in list.ROW_DS)
                {
                    i.fullcity = i.city + ", " + i.statecode;
                }
                CityVisible = true;
            }
            else
                CityVisible = false;

        }
        public void Selectajaxcity(Cityajax model)
        {
            SelTrainingcity = model.city + ", " + model.statecode;
            Trainingcity = model.city + ", " + model.statecode;
            Selcity = model.citystatecodeurl;
            zipcode = model.zipcode;
            statecode = model.statecode;
            state = model.statename;
            city = model.city;
            country = model.country;
            latitude = model.latitude;
            longitude = model.longitude;
            CityVisible = false;
        }
        bool isvalid = true;
        public async void sbmtjobdetails()
        {
            var connected = CrossConnectivity.Current.IsConnected;
            try
            {
                if (connected == true)
                {
                    var baseApi = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                    string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                    var currentpage = GetCurrentPage();
                    //  formvalidation();
                    if (formvalidation())
                    {
                        dialog.ShowLoading("");
                        if (FresherID == 1)
                        {
                            data.minexp = "0";
                            data.maxexp = "0";
                        }
                        else
                        {
                            data.minexp = Minexp;
                            data.maxexp = Maxexp;
                        }

                        data.minsalary = Minsal;
                        data.maxsalary = Maxsal;
                        data.desc = Desc;
                        data.title = AdTitle;
                        data.joblocation = Trainingcity;
                        data.newcityurl = Selcity;
                        data.zipcode = zipcode;
                        data.statecode = statecode;
                        data.state = state;
                        data.country = country;
                        data.city = city;
                        data.useremail = Commonsettings.UserEmail;
                        data.userpid = Commonsettings.UserPid;
                        data.username = Commonsettings.UserName;
                        data.usermobileno = Commonsettings.UserMobileno;
                        data.lat = latitude;
                        data.longitude = longitude;
                        data.ip = ipaddress;
                        data.salmode = Selsalmodeval;
                        data.salmodeid = Selsalmodeid;
                        data.isbestinindustry = BestinIndustryID;
                        data.employmenttype = checkedemployementtypevalue;
                        postingsbmtdetails details = await baseApi.Jobsposting(data);
                        //if (details.otpsend == "otpsend")
                        if (details.adid != "" && details.adid != null)
                        {
                            data.adid = details.adid;
                            dialog.HideLoading();
                            await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Businessdetails(data));
                        }
                        else
                        {
                            dialog.HideLoading();
                        }

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
                if(connected==false)
                    dialog.Toast("Kindly check your internet connection!");
            }
        }

        public bool formvalidation()
        {
            var currentpage = GetCurrentPage();
            isvalid = true;
            if (string.IsNullOrEmpty(Selcity))
            {
                dialog.Toast("Please select city from the list");
                //Entry myEntry = currentpage.FindByName<Entry>("txtajxcity");
                //myEntry.Text = "";
                //myEntry.Placeholder = "Please select city from the list";
                //myEntry.PlaceholderColor = Color.Red;
                //myEntry.Focus();
                isvalid = false;
                return false;
            }
            if (string.IsNullOrEmpty(AdTitle) || AdTitle.Trim().Length==0)
            {
                isvalid = false;
                dialog.Toast("Enter your job title");
                return false;
               
            }
            //else if (AdTitle.Length < 10)
            //{
            //    isvalid = false;
            //    dialog.Toast("Please enter title atleast 10 characters");
            //    return false;
            //}
            if (Desc == "" || Desc.Trim().Length==0)
            {
                isvalid = false;
                dialog.Toast("Please enter the description");
                return false;
            }
            else if (Desc.Length >5000)
            {
                isvalid = false;
                dialog.Toast("Please enter a detailed description within 5000 characters.");

            }
           if(FresherID==0)
            {
                if (string.IsNullOrEmpty(Minexp) || Minexp.Trim().Length == 0)
                {
                    isvalid = false;
                    dialog.Toast("Enter the Min Exp");
                    return false;
                }

                if (!string.IsNullOrEmpty(Minexp))
                {
                    if (Minexp.Contains("."))
                    {
                        dialog.Toast("Experience cannot contain any special characters");
                        return false;
                    }
                    else if ((Convert.ToInt64(Minexp) > 40))
                    {
                        isvalid = false;
                        dialog.Toast("Enter the Min Exp between 0-40 years");
                        return false;
                    }

                }
                if (string.IsNullOrEmpty(Maxexp) || Maxexp.Trim().Length == 0)
                {
                    isvalid = false;
                    dialog.Toast("Enter the Max Exp");
                    return false;

                }
                //if(!string.IsNullOrEmpty(Maxexp)) 
                //{
                //    if((Maxexp.Length > 2))
                //    {
                //        isvalid = false;
                //        dialog.Toast("Enter the Max Exp between 0-40 years");
                //        return false;
                //    }

                //}
                //if(Minsal.Contains(".") || Minsal.Contains("."))
                //{
                //    dialog.Toast("Enter the Max Exp");
                //    return false;
                //}
                if (!string.IsNullOrEmpty(Maxexp))
                {
                    if (Maxexp.Contains("."))
                    {
                        dialog.Toast("Experience cannot contain any special characters");
                        return false;
                    }
                    else if ((Convert.ToInt64(Maxexp) > 40))
                    {
                        isvalid = false;
                        dialog.Toast("Enter the Max Exp between 0-40 years");
                        return false;
                    }

                }
                if (!string.IsNullOrEmpty(Minexp) && !string.IsNullOrEmpty(Maxexp))
                {
                    if (Maxexp.Contains(".") || Minexp.Contains("."))
                    {
                        dialog.Toast("Experience cannot contain any special characters");
                        return false;
                    }
                    else if (Convert.ToInt64(Minexp) > Convert.ToInt64(Maxexp))
                    {
                        isvalid = false;
                        dialog.Toast("To experience should grater than from experience");
                        return false;
                    }
                }
            }
            if(BestinIndustryID==0)
            {
                if (Minsal == "" || Minexp.Trim().Length==0)
                {
                    isvalid = false;
                    dialog.Toast("Enter the Min Salary");
                    return false;

                }
                if (!string.IsNullOrEmpty(Minsal))
                {
                    if (Minsal.Contains("."))
                    {
                        dialog.Toast("Salary cannot contain any special characters...");
                        return false;
                    }
                    else if ((Minsal.Length > 8))
                    {
                        isvalid = false;
                        dialog.Toast("Enter the Min Salary between 0-8 digits");
                    }
                }
                if (Maxsal == "" || Maxsal.Trim().Length==0)
                {
                    isvalid = false;
                    dialog.Toast("Enter the Max Salary");
                    return false;
                    //Entry myEntry = currentpage.FindByName<Entry>("txtmaxsal");
                    //myEntry.Text = "";
                    //myEntry.Placeholder = "Enter the Max Salary";
                    //myEntry.PlaceholderColor = Color.Red;
                    //myEntry.Focus();
                }
                if (!string.IsNullOrEmpty(Maxsal))
                {
                    if (Maxsal.Contains("."))
                    {
                        dialog.Toast("Salary cannot contain any special characters...");
                        return false;
                    }
                    else if ((Maxsal.Length > 8))
                    {
                        isvalid = false;
                        dialog.Toast("Enter the Max Salary between 0-8 digits");
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(Minsal) && !string.IsNullOrEmpty(Maxsal))
                {
                    if ((Convert.ToInt64(Minsal) > Convert.ToInt64(Maxsal)))
                    {
                        isvalid = false;
                        dialog.Toast("Max Salary should greater than Min Salary");
                        return false;
                    }

                }
            }
            if(string.IsNullOrEmpty(checkedemployementtypevalue))
            {
                dialog.Toast("Please select employement type");
                return false;
            }
            return isvalid;
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
