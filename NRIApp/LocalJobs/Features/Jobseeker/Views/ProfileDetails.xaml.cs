using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Jobseeker.Interface;
using NRIApp.LocalJobs.Features.Jobseeker.Models;
using NRIApp.LocalJobs.Features.Listing.Interfaces;
using NRIApp.LocalJobs.Features.Listing.Models;
using NRIApp.LocalJobs.Features.Posting.Interfaces;
using NRIApp.LocalJobs.Features.Posting.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;

namespace NRIApp.LocalJobs.Features.Jobseeker.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfileDetails : ContentPage
	{
        IUserDialogs dialogs = UserDialogs.Instance;
        public List<Filterdata> getvisatypelist { get; set; }
        public List<Filterdata> getvisatypelist1 { get; set; }
        Jobseekers_DATA data = new Jobseekers_DATA();
        string Emailpattern = "^[\\W]*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z] {2,4}[\\W]*,{1}[\\W]*)*([\\w+\\-.%]+@[\\w\\-.]+\\.[A-Za-z]{2,4})[\\W]*$";
        public ProfileDetails (Jobseekers_DATA Sdata)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            data = Sdata;
            Title = "Profile Details";
            getworkauthorization(Sdata.workauthorization);
            jobrolelayout.IsVisible = false;
            //jobrolesmltxt.IsVisible = true;
            locationlist.IsVisible = false;
            //locationsmltxt.IsVisible = true;
            txtcontactemail.Text = Sdata.email;
            txtcontactname.Text = Sdata.contactperson;
            txtcontactphone.Text = Sdata.phone;
            Tjobroletxt = Sdata.jobrole;
            jobroletext.Text = Sdata.jobrole;
            
            Tlocationtxt = Sdata.city;
            txtlocation.Text = Sdata.city;
            city = Sdata.city;
            cityurl = Sdata.cityurl;
            checkedvisatypevalue = Sdata.workauthorization;

            if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                txtcontactemail.Text = Commonsettings.UserEmail;
            if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                txtcontactname.Text = Commonsettings.UserName;
            if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                txtcontactphone.Text = Commonsettings.UserMobileno;
        }
        public  string _Tjobroletxt = "";
        public string Tjobroletxt
        {
            get { return _Tjobroletxt; }
            set { _Tjobroletxt = value; OnPropertyChanged(nameof(Tjobroletxt)); }
        }
        private string _cityurl = "";
        public string cityurl
        {
            get { return _cityurl; }
            set { _cityurl = value; OnPropertyChanged(nameof(cityurl)); }
        }
        private string _Tlocationtxt = "";
        public string Tlocationtxt
        {
            get { return _Tlocationtxt; }
            set { _Tlocationtxt = value; OnPropertyChanged(nameof(Tlocationtxt)); }
        }
        private string _Zipcode = "";
        public string Zipcode
        {
            get { return _Zipcode; }
            set { _Zipcode = value; OnPropertyChanged(nameof(Zipcode)); }
        }
        private string _userlat = "";
        public string userlat
        {
            get { return _userlat; }
            set { _userlat = value; OnPropertyChanged(nameof(userlat)); }
        }
        private string _userlong = "";
        public string userlong
        {
            get { return _userlong; }
            set { _userlong = value; OnPropertyChanged(nameof(userlong)); }
        }
        private string _jobroleid = "";
        public string jobroleid
        {
            get { return _jobroleid; }
            set { _jobroleid = value; OnPropertyChanged(nameof(jobroleid)); }
        }
        private string _city = "";
        public string city
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged(nameof(city)); }
        }
        private string _country = "";
        public string country
        {
            get { return _country; }
            set { _country = value; OnPropertyChanged(nameof(country)); }
        }
        private string _checkedvisatypevalue;
        public string checkedvisatypevalue
        {
            get { return _checkedvisatypevalue; }
            set { _checkedvisatypevalue = value; OnPropertyChanged(nameof(checkedvisatypevalue)); }
        }
        private void jobroletxt_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            if (jobroletext.Text != Tjobroletxt)
            {
                getjobroleajax();
            }
        }
        public async void getjobroleajax()
        {
            try
            {
                jobroleid = "";
                Tjobroletxt = "";
                var LJAPI = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                Jobroleslist list = await LJAPI.GetJobroleajax(jobroletext.Text);
                if (list.ROW_DATA.Count > 0 && list.ROW_DATA != null)
                {
                    jobroledata.ItemsSource = list.ROW_DATA;
                    jobrolelayout.IsVisible = true;
                  //  jobrolesmltxt.IsVisible = false;
                }
            }
            catch (Exception e)
            {
            }
        }
        public async void getworkauthorization(string visatype)
        {
            try
            {
                dialogs.ShowLoading("", null);
                var LJAPI = RestService.For<ILocalJob>(Commonsettings.LocaljobsAPI);
                FilterListings list = await LJAPI.Getfilterdata("4");
                double lstcnt = list.ROW_DATA.Count;
                double halfcnt = Math.Round(lstcnt / 2, 1);
                getvisatypelist = list.ROW_DATA.Take(Convert.ToInt32(halfcnt)).ToList();
                getvisatypelist1 = list.ROW_DATA.Skip(Convert.ToInt32(halfcnt)).ToList();
                foreach (var data in getvisatypelist)
                {
                    //BoxView boxx = new BoxView();
                    //boxx.BackgroundColor = Color.FromHex("e0e0e0");
                    //boxx.HeightRequest = 1;
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    chbx.Text = data.tag;
                    chbx.ClassId = data.tag;
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_visatypeCheckChanged;
                    chbx.Padding = 5;
                    if (!string.IsNullOrEmpty(visatype))
                    {
                        string[] visatypelst = visatype.Split(',');
                        foreach (var i in visatypelst)
                        {
                            if (data.tag == i)
                            {
                                chbx.IsChecked = true;
                            }
                        }
                    }
                    Visatypelayout.Children.Add(chbx);
                   // Visatypelayout.Children.Add(boxx);
                }
                foreach (var data in getvisatypelist1)
                {
                    //BoxView boxx = new BoxView();
                    //boxx.BackgroundColor = Color.FromHex("e0e0e0");
                    //boxx.HeightRequest = 1;
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    chbx.Text = data.tag;
                    
                    chbx.ClassId = data.tag;
                    //chbx.ShowLabel = true;

                    //chbx.HeightRequest = 20;
                    //chbx.WidthRequest = 20;

                    chbx.CheckChanged += Chb_visatypeCheckChanged;
                    chbx.Padding = 5;
                    if (!string.IsNullOrEmpty(visatype))
                    {
                        string[] visatypelst = visatype.Split(',');
                        foreach (var i in visatypelst)
                        {
                            if (data.tag == i)
                            {
                                chbx.IsChecked = true;
                            }
                        }
                    }
                    Visatypelayout1.Children.Add(chbx);
                   // Visatypelayout1.Children.Add(boxx);
                }
                dialogs.HideLoading();
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
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
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public bool validation()
        {
            bool result = true;
            if(string.IsNullOrEmpty(txtcontactname.Text))
            {
                dialogs.Toast("Please enter your name");
                return false;
            }
            if (string.IsNullOrEmpty(jobroleid) && string.IsNullOrEmpty(Tjobroletxt))
            {
                dialogs.Toast("Please select job role");
                return false;
            }
            if (string.IsNullOrEmpty(Zipcode) && string.IsNullOrEmpty(Tlocationtxt))
            {
                dialogs.Toast("Please select location");
                return false;
            }
            if (string.IsNullOrEmpty(txtcontactemail.Text))
            {
                dialogs.Toast("Please enter your email");
                return false;
            }
            if(!string.IsNullOrEmpty(txtcontactemail.Text))
            {
                if (!Regex.IsMatch(txtcontactemail.Text, Emailpattern))
                {
                    dialogs.Toast("Please enter Proper Email");
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtcontactphone.Text))
            {
                dialogs.Toast("Please enter Contact Number");
                return false;
            }
            if (txtcontactphone.Text.Length < 10)
            {
                dialogs.Toast("Minimum 10 Digits required");
                return false;
            }
            else if (!CheckValidPhone(txtcontactphone.Text))
            {
                dialogs.Toast("Please enter valid mobile number");
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
        private void Jobroledata_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var radiogrp = sender as ListView;
                Jobroles lsow = new Jobroles();
                lsow = (Jobroles)radiogrp.SelectedItem;
                Tjobroletxt = lsow.rolename;
                jobroletext.Text = lsow.rolename;
                jobroleid = lsow.contentid;
                //jobroleurl = lsow.roleurl;
                jobrolelayout.IsVisible = false;
              //  jobrolesmltxt.IsVisible = true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        private void Txtlocation_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtlocation.Text != Tlocationtxt)
                {
                    GetLocationname();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public async void GetLocationname()
        {
            try
            {
                Zipcode = "";
                Tlocationtxt = "";
                var LocationAPI = RestService.For<ILocalJob>(Commonsettings.LocaljobsAPI);
                LocationOverallData response = await LocationAPI.getoverallLocation(txtlocation.Text, "0");
                if (response != null && response.ROW_DATA.Count > 0)
                {
                    cityurl = response.ROW_DATA.Last().newcityurl;
                    OnPropertyChanged();
                    locationlist.IsVisible = true;
                   // locationsmltxt.IsVisible = false;
                    listdata.ItemsSource = response.ROW_DATA;
                }
            }
            catch (Exception e)
            {
            }
        }
        private void LocationCmd(object sender, EventArgs e)
        {
            try
            {
                var radiogrp = sender as ListView;
                Locations lsow = new Locations();
                lsow = (Locations)radiogrp.SelectedItem;
                Tlocationtxt = lsow.city;
                txtlocation.Text = lsow.city;
                cityurl = lsow.newcityurl;
                city = lsow.city;
                country = lsow.country;
                Zipcode = lsow.zipcode.ToString();
                userlat = lsow.latitude.ToString();
                userlong = lsow.longitude.ToString();
                string Country = lsow.country;
                locationlist.IsVisible = false;
                //locationsmltxt.IsVisible = true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        private void Cancel_Profileupdate(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
       // Sendprofiledata update = new Sendprofiledata();
        private async void Update_Profile(object sender, EventArgs e)
        {
            try
            {
                dialogs.ShowLoading("", null);
                if (validation())
                {
                    string name= txtcontactname.Text;
                    string email = txtcontactemail.Text;
                    string phone= txtcontactphone.Text;
                    data.contactperson = name;
                    data.email = email;
                    data.phone = phone;
                    if(countrycode.SelectedItem==null)
                    {
                        data.countrycode = "+1";
                    }
                    else
                    {
                        data.countrycode = countrycode.SelectedItem.ToString();
                    }
                    data.cityurl = cityurl;
                    data.city = city;
                    data.country = country; 
                    data.block = "profile";
                    data.jobrole = Tjobroletxt;
                    data.workauthorization = checkedvisatypevalue;
                    var profileupdata = RestService.For<IJobseeker>(Commonsettings.LocaljobsAPI);
                    var profiledatas = await profileupdata.profilepagecreate(data);
                    if(profiledatas!=null)
                    {
                        //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                        //Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                        Xamarin.Forms.Page page1 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 1];
                        Xamarin.Forms.Page page2 = Navigation.NavigationStack[Navigation.NavigationStack.Count - 2];
                        Navigation.RemovePage(page1);
                        Navigation.RemovePage(page2);
                        await Navigation.PushAsync(new Jobseeker.View.Jobseekerprofile(data));
                    }
                    else
                    {
                        dialogs.Toast("Try again later...");
                    }
                }
                dialogs.HideLoading();
            }
            catch(Exception ex)
            {

            }
        }
        //private void PopupContentTap(object sender, EventArgs e)
        //{
        //    contentlayout.IsVisible = true;
        //}

        //private void ContentViewTap(object sender, EventArgs e)
        //{
        //    contentlayout.IsVisible = false;
        //}

        //private void getvisalistonTap(object sender, EventArgs e)
        //{
        //    getworkauthorization();
        //    contentlayout.IsVisible = true;
        //}
    }
}