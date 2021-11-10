using Acr.UserDialogs;
using NRIApp.LocalService.Features.Models;
using NRIApp.LocalService.Features.ViewModels;
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
    public partial class LS_Additional_Cities_Display : ContentPage
    {
        public int changedcities = 0; string removedcities = "";
        IUserDialogs _Dialog = UserDialogs.Instance;
        SELECTED_PACKAGE sPack = new SELECTED_PACKAGE();
        public LS_Additional_Cities_Display(SELECTED_PACKAGE sp)
        {
            InitializeComponent();
            Bindselectedcities();
            sPack.amount = sp.amount;
            sPack.mcamount = sp.mcamount;
            sPack.mcoriginalamt = sp.mcoriginalamt;
            sPack.mcsavepercentage = sp.mcsavepercentage;
            sPack.mcsaveperday = sp.mcsaveperday;
            sPack.noofdays = sp.noofdays;
            sPack.packageamount = sp.packageamount;
            sPack.valueserviceamount = sp.valueserviceamount;
        }
        public async void Bindselectedcities()
        {
            try
            {
                _Dialog.ShowLoading("");
                await Task.Delay(2000);
                var curpage = LS_ViewModel.GetCurrentModalPage();
                var stack = curpage.FindByName<StackLayout>("stackaddtionalcities");
                foreach (var item in LS_ViewModel.addionalcities)
                {
                    BoxView box = new BoxView();
                    box.BackgroundColor = Color.FromHex("e0e0e0");
                    box.HeightRequest = 1;

                    Plugin.InputKit.Shared.Controls.CheckBox chb = new Plugin.InputKit.Shared.Controls.CheckBox(); chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;chb.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check; chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;
                    chb.Text = item.Value;
                    //chb.OnImg = "CheckBoxChecked.png";
                    //chb.OffImg = "CheckBoxUnChecked.png";
                    chb.WidthRequest = 18;
                    chb.HeightRequest = 18;
                    //chb.ShowLabel = true;
                    chb.CheckChanged += Chb_CheckChanged;
                    chb.ClassId = item.Key;
                    chb.Padding = 5;
                    chb.IsChecked = true;

                    stack.Children.Add(chb);
                    stack.Children.Add(box);
                }

                _Dialog.HideLoading();

            }
            catch (Exception ee)
            {

            }

        }
        public void Chb_CheckChanged(object sender, EventArgs e)
        {
            changedcities = 1;
            var chk = sender as Plugin.InputKit.Shared.Controls.CheckBox;
            if (chk.IsChecked==true)
            {
                if (!LS_ViewModel.addionalcities.ContainsKey(chk.ClassId))
                {
                    LS_ViewModel.addionalcities.Add(chk.ClassId, chk.Text);

                    if (LS_ViewModel.addionalcities.Count == 1)
                    {
                        var curpage = LS_ViewModel.GetCurrentPage();
                        var stack = curpage.FindByName<StackLayout>("stackmodify");
                        stack.IsVisible = true;
                        //Citiestext = "Totally " + LS_ViewModel.addionalcities.Count + " city selected by you";
                    }
                    else
                    {
                        // Citiestext = "Totally " + LS_ViewModel.addionalcities.Count + " citie(s) selected by you";
                    }

                }
                else
                {
                    _Dialog.Toast("Already added " + chk.Text);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(removedcities))
                {
                    removedcities = chk.ClassId + "|" + chk.Text + ":";
                }
                else
                {
                    removedcities += chk.ClassId + "|" + chk.Text + ":";
                }
                LS_ViewModel.addionalcities.Remove(chk.ClassId);

                //var curpage = LS_ViewModel.GetCurrentPage();
                //var stack = curpage.FindByName<StackLayout>("stackmodify");
                //if (LS_ViewModel.addionalcities.Count > 0)
                //{


                //    stack.IsVisible = true;

                //    if (LS_ViewModel.addionalcities.Count == 1)
                //    {

                //        Citiestext = "Totally " + LS_ViewModel.addionalcities.Count + " city selected by you";
                //    }
                //    else
                //    {
                //        Citiestext = "Totally " + LS_ViewModel.addionalcities.Count + " citie(s) selected by you";
                //    }
                //}
                //else
                //{
                //    stack.IsVisible = false;
                //}


            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var curmodpage = LS_ViewModel.GetCurrentModalPage();
            var curpage = LS_ViewModel.GetCurrentPage();
            if (sPack.packageamount != sPack.amount)
            {
                sPack.amount = sPack.packageamount;
            }


            if (changedcities == 1)
            {
                var answer = await _Dialog.ConfirmAsync("", "Saved Changes?", "OK", "Cancel");
                if (!answer)
                {
                    if (!string.IsNullOrEmpty(removedcities))
                    {
                        string[] cities = removedcities.Split(':');

                        foreach (var item in cities)
                        {
                            if (item != "" && !LS_ViewModel.addionalcities.ContainsKey(item.Split('|')[0]))
                            {
                                LS_ViewModel.addionalcities.Add(item.Split('|')[0], item.Split('|')[1]);
                            }

                        }
                    }
                }

            }
            sPack.amount = sPack.amount + LS_ViewModel.addionalcities.Count * sPack.mcamount + sPack.valueserviceamount;


            await curmodpage.Navigation.PopModalAsync();

            await curpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Posting.LS_Additional_Cities(LS_ViewModel.addionalcities.Count, sPack));
            curpage.Navigation.RemovePage(curpage.Navigation.NavigationStack[curpage.Navigation.NavigationStack.Count - 2]);
        }
        private async void btn_leafsubmit(object sender, EventArgs e)
        {
            var curmodpage = LS_ViewModel.GetCurrentModalPage();
            var curpage = LS_ViewModel.GetCurrentPage();
            if (sPack.packageamount != sPack.amount)
            {
                sPack.amount = sPack.packageamount;
            }

            sPack.amount = sPack.amount + LS_ViewModel.addionalcities.Count * sPack.mcamount + sPack.valueserviceamount;

            await curmodpage.Navigation.PopModalAsync();

            await curpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Posting.LS_Additional_Cities(LS_ViewModel.addionalcities.Count, sPack));
            curpage.Navigation.RemovePage(curpage.Navigation.NavigationStack[curpage.Navigation.NavigationStack.Count - 2]);
        }
    }
}