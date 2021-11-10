using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Posting.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Spammer : ContentPage
	{
		public Spammer ()
		{
			InitializeComponent ();
		}
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            Navigation.PopToRootAsync();
            return true;
        }
    }
}