using NRIApp.Helpers;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using NRIApp.Techjobs.Features.LeadForm.Models;
using Plugin.Connectivity;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Techjobs.Features.LeadForm.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Leadform : ContentPage
	{
        bool connected = CrossConnectivity.Current.IsConnected;
        public Leadform (string module, string moduleid)
		{

			InitializeComponent ();
            Title = "Get Quotes";
            Getbizcnt(moduleid);
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
                var vm = new ViewModels.Leadformviewmodel();
                vm.SelModule = module;
                vm.Module = module;
                vm.Moduleid = moduleid;
                vm.bizid = "0";
                this.BindingContext = vm;
        }

        public async void Getbizcnt(string moduleid)
        {
            var nsAPI = RestService.For<ILeadformService>(Commonsettings.TechjobsAPI);
            Bizcnt list = await nsAPI.Getbizcnt(moduleid);
            if (list.ROW_DATA.LastOrDefault().businesscount != "0")
                skipbtn.IsVisible = true;
            else
                skipbtn.IsVisible = false;
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
                    DependencyService.Get<IKeyboardHelper>().HideKeyboard();
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
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    Page prevpage;
        //    int index = Application.Current.MainPage.Navigation.NavigationStack.Count - 1;
        //    prevpage = Application.Current.MainPage.Navigation.NavigationStack[index];
        //    string pagename = prevpage.Title;
        //}


    }
}