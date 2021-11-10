using NRIApp.Helpers;
using NRIApp.USHome.Interfaces;
using NRIApp.USHome.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRIApp.USHome.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Internals;

namespace NRIApp.USHome.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Homesearch : ContentPage
	{
        public static List<HomeSearch> listings { get; set; }
        private int _lastItemAppearedIdx;
        public Homesearch ()
		{
			InitializeComponent ();
            lblcity.Text = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
            List<HomeSearch> srch = new List<HomeSearch>();
            srch.Add(new HomeSearch() { id = "195", tags = "BA", title="BA", titleurl = "ba",tagtype="0",ltype="3", resulttype = "modules",header= "IT TRAINING" });
            srch.Add(new HomeSearch() { id = "183", tags = "QA", title = "QA", titleurl = "qa", tagtype = "0", ltype = "3", resulttype = "modules", header = "IT TRAINING" });
            srch.Add(new HomeSearch() { id = "41", tags = "Sap Hana", title = "Sap Hana", titleurl = "sap-hana", tagtype = "0", ltype = "3", resulttype = "modules", header = "IT TRAINING" });
            srch.Add(new HomeSearch() { id = "202", tags = "Hadoop", title = "Hadoop", titleurl = "hadoop", tagtype = "0", ltype = "3", resulttype = "modules", header = "IT TRAINING" });
            srch.Add(new HomeSearch() { id = "370", tags = "Workday HCM", title = "Workday HCM", titleurl = "workday-hcm", tagtype = "0", ltype = "3", resulttype = "modules", header = "IT TRAINING" });

            srch.Add(new HomeSearch() { id = "19", title = "Astrologers", titleurl = "astrologers", resulttype = "tags", tagtype = "2", supertag = "Wedding & Events", supertagid = "16", header = "LOCAL SERVICES" });
            srch.Add(new HomeSearch() { id = "20", title = "Beautician Services", titleurl = "beautician", resulttype = "tags", tagtype = "2", supertag = "Wedding & Events", supertagid = "16", header = "LOCAL SERVICES" });
            srch.Add(new HomeSearch() { id = "7", title = "Dance Classes", titleurl = "dance-classes", resulttype = "tags", tagtype = "2", supertag = "Lessons/Tuitions", supertagid = "3", header = "LOCAL SERVICES" });
            srch.Add(new HomeSearch() { id = "17", title = "DJ Services", titleurl = "dj-service", resulttype = "tags", tagtype = "2", supertag = "Wedding & Events", supertagid = "16", header = "LOCAL SERVICES" });
            srch.Add(new HomeSearch() { id = "671", title = "Realtor", titleurl = "real-estate-agents", resulttype = "tags", tagtype = "2", supertag = "Real Estate Services", supertagid = "576", header = "LOCAL SERVICES" });

            srch.Add(new HomeSearch() { id = "7", title = "Single Room", titleurl = "single", resulttype = "roommates",supertagid="2", header = "ROOMMATES" });
            srch.Add(new HomeSearch() { id = "6", title = "Shared Room", titleurl = "shared", resulttype = "roommates", supertagid = "2", header = "ROOMMATES" });
            srch.Add(new HomeSearch() { id = "8", title = "Paying Guest", titleurl = "paying-guest", resulttype = "roommates", supertagid = "2", header = "ROOMMATES" });

            srch.Add(new HomeSearch() { id = "29", title = "Apartment", titleurl = "apartment", resulttype = "rentals", supertagid = "4", header = "RENTALS" });
            srch.Add(new HomeSearch() { id = "28", title = "Single Family Home", titleurl = "single-family-home", resulttype = "rentals", supertagid = "4", header = "RENTALS" });
            srch.Add(new HomeSearch() { id = "30", title = "Condo", titleurl = "condo", resulttype = "rentals", supertagid = "4", header = "RENTALS" });

            srch.Add(new HomeSearch() { id = "84", title = "IT Software", titleurl = "it-software", resulttype = "jobs", header = "JOBS" });
            srch.Add(new HomeSearch() { id = "119", title = "Administration", titleurl = "administration", resulttype = "jobs", header = "JOBS" });
            srch.Add(new HomeSearch() { id = "58", title = "Accounting", titleurl = "accounting", resulttype = "jobs", header = "JOBS" });
            srch.Add(new HomeSearch() { id = "65", title = "Sales & Marketing", titleurl = "sales-marketing", resulttype = "jobs", header = "JOBS" });
            srch.Add(new HomeSearch() { id = "3399", title = "Cyber Security", titleurl = "cyber-security", resulttype = "jobs", header = "JOBS" });

            //daycare
            srch.Add(new HomeSearch() { id = "", title = "Child Care", titleurl = "child-care", resulttype = "daycare", header = "DAY CARE" });
            srch.Add(new HomeSearch() { id = "", title = "Nanny/Babsitter", titleurl = "nanny-babysitter", resulttype = "daycare", header = "DAY CARE" });
            srch.Add(new HomeSearch() { id = "", title = "Care Center", titleurl = "care-center", resulttype = "daycare", header = "DAY CARE" });
            srch.Add(new HomeSearch() { id = "", title = "Cook", titleurl = "cook", resulttype = "daycare", header = "DAY CARE" });
            srch.Add(new HomeSearch() { id = "", title = "Housekeeper", titleurl = "cyber-security", resulttype = "daycare", header = "DAY CARE" });
            srch.Add(new HomeSearch() { id = "", title = "Nanny/Babysitter Jobs", titleurl = "cyber-security", resulttype = "daycare", header = "DAY CARE" });
            srch.Add(new HomeSearch() { id = "", title = "Care Corner", titleurl = "cyber-security", resulttype = "daycare", header = "DAY CARE" });



            var groupedData =srch.GroupBy(e => e.header)
                   .Select(e => new ObservableGroupCollection<string, HomeSearch>(e))
                   .ToList();
            homesrchlistview.ItemsSource = groupedData;
            this.BindingContext = new HomesearchViewModel();
           

        }
        public async void homesearch(object sender, TextChangedEventArgs e)
        {
           try
            {
                string keyword = homesrch.Text;
                if (keyword.Length > 1)
                {
                    var nsAPI = RestService.For<IUSHome>(Commonsettings.TechjobsAPI);
                    //var nsAPI = RestService.For<IUSHome>(Commonsettings.DaycareAPI);
                    HomeSearchlist list = await nsAPI.GetHomesearch(keyword, Commonsettings.Usercityurl, Commonsettings.UserLat, Commonsettings.UserLong);
                    listings = list.ROW_DS;
                    if (list.ROW_DS.Count > 0 && list != null)
                    {

                        foreach (var i in list.ROW_DS)
                        {
                            i.stackdetails = false;
                            if (i.resulttype == "modules")
                                i.header = "IT TRAINING";
                            else if (i.resulttype == "tags")
                                i.header = "LOCAL SERVICES";
                            else if (i.resulttype == "roommates")
                            {
                                i.header = "ROOMMATES";
                                i.stackdetails = true;
                                i.price = i.price1;
                                i.adtype = i.adtype + " , " + i.roomtype + " ,"+" $" + Math.Round(Convert.ToDecimal(i.price)) + " ," + i.cityname + " , " + i.statecode;
                            }
                            else if (i.resulttype == "jobs")
                            {
                                i.header = "Jobs";
                                i.stackdetails = true;
                                i.adtype = i.adtype + " , " + i.jobrole + " in " + i.cityname + " , " + i.statecode;
                            }
                            else if (i.resulttype == "rentals")
                            {
                                i.header = "RENTALS";
                                i.stackdetails = true;
                                if (i.adtype.ToLower() == "offered")
                                    i.price = i.price1;
                                else
                                    i.price = i.price2;

                                i.adtype = i.adtype + " , " + i.roomtype+" ," + " $" + Math.Round(Convert.ToDecimal(i.price)) + " ," + i.cityname + " , " + i.statecode;
                            }
                            else if (i.resulttype == "event")
                            {
                                i.header = "EVENTS";
                            }
                            else if (i.resulttype == "daycare")
                            {
                                i.header = "RELATED DAY CARE SERVICES";
                                i.stackdetails = false;
                            }
                            else if(i.search== "offered_latlong" || i.search== "wanted_latlong")
                            {
                                i.header = "DAY CARE";
                                i.stackdetails = true;
                                i.adtype = i.secondarycategoryvalue + " , " + i.tertiarycategoryvalue + " in " + i.city + " , " + i.statecode;
                            }
                        }
                        var sorted = from srch in list.ROW_DS group srch by srch.header into searchGroup select new Grouping<string, HomeSearch>(searchGroup.Key, searchGroup);
                        homesrchlistview.ItemsSource = sorted;
                        stacknoresult.IsVisible = false;
                        homesrchlistview.IsVisible = true;
                    }
                    else
                    {
                        stacknoresult.IsVisible = true;
                        homesrchlistview.IsVisible = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private async void backbtn()
        {
            OnBackButtonPressed();
            await Task.Delay(300);
            await Navigation.PopAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        protected override void OnAppearing()
        {
            lblcity.Text = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
            //  homesrch.Focus();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NRIApp.USHome.Views.Citysearch());
        }

       
        private void Handle_ItemAppearing(object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
        {
            if (listings != null)
            {
                var data = listings;
                var currentIdx = data.IndexOf(e.Item);

                if (currentIdx > _lastItemAppearedIdx)
                    homesrch.Focus();
                else
                    homesrch.Unfocus();

                    _lastItemAppearedIdx = data.IndexOf(e.Item);
            }


        }

      
    }
}