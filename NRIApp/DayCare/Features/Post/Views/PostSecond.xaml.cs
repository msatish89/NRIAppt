using NRIApp.DayCare.Features.Post.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.DayCare.Features.Post.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PostSecond : ContentPage
	{
        public static int otppgid = 0;
		public PostSecond (Daycareposting postdata)
		{
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            if (postdata.ismyneed == "1")
            {
                // obj.Imagepath = "roommates_2019-09-23-01-22-41-127_10113184.jpeg,roommates_2019-07-23-01-44-38-128_10032400.jpeg";
                if (!string.IsNullOrEmpty(postdata.photo))
                {
                    if(postdata.Secondarycategoryvalueurl== "nanny-babysitter")
                    {
                        stackprfimg.IsVisible = true;
                        profileimg.Source = postdata.photo;
                    }
                    else
                    {
                        string imagename = "";
                        ViewModels.PostSecond_VM.AdmultiImage = new List<string>();
                        string[] img = postdata.photo.Split(',');
                        foreach (var image in img)
                        {
                            //imagename = "http://usimg.sulekhalive.com/cdn/roommates/images/" + image;
                            imagename = image;
                            ViewModels.PostSecond_VM.AdmultiImage.Add(imagename);
                        }
                        multiimglist.ItemsSource = null;
                        multiimglist.ItemsSource = ViewModels.PostSecond_VM.AdmultiImage;
                    }
                    
                }
                
            }
            BindingContext = new ViewModels.PostSecond_VM(postdata);
            if (postdata.supercategoryid == "1")
            {
                titletxt.Placeholder = "Enter title which describes your work";
                Descriptiontxt.Placeholder = "Describe your work experience & special tasks involved";
            }
            else
            {
                titletxt.Placeholder = "Enter a title about your care needs";
                Descriptiontxt.Placeholder = "Describe your care requirement";
            }
            Title = "Care Details";
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            otppgid = 0;
        }
    }
}