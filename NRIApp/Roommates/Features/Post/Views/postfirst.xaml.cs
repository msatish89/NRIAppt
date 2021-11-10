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
    public partial class postfirst : ContentPage
    {
        public postfirst(CategoryList obj)
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            // lblcat.Text = obj.categoryname;
            
            BindingContext = new VMPost(obj);
            countrycode.SelectedIndex = 0;
            Title = "Enter Contact Details";
            if (obj.ismyneed == "1")
            {
                if (!obj.Countrycode.Contains("+"))
                {
                    countrycode.SelectedItem = "+" + obj.Countrycode;
                }
                else
                {
                    countrycode.SelectedItem = obj.Countrycode;
                }
                if (!string.IsNullOrEmpty(obj.Email))
                {
                    txtcontactemail.IsEnabled = false;
                }
              
            }
        }
        public void gototc()
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }
    }
}