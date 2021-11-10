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
	public partial class LS_Flex_Order_Summary : ContentPage
	{
      
        SELECTED_PACKAGE pack = new SELECTED_PACKAGE();
        string Primeid = "", sguid = ""; decimal finalamount = 0;
        IUserDialogs _Dialog = UserDialogs.Instance;
        public LS_Flex_Order_Summary(LS_FLEX_SELECTED_RESULT_DATA selectdata,LS_FLEX_SELECT_PACKAGE spack)
		{
			InitializeComponent ();
            Title = "Order Summary";
            packageamnt.Text =Convert.ToString(spack.package);
            getPrimeid(selectdata.guid);
            sguid = selectdata.guid;
            //finalamount =Convert.ToDecimal(spack.package);
            lblprimarycity.Text = LS_ViewModel.primarytag;

            pack.amount = Convert.ToDecimal(spack.package);
            
            //pack.mcamount = spack.mcamount;
            //pack.mcoriginalamt = spack.mcoriginalamt;
            //pack.mcsavepercentage = spack.mcsavepercentage;
            //pack.mcsaveperday = spack.mcsaveperday;
            //pack.noofdays = spack.noofdays;
            //pack.packageamount = spack.packageamount;
            //pack.valueserviceamount = spack.valueserviceamount;
            //pack.addtionalcitiesamount = spack.addtionalcitiesamount;
            //entrycoupon.Text = "";



            //if (Convert.ToInt32(pack.noofdays)>15)
            //{
            frmaecouponblk.IsVisible = true;
            //}
            //else
            //{
            //    frmaecouponblk.IsVisible = false;
            //}
            
            singlecitydays.Text = spack.noofdays + " Days";
            totalamntfinal.Text= totalpaymbleamount.Text = "$" + Convert.ToString(spack.package);
        }
        LS_PRIME_BY_GUID_DATA lpbg = new LS_PRIME_BY_GUID_DATA();
        public async void getPrimeid(string guid)
        {
            try
            {
                var choosepackagedata = RestService.For<ILS_Prime_Form>(Commonsettings.Localservices);
                var result = await choosepackagedata.Getprimedata(guid);
                if(result != null)
                {
                    LS_Posting_VM.sBusinessname = result.ROW_DATA[0].title;
                  lpbg.primeid=  Primeid = result.ROW_DATA[0].primeid;
                    lpbg.addon_amount = result.ROW_DATA[0].addon_amount;
                    lpbg.altmobileno = result.ROW_DATA[0].altmobileno;
                    lpbg.billingemail = result.ROW_DATA[0].billingemail;
                    lpbg.billingstreetname = result.ROW_DATA[0].billingstreetname;
                    lpbg.billingtype = result.ROW_DATA[0].billingtype;
                    lpbg.city = result.ROW_DATA[0].city;
                    lpbg.cityurl = result.ROW_DATA[0].cityurl;
                    lpbg.clientapprsaleswip = result.ROW_DATA[0].clientapprsaleswip;
                    lpbg.clientswipeallowvia = result.ROW_DATA[0].clientswipeallowvia;
                    lpbg.contactname = result.ROW_DATA[0].contactname;
                    lpbg.country = result.ROW_DATA[0].country;
                    lpbg.couponcode = result.ROW_DATA[0].couponcode;
                    lpbg.days = result.ROW_DATA[0].days;
                    lpbg.discountamount = result.ROW_DATA[0].discountamount;
                    lpbg.email = result.ROW_DATA[0].email;
                    lpbg.guid = result.ROW_DATA[0].guid;
                    lpbg.lat = result.ROW_DATA[0].lat;
                    lpbg.linkstatus = result.ROW_DATA[0].linkstatus;
                    lpbg.metro = result.ROW_DATA[0].metro;
                    lpbg.metrourl = result.ROW_DATA[0].metrourl;
                    lpbg.mobileno = result.ROW_DATA[0].mobileno;
                    lpbg.oldclpostid = result.ROW_DATA[0].oldclpostid;
                    lpbg.OrderID = result.ROW_DATA[0].OrderID;
                    lpbg.overall_amount = result.ROW_DATA[0].overall_amount;
                    lpbg.overall_amount_disc = result.ROW_DATA[0].overall_amount_disc;
                    lpbg.packagetype = result.ROW_DATA[0].packagetype;
                    lpbg.package_amount = result.ROW_DATA[0].package_amount;
                    lpbg.package_name = result.ROW_DATA[0].package_name;
                    lpbg.packassured = result.ROW_DATA[0].packassured;
                    lpbg.paid_amount = result.ROW_DATA[0].paid_amount;
                    lpbg.passed_amount = result.ROW_DATA[0].passed_amount;
                    lpbg.pricingtypevia = result.ROW_DATA[0].pricingtypevia;
                    lpbg.ptagid = result.ROW_DATA[0].ptagid;
                    lpbg.salesemail = result.ROW_DATA[0].salesemail;
                    lpbg.statecode = result.ROW_DATA[0].statecode;
                    lpbg.streetname = result.ROW_DATA[0].streetname;
                    lpbg.upddate = result.ROW_DATA[0].upddate;
                    lpbg.zipcode = result.ROW_DATA[0].zipcode;
                }
            }
            catch(Exception e)
            {

            }
        }
        private async void Backbtncommand(object sender, EventArgs e)
        {
            var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
            var _lastPage = curpage.Navigation.NavigationStack.LastOrDefault();
            Navigation.RemovePage(_lastPage);
            await Application.Current.MainPage.Navigation.PopAsync();
          // await curpage.Navigation.PushAsync(new PrimePakage(pack.amount, "", pack));
        }

       

     

       

        private async void Btncoupon_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(entrycoupon.Text))
                {
                    _Dialog.ShowLoading("");
                    var couponservice= RestService.For<ILS_Prime_Form>(NRIApp.Helpers.Commonsettings.TechjobsAPI);
                    COUPON_DATA ca = new COUPON_DATA()
                    {
                        noofdays=pack.noofdays,
                        couponcode= entrycoupon.Text,
                        mccitycount=LS_ViewModel.addionalcities.Count,
                        addonordertype = LS_ViewModel.Valueaddedcontentid,
                        email=""


                    };
                    var couponsuccess = await couponservice.ApplyFlex_couponcode(sguid,Primeid, entrycoupon.Text);
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
                            lpbg.couponcode = entrycoupon.Text;
                        }
                        else
                        {
                            lblcouponsuccess.Text = "Coupon not valid";
                            lblcouponsuccess.TextColor = Color.Red;
                            lpbg.couponcode = "";
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
           // LS_Value_Services.totalamount = pack.amount;
            lpbg.couponcode = "";

                frmaecouponblk.IsVisible = true;
            
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

                }; var curpage = LS_ViewModel.GetCurrentPage();
                await curpage.Navigation.PushAsync(new LocalService.Features.Views.Posting.LS_Payment(lpbg,pack));
                //var orderinsert = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);
                //var data =await orderinsert.OrderSummaryInsert(ods);
                //if (data.ROW_DATA !=null)
                //{
                //    if (data.ROW_DATA.First().result== "success")
                //    {
                //        var curpage = LS_ViewModel.GetCurrentPage();
                //        await curpage.Navigation.PushAsync(new LocalService.Features.Views.Posting.LS_Payment(pack.amount, data.ROW_DATA.FirstOrDefault().adid));
                //    }
                //    else
                //    {
                //        _Dialog.Toast("Please try again!");
                //    }


                //}
                //else
                //{
                //    _Dialog.Toast("Please try again!");
                //}

                _Dialog.HideLoading();
            }
            catch(Exception ee)
            {

            }
          

        }

        //protected  override bool OnBackButtonPressed()
        //{
        //    var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
        //    var _lastPage = curpage.Navigation.NavigationStack.LastOrDefault();
        //    Navigation.RemovePage(_lastPage);
        //    curpage.Navigation.PushAsync(new LS_Value_Services(pack.amount, "", pack));
        //    return true;
        //}
       
    }
}