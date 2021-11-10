using Acr.UserDialogs;
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
	public partial class LS_LEAD_Thankyou : ContentPage
	{
        IUserDialogs _Dialog = UserDialogs.Instance;
        public LS_LEAD_Thankyou(string adid)
        {
           
            InitializeComponent();
            if (string.IsNullOrEmpty(adid))
            {
                adid = "0";
            }
            _Dialog.ShowLoading("");

            BindingContext = new ViewModels.LS_Leadform_VM(adid,"leadform");
        }
        
        public LS_LEAD_Thankyou()
        {
            InitializeComponent();
            string adid = "0";
            BindingContext = new ViewModels.LS_Leadform_VM("","respform");
        }
        private void Closecommand(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
            try {
                 curpage.Navigation.PopToRootAsync();
            }
            catch (Exception ee)
            {

            }
           
            return true;
        }

    }
}