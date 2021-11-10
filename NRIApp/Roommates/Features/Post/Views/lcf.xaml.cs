using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.Roommates.Features.Post.Models;
using NRIApp.Roommates.Features.Post.ViewModels;

namespace NRIApp.Roommates.Features.Post.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class lcf : ContentPage
    {
        public lcf(CategoryList obj)
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            BindingContext = new VMLCF(obj);
            //lblcat.Text = obj.categoryname;
            if(obj.ismyneed=="1")
            {
                pricemodetxt.TextColor = Color.FromHex("#212121");
                leasetypetxt.TextColor = Color.FromHex("#212121");
                accomodatestxt.TextColor = Color.FromHex("#212121");
                attachedbathtxt.TextColor = Color.FromHex("#212121");
                //parkingfrequency.TextColor = Color.FromHex("#212121");
                //parkingtype.TextColor = Color.FromHex("#212121");
            }
            Title = "Enter Availability & Rent";
        }

       // private void Txtrent_Focused(object sender, FocusEventArgs e)
       // {
           // var vm = this.BindingContext as VMLCF;
            // vm?.rentpopup();
       // }
    }
}