using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.Rentals.Features.Post.Models;
using NRIApp.Rentals.Features.Post.ViewModels;

namespace NRIApp.Rentals.Features.Post.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class postsecond : ContentPage
    {
        public postsecond(RTPostFirst obj, int adid, int agentid, string ordertype)
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            
            if (obj.ismyneed == "1")
            {
                pricemodetxt.TextColor = Color.FromHex("#212121");
                leasetypetxt.TextColor = Color.FromHex("#212121");
                accomodatestxt.TextColor = Color.FromHex("#212121");
                attachedbathtxt.TextColor = Color.FromHex("#212121");
                parkingfrequency.TextColor = Color.FromHex("#212121");
                parkingtype.TextColor = Color.FromHex("#212121");
                // obj.Imagepath = "roommates_2019-09-23-01-22-41-127_10113184.jpeg,roommates_2019-07-23-01-44-38-128_10032400.jpeg";
                if (!string.IsNullOrEmpty(obj.Imagepath))
                {
                    string imagename = "";
                    VMRTPost.AdmultiImage = new List<string>();
                    string[] img = obj.Imagepath.Split(',');
                    foreach (var image in img)
                    {
                        imagename = "http://usimg.sulekhalive.com/cdn/roommates/images/" + image;
                        VMRTPost.AdmultiImage.Add(imagename);
                    }
                }
                multiimglist.ItemsSource = null;
                multiimglist.ItemsSource = VMRTPost.AdmultiImage;
            }
            BindingContext = new VMRTPost(obj, adid, agentid, ordertype);
            Title = "Enter Room Details";
        }
        public void gototc()
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }


    }
}