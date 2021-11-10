using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using System.Linq;
using System.Runtime.CompilerServices;
using NRIApp.USHome.Models;
using NRIApp.USHome.Interfaces;
using Refit;
using NRIApp.Helpers;
using System.Windows.Input;
using NRIApp.Techjobs.Features.LeadForm.Models;
using Acr.UserDialogs;
using Plugin.Connectivity;
using NRIApp.Roommates.Features.Post.Models;
using NRIApp.Rentals.Features.Post.Models;
using Plugin.Permissions;
using Plugin.Geolocator;
using Plugin.Permissions.Abstractions;
using Plugin.Geolocator.Abstractions;
using System.Net;
using System.IO;
using System.Xml;

namespace NRIApp.USHome.ViewModels
{
    public class USHomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<TrainingCourse> GetTechjobslist { get; set; }
        public List<LSlist> GetLslist { get; set; }
        public List<Userlocdetails> locdetails { get; set; }
        // public List<Masteritems> Masterlist { get; set; }
        // public ICommand mastermenucommand { get; set; }

        // public ICommand Backbtncommand { get; set; }
        public ICommand usercitychangedcommand { get; set; }
        public ICommand Ittrainingcommand { get; set; }
        public ICommand lscommand { get; set; }
        public ICommand Homesearchcommand { get; set; }
        public ICommand OpenHomesearchcommand { get; set; }
        IUserDialogs dialog = UserDialogs.Instance;

        public Command RTViewAllCmd { get; set; }

        public Command  SingleFamilyCmd { get; set; }

        public Command ApartmentCmd { get; set; }

        public Command CondoCmd { get; set; }

        public Command TownHouseCmd { get; set; }

        public Command HomesCmd { get; set; }

        public Command HousesCmd { get; set; }

        public Command HostelsCmd { get; set; }

        public Command HotelsCmd { get; set; }

        public Command BasementApartmentCmd { get; set; }

        public Command ParkingGarageCmd { get; set; }

        public Command RMViewAllCmd { get; set; }

        public Command SingleCmd { get; set; }

        public Command SharedCmd { get; set; }

        public Command PayingCmd { get; set; }

        public Command RoommatesCmd { get; set; }

        public Command RentalsCmd { get; set; }

        public Command DaycareCmd { get; set; }

        private string _cityTitle = Commonsettings.Usercity;
        public string CityTitle
        {
            get { return _cityTitle; }
            set { _cityTitle = value; OnPropertyChanged(); }
        }
        private string _fbsvg = "resource://NRIApp.Resources.facebooknew.svg";
        public string fbsvg
        {
            get { return _fbsvg; }
            set { _fbsvg = value; }
        }
        public USHomeViewModel()
        {
           // Getlocation();
            Techjoblists();
           
            getloc();

            Setxmlvalues();
            Ittrainingcommand = new Command<TrainingCourse>(clickittraining);
            usercitychangedcommand = new Command(usercitychanged);
            OpenHomesearchcommand = new Command(OpenHomesearch);
            lscommand = new Command<LSlist>(lscommandclcik);
            RTViewAllCmd = new Command(ClickRTViewAllCmd);
            SingleFamilyCmd = new Command(ClickSingleFamilyCmd);
            ApartmentCmd = new Command(ClickApartmentCmd);
            CondoCmd = new Command(ClickCondoCmd);
            TownHouseCmd = new Command(ClickTownHouseCmd);
            HomesCmd = new Command(ClickHomesCmd);
            HousesCmd = new Command(ClickHousesCmd);
            HostelsCmd = new Command(ClickHostelsCmd);
            HotelsCmd = new Command(ClickHotelsCmd);
            BasementApartmentCmd = new Command(ClickBasementApartmentCmd);
            ParkingGarageCmd = new Command(ClickParkingGarageCmd);

            RMViewAllCmd = new Command(ClickRMViewAllCmd);
            SingleCmd = new Command(ClickSingleCmd);
            SharedCmd = new Command(ClickSharedCmd);
            PayingCmd = new Command(ClickPayingCmd);

            RoommatesCmd = new Command(ClickRoommatesCmd);
            RentalsCmd = new Command(ClickRentalsCmd);
            DaycareCmd = new Command(ClickDaycareCmd);
            
            if (Commonsettings.Usercity != null && Commonsettings.Usercity != "")
                CityTitle = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
        }

