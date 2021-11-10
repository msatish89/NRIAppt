using NRIApp.Helpers;
using NRIApp.Techjobs.Features.LeadForm.Models;
using NRIApp.USHome.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using NRIApp.Roommates.Features.Post.Models;
using NRIApp.Rentals.Features.Post.Models;
using Refit;
using NRIApp.USHome.Interfaces;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Acr.UserDialogs;
using System.Runtime.CompilerServices;

namespace NRIApp.USHome.ViewModels
{
   public class HomesearchViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;
        public ICommand Homesearchcommand { get; set; }
        public List<Userlocdetails> locdetails { get; set; }
        public ICommand selectcitycommand { get; set; }
        public ICommand Setmylocation { get; set; }
        public HomesearchViewModel()
        {
            Homesearchcommand = new Command<HomeSearch>(Homesearch);
          
            selectcitycommand = new Command<Cityajax>(Selectcity);
            Setmylocation = new Command(getloc);
        }
        LocalJobs.Features.Listing.Models.LocalJobSenddata listdata = new LocalJobs.Features.Listing.Models.LocalJobSenddata();
        public async void Homesearch(HomeSearch list)
        {
            var currentpage = GetCurrentPage();
            if (list.resulttype == "modules")
            {
                if (list.ltype == "2")
                    await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.Home.Views.Modules(list.id, list.tags));
                else
                    await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.LeadForm.Views.Leadform(list.tags, list.id));
            }
            else if (list.resulttype == "tags")
            {
              //  if (list.tagtype == "3")
              //  {
                    NRIApp.LocalService.Features.ViewModels.LS_ViewModel.primarytagid = list.id;
                    NRIApp.LocalService.Features.ViewModels.LS_ViewModel.primarytagurl = list.titleurl;
                    await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Leadform.LS_Prime(Convert.ToInt32(list.supertagid), list.supertag, list.title, list.id, "", ""));
                    
                //}
                //else
                //{
                //    await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.LS_Leaftypes(list.title, Convert.ToInt32(list.id), 2, Convert.ToInt32(list.tagtype), Convert.ToInt32(list.supertagid), list.supertag));
                //}
            }
            else if (list.resulttype == "roommates")
            {
                var curpage = GetCurrentPage();
                if (list.adid == "" || list.adid == null)
                {
                    Commonsettings.CAdTypeID = 0;
                    CategoryList obj = new CategoryList();
                    obj.supercategoryid = Convert.ToInt32(list.supertagid);
                    obj.primarycategoryid = list.id;
                    //Convert.ToInt32(list.id);
                    obj.secondarycategoryid = 0;
                    obj.categoryname = list.title;
                    obj.maincategory = list.resulttype;
                    await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Catsec(obj));
                }
                else

                    await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.List.Views.DetailRMList(list.adid, Commonsettings.Usercityurl));


            }
            else if (list.resulttype == "rentals")
            {
                var curpage = GetCurrentPage();
                if (list.adid == "" || list.adid == null)
                {
                    Commonsettings.CRTAdTypeID = 0;
                    RTCategoryList obj = new RTCategoryList();
                    obj.supercategoryid = Convert.ToInt32(list.supertagid); ;
                    obj.primarycategoryid = Convert.ToInt32(list.id);
                    obj.secondarycategoryid = 0;
                    obj.categoryname = list.title;
                    obj.maincategory = list.resulttype;

                    await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.Post.Views.Catsec(obj));
                }
                else
                    await curpage.Navigation.PushAsync(new NRIApp.Rentals.Features.List.Views.DetailRentalList(list.adid, Commonsettings.Usercityurl));

            }
            else if (list.resulttype == "event")
            {
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.Events.Features.Detail.Views.Detail(list.id, list.title,list.supertagid));
            }
            else if (list.resulttype == "jobs" && list.search != "localjobs_latlong" && list.search != "localjobs_latest")
            {
                LocalJobs.Features.Listing.Models.LocalJobSenddata listdata = new LocalJobs.Features.Listing.Models.LocalJobSenddata();
                listdata.functionids = Convert.ToInt32(list.id);
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Listing.Views.JobList(listdata));
            }
            else if (list.search == "localjobs_latlong" || list.search == "localjobs_latest")
            {
                //  Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new LocalJobs.Features.Detail.Views.JobDetails(list.adid, list.titleurl, Commonsettings.UserPid));
            }
            else if (list.search == "offered_latlong" || list.search == "wanted_latlong" || list.search == "wanted_latlong" || list.search == "daycare_latest")
            {
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Detail.Views.CareDetail(list.businessid, list.titleurl, ""));
            }
            else if (list.resulttype== "daycare")
            {
                var curpage = GetCurrentPage();
                DayCare.Features.List.Models.DClistinfo listdcdata = new DayCare.Features.List.Models.DClistinfo();
                listdcdata.city = Commonsettings.Usercity;
                listdcdata.cityurl = Commonsettings.Usercityurl;
                listdcdata.newcategoryurl = list.titleurl;
                listdcdata.careprovidertype = list.titleurl;
                await curpage.Navigation.PushAsync(new DayCare.Features.List.Views.DaycareListing(listdcdata));
            }
           
        }

        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }

       

        public async void Selectcity(Cityajax cityl)
        {
            var currentpage = GetCurrentPage();
            Commonsettings.Usercity = cityl.city;
            Commonsettings.Userzipcode = cityl.zipcode;
            Commonsettings.Usercountry = cityl.countrycode;
            Commonsettings.UserLat = Convert.ToDouble(cityl.latitude);
            Commonsettings.UserLong = Convert.ToDouble(cityl.longitude);
            Commonsettings.Userstatecode = cityl.statecode;
            Commonsettings.Usercityurl = cityl.citystatecodeurl;
            Commonsettings.Usermetrourl = cityl.metrourl;
            Commonsettings.Userstate = cityl.statename;

            //await currentpage.Navigation.PopAsync();
            currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count-1]);
        }

        public async void getloc()
        {
            try
            {
                var currentpage = GetCurrentPage();
                dialog.ShowLoading("", null);
                var hasPermission = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (hasPermission != PermissionStatus.Granted)
                {
                    try
                    {
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            var title = $"{Permission.Location} Permission";
                            var question = $"To use this plugin the {Permission.Location} permission is required. Please go into Settings and turn on {Permission.Location} for the app.";
                            var positive = "Settings";
                            var negative = "Maybe Later";
                            var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                            //if (task == null)
                            //    return false;

                            var result = await task;
                            if (result)
                            {
                                CrossPermissions.Current.OpenAppSettings();
                            }
                        }
                        else
                        {
                            var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                            hasPermission = results[Permission.Location];
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message.ToString();
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
                            dialog.HideLoading();
                            await currentpage.Navigation.PopModalAsync();
                        }
                    }
                    else
                    {
                        var title = "Permission";
                        var question = "To continue, turn on device location.";
                        var positive = "Settings";
                        var negative = "Maybe Later";
                        var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                        if (task == null)
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
                            dialog.HideLoading();
                            await currentpage.Navigation.PopModalAsync();
                        }

                        var result = await task;
                        if (result)
                        {
                            DependencyService.Get<IKeyboardHelper>().Gotosettings();
                            dialog.HideLoading();
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
                            dialog.HideLoading();
                            await currentpage.Navigation.PopModalAsync();
                        }  
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
                    dialog.HideLoading();
                    await currentpage.Navigation.PopModalAsync();
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
                // CityTitle = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                dialog.HideLoading();
                await currentpage.Navigation.PopModalAsync();
            }

        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
