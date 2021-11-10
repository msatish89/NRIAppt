using Acr.UserDialogs;
using NRIApp.Controls;
using NRIApp.Helpers;
using NRIApp.LocalService.Features.Interfaces;
using NRIApp.LocalService.Features.Models;
using Plugin.Connectivity;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views.Leadform
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LS_RespForm : ContentPage
    {
        IUserDialogs _Dialog = UserDialogs.Instance;

        public LS_RespForm(string adid, string premiumad)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == false)
                Navigation.PushModalAsync(new NRIApp.USHome.Views.Nointernet());
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Enter Contact Details";
            string ptagid = ViewModels.LS_ViewModel.primarytagid;
            BindingContext = new ViewModels.LS_Listing_VM(ptagid, premiumad, adid, "respform");
            Bindservicesbyads(adid);



        }
        private List<SERVICE_LEAF_TYPES_DATA> _getadservices { get; set; }
        public List<SERVICE_LEAF_TYPES_DATA> getadservices
        {
            get { return _getadservices; }

            set
            {
                _getadservices = value;
                OnPropertyChanged(nameof(getadservices));
            }
        }
        public void gototc()
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }
        public async void Bindservicesbyads(string adid)
        {
            try
            {
                _Dialog.ShowLoading("", null);
                var listingdata = RestService.For<ILS_Listing>(Commonsettings.Localservices);
                var data = await listingdata.GetServiceLeaftypes(adid);
                List<SERVICE_LEAF_TYPES_DATA> selectedleaf = new List<SERVICE_LEAF_TYPES_DATA>();
                getadservices = data.ROW_DATA.Where(i => i.ltypeid == 3).ToList();
                if (getadservices.Count == 0)
                {
                    getadservices = data.ROW_DATA.Where(i => i.ltypeid == 2).ToList();
                }
                if (data.ROW_DATA != null && data.ROW_DATA.Count > 0)
                {
                    foreach (var item in getadservices)
                    {
                        Plugin.InputKit.Shared.Controls.CheckBox chb = new Plugin.InputKit.Shared.Controls.CheckBox(); chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;chb.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check; chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;
                        //chb.OnImg = "CheckBoxChecked.png";
                        //chb.OffImg = "CheckBoxUnChecked.png";
                        chb.Text = item.tag;
                        //chb.ShowLabel = true;
                        chb.CheckChanged += Chb_CheckChanged;
                        chb.ClassId = item.tagid.ToString();
                        stackadservices.Children.Add(chb);
                    }
                }
            }
            catch (Exception ex)
            {
                _Dialog.HideLoading();
            }
            _Dialog.HideLoading();
        }
        private void Chb_CheckChanged(object sender, EventArgs e)
        {
            var vm = BindingContext as LS_Leaftypes;
            var b = sender as Plugin.InputKit.Shared.Controls.CheckBox;

            if (b.IsChecked==true)
            {
                if (ViewModels.LS_Listing_VM.Checkedservicevalues != null && ViewModels.LS_Listing_VM.Checkedservicevalues != "")
                {
                    if (!ViewModels.LS_Listing_VM.Checkedservicevalues.Contains(b.ClassId))
                    {
                        ViewModels.LS_Listing_VM.Checkedservicevalues += b.ClassId + ",";
                    }
                }
                else
                {

                    ViewModels.LS_Listing_VM.Checkedservicevalues += b.ClassId + ",";
                }
                if (ViewModels.LS_Listing_VM.Checkedservicetext != null && ViewModels.LS_Listing_VM.Checkedservicetext != "")
                {
                    if (!ViewModels.LS_Listing_VM.Checkedservicetext.Contains(b.Text))
                    {
                        ViewModels.LS_Listing_VM.Checkedservicetext += b.Text + ",";
                    }
                }
                else
                {

                    ViewModels.LS_Listing_VM.Checkedservicetext += b.Text + ",";
                }


            }
            else
            {

                string[] indexvalue = ViewModels.LS_Listing_VM.Checkedservicevalues.Split(',');
                int numIndex = Array.IndexOf(indexvalue, b.ClassId.ToString());
                indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                ViewModels.LS_Listing_VM.Checkedservicevalues = string.Join(",", indexvalue);

                string[] indextext = ViewModels.LS_Listing_VM.Checkedservicetext.Split(',');
                int txtIndex = Array.IndexOf(indextext, b.Text);
                indextext = indextext.Where((val, idx) => idx != txtIndex).ToArray();
                ViewModels.LS_Listing_VM.Checkedservicetext = string.Join(",", indextext);


            }
            string[] chkcount = ViewModels.LS_Listing_VM.Checkedservicevalues.Split(',');
            if (chkcount.Length > 0)
            {
                if (chkcount.Length - 1 == 0)
                {
                    uselectservice.Text = "Select Service";
                }
                else if (chkcount.Length - 1 == 1)
                {
                    uselectservice.Text = "1 Service Type Selected";
                }
                else
                {

                    uselectservice.Text = chkcount.Length - 1 + " Service Types Selected";
                }

            }
        }

        public void On_Clicked_Service(object sender, EventArgs e)
        {

            var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();
            Entry entry = CurrentPage.FindByName<Entry>("uname");

            if (stackadservices.IsVisible)
            {
                stackadservices.IsVisible = false;

            }
            else
            {
                stackadservices.IsVisible = true;

            }
        }
        private void Selectservice_Tapped(object sender, EventArgs e)
        {
            if (stackadservices.IsVisible)
            {
                stackadservices.IsVisible = false;
            }
            else
            {
                stackadservices.IsVisible = true;
            }
        }

    }
}