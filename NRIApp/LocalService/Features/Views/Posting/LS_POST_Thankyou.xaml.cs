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
    public partial class LS_POST_Thankyou : ContentPage
    {
        public LS_POST_Thankyou(string oldclpostid = "",string primeid="")
        {
            InitializeComponent();

           BindingContext = new LS_Flex_Payment_VM(oldclpostid, primeid);
        }
        private void Postsuccessclose(object sender, EventArgs e)
        {

            // Navigation.PushAsync(new NRIApp.LocalService.Features.Views.LS_Servicetype());try{
            try
            {
                Navigation.PopToRootAsync();
            }catch(Exception ee) { }

            LS_ViewModel.packageamount.Clear();
            LS_ViewModel.packagevalueamountdetails.Clear();
            LS_ViewModel.addionalcities.Clear();
            LS_ViewModel.valueadded = 0;
            LS_ViewModel.Valueaddedcontentid = "";

           
        }

        //private void Viewad_Tapped(object sender, EventArgs e)
        //{
        //    Uri uri = new Uri(lblurl.Text.ToString());
        //    Device.OpenUri(uri);
        //    //var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
        //    //curpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Detail.LS_Detail());

        //}
        protected override bool OnBackButtonPressed()
        {
            var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
            curpage.Navigation.PopToRootAsync();
            return true;
        }
    }
}