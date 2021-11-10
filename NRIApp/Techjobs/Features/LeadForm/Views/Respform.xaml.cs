using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRIApp.Helpers;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using NRIApp.Techjobs.Features.LeadForm.Models;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Techjobs.Features.LeadForm.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Respform : ContentPage
	{
		public Respform (string module, string moduleid, string bizid)
		{
			InitializeComponent ();
            Title = "Get Quotes";
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            var vm = new ViewModels.Leadformviewmodel();
            vm.SelModule = module;
            vm.Module = module;
            vm.Moduleid = moduleid;
            vm.bizid = bizid;
            this.BindingContext = vm;
        }


        public async void citysearch(object sender, TextChangedEventArgs e)
        {
            string keyword = txtajxcity.Text;
            if (keyword.Length > 1)
            {
                var nsAPI = RestService.For<ILeadformService>(Commonsettings.TechjobsAPI);
                Cityajaxlist list = await nsAPI.Getcityajax(keyword,"all");
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
                    //listajxcity.ItemsSource = sorted;
                    var vm = new ViewModels.Leadformviewmodel();
                    vm.CityVisible = true;
                    this.BindingContext = vm;
                }
            }

        }

        public void gototc()
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }
    }
}