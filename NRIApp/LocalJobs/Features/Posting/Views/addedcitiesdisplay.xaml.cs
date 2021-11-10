using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRIApp.LocalJobs.Features.Posting.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.LocalJobs.Features.Posting.Models;

namespace NRIApp.LocalJobs.Features.Posting.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class addedcitiesdisplay : ContentPage
	{
        public int changedcities = 0; string removedcities = "";
        IUserDialogs _Dialog = UserDialogs.Instance;
        public decimal percityamnt;
        public decimal pkgamnt;
        public string addbanneramnt;
        public string addnationwideamnt;
        public string pkg = "";
        //addon
        public string emailblastamountt = "";
        public string addonamountt = "";
        public decimal perdaycostamountt ;
        public int daycounttxtID;


        public string topbannerbtntxt = "";
        public string excmailerbtntxt = "";
        public string jobslotbtntxt = "";
        public string bonusdaybtntxt = "";

        Postingdata pd = new Postingdata();
                                              
        public addedcitiesdisplay (Postingdata data, string package, string amnt, decimal cityamnt, string banneramnt, string nationwideamnt,string emailblastamount,string addonamount,string perdaycostamount,int daycounttxt, string topbannerbtntxtid, string excmailerbtntxtid, string jobslotbtntxtid, string bonusdaybtntxtid)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            //Title = "Expand to more cities";
            Title = "Selected cities";
            Bindselectedcities();
            pd = data;
            percityamnt = cityamnt;
            addbanneramnt = banneramnt;
            emailblastamountt = emailblastamount;
            addnationwideamnt = nationwideamnt;
            addonamountt = addonamount;
            topbannerbtntxt = topbannerbtntxtid;
            excmailerbtntxt = excmailerbtntxtid;
            jobslotbtntxt = jobslotbtntxtid;
            bonusdaybtntxt = bonusdaybtntxtid;
            daycounttxtID = daycounttxt;
            if (!string.IsNullOrEmpty(perdaycostamount))
            {
                perdaycostamountt = Convert.ToDecimal(perdaycostamount);
            }
            pkg = package;
            pkgamnt = Convert.ToDecimal(amnt.Replace("$", ""));

        }

        public async void Bindselectedcities()
        {
            try
            {
                _Dialog.ShowLoading("");
                await Task.Delay(2000);
                var curpage = JobPackage_VM.GetCurrentModalPage();
                var stack = curpage.FindByName<StackLayout>("stackaddtionalcities");
                foreach (var item in JobPackage_VM.addionalcities)
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
           try
            {
                changedcities = 1;
                var chk = sender as Plugin.InputKit.Shared.Controls.CheckBox;
                if (chk.IsChecked==true)
                {
                    if (!JobPackage_VM.addionalcities.ContainsKey(chk.ClassId))
                    {
                        JobPackage_VM.addionalcities.Add(chk.ClassId, chk.Text);

                        if (JobPackage_VM.addionalcities.Count == 1)
                        {
                            var curpage = JobPackage_VM.GetCurrentPage();
                            var stack = curpage.FindByName<StackLayout>("stackmodify");
                            stack.IsVisible = true;
                            //Citiestext = "Totally " + JobPackage_VM.addionalcities.Count + " city selected by you";
                        }
                        else
                        {
                            // Citiestext = "Totally " + JobPackage_VM.addionalcities.Count + " citie(s) selected by you";
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
                    JobPackage_VM.addionalcities.Remove(chk.ClassId);



                }
            }
            catch(Exception ex)
            {

            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
           try
            {
                var curmodpage = JobPackage_VM.GetCurrentModalPage();
                var curpage = JobPackage_VM.GetCurrentPage();
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
                                if (item != "" && !JobPackage_VM.addionalcities.ContainsKey(item.Split('|')[0]))
                                {
                                    JobPackage_VM.addionalcities.Add(item.Split('|')[0], item.Split('|')[1]);
                                }

                            }
                        }
                    }

                }

                await curmodpage.Navigation.PopModalAsync();

                await curpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.additionalcities(pd, pkg, pkgamnt.ToString(), percityamnt, addbanneramnt, addnationwideamnt, "cities", emailblastamountt, addonamountt, perdaycostamountt, daycounttxtID, topbannerbtntxt, excmailerbtntxt, jobslotbtntxt, bonusdaybtntxt));
                curpage.Navigation.RemovePage(curpage.Navigation.NavigationStack[curpage.Navigation.NavigationStack.Count - 2]);
            }
            catch(Exception ex)
            {

            }
        }
        protected  override bool OnBackButtonPressed()
        {
            try
            {
                var curmodpage = JobPackage_VM.GetCurrentModalPage();
                var curpage = JobPackage_VM.GetCurrentPage();
                curmodpage.Navigation.PopModalAsync();
                curpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.additionalcities(pd, pkg, pkgamnt.ToString(), percityamnt, addbanneramnt, addnationwideamnt, "cities", emailblastamountt, addonamountt, perdaycostamountt, daycounttxtID, topbannerbtntxt, excmailerbtntxt, jobslotbtntxt, bonusdaybtntxt));
                curpage.Navigation.RemovePage(curpage.Navigation.NavigationStack[curpage.Navigation.NavigationStack.Count - 2]);
            }
            catch (Exception ex)
            {

            }
            return true;

        }
        private async void btn_leafsubmit(object sender, EventArgs e)
        {
            var curmodpage = JobPackage_VM.GetCurrentModalPage();
            var curpage = JobPackage_VM.GetCurrentPage();
            await curmodpage.Navigation.PopModalAsync();
            await curpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.additionalcities(pd, pkg, pkgamnt.ToString(), percityamnt, addbanneramnt, addnationwideamnt, "cities", emailblastamountt, addonamountt, perdaycostamountt, daycounttxtID, topbannerbtntxt, excmailerbtntxt, jobslotbtntxt, bonusdaybtntxt));
            curpage.Navigation.RemovePage(curpage.Navigation.NavigationStack[curpage.Navigation.NavigationStack.Count - 2]);
        }
    }
}