using NRIApp.Events.Features.Listing.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.Events.Features
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class dynamicpicker : ContentPage
    {

        private readonly PensumCollection _collection = new PensumCollection();
        public dynamicpicker()
        {
            InitializeComponent();
            BindingContext = _collection;
            //BindingContext= new NRIApp.Events.Features.Listing.ViewModel.picker_vm();
        }
    }

    
}