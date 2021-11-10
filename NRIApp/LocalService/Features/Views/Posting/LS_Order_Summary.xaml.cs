using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.LocalService.Features.Interfaces;
using NRIApp.LocalService.Features.Models;
using NRIApp.LocalService.Features.ViewModels;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views.Posting
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LS_Order_Summary : ContentPage
	{
      
        SELECTED_PACKAGE pack = new SELECTED_PACKAGE();
        IUserDialogs _Dialog = UserDialogs.Instance;
        public LS_Order_Summary (SELECTED_PACKAGE spack)
		{
			InitializeComponent();
            Title = "Order Summary";
            lblprimarycity.Text = "Primary city - "+ LS_ViewModel.Bizcity;
            pack.amount = spack.amount;
            pack.mcamount = spack.mcamount;
            pack.mcoriginalamt = spack.mcoriginalamt;
            pack.mcsavepercentage = spack.mcsavepercentage;
            pack.mcsaveperday = spack.mcsaveperday;
            pack.noofdays = spack.noofdays;
            pack.packageamount = spack.packageamount;
            pack.valueserviceamount = spack.valueserviceamount;
            pack.addtionalcitiesamount = spack.addtionalcitiesamount;
            entrycoupon.Text = "";

            if (LS_ViewModel.addionalcities.Count>0)
            {
                boxaddtionalcities.IsVisible=stackaddtionalcities.IsVisible = true;
                if(LS_ViewModel.addionalcities.Count == 1)
                {
                    citycntlabel.Text = LS_ViewModel.addionalcities.Count + " addtional city";
                }
                else
                {
                    citycntlabel.Text = LS_ViewModel.addionalcities.Count + " addtional cities";
                }
                

                if(LS_ViewModel.addionalcities.Count - 1 == 0)
                {
                    citylistlbl.Text = LS_ViewModel.addionalcities.FirstOrDefault().Value ;
                }
                else
                {
                    citylistlbl.Text = LS_ViewModel.addionalcities.FirstOrDefault().Value + ", +" + (LS_ViewModel.addionalcities.Count - 1) + " more";
                }
                
                addtionalcityamount.Text = "$" + pack.addtionalcitiesamount;
                addtionalcitydays.Text = pack.noofdays + " Days";
            }
            if (LS_ViewModel.valueadded !=0 && pack.valueserviceamount !=0)
            {
                boxvalueaddservices.IsVisible = stackvalueaddservices.IsVisible = true;
                valueamntlbl.Text = "$" + Convert.ToString(pack.valueserviceamount);
                valuedayslbl.Text = pack.noofdays + " Days";

            }
            if (Convert.ToInt32(pack.noofdays)>15)
            {
                frmaecouponblk.IsVisible = true;
            }
            else
            {
                frmaecouponblk.IsVisible = false;
            }
            if (LS_ViewModel.addionalcities.Count == 0&& LS_ViewModel.valueadded==0)
            {
                pack.amount = pack.packageamount;
            }
                singlecityamnt.Text = Convert.ToString(pack.packageamount);
            singlecitydays.Text = pack.noofdays + " Days";
            totalamntfinal.Text= totalpaymbleamount.Text = "$" + Convert.ToString(pack.amount);
        }
        //private async void Backbtncommand(object sender, EventArgs e)
        //{
        //    var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
        //    var _lastPage = curpage.Navigation.NavigationStack.LastOrDefault();
        //    Navigation.RemovePage(_lastPage);
        //    await Application.Current.MainPage.Navigation.PopAsync();
        //   await curpage.Navigation.PushAsync(new LS_Value_Services(pack.amount, "", pack));
        //}

        private async void Tapchangecity_Tapped(object sender, EventArgs e)
        {
            var curpage = LS_ViewModel.GetCurrentPage();
            await curpage.Navigation.PushAsync(new LocalService.Features.Views.Posting.LS_Additional_Cities(LS_ViewModel.addionalcities.Count, pack));
        }

        private async void Removecities_Tapped(object sender, EventArgs e)
        {
            _Dialog.ShowLoading("");
            boxaddtionalcities.IsVisible = stackaddtionalcities.IsVisible = false;

            pack.amount = pack.packageamount + pack.valueserviceamount + pack.mcamount * LS_ViewModel.addionalcities.Count ;
            pack.amount = pack.amount - pack.addtionalcitiesamount;
            pack.addtionalcitiesamount = 0;
            totalamntfinal.Text = totalpaymbleamount.Text = "$" + Convert.ToString(pack.amount);
          
            LS_ViewModel.addionalcities.Clear();
            if (!string.IsNullOrEmpty(entrycoupon.Text))
            {
                var couponservice = RestService.For<IServiceAPI>(NRIApp.Helpers.Commonsettings.TechjobsAPI);
                COUPON_DATA ca = new COUPON_DATA()
                {
                    noofdays = pack.noofdays,
                    couponcode = entrycoupon.Text,
                    mccitycount = LS_ViewModel.addionalcities.Count,
                    addonordertype = LS_ViewModel.Valueaddedcontentid,
                    email = ""


                };
                var couponsuccess = await couponservice.Apply_couponcode(ca);
                if (couponsuccess.ROW_DATA.Count > 0)
                {
                    if (couponsuccess.ROW_DATA[0].couponstatus == "valid")
                    {
                        lblcouponsuccess.Text = "Coupon applied successfully";
                        lblcouponsuccess.TextColor = Color.Green;
                        pack.couponamount = couponsuccess.ROW_DATA[0].discountamount;
                        pack.amount = pack.amount - couponsuccess.ROW_DATA[0].discountamount;
                        totalamntfinal.Text = totalpaymbleamount.Text = "$" + Convert.ToString(pack.amount);
                        lblcouponamt.Text = "$" + couponsuccess.ROW_DATA[0].discountamount;
                        boxcoupon.IsVisible = stackcoupon.IsVisible = true;
                        frmaecouponblk.IsVisible = false;
                    }
                    else
                    {
                        lblcouponsuccess.Text = "Coupon not valid";
                        lblcouponsuccess.TextColor = Color.Red;
                    }
                }
            }
          //  LS_Value_Services.totalamount = pack.amount;
            _Dialog.HideLoading();
        }

        private async void Removevalueservices_Tapped(object sender, EventArgs e)
        {
            _Dialog.ShowLoading("");
            boxvalueaddservices.IsVisible = stackvalueaddservices.IsVisible = false;
            pack.amount = pack.packageamount + pack.mcamount * LS_ViewModel.addionalcities.Count+pack.valueserviceamount;
            pack.amount = pack.amount - pack.valueserviceamount;
            pack.valueserviceamount = 0;
            LS_ViewModel.Valueaddedcontentid = "";
            LS_ViewModel.valueadded = 0;

            if (!string.IsNullOrEmpty(entrycoupon.Text))
            {
                var couponservice = RestService.For<IServiceAPI>(NRIApp.Helpers.Commonsettings.TechjobsAPI);
                COUPON_DATA ca = new COUPON_DATA()
                {
                    noofdays = pack.noofdays,
                    couponcode = entrycoupon.Text,
                    mccitycount = LS_ViewModel.addionalcities.Count,
                    addonordertype = LS_ViewModel.Valueaddedcontentid,
                    email = ""


                };
                var couponsuccess = await couponservice.Apply_couponcode(ca);
                if (couponsuccess.ROW_DATA.Count > 0)
                {
                    if (couponsuccess.ROW_DATA[0].couponstatus == "valid")
                    {
                        lblcouponsuccess.Text = "Coupon applied successfully";
                        lblcouponsuccess.TextColor = Color.Green;
                        pack.couponamount = couponsuccess.ROW_DATA[0].discountamount;
                        pack.amount = pack.amount - couponsuccess.ROW_DATA[0].discountamount;
                        totalamntfinal.Text = totalpaymbleamount.Text = "$" + Convert.ToString(pack.amount);
                        lblcouponamt.Text = "$" + couponsuccess.ROW_DATA[0].discountamount;
                        boxcoupon.IsVisible = stackcoupon.IsVisible = true;
                        frmaecouponblk.IsVisible = false;
                    }
                    else
                    {
                        lblcouponsuccess.Text = "Coupon not valid";
                        lblcouponsuccess.TextColor = Color.Red;
                    }
                }
            }
            totalamntfinal.Text = totalpaymbleamount.Text = "$" + Convert.ToString(pack.amount);
            List<Page> li = Navigation.NavigationStack.ToList();
            Page last = li.ElementAt(li.Count - 1);
          //  LS_Value_Services.totalamount = pack.amount;

            _Dialog.HideLoading();
        }

        private async void Btncoupon_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(entrycoupon.Text))
                {
                    _Dialog.ShowLoading("");
                    var couponservice= RestService.For<IServiceAPI>(NRIApp.Helpers.Commonsettings.TechjobsAPI);
                    COUPON_DATA ca = new COUPON_DATA()
                    {
                        noofdays=pack.noofdays,
                        couponcode= entrycoupon.Text,
                        mccitycount=LS_ViewModel.addionalcities.Count,
                        addonordertype = LS_ViewModel.Valueaddedcontentid,
                        email=""


                    };
                    var couponsuccess = await couponservice.Apply_couponcode(ca);
                    if (couponsuccess.ROW_DATA.Count>0)
                    {
                        if (couponsuccess.ROW_DATA[0].couponstatus == "valid")
                        {
                            lblcouponsuccess.Text = "Coupon applied successfully";
                            lblcouponsuccess.TextColor = Color.Green;
                            pack.couponamount = couponsuccess.ROW_DATA[0].discountamount;
                            pack.amount = pack.amount - couponsuccess.ROW_DATA[0].discountamount;
                            totalamntfinal.Text = totalpaymbleamount.Text = "$" + Convert.ToString(pack.amount);
                            lblcouponamt.Text="$"+ couponsuccess.ROW_DATA[0].discountamount;
                            boxcoupon.IsVisible= stackcoupon.IsVisible = true;
                            frmaecouponblk.IsVisible = false;
                        }
                        else
                        {
                            lblcouponsuccess.Text = "Coupon not valid";
                            lblcouponsuccess.TextColor = Color.Red;
                        }
                    }
                    _Dialog.HideLoading();
                    
                }
                else
                {
                    _Dialog.Toast("Please enter coupon code");
                }
            }
            catch(Exception ee)
            {

            }
        }



        private void Removecoupon_Tapped(object sender, EventArgs e)
        {
            stackcoupon.IsVisible = false;
            boxcoupon.IsVisible = false;
            pack.amount = pack.amount + pack.couponamount;
            totalamntfinal.Text = totalpaymbleamount.Text = "$" + Convert.ToString(pack.amount);
            pack.couponamount = 0;
            lblcouponsuccess.Text = ""; entrycoupon.Text = "";
            //LS_Value_Services.totalamount = pack.amount;
            if (Convert.ToInt32(pack.noofdays) > 15)
            {
                frmaecouponblk.IsVisible = true;
            }
            else
            {
                frmaecouponblk.IsVisible = false;
            }
        }


        private async void BtnPayment_Clicked(object sender, EventArgs e)
        {
            try
            {
                _Dialog.ShowLoading("");
                string[] citiesarr = LS_ViewModel.addionalcities.Keys.ToArray();
                string cities = string.Join(",", citiesarr);

                string[] cityarr = LS_ViewModel.addionalcities.Values.ToArray();
                string citylist = string.Join(",", cityarr);

                string postedviaadid = "";

                if (Commonsettings.UserMobileOS.ToLower().Contains("iphone"))
                {
                    postedviaadid = "194";
                }
                else
                {
                    postedviaadid = "197";
                }
                ORDER_SUMMARY ods = new ORDER_SUMMARY()
                {
                    cityurl = cities,
                    city = citylist,
                    adid = LS_ViewModel.Adid,
                    couponcode = entrycoupon.Text,
                    userpid = Commonsettings.UserPid,
                    postedvia = Commonsettings.UserMobileOS.ToLower(),
                    postedviaid= postedviaadid,
                    addonordertype=LS_ViewModel.Valueaddedcontentid,
                    noofdays=pack.noofdays

                };
                var orderinsert = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);
                var data =await orderinsert.OrderSummaryInsert(ods);
                if (data.ROW_DATA !=null)
                {
                    if (data.ROW_DATA.First().result== "success")
                    {
                        var curpage = LS_ViewModel.GetCurrentPage();
                        await curpage.Navigation.PushAsync(new LocalService.Features.Views.Posting.LS_Payment(pack.amount, data.ROW_DATA.FirstOrDefault().adid));
                    }
                    else
                    {
                        _Dialog.Toast("Please try again!");
                    }
                  

                }
                else
                {
                    _Dialog.Toast("Please try again!");
                }

                _Dialog.HideLoading();
            }
            catch(Exception ee)
            {

            }
          

        }

        protected  override bool OnBackButtonPressed()
        {
            var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
            var _lastPage = curpage.Navigation.NavigationStack.LastOrDefault();
            Navigation.RemovePage(_lastPage);
       //     curpage.Navigation.PushAsync(new LS_Value_Services(pack.amount, "", pack));
            return true;
        }
       
    }
}