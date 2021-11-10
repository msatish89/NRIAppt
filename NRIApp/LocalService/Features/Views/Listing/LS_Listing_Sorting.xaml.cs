using NRIApp.LocalService.Features.Models;
using NRIApp.LocalService.Features.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalService.Features.Views.Listing
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LS_Listing_Sorting : ContentPage
    {

        public LS_Listing_Sorting()
        {
            InitializeComponent();
            Title = "Sort By";
            BindingContext = new ViewModels.LS_Listing_Search("", "sort", "2");
        }

    }
}