using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Rentals.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Package_RT : ContentPage
	{
       // NRIApp.Rentals.Features.Post.Models.RTPostFirst myneedpost = new NRIApp.Rentals.Features.Post.Models.RTPostFirst();
        public Package_RT (NRIApp.Rentals.Features.Post.Models.RTPostFirst pst)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Choose your package";
            BindingContext = new ViewModels.Package_VMRT(pst);
        }
        protected override bool OnBackButtonPressed()
        {
            var currentpage = GetCurrentpage();
            if (lcfsecond.postsuccessbackbtn == 0)
            {
                currentpage.Navigation.PopAsync();
            }
            else if (lcfsecond.postsuccessbackbtn == 1)
            {
                currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
               // currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            }
            return true;
        }

        private void Backbutton_Tapped(object obj, EventArgs e)
        {
            var currentpage = GetCurrentpage();
            if (lcfsecond.postsuccessbackbtn == 0)
            {
                currentpage.Navigation.PopAsync();
            }
            else if (lcfsecond.postsuccessbackbtn == 1)
            {
                currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
                currentpage.Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 1]);
            }
        }
        
        public Page GetCurrentpage()
        {
            var currentpg = App.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpg;
        }
    }
}