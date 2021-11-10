using NRIApp.Rentals.Features.Post.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Rentals.Features.List.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sortby_RT : ContentPage
    {
        private string _cityurl;
        public static string orderby = "";
        public string Cityurl
        {
            get { return _cityurl; }
            set { _cityurl = value; OnPropertyChanged(); }
        }
        List<sortlist> sortlist = new List<sortlist>();
        RTResponse sortres = new RTResponse();

        public Sortby_RT(RTResponse res, string cityurl)
        {
            InitializeComponent();
            Getsortorder(res.orderby);
            res.Newcityurl = cityurl;
            Cityurl = cityurl;
            sortres.primarycategoryvalue = res.primarycategoryvalue;
            sortres.usertype = res.usertype;
            sortres.stayperiod = res.stayperiod;
            // sortres.petpolicy = res.petpolicy;
            sortres.isveg = res.isveg;
            sortres.furnishing = res.furnishing;
            sortres.amenities = res.amenities;
            sortres.distancefrm = res.distancefrm;
            sortres.distanceto = res.distanceto;
            sortres.pricefrom = res.pricefrom;
            sortres.ExpRent = res.ExpRent;
        }
        public void Getsortorder(string sortorder)
        {
            sortlist.Add(new sortlist { oderlist = "Latest", type = "date", image = "RadioButtonUnChecked.png" });
            sortlist.Add(new sortlist { oderlist = "Featured", type = "ordergroup", image = "RadioButtonUnChecked.png" });
            sortlist.Add(new sortlist { oderlist = "Distance", type = "distance", image = "RadioButtonUnChecked.png" });
            sortlist.Add(new sortlist { oderlist = "Price(low to high)", type = "priceasc", image = "RadioButtonUnChecked.png" });
            sortlist.Add(new sortlist { oderlist = "Price(high to low)", type = "pricedesc", image = "RadioButtonUnChecked.png" });

            sortlist.Add(new sortlist { oderlist = "Open house", type = "openhouse", image = "RadioButtonUnChecked.png" });
            sortlist.Add(new sortlist { oderlist = "Photos", type = "photos", image = "RadioButtonUnChecked.png" });
            foreach (var item in sortlist)
            {
                if (!string.IsNullOrEmpty(sortorder))
                {
                    if (sortorder == item.type || sortorder == item.type)
                    {
                        item.image = "RadioButtonChecked.png";
                        Sortby_RT.orderby = item.type;
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
                        Sortby_RT.orderby = item.type;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }

                }
                sortdata.ItemsSource = sortlist;
            }
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
                    Sortby_RT.orderby = item.type;
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
            sortres.orderby = orderby;
            if (!string.IsNullOrEmpty(Filter_RT.checkedcategoryvalue))
                sortres.primarycategoryvalue = Filter_RT.checkedcategoryvalue;
            if (!string.IsNullOrEmpty(Filter_RT.noofbed))
                sortres.secondarycategoryvalue = Filter_RT.noofbed;
            if (!string.IsNullOrEmpty(Filter_RT.tenant))
                sortres.usertype = Filter_RT.tenant;
            if (!string.IsNullOrEmpty(Filter_RT.leaseperiod))
                sortres.stayperiod = Filter_RT.leaseperiod;
            if (!string.IsNullOrEmpty(Filter_RT.petpreference))
                sortres.petpolicy = Filter_RT.petpreference;
            if (!string.IsNullOrEmpty(Filter_RT.vegpreference))
                sortres.isveg = Filter_RT.vegpreference;
            if (!string.IsNullOrEmpty(Filter_RT.furnish))
                sortres.furnishing = Filter_RT.furnish;
            if (!string.IsNullOrEmpty(Filter_RT.checkedamenitycategoryvalue))
                sortres.amenities = Filter_RT.checkedamenitycategoryvalue;
            if (!string.IsNullOrEmpty(Filter_RT.noofaccomodate))
                sortres.accommodates = Filter_RT.noofaccomodate;
            if (!string.IsNullOrEmpty(Filter_RT.noofbath))
                sortres.noofbaths = Filter_RT.noofbath;
            if (!string.IsNullOrEmpty(Filter_RT.noofbed))
                sortres.secondarycategoryvalue = Filter_RT.noofbed;
            if (!string.IsNullOrEmpty(Filter_RT.pricefrm))
                sortres.pricefrom = Filter_RT.pricefrm;
            if (!string.IsNullOrEmpty(Filter_RT.priceto))
                sortres.ExpRent = Filter_RT.priceto;
            if (!string.IsNullOrEmpty(Filter_RT.distancefrm))
                sortres.distancefrm = Filter_RT.distancefrm;
            if (!string.IsNullOrEmpty(Filter_RT.distanceto))
                sortres.distanceto = Filter_RT.distanceto;

            if (!string.IsNullOrEmpty(Filter_RT.availability))
                sortres.availablefrm = Filter_RT.availability;

            await Navigation.PopModalAsync();
            await Navigation.PushAsync(new RentalList(sortres, Cityurl));
        }


        private void Backbtn_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}