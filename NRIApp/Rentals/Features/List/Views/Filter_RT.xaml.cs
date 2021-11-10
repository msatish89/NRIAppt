using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.Rentals.Features.Post.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Rentals.Features.List.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Filter_RT : ContentPage
	{
        IUserDialogs dialogs = UserDialogs.Instance;

    

        public int roomtypeimgnum = 0;
        public int priceimgnum = 0;
        public int tenantimgnum = 0;
        public int dateimgnum = 0;
        public int Leaseimgnum = 0;
        public int petimgnum = 0;
        public int Vegimgnum = 0;
        public int furnishimgnum = 0;
        public int amenitiesimgnum = 0;
        public int distanceimgnum = 0;
        public int noofbedsimgnum = 0;
        public int accomodateimgnum = 0;
        public int noofbathimgnum = 0;


        public static string pagetype = "";

        public static string checkedcategoryvalue = "";
        public static string tenant = "";
        public static string leaseperiod = "";
        public static string petpreference = "";
        public static string vegpreference = "";
        public static string furnish = "";
        public static string checkedamenitycategoryvalue = "";
        public static string checkeddistancecategoryvalue = "";
        public static string availability = "";
        public static string distancefrm = "";
        public static string distanceto = "";
        public static string pricefrm = "";
        public static string priceto = "";
        public static string noofbed = "";
        public static string noofaccomodate = "";
        public static string noofbath = "";
        public static string filtercity = "";
        public static string filtercityurl = "";
        public static string filterlatitude = "";
        public static string filterlongitude = "";
        public string city { get; set; }
        private string _cityurl;
        public string Cityurl
        {
            get { return _cityurl; }
            set { _cityurl = value; OnPropertyChanged(); }
        }
        List<sortlist> getroomtype = new List<sortlist>();
        List<sortlist> tenanttype = new List<sortlist>();
        List<sortlist> noofbhk = new List<sortlist>();
        List<sortlist> leasetype = new List<sortlist>();
        List<sortlist> petType = new List<sortlist>();
        List<sortlist> vegtype = new List<sortlist>();
        List<sortlist> Furnishtype = new List<sortlist>();
        List<sortlist> Bathtype = new List<sortlist>();


        public List<sortlist> getamenitieslist { get; set; }

        public Filter_RT (RTResponse res, string cityurl)
		{
			InitializeComponent ();
            dialogs.ShowLoading();
           
            try
            {
                res.Newcityurl = cityurl;
                Cityurl = cityurl;
                roomtypeimg.Source = "minusGrey.png";
                roomtypeimgnum = 1;
                roomtypelayout.IsVisible = true;
                GetRoomtype(res.supercategoryid, res.primarycategoryvalue);
                noofbedsimg.Source = "downarrowGrey.png";
                Noofbedlayout.IsVisible = false;
                Getnoofbeds(res.secondarycategoryvalue);
                priceimg.Source = "downarrowGrey.png";
                pricelist.IsVisible = false;
                tenantimg.Source = "downarrowGrey.png";
                tenantlayout.IsVisible = false;
                GetTenantType(res.categoryname);
                dateimg.Source = "downarrowGrey.png";
                availabilitylayout.IsVisible = false;
                Leaseimg.Source = "downarrowGrey.png";
                Leasetypelist.IsVisible = false;
                Getleasetype(res.stayperiod);
                petimg.Source = "downarrowGrey.png";
                Petlist.IsVisible = false;
                GetPetpolicy(res.petpolicy);
                Vegimg.Source = "downarrowGrey.png";
                Vegpreferencelayout.IsVisible = false;
                GetVegpreference(res.isveg);
                furnishimg.Source = "downarrowGrey.png";
                Furnishlayout.IsVisible = false;
                GetFurnishedtype(res.furnishing);
                noofbathsimg.Source = "downarrowGrey.png";
                Bathlayout.IsVisible = false;
                GetBathtype(res.noofbaths);
                accomodateimg.Source = "downarrowGrey.png";
                Accomodatelayout.IsVisible = false;
                amenitiesimg.Source = "downarrowGrey.png";
                Amenitieslayout.IsVisible = false;
                Getamenitieslist(res.amenities);
                distanceimg.Source = "downarrowGrey.png";
                layoutdistance.IsVisible = false;
                //newly add
                changecitylayout.IsVisible = false;
                changecitytypeimg.Source = "downarrowGrey.png";
                if (!string.IsNullOrEmpty(res.City))
                {
                    if (res.City.Contains(","))
                    {
                        Tlocationtxt = res.City;
                        txtlocation.Text = res.City;
                        city = res.City;
                    }
                    else
                    {
                        Tlocationtxt = res.City + ", " + res.statecode;
                        txtlocation.Text = res.City + ", " + res.statecode;
                        city = res.City + ", " + res.statecode;
                    }
                }
                else
                {
                   // if(!string.IsNullOrEmpty(Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName))
                   // {
                   //     Tlocationtxt = Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                   //     txtlocation.Text = Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                   //     city = Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName;
                   // }
                   //else
                   // {
                        Tlocationtxt = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                        txtlocation.Text = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                        city = Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
                    //}
                }
                locationlist.IsVisible = false;
                changecitytypeimg.Source = "downarrowGrey.png";
                changecitytypeimgnum = 0;
                changecitylayout.IsVisible = false;
                changezipcode = cityurl;
                filterlatitude = res.userlat;
                filterlongitude = res.userlong;
                locationlist.IsVisible = false;
                changecitytypeimg.Source = "downarrowGrey.png";
                changecitytypeimgnum = 0;
                // citytypeimg.Source = "downarrowGrey.png";
                //citylayout.IsVisible = false;
                if (string.IsNullOrEmpty(res.Movedate))
                {
                   
                    AvailDate.Date = DateTime.Today;
                }

                if (cityurl != Commonsettings.Usercityurl)
                {
                    changelocationtickimg.IsVisible = true;
                }
                else
                {
                    changelocationtickimg.IsVisible = false;
                }
                //else
                //{
                //    AvailDate.Date = DateTime.ParseExact(res.Movedate, "yyyy-MM-dd", null);
                //}
                // AvailDate.Date = Convert.ToDateTime(res.Movedate).Date;
                if (!string.IsNullOrEmpty(res.pricefrom))
                    slidervalue.LowerValue = Convert.ToInt64(res.pricefrom);
                if (!string.IsNullOrEmpty(res.ExpRent))
                    slidervalue.UpperValue = Convert.ToInt64(res.ExpRent);
                if (!string.IsNullOrEmpty(res.distancefrm))
                    distancerange.LowerValue = Convert.ToInt64(res.distancefrm);
                if (!string.IsNullOrEmpty(res.distanceto))
                    distancerange.UpperValue = Convert.ToInt64(res.distanceto);
            }
            catch(Exception e)
            {

            }
            dialogs.HideLoading();
        }

        public void GetRoomtype(int supercatgryval,string roomtype)
        {
           if(supercatgryval==4)
            {
                getroomtype.Add(new sortlist { oderbylist = "Single Family Home", type = "1" });
                getroomtype.Add(new sortlist { oderbylist = "Apartment", type = "2" });
                getroomtype.Add(new sortlist { oderbylist = "Condo", type = "3" });
                getroomtype.Add(new sortlist { oderbylist = "Town house", type = "6" });
                getroomtype.Add(new sortlist { oderbylist = "Homes", type = "8" });
                getroomtype.Add(new sortlist { oderbylist = "Houses", type = "9" });
                getroomtype.Add(new sortlist { oderbylist = "Hostels", type = "10" });
                getroomtype.Add(new sortlist { oderbylist = "Hotels", type = "11" });
                getroomtype.Add(new sortlist { oderbylist = "Basement Apartment", type = "12" });
                getroomtype.Add(new sortlist { oderbylist = "Parking Garage", type = "13" });
                foreach (var data in getroomtype)
                {
                    BoxView boxx = new BoxView();
                    boxx.BackgroundColor = Color.FromHex("e0e0e0");
                    boxx.HeightRequest = 1;
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    chbx.Text = data.oderbylist;
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_CategoryCheckChanged;
                    chbx.ClassId = data.type;
                    chbx.Padding = 5;
                    if (roomtype != "")
                    {
                        string[] rmtype = roomtype.Split(',');
                        foreach (var i in rmtype)
                        {
                            if (data.type == i)
                            {
                                chbx.IsChecked = true;
                            }

                        }
                    }

                    roomtypelayout.Children.Add(chbx);
                    roomtypelayout.Children.Add(boxx);
                }
            }
           //I need short stay accomodation
           else
            {
                getroomtype.Add(new sortlist { oderbylist = "Single Family Home", type = "1" });
                getroomtype.Add(new sortlist { oderbylist = "Apartment", type = "2" });
                getroomtype.Add(new sortlist { oderbylist = "Condo", type = "3" });
                getroomtype.Add(new sortlist { oderbylist = "Town house", type = "6" });
                getroomtype.Add(new sortlist { oderbylist = "Homes", type = "8" });
                getroomtype.Add(new sortlist { oderbylist = "Houses", type = "9" });
                getroomtype.Add(new sortlist { oderbylist = "Hostels", type = "10" });
                getroomtype.Add(new sortlist { oderbylist = "Hotels", type = "11" });
                foreach (var data in getroomtype)
                {
                    BoxView boxx = new BoxView();
                    boxx.BackgroundColor = Color.FromHex("e0e0e0");
                    boxx.HeightRequest = 1;
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    chbx.Text = data.oderbylist;
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_CategoryCheckChanged;
                    chbx.ClassId = data.type;
                    chbx.Padding = 5;
                    if (roomtype != "")
                    {
                        string[] rmtype = roomtype.Split(',');
                        foreach (var i in rmtype)
                        {
                            if (data.type == i)
                            {
                                chbx.IsChecked = true;
                            }

                        }
                    }

                    roomtypelayout.Children.Add(chbx);
                    roomtypelayout.Children.Add(boxx);
                }
            }
            
        }
        public void Getnoofbeds(string noofbeds)
        {
            noofbhk.Add(new sortlist { oderbylist = "1 Bed", type = "1", image = "RadioButtonUnChecked.png" });
            noofbhk.Add(new sortlist { oderbylist = "2 Beds", type = "2", image = "RadioButtonUnChecked.png" });
            noofbhk.Add(new sortlist { oderbylist = "3 Beds", type = "3", image = "RadioButtonUnChecked.png" });
            noofbhk.Add(new sortlist { oderbylist = "4+ Beds", type = "4", image = "RadioButtonUnChecked.png" });
            foreach (var item in noofbhk)
            {
                if (!string.IsNullOrEmpty(noofbeds))
                {
                    if (noofbeds == item.type || noofbeds == item.oderbylist)
                    {
                        item.image = "RadioButtonChecked.png";
                        Filter_RT.noofbed = item.type;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }
                }
                else
                {
                    Filter_RT.noofbed = "";
                }
            }
            Noofbeddata.ItemsSource = noofbhk;
        }
        private void NoofBedData_ItemTapped(object sender, EventArgs e)
        {
            //var radiogrp =(sortlist)e.item;
            var radiogrp = sender as ListView;
            sortlist lsow = new sortlist();
            lsow = (sortlist)radiogrp.SelectedItem;
            string orderby = lsow.type;

            foreach (var item in noofbhk)
            {
                if (item.type == lsow.type)
                {
                    item.image = "RadioButtonChecked.png";
                    Filter_RT.noofbed = item.type;
                }
                else
                {
                    item.image = "RadioButtonUnChecked.png";
                }
            }

            Noofbeddata.ItemsSource = null;
            Noofbeddata.ItemsSource = noofbhk;
        }
        public void GetTenantType(string categoryname)
        {
            tenanttype.Add(new sortlist { oderbylist = "Individual", type = "I am an individual",oderlist="0", image = "RadioButtonUnChecked.png" });
            tenanttype.Add(new sortlist { oderbylist = "Couple / Family", type = "I am a couple / family", image = "RadioButtonUnChecked.png" });
            tenanttype.Add(new sortlist { oderbylist = "Group", type = "I am a group", image = "RadioButtonUnChecked.png" });
            foreach (var item in tenanttype)
            {
                if (!string.IsNullOrEmpty(categoryname))
                {
                    if (categoryname == item.type || categoryname == item.oderbylist)
                    {
                        item.image = "RadioButtonChecked.png";
                        Filter_RT.tenant = item.oderbylist;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }
                }
                else
                {
                    Filter_RT.tenant = "";
                }
            }
            tenantdata.ItemsSource = tenanttype;
        }
        private void Tenantdata_ItemTapped(object sender, EventArgs e)
        {
            //var radiogrp =(sortlist)e.item;
            var radiogrp = sender as ListView;
            sortlist lsow = new sortlist();
            lsow = (sortlist)radiogrp.SelectedItem;
            string orderby = lsow.type;

            foreach (var item in tenanttype)
            {
                if (item.oderbylist == lsow.oderbylist)
                {
                    item.image = "RadioButtonChecked.png";
                    Filter_RT.tenant = item.oderbylist;
                }
                else
                {
                    item.image = "RadioButtonUnChecked.png";
                }
            }

            tenantdata.ItemsSource = null;
            tenantdata.ItemsSource = tenanttype;
        }
        public void Getleasetype(string stayperiod)
        {
            leasetype.Add(new sortlist { oderbylist = "Long term (6+ months)", type = "1", image = "RadioButtonUnChecked.png" });
            leasetype.Add(new sortlist { oderbylist = "Short term", type = "2", image = "RadioButtonUnChecked.png" });
            leasetype.Add(new sortlist { oderbylist = "Any", type = "3", image = "RadioButtonUnChecked.png" });
            foreach (var item in leasetype)
            {
                if (!string.IsNullOrEmpty(stayperiod))
                {
                    if (stayperiod == item.type || stayperiod == item.type)
                    {
                        item.image = "RadioButtonChecked.png";
                        Filter_RT.leaseperiod = item.type;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }
                }
                else
                {
                    stayperiod = "3";

                    if (stayperiod == item.type)
                    {
                        item.image = "RadioButtonChecked.png";
                        Filter_RT.leaseperiod = item.type;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }

                }
                Leasedata.ItemsSource = leasetype;
            }
        }
        private void Leasedata_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var radiogrp = sender as ListView;
            sortlist lsow = new sortlist();
            lsow = (sortlist)radiogrp.SelectedItem;

            foreach (var item in leasetype)
            {
                if (item.type == lsow.type)
                {
                    item.image = "RadioButtonChecked.png";
                    Filter_RT.leaseperiod = item.type;
                }
                else
                {
                    item.image = "RadioButtonUnChecked.png";
                }
            }

            Leasedata.ItemsSource = null;
            Leasedata.ItemsSource = leasetype;
        }

     
        public void GetPetpolicy(string petpolicy)
        {
            petType.Add(new sortlist { oderbylist = "No Pets", type = "0", image = "RadioButtonUnChecked.png" });
            petType.Add(new sortlist { oderbylist = "Only Dogs", type = "1", image = "RadioButtonUnChecked.png" });
            petType.Add(new sortlist { oderbylist = "Only Cats", type = "2", image = "RadioButtonUnChecked.png" });
            petType.Add(new sortlist { oderbylist = "Any Pet is Ok", type = "999", image = "RadioButtonUnChecked.png" });
            foreach (var item in petType)
            {
                if (!string.IsNullOrEmpty(petpolicy))
                {
                    if (petpolicy == item.type || petpolicy == item.type)
                    {
                        item.image = "RadioButtonChecked.png";
                        Filter_RT.petpreference = item.type;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }
                }
                else
                {
                    petpolicy = "999";

                    if (petpolicy == item.type)
                    {
                        item.image = "RadioButtonChecked.png";
                        Filter_RT.petpreference = item.type;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }

                }
                Petdata.ItemsSource = petType;
            }
        }

        private void Petdata_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var radiogrp = sender as ListView;
            sortlist lsow = new sortlist();
            lsow = (sortlist)radiogrp.SelectedItem;
            string orderby = lsow.type;

            foreach (var item in petType)
            {
                if (item.type == lsow.type)
                {
                    item.image = "RadioButtonChecked.png";
                    Filter_RT.petpreference = item.type;
                }
                else
                {
                    item.image = "RadioButtonUnChecked.png";
                }
            }

            Petdata.ItemsSource = null;
            Petdata.ItemsSource = petType;
        }

        public void GetVegpreference(string IsVeg)
        {
            vegtype.Add(new sortlist { oderbylist = "Yes, Vegetarian mandatory", type = "1", image = "RadioButtonUnChecked.png" });
            vegtype.Add(new sortlist { oderbylist = "No, Non-veg is ok", type = "2", image = "RadioButtonUnChecked.png" });
            vegtype.Add(new sortlist { oderbylist = "Both", type = "0", image = "RadioButtonUnChecked.png" });

            foreach (var item in vegtype)
            {
                if (!string.IsNullOrEmpty(IsVeg))
                {
                    if (IsVeg == item.type || IsVeg == item.type)
                    {
                        item.image = "RadioButtonChecked.png";
                        Filter_RT.vegpreference = item.type;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }
                }
                else
                {
                    IsVeg = "0";

                    if (IsVeg == item.type)
                    {
                        item.image = "RadioButtonChecked.png";
                        Filter_RT.vegpreference = item.type;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }

                }
                Vegdata.ItemsSource = vegtype;
            }
        }

        private void Vegdata_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var radiogrp = sender as ListView;
            sortlist lsow = new sortlist();
            lsow = (sortlist)radiogrp.SelectedItem;

            foreach (var item in vegtype)
            {
                if (item.type == lsow.type)
                {
                    item.image = "RadioButtonChecked.png";
                    Filter_RT.vegpreference = item.type;
                }
                else
                {
                    item.image = "RadioButtonUnChecked.png";
                }
            }

            Vegdata.ItemsSource = null;
            Vegdata.ItemsSource = vegtype;
        }



        public void GetFurnishedtype(string Furnish)
        {
            Furnishtype.Add(new sortlist { oderbylist = "Unfurnished", type = "0", image = "RadioButtonUnChecked.png" });
            Furnishtype.Add(new sortlist { oderbylist = "Furnished with Bed", type = "1", image = "RadioButtonUnChecked.png" });
            Furnishtype.Add(new sortlist { oderbylist = "Semi Furnished", type = "2", image = "RadioButtonUnChecked.png" });
            Furnishtype.Add(new sortlist { oderbylist = "Fully Furnished", type = "3", image = "RadioButtonUnChecked.png" });
            Furnishtype.Add(new sortlist { oderbylist = "Any", type = "999", image = "RadioButtonUnChecked.png" });
            foreach (var item in Furnishtype)
            {
                if (!string.IsNullOrEmpty(Furnish))
                {
                    if (Furnish == item.type || Furnish == item.type)
                    {
                        item.image = "RadioButtonChecked.png";
                        Filter_RT.furnish = item.type;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }
                }
                else
                {
                    Furnish = "999";

                    if (Furnish == item.type)
                    {
                        item.image = "RadioButtonChecked.png";
                        Filter_RT.leaseperiod = item.type;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }

                }
                Furnishdata.ItemsSource = Furnishtype;
            }
        }
        public void GetBathtype(string nofbath)
        {
            Bathtype.Add(new sortlist { oderbylist = "1", type = "1", image = "RadioButtonUnChecked.png" });
            Bathtype.Add(new sortlist { oderbylist = "2", type = "2", image = "RadioButtonUnChecked.png" });
            Bathtype.Add(new sortlist { oderbylist = "3", type = "3", image = "RadioButtonUnChecked.png" });
            Bathtype.Add(new sortlist { oderbylist = "4", type = "4", image = "RadioButtonUnChecked.png" });
            Bathtype.Add(new sortlist { oderbylist = "4+", type = "5", image = "RadioButtonUnChecked.png" });

            foreach (var item in Bathtype)
            {
                if (!string.IsNullOrEmpty(nofbath))
                {
                    if (nofbath == item.type || nofbath == item.oderbylist)
                    {
                        item.image = "RadioButtonChecked.png";
                        Filter_RT.noofbath = item.type;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }
                }
                else
                {
                    nofbath = "1";

                    if (nofbath == item.type)
                    {
                        item.image = "RadioButtonChecked.png";
                        Filter_RT.noofbath = item.type;
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }

                }
                Noofbathdata.ItemsSource = Bathtype;
            }
        }

        private void Bathtypedata_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var radiogrp = sender as ListView;
            sortlist lsow = new sortlist();
            lsow = (sortlist)radiogrp.SelectedItem;

            foreach (var item in Bathtype)
            {
                if (item.type == lsow.type)
                {
                    item.image = "RadioButtonChecked.png";
                    Filter_RT.noofbath = item.type;
                }
                else
                {
                    item.image = "RadioButtonUnChecked.png";
                }
            }

            Noofbathdata.ItemsSource = null;
            Noofbathdata.ItemsSource = Bathtype;
        }

        private void Furnishdata_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var radiogrp = sender as ListView;
            sortlist lsow = new sortlist();
            lsow = (sortlist)radiogrp.SelectedItem;

            foreach (var item in Furnishtype)
            {
                if (item.type == lsow.type)
                {
                    item.image = "RadioButtonChecked.png";
                    Filter_RT.furnish = item.type;
                }
                else
                {
                    item.image = "RadioButtonUnChecked.png";
                }
            }

            Furnishdata.ItemsSource = null;
            Furnishdata.ItemsSource = Furnishtype;
        }
        public void Getamenitieslist(string amenities)
        {
            List<sortlist> amenitylist = new List<sortlist>();
            amenitylist.Add(new sortlist { oderbylist = "Gym/Fitness Center" });
            amenitylist.Add(new sortlist { oderbylist = "Swimming Pool" });
            amenitylist.Add(new sortlist { oderbylist = "Car Park" });
            amenitylist.Add(new sortlist { oderbylist = "Visitors Parking" });
            amenitylist.Add(new sortlist { oderbylist = "Power Backup" });
            amenitylist.Add(new sortlist { oderbylist = "Garbage Disposal" });
            amenitylist.Add(new sortlist { oderbylist = "Private Lawn" });
            amenitylist.Add(new sortlist { oderbylist = "Water Heater Plant" });
            amenitylist.Add(new sortlist { oderbylist = "Security System" });
            amenitylist.Add(new sortlist { oderbylist = "Laundry Service" });
            amenitylist.Add(new sortlist { oderbylist = "Elevator" });
            amenitylist.Add(new sortlist { oderbylist = "Club House" });
            getamenitieslist = amenitylist;
            foreach (var data in getamenitieslist)
            {
                BoxView boxx = new BoxView();
                boxx.BackgroundColor = Color.FromHex("e0e0e0");
                boxx.HeightRequest = 1;
                Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                //chbx.OnImg = "CheckBoxChecked.png";
                //chbx.OffImg = "CheckBoxUnChecked.png";
                chbx.Text = data.oderbylist;
                chbx.ClassId = data.oderbylist;
                //chbx.ShowLabel = true;
                chbx.CheckChanged += Chb_amenitiesCheckChanged;
                chbx.Padding = 5;
                if (!string.IsNullOrEmpty(amenities))
                {
                    string[] aminitylst = amenities.Split(',');
                    foreach (var i in aminitylst)
                    {
                        if (data.oderbylist == i)
                        {
                            chbx.IsChecked = true;

                        }

                    }
                }
                Amenitieslayout.Children.Add(chbx);
                Amenitieslayout.Children.Add(boxx);
            }
        }
        private void Chb_CategoryCheckChanged(object sender, EventArgs e)
        {
            var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
            if (category.IsChecked==true)
            {
                if (Filter_RT.checkedcategoryvalue != null && Filter_RT.checkedcategoryvalue != "")
                {
                    if (!Filter_RT.checkedcategoryvalue.Contains(category.ClassId))
                    {
                        Filter_RT.checkedcategoryvalue += "," + category.ClassId;
                    }
                }
                else
                {

                    Filter_RT.checkedcategoryvalue = category.ClassId;
                }
            }
            else
            {
                string[] indexvalue = Filter_RT.checkedcategoryvalue.Split(',');
                int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                Filter_RT.checkedcategoryvalue = string.Join(",", indexvalue);
            }

        }
     
        private void Chb_amenitiesCheckChanged(object sender, EventArgs e)
        {
            var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
            if (category.IsChecked==true)
            {
                if (Filter_RT.checkedamenitycategoryvalue != null && Filter_RT.checkedamenitycategoryvalue != "")
                {
                    if (!Filter_RT.checkedamenitycategoryvalue.Contains(category.ClassId))
                    {
                        Filter_RT.checkedamenitycategoryvalue += "," + category.ClassId;
                    }
                }
                else
                {

                    Filter_RT.checkedamenitycategoryvalue += category.ClassId;
                }

            }
            else
            {
                string[] indexvalue = Filter_RT.checkedamenitycategoryvalue.Split(',');
                int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                Filter_RT.checkedamenitycategoryvalue = string.Join(",", indexvalue);
            }
        }
        

        private void roomtypetapcmd(object sender, EventArgs e)
        {
            if (roomtypeimgnum == 0)
            {
                roomtypeimg.Source = "minusGrey.png";
                roomtypeimgnum = 1;
                roomtypelayout.IsVisible = true;
            }
            else
            {
                roomtypeimg.Source = "downarrowGrey.png";
                roomtypeimgnum = 0;
                roomtypelayout.IsVisible = false;
            }
        }
        private void NoofBedtapcmd(object sender, EventArgs e)
        {
            if (noofbedsimgnum == 0)
            {
                noofbedsimg.Source = "minusGrey.png";
                noofbedsimgnum = 1;
                Noofbedlayout.IsVisible = true;
            }
            else
            {
                noofbedsimg.Source = "downarrowGrey.png";
                noofbedsimgnum = 0;
                Noofbedlayout.IsVisible = false;
            }
        }
        private void accomodate_Tapcmd(object sender, EventArgs e)
        {
            if (accomodateimgnum == 0)
            {
                accomodateimg.Source = "minusGrey.png";
                accomodateimgnum = 1;
                Accomodatelayout.IsVisible = true;
            }
            else
            {
                accomodateimg.Source = "downarrowGrey.png";
                accomodateimgnum = 0;
                Accomodatelayout.IsVisible = false;
            }
        }

        private void pricerangetapcmd(object sender, EventArgs e)
        {
            if (priceimgnum == 0)
            {
                priceimg.Source = "minusGrey.png";
                priceimgnum = 1;
                pricelist.IsVisible = true;
            }
            else
            {
                priceimg.Source = "downarrowGrey.png";
                priceimgnum = 0;
                pricelist.IsVisible = false;
            }
        }

        private void Tenanttapcmd(object sender, EventArgs e)
        {
            if (tenantimgnum == 0)
            {
                tenantimg.Source = "minusGrey.png";
                tenantimgnum = 1;
                tenantlayout.IsVisible = true;
            }
            else
            {
                tenantimg.Source = "downarrowGrey.png";
                tenantimgnum = 0;
                tenantlayout.IsVisible = false;
            }
        }

        private void Availability_date(object sender, DateChangedEventArgs e)
        {
            var datepickr = sender as DatePicker;
            datepickr.Date = e.NewDate;
            availability = e.NewDate.ToString("dd MMM yyyy");
            //datepickr.MinimumDate=
        }

        private void availability_Tap(object sender, EventArgs e)
        {
            if (dateimgnum == 0)
            {
                dateimg.Source = "minusGrey.png";
                dateimgnum = 1;
                availabilitylayout.IsVisible = true;
            }
            else
            {
                dateimg.Source = "downarrowGrey.png";
                dateimgnum = 0;
                availabilitylayout.IsVisible = false;
            }
        }

        private void Leasetapcmd(object sender, EventArgs e)
        {
            if (Leaseimgnum == 0)
            {
                Leaseimg.Source = "minusGrey.png";
                Leaseimgnum = 1;
                Leasetypelist.IsVisible = true;
            }
            else
            {
                Leaseimg.Source = "downarrowGrey.png";
                Leaseimgnum = 0;
                Leasetypelist.IsVisible = false;
            }
        }



        private void Pet_Tapcmd(object sender, EventArgs e)
        {
            if (petimgnum == 0)
            {
                petimg.Source = "minusGrey.png";
                petimgnum = 1;
                Petlist.IsVisible = true;
            }
            else
            {
                petimg.Source = "downarrowGrey.png";
                petimgnum = 0;
                Petlist.IsVisible = false;
            }
        }

        private void Veg_Tapcmd(object sender, EventArgs e)
        {
            if (Vegimgnum == 0)
            {
                Vegimg.Source = "minusGrey.png";
                Vegimgnum = 1;
                Vegpreferencelayout.IsVisible = true;
            }
            else
            {
                Vegimg.Source = "downarrowGrey.png";
                Vegimgnum = 0;
                Vegpreferencelayout.IsVisible = false;
            }
        }

        private void Furnish_Tapcmd(object sender, EventArgs e)
        {
            if (furnishimgnum == 0)
            {
                furnishimg.Source = "minusGrey.png";
                furnishimgnum = 1;
                Furnishlayout.IsVisible = true;
            }
            else
            {
                furnishimg.Source = "downarrowGrey.png";
                furnishimgnum = 0;
                Furnishlayout.IsVisible = false;
            }
        }
        private void Bath_Tapcmd(object sender, EventArgs e)
        {
            if (noofbathimgnum == 0)
            {
                noofbathsimg.Source = "minusGrey.png";
                noofbathimgnum = 1;
                Bathlayout.IsVisible = true;
            }
            else
            {
                noofbathsimg.Source = "downarrowGrey.png";
                noofbathimgnum = 0;
                Bathlayout.IsVisible = false;
            }
        }
        private void amenities_Tapcmd(object sender, EventArgs e)
        {
            if (amenitiesimgnum == 0)
            {
                amenitiesimg.Source = "minusGrey.png";
                amenitiesimgnum = 1;
                Amenitieslayout.IsVisible = true;
            }
            else
            {
                amenitiesimg.Source = "downarrowGrey.png";
                amenitiesimgnum = 0;
                Amenitieslayout.IsVisible = false;
            }
        }
        private void Accomodatenum_TextChanged(object sender, TextChangedEventArgs e)
        {
            
           try
            {
                if (!string.IsNullOrEmpty(accomodatenum.Text))
                {


                    if (Convert.ToInt32(accomodatenum.Text) > 20)
                    {
                        //accomodatenum.Text = "";
                        //accomodatenum.Placeholder = "Kindly enter numbers between 1-20";
                        //accomodatenum.PlaceholderColor = Color.Red;
                        dialogs.Alert("Kindly enter numbers between 1-20 in Accomodates");
                    }
                    
                }
                else
                {
                    // if(CheckAccomodates(e.NewTextValue))
                    //  {
                    accomodatenum.Text = e.NewTextValue;
                    Filter_RT.noofaccomodate = accomodatenum.Text;
                    // }

                }

            }
            catch(Exception ex)
            {
                accomodatenum.Text = "20";
            }
        }

        private void distance_Tapcmd(object sender, EventArgs e)
        {
            if (distanceimgnum == 0)
            {
                distanceimg.Source = "minusGrey.png";
                distanceimgnum = 1;
                layoutdistance.IsVisible = true;
            }
            else
            {
                distanceimg.Source = "downarrowGrey.png";
                distanceimgnum = 0;
                layoutdistance.IsVisible = false;
            }
        }
        RTResponse filterres = new RTResponse();
        private async void ApplyFiltercmd(object sender, EventArgs e)
        {
           try
            {
                if ((txtlocation.Text.Trim().Length == 0 || changezipcode.Trim().Length == 0) )
                {
                    dialogs.Toast("Select Location...");
                }
                else
                {
                    if (!string.IsNullOrEmpty(Cityurl))
                    {
                        filterres.cityurl = Cityurl;
                    }
                    filterres.userlat = latitude.ToString();
                    filterres.userlong = longitude.ToString();
                    filterres.City = city;

                    NRIApp.Rentals.Features.Post.ViewModels.VMRTLCF.SelectedCityName = city;
                    filterres.primarycategoryvalue = Filter_RT.checkedcategoryvalue;
                    filterres.stayperiod = Filter_RT.leaseperiod;
                    filterres.pricefrom = Convert.ToString(Math.Round(slidervalue.LowerValue));
                    filterres.ExpRent = Convert.ToString(Math.Round(slidervalue.UpperValue));
                    filterres.distancefrm = Convert.ToString(Math.Round(distancerange.LowerValue));
                    filterres.distanceto = Convert.ToString(Math.Round(distancerange.UpperValue));
                    filterres.petpolicy = Filter_RT.petpreference;
                    filterres.furnishing = Filter_RT.furnish;
                    filterres.isveg = Filter_RT.vegpreference;
                    filterres.amenities = Filter_RT.checkedamenitycategoryvalue;
                    filterres.orderby = Sortby_RT.orderby;
                    filterres.categoryname = Filter_RT.tenant;
                    filterres.secondarycategoryvalue = Filter_RT.noofbed;
                    filterres.noofbaths = Filter_RT.noofbath;
                    filterres.accommodates = Filter_RT.noofaccomodate;


                    if (string.IsNullOrEmpty(availability))
                        availability = AvailDate.Date.ToString("yyyy-MM-dd");
                    else
                        availability = AvailDate.Date.ToString("yyyy-MM-dd");
                    filterres.availablefrm = Filter_RT.availability;
                    filterres.Movedate = Filter_RT.availability;
                    Filter_RT.distancefrm = Convert.ToString(Math.Round(distancerange.LowerValue));
                    Filter_RT.distanceto = Convert.ToString(Math.Round(distancerange.UpperValue));
                    Filter_RT.pricefrm = Convert.ToString(Math.Round(slidervalue.LowerValue));
                    Filter_RT.priceto = Convert.ToString(Math.Round(slidervalue.UpperValue));
                    filterres.Newcityurl = Cityurl;
                    filterres.City = city;
                    filtercity = city;
                    filtercityurl = Cityurl;
                    filterlatitude = filterres.userlat;
                    filterlongitude = filterres.userlong;
                    //var vm = BindingContext as ViewModels.VMRoommates;
                    await Navigation.PopModalAsync();
                    await Navigation.PushAsync(new RentalList(filterres, Cityurl));
                }
            }
            catch(Exception ex)
            {
                try
                {
                    await Navigation.PushAsync(new RentalList(filterres, Cityurl));
                }
                catch (Exception exx)
                {
                    string msg = exx.Message;
                }
            }
        }

        private async void Backbtn_Tapped(object sender, EventArgs e)
        {
           try
            {
                await Navigation.PopModalAsync();
            }
            catch(Exception ex)
            {

            }
        }

        private async void reset_Tap(object sender, EventArgs e)
        {
            try
            {
                var alert = await DisplayAlert("Alert!", "Are you sure?", "Ok", "Cancel");
                if (alert)
                {
                    dialogs.ShowLoading("", null);
                    Filter_RT.checkedcategoryvalue = "";
                    Filter_RT.checkedamenitycategoryvalue = "";
                    OnPropertyChanged(nameof(roomtypeimg));

                    slidervalue.LowerValue = 0;
                    slidervalue.UpperValue = 50000;
                    distancerange.LowerValue = 0;
                    distancerange.UpperValue = 75;
                    var currentpage = GetCurrentPage();
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    foreach (var item in roomtypelayout.Children)
                    {
                        if (item.GetType() == chbx.GetType())
                        {
                            var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                            if (cat.IsChecked == true)
                            {
                                cat.IsChecked = false;
                            }
                        }

                    }
                    //  NRIApp.Helpers.ScrollBar lstvew = new NRIApp.Helpers.ScrollBar();

                    foreach (var item in tenantlayout.Children)
                    {
                        // if (item.GetType() == lstvew.GetType())
                        foreach (var item1 in tenanttype)
                        {
                            if (item1.oderlist == "0")
                            {
                                item1.image = "RadioButtonChecked.png";
                                Filter_RT.tenant = "Individual";
                            }
                            else
                            {
                                item1.image = "RadioButtonUnChecked.png";
                            }
                        }
                        tenantdata.ItemsSource = null;
                        tenantdata.ItemsSource = tenanttype;

                    }
                    foreach (var item in Leasetypelist.Children)
                    {

                        foreach (var item1 in leasetype)
                        {
                            if (item1.type == "0")
                            {
                                item1.image = "RadioButtonChecked.png";
                                Filter_RT.leaseperiod = "0";
                            }
                            else
                            {
                                item1.image = "RadioButtonUnChecked.png";
                            }
                        }
                        Leasedata.ItemsSource = null;
                        Leasedata.ItemsSource = leasetype;

                    }
                    foreach (var item in Petlist.Children)
                    {

                        foreach (var item1 in petType)
                        {
                            if (item1.type == "999")
                            {
                                item1.image = "RadioButtonChecked.png";
                                Filter_RT.petpreference = "999";
                            }
                            else
                            {
                                item1.image = "RadioButtonUnChecked.png";
                            }
                        }
                        Petdata.ItemsSource = null;
                        Petdata.ItemsSource = petType;

                    }
                    foreach (var item in Vegpreferencelayout.Children)
                    {
                        foreach (var item1 in vegtype)
                        {
                            if (item1.type == "0")
                            {
                                item1.image = "RadioButtonChecked.png";
                                Filter_RT.vegpreference = "0";
                            }
                            else
                            {
                                item1.image = "RadioButtonUnChecked.png";
                            }
                        }
                        Vegdata.ItemsSource = null;
                        Vegdata.ItemsSource = vegtype;

                    }
                    foreach (var item in Furnishlayout.Children)
                    {
                        foreach (var item1 in Furnishtype)
                        {
                            if (item1.type == "999")
                            {
                                item1.image = "RadioButtonChecked.png";
                                Filter_RT.furnish = "999";
                            }
                            else
                            {
                                item1.image = "RadioButtonUnChecked.png";
                            }
                        }
                        Furnishdata.ItemsSource = null;
                        Furnishdata.ItemsSource = Furnishtype;

                    }
                    foreach (var item in Noofbedlayout.Children)
                    {
                        foreach (var item1 in noofbhk)
                        {
                            if (item1.type == "1")
                            {
                                item1.image = "RadioButtonChecked.png";
                                Filter_RT.noofbed = item1.type;
                            }
                            else
                            {
                                item1.image = "RadioButtonUnChecked.png";
                            }
                        }
                        Noofbeddata.ItemsSource = null;
                        Noofbeddata.ItemsSource = noofbhk;

                    }

                    foreach (var item in Bathlayout.Children)
                    {

                        foreach (var item1 in Bathtype)
                        {
                            if (item1.type == "1")
                            {
                                item1.image = "RadioButtonChecked.png";
                                Filter_RT.noofbath = "1";
                            }
                            else
                            {
                                item1.image = "RadioButtonUnChecked.png";
                            }
                        }
                        Noofbathdata.ItemsSource = null;
                        Noofbathdata.ItemsSource = Bathtype;

                    }

                    foreach (var item in Amenitieslayout.Children)
                    {
                        if (item.GetType() == chbx.GetType())
                        {
                            var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                            if (cat.IsChecked == true)
                            {
                                cat.IsChecked = false;
                            }
                        }

                    }
                    dialogs.HideLoading();
                }
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        public int changecitytypeimgnum = 0;
        private string _Tlocationtxt = "";
        public string Tlocationtxt
        {
            get { return _Tlocationtxt; }
            set { _Tlocationtxt = value; OnPropertyChanged(nameof(Tlocationtxt)); }
        }
       
        private string _Zipcode;
        public string Zipcode
        {
            get { return _Zipcode; }
            set { _Zipcode = value; }
        }

        private string _changezipcode;
        public string changezipcode
        {
            get { return _changezipcode; }
            set { _changezipcode = value; }
        }
        private double _latitude;
        public double latitude
        {
            get { return _latitude; }
            set { _latitude = value; OnPropertyChanged(nameof(latitude)); }
        }
        private double _longitude;
        public double longitude
        {
            get { return _longitude; }
            set { _longitude = value; OnPropertyChanged(nameof(longitude)); }
        }
        private List<NRIApp.Roommates.Features.Post.Models.Location> _LocationList { get; set; }
        public List<NRIApp.Roommates.Features.Post.Models.Location> LocationList
        {
            get { return _LocationList; }
            set
            {
                _LocationList = value;
            }
        }

        private void changecitytapcmd(object sender, EventArgs e)
        {
            if (changecitytypeimgnum == 0)
            {
                changecitytypeimg.Source = "minusGrey.png";
                changecitytypeimgnum = 1;
                changecitylayout.IsVisible = true;
            }
            else
            {
                changecitytypeimg.Source = "downarrowGrey.png";
                changecitytypeimgnum = 0;
                changecitylayout.IsVisible = false;
            }
            locationlist.IsVisible = false;
        }
        private void Txtlocation_PropertyChanged(object sender, TextChangedEventArgs e)
        {
            if (txtlocation.Text != Tlocationtxt)
            {
                GetLocationname();
            }
        }
        public async void GetLocationname()
        {
            try
            {
                Cityurl = "";
                changezipcode = "";
                var LocationAPI = RestService.For<LocalJobs.Features.Listing.Interfaces.ILocalJob>(Commonsettings.LocaljobsAPI);
                LocalJobs.Features.Listing.Models.LocationOverallData response = await LocationAPI.getoverallLocation(txtlocation.Text, "0");
                if (response != null && response.ROW_DATA.Count > 0)
                {
                        Cityurl = response.ROW_DATA.Last().newcityurl;
                    OnPropertyChanged(nameof(LocationList));
                    locationlist.IsVisible = true;
                    listdata.ItemsSource = response.ROW_DATA;
                }

            }
            catch (Exception e)
            {
            }
        }
        private void LocationCmd(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var radiogrp = sender as ListView;
                LocalJobs.Features.Listing.Models.Locations lsow = new LocalJobs.Features.Listing.Models.Locations();
                lsow = (LocalJobs.Features.Listing.Models.Locations)radiogrp.SelectedItem;
                Tlocationtxt = lsow.city;
                txtlocation.Text = lsow.city;
                Cityurl = lsow.newcityurl;

                changezipcode = lsow.newcityurl;

                //StateName = lsow.statename;
                //StateCode = lsow.statecode;
                city = lsow.city;
                Zipcode = lsow.zipcode.ToString();
                latitude = lsow.latitude;
                longitude = lsow.longitude;
                string Country = lsow.country;
                locationlist.IsVisible = false;

                if (lsow.newcityurl != Commonsettings.Usercityurl)
                {
                    changelocationtickimg.IsVisible = true;
                }
                else
                {
                    changelocationtickimg.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }


    }
}