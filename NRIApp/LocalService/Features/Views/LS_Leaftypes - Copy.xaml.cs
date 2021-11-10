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
    public partial class LS_Leaftypes : ContentPage
    {
        IUserDialogs _Dialog = UserDialogs.Instance;
        int _servicetype, supertagid; string supertag = string.Empty;
        public LS_Leaftypes(string ptag, int tagid, int servicetype, int ltype = 0, int stagid = 0, string stag = null)
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

                        AsNum.XFControls.CheckBox chb = new AsNum.XFControls.CheckBox();
                        chb.OnImg = "CheckBoxChecked.png";
                        chb.OffImg = "CheckBoxUnChecked.png";
                        chb.Text = item.tag;
                        chb.ShowLabel = true;
                        chb.CheckChanged += Chb_CheckChanged;
                        chb.ClassId = item.tagid.ToString();
                        layoutinnercheckbox.Children.Add(chb);
                        //CheckBox chb = new CheckBox();
                        //chb.Title = item.tag;
                        //chb.Value = item.tagid.ToString();
                        //chb.BorderImageSource = "CheckBoxUnChecked.png";
                        //chb.CheckedBackgroundImageSource = "CheckBoxUnChecked.png";
                        //chb.CheckmarkImageSource = "CheckBoxChecked.png";
                        //chb.CheckedChangedCommand = ViewModels.LS_Leaftypes.checkedclik;
                        //layoutinnercheckbox.Children.Add(chb);
                    }


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
                    AsNum.XFControls.CheckBox chbx = new AsNum.XFControls.CheckBox();
                    chbx.OnImg = "CheckBoxChecked.png";
                    chbx.OffImg = "CheckBoxUnChecked.png";
                    chbx.Text = selectedleaf.First().tag;
                    chbx.Checked = true;
                    chbx.ShowLabel = true;
                    chbx.ClassId = selectedleaf.First().tagid.ToString();
                    chbx.CheckChanged += Chb_CheckChanged;
                    layoutinnercheckbox.Children.Add(chbx);
                    //CheckBox ch = new CheckBox();
                    //ch.Title = selectedleaf.First().tag;
                    //ch.BorderImageSource = "CheckBoxChecked.png";
                    //ch.CheckedBackgroundImageSource = "CheckBoxChecked.png";
                    //ch.CheckmarkImageSource = "CheckBoxChecked.png";
                    //ch.Value = selectedleaf.First().tagid.ToString();
                    //layoutinnercheckbox.Children.Add(ch);
                    //layoutinnercheckbox.Children.First().GestureRecognizers.First().PropertyChanged += LS_Leaftypes_PropertyChanged;
                    ViewModels.LS_Leaftypes.Checkedvalues = selectedleaf.First().tagid.ToString() + ",";
                    ViewModels.LS_Leaftypes.Checkedtext = selectedleaf.First().tag + ",";

                    foreach (var item in unselectedleaf)
                    {
                        AsNum.XFControls.CheckBox chb = new AsNum.XFControls.CheckBox();
                        chb.OnImg = "CheckBoxChecked.png";
                        chb.OffImg = "CheckBoxUnChecked.png";
                        chb.Text = item.tag;
                        chb.ShowLabel = true;
                        chb.CheckChanged += Chb_CheckChanged;
                        chb.ClassId = item.tagid.ToString();
                        layoutinnercheckbox.Children.Add(chb);
                        //CheckBox chb = new CheckBox();
                        //chb.Title = item.tag;
                        //chb.BorderImageSource = "CheckBoxUnChecked.png";
                        //chb.CheckedBackgroundImageSource = "CheckBoxUnChecked.png";
                        //chb.CheckmarkImageSource = "CheckBoxChecked.png";
                        //chb.Value = item.tagid.ToString();
                        //layoutinnercheckbox.Children.Add(chb);
                    }


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

        private void Chb_CheckChanged(object sender, EventArgs e)
        {
            var vm = BindingContext as LS_Leaftypes;
            var b = sender as AsNum.XFControls.CheckBox;

            if (b.Checked)
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

        public void Chb_CheckChanged(AsNum.XFControls.CheckBox sender, EventArgs e)
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


                    _Dialog.ShowLoading("", null);


                    var curpage = ViewModels.LS_ViewModel.GetCurrentPage();

                    if (_servicetype == 2)
                    {
                        //await curpage.Navigation.PushAsync(new Views.Posting.LS_Posting(ltprimarytag.Text, ltprimarytagid.Text, val, text));

                    }
                    else
                    {
                        // await curpage.Navigation.PushAsync(new Views.Leadform.LS_LeadForm(supertagid, supertag, ltprimarytag.Text, ltprimarytagid.Text, val, text));
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