using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using NRIApp.Techjobs.Features.LeadForm.Models;
using NRIApp.USHome.Interfaces;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace NRIApp.USHome.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Citysearch : ContentPage
	{
        string country = "all";
        List<Cityajax> testad = new List<Cityajax>();
        private int _lastItemAppearedIdx;
        IUserDialogs dialog = UserDialogs.Instance;
        string cntrycode = NRIApp.Helpers.Commonsettings.Usercountry.ToLower();
        public Citysearch ()
		{
			InitializeComponent ();
            cntrycode = cntrycode.Replace(" ", "");
            if (cntrycode =="ca")
            {
                imgcntry.Source = "CAflagTop.png";
                country = "ca";
            } 
            else
            {
                country = "us";
                imgcntry.Source = "USAflagTop.png";
            }
            List<Cityajax> srch = new List<Cityajax>();
            srch.Add(new Cityajax() {countrycode="US",country= "united states",city= "Austin", statename = "Texas", statecode = "TX",zipcode= "73301",longitude= "-97.7429",latitude= "30.2669",citystatecodeurl= "austin-tx",metrourl= "austin-metro-area",fullcity= "Austin, TX",header="USA" });
            srch.Add(new Cityajax() {countrycode = "US", country = "united states", city = "Chicago", statename = "Illinois", statecode = "IL", zipcode = "60290", longitude = "-87.8849", latitude = "41.96139", citystatecodeurl = "chicago-il", metrourl = "chicago-metro-area", fullcity = "Chicago, IL", header = "USA" });
            srch.Add(new Cityajax() {countrycode = "US", country = "united states", city = "Dallas", statename = "Texas", statecode = "TX", zipcode = "75326", longitude = "-96.8001", latitude = "32.7835", citystatecodeurl = "dallas-tx", metrourl = "dallas-fortworth-area",fullcity = "Dallas, TX", header = "USA" });
            srch.Add(new Cityajax() {countrycode = "US", country = "united states", city = "Jersey City", statename = "New Jersey", statecode = "NJ", zipcode = "7305", longitude = "-74.075359", latitude = "40.692524", citystatecodeurl = "jersey-city-nj", metrourl = "new-jersey-area", fullcity = "Jersey City, NJ", header = "USA" });
            srch.Add(new Cityajax() {countrycode = "US", country = "united states", city = "New York", statename = "New York", statecode = "NY", zipcode = "10026", longitude = "-73.952833", latitude = "40.80405", citystatecodeurl = "new-york-ny", metrourl = "new-york-metro-area", fullcity = "New York, NY", header = "USA" });
            srch.Add(new Cityajax() { countrycode = "US", country = "united states", city = "San Francisco", statename = "California", statecode = "CA", zipcode = "94128", longitude = "-122.379", latitude = "37.62143", citystatecodeurl = "san-francisco-ca", metrourl = "bay-area", fullcity = "San Francisco, CA", header = "USA" });

            srch.Add(new Cityajax() { countrycode = "ca", country = "canada", city = "Brampton",statename= "Ontario", statecode = "ON", zipcode = "L6P 0A1", longitude = "-79.735346", latitude = "43.790546", citystatecodeurl = "brampton-on", metrourl = "toronto-metro-area", fullcity = "Brampton, ON", header = "Canada" });
            srch.Add(new Cityajax() { countrycode = "ca", country = "canada", city = "Calgary", statename = "Alberta", statecode = "AB", zipcode = "T2K 3C9", longitude = "-114.073546", latitude = "51.103236", citystatecodeurl = "calgary-ab", metrourl = "calgary-metro-area", fullcity = "Calgary, AB", header = "Canada" });
            srch.Add(new Cityajax() { countrycode = "ca", country = "canada", city = "Mississauga", statename = "Ontario", statecode = "ON", zipcode = "L5J 2T8", longitude = "-79.6169875", latitude = "43.513046", citystatecodeurl = "mississauga-on", metrourl = "toronto-metro-area", fullcity = "Mississauga, ON", header = "Canada" });
            srch.Add(new Cityajax() { countrycode = "ca", country = "canada", city = "Montreal", statename = "Quebec ", statecode = "QC", zipcode = "H1A 0A1", longitude = "-73.50238", latitude = "45.651381", citystatecodeurl = "montreal-qc", metrourl = "montreal-metro-area", fullcity = "Montreal, QC", header = "Canada" });
            srch.Add(new Cityajax() { countrycode = "ca", country = "canada", city = "Vancouver", statename = "British Columbia", statecode = "BC", zipcode = "V5K 0A1", longitude = "-123.040784", latitude = "49.289956", citystatecodeurl = "vancouver-bc", metrourl = "vancouver-metro-area", fullcity = "Vancouver, BC", header = "Canada" });

            var groupedData = srch.GroupBy(e => e.header)
                 .Select(e => new ObservableGroupCollection<string, Cityajax>(e))
                 .ToList();

            usercitylist.ItemsSource = groupedData;
            // popularcities();
            BindingContext = new NRIApp.USHome.ViewModels.HomesearchViewModel();
        }

       
        public void selectus()
        {
            country = "us";
            imgcntry.Source = "USAflagTop.png";
            usercitylist.ItemsSource = null;
            stackcntry.IsVisible = false;
            stackpop.IsVisible = false;
            txtusercity.Text = "";
            txtusercity.Focus();
           // stackor.IsVisible = false;
        }
        public void selectcanada()
        {
            country = "ca";
            imgcntry.Source = "CAflagTop.png";
            usercitylist.ItemsSource = null;
            stackcntry.IsVisible = false;
            stackpop.IsVisible = false;
            txtusercity.Text = "";
            txtusercity.Focus();
            //stackor.IsVisible = false;
        }
        public void opencountry()
        {
            if (country == "ca")
            {
                imgcntry.Source = "CAflagTop.png";
                lblus.BackgroundColor = Color.FromHex("#ffffff");
                lblcanada.BackgroundColor = Color.FromHex("#009688");
                txtca.TextColor = Color.FromHex("#ffffff");
                txtus.TextColor = Color.FromHex("#565656");
            }
            else
            {
                imgcntry.Source = "USAflagTop.png";
                lblus.BackgroundColor = Color.FromHex("#009688");
                lblcanada.BackgroundColor = Color.FromHex("#ffffff");
                txtca.TextColor = Color.FromHex("#565656");
                txtus.TextColor = Color.FromHex("#ffffff");
            }
            stackcntry.IsVisible = true;
           // stackor.IsVisible = false;
        }
        //public void selectall()
        //{
        //    country = "all";
        //    lblus.BackgroundColor = Color.FromHex("#ffffff");
        //    lblcanada.BackgroundColor = Color.FromHex("#ffffff");
        //    txtca.TextColor = Color.FromHex("#565656");
        //    txtus.TextColor = Color.FromHex("#565656");
        //}
        public async void cityajax()
        {
            string keyword = txtusercity.Text;
            if (keyword.Length > 1)
            {
                var nsAPI = RestService.For<ILeadformService>(Commonsettings.TechjobsAPI);
                Cityajaxlist list = await nsAPI.Getcityajax(keyword, country);
                testad = list.ROW_DS;
                if (list.ROW_DS.Count > 0)
                {

                    foreach (var i in list.ROW_DS)
                    {
                        i.fullcity = i.city + ", " + i.statecode;
                        if (i.country == "united states")
                            i.header = "USA";
                        else if (i.country == "canada")
                            i.header = "Canada";

                    }
                    var sorted = from srch in list.ROW_DS group srch by srch.header into searchGroup select new Grouping<string, Cityajax>(searchGroup.Key, searchGroup);
                    usercitylist.ItemsSource = sorted;
                    usercitylist.IsVisible = true;
                    stackpop.IsVisible = false;
                }
            }
           
        }

        public async void popularcities()
        {
            dialog.ShowLoading("", null);
                var nsAPI = RestService.For<IUSHome>(Commonsettings.TechjobsAPI);
                Cityajaxlist list = await nsAPI.Getpopularcities();
                if (list.ROW_DS.Count > 0)
                {

                    foreach (var i in list.ROW_DS)
                    {
                        i.fullcity = i.city + ", " + i.statecode;
                        if (i.country == "united states")
                            i.header = "USA";
                        else if (i.country == "canada")
                            i.header = "Canada";

                    }
                    var sorted = from srch in list.ROW_DS group srch by srch.header into searchGroup select new Grouping<string, Cityajax>(searchGroup.Key, searchGroup);
                    usercitylist.ItemsSource = sorted;
                usercitylist.IsVisible = true;
                dialog.HideLoading();
                }

        }

        private async void backbtn()
        {
            //await Navigation.PopModalAsync();
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
        }

        protected override void OnAppearing()
        {
          //  DependencyService.Get<IKeyboardHelper>().HideKeyboard();
            // txtusercity.Focus();
        }

        private void Handle_ItemAppearing(object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
        {
           
            var sorted = from srch in testad group srch by srch.header into searchGroup select new Grouping<string, Cityajax>(searchGroup.Key, searchGroup);
            var data = sorted;
            var currentIdx = data.IndexOf(e.Item);

            if (currentIdx > _lastItemAppearedIdx)
               // txtusercity.Unfocus();
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();
            else
              //  txtusercity.Unfocus();
            DependencyService.Get<IKeyboardHelper>().HideKeyboard();

            _lastItemAppearedIdx = data.IndexOf(e.Item);

        }

    }
}