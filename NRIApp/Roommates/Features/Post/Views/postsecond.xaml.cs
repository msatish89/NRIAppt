using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NRIApp.Roommates.Features.Post.Models;
using NRIApp.Roommates.Features.Post.ViewModels;
using System.IO;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace NRIApp.Roommates.Features.Post.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class postsecond : ContentPage
    {
        public static List<string> multiImageurlList = new List<string>();

        public static string multiimageurl;

        public postsecond(Postfirst obj, int adid, int agentid, string ordertype)
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            if(obj.ismyneed=="1")
            {
                pricemodetxt.TextColor = Color.FromHex("#212121");
                leasetypetxt.TextColor = Color.FromHex("#212121");
                accomodatestxt.TextColor = Color.FromHex("#212121");
                attachedbathtxt.TextColor = Color.FromHex("#212121");
                // obj.Imagepath = "roommates_2019-09-23-01-22-41-127_10113184.jpeg,roommates_2019-07-23-01-44-38-128_10032400.jpeg";
                if (!string.IsNullOrEmpty(obj.Imagepath))
                {
                    string imagename = "";
                    VMPost.AdmultiImage = new List<string>();
                    string[] img = obj.Imagepath.Split(',');
                    foreach (var image in img)
                    {
                        imagename = "http://usimg.sulekhalive.com/cdn/roommates/images/" + image;
                        VMPost.AdmultiImage.Add(imagename);
                    }
                }
                multiimglist.ItemsSource = null;
                multiimglist.ItemsSource = VMPost.AdmultiImage;
            }
            BindingContext = new VMPost(obj, adid, agentid, ordertype);
            Title = "Enter Room Details";
        }

       
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }


        private void gototc(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://us.sulekha.com/terms"));
        }

    }
}