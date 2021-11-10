using NRIApp.LocalService.Features.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views.Detail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Online_Thankyou : ContentPage
    {
        public Online_Thankyou(string paymentgudid)
        {
            InitializeComponent();

            BindingContext = new LS_Online_Class_Payment_VM(paymentgudid);
        }
        private async void Postsuccessclose(object sender, EventArgs e)
        {
            var curpage = LS_ViewModel.GetCurrentPage();
            // curpage.Navigation.RemovePage(curpage.Navigation.NavigationStack[curpage.Navigation.NavigationStack.Count - 2]);

            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
        }

       
        protected  override bool OnBackButtonPressed()
        {

            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            return false;
        }
    }
}