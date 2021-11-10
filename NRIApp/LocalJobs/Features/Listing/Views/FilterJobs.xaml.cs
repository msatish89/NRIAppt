using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Listing.Interfaces;
using NRIApp.LocalJobs.Features.Listing.Models;
using NRIApp.LocalJobs.Features.Posting.Interfaces;
using NRIApp.LocalJobs.Features.Posting.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Listing.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FilterJobs : ContentPage
	{
        IUserDialogs dialog = UserDialogs.Instance;
        List<sortlist> nearbytype = new List<sortlist>();
        List<sortlist> jobtype = new List<sortlist>();
        public List<sortlist> getnearbytype { get; set; }
        //public List<Jobroles> getjobrolelist { get; set; }
       // public List<Jobroles> getjobrolelist { get; set; }
        public List<Skillslist> getskillslist { get; set; }
        public List<Filterdata> getemployementtypelist { get; set; }
        public List<Filterdata> getvisatypelist { get; set; }
        public List<Filterdata> getindustrytypelist { get; set; }
        public List<Functionalarea> Functionalareas { get; set; }



        private int _functionalareaid;
        public int functionalareaid
        {
            get { return _functionalareaid; }
            set { _functionalareaid = value;OnPropertyChanged(nameof(functionalareaid)); }
        }
        private string _industrytypeid;
        public string industrytypeid
        {
            get { return _industrytypeid; }
            set { _industrytypeid = value; OnPropertyChanged(nameof(industrytypeid)); }
        }
        private string _Fnearby;
        public string Fnearby
        {
            get { return _Fnearby; }
            set { _Fnearby = value; OnPropertyChanged(nameof(Fnearby)); }
        }
        private string _checkedvisatypevalue;
        public string checkedvisatypevalue
        {
            get { return _checkedvisatypevalue; }
            set { _checkedvisatypevalue = value; OnPropertyChanged(nameof(checkedvisatypevalue)); }
        }
        private string _checkedemployementtypevalue;
        public string checkedemployementtypevalue
        {
            get { return _checkedemployementtypevalue; }
            set { _checkedemployementtypevalue = value; OnPropertyChanged(nameof(checkedemployementtypevalue)); }
        }
        private string _checkedfunctionalareavalue;
        public string checkedfunctionalareavalue
        {
            get { return _checkedfunctionalareavalue; }
            set { _checkedfunctionalareavalue = value; OnPropertyChanged(nameof(checkedfunctionalareavalue)); }
        }
        private string _checkedskillsavalue;
        public string checkedskillsavalue
        {
            get { return _checkedskillsavalue; }
            set { _checkedskillsavalue = value; OnPropertyChanged(nameof(checkedskillsavalue)); }
        }
        private string _jobroleid;
        public string jobroleid
        {
            get { return _jobroleid; }
            set { _jobroleid = value; OnPropertyChanged(nameof(jobroleid)); }
        }
        private string _skillsid;
        public string skillsid
        {
            get { return _skillsid; }
            set { _skillsid = value; OnPropertyChanged(nameof(skillsid)); }
        }
        public int functionalareaimgnum = 0;
        public int jobroleimgnum = 0;
        public int cityimgnum = 0;
        public int changecitytypeimgnum = 0;
        public int salaryimgnum = 0;
        public int experienceimgnum = 0;
        public int skillsimgnum = 0;
        public int employementtypeimgnum = 0;
        public int visatypeimgnum = 0;
        public int industryimgnum = 0;

        private string _Tlocationtxt = "";
        public string Tlocationtxt
        {
            get { return _Tlocationtxt; }
            set { _Tlocationtxt = value;OnPropertyChanged(nameof(Tlocationtxt)); }
        }
        public static string _Tjobroletxt = "";
        public string Tjobroletxt
        {
            get { return _Tjobroletxt; }
            set { _Tjobroletxt = value; OnPropertyChanged(nameof(Tjobroletxt)); }
        }
        public static string _jobroleurl = "";
        public string jobroleurl
        {
            get { return _jobroleurl; }
            set { _jobroleurl = value; OnPropertyChanged(nameof(jobroleurl)); }
        }
        private string _cityurl = "";
        public string cityurl
        {
            get { return _cityurl; }
            set { _cityurl = value; OnPropertyChanged(nameof(cityurl)); }
        }
        private string _metrourl = "";
        public string metrourl
        {
            get { return _metrourl; }
            set { _metrourl = value; OnPropertyChanged(nameof(metrourl)); }
        }
        private string _Jobroletext = "";
        public string Jobroletext
        {
            get { return _Jobroletext; }
            set { _Jobroletext = value;
                OnPropertyChanged(nameof(Jobroletext));
                if(!string.IsNullOrEmpty(Jobroletext))
                {
                   // getjobrole(Jobroletext);
                }
            }
        }
        private string _Zipcode;
        public string Zipcode
        {
            get { return _Zipcode; }
            set { _Zipcode = value; }
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
        LocalJobSenddata Applyfilterdata = new LocalJobSenddata();
        public FilterJobs (LocalJobSenddata listdata)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            try
            {
                city = listdata.city;
                StateCode = listdata.statecode;
                Applyfilterdata = listdata;
                changecitylayout.IsVisible = false;
               // txtlocation.Text = listdata.city;
                changecitytypeimg.Source= "downarrowGrey.png";
                citytypeimg.Source = "downarrowGrey.png";
                citylayout.IsVisible = false;
                jobroleimg.Source = "downarrowGrey.png";
                jobrolelayout.IsVisible = false;
                jobrolemainlayout.IsVisible = false;
                ////getjobrole(functionalareaid);
                skillstypeimg.Source = "downarrowGrey.png";
                skillslayout.IsVisible = false;
                functionalareaimg.Source = "downarrowGrey.png";
              // // functionalareaimgnum = 1;
                Fuctionalarealayout.IsVisible = false;
                industryimg.Source = "downarrowGrey.png";
                Industrytypelayout.IsVisible = false;
               // Skillsmoreblk.IsVisible = false;
                experiencerange.IsVisible = false;
                if (!string.IsNullOrEmpty(listdata.experiencefrom) && listdata.experiencefrom != "0")
                {
                    experiencevalue.LowerValue = Convert.ToInt64(listdata.experiencefrom);
                }
                if (!string.IsNullOrEmpty(listdata.experienceto) && listdata.experienceto != "0")
                {
                    experiencevalue.UpperValue = Convert.ToInt64(listdata.experienceto);
                }
                salaryrange.IsVisible = false;
                if(!string.IsNullOrEmpty(listdata.minsal) && listdata.minsal!="0")
                {
                    salaryvalue.LowerValue = Convert.ToInt64(listdata.minsal);
                }
                if (!string.IsNullOrEmpty(listdata.maxsal) && listdata.maxsal != "0")
                {
                    salaryvalue.UpperValue = Convert.ToInt64(listdata.maxsal);
                }
                if(listdata.nearby=="2")
                {
                    CityLayoutChange.IsVisible = false;
                }
                else
                {
                    CityLayoutChange.IsVisible = true;
                }
                experienceimg.Source = "downarrowGrey.png";
                salaryimg.Source = "downarrowGrey.png";
                employementtypeimg.Source = "downarrowGrey.png";
                Employmenttypelayout.IsVisible = false;
                visatypeimg.Source = "downarrowGrey.png";
                Visatypelayout.IsVisible = false;

                getnearby(listdata.nearby);
                getskills(listdata.skillids, listdata.functionids);
                
                getfunctionalareadata(listdata.functionids);
                getindustrytype(listdata.industryids);
                getemployementtype(listdata.employmenttype);
                getworkauthorization(listdata.visatypeids);

                checkedemployementtypevalue = listdata.employmenttype;
                checkedfunctionalareavalue = listdata.functionids.ToString();
                checkedvisatypevalue = listdata.visatypeids;
                checkedskillsavalue = listdata.skillids;
                if(listdata.city.Contains(","))
                {
                    Tlocationtxt = listdata.city;
                    txtlocation.Text = listdata.city;
                }
                else
                {
                    Tlocationtxt = listdata.city + ", " + listdata.statecode;
                    txtlocation.Text = listdata.city + ", " + listdata.statecode;
                }
                //Tlocationtxt = listdata.city + ", " + listdata.statecode;
                //txtlocation.Text = listdata.city + ", " + listdata.statecode;
                if (!string.IsNullOrEmpty(listdata.roletxt))
                {
                    jobroleselected.IsVisible = true;
                }
                else
                {
                    jobroleselected.IsVisible = false;
                }
                jobroletext.Text = listdata.roletxt;
                Tjobroletxt = listdata.roletxt;
                if(!string.IsNullOrEmpty(listdata.roleid))
                {
                    jobroleid = listdata.roleid;
                }
                changezipcode = listdata.cityurl;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

        }
        public async void getworkauthorization(string visatype)
        {
           try
            {
                if (!string.IsNullOrEmpty(visatype))
                {
                    string[] visatypecountarr = visatype.Split(',');
                    visatypeselectedcount.Text = visatypecountarr.Count().ToString();
                    visatypeselected.IsVisible = true;
                }
                else
                {
                    visatypeselectedcount.Text = "0";
                    visatypeselected.IsVisible = false;
                }

                var LJAPI = RestService.For<ILocalJob>(Commonsettings.LocaljobsAPI);
                FilterListings list = await LJAPI.Getfilterdata("4");
                getvisatypelist = list.ROW_DATA;
                foreach (var data in getvisatypelist)
                {
                    BoxView boxx = new BoxView();
                    boxx.BackgroundColor = Color.FromHex("e0e0e0");
                    boxx.HeightRequest = 1;
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    chbx.Text = data.tag;
                    chbx.ClassId = data.contentid;
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_visatypeCheckChanged;
                    chbx.Padding = 5;
                    if (!string.IsNullOrEmpty(visatype))
                    {
                        string[] visatypelst = visatype.Split(',');
                        foreach (var i in visatypelst)
                        {
                            if (data.contentid == i)
                            {
                                chbx.IsChecked = true;
                            }
                        }
                    }
                    Visatypelayout.Children.Add(chbx);
                    Visatypelayout.Children.Add(boxx);
                }
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
        }
        private void Chb_visatypeCheckChanged(object sender, EventArgs e)
        {
            try
            {
                var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
                if (category.IsChecked==true)
                {
                    if (checkedvisatypevalue != null && checkedvisatypevalue != "")
                    {
                        if (!checkedvisatypevalue.Contains(category.ClassId))
                        {
                            checkedvisatypevalue += "," + category.ClassId;
                        }
                    }
                    else
                    {
                        checkedvisatypevalue += category.ClassId;
                    }
                }
                else
                {
                    string[] indexvalue = checkedvisatypevalue.Split(',');
                    int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                    indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                    checkedvisatypevalue = string.Join(",", indexvalue);
                }
                if(checkedvisatypevalue.Trim().Length!=0)
                {
                    string[] visatypecountarr = checkedvisatypevalue.Split(',');
                    visatypeselectedcount.Text = visatypecountarr.Count().ToString();
                    visatypeselected.IsVisible = true;
                }
                else
                {
                    visatypeselectedcount.Text = "0";
                    visatypeselected.IsVisible = false;
                }
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public async void getemployementtype(string employementtype)
        {
            try
            {
                if (!string.IsNullOrEmpty(employementtype))
                {
                    employementselected.IsVisible = true;
                    string[] employementcountarr = employementtype.Split(',');
                    employementselectedcount.Text = employementcountarr.Count().ToString();
                }
                else
                {
                    employementselected.IsVisible = false;
                    employementselectedcount.Text = "0";
                }

                var LJAPI = RestService.For<ILocalJob>(Commonsettings.LocaljobsAPI);
                FilterListings list = await LJAPI.Getfilterdata("5");
                getemployementtypelist = list.ROW_DATA;
                foreach (var data in getemployementtypelist)
                {
                    BoxView boxx = new BoxView();
                    boxx.BackgroundColor = Color.FromHex("e0e0e0");
                    boxx.HeightRequest = 1;
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    chbx.Text = data.tag;
                    chbx.ClassId = data.contentid;
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_employementypeCheckChanged;
                    chbx.Padding = 5;
                    if (!string.IsNullOrEmpty(employementtype))
                    {
                        string[] emptypelst = employementtype.Split(',');
                        foreach (var i in emptypelst)
                        {
                            if (data.contentid == i)
                            {
                                chbx.IsChecked = true;
                            }
                        }
                    }
                    Employmenttypelayout.Children.Add(chbx);
                    Employmenttypelayout.Children.Add(boxx);
                }
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
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
                if(checkedemployementtypevalue.Trim().Length!=0)
                {
                    employementselected.IsVisible = true;
                    string[] employementcountarr = checkedemployementtypevalue.Split(',');
                    employementselectedcount.Text = employementcountarr.Count().ToString();
                }
                else
                {
                    employementselected.IsVisible = false;
                    employementselectedcount.Text = "0";
                }

            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public async void getindustrytype(string industrytype)
        {
            try
            {
                if (!string.IsNullOrEmpty(industrytype))
                {
                    industryselected.IsVisible = true;
                }
                var LJAPI = RestService.For<ILocalJob>(Commonsettings.LocaljobsAPI);
                FilterListings list = await LJAPI.Getfilterdata("6");
                getindustrytypelist = list.ROW_DATA;
                foreach (var data in getindustrytypelist)
                {
                    if (industrytype == data.contentid)
                    {
                        data.image = "RadioButtonChecked.png";
                        industrytypeid = data.contentid;
                    }
                    else
                    {
                        data.image = "RadioButtonUnChecked.png";
                    }
                }

                Industrydata.ItemsSource = getindustrytypelist;
            }
            catch(Exception ex)
            {

            }
           // getindustrytypelist = nearbytype;
        }
        private void Industrydata_ItemTapped(object sender, ItemTappedEventArgs e)
        {
           try
            {
                var radiogrp = sender as ListView;
                Filterdata lsow = new Filterdata();
                lsow = (Filterdata)radiogrp.SelectedItem;
                string industryid = lsow.contentid;

                foreach (var item in getindustrytypelist)
                {
                    if (item.contentid == industryid)
                    {
                        item.image = "RadioButtonChecked.png";
                        industrytypeid = item.contentid;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }
                }

                Industrydata.ItemsSource = null;
                Industrydata.ItemsSource = getindustrytypelist;

                if(!string.IsNullOrEmpty(industrytypeid))
                {
                    industryselected.IsVisible = true;
                }
                

            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
        }
        private void Industrytapcmd(object sender, EventArgs e)
        {
            try
            {
                if (industryimgnum == 0)
                {
                    industryimg.Source = "minusGrey.png";
                    industryimgnum = 1;
                    Industrytypelayout.IsVisible = true;
                }
                else
                {
                    industryimg.Source = "downarrowGrey.png";
                    industryimgnum = 0;
                    Industrytypelayout.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        private void EmployementTapcmd(object sender, EventArgs e)
        {
            try
            {
                if (employementtypeimgnum == 0)
                {
                    employementtypeimg.Source = "minusGrey.png";
                    employementtypeimgnum = 1;
                    Employmenttypelayout.IsVisible = true;
                }
                else
                {
                    employementtypeimg.Source = "downarrowGrey.png";
                    employementtypeimgnum = 0;
                    Employmenttypelayout.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            
        }
        private void visatypeTapcmd(object sender, EventArgs e)
        {
            try
            {
                if (visatypeimgnum == 0)
                {
                    visatypeimg.Source = "minusGrey.png";
                    visatypeimgnum = 1;
                    Visatypelayout.IsVisible = true;
                }
                else
                {
                    visatypeimg.Source = "downarrowGrey.png";
                    visatypeimgnum = 0;
                    Visatypelayout.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            
        }
        public int skillpageno = 1;
        public int skillID = 1;
        public async void getskills(string skillsid,int functionid)
        {
            try
            {
                // dialog.ShowLoading("", null);
                if (!string.IsNullOrEmpty(skillsid))
                {
                    string[] checkedskillarr = skillsid.Split(',');
                    checkedskillcount.Text = checkedskillarr.Count().ToString();
                    skillsselected.IsVisible = true;
                }
                else
                {
                    checkedskillcount.Text = "0";
                    skillsselected.IsVisible = false;
                }
                skillID = functionid;
                skillpageno = 1;
                if (string.IsNullOrEmpty(functionid.ToString()) || functionid==0)
                {
                    Skillsmainlayout.IsVisible = false;
                }
                else
                {
                    Skillsmainlayout.IsVisible = true;
                    var LJAPI = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                    Skills list = await LJAPI.GetSkills(functionid,1);
                    getskillslist = null;
                    getskillslist = list.ROW_DATA; //rolename contentid
                   if(getskillslist.Count>0)
                    {
                        if (list.ROW_DATA.Count > 0)
                        {
                            Skillsmainlayout.IsVisible = true;
                        }
                        else
                        {
                            Skillsmainlayout.IsVisible = false;
                        }
                        if (string.IsNullOrEmpty(skillsid))
                        {
                            skillslayout.Children.Clear();
                        }
                        foreach (var data in getskillslist)
                        {
                            BoxView boxx = new BoxView();
                            boxx.BackgroundColor = Color.FromHex("e0e0e0");
                            boxx.HeightRequest = 1;
                            Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                            //chbx.OnImg = "CheckBoxChecked.png";
                            //chbx.OffImg = "CheckBoxUnChecked.png";
                            chbx.Text = data.primarytag;
                            chbx.ClassId = data.primarytagid.ToString();
                            //chbx.ShowLabel = true;
                            chbx.CheckChanged += skills_checkchanged;
                            chbx.Padding = 5;

                            if (!string.IsNullOrEmpty(skillsid))
                            {
                                string[] skillslst = skillsid.Split(',');
                                foreach (var i in skillslst)
                                {
                                    if (i == data.primarytagid.ToString())
                                    {
                                        chbx.IsChecked = true;
                                    }
                                }
                            }
                            skillslayout.Children.Add(boxx);
                            skillslayout.Children.Add(chbx);
                        }
                        if (list.ROW_DATA.Last().totalrecs > 25)
                        {
                            Skillsmoreblk.IsVisible = true;
                            Skillsmainlayout.IsVisible = true;
                            skillstypeimg.Source = "minusGrey.png";
                            skillsimgnum = 1;
                            skillslayout.IsVisible = true;
                        }
                        else
                        {
                            Skillsmoreblk.IsVisible = false;
                        }
                    }
                   else
                    {
                        Skillsmoreblk.IsVisible = false;
                        Skillsmainlayout.IsVisible = false;
                    }
                }
              //  dialog.HideLoading();
            }
            catch(Exception e)
            {
                string exmsg = e.Message;
                dialog.HideLoading();
            }
        }
        public async void Morejobskills(object sender, EventArgs e)
        {
            Skillsmainlayout.IsVisible = true;
            skillpageno = skillpageno + 1;
            skillsloader.IsRunning = true;
            try
            {
              //  dialog.ShowLoading("", null);
                    Skillsmainlayout.IsVisible = true;
                    var LJAPI = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                    Skills list = await LJAPI.GetSkills(skillID, skillpageno);
                    getskillslist = null;
                    getskillslist = list.ROW_DATA; //rolename contentid
                    if (getskillslist.Count > 0)
                    {
                    if (list.ROW_DATA.Count > 0)
                    {
                        Skillsmainlayout.IsVisible = true;
                    }
                    else
                    {
                        Skillsmainlayout.IsVisible = false;
                    }
                    //if (string.IsNullOrEmpty(skillsid))
                    //{
                    //    skillslayout.Children.Clear();
                    //}
                    foreach (var data in getskillslist)
                    {
                        BoxView boxx = new BoxView();
                        boxx.BackgroundColor = Color.FromHex("e0e0e0");
                        boxx.HeightRequest = 1;
                        Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                        //chbx.OnImg = "CheckBoxChecked.png";
                        //chbx.OffImg = "CheckBoxUnChecked.png";
                        chbx.Text = data.primarytag;
                        chbx.ClassId = data.primarytagid.ToString();
                        //chbx.ShowLabel = true;
                        chbx.CheckChanged += skills_checkchanged;
                        chbx.Padding = 5;

                        if (!string.IsNullOrEmpty(skillsid))
                        {
                            string[] skillslst = skillsid.Split(',');
                            foreach (var i in skillslst)
                            {
                                if (i == data.primarytagid.ToString())
                                {
                                    chbx.IsChecked = true;
                                }
                            }
                        }
                        skillslayout.Children.Add(boxx);
                        skillslayout.Children.Add(chbx);
                    }
                    if (list.ROW_DATA.Last().totalrecs > 25 )
                    {
                        Skillsmoreblk.IsVisible = true;
                        
                    }
                    else
                    {
                        Skillsmoreblk.IsVisible = false;
                    }
                }
                    else
                    {
                        Skillsmainlayout.IsVisible = false;
                        Skillsmoreblk.IsVisible = false;
                }
              
               // dialog.HideLoading();
                skillsloader.IsRunning = false;
            }
            catch (Exception ex)
            {
                skillsloader.IsRunning = false;
                string exmsg = ex.Message;
                dialog.HideLoading();
            }
        }

        private void skills_checkchanged(object sender, EventArgs e)
        {
            try
            {

                var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
                if (category.IsChecked==true)
                {
                    if (checkedskillsavalue != null && checkedskillsavalue != "")
                    {
                        if (!checkedskillsavalue.Contains(category.ClassId))
                        {
                            checkedskillsavalue += "," + category.ClassId;
                        }
                    }
                    else
                    {
                        checkedskillsavalue += category.ClassId;
                    }

                }
                else
                {
                    string[] indexvalue = checkedskillsavalue.Split(',');
                    int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                    indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                    checkedskillsavalue = string.Join(",", indexvalue);
                }
                if(checkedskillsavalue.Trim().Length!=0)
                {
                    string[] checkedskillarr = checkedskillsavalue.Split(',');
                    checkedskillcount.Text = checkedskillarr.Count().ToString();
                    skillsselected.IsVisible = true;
                }
                else
                {
                    checkedskillcount.Text = "0";
                    skillsselected.IsVisible = false;
                }
                
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            
        }
        
        private void Skillstapcmd(object sender, EventArgs e)
        {
            try
            {
                if (skillsimgnum == 0)
                {
                    skillstypeimg.Source = "minusGrey.png";
                    skillsimgnum = 1;
                    skillslayout.IsVisible = true;
                }
                else
                {
                    skillstypeimg.Source = "downarrowGrey.png";
                    skillsimgnum = 0;
                    skillslayout.IsVisible = false;
                    Skillsmoreblk.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            
        }
        public async void getfunctionalareadata(int functid)
        {
            try
            {
                dialog.ShowLoading("", null);
                if (!string.IsNullOrEmpty(functid.ToString()) && functid!=0)
                {
                    functionalareaselected.IsVisible = true;
                }
                else
                {
                    functionalareaselected.IsVisible = false;
                }
                int jobtype = 4;

                
                if (Applyfilterdata.jobtype=="1")
                {
                    //IT
                    jobtype = 3;
                    Functionalareadata.HeightRequest = 650;
                }
                else if(Applyfilterdata.jobtype=="2")
                {
                    //nonit
                    jobtype = 4;
                    Functionalareadata.HeightRequest = 2150;
                }
                else
                {
                    //alljobs
                    jobtype = 1;
                    //  Functionalareadata.HeightRequest = 2100;
                    Functionalareadata.HeightRequest = 1850;
                }
                
                //var LJAPI = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                //Functionalarealist list = await LJAPI.Getfunctionalarea(jobtype);
                var LJAPI = RestService.For<ILocalJob>(Commonsettings.LocaljobsAPI);
                Functionalarealist list = await LJAPI.Getfunctionalarea(jobtype);

                if(list!=null)
                {
                    Functionalareadata.HeightRequest = list.ROW_DATA.Count * 50;
                }

                foreach (var data in list.ROW_DATA)
                {
                    if (functid == data.secondarycategoryid)
                    {
                        data.image = "RadioButtonChecked.png";
                        functionalareaid = data.secondarycategoryid;
                    }
                    else
                    {
                        data.image = "RadioButtonUnChecked.png";
                    }
                }
                Functionalareadata.ItemsSource = list.ROW_DATA;
                Functionalareas = list.ROW_DATA;
                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                dialog.HideLoading();
            }
            
        }
        private void functionalareadata_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                dialog.ShowLoading("",null);
                var radiogrp = sender as ListView;
                Functionalarea lsow = new Functionalarea();
                lsow = (Functionalarea)radiogrp.SelectedItem;
                int functionalarea = lsow.secondarycategoryid;

                foreach (var item in Functionalareas)
                {
                    if (item.secondarycategoryid == functionalarea)
                    {
                        item.image = "RadioButtonChecked.png";
                        functionalareaid = item.secondarycategoryid;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }
                }
                if(!string.IsNullOrEmpty(functionalareaid.ToString()))
                {
                    functionalareaselected.IsVisible = true;
                }
                else
                {
                    functionalareaselected.IsVisible = false;
                }
                Functionalareadata.ItemsSource = null;
                Functionalareadata.ItemsSource = Functionalareas;
                getskills("", functionalarea);
                //getjobrole(functionalarea);
                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
                string msg = ex.Message;
            }
            
        }
        private void Functionalareatapcmd(object sender, EventArgs e)
        {
            if (functionalareaimgnum == 0)
            {
                functionalareaimg.Source = "minusGrey.png";
                functionalareaimgnum = 1;
                Fuctionalarealayout.IsVisible = true;
            }
            else
            {
                functionalareaimg.Source = "downarrowGrey.png";
                functionalareaimgnum = 0;
                Fuctionalarealayout.IsVisible = false;
            }
        }
        public void getnearby(string nearby)
        {
            try
            {
                nearbytype.Add(new sortlist { filterby = "City", type = "0" });
                nearbytype.Add(new sortlist { filterby = "Metro", type = "1" });
                nearbytype.Add(new sortlist { filterby = "USA/Canada", type = "2" });
                foreach (var nearbylist in nearbytype)
                {
                    if (!string.IsNullOrEmpty(nearby))
                    {
                        if (nearby == nearbylist.type)
                        {
                            nearbylist.image = "RadioButtonChecked.png";
                            Fnearby = nearbylist.type;
                        }
                        else
                        {
                            nearbylist.image = "RadioButtonUnChecked.png";
                        }
                    }
                }
                citydata.ItemsSource = nearbytype;
                getnearbytype = nearbytype;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
           
        }
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
            catch(Exception ex)
            {
                
            }
        }
        private void nearbydata_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var radiogrp = sender as ListView;
                sortlist lsow = new sortlist();
                lsow = (sortlist)radiogrp.SelectedItem;
                string orderby = lsow.type;

                foreach (var item in nearbytype)
                {
                    if (item.type == lsow.type)
                    {
                        item.image = "RadioButtonChecked.png";
                        Fnearby = item.type;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }
                }

                citydata.ItemsSource = null;
                citydata.ItemsSource = nearbytype;

              //  var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
              if(lsow.type=="2" && string.IsNullOrEmpty(city))
                {
                    Tlocationtxt = Commonsettings.Usercity+","+Commonsettings.Userstatecode;
                    txtlocation.Text = Commonsettings.Usercity + "," + Commonsettings.Userstatecode;
                    cityurl = Commonsettings.Usercityurl;
                    Zipcode = Commonsettings.Userzipcode;
                    txtlocation.Unfocus();
                    locationlist.IsVisible = false;
                   
                }
                if (lsow.type!="2")
                {
                    changecitytypeimg.Source = "minusGrey.png";
                    changecitytypeimgnum = 1;
                    changecitylayout.IsVisible = true;
                    locationlist.IsVisible = false;
                    txtlocation.Text = "";
                    txtlocation.Focus();
                }
                if(lsow.type=="2")
                {
                    CityLayoutChange.IsVisible = false;
                  //  txtlocation.Text = Applyfilterdata.city + "," + Applyfilterdata.statecode;
                }
                else
                {
                    CityLayoutChange.IsVisible = true;
                }
            if(Fnearby!="0")
                {
                    searchbycityimg.IsVisible = true;
                }
            else
                {
                    searchbycityimg.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        
        private void Jobroletapcmd(object sender, EventArgs e)
        {
            if (jobroleimgnum == 0)
            {
                jobroleimg.Source = "minusGrey.png";
                jobroleimgnum = 1;
                jobrolelayout.IsVisible = false;
                jobrolemainlayout.IsVisible = true;
            }
            else
            {
                jobroleimg.Source = "downarrowGrey.png";
                jobroleimgnum = 0;
                jobrolelayout.IsVisible = false;
                jobrolemainlayout.IsVisible = false;
            }
        }
        
        private void Jobroledata_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var radiogrp = sender as ListView;
                Jobroles lsow = new Jobroles();
                lsow = (Jobroles)radiogrp.SelectedItem;
                Tjobroletxt = lsow.rolename;
                jobroletext.Text = lsow.rolename;
                //jobroleid = lsow.contentid;
                jobroleid = lsow.tagid;
                jobroleurl = lsow.roleurl;
                jobrolelayout.IsVisible = false;

                if(!string.IsNullOrEmpty(jobroleid))
                {
                    jobroleselected.IsVisible = true;
                }
                else
                {
                    jobroleselected.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        private void salaryrangetapcmd(object sender, EventArgs e)
        {
            if (salaryimgnum == 0)
            {
                salaryimg.Source = "minusGrey.png";
                salaryimgnum = 1;
                salaryrange.IsVisible = true;
            }
            else
            {
                salaryimg.Source = "downarrowGrey.png";
                salaryimgnum = 0;
                salaryrange.IsVisible = false;
            }
        }
        private void experiencerangetapcmd(object sender, EventArgs e)
        {
            if (experienceimgnum == 0)
            {
                experienceimg.Source = "minusGrey.png";
                experienceimgnum = 1;
                experiencerange.IsVisible = true;
            }
            else
            {
                experienceimg.Source = "downarrowGrey.png";
                experienceimgnum = 0;
                experiencerange.IsVisible = false;
            }
        }
        private void Backbtn_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void reset_Tap(object sender, EventArgs e)
        {
            try
            {
                var alert = await DisplayAlert("Alert!", "Are you sure?", "Reset", "Back");
                if (alert)
                {
                  //  dialog.ShowLoading("", null);
                    salaryvalue.LowerValue = 0;
                    salaryvalue.UpperValue = 500000;
                    experiencevalue.LowerValue = 0;
                    experiencevalue.UpperValue = 40;
                    checkedemployementtypevalue = "";
                    checkedskillsavalue = "";
                    checkedvisatypevalue = "";

                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    foreach (var item in skillslayout.Children)
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
                    foreach (var item in Employmenttypelayout.Children)
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
                    foreach (var item in Visatypelayout.Children)
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
                    foreach (var item in Fuctionalarealayout.Children)
                    {
                        foreach (var item1 in Functionalareas)
                        {
                            //if (item1.oderbylist == "Any")
                            //{
                            //    item1.image = "RadioButtonChecked.png";
                            //    Filter_RM.gender = "any";
                            //}
                            //else
                            //{
                            item1.image = "RadioButtonUnChecked.png";
                            // }
                            Functionalareadata.ItemsSource = null;
                            Functionalareadata.ItemsSource = Functionalareas;
                        }
                    }
                    foreach (var item in Industrytypelayout.Children)
                    {
                        foreach (var item1 in getindustrytypelist)
                        {
                            item1.image = "RadioButtonUnChecked.png";
                            Industrydata.ItemsSource = null;
                            Industrydata.ItemsSource = getindustrytypelist;
                        }
                    }
                    foreach (var item in citylayout.Children)
                    {
                        foreach (var item1 in getnearbytype)
                        {
                            if (item1.type == "1")
                            {
                                item1.image = "RadioButtonChecked.png";
                                Fnearby = item1.type;
                            }
                            else
                            {
                                item1.image = "RadioButtonUnChecked.png";
                            }
                            citydata.ItemsSource = null;
                            citydata.ItemsSource = getnearbytype;
                        }
                    }

                    Applyfilterdata.userlat = Commonsettings.UserLat;
                    Applyfilterdata.userlong = Commonsettings.UserLong;
                    Applyfilterdata.userpid = Commonsettings.UserPid;
                    Applyfilterdata.cityurl = Commonsettings.Usercityurl;
                    Applyfilterdata.city = Commonsettings.Usercity;
                    Applyfilterdata.zipcode = Commonsettings.Userzipcode;
                    Applyfilterdata.statecode = Commonsettings.Userstatecode;
                    Tlocationtxt = Applyfilterdata.city + ", " + Applyfilterdata.statecode;
                    txtlocation.Text = Applyfilterdata.city + ", " + Applyfilterdata.statecode;
                    
                    Tjobroletxt = "";
                    jobroletext.Text = "";
                    jobroleurl = "";
                    jobroleid = "";
                    jobrolelayout.IsVisible = false;
                    functionalareaid = 0;
                    industrytypeid = "";
                    //filtercount reset
                    functionalareaselected.IsVisible =false;
                    checkedskillcount.Text = "0";
                    jobroleselected.IsVisible =false;
                    industryselected.IsVisible = false;
                    employementselectedcount.Text = "0";
                    visatypeselectedcount.Text = "0";
                    changelocationtickimg.IsVisible = false;
                    searchbycityimg.IsVisible = false;
                    salarytickimg.IsVisible = false;
                    experiencetickimg.IsVisible = false;
                    // applyfilter();

                    if (!string.IsNullOrEmpty(cityurl))
                    {
                        Applyfilterdata.cityurl = cityurl;
                    }

                    if (string.IsNullOrEmpty(jobroletext.Text))
                    {
                        Tjobroletxt = "";
                        jobroleid = "";
                    }
                  
                    //Applyfilterdata.city = city;
                    //Applyfilterdata.statecode = StateCode;
                    //Applyfilterdata.nearby = Fnearby;
                   
                    Applyfilterdata.skillids = checkedskillsavalue;
                    Applyfilterdata.functionids = functionalareaid;
                    Applyfilterdata.industryids = industrytypeid;
                    Applyfilterdata.roleid = "";
                    Applyfilterdata.roletxt = "";
                    Applyfilterdata.jobrole = "";
                    Applyfilterdata.experiencefrom = Math.Round(experiencevalue.MinimumValue).ToString();
                    Applyfilterdata.experienceto = Math.Round(experiencevalue.MaximumValue).ToString();
                    Applyfilterdata.minsal = Math.Round(salaryvalue.MinimumValue).ToString();
                    Applyfilterdata.maxsal = Math.Round(salaryvalue.MaximumValue).ToString();
                    Applyfilterdata.employmenttype = checkedemployementtypevalue;
                    Applyfilterdata.visatypeids = checkedvisatypevalue;
                    Applyfilterdata.businessurl = "";
                    Applyfilterdata.filtercount = 0;
                    await Navigation.PopModalAsync();
                    await Navigation.PushAsync(new JobList(Applyfilterdata));

                  //  dialog.HideLoading();

                }
            }
            catch(Exception ex)
            {
                string msg = ex.Message;
            }
        }
        private void ApplyFiltercmd(object sender, EventArgs e)
        {
            applyfilter();
        }
        private int _filtercount = 0;
        public int filtercount
        {
            get { return _filtercount; }
            set { _filtercount = value;OnPropertyChanged(nameof(filtercount)); }
        }
        public async void applyfilter()
        {
            try
            {
                if ((txtlocation.Text.Trim().Length == 0 || changezipcode.Trim().Length == 0) && Fnearby != "2")
                {
                        dialog.Toast("Select Location...");
                }
                else
                {
                    if (!string.IsNullOrEmpty(cityurl))
                    {
                        Applyfilterdata.cityurl = cityurl;
                    }

                    if (string.IsNullOrEmpty(jobroletext.Text))
                    {
                        Tjobroletxt = "";
                        jobroleid = "";
                    }
                    if (!string.IsNullOrEmpty(Zipcode))
                    {
                        Applyfilterdata.userlat = latitude;
                        Applyfilterdata.userlong = longitude;
                    }
                    Applyfilterdata.city = city;
                    Applyfilterdata.statecode = StateCode;
                    Applyfilterdata.nearby = Fnearby;
                    // Applyfilterdata.metrourl = metrourl;
                    Applyfilterdata.skillids = checkedskillsavalue;
                    Applyfilterdata.functionids = functionalareaid;
                    Applyfilterdata.industryids = industrytypeid;
                    Applyfilterdata.roleid = jobroleid;
                    Applyfilterdata.roletxt = Tjobroletxt;
                    Applyfilterdata.jobrole = jobroleurl;
                    Applyfilterdata.experiencefrom = Math.Round(experiencevalue.LowerValue).ToString();
                    Applyfilterdata.experienceto = Math.Round(experiencevalue.UpperValue).ToString();
                    Applyfilterdata.minsal = Math.Round(salaryvalue.LowerValue).ToString();
                    Applyfilterdata.maxsal = Math.Round(salaryvalue.UpperValue).ToString();
                    Applyfilterdata.employmenttype = checkedemployementtypevalue;
                    Applyfilterdata.visatypeids = checkedvisatypevalue;
                    Applyfilterdata.businessurl = "";

                    //filtercount
                    if(functionalareaselected.IsVisible==true)
                    {
                        filtercount = filtercount + 1;
                    }
                    if(checkedskillcount.Text.Trim().Length!=0 && checkedskillcount.Text!="0" )
                    {
                        filtercount = filtercount + 1;
                    }
                    if(jobroleselected.IsVisible==true)
                    {
                        filtercount = filtercount + 1;
                    }
                    if(industryselected.IsVisible==true)
                    {
                        filtercount = filtercount + 1;
                    }
                    if (employementselectedcount.Text.Trim().Length != 0 && employementselectedcount.Text != "0")
                    {
                        filtercount = filtercount + 1;
                    }
                    if (visatypeselectedcount.Text.Trim().Length != 0 && visatypeselectedcount.Text != "0")
                    {
                        filtercount = filtercount + 1;
                    }
                    if((experiencevalue.MinimumValue!=experiencevalue.LowerValue) || (experiencevalue.MaximumValue != experiencevalue.UpperValue))
                    {
                        filtercount = filtercount + 1;
                    }
                    if ((salaryvalue.MinimumValue != salaryvalue.LowerValue) || (salaryvalue.MaximumValue != salaryvalue.UpperValue))
                    {
                        filtercount = filtercount + 1;
                    }
                    //Tlocationtxt = Applyfilterdata.city + ", " + Applyfilterdata.statecode;
                    if (Tlocationtxt != Commonsettings.Usercity + ", " + Commonsettings.Userstatecode)
                    {
                        filtercount = filtercount + 1;
                    }
                    if(Fnearby=="1" || Fnearby=="2")
                    {
                        filtercount = filtercount + 1;
                    }
                        Applyfilterdata.filtercount = filtercount;
                    await Navigation.PopModalAsync();
                    await Navigation.PushAsync(new JobList(Applyfilterdata));
                }
            }
            catch(Exception e)
            {
                string msg = e.Message;
            }
        }
        
        private void Txtlocation_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            if (txtlocation.Text != Tlocationtxt)
            {
                GetLocationname();
            }
        }

        private void LocationCmd(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var radiogrp = sender as ListView;
                Locations lsow = new Locations();
                lsow = (Locations)radiogrp.SelectedItem;
                Tlocationtxt = lsow.city ;
                txtlocation.Text = lsow.city;
                cityurl = lsow.newcityurl;

                changezipcode = lsow.newcityurl;

                //StateName = lsow.statename;
                //StateCode = lsow.statecode;
                city = lsow.city;
                Zipcode = lsow.zipcode.ToString();
                latitude = lsow.latitude;
                longitude = lsow.longitude;
                string Country = lsow.country;
                locationlist.IsVisible = false;

                if(lsow.newcityurl != Commonsettings.Usercityurl)
                {
                    changelocationtickimg.IsVisible = true;
                }
                else
                {
                    changelocationtickimg.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        private string _StateName = "";
        public string StateName
        {
            get { return _StateName; }
            set { _StateName = value; OnPropertyChanged(nameof(StateName)); }
        }
        private string _StateCode = "";
        public string StateCode
        {
            get { return _StateCode; }
            set { _StateCode = value; OnPropertyChanged(nameof(StateCode)); }
        }
        private string _city = "";
        public string city
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged(nameof(city)); }
        }
        private double _latitude ;
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

        private void changecitytapcmd(object sender, EventArgs e)
        {
            if (changecitytypeimgnum == 0)
            {
                changecitytypeimg.Source = "minusGrey.png";
                changecitytypeimgnum = 1;
                changecitylayout.IsVisible = true;
            }
            else
            {
                changecitytypeimg.Source = "downarrowGrey.png";
                changecitytypeimgnum = 0;
                changecitylayout.IsVisible = false;
            }
            locationlist.IsVisible = false;
        }

        private void jobroletxt_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            if(jobroletext.Text != Tjobroletxt)
            {
                getjobroleajax();
            }
            
        }
        public async void getjobroleajax()
        {
            try
            {
                Zipcode = "";
                var LJAPI = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                Jobroleslist list = await LJAPI.GetJobroleajax(jobroletext.Text);
                if(list.ROW_DATA.Count>0 && list.ROW_DATA!=null)
                {
                    jobroledata.ItemsSource = list.ROW_DATA;
                    jobrolelayout.IsVisible = true;
                }
            }
            catch (Exception e)
            {
            }
        }
        public async void GetLocationname()
        {
            try
            {
                cityurl = "";
                changezipcode = "";
                var LocationAPI = RestService.For<ILocalJob>(Commonsettings.LocaljobsAPI);
                LocationOverallData response = await LocationAPI.getoverallLocation(txtlocation.Text, Fnearby);
                if(response!=null && response.ROW_DATA.Count>0)
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
                    listdata.ItemsSource = response.ROW_DATA;
                }
               
            }
            catch (Exception e)
            {
            }
        }
     

        private  void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {

                //var scrollview = currentpage.FindByName<StackLayout>("cmscroll");
                //var stacklayout = currentpage.FindByName<ScrollView>("cmlayout");
                Device.BeginInvokeOnMainThread(() =>
                {
                    fscorll.ScrollToAsync(fstack, ScrollToPosition.Start, false);
                    //  await ScrollView. ScrollToAsync((Grid)grid, ScrollToPosition.End, false);
                });
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }

        private void Salaryvalue_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
           try
            {
                if (salaryvalue.LowerValue != salaryvalue.MinimumValue || salaryvalue.UpperValue != salaryvalue.MaximumValue)
                {
                    salarytickimg.IsVisible = true;
                }
                else
                {
                    salarytickimg.IsVisible = false;
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void Experiencevalue_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (experiencevalue.LowerValue != experiencevalue.MinimumValue || experiencevalue.UpperValue != experiencevalue.MaximumValue)
                {
                    experiencetickimg.IsVisible = true;
                }
                else
                {
                    experiencetickimg.IsVisible = false;
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}