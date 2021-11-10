using NRIApp.LocalJobs.Features.Listing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Listing.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SortbyJobs : ContentPage
	{
        List<sortlist> sortlist = new List<sortlist>();
        public static string sortby = "";
        LocalJobSenddata sortlistdata = new LocalJobSenddata();
        public SortbyJobs (LocalJobSenddata listdata)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            sortlistdata = listdata;
            getsortorder(listdata.orderby);
		}
        public void getsortorder(string sortorder)
        {
            sortlist.Add(new sortlist { sortby = "Featured", type = "ordergroup", image = "RadioButtonUnChecked.png" });
            sortlist.Add(new sortlist { sortby = "Latest", type = "date", image = "RadioButtonUnChecked.png" });
            sortlist.Add(new sortlist { sortby = "Experience(low to high)", type = "experienceasc", image = "RadioButtonUnChecked.png" });
            sortlist.Add(new sortlist { sortby = "Experience(high to low)", type = "experiencedesc", image = "RadioButtonUnChecked.png" });
            sortlist.Add(new sortlist { sortby = "Distance", type = "distance", image = "RadioButtonUnChecked.png" });
            sortlist.Add(new sortlist { sortby = "Salary (low to high)", type = "salaryasc", image = "RadioButtonUnChecked.png" });
            sortlist.Add(new sortlist { sortby = "Salary (high to low)", type = "salarydesc", image = "RadioButtonUnChecked.png" });

            foreach (var item in sortlist)
            {
                if (!string.IsNullOrEmpty(sortorder))
                {
                    if (sortorder == item.type || sortorder == item.type)
                    {
                        item.image = "RadioButtonChecked.png";
                        sortby = item.type;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }
                }
                else
                {
                    sortorder = "ordergroup";
                    if (sortorder == item.type)
                    {
                        item.image = "RadioButtonChecked.png";
                        sortby = item.type;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }
                }
                sortdata.ItemsSource = sortlist;
            }
        }
        private void Backbtn_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Sortdata_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var radiogrp = sender as ListView;
            sortlist lsow = new sortlist();
            lsow = (sortlist)radiogrp.SelectedItem;

            foreach (var item in sortlist)
            {
                if (item.type == lsow.type)
                {
                    item.image = "RadioButtonChecked.png";
                    sortby = item.type;
                }
                else
                {
                    item.image = "RadioButtonUnChecked.png";
                }
            }

            sortdata.ItemsSource = null;
            sortdata.ItemsSource = sortlist;
        }

        private async void Sortby_Tapped(object sender, EventArgs e)
        {
            sortlistdata.orderby = sortby;
            await Navigation.PopModalAsync();
            await Navigation.PushAsync(new JobList(sortlistdata));
        }
    }
}