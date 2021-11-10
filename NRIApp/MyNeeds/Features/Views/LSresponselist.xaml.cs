using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.MyNeeds.Features.Models;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.MyNeeds.Features.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LSresponselist : ContentPage
    {
        IUserDialogs dialog = UserDialogs.Instance;
        string oldpostID = "", adID = "";
        public LSresponselist(string oldclpostid,string adid)
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            adID = adid;
            oldpostID = oldclpostid;
            Title = "Responses";
            getlsresponselist();
        }
        public async void getlsresponselist()
        {
            try
            {
                dialog.ShowLoading("", null);
                var Adresponse = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                string email = Commonsettings.UserEmail;
                var AdresponseData = await Adresponse.getlsresponse(email.Replace("@","^"));
                foreach(var dta in AdresponseData.RowData)
                {

                    string s = dta.Email;
                    string[] emailarr= s.Split('@');
                    dta.Email = "XXXXXX" + "@"+ emailarr;
                }
                responseLSListview.ItemsSource = AdresponseData.RowData;
                dialog.HideLoading();
                //if free hide email
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }

        private void gotonribusinessapp(object sender, EventArgs e)
        {
           try
            {
               NRIApp.LocalService.Features.ViewModels.LS_ViewModel.isrenewclickble = 1;
               Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Posting.PrimePakage(oldpostID, adID));

            }
            catch (Exception ex)
            {
            }
        }
    }
}