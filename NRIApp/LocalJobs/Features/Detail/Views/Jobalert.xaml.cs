using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Detail.Interfaces;
using NRIApp.LocalJobs.Features.Listing.Interfaces;
using NRIApp.LocalJobs.Features.Listing.Models;
using NRIApp.LocalJobs.Features.Posting.Interfaces;
using NRIApp.LocalJobs.Features.Posting.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Detail.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Jobalert : ContentPage
	{
        IUserDialogs dialog = UserDialogs.Instance;
        public Jobalert()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            try
            {
                dialog.ShowLoading("", null);
                Daily.Source = "RadioButtonChecked.png";
                Weekly.Source = "RadioButtonUnChecked.png";
                Monthly.Source = "RadioButtonUnChecked.png";
                Title = "Job Alert";
                alertid = "0";
                jobrolelayout.IsVisible = false;
               // jobrolesmltxt.IsVisible = true;
                locationlist.IsVisible = false;
               // locationsmltxt.IsVisible = true;
                if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                    txtcontactemail.Text = Commonsettings.UserEmail;
                dialog.HideLoading();
            }
            catch(Exception ex)
            {

            }
        }
            
        public static string _alertid = "";
        public string alertid
        {
            get { return _alertid; }
            set { _alertid = value; OnPropertyChanged(nameof(alertid)); }
        }
        public static string _Tjobroletxt = "";
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
       
        private void jobroletxt_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (jobroletext.Text != Tjobroletxt)
                {
                    getjobroleajax();
                }
            }
            catch(Exception ex)
            {

            }
        }
       
        public async void getjobroleajax()
        {
            try
            {
                jobroleid = "";
                var LJAPI = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                Jobroleslist list = await LJAPI.GetJobroleajax(jobroletext.Text);
                if (list.ROW_DATA.Count > 0 && list.ROW_DATA != null)
                {
                    jobroledata.ItemsSource = list.ROW_DATA;
                    jobrolelayout.IsVisible = true;
                    //jobrolesmltxt.IsVisible = false;
                }
            }
            catch (Exception e)
            {
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
                jobroleid = lsow.contentid;
                //jobroleurl = lsow.roleurl;
                jobrolelayout.IsVisible = false;
                //jobrolesmltxt.IsVisible = true;
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
            catch(Exception ex)
            {

            }
        }
        public async void GetLocationname()
        {
            try
            {
                Zipcode = "";
                var LocationAPI = RestService.For<ILocalJob>(Commonsettings.LocaljobsAPI);
                LocationOverallData response = await LocationAPI.getoverallLocation(txtlocation.Text, "0");
                if (response != null && response.ROW_DATA.Count > 0)
                {
                    cityurl = response.ROW_DATA.Last().newcityurl;
                    OnPropertyChanged();
                    locationlist.IsVisible = true;
                    //locationsmltxt.IsVisible = false;
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

        private void DailyCommand(object sender, EventArgs e)
        {
            Daily.Source = "RadioButtonChecked.png";
            Weekly.Source = "RadioButtonUnChecked.png";
            Monthly.Source = "RadioButtonUnChecked.png";
            alertid = "0";
        }

        private void WeeklyCommand(object sender, EventArgs e)
        {
            Daily.Source = "RadioButtonUnChecked.png";
            Weekly.Source = "RadioButtonChecked.png";
            Monthly.Source = "RadioButtonUnChecked.png";
            alertid = "1";
        }

        private void MonthlyCommand(object sender, EventArgs e)
        {
            Daily.Source = "RadioButtonUnChecked.png";
            Weekly.Source = "RadioButtonUnChecked.png";
            Monthly.Source = "RadioButtonChecked.png";
            alertid = "2";
        }
        public bool validation()
        {
            bool result = true;
            if (string.IsNullOrEmpty(jobroleid))
            {
                dialog.Toast("Please enter your jobrole");
                return false;
            }
            if (txtcontactemail.Text.Trim().Length == 0)
            {
                dialog.Toast("Please enter your email");
                return false;
            }
            if (!CheckValidEmail(txtcontactemail.Text))
            {
                dialog.Toast("Please enter valid email id");
                return false;
            }
            if (string.IsNullOrEmpty(txtcontactemail.Text))
            {
                dialog.Toast("Please enter your mailid");
                return false;
            }
            if(string.IsNullOrEmpty(Zipcode))
            {
                dialog.Toast("Please select location");
                return false;
            }
            return result;
        }
  

        bool CheckValidEmail(string inputEmail)
        {
            return Regex.IsMatch(inputEmail, "[\\w\\.-]+@([\\w\\-]+\\.)+[A-Z]{2,4}$", RegexOptions.IgnoreCase);
        }
        Features.Detail.Models.Jobalert_DATA alertdata = new Models.Jobalert_DATA();
        public async void jobalertsubmit(object sender, EventArgs e)
        {
            try
            {
                dialog.ShowLoading("", null);
                if(validation())
                {
                    alertdata.email = txtcontactemail.Text;
                    alertdata.jobrole = jobroletext.Text;
                    alertdata.jobroleid = jobroleid;
                    alertdata.city = city;
                    alertdata.cityurl = cityurl;
                    alertdata.userlong = userlong;
                    alertdata.userlat = userlat;
                    alertdata.dayformat = alertid;
                    var alert = RestService.For<IComapanydata>(Commonsettings.LocaljobsAPI);
                    var alertdatas = await alert.Getjobalert(alertdata);
                    if (alertdatas.resultinformation == "update" )
                    {
                        await Navigation.PopAsync();
                        dialog.Toast("created job alert updated successfully...");
                    }
                    else if(alertdatas.resultinformation== "inserted")
                    {
                        await Navigation.PopAsync();
                        dialog.Toast("created job alert created successfully...");

                    }
                    else
                    {
                        dialog.Toast("Try again later...");
                    }
                }
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
    }
}