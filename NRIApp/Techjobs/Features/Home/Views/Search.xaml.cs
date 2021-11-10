using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.Techjobs.Features.Home.Interfaces;
using NRIApp.Techjobs.Features.Home.Models;
using NRIApp.Techjobs.Features.Home.ViewModels;
using NRIApp.Helpers;

namespace NRIApp.Techjobs.Features.Home.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Search : ContentPage
	{
		public Search ()
		{
			InitializeComponent ();
            List<SearchTags> srch = new List<SearchTags>();
            srch.Add(new SearchTags() { id = "167", title = "Business Analyst", tags = "Business Analyst", url = "business-analyst", resulttype = "courses", header = "IT TRAINING COURSES" });
            srch.Add(new SearchTags() { id = "1", title = "DW / BI", tags = "DW / BI", url = "dw-bi", resulttype = "coursse", header = "IT TRAINING COURSES" });
            srch.Add(new SearchTags() { id = "2", title = "SAP", tags = "SAP", url = "sap", resulttype = "courses", header = "IT TRAINING COURSES" });
            srch.Add(new SearchTags() { id = "13", title = "Quality Assurance (QA)", tags = "Quality Assurance (QA)", url = "quality-assurance-qa", resulttype = "courses", header = "IT TRAINING COURSES" });
            srch.Add(new SearchTags() { id = "369", title = "Workday", tags = "Workday", url = "workday", resulttype = "courses", header = "IT TRAINING COURSES" });

            srch.Add(new SearchTags() { id = "195", title = "BA", tags = "BA", url = "ba", resulttype = "module", header = "IT TRAINING MODULES" });
            srch.Add(new SearchTags() { id = "183", title = "QA", tags = "QA", url = "qa",  resulttype = "module", header = "IT TRAINING MODULES" });
            srch.Add(new SearchTags() { id = "41", title = "Sap Hana", tags = "Sap Hana", url = "sap-hana", resulttype = "module", header = "IT TRAINING MODULES" });
            srch.Add(new SearchTags() { id = "202", title = "Hadoop", tags = "Hadoop", url = "hadoop",  resulttype = "module", header = "IT TRAINING MODULES" });
            srch.Add(new SearchTags() { id = "370", title = "Workday HCM Training", tags = "Workday HCM Training", url = "workday-hcm", resulttype = "module", header = "IT TRAINING MODULES" });

          
            var groupedData = srch.GroupBy(e => e.header)
                  .Select(e => new ObservableGroupCollection<string, SearchTags>(e))
                  .ToList();
            listsearch.ItemsSource = groupedData;
            BindingContext = new Searchviewmodel();
        }

        public async void searchchanged(object sender, TextChangedEventArgs e)
        {
            string keyword = txtsearch.Text;
            if (keyword.Length > 1)
            {
                var nsAPI = RestService.For<IHomeservice>(Commonsettings.TechjobsAPI);
                Searchlist list = await nsAPI.Getsearch(keyword);
                if (list.ROW_DS.Count > 0 && list!=null)
                {

                    foreach (var i in list.ROW_DS)
                    {
                        if (i.resulttype == "modules")
                            i.header = "IT TRAINING MODULES";
                        else if (i.resulttype == "courses")
                            i.header = "IT TRAINING COURSES";

                    }
                    var sorted = from srch in list.ROW_DS group srch by srch.header into searchGroup select new Grouping<string, SearchTags>(searchGroup.Key, searchGroup);

                    listsearch.ItemsSource = sorted;
                    listsearch.IsVisible = true;
                    stacknoresults.IsVisible = false;
                }
                else
                {
                    listsearch.IsVisible = false;
                    stacknoresults.IsVisible = true;
                }
            }
        }

        private void backbtn()
        {
            Navigation.PopModalAsync();
        }
        protected override void OnAppearing()
        {
            txtsearch.Focus();
        }
    }
}