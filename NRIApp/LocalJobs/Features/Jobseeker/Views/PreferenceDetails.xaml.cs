using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Listing.Interfaces;
using NRIApp.LocalJobs.Features.Listing.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.LocalJobs.Features.Jobseeker.Interface;
using NRIApp.LocalJobs.Features.Jobseeker.Models;

namespace NRIApp.LocalJobs.Features.Jobseeker.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PreferenceDetails : ContentPage
	{
        IUserDialogs dialogs = UserDialogs.Instance;
        Jobseekers_DATA data = new Jobseekers_DATA();
        public PreferenceDetails (Jobseekers_DATA Sdata)
		{
            data = Sdata;
            Title = "Preferences Details";
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            //locationlist.IsVisible = false;
            if(!string.IsNullOrEmpty(Sdata.facebook))
            {
                facebooktxt.Text = Sdata.facebook;
            }
            if (!string.IsNullOrEmpty(Sdata.twitter))
            {
                twittertxt.Text = Sdata.twitter;
            }
            if (!string.IsNullOrEmpty(Sdata.linkedin))
            {
                linkedintxt.Text = Sdata.linkedin;
            }
            if (!string.IsNullOrEmpty(Sdata.blog))
            {
                blogtxt.Text = Sdata.blog;
            }
            if (Sdata.jobalerts=="monthly")
            {
                monthlyimg.Source = "RadioButtonChecked.png";
                weeklyimg.Source = "RadioButtonUnChecked.png";
                dailyimg.Source = "RadioButtonUnChecked.png";
                alertfrequency = "monthly";
            }
            else if (Sdata.jobalerts == "weekly")
            {
                monthlyimg.Source = "RadioButtonUnChecked.png";
                weeklyimg.Source = "RadioButtonChecked.png";
                dailyimg.Source = "RadioButtonUnChecked.png";
                alertfrequency = "weekly-once";
            }
            else if (Sdata.jobalerts == "daily")
            {
                monthlyimg.Source = "RadioButtonUnChecked.png";
                weeklyimg.Source = "RadioButtonUnChecked.png";
                dailyimg.Source = "RadioButtonChecked.png";
                alertfrequency = "daily";
            }
            else
            {
                monthlyimg.Source = "RadioButtonChecked.png";
                weeklyimg.Source = "RadioButtonUnChecked.png";
                dailyimg.Source = "RadioButtonUnChecked.png";
                alertfrequency = "monthly";
            }
            if (Sdata.hideprofile == "1")
            {
                hideprofileimg.Source = "RadioButtonChecked.png";
                showprofileimg.Source = "RadioButtonUnChecked.png";
                hideshowprofilevalue = "1";
            }
            else 
            {
                hideprofileimg.Source = "RadioButtonUnChecked.png";
                showprofileimg.Source = "RadioButtonChecked.png";
                hideshowprofilevalue = "0";
            }
            if(!string.IsNullOrEmpty(Sdata.jobcity))
            {
                txtlocation.Text = Tlocationtxt = Sdata.jobcity;
            }
            else
            {
                txtlocation.Text = Commonsettings.Usercity+", "+Commonsettings.Userstatecode;
                Tlocationtxt = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                
                cityurl = Commonsettings.Usercityurl;
                city = Commonsettings.Usercity;
                country = Commonsettings.Usercityurl;
                Zipcode = Commonsettings.Userzipcode;
                userlat = Commonsettings.UserLat.ToString();
                userlong = Commonsettings.UserLong.ToString();
            }

            locationlist.IsVisible = false;
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
        private string _alertfrequency = "";
        public string alertfrequency
        {
            get { return _alertfrequency; }
            set { _alertfrequency = value; OnPropertyChanged(nameof(alertfrequency)); }
        }
        private string _hideshowprofilevalue = "";
        public string hideshowprofilevalue
        {
            get { return _hideshowprofilevalue; }
            set { _hideshowprofilevalue = value; OnPropertyChanged(nameof(hideshowprofilevalue)); }
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
                    locationsmltxt.IsVisible = false;
                    listdata.ItemsSource = response.ROW_DATA;
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
                locationsmltxt.IsVisible = true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        private void monthlycommand(object sender, EventArgs e)
        {
            monthlyimg.Source = "RadioButtonChecked.png";
            weeklyimg.Source = "RadioButtonUnChecked.png";
            dailyimg.Source = "RadioButtonUnChecked.png";
            alertfrequency = "monthly";
        }
        private void weeklycommand(object sender, EventArgs e)
        {
            monthlyimg.Source = "RadioButtonUnChecked.png";
            weeklyimg.Source = "RadioButtonChecked.png";
            dailyimg.Source = "RadioButtonUnChecked.png";
            alertfrequency = "weekly-once";
        }
        private void dailycommand(object sender, EventArgs e)
        {
            monthlyimg.Source = "RadioButtonUnChecked.png";
            weeklyimg.Source = "RadioButtonUnChecked.png";
            dailyimg.Source = "RadioButtonChecked.png";
            alertfrequency = "daily";
        }
        private void hideprofile(object sender, EventArgs e)
        {
            hideprofileimg.Source = "RadioButtonChecked.png";
            showprofileimg.Source = "RadioButtonUnChecked.png";
            hideshowprofilevalue = "1";
        }

        private void showprofile(object sender, EventArgs e)
        {
            hideprofileimg.Source = "RadioButtonUnChecked.png";
            showprofileimg.Source = "RadioButtonChecked.png";
            hideshowprofilevalue = "0";
        }
        public bool validation()
        {
            bool result = true;
            if(string.IsNullOrEmpty(Zipcode) && string.IsNullOrEmpty(Tlocationtxt))
            {
                dialogs.Toast("Please select location");
                return false;
            }
            if (string.IsNullOrEmpty(alertfrequency))
            {
                dialogs.Toast("Please select Job alert frequency");
                return false;
            }
            //if (string.IsNullOrEmpty(facebooktxt.Text))
            //{
            //    dialogs.Toast("Enter your Facebook url");
            //    return false;
            //}
            //if (string.IsNullOrEmpty(twittertxt.Text))
            //{
            //    dialogs.Toast("Enter your Twitter handle");
            //    return false;
            //}
            //if (string.IsNullOrEmpty(blogtxt.Text))
            //{
            //    dialogs.Toast("Enter your blog site url");
            //    return false;
            //}
            //if (string.IsNullOrEmpty(linkedintxt.Text))
            //{
            //    dialogs.Toast("Enter your Linkedin url");
            //    return false;
            //}
            if (string.IsNullOrEmpty(hideshowprofilevalue))
            {
                dialogs.Toast("Please select privacy mode");
                return false;
            }
            return result;
        }
        private async void Update_Preference(object sender, EventArgs e)
        {
            try
            {
                dialogs.ShowLoading("", null);
                if (validation())
                {
                    data.block = "Preferences";
                    data.jobcity = city;
                    data.jobcityurl = cityurl;
                    data.jobalerts = alertfrequency;
                    data.facebook = facebooktxt.Text;
                    data.twitter = twittertxt.Text;
                    data.linkedin = linkedintxt.Text;
                    data.blog = blogtxt.Text;
                    data.hideprofile = hideshowprofilevalue;
                    var profileupdata = RestService.For<IJobseeker>(Commonsettings.LocaljobsAPI);
                    var profiledatas = await profileupdata.profilepagecreate(data);
                    if (profiledatas != null)
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
                dialogs.HideLoading();
            }
        }
        private void Cancel_Preferenceupdate(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        
    }
}