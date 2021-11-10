﻿using NRIApp.LocalService.Features.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.LocalService.Features.Views;
using NRIApp.Helpers;
using NRIApp.LocalService.Features.Interfaces;
using Refit;
using Acr.UserDialogs;
using Plugin.Connectivity;

namespace NRIApp.LocalService.Features.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LS_Leaftypes_prime : ContentPage
    {
        IUserDialogs _Dialog = UserDialogs.Instance;
        int _servicetype, supertagid; string supertag = string.Empty;
        public LS_Leaftypes_prime(string ptag, int tagid, int servicetype, int ltype = 0, int stagid = 0, string stag = null)
        {
           
            Title = ptag;
            InitializeComponent();

            _servicetype = servicetype;
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new ViewModels.LS_Leaftypes(tagid);

            ViewModels.LS_Leaftypes.Checkedvalues = "";
            ViewModels.LS_Leaftypes.Checkedtext = "";
            if (ltype == 2)
            {
                supertagid = stagid; supertag = stag;
                ltprimarytagid.Text = tagid.ToString();
                Title = ltprimarytag.Text = ptag;
                BindingLeaftypes(tagid);
            }
            else
            {
                supertagid = stagid; supertag = stag;
                ltprimarytagid.Text = stagid.ToString();
                Title = ltprimarytag.Text = stag;
                BindingSelectedLeaftypes(tagid, stagid);
            }
        }
        private List<LEAF_TAGS_DATA> _getlsleafdata { get; set; }
        public List<LEAF_TAGS_DATA> getlsleafdata
        {
            get { return _getlsleafdata; }

            set
            {
                _getlsleafdata = value;
                OnPropertyChanged(nameof(getlsleafdata));
            }
        }
        public async void BindingLeaftypes(int id)
        {
            try
            {
                _Dialog.ShowLoading("", null);
                var listingdata = RestService.For<IServiceAPI>(Commonsettings.Localservices);
                var data = await listingdata.GetLeaftypes(id);
                _getlsleafdata = data.ROW_DATA;

                if (getlsleafdata != null && getlsleafdata.Count > 0)
                {

                    foreach (var item in data.ROW_DATA)
                    {
                        BoxView box = new BoxView();
                        box.BackgroundColor = Color.FromHex("e0e0e0");
                        box.HeightRequest = 1;
                        Plugin.InputKit.Shared.Controls.CheckBox chb = new Plugin.InputKit.Shared.Controls.CheckBox(); chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;chb.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check; chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;
                        //chb.OnImg = "CheckBoxChecked.png";
                        //chb.OffImg = "CheckBoxUnChecked.png";
                        chb.Text = item.tag;
                        //chb.ShowLabel = true;
                        chb.CheckChanged += Chb_CheckChanged;
                        chb.ClassId = item.tagid.ToString();
                        chb.Padding = 10;
                        layoutinnercheckbox.Children.Add(chb);
                        layoutinnercheckbox.Children.Add(box);
                       
                    }


                }
                else
                {
                    string val = ViewModels.LS_Leaftypes.Checkedvalues, text = ViewModels.LS_Leaftypes.Checkedtext;
                    var curpage = ViewModels.LS_ViewModel.GetCurrentPage();

                    if (_servicetype == 2)
                    {
                        await curpage.Navigation.PushAsync(new Views.Posting.LS_Posting(Convert.ToString(supertagid), supertag, ltprimarytag.Text, ltprimarytagid.Text, val, text));

                    }
                    else
                    {
                        await curpage.Navigation.PushAsync(new Views.Leadform.LS_LeadForm(supertagid, supertag, ltprimarytag.Text, ltprimarytagid.Text, val, text));
                    }
                }
                _Dialog.HideLoading();
            }
            catch (Exception e)
            {
                _Dialog.HideLoading();
            }
        }
        public async void BindingSelectedLeaftypes(int leaftagid, int stagid)
        {
            try
            {

                _Dialog.ShowLoading("");
                var listingdata = RestService.For<IServiceAPI>(Commonsettings.Localservices);
                var data = await listingdata.GetLeaftypes(stagid);

                List<LEAF_TAGS_DATA> selectedleaf = new List<LEAF_TAGS_DATA>();

                List<LEAF_TAGS_DATA> unselectedleaf = new List<LEAF_TAGS_DATA>();
                selectedleaf = data.ROW_DATA.Where(i => i.tagid == leaftagid).ToList();
                unselectedleaf = data.ROW_DATA.Where(i => i.tagid != leaftagid).ToList();

                if (data.ROW_DATA != null && data.ROW_DATA.Count > 0)
                {
                    BoxView boxx = new BoxView();
                    boxx.BackgroundColor = Color.FromHex("e0e0e0");
                    boxx.HeightRequest = 1;
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    chbx.Text = selectedleaf.First().tag;
                    chbx.IsChecked = true;
                    //chbx.ShowLabel = true;
                    chbx.ClassId = selectedleaf.First().tagid.ToString();
                    chbx.CheckChanged += Chb_CheckChanged;
                    chbx.Padding = 10;
                    layoutinnercheckbox.Children.Add(chbx);
                    layoutinnercheckbox.Children.Add(boxx);
                    ViewModels.LS_Leaftypes.Checkedvalues = selectedleaf.First().tagid.ToString() + ",";
                    ViewModels.LS_Leaftypes.Checkedtext = selectedleaf.First().tag + ",";

                    foreach (var item in unselectedleaf)
                    {
                        BoxView box = new BoxView();
                        box.BackgroundColor = Color.FromHex("e0e0e0");
                        box.HeightRequest = 1;
                        Plugin.InputKit.Shared.Controls.CheckBox chb = new Plugin.InputKit.Shared.Controls.CheckBox(); chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;chb.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check; chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;
                        //chb.OnImg = "CheckBoxChecked.png";
                        //chb.OffImg = "CheckBoxUnChecked.png";
                        chb.Text = item.tag;
                        //chb.ShowLabel = true;
                        chb.CheckChanged += Chb_CheckChanged;
                        chb.ClassId = item.tagid.ToString();
                        chb.Padding = 10;
                        layoutinnercheckbox.Children.Add(chb);
                        layoutinnercheckbox.Children.Add(box);
                    }


                }
                else
                {
                    string val = ViewModels.LS_Leaftypes.Checkedvalues, text = ViewModels.LS_Leaftypes.Checkedtext;
                    var curpage = ViewModels.LS_ViewModel.GetCurrentPage();

                    if (_servicetype == 2)
                    {
                        await curpage.Navigation.PushAsync(new Views.Posting.LS_Posting(Convert.ToString(supertagid), supertag, ltprimarytag.Text, ltprimarytagid.Text, val, text));

                    }
                    else
                    {
                        await curpage.Navigation.PushAsync(new Views.Leadform.LS_LeadForm(supertagid, supertag, ltprimarytag.Text, ltprimarytagid.Text, val, text));
                    }
                }
                await Task.Delay(4000);
                _Dialog.HideLoading();
            }
            catch (Exception e)
            {
                _Dialog.HideLoading();
            }
        }

        private void Chb_CheckChanged(object sender, EventArgs e)
        {
            var vm = BindingContext as LS_Leaftypes;
            var b = sender as Plugin.InputKit.Shared.Controls.CheckBox;

            if (b.IsChecked==true)
            {
                if (ViewModels.LS_Leaftypes.Checkedvalues != null && ViewModels.LS_Leaftypes.Checkedvalues != "")
                {
                    if (!ViewModels.LS_Leaftypes.Checkedvalues.Contains(b.ClassId))
                    {
                        ViewModels.LS_Leaftypes.Checkedvalues += b.ClassId + ",";
                    }
                }
                else
                {

                    ViewModels.LS_Leaftypes.Checkedvalues += b.ClassId + ",";
                }
                if (ViewModels.LS_Leaftypes.Checkedtext != null && ViewModels.LS_Leaftypes.Checkedtext != "")
                {
                    if (!ViewModels.LS_Leaftypes.Checkedtext.Contains(b.Text))
                    {
                        ViewModels.LS_Leaftypes.Checkedtext += b.Text + ",";
                    }
                }
                else
                {

                    ViewModels.LS_Leaftypes.Checkedtext += b.Text + ",";
                }


            }
            else
            {

                string[] indexvalue = ViewModels.LS_Leaftypes.Checkedvalues.Split(',');
                int numIndex = Array.IndexOf(indexvalue, b.ClassId.ToString());
                indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                ViewModels.LS_Leaftypes.Checkedvalues = string.Join(",", indexvalue);

                string[] indextext = ViewModels.LS_Leaftypes.Checkedtext.Split(',');
                int txtIndex = Array.IndexOf(indextext, b.Text);
                indextext = indextext.Where((val, idx) => idx != txtIndex).ToArray();
                ViewModels.LS_Leaftypes.Checkedtext = string.Join(",", indextext);


            }
        }

        public void Chb_CheckChanged(Plugin.InputKit.Shared.Controls.CheckBox sender, EventArgs e)
        {

            var vm = BindingContext as LS_Leaftypes;
            // vm.checklist(sender);
        }

        private void LS_Leaftypes_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public async void btn_leafsubmit(object sender, EventArgs e)
        {

            try
            {
                string val = ViewModels.LS_Leaftypes.Checkedvalues, text = ViewModels.LS_Leaftypes.Checkedtext;
                if (val != null && val != "")
                {
                   
                    
                    ViewModels.LS_ViewModel.filterprimarytagid = ViewModels.LS_Leaftypes.Checkedvalues;
                    ViewModels.LS_ViewModel.filterprimarytag = ViewModels.LS_Leaftypes.Checkedtext;

                    _Dialog.ShowLoading("", null);


                    var curpage = ViewModels.LS_ViewModel.GetCurrentPage();

                    if (_servicetype == 2)
                    {
                       await curpage.Navigation.PushAsync(new Views.Posting.LS_Posting(Convert.ToString(supertagid), supertag, ltprimarytag.Text, ltprimarytagid.Text, val, text));

                    }
                    else
                    {
                     await curpage.Navigation.PushAsync(new Views.Leadform.LS_Prime(supertagid, supertag, ltprimarytag.Text, ltprimarytagid.Text, val, text));
                    }
                    _Dialog.HideLoading();
                }
                else
                {
                    _Dialog.Toast("Please choose atleast one category");
                }

            }
            catch (Exception ex) { _Dialog.HideLoading(); }
        }

    }
}