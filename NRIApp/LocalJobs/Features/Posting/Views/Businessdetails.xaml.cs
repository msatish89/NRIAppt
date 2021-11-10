using NRIApp.LocalJobs.Features.Posting.Models;
using NRIApp.LocalJobs.Features.Posting.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.LocalJobs.Features.Posting.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Businessdetails : ContentPage
	{
		public Businessdetails (Postingdata pdata)
		{
			InitializeComponent ();
            if (Device.RuntimePlatform == Device.iOS)
                NavigationPage.SetBackButtonTitle(this, "Back");
            Title = "Enter Business Details";
            if(pdata.disclosedbusiness=="1")
            {
                checkbximg.Source = "CheckBoxChecked.png";
            }

            if (pdata.ismyneed == "1")
            {
                string imagename = "";
                //"https://az827626.vo.msecnd.net/cdn/job/images/thumbnail/job_2021-03-10-02-20-52-678_10323033.jpeg" 
                if (!string.IsNullOrEmpty(pdata.imagepath))
                {
                   
                  //  BusinessdetailsVM.AdmultiImage = new List<string>();
                    string[] img = pdata.imagepath.Split(';');
                    foreach (var image in img)
                    {
                        imagename = "https://az827626.vo.msecnd.net/cdn/job/images/thumbnail/" + image;
                        //BusinessdetailsVM.AdmultiImage.Add(imagename);
                    }
                }
                profileimage.Source = imagename;
            }

            this.BindingContext = new BusinessdetailsVM(pdata);
		}

        private void Uaddress_Focused(object sender, FocusEventArgs e)
        {
            stackbiz.IsVisible = false;
        }
    }
}