using NRIApp.MyNeeds.Features.Models;
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
    public partial class Replytoresponse : ContentPage
    {
        public Replytoresponse(Ad_Response_Data needData)
        {
            InitializeComponent();
            this.Title = "Response";
            BindingContext = new NRIApp.MyNeeds.Features.ViewModel.Replyresponse_VM(needData);
        }
    }
}