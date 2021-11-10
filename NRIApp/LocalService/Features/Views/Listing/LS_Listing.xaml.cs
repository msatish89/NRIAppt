using NRIApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views.Listing
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LS_Listing : ContentPage
    {
        private int _lastItemAppearedIdx;
        public LS_Listing()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");

            string ptagid = "", cityurl = Commonsettings.Usercityurl, ptag = "";
            if (ViewModels.LS_Listing_Search.issearchtext == 1)
            {
                ptag = ViewModels.LS_ViewModel.searchtag;
                if (!string.IsNullOrEmpty(ViewModels.LS_ViewModel.filterprimarytagid))
                {
                    ptagid = ViewModels.LS_ViewModel.filterprimarytagid;
                }
                else
                {
                    ptagid = ViewModels.LS_ViewModel.searchtagid;
                }

            }
            else if (!string.IsNullOrEmpty(ViewModels.LS_ViewModel.filterprimarytagid))
            {
                ptagid = ViewModels.LS_ViewModel.filterprimarytagid;
                ptag = ViewModels.LS_ViewModel.searchtag;
            }
            else
            {
                ptagid = ViewModels.LS_ViewModel.primarytagid;
                ptag = ViewModels.LS_ViewModel.primarytag;
            }
            IUserDialogs Dialogs = UserDialogs.Instance;
            Dialogs.ShowLoading("");

            Title = ptag + " in " + Commonsettings.Usercity + ", " + Commonsettings.Userstatecode;
            BindingContext = new NRIApp.LocalService.Features.ViewModels.LS_Listing_VM(ptagid, cityurl);
        }
       

    }
}