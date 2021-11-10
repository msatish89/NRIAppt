using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.MyNeeds.Features.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyNeeds : ContentPage
    {
        public MyNeeds()
        {
            InitializeComponent();
            BindingContext = new NRIApp.MyNeeds.Features.ViewModel.MyNeed_VM();
            Title = "My Needs";
        }
       
    }
}