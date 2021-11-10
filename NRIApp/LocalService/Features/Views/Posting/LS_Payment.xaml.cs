using NRIApp.LocalService.Features.Models;
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
    public partial class LS_Payment : ContentPage
    {
        public LS_Payment(LS_PRIME_BY_GUID_DATA lpbg, SELECTED_PACKAGE sp)
        {
            InitializeComponent();
          //  amount = 150;adid = "1264301";
            BindingContext = new LocalService.Features.ViewModels.LS_Flex_Payment_VM(lpbg, sp);
            Title = "Payment";
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            lblprice.Text = Convert.ToString("$"+sp.amount);
            lblamnt.Text = Convert.ToString("$" + sp.amount);
        }
        public LS_Payment(decimal amount, string adid)
        {

        }
    }
}