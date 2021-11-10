using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Refit;
using Newtonsoft.Json;
using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.DayCare.Features.List.Interface;
using NRIApp.DayCare.Features.List.Models;
using NRIApp.LocalJobs.Features.Listing.Models;
using NRIApp.LocalJobs.Features.Listing.Interfaces;
using NRIApp.DayCare.Features.Post.Interface;
using NRIApp.DayCare.Features.Post.Models;

namespace NRIApp.DayCare.Features.List.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Filter_dc : ContentPage
	{
        IUserDialogs dialog = UserDialogs.Instance;
        DClistinfo listdata = new DClistinfo();
        public List<DCfilter_DATA> allservicetypelist { get; set; }
      
        List<language> languagelists = new List<language>()
        {
            new language{languagetxt="Any",languageid="any",image="RadioButtonChecked.png"},
            new language{languagetxt="Hindi",languageid="hindi",image="RadioButtonUnChecked.png"},
            new language{languagetxt="English",languageid="english",image="RadioButtonUnChecked.png"},
            new language{languagetxt="Telugu",languageid="telugu",image="RadioButtonUnChecked.png"},
            new language{languagetxt="Punjabi",languageid="punjabi",image="RadioButtonUnChecked.png"},
            new language{languagetxt="Gujarati",languageid="gujarati",image="RadioButtonUnChecked.png"},
            new language{languagetxt="Tamil",languageid="tamil",image="RadioButtonUnChecked.png"},
            new language{languagetxt="Malayalam",languageid="malayalam",image="RadioButtonUnChecked.png"},
            new language{languagetxt="Nepali",languageid="nepali",image="RadioButtonUnChecked.png"},
            new language{languagetxt="Bengali",languageid="bengali",image="RadioButtonUnChecked.png"},
            new language{languagetxt="Urudu",languageid="urdu",image="RadioButtonUnChecked.png"},
        };
        private double _latitude;
        public double latitude
        {
            get { return _latitude; }
            set { _latitude = value; OnPropertyChanged(nameof(latitude)); }
        }
        private double _longitude;
        public double longitude
        {
            get { return _longitude; }
            set { _longitude = value; OnPropertyChanged(nameof(longitude)); }
        }
        private string _changezipcode;
        public string changezipcode
        {
            get { return _changezipcode; }
            set { _changezipcode = value; }
        }
        private List<NRIApp.Roommates.Features.Post.Models.Location> _LocationList { get; set; }
        public List<NRIApp.Roommates.Features.Post.Models.Location> LocationList
        {
            get { return _LocationList; }
            set
            {
                _LocationList = value;
            }
        }
        private string _metrourl = "";
        public string metrourl
        {
            get { return _metrourl; }
            set { _metrourl = value; OnPropertyChanged(nameof(metrourl)); }
        }
        private string _Tlocationtxt = "";
        public string Tlocationtxt
        {
            get { return _Tlocationtxt; }
            set { _Tlocationtxt = value; OnPropertyChanged(nameof(Tlocationtxt)); }
        }
        private string _cityurl = "";
        public string cityurl
        {
            get { return _cityurl; }
            set { _cityurl = value; OnPropertyChanged(nameof(cityurl)); }
        }
        private string _city = "";
        public string city
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged(nameof(city)); }
        }
        private string _Zipcode;
        public string Zipcode
        {
            get { return _Zipcode; }
            set { _Zipcode = value; }
        }
        private string _landmarkID;
        public string landmarkID
        {
            get { return _landmarkID; }
            set { _landmarkID = value; }
        }
        private string _landmark;
        public string landmark
        {
            get { return _landmark; }
            set { _landmark = value; }
        }
        private List<careservicedata> _serviceneedlstdata;
        public List<careservicedata> serviceneedlstdata
        {
            get { return _serviceneedlstdata; }
            set { _serviceneedlstdata = value; OnPropertyChanged(nameof(serviceneedlstdata)); }
        }
        private string _checkedneedservicetypevalue = "";
        public string checkedneedservicetypevalue
        {
            get { return _checkedneedservicetypevalue; }
            set { _checkedneedservicetypevalue = value; OnPropertyChanged(nameof(checkedneedservicetypevalue)); }
        }
        private string _languagevalue = "";
        public string languagevalue
        {
            get { return _languagevalue; }
            set { _languagevalue = value; OnPropertyChanged(nameof(languagevalue)); }
        }
        private string _checkedagegrouptypevalue = "";
        public string checkedagegrouptypevalue
        {
            get { return _checkedagegrouptypevalue; }
            set { _checkedagegrouptypevalue = value; OnPropertyChanged(nameof(checkedagegrouptypevalue)); }
        }
        private List<careservicedata> _agegrouplstdata;
        public List<careservicedata> agegrouplstdata
        {
            get { return _agegrouplstdata; }
            set { _agegrouplstdata = value; OnPropertyChanged(nameof(agegrouplstdata)); }
        }
        private string _gendertype = "";
        public string gendertype
        {
            get { return _gendertype; }
            set { _gendertype = value; OnPropertyChanged(nameof(gendertype)); }
        }
        private string _experienceyrs = "";
        public string experienceyrs
        {
            get { return _experienceyrs; }
            set { _experienceyrs = value; OnPropertyChanged(nameof(experienceyrs)); }
        }
        private string _worktypeID = "";
        public string worktypeID
        {
            get { return _worktypeID; }
            set { _worktypeID = value; OnPropertyChanged(nameof(worktypeID)); }
        }
        private string _checkedworktypetypevalue = "";
        public string checkedworktypetypevalue
        {
            get { return _checkedworktypetypevalue; }
            set { _checkedworktypetypevalue = value; OnPropertyChanged(nameof(checkedworktypetypevalue)); }
        }
        private int _filtercount = 0;
        public int filtercount
        {
            get { return _filtercount; }
            set { _filtercount = value; OnPropertyChanged(nameof(filtercount)); }
        }
        List<tset> experiencelist = new List<tset>()
        {
            new tset{ tsetval="Beginner",tsetvalID="0",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{ tsetval="1",tsetvalID="1",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{ tsetval="2",tsetvalID="2",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{ tsetval="3",tsetvalID="3",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{ tsetval="4",tsetvalID="4",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{ tsetval="5",tsetvalID="5",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{ tsetval="6",tsetvalID="6",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{ tsetval="7",tsetvalID="7",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{ tsetval="8",tsetvalID="8",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{ tsetval="9",tsetvalID="9",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{ tsetval="10+",tsetvalID="10",tsetvalimg="RadioButtonUnChecked.png"}
        };
        public Filter_dc (DClistinfo list)
		{
			InitializeComponent ();
            listdata = list;
            
            if(list.locationselected==true)
            {
                locationselected.IsVisible = true;
            }
            if(list.careservicetickimg==true)
            {
                careservicetickimg.IsVisible = true;
            }
            //if (list.needserviceselectedcount == true)
            //{
            //    needserviceselectedcount.IsVisible = true;
            //}
            if (list.languagetickimg == true)
            {
                languagetickimg.IsVisible = true;
            }
            //if (list.agegroupcount == true)
            //{
            //    agegroupcount.IsVisible = true;
            //}
            if (list.gendertickimg == true)
            {
                gendertickimg.IsVisible = true;
            }
            if (list.worktypecount == true)
            {
                worktypecount.IsVisible = true;
            }
            if (list.radioworktypetickimg == true)
            {
                radioworktypetickimg.IsVisible = true;
            }
            if (list.calentickimg == true)
            {
                calentickimg.IsVisible = true;
            }
            if (list.experiencetickimg == true)
            {
                experiencetickimg.IsVisible = true;
            }
            

            changezipcode = list.cityurl;
            //new
            city = list.city;
            txtlocation.Text= Tlocationtxt = list.city;
            //location radio btn
            if(list.nearby== "metro")
            {
                metroimg.Source = "RadioButtonChecked.png";
                cityimg.Source = "RadioButtonUnChecked.png";
                landmarkimg.Source = "RadioButtonUnChecked.png";
                nearbyID = "metro";
            }
            else if(list.nearby=="city")
            {
                metroimg.Source = "RadioButtonUnChecked.png";
                cityimg.Source = "RadioButtonChecked.png";
                landmarkimg.Source = "RadioButtonUnChecked.png";
                nearbyID = "city";
            }
            else
            {
                txtlocation.Text = Tlocationtxt = list.landmark; 
                metroimg.Source = "RadioButtonUnChecked.png";
                cityimg.Source = "RadioButtonUnChecked.png";
                landmarkimg.Source = "RadioButtonChecked.png";
                nearbyID = "landmark";
            }
            //if (!string.IsNullOrEmpty(list.landmark))
            //{
            //    Tlocationtxt = list.landmark;
            //    txtlocation.Text = list.landmark;
            //}
            if (list.worktype=="19" || list.worktype == "19,42")
            {
                liveoutImg.Source = "RadioButtonUnChecked.png";
                liveinImg.Source = "RadioButtonChecked.png";
           
               
                    worktypeID = "19,42";
               
            }
            else if(list.worktype=="20"|| list.worktype == "20,42")
            {
                liveoutImg.Source = "RadioButtonChecked.png";
                liveinImg.Source = "RadioButtonUnChecked.png";
                // LiloImg.Source = "RadioButtonUnChecked.png";
                //radioworktypetickimg.IsVisible = true;
               
                    worktypeID = "20,42";
            }
            else
            {
                liveoutImg.Source = "RadioButtonUnChecked.png";
                liveinImg.Source = "RadioButtonUnChecked.png";
               // LiloImg.Source = "RadioButtonChecked.png";
                worktypeID = "";
            }
            contentlayout.IsVisible = false;
            locationlist.IsVisible = false;
            landmarklist.IsVisible = false;
            listdata = list;
            //listdata.worktype=listdata.ter
            getfilters(list);
            
		}
        public async void getfilters(DClistinfo list)
        {
            await Task.Delay(500); //New York, NY
            if ((list.city != Commonsettings.Usercity+", "+Commonsettings.Userstatecode)&& (list.city!= Commonsettings.Usercity))
            {
                locationselected.IsVisible = true;
            }
            else
            {
                locationselected.IsVisible = false;
            }
            citylayout.IsVisible = false;
            needserviceboxlayout.IsVisible = false;
            languageboxlayout.IsVisible = false;
            experiencelistlayout.IsVisible = false;
            calendarlayout.IsVisible = false;
            genderlayout.IsVisible = false;
            agegrouplayout.IsVisible = false;
            chkbxworktypelistlayout.IsVisible = false;
            radioworktypelistlayout.IsVisible = false;
            carserviceradiolayout.IsVisible = false;
            getlanguage(list.language);
            getallservices(list.careprovidertype);

            if (list.careprovidertype == "nanny-babysitter" || list.careprovidertype == "care-center" || list.careprovidertype == "family-child-care" || list.careprovidertype == "cook-jobs" || list.careprovidertype == "housekeeper-jobs" || list.careprovidertype == "elder-care-center-jobs" ||list.careprovidertype== "elder-care-center")
            {
                checkedneedservicetypevalue = list.needservice;
                getneedservice(list.needservice);
            }
           
            if(list.careprovidertype == "nanny-babysitter" || list.careprovidertype == "family-child-care" || list.careprovidertype == "care-center")
            {
                agegroupmainlayout.IsVisible = true;
                checkedagegrouptypevalue = list.agegroupid;
                getagegroupdata(list.agegroupid);
            }
            if(list.careprovidertype == "nanny-babysitter" || list.careprovidertype == "elder-care-nurse-services" || list.careprovidertype == "part-time-housekeeping" || list.careprovidertype == "part-time-cook")
            {
                gendermainlayout.IsVisible = true;
                getgendertype(list.gendertype);
            }
            if(list.careprovidertype== "elder-care-nurse-services" ||list.careprovidertype== "part-time-housekeeping" || list.careprovidertype == "part-time-cook" || list.careprovidertype== "nanny-babysitter-jobs" || list.careprovidertype== "nanny-babysitter")
            {
                experiencemainlayout.IsVisible = true;
                getexperiencelist(list.experience);
            }
            
            if (list.careprovidertype== "nanny-babysitter" || list.careprovidertype == "elder-care-nurse-services"  || list.careprovidertype == "elder-care-center-jobs")
            {
                radioworktypelayout.IsVisible = true;
                getradioworktypelists(list.worktype);
            }
            else if (list.careprovidertype == "elder-care-center" || list.careprovidertype == "nanny-babysitter-jobs"||list.careprovidertype== "care-center")
            {
                checkboxworktypelayout.IsVisible = true;
                if(list.worktype=="19"||list.worktype=="20"||list.worktype=="19,42"||list.worktype=="20,42")
                {
                    list.worktype = "";
                    listdata.worktype = "";
                }
                checkedworktypetypevalue = list.worktype;
                getcheckboxworktypelists(list.worktype);
            }
            else
            {
                radioworktypelayout.IsVisible = false;
                checkboxworktypelayout.IsVisible = false;
            }


            if (list.careprovidertype == "care-services" || list.careprovidertype== "child-care" || list.careprovidertype== "elder-care" ||list.careprovidertype== "pet-care")
            {
                landmarklayout.IsVisible = false;
                languagemainlayout.IsVisible = false;
                checkboxworktypelayout.IsVisible = false;
                radioworktypelayout.IsVisible = false;
                gendermainlayout.IsVisible = false;
            }
           else if(list.careprovidertype == "care-center")
            {
                gendermainlayout.IsVisible = false;
            }
            else if(list.careprovidertype== "family-child-care")
            {
                checkboxworktypelayout.IsVisible = false;
                radioworktypelayout.IsVisible = false;
                gendermainlayout.IsVisible = false;
            }
            else if(list.careprovidertype== "elder-care")
            {
                languagemainlayout.IsVisible = false;
                checkboxworktypelayout.IsVisible = false;
                radioworktypelayout.IsVisible = false;
                landmarklayout.IsVisible = false;
            }
            else if(list.careprovidertype== "housekeeper" || list.careprovidertype == "cook")
            {
                languagemainlayout.IsVisible = false;
                checkboxworktypelayout.IsVisible = false;
                radioworktypelayout.IsVisible = false;
                gendermainlayout.IsVisible = false;
            }
            else if(list.careprovidertype== "pet-care-provider")
            {
                checkboxworktypelayout.IsVisible = false;
                radioworktypelayout.IsVisible = false;
                gendermainlayout.IsVisible = false;
            }
            else if(list.careprovidertype == "pet-care-center")
            {
                checkboxworktypelayout.IsVisible = false;
                radioworktypelayout.IsVisible = false;
                gendermainlayout.IsVisible = false;
            }
            else if (list.careprovidertype == "care-jobs" || list.careprovidertype== "child-care-jobs" || list.careprovidertype == "child-care-jobs"||list.careprovidertype== "cook-jobs")
            {
                checkboxworktypelayout.IsVisible = false;
                radioworktypelayout.IsVisible = false;
                gendermainlayout.IsVisible = false;
            }
            else if (list.careprovidertype == "elder-care-jobs")
            {
                checkboxworktypelayout.IsVisible = false;
                radioworktypelayout.IsVisible = false;
                gendermainlayout.IsVisible = false;
            }
            else if(list.careprovidertype== "elder-care-center-jobs")
            {
                gendermainlayout.IsVisible = false;
            }
            else if(list.careprovidertype== "pet-care-jobs")
            {
                languagemainlayout.IsVisible = false;
                checkboxworktypelayout.IsVisible = false;
                radioworktypelayout.IsVisible = false;
                gendermainlayout.IsVisible = false;
            }
        }
        public List<worktype_data> radioworktypelist = new List<worktype_data>();
        public  void getradioworktypelists(string worktype)
        {
            try
            {
                radioworktypelayout.IsVisible = true;
                checkboxworktypelayout.IsVisible = false;

                //var dcapi = RestService.For<IDClistings>(Commonsettings.DaycareAPI);
                //worktype worktypedata = await dcapi.getworktype(listdata);
                //if(worktypedata.ROW_DATA.Count>0)
                //{
                //    radioworktypelayout.IsVisible = true;
                //    checkboxworktypelayout.IsVisible = false;
                //}
                //foreach(var dt in worktypedata.ROW_DATA)
                //{
                //    if (worktype == dt.categoryname)
                //    {
                //        dt.checkimage = "RadioButtonChecked.png";
                //        worktypeID = dt.categoryname;
                //    }
                //    else
                //    {
                //        dt.checkimage = "RadioButtonUnChecked.png";
                //    }
                //    if (dt.tertiarycategoryid!="42")
                //    {
                //        radioworktypelist.Add(dt);
                //    }
                //}
                //radioworktlist.ItemsSource = radioworktypelist;
            }
            catch (Exception ex)
            {

            }
        }
        private void LiveOutCommand(object sender, EventArgs e)
        {
            liveoutImg.Source = "RadioButtonChecked.png";
            liveinImg.Source = "RadioButtonUnChecked.png";
            //LiloImg.Source = "RadioButtonUnChecked.png";
            if (worktypeID!="20,42")
            {
                radioworktypetickimg.IsVisible = true;
            }
            worktypeID = "20,42";
        }

        private void LiveInCommand(object sender, EventArgs e)
        {
            liveoutImg.Source = "RadioButtonUnChecked.png";
            liveinImg.Source = "RadioButtonChecked.png";
            //LiloImg.Source = "RadioButtonUnChecked.png";
            if (worktypeID!="19,42")
            {
                radioworktypetickimg.IsVisible = true;
            }
            worktypeID = "19,42";
        }
     
        //private void radioworktype_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    var radiogrp = sender as ListView;
        //    worktype_data lsow = new worktype_data();
        //    lsow = (worktype_data)radiogrp.SelectedItem;
        //    foreach (var lst in radioworktypelist)
        //    {
        //        if (lsow.categoryname == lst.categoryname)
        //        {
        //            lst.checkimage = "RadioButtonChecked.png";
        //            worktypeID = lst.categoryname;
        //        }
        //        else
        //        {
        //            lst.checkimage = "RadioButtonUnChecked.png";
        //        }
        //    }
        //    radioworktlist.ItemsSource = null;
        //    radioworktlist.ItemsSource = radioworktypelist;
        //}
        public async void getcheckboxworktypelists(string worktype)
        {
            try
            {
                await Task.Delay(500);
                if (!string.IsNullOrEmpty(worktype))
                {
                    worktypecountlayout.IsVisible = true;
                    string[] worktypecountarr = worktype.Split(',');
                    worktypecount.Text = worktypecountarr.Count().ToString();
                }
                else
                {
                    worktypecountlayout.IsVisible = false;
                    worktypecount.Text = "0";
                }
                var dcapi = RestService.For<IDClistings>(Commonsettings.DaycareAPI);
                worktype worktypedata = await dcapi.getworktype(listdata);
                if (worktypedata.ROW_DATA.Count > 0)
                {
                    checkboxworktypelayout.IsVisible = true;
                    radioworktypelayout.IsVisible = false;
                }
                foreach (var data in worktypedata.ROW_DATA)
                {
                    BoxView boxx = new BoxView();
                    boxx.BackgroundColor = Color.FromHex("e0e0e0");
                    boxx.HeightRequest = 1;
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    chbx.Text = data.categoryname;
                    //chbx.ClassId = data.primarytagid.ToString();
                    chbx.ClassId = data.tertiarycategoryid;
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_worktypeCheckChanged;
                    chbx.Padding = 5;
                    if (!string.IsNullOrEmpty(worktype))
                    {
                        string[] worktypelst = worktype.Split(',');
                        foreach (var i in worktypelst)
                        {
                            if (i==data.tertiarycategoryid)
                            {
                                chbx.IsChecked = true;
                            }
                        }
                    }
                    worktypelayout.Children.Add(chbx);
                    worktypelayout.Children.Add(boxx);
                }
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private void Chb_worktypeCheckChanged(object sender, EventArgs e)
        {
            try
            {
                var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
                if (category.IsChecked==true)
                {
                    if (checkedworktypetypevalue != null && checkedworktypetypevalue != "")
                    {
                        if (!checkedworktypetypevalue.Contains(category.ClassId))
                        {
                            checkedworktypetypevalue += "," + category.ClassId;
                        }
                    }
                    else
                    {
                        checkedworktypetypevalue += category.ClassId;
                    }
                }
                else
                {
                    string[] indexvalue = checkedworktypetypevalue.Split(',');
                    int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                    indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                    checkedworktypetypevalue = string.Join(",", indexvalue);
                }

                if (checkedworktypetypevalue.Trim().Length != 0)
                {
                    worktypecountlayout.IsVisible = true;
                    string[] employementcountarr = checkedworktypetypevalue.Split(',');
                    worktypecount.Text = employementcountarr.Count().ToString();
                }
                else
                {
                    worktypecountlayout.IsVisible = false;
                    worktypecount.Text = "0";
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

            }
        }
        public void getexperiencelist(string experience)
        {
            try
            {
                if(string.IsNullOrEmpty(experience))
                {
                    experience = "0";
                    experiencetxt.Text = "Select";
                }
                if(experience!="0")
                {
                    experiencetickimg.IsVisible = true;
                }
                foreach(var yrs in experiencelist)
                {
                    if(experience==yrs.tsetval)
                    {
                        yrs.tsetvalimg = "RadioButtonChecked.png";
                        experiencetxt.Text=yrs.tsetval;
                        experienceyrs = yrs.tsetvalID;
                    }
                    else
                    {
                        yrs.tsetvalimg = "RadioButtonUnChecked.png";
                    }
                }
                experiencelists.ItemsSource = null;
                experiencelists.ItemsSource = experiencelist;
            }
            catch(Exception ex)
            {

            }
        }
        private void experience_tap(object sender, ItemTappedEventArgs e)
        {
            var radiogrp = sender as ListView;
            tset lsow = new tset();
            lsow = (tset)radiogrp.SelectedItem;
            foreach (var lst in experiencelist)
            {
                if (lsow.tsetval == lst.tsetval)
                {
                    lst.tsetvalimg = "RadioButtonChecked.png";
                    experienceyrs = lst.tsetvalID;
                    experiencetxt.Text  = lst.tsetval;
                }
                else
                {
                    lst.tsetvalimg = "RadioButtonUnChecked.png";
                }
            }
            if(experienceyrs!="0")
            {
                experiencetickimg.IsVisible = true;
            }
            allservicelist.ItemsSource = null;
            allservicelist.ItemsSource = allservicetypelist;
            contentlayout.IsVisible = false;
        }
        public void getgendertype(string gendertype)
        {
           if(gendertype=="male")
            {
                AnyImg.Source = "RadioButtonUnChecked.png";
                MaleImg.Source = "RadioButtonChecked.png";
                FeMaleImg.Source = "RadioButtonUnChecked.png";
                gendertype = "male";
            }
           else if(gendertype == "female")
            {
                AnyImg.Source = "RadioButtonUnChecked.png";
                MaleImg.Source = "RadioButtonUnChecked.png";
                FeMaleImg.Source = "RadioButtonChecked.png";
                gendertype = "female";
            }
           else
            {
                AnyImg.Source = "RadioButtonChecked.png";
                MaleImg.Source = "RadioButtonUnChecked.png";
                FeMaleImg.Source = "RadioButtonUnChecked.png";
                gendertype = "any";
            }

            if (gendertype != "any")
            {
                gendertickimg.IsVisible = true;
            }
        }
        public async void getagegroupdata(string agegroup)
        {
            try
            {
                await Task.Delay(500);
                if (!string.IsNullOrEmpty(agegroup))
                {
                    agegroupcountlayout.IsVisible = true;
                    string[] agegroupcountarr = agegroup.Split(',');
                    agegroupcount.Text = agegroupcountarr.Count().ToString();
                }
                else
                {
                    agegroupcountlayout.IsVisible = false;
                    agegroupcount.Text = "0";
                }
                var dcapi = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                careservice agegroupdata = await dcapi.getagegroup();
                agegrouplstdata = agegroupdata.ROW_DATA;
                foreach (var data in agegrouplstdata)
                {
                    BoxView boxx = new BoxView();
                    boxx.BackgroundColor = Color.FromHex("e0e0e0");
                    boxx.HeightRequest = 1;
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    chbx.Text = data.primarytag;
                    chbx.ClassId = data.primarytagid.ToString();
                    //chbx.ClassId = data.primarytagid.ToString() + "::" + data.primarytag + "::" + data.primarytagurl;
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_agegrouptypeCheckChanged;
                    chbx.Padding = 5;
                    if (!string.IsNullOrEmpty(agegroup))
                    {
                        string[] agetypelst = agegroup.Split(',');
                        foreach (var i in agetypelst)
                        {
                            if (data.primarytagid==i)
                            {
                                chbx.IsChecked = true;
                            }
                        }
                    }
                    agegrouplayout.Children.Add(chbx);
                    agegrouplayout.Children.Add(boxx);
                }

            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private void Chb_agegrouptypeCheckChanged(object sender, EventArgs e)
        {
            try
            {
                var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
                if (category.IsChecked==true)
                {
                    if (checkedagegrouptypevalue != null && checkedagegrouptypevalue != "")
                    {
                        if (!checkedagegrouptypevalue.Contains(category.ClassId))
                        {
                            checkedagegrouptypevalue += "," + category.ClassId;
                        }
                    }
                    else
                    {
                        checkedagegrouptypevalue += category.ClassId;
                    }
                }
                else
                {
                    string[] indexvalue = checkedagegrouptypevalue.Split(',');
                    int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                    indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                    checkedagegrouptypevalue = string.Join(",", indexvalue);
                }
                if (checkedagegrouptypevalue.Trim().Length != 0)
                {
                    agegroupcountlayout.IsVisible = true;
                    string[] agegroupcountarr = checkedagegrouptypevalue.Split(',');
                    agegroupcount.Text = agegroupcountarr.Count().ToString();
                }
                else
                {
                    agegroupcountlayout.IsVisible = false;
                    agegroupcount.Text = "0";
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

            }
        }
        public void getlanguage(string language)
        {
            try
            {
               if(string.IsNullOrEmpty(language)|| language=="any")
                {
                    listdata.language = "any";
                    languagevalue = "any";
                    language = "any";
                    languagetickimg.IsVisible = false;
                }
               
                foreach (var data in languagelists)
                {
                    if (language == data.languageid)
                    {
                        data.image = "RadioButtonChecked.png";
                        languagevalue = data.languageid;
                    }
                    else
                    {
                        data.image = "RadioButtonUnChecked.png";
                    }
                }
                if (languagevalue != "any")
                {
                    languagetickimg.IsVisible = true;
                }

                languagelistdata.ItemsSource = languagelists;
            }
            catch (Exception ex)
            {

            }
        }
        private void languagedata_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var radiogrp = sender as ListView;
                language lsow = new language();
                lsow = (language)radiogrp.SelectedItem;

                foreach (var item in languagelists)
                {
                    if (item.languageid == lsow.languageid)
                    {
                        item.image = "RadioButtonChecked.png";
                        languagevalue = item.languageid;
                        listdata.language = item.languageid;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }
                }
                if (languagevalue != "any")
                {
                    languagetickimg.IsVisible = true;
                }
                languagelistdata.ItemsSource = null;
                languagelistdata.ItemsSource = languagelists;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public async void getneedservice(string needservice)
        {
            try
            {
                if (!string.IsNullOrEmpty(needservice))
                {
                    needserviceselected.IsVisible = true;
                    string[] servicecountarr = needservice.Split(',');
                    needserviceselectedcount.Text = servicecountarr.Count().ToString();
                }
                else
                {
                    needserviceselected.IsVisible = false;
                    needserviceselectedcount.Text = "0";
                }
                if (!string.IsNullOrEmpty(listdata.careprovidertype))
                {
                    var dcapi = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                    careservice needservicedata = await dcapi.getneedservicelist(listdata.careprovidertype);
                    serviceneedlstdata = needservicedata.ROW_DATA;
                    needservicelayout.Children.Clear();
                    if (serviceneedlstdata != null && serviceneedlstdata.Count > 0)
                    {
                        needservicelistlayout.IsVisible = true;
                        foreach (var data in serviceneedlstdata)
                        {
                            BoxView boxx = new BoxView();
                            boxx.BackgroundColor = Color.FromHex("e0e0e0");
                            boxx.HeightRequest = 1;
                            Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                            //chbx.OnImg = "CheckBoxChecked.png";
                            //chbx.OffImg = "CheckBoxUnChecked.png";
                            chbx.Text = data.primarytag;
                            //18::Activities::activities
                            //chbx.ClassId = data.primarytagid.ToString() + "::" + data.primarytag + "::" + data.primarytagurl;
                            chbx.ClassId = data.primarytagid;
                            //chbx.ShowLabel = true;
                            chbx.CheckChanged += chb_needservicetypechanges;
                            chbx.Padding = 5;
                            if (!string.IsNullOrEmpty(needservice))
                            {
                                string[] servicetypelst = needservice.Split(',');
                                foreach (var i in servicetypelst)
                                {
                                    if (data.primarytagid == i)
                                    {
                                        chbx.IsChecked = true;
                                    }
                                }
                            }
                            needservicelayout.Children.Add(chbx);
                            needservicelayout.Children.Add(boxx);
                        }

                    }
                    //  dialog.HideLoading();
                }

            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private void chb_needservicetypechanges(object sender, EventArgs e)
        {
            try
            {
                var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
                if (category.IsChecked==true)
                {
                    if (checkedneedservicetypevalue != null && checkedneedservicetypevalue != "")
                    {
                        if (!checkedneedservicetypevalue.Contains(category.ClassId))
                        {
                            checkedneedservicetypevalue += "," + category.ClassId;
                        }
                    }
                    else
                    {
                        checkedneedservicetypevalue += category.ClassId;
                    }
                }
                else
                {
                    string[] indexvalue = checkedneedservicetypevalue.Split(',');
                    int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                    indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                    checkedneedservicetypevalue = string.Join(",", indexvalue);
                }

                if (checkedneedservicetypevalue.Trim().Length != 0)
                {
                    needserviceselected.IsVisible = true;
                    string[] employementcountarr = checkedneedservicetypevalue.Split(',');
                    needserviceselectedcount.Text = employementcountarr.Count().ToString();
                }
                else
                {
                    needserviceselected.IsVisible = false;
                    needserviceselectedcount.Text = "0";
                }
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private string _careprovidertypeurl = "";
        public string careprovidertypeurl
        {
            get { return _careprovidertypeurl; }
            set { _careprovidertypeurl = value;OnPropertyChanged(nameof(careprovidertypeurl)); }
        }
        private string _caretypeurl = "";
        public string caretypeurl
        {
            get { return _caretypeurl; }
            set { _caretypeurl = value; OnPropertyChanged(nameof(caretypeurl)); }
        }
        public async void getallservices(string careprovidertype)
        {
            try
            {
                dialog.ShowLoading("",null);
                if(careprovidertype== "full-time-housekeeping" ||careprovidertype== "housekeeper-cook" || careprovidertype == "any-time-housekeeping" || careprovidertype== "full-time-cook" || careprovidertype== "any-time-cook" ||string.IsNullOrEmpty(careprovidertype))
                {
                    careprovidertypetxt.Text = "Select";
                }
                careprovidertypeurl = careprovidertype;
                var dcpackageapi = RestService.For<IDClistings>(Commonsettings.DaycareAPI);
                DCfilter list = await dcpackageapi.getallservices();
                foreach(var caretype in list.RowData)
                {
                    if(caretype.Newcategoryurl==careprovidertype)
                    {
                        caretype.checkimage = "RadioButtonChecked.png";
                        careprovidertypetxt.Text = caretype.Newcategory;
                        careprovidertypeurl = caretype.Newcategoryurl;
                        caretypeurl = caretype.Primaryurl;
                        listdata.careprovidertype = caretype.Newcategoryurl;
                        listdata.newcategoryurl = caretype.Newcategoryurl;
                        if (!string.IsNullOrEmpty(caretype.Primaryurl))
                        {
                            listdata.caretype = caretype.Primaryurl;
                        }
                    }
                    else
                    {
                        caretype.checkimage = "RadioButtonUnChecked.png";
                    }
                }
                allservicelist.ItemsSource = list.RowData;
                allservicetypelist = list.RowData;
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        

        private async void Reset_Tapped(object sender, EventArgs e)
        {
            try
            {
                var alert = await DisplayAlert("Alert!", "Are you sure?", "Reset", "Back");
                if (alert)
                {
                    //location
                    listdata.userlat = Commonsettings.UserLat.ToString();
                    listdata.userlong = Commonsettings.UserLong.ToString();
                    listdata.cityurl = Commonsettings.Usercityurl;
                    listdata.city = Commonsettings.Usercity;
                    Tlocationtxt = listdata.city + ", " + Commonsettings.Userstatecode;
                    txtlocation.Text = listdata.city + ", " + Commonsettings.Userstatecode;
                    cityimg.Source = "RadioButtonChecked.png";
                    metroimg.Source = "RadioButtonUnChecked.png";
                    landmarkimg.Source = "RadioButtonUnChecked.png";
                    nearbyID = "city";
                    listdata.nearby = nearbyID;

                    //selectcareservices
                    careprovidertypeurl =listdata.careprovidertype;
                    // listdata.careprovidertype = careprovidertypeurl;//allservice
                    
                    //services
                    checkedneedservicetypevalue = "";
                    listdata.needservice = "";

                    //language
                    listdata.language = "";

                    //worktype
                    //worktypeID -- radio       //radioworktypelistlayout,checkboxworktypelayout
                    //checkedworktypetypevalue -- checkbox
                    checkedworktypetypevalue = "";
                    worktypeID = "";
                    listdata.worktype = "";
                    liveoutImg.Source = "RadioButtonUnChecked.png";
                    liveinImg.Source = "RadioButtonUnChecked.png";

                    //childagegroup
                    checkedagegrouptypevalue = "";
                    listdata.agegroupid = "";

                    //preferred gender
                    listdata.gendertype = "";

                    //available from
                    listdata.date = "";


                    //yoexperience
                    listdata.experience = "";
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    foreach (var item in worktypelayout.Children)
                    {
                        if (item.GetType() == chbx.GetType())
                        {
                            var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                            if (cat.IsChecked == true)
                            {
                                cat.IsChecked = false;
                            }
                        }
                    }
                    foreach (var item in agegrouplayout.Children)
                    {
                        if (item.GetType() == chbx.GetType())
                        {
                            var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                            if (cat.IsChecked == true)
                            {
                                cat.IsChecked = false;
                            }
                        }
                    }
                    foreach (var item in needservicelayout.Children)
                    {
                        if (item.GetType() == chbx.GetType())
                        {
                            var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                            if (cat.IsChecked == true)
                            {
                                cat.IsChecked = false;
                            }
                        }
                    }
                   
                      foreach (var item1 in allservicetypelist)
                      {
                            item1.checkimage = "RadioButtonUnChecked.png";
                        allservicelist.ItemsSource = null;
                        allservicelist.ItemsSource = allservicetypelist;
                      }

                    foreach (var item1 in experiencelist)
                    {
                        item1.tsetvalimg = "RadioButtonUnChecked.png";
                        experiencelists.ItemsSource = null;
                        experiencelists.ItemsSource = experiencelist;
                    }
                    if (!string.IsNullOrEmpty(cityurl))
                    {
                        listdata.cityurl = cityurl;
                    }
                    listdata.careprovidertype = "care-services";
                    
                    listdata.caretype = "";
                    listdata.filtercount = 0;
                    await Navigation.PopModalAsync();
                    await Navigation.PushAsync(new DaycareListing(listdata));
                    dialog.HideLoading();

                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        public string nearbyID = "";
        private void metro_Tap(object sender, EventArgs e)
        {
            txtlocation.Text = "";
            Tlocationtxt = "";
            txtlocation.Focus();
            metroimg.Source = "RadioButtonChecked.png";
            cityimg.Source = "RadioButtonUnChecked.png";
            landmarkimg.Source = "RadioButtonUnChecked.png";
            nearbyID = "metro";
           // listdata.nearby = nearbyID;
            locationlist.IsVisible = false;
            landmarklist.IsVisible = false;
            
        }

        private void landmark_Tap(object sender, EventArgs e)
        {
            txtlocation.Text = "";
            Tlocationtxt = "";
            txtlocation.Focus();
            landmarkimg.Source = "RadioButtonChecked.png";
            metroimg.Source = "RadioButtonUnChecked.png";
            cityimg.Source = "RadioButtonUnChecked.png";
            nearbyID = "landmark";
            //listdata.nearby = nearbyID;
            locationlist.IsVisible = false;
            landmarklist.IsVisible = false;
        }

        private void city_Tap(object sender, EventArgs e)
        {
            txtlocation.Text = "";
            Tlocationtxt = "";
            txtlocation.Focus();
            cityimg.Source = "RadioButtonChecked.png";
            metroimg.Source = "RadioButtonUnChecked.png";
            landmarkimg.Source = "RadioButtonUnChecked.png";
            nearbyID = "city";
            //listdata.nearby = nearbyID;
            locationlist.IsVisible = false;
            landmarklist.IsVisible = false;
        }

        private void Allservicelist_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var radiogrp = sender as ListView;
            DCfilter_DATA lsow = new DCfilter_DATA();
            lsow = (DCfilter_DATA)radiogrp.SelectedItem;
            if (listdata.careprovidertype != lsow.Newcategoryurl)
            {
                careservicetickimg.IsVisible = true;
            }
            else
            {
                careservicetickimg.IsVisible = false;
            }
            foreach (var lst in allservicetypelist)
            {
                if (lsow.Newcategoryurl == lst.Newcategoryurl)
                {
                    lst.checkimage = "RadioButtonChecked.png";
                    careprovidertypetxt.Text = lst.Newcategory; 
                    careprovidertypeurl = lst.Newcategoryurl;
                    listdata.careprovidertype = lst.Newcategoryurl;
                    listdata.newcategoryurl = lst.Newcategoryurl;
                    if(!string.IsNullOrEmpty(lst.Primaryurl))
                    {
                        listdata.caretype = lst.Primaryurl;
                    }
                }
                else
                {
                    lst.checkimage = "RadioButtonUnChecked.png";
                }
            }
            allservicelist.ItemsSource = null;
            allservicelist.ItemsSource = allservicetypelist;
            contentlayout.IsVisible = false;
            getalldetails(careprovidertypeurl);
        }
        public void getalldetails(string careprovidertypeurl)
        {
            try
            {
                dialog.ShowLoading("", null);
                if (careprovidertypeurl == "nanny-babysitter" || careprovidertypeurl == "care-center" || careprovidertypeurl == "family-child-care" || careprovidertypeurl == "cook-jobs" || careprovidertypeurl == "housekeeper-jobs" || careprovidertypeurl == "elder-care-center-jobs" || careprovidertypeurl == "elder-care-center")
                {
                    getneedservice(listdata.needservice);
                }

                if (careprovidertypeurl == "nanny-babysitter" || careprovidertypeurl == "family-child-care" || careprovidertypeurl == "care-center")
                {
                    agegroupmainlayout.IsVisible = true;
                    getagegroupdata(listdata.agegroupid);
                }
                else
                {
                    agegroupmainlayout.IsVisible = false;
                }
                if (careprovidertypeurl == "nanny-babysitter" || careprovidertypeurl == "elder-care-nurse-services" || careprovidertypeurl == "part-time-housekeeping" || careprovidertypeurl == "part-time-cook")
                {
                    gendermainlayout.IsVisible = true;
                    getgendertype(listdata.gendertype);
                }
                else
                {
                    gendermainlayout.IsVisible = false;
                }
                if (careprovidertypeurl == "elder-care-nurse-services" || careprovidertypeurl == "part-time-housekeeping" || careprovidertypeurl == "part-time-cook" || careprovidertypeurl == "nanny-babysitter-jobs" || careprovidertypeurl == "nanny-babysitter")
                {
                    experiencemainlayout.IsVisible = true;
                    getexperiencelist(listdata.experience);
                }
                else
                {
                    experiencemainlayout.IsVisible = false;
                }
                if (careprovidertypeurl == "nanny-babysitter" || careprovidertypeurl == "elder-care-nurse-services" || careprovidertypeurl == "elder-care-center-jobs")
                {
                    radioworktypelayout.IsVisible = true;
                    getradioworktypelists(listdata.worktype);
                }
                else if (careprovidertypeurl == "elder-care-center" || careprovidertypeurl == "nanny-babysitter-jobs" || careprovidertypeurl == "care-center")
                {
                    checkboxworktypelayout.IsVisible = true;
                    if (listdata.worktype == "19" || listdata.worktype == "20" || listdata.worktype == "19,42" || listdata.worktype == "20,42")
                    {
                        listdata.worktype = "";
                        listdata.worktype = "";
                    }
                    getcheckboxworktypelists(listdata.worktype);
                }
                else
                {
                    radioworktypelayout.IsVisible = false;
                    checkboxworktypelayout.IsVisible = false;
                }


                if (careprovidertypeurl == "care-services" || careprovidertypeurl == "child-care" || careprovidertypeurl == "elder-care" || careprovidertypeurl == "pet-care")
                {
                    landmarklayout.IsVisible = false;
                    languagemainlayout.IsVisible = false;
                    checkboxworktypelayout.IsVisible = false;
                    radioworktypelayout.IsVisible = false;
                    gendermainlayout.IsVisible = false;
                }
                else if (careprovidertypeurl == "care-center")
                {
                    gendermainlayout.IsVisible = false;
                }
                else if (careprovidertypeurl == "family-child-care")
                {
                    checkboxworktypelayout.IsVisible = false;
                    radioworktypelayout.IsVisible = false;
                    gendermainlayout.IsVisible = false;
                }
                else if (careprovidertypeurl == "elder-care")
                {
                    languagemainlayout.IsVisible = false;
                    checkboxworktypelayout.IsVisible = false;
                    radioworktypelayout.IsVisible = false;
                    landmarklayout.IsVisible = false;
                }
                else if (careprovidertypeurl == "housekeeper" || careprovidertypeurl == "cook")
                {
                    languagemainlayout.IsVisible = false;
                    checkboxworktypelayout.IsVisible = false;
                    radioworktypelayout.IsVisible = false;
                    gendermainlayout.IsVisible = false;
                }
                else if (careprovidertypeurl == "pet-care-provider")
                {
                    checkboxworktypelayout.IsVisible = false;
                    radioworktypelayout.IsVisible = false;
                    gendermainlayout.IsVisible = false;
                }
                else if (careprovidertypeurl == "pet-care-center")
                {
                    checkboxworktypelayout.IsVisible = false;
                    radioworktypelayout.IsVisible = false;
                    gendermainlayout.IsVisible = false;
                }
                else if (careprovidertypeurl == "care-jobs" || careprovidertypeurl == "child-care-jobs" || careprovidertypeurl == "child-care-jobs" || careprovidertypeurl == "cook-jobs")
                {
                    checkboxworktypelayout.IsVisible = false;
                    radioworktypelayout.IsVisible = false;
                    gendermainlayout.IsVisible = false;
                }
                else if (careprovidertypeurl == "elder-care-jobs")
                {
                    checkboxworktypelayout.IsVisible = false;
                    radioworktypelayout.IsVisible = false;
                    gendermainlayout.IsVisible = false;
                }
                else if (careprovidertypeurl == "elder-care-center-jobs")
                {
                    gendermainlayout.IsVisible = false;
                }
                else if (careprovidertypeurl == "pet-care-jobs")
                {
                    languagemainlayout.IsVisible = false;
                    checkboxworktypelayout.IsVisible = false;
                    radioworktypelayout.IsVisible = false;
                    gendermainlayout.IsVisible = false;
                }

                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private void chbxPopupContentTap(object sender, EventArgs e)
        {
            contentlayout.IsVisible = true;
        }

        private void chbxContentViewTap(object sender, EventArgs e)
        {
            contentlayout.IsVisible = false;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void showcaretypes_Tapped(object sender, EventArgs e)
        {
            contentlayout.IsVisible = true;
            allservicevisible.IsVisible = true;
            experiencelistviewvisble.IsVisible = false;
        }

        private void Txtlocation_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            if (txtlocation.Text != Tlocationtxt)
            {
                if (nearbyID == "landmark")
                {
                    GetLandmark();
                    locationlist.IsVisible = false;
                }
                else
                {
                    GetLocationname();
                    landmarklist.IsVisible = false;
                }
            }
        }
        public async void GetLandmark()
        {
            try
            {
                cityurl = "";
                changezipcode = "";
                listdata.landmark = "";
                var LocationAPI = RestService.For<IDClistings>(Commonsettings.DaycareAPI);
                LandmarkList response = await LocationAPI.getlandmarklist(txtlocation.Text);
                if (response != null && response.RowData.Count > 0)
                {
                   
                    cityurl = response.RowData.Last().Cityurl;
                    OnPropertyChanged(nameof(LocationList));
                    landmarklist.IsVisible = true;
                    locationlist.IsVisible = false;
                    landmarklistdata.ItemsSource = response.RowData;
                }

            }
            catch (Exception e)
            {
            }
        }
        private void LandmarkCmd(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var radiogrp = sender as ListView;
                LandmarkList_DATA lsow = new LandmarkList_DATA();
                lsow = (LandmarkList_DATA)radiogrp.SelectedItem;
                Tlocationtxt = lsow.Landmark;
                txtlocation.Text = lsow.Landmark;
                listdata.landmark = lsow.Landmark;
                landmark = lsow.Landmark;
                cityurl = lsow.Cityurl;
                landmarkID = lsow.Contentid;
                changezipcode = lsow.Cityurl;
                city = lsow.City;
                Zipcode = lsow.Zipcode.ToString();
                latitude = lsow.Lat;
                longitude = lsow.Long;
                locationlist.IsVisible = false;
                landmarklist.IsVisible = false;

                if (lsow.Cityurl != Commonsettings.Usercityurl)
                {
                    locationselected.IsVisible = true;
                }
                else
                {
                    locationselected.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public async void GetLocationname()
        {
            try
            {
                cityurl = "";
                changezipcode = "";
                string Fnearby = "";
                if (nearbyID== "metro")
                {
                    Fnearby = "1";
                }
                else 
                {
                    Fnearby = "0";
                }
                var LocationAPI = RestService.For<ILocalJob>(Commonsettings.LocaljobsAPI);
                LocationOverallData response = await LocationAPI.getoverallLocation(txtlocation.Text, Fnearby);
                if (response != null && response.ROW_DATA.Count > 0)
                {
                    if (Fnearby == "0")
                    {
                        cityurl = response.ROW_DATA.Last().newcityurl;
                    }
                    else if (Fnearby == "1")
                    {
                        metrourl = response.ROW_DATA.Last().newcityurl;
                    }
                    OnPropertyChanged(nameof(LocationList));
                    locationlist.IsVisible = true;
                    locationlistdata.ItemsSource = response.ROW_DATA;
                }

            }
            catch (Exception e)
            {
            }
        }
       
        private void LocationCmd(object sender, ItemTappedEventArgs e)
        {
            try
            {
                landmarkID = "";
                var radiogrp = sender as ListView;
                Locations lsow = new Locations();
                lsow = (Locations)radiogrp.SelectedItem;
                Tlocationtxt = lsow.city;
                txtlocation.Text = lsow.city;
                cityurl = lsow.newcityurl;
                if(nearbyID=="metro")
                {
                    metrourl = lsow.newcityurl;
                }
                changezipcode = lsow.newcityurl;
                city = lsow.city;
                Zipcode = lsow.zipcode.ToString();
                latitude = lsow.latitude;
                longitude = lsow.longitude;
                string Country = lsow.country;
                locationlist.IsVisible = false;

                if (lsow.newcityurl != Commonsettings.Usercityurl)
                {
                    locationselected.IsVisible = true;
                }
                else
                {
                    locationselected.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        private void GenderMaleCommand(object sender, EventArgs e)
        {
            AnyImg.Source = "RadioButtonUnChecked.png";
            MaleImg.Source = "RadioButtonChecked.png";
            FeMaleImg.Source = "RadioButtonUnChecked.png";
            gendertype = "male";
            gendertickimg.IsVisible = true;
        }

        private void GenderFemaleCommand(object sender, EventArgs e)
        {
            AnyImg.Source = "RadioButtonUnChecked.png";
            MaleImg.Source = "RadioButtonUnChecked.png";
            FeMaleImg.Source = "RadioButtonChecked.png";
            gendertype = "female";
            gendertickimg.IsVisible = true;
        }

        private void AnyCommand(object sender, EventArgs e)
        {
            AnyImg.Source = "RadioButtonChecked.png";
            MaleImg.Source = "RadioButtonUnChecked.png";
            FeMaleImg.Source = "RadioButtonUnChecked.png";
            gendertype = "any";
            gendertickimg.IsVisible = false;
        }

        private void experience_Tapped(object sender, EventArgs e)
        {
            contentlayout.IsVisible = true;
            allservicevisible.IsVisible = false;
            experiencelistviewvisble.IsVisible = true;
        }
        public int cityimgnum = 0;
        public int needserviceimgnum = 0;
        public int languageimgnum = 0;
        public int careserviceimgnum = 0;
        public int radioworktypeimgnum = 0;
        public int radioworkcheckboximgnum = 0;
        public int agegroupimgnum = 0;
        public int genderimgnum = 0;
        public int calenderimgnum = 0;
        public int experienceimgnum = 0;
        private void citytapcmd(object sender, EventArgs e)
        {
            try
            {
                if (cityimgnum == 0)
                {
                    citytypeimg.Source = "minusGrey.png";
                    cityimgnum = 1;
                    citylayout.IsVisible = true;
                }
                else
                {
                    citytypeimg.Source = "downarrowGrey.png";
                    cityimgnum = 0;
                    citylayout.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void needservice_tap(object sender, EventArgs e)
        {
            try
            {
                if (needserviceimgnum == 0)
                {
                    needserviceimg.Source = "minusGrey.png";
                    needserviceimgnum = 1;
                    needserviceboxlayout.IsVisible = true;
                }
                else
                {
                    needserviceimg.Source = "downarrowGrey.png";
                    needserviceimgnum = 0;
                    needserviceboxlayout.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void language_tap(object sender, EventArgs e)
        {
            try
            {
                if (languageimgnum == 0)
                {
                    languageimg.Source = "minusGrey.png";
                    languageimgnum = 1;
                    languageboxlayout.IsVisible = true;
                }
                else
                {
                    languageimg.Source = "downarrowGrey.png";
                    languageimgnum = 0;
                    languageboxlayout.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void careservice_tap(object sender, EventArgs e)
        {
            try
            {
                if (careserviceimgnum == 0)
                {
                    careserviceimg.Source = "minusGrey.png";
                    careserviceimgnum = 1;
                    carserviceradiolayout.IsVisible = true;
                }
                else
                {
                    careserviceimg.Source = "downarrowGrey.png";
                    careserviceimgnum = 0;
                    carserviceradiolayout.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void radioworktype_tap(object sender, EventArgs e)
        {
            try
            {
                if (radioworktypeimgnum == 0)
                {
                    radioworktypeimg.Source = "minusGrey.png";
                    radioworktypeimgnum = 1;
                    radioworktypelistlayout.IsVisible = true;
                }
                else
                {
                    radioworktypeimg.Source = "downarrowGrey.png";
                    radioworktypeimgnum = 0;
                    radioworktypelistlayout.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void chkbxworktyp_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (radioworkcheckboximgnum == 0)
                {
                    chkbxworktypeimg.Source = "minusGrey.png";
                    radioworkcheckboximgnum = 1;
                    chkbxworktypelistlayout.IsVisible = true;
                }
                else
                {
                    chkbxworktypeimg.Source = "downarrowGrey.png";
                    radioworkcheckboximgnum = 0;
                    chkbxworktypelistlayout.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void agegroup_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (agegroupimgnum == 0)
                {
                    agegroupimg.Source = "minusGrey.png";
                    agegroupimgnum = 1;
                    agegrouplayout.IsVisible = true;
                }
                else
                {
                    agegroupimg.Source = "downarrowGrey.png";
                    agegroupimgnum = 0;
                    agegrouplayout.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void gender_tapped(object sender, EventArgs e)
        {
            try
            {
                if (genderimgnum == 0)
                {
                    genderimg.Source = "minusGrey.png";
                    genderimgnum = 1;
                    genderlayout.IsVisible = true;
                }
                else
                {
                    genderimg.Source = "downarrowGrey.png";
                    genderimgnum = 0;
                    genderlayout.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void calender_tap(object sender, EventArgs e)
        {
            try
            {
                if (calenderimgnum == 0)
                {
                    calendarimg.Source = "minusGrey.png";
                    calenderimgnum = 1;
                    calendarlayout.IsVisible = true;
                }
                else
                {
                    calendarimg.Source = "downarrowGrey.png";
                    calenderimgnum = 0;
                    calendarlayout.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void expandexperience_tap(object sender, EventArgs e)
        {
            try
            {
                if (experienceimgnum == 0)
                {
                    experienceimg.Source = "minusGrey.png";
                    experienceimgnum = 1;
                    experiencelistlayout.IsVisible = true;
                }
                else
                {
                    experienceimg.Source = "downarrowGrey.png";
                    experienceimgnum = 0;
                    experiencelistlayout.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Date_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(date.Date != DateTime.Today)
            {
                calentickimg.IsVisible = true;
            }
        }

        private void applyfiltercmd(object sender, EventArgs e)
        {
            applyfilter();
        }
        public async void applyfilter()
        {
            try
            {
                //if ((txtlocation.Text.Trim().Length == 0 || changezipcode.Trim().Length == 0) && nearbyID != "2")
                if ((txtlocation.Text.Trim().Length == 0 || string.IsNullOrEmpty(changezipcode)) && nearbyID != "2")
                {
                    dialog.Toast("Select Location...");
                }
                else
                {
                    if (!string.IsNullOrEmpty(cityurl))
                    {
                        listdata.cityurl = cityurl;
                    }

                    if (!string.IsNullOrEmpty(Zipcode))
                    {
                        listdata.userlat = latitude.ToString();
                        listdata.userlong = longitude.ToString();
                    }
                    //if(!string.IsNullOrEmpty(caretypeurl))
                    //{
                    //    listdata.caretype = caretypeurl;
                    //}
                    listdata.city = city;
                    //listdata.statecode = state;
                    listdata.nearby = nearbyID;
                    //if(nearbyID=="metro" && !string.IsNullOrEmpty(metrourl))
                    //{
                    //    listdata.cityurl = metrourl;
                    //}
                    //listdata.metr = metrourl;
                    listdata.careprovidertype = listdata.careprovidertype;
                    listdata.needservice = checkedneedservicetypevalue;
                    if(languagevalue== "any")
                    {
                        listdata.language = "";
                    }
                    else
                    {
                        listdata.language = languagevalue;
                    }
                    
                    if(radioworktypelayout.IsVisible==true)
                    {
                        listdata.worktype = worktypeID;
                    }
                    else if(checkboxworktypelayout.IsVisible==true)
                    {
                        listdata.worktype = checkedworktypetypevalue;
                    }
                    listdata.agegroupid = checkedagegrouptypevalue;
                    listdata.gendertype = gendertype;
                    if(calentickimg.IsVisible==true)
                    {
                        listdata.date = date.Date.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        listdata.date = "";
                    }
                    
                    listdata.experience = experienceyrs;
                    if (listdata.careprovidertype == "elder-care-center" || listdata.careprovidertype == "nanny-babysitter-jobs" || listdata.careprovidertype == "care-center")
                    {
                        await Task.Delay(200);
                        if (listdata.worktype == "19" || listdata.worktype == "20" || listdata.worktype == "19,42" || listdata.worktype == "20,42")//|| worktypeID == "19,42" || worktypeID == "20,42"
                        {
                            listdata.worktype = "";
                        }
                    }
                        //filtercount
                    if (locationselected.IsVisible == true)
                    {
                        filtercount = filtercount + 1;
                        listdata.locationselected = true;
                    }
                    if (careservicetickimg.IsVisible == true)
                    {
                        filtercount = filtercount + 1;
                        listdata.careservicetickimg = true;
                    }
                    if (needserviceselectedcount.Text.Trim().Length != 0 && needserviceselectedcount.Text != "0")
                    {
                        filtercount = filtercount + 1;
                        listdata.needserviceselectedcount = true;
                    }
                    if (languagetickimg.IsVisible == true)
                    {
                        filtercount = filtercount + 1;
                        listdata.languagetickimg = true;
                    }
                    if (agegroupcount.Text.Trim().Length != 0 && agegroupcount.Text != "0")
                    {
                        filtercount = filtercount + 1;
                        listdata.agegroupcount = true;
                    }
                    if(gendertickimg.IsVisible==true)
                    {
                        filtercount = filtercount + 1;
                        listdata.gendertickimg = true;
                    }
                    if(worktypecount.Text.Trim().Length != 0 && worktypecount.Text != "0")
                    {
                        filtercount = filtercount + 1;
                        listdata.worktypecount = true;
                    }
                    if(radioworktypetickimg.IsVisible==true)
                    {
                        filtercount = filtercount + 1;
                        listdata.radioworktypetickimg = true;
                    }
                    if(calentickimg.IsVisible==true)
                    {
                        filtercount = filtercount + 1;
                        listdata.calentickimg = true;
                    }
                    if(experiencetickimg.IsVisible==true)
                    {
                        filtercount = filtercount + 1;
                        listdata.experiencetickimg = true;
                    }
                    //if (Tlocationtxt != Commonsettings.Usercity + ", " + Commonsettings.Userstatecode)
                    //{
                    //    filtercount = filtercount + 1;
                    //}
                    listdata.filtercount = filtercount;
                    await Navigation.PopModalAsync();
                    await Navigation.PushAsync(new DaycareListing(listdata));
                }
            }
            catch (Exception e)
            {
                string msg = e.Message;
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}