using NRIApp.DayCare.Features.List.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.DayCare.Features.List.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResponseForm : ContentPage
	{
        DClistings lsdata = new DClistings();
        public string pgeID = "";
        public ResponseForm (DClistings data, string pageid)
        {
			InitializeComponent ();
            lsdata = data; pgeID = pageid;
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Get Connected";
            BindingContext = new ViewModels.ResponseForm_VM(data, pageid);
        }
        
        private void gototc(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (NRIApp.DayCare.Features.Post.Views.PostSecond.otppgid == 1)
            {
                //var vm = new ViewModels.ResponseForm_VM(lsdata,pgeID);
                //lsdata.description = reasondesc.Text;
                //vm.responsesubmit();
                // Navigation.PushAsync(new NRIApp.DayCare.Features.List.Views.Thankyou_DC());
                NRIApp.DayCare.Features.Post.Views.PostSecond.otppgid = 0;
                Navigation.PopAsync();
            }
            else
            {
                NRIApp.DayCare.Features.Post.Views.PostSecond.otppgid = 0;
            }

        }
    }
}