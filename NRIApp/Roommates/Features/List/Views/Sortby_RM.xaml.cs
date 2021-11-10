using NRIApp.Roommates.Features.List.ViewModels;
using NRIApp.Roommates.Features.Post.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Roommates.Features.List.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sortby_RM : ContentPage
    {
        private string _cityurl;
        public static string orderby = "";
        public static string pagetype = "";
        public string Cityurl
        {
            get { return _cityurl; }
            set { _cityurl = value; OnPropertyChanged(); }
        }
        List<sortlist> sortlist = new List<sortlist>();
        Response sortres = new Response();
        public Sortby_RM(Response res, string cityurl)
        {

            InitializeComponent();
            try
            {
                Getsortorder(res.orderby);
                res.Newcityurl = cityurl;
                Cityurl = cityurl;
                sortres.primarycategoryvalue = res.primarycategoryvalue;
                sortres.usertype = res.usertype;
                sortres.gender = res.gender;
                sortres.stayperiod = res.stayperiod;
                sortres.attachedbaths = res.attachedbaths;
                sortres.petpolicy = res.petpolicy;
                sortres.isveg = res.isveg;
                sortres.furnishing = res.furnishing;
                sortres.amenities = res.amenities;
                sortres.distancefrm = res.distancefrm;
                sortres.distanceto = res.distanceto;
                sortres.pricefrom = res.pricefrom;
                sortres.ExpRent = res.ExpRent;
            }
            catch (Exception e)
            {

            }

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
                        Sortby_RM.orderby = item.type;
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
                        Sortby_RM.orderby = item.type;
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
                    Sortby_RM.orderby = item.type;
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
            if (!string.IsNullOrEmpty(Filter_RM.checkedcategoryvalue))
                sortres.primarycategoryvalue = Filter_RM.checkedcategoryvalue;
            if (!string.IsNullOrEmpty(Filter_RM.tenant))
                sortres.usertype = Filter_RM.tenant;
            if (!string.IsNullOrEmpty(Filter_RM.gender))
                sortres.gender = Filter_RM.gender;
            if (!string.IsNullOrEmpty(Filter_RM.leaseperiod))
                sortres.stayperiod = Filter_RM.leaseperiod;
            if (!string.IsNullOrEmpty(Filter_RM.bathtype))
                sortres.attachedbaths = Filter_RM.bathtype;
            if (!string.IsNullOrEmpty(Filter_RM.petpreference))
                sortres.petpolicy = Filter_RM.petpreference;
            if (!string.IsNullOrEmpty(Filter_RM.vegpreference))
                sortres.isveg = Filter_RM.vegpreference;
            if (!string.IsNullOrEmpty(Filter_RM.furnish))
                sortres.furnishing = Filter_RM.furnish;
            if (!string.IsNullOrEmpty(Filter_RM.checkedamenitycategoryvalue))
                sortres.amenities = Filter_RM.checkedamenitycategoryvalue;
            if (!string.IsNullOrEmpty(Filter_RM.pricefrm))
                sortres.pricefrom = Filter_RM.pricefrm;
            if (!string.IsNullOrEmpty(Filter_RM.priceto))
                sortres.ExpRent = Filter_RM.priceto;
            if (!string.IsNullOrEmpty(Filter_RM.distancefrm))
                sortres.distancefrm = Filter_RM.distancefrm;
            if (!string.IsNullOrEmpty(Filter_RM.distanceto))
                sortres.distanceto = Filter_RM.distanceto;

            await Navigation.PopModalAsync();
            await Navigation.PushAsync(new Category(sortres, Cityurl));
        }


        private void Backbtn_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}