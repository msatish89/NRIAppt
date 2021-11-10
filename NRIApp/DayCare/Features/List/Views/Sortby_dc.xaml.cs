using NRIApp.DayCare.Features.List.Models;
using NRIApp.LocalJobs.Features.Listing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.DayCare.Features.List.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Sortby_dc : ContentPage
	{
        List<sortlist> sortlist = new List<sortlist>();
        public static string sortby = "";
        DClistinfo sortlistdata = new DClistinfo();
        public Sortby_dc (DClistinfo listdata)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            sortlistdata = listdata;
            getsortorder(listdata.orderby);
        }
        public void getsortorder(string sortorder)
        {
            sortlist.Add(new sortlist { sortby = "Default", type = "bizrunrate", image = "RadioButtonUnChecked.png" });
            sortlist.Add(new sortlist { sortby = "Distance", type = "distance", image = "RadioButtonUnChecked.png" });
            sortlist.Add(new sortlist { sortby = "Latest", type = "date", image = "RadioButtonUnChecked.png" });
            sortlist.Add(new sortlist { sortby = "Phone verified", type = "mobileverified", image = "RadioButtonUnChecked.png" });
            sortlist.Add(new sortlist { sortby = "Featured First", type = "ordergroup", image = "RadioButtonUnChecked.png" });

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
            try
            {
                sortlistdata.orderby = sortby;
                await Navigation.PopModalAsync();
                await Navigation.PushAsync(new DaycareListing(sortlistdata));
            }
            catch(Exception ex)
            {

            }
        }
    }
}