        public void Setxmlvalues()
        {
            try
            {
                string readerdata = string.Empty;
                Uri uri = new Uri("https://techjobs.sulekha.com/mobileapp/mobileapp.xml");
                WebRequest request = HttpWebRequest.Create(uri);
                WebResponse response = request.GetResponse();
                StreamReader streader = new StreamReader(response.GetResponseStream());
                readerdata = streader.ReadToEnd();
                Dictionary<string, string> keyValues = new Dictionary<string, string>();
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(readerdata);
                XmlNodeList resources = xml.SelectNodes("AppSettings/add");
                foreach (XmlNode node in resources)
                {
                    keyValues.Add(node.Attributes["key"].Value.ToString(), node.Attributes["value"].Value.ToString());
                }
                var blobkey = keyValues["StorageConnectionString"].ToString();
                Commonsettings.AzureBlobStorgekey = blobkey;

            }
            catch(Exception ex)
            {

            }
        }


            public async void clickittraining(TrainingCourse tr)
            {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.Home.Views.Modules(tr.tagid.ToString(), tr.tag));
            }
        public async void OpenHomesearch()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.USHome.Views.Homesearch());
        }
       
        public async void Menuclick(Masteritems menu)
        {
            var currentpage = GetCurrentPage();
            if (menu.id == 1)
                await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.Home.Views.Courses());
        }

        public async void Techjoblists()
        {
           //   dialog.ShowLoading("",null);
              var ApiC = RestService.For<IUSHome>(Commonsettings.USHomeAPI);
            try
            {
            Techjobslist listofcourse = await ApiC.GetItechjobs();
            OnPropertyChanged(nameof(GetTechjobslist));
            GetTechjobslist = listofcourse.gettrainingcourse;
            }
            catch (Exception es)
            {

            } // dialog.HideLoading();
        }

        public async void LSlists()
        {
            var ApiC = RestService.For<IUSHome>(Commonsettings.USHomeAPI);
            if (Commonsettings.Usercityurl == null || Commonsettings.Usercityurl == "")
                Commonsettings.Usercityurl = "new-york-ny";
            try
            {
                LSdata list = await ApiC.GetLslist(Commonsettings.Usercityurl);
                OnPropertyChanged(nameof(GetLslist));
                GetLslist = list.ROW_DATA;
                dialog.HideLoading();
            }catch(Exception es)
            {
                dialog.HideLoading();
            }
        }

        public async void lscommandclcik(LSlist list)
        {
            var currentpage = GetCurrentPage();
            if (list.nextlevelcount !="0")
            {
                NRIApp.LocalService.Features.ViewModels.LS_ViewModel.primarytagid = NRIApp.LocalService.Features.ViewModels.LS_ViewModel.searchtagid = NRIApp.LocalService.Features.ViewModels.LS_ViewModel.stagid = Convert.ToString(list.tagid);
                NRIApp.LocalService.Features.ViewModels.LS_ViewModel.primarytag = NRIApp.LocalService.Features.ViewModels.LS_ViewModel.searchtag = list.primarytag;
                await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.LS_Leaftypes(list.primarytag, Convert.ToInt16(list.tagid), 1, 2, Convert.ToInt16(list.supertagid), list.supertag));
            }
            else
            {
                await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Leadform.LS_LeadForm(Convert.ToInt32(list.supertagid), list.supertag, list.primarytag, list.tagid, "", ""));
            }
            
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
        public string _citystatecode = "";
        public string CityStatecode
        {
            get { return _citystatecode; }
            set
            {
                _citystatecode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CityStatecode)));

            }
        }
      

        public async void usercitychanged()
        {
           // dialog.ShowLoading("");
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.USHome.Views.Citysearch());
        }

        public async void getprevpage()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PopAsync();
        }
        public async void ClickRTViewAllCmd()
        {
            var curpage = GetCurrentPage();
           await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.offerwant(rtcatlistt));
        }

      public  async void ClickSingleFamilyCmd()
        {
            try
            {
                Commonsettings.CRTAdTypeID = 0;
                RTCategoryList obj = new RTCategoryList();
                obj.supercategoryid = 4;
                obj.primarycategoryid = 28;
                obj.secondarycategoryid = 0;
                obj.categoryname = "Single Family Home";
                obj.maincategory = "rentals";
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Catsec(obj));
            }
            catch (Exception e) { }
        }
        public async void ClickApartmentCmd()
        {
            try
            {
                Commonsettings.CRTAdTypeID = 0;
                RTCategoryList obj = new RTCategoryList();
                obj.supercategoryid = 4;
                obj.primarycategoryid = 29;
                obj.secondarycategoryid = 0;
                obj.categoryname = "Apartment";
                obj.maincategory = "rentals";
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Catsec(obj));
            }
            catch (Exception e) { }
        }
        public async void ClickCondoCmd()
        {
            try
            {
                Commonsettings.CRTAdTypeID = 0;
                RTCategoryList obj = new RTCategoryList();
                obj.supercategoryid = 4;
                obj.primarycategoryid = 30;
                obj.secondarycategoryid = 0;
                obj.categoryname = "Condo";
                obj.maincategory = "rentals";
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Catsec(obj));
            }
            catch (Exception e) { }
        }

        
        public async void ClickTownHouseCmd()
        {
            try
            {
                Commonsettings.CRTAdTypeID = 0;
                RTCategoryList obj = new RTCategoryList();
                obj.supercategoryid = 4;
                obj.primarycategoryid = 31;
                obj.secondarycategoryid = 0;
                obj.categoryname = "Town house";
                obj.maincategory = "rentals";
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Catsec(obj));
            }
            catch (Exception e) { }
        }
        public async void ClickHomesCmd()
        {
            try
            {
                Commonsettings.CRTAdTypeID = 0;
                RTCategoryList obj = new RTCategoryList();
                obj.supercategoryid = 4;
                obj.primarycategoryid = 38;
                obj.secondarycategoryid = 0;
                obj.categoryname = "Homes";
                obj.maincategory = "rentals";
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Catsec(obj));
            }
            catch (Exception e) { }
        }
        
        public async void ClickHousesCmd()
        {
            try
            {
                Commonsettings.CRTAdTypeID = 0;
                RTCategoryList obj = new RTCategoryList();
                obj.supercategoryid = 4;
                obj.primarycategoryid = 39;
                obj.secondarycategoryid = 0;
                obj.categoryname = "Houses";
                obj.maincategory = "rentals";
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Catsec(obj));
            }
            catch (Exception e) { }
        }
        public async void ClickHostelsCmd()
        {
            try
            {
                Commonsettings.CRTAdTypeID = 0;
                RTCategoryList obj = new RTCategoryList();
                obj.supercategoryid = 4;
                obj.primarycategoryid = 40;
                obj.secondarycategoryid = 0;
                obj.categoryname = "Hostels";
                obj.maincategory = "rentals";
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Catsec(obj));
            }
            catch (Exception e) { }
        }
        public async void ClickHotelsCmd()
        {
            try
            {
                Commonsettings.CRTAdTypeID = 0;
                RTCategoryList obj = new RTCategoryList();
                obj.supercategoryid = 4;
                obj.primarycategoryid = 41;
                obj.secondarycategoryid = 0;
                obj.categoryname = "Hotels";
                obj.maincategory = "rentals";
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Catsec(obj));
            }
            catch (Exception e) { }
        }
        public async void ClickBasementApartmentCmd()
        {
            try
            {
                Commonsettings.CRTAdTypeID = 0;
                RTCategoryList obj = new RTCategoryList();
                obj.supercategoryid = 4;
                obj.primarycategoryid = 42;
                obj.secondarycategoryid = 0;
                obj.categoryname = "Basement Apartment";
                obj.maincategory = "rentals";
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Catsec(obj));
            }
            catch (Exception e) { }
        }
        public async void ClickParkingGarageCmd()
        {
            try
            {
                Commonsettings.CRTAdTypeID = 0;
                RTCategoryList obj = new RTCategoryList();
                obj.supercategoryid = 4;
                obj.primarycategoryid = 43;
                obj.secondarycategoryid = 0;
                obj.categoryname = "Parking Garage";
                obj.maincategory = "rentals";
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Catsec(obj));
            }
            catch (Exception e) { }
        }
        Roommates.Features.Post.Models.CategoryList catlist = new CategoryList();
        public async void ClickRMViewAllCmd()
        {
            var curpage = GetCurrentPage();
            await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.offerwant(catlist));
        }


        public async void ClickSingleCmd()
        {
            try
            {
                Commonsettings.CAdTypeID = 0;
                CategoryList obj = new CategoryList();
                obj.supercategoryid = 5;
                obj.primarycategoryid = "7";
                obj.secondarycategoryid = 0;
                obj.categoryname = "Single Room";
                obj.maincategory = "roommates";

                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Catsec(obj));
            }
            catch (Exception e) { }
        }

        public async void ClickSharedCmd()
        {
            try
            {
                Commonsettings.CAdTypeID = 0;
                CategoryList obj = new CategoryList();
                obj.supercategoryid = 5;
                obj.primarycategoryid = "6";
                obj.secondarycategoryid = 0;
                obj.categoryname = "Shared Room";
                obj.maincategory = "roommates";

                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Catsec(obj));
            }
            catch (Exception e) { }
        }

        public async void ClickPayingCmd()
        {
            try
            {
                Commonsettings.CAdTypeID = 0;
                CategoryList obj = new CategoryList();
                obj.supercategoryid = 5;
                obj.primarycategoryid = "8";
                obj.secondarycategoryid = 0;
                obj.categoryname = "Paying Guest";
                obj.maincategory = "roommates";

                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Catsec(obj));
            }
            catch (Exception e) { }
        }
        CategoryList catlistt = new CategoryList(); 
        public async void ClickRoommatesCmd()
        {
            try
            {
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.offerwant(catlistt));
            }
            catch (Exception e) { }
        }
        RTCategoryList rtcatlistt = new RTCategoryList();
        public async void ClickRentalsCmd()
        {
            try
            {
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.offerwant(rtcatlistt));
            }
            catch (Exception e) { }
        }
        NRIApp.DayCare.Features.Post.Models.DC_CategoryList dccatlist = new DayCare.Features.Post.Models.DC_CategoryList();
        public async void ClickDaycareCmd()
        {
            try
            {
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Catfist(dccatlist));
            }
            catch (Exception e) { }
        }

        public async void getloc()
        {
            try
            {
                dialog.ShowLoading("", null);
                var hasPermission = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (hasPermission != PermissionStatus.Granted)
                {
                    try
                    {
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                            hasPermission = results[Permission.Location];
                        }
                        //if (Device.RuntimePlatform == Device.iOS)
                        //{
                        //    var title = $"{Permission.Location} Permission";
                        //    var question = $"To use this plugin the {Permission.Location} permission is required. Please go into Settings and turn on {Permission.Location} for the app.";
                        //    var positive = "Settings";
                        //    var negative = "Maybe Later";
                        //    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                        //    //if (task == null)
                        //    //    return false;

                        //    var result = await task;
                        //    if (result)
                        //    {
                        //        CrossPermissions.Current.OpenAppSettings();
                        //    }
                        //}
                        //else
                        //{
                        //    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                        //    hasPermission = results[Permission.Location];
                        //}

                    }
                    catch (Exception ex)
                    {

                    }
                }
                if (hasPermission == PermissionStatus.Granted)
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;
                    if (locator.IsGeolocationEnabled)
                    {
                        Position position = await locator.GetPositionAsync(TimeSpan.FromSeconds(5));
                        if (position != null)
                        {
                            Commonsettings.UserLat = position.Latitude;
                            Commonsettings.UserLong = position.Longitude;

                              Getlocation();
                        }
                        else
                        {
                            Commonsettings.Usercity = "New York";
                            Commonsettings.Userzipcode = "10026";
                            Commonsettings.Usercountry = "US";
                            Commonsettings.UserLat = Convert.ToDouble("40.80405");
                            Commonsettings.UserLong = Convert.ToDouble("-73.952833");
                            Commonsettings.Userstatecode = "NY";
                            Commonsettings.Usercityurl = "new-york-ny";
                            Commonsettings.Usermetrourl = "new-york-metro-area";
                            Commonsettings.Userstate = "New York";
                        }
                    }
                    else
                    {
                        Commonsettings.Usercity = "New York";
                        Commonsettings.Userzipcode = "10026";
                        Commonsettings.Usercountry = "US";
                        Commonsettings.UserLat = Convert.ToDouble("40.80405");
                        Commonsettings.UserLong = Convert.ToDouble("-73.952833");
                        Commonsettings.Userstatecode = "NY";
                        Commonsettings.Usercityurl = "new-york-ny";
                        Commonsettings.Usermetrourl = "new-york-metro-area";
                        Commonsettings.Userstate = "New York";
                        LSlists();
                       // dialog.HideLoading();
                    }
                }
                else
                {
                    Commonsettings.Usercity = "New York";
                    Commonsettings.Userzipcode = "10026";
                    Commonsettings.Usercountry = "US";
                    Commonsettings.UserLat = Convert.ToDouble("40.80405");
                    Commonsettings.UserLong = Convert.ToDouble("-73.952833");
                    Commonsettings.Userstatecode = "NY";
                    Commonsettings.Usercityurl = "new-york-ny";
                    Commonsettings.Usermetrourl = "new-york-metro-area";
                    Commonsettings.Userstate = "New York";
                    LSlists();
                   // dialog.HideLoading();
                }


            }
            catch (Exception e)
            {
                string Error = e.Message + e.StackTrace;
                dialog.HideLoading();
            }
           
        }

        public async void Getlocation()
        {
            try
            {
                var currentpage = GetCurrentPage();
                var ApiC = RestService.For<IUSHome>(Commonsettings.TechjobsAPI);
                Userloc loc = await ApiC.Getuserloc(Commonsettings.UserLat, Commonsettings.UserLong);
                OnPropertyChanged(nameof(locdetails));
                locdetails = loc.ROW_DATA;
                if (locdetails != null)
                {
                    Commonsettings.Usercity = locdetails.First().city;
                    Commonsettings.Userzipcode = locdetails.First().zipcode;
                    Commonsettings.Usercountry = locdetails.First().countrycode;
                    Commonsettings.UserLat = Convert.ToDouble(locdetails.First().latitude);
                    Commonsettings.UserLong = Convert.ToDouble(locdetails.First().longitude);
                    Commonsettings.Userstatecode = locdetails.First().statecode;
                    Commonsettings.Usercityurl = locdetails.First().cityurl;
                    Commonsettings.Usermetrourl = locdetails.First().metrourl;
                    Commonsettings.Userstate = locdetails.First().statename;
                    Label mylbl = currentpage.FindByName<Label>("cityname");
                    mylbl.Text = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                    // CityTitle = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                    LSlists();
                    //dialog.HideLoading();
                }
            }catch(Exception ee)
            {

            }

        }

       

    }
}
