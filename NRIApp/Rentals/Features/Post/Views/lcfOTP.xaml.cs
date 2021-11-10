using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.Rentals.Features.Post.ViewModels;
using NRIApp.Rentals.Features.Post.Models;

namespace NRIApp.Rentals.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class lcfOTP : ContentPage
	{
		public lcfOTP (RTResponse res, string pinno)
		{
			InitializeComponent ();
            BindingContext = new VMRTLCF(res, pinno);
            newcountrycode.SelectedIndex = 0;
        }
        protected override bool OnBackButtonPressed()
        {
            Task.Run(async () =>
            {
                await Navigation.PopToRootAsync();
            });
            return true;
        }
    }
}