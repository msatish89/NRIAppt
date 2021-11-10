using NRIApp.LocalService.Features.Models;
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
    public partial class LS_Leaftypes_ : ContentPage
    {
        IUserDialogs _Dialog = UserDialogs.Instance;
        int _servicetype, supertagid; string supertag = string.Empty;
        public LS_Leaftypes_(string ptag, int tagid, int servicetype, int ltype = 0, int stagid = 0, string stag = null)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == false)
                Navigation.PushModalAsync(new NRIApp.USHome.Views.Nointernet());
            Title = ptag;
            InitializeComponent();
           
            _servicetype = servicetype;
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new ViewModels.LS_Leaftypes(tagid);
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
                        CheckBox chb = new CheckBox();
                        chb.Title = item.tag;
                        chb.Value = item.tagid.ToString();
                        chb.BorderImageSource = "CheckBoxUnChecked.png";
                        chb.CheckedBackgroundImageSource = "CheckBoxUnChecked.png";
                        chb.CheckmarkImageSource = "CheckBoxChecked.png";
                        layoutinnercheckbox.Children.Add(chb);
                    }


                }
                else
                {
                    string val = ViewModels.LS_Leaftypes.Checkedvalues, text = ViewModels.LS_Leaftypes.Checkedtext;
                    var curpage = ViewModels.LS_ViewModel.GetCurrentPage();

                    if (_servicetype == 2)
                    {
                        await curpage.Navigation.PushAsync(new Views.Posting.LS_Posting(ltprimarytag.Text, ltprimarytagid.Text, val,text));

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

                _Dialog.ShowLoading("", null);
                var listingdata = RestService.For<IServiceAPI>(Commonsettings.Localservices);
                var data = await listingdata.GetLeaftypes(stagid);

                List<LEAF_TAGS_DATA> selectedleaf = new List<LEAF_TAGS_DATA>();

                List<LEAF_TAGS_DATA> unselectedleaf = new List<LEAF_TAGS_DATA>();
                selectedleaf = data.ROW_DATA.Where(i => i.tagid == leaftagid).ToList();
                unselectedleaf = data.ROW_DATA.Where(i => i.tagid != leaftagid).ToList();

                if (data.ROW_DATA != null && data.ROW_DATA.Count > 0)
                {
                    CheckBox ch = new CheckBox();
                    ch.Title = selectedleaf.First().tag;
                    ch.BorderImageSource = "CheckBoxUnChecked.png";
                    ch.CheckedBackgroundImageSource = "CheckBoxUnChecked.png";
                    ch.CheckmarkImageSource = "CheckBoxChecked.png";
                    ch.IsChecked = true;
                    ch.Value = selectedleaf.First().tagid.ToString();
                    layoutinnercheckbox.Children.Add(ch);

                    ViewModels.LS_Leaftypes.Checkedvalues = selectedleaf.First().tagid.ToString() + ",";
                    ViewModels.LS_Leaftypes.Checkedtext = selectedleaf.First().tag + ",";

                    foreach (var item in unselectedleaf)
                    {
                        CheckBox chb = new CheckBox();
                        chb.Title = item.tag;
                        chb.BorderImageSource = "CheckBoxUnChecked.png";
                        chb.CheckedBackgroundImageSource = "CheckBoxUnChecked.png";
                        chb.CheckmarkImageSource = "CheckBoxChecked.png";
                        chb.Value = item.tagid.ToString();
                        layoutinnercheckbox.Children.Add(chb);
                    }

                    //foreach (var item in data.ROW_DATA)
                    //{
                    //    CheckBox chb = new CheckBox();
                    //    chb.Title = item.tag;
                    //    if (item.tagid  == leaftagid)
                    //    {
                    //        chb.BorderImageSource = "CheckBoxUnChecked.png";
                    //        chb.CheckedBackgroundImageSource = "CheckBoxUnChecked.png";
                    //        chb.CheckmarkImageSource = "CheckBoxChecked.png";
                    //        chb.IsChecked = true;
                    //    }
                    //    else { 
                    //    chb.BorderImageSource = "CheckBoxUnChecked.png";
                    //    chb.CheckedBackgroundImageSource = "CheckBoxUnChecked.png";
                    //    chb.CheckmarkImageSource = "CheckBoxChecked.png";
                    //    }
                    //    chb.Value = item.tagid.ToString();
                    //    layoutinnercheckbox.Children.Add(chb);

                    //}

                }
                else
                {
                    string val = ViewModels.LS_Leaftypes.Checkedvalues, text = ViewModels.LS_Leaftypes.Checkedtext;
                    var curpage = ViewModels.LS_ViewModel.GetCurrentPage();

                    if (_servicetype == 2)
                    {
                        await curpage.Navigation.PushAsync(new Views.Posting.LS_Posting(ltprimarytag.Text, ltprimarytagid.Text, val, text));

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
        public async void btn_leafsubmit(object sender, EventArgs e)
        {

            try
            {
                string val = ViewModels.LS_Leaftypes.Checkedvalues, text = ViewModels.LS_Leaftypes.Checkedtext;
                if (val != null && val != "")
                {


                    _Dialog.ShowLoading("", null);


                    var curpage = ViewModels.LS_ViewModel.GetCurrentPage();

                    if (_servicetype == 2)
                    {
                        await curpage.Navigation.PushAsync(new Views.Posting.LS_Posting(ltprimarytag.Text, ltprimarytagid.Text, val, text));

                    }
                    else
                    {
                        await curpage.Navigation.PushAsync(new Views.Leadform.LS_LeadForm(supertagid, supertag, ltprimarytag.Text, ltprimarytagid.Text, val, text));
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