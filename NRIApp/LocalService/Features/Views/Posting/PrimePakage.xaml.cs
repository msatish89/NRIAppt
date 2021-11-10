using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.LocalService.Features.Models;
using NRIApp.LocalService.Features.ViewModels;

namespace NRIApp.LocalService.Features.Views.Posting
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrimePakage : ContentPage
    {
        public PrimePakage(string oldclpostid, string adid)
        {
            InitializeComponent();
            //  Bindpackagemonth();
            Title = "Choose your package";
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new NRIApp.LocalService.Features.ViewModels.LS_Flex_Package_VM(oldclpostid,adid);
           
           
        }
        protected override bool OnBackButtonPressed()
        {
            LS_ViewModel.packageamount.Clear();
            LS_ViewModel.packagevalueamountdetails.Clear();
            LS_ViewModel.addionalcities.Clear();
            LS_ViewModel.valueadded = 0;
            LS_ViewModel.Valueaddedcontentid = "";
            if (LS_ViewModel.isOTPpage == 1)
            {
                var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
                currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                LS_ViewModel.isOTPpage = 0;
            }
            return true;
        }



    }
}