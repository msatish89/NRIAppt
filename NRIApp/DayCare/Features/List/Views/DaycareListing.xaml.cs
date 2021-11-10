using NRIApp.DayCare.Features.List.Models;
using NRIApp.DayCare.Features.Post.Models;
using NRIApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NRIApp.DayCare.Features.List.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DaycareListing : ContentPage
	{
        //private string _title = "";
        //public string title
        //{
        //    get { return _title; }
        //    set { _title = value;OnPropertyChanged(nameof(title)); }
        //}
        DClistinfo data = new DClistinfo();
        public DaycareListing (DClistinfo list)
		{
			InitializeComponent ();
            data = list;
            list.miles = "0";
            this.BindingContext = new List.ViewModels.DaycareListingVM(list);
            //if(!string.IsNullOrEmpty(list.careprovidertype) && (list.careprovidertype!= "cyber-security"))
            //{
            //    string titlename = char.ToUpper(list.careprovidertype[0]) + list.careprovidertype.Substring(1);//list.careprovidertype.Substring(0, 1).ToUpper();
            //    title = titlename.Replace("-", " ") + " in " + list.city;
            //    OnPropertyChanged(nameof(title));
            //}
		}
        protected override void OnAppearing()
        {
            try
            {
                if (!string.IsNullOrEmpty(data.careprovidertype) && (data.careprovidertype != "cyber-security"))
                {
                    string titlename = char.ToUpper(data.careprovidertype[0]) + data.careprovidertype.Substring(1);//list.careprovidertype.Substring(0, 1).ToUpper();
                    Title = titlename.Replace("-", " ") + " in " + data.city;
                    OnPropertyChanged(nameof(Title));
                }

                //OnPropertyChanged(nameof(title));
                //base.OnAppearing();
                ViewModels.DaycareListingVM.filtercnt = 0;
                ViewModels.DaycareListingVM.sortingcnt = 0;
                //NRIApp.LocalJobs.Features.Detail.Views.Responsetq.dtlpagecode = 0;
                if (ViewModels.DaycareListingVM.logincode == 1 && Commonsettings.UserPid != "0" && Commonsettings.UserPid != null && Commonsettings.UserPid != "")
                {
                    Navigation.PushAsync(new NRIApp.DayCare.Features.List.Views.Savedads());
                }
            }
            catch (Exception ex)
            {

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}