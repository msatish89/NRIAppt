using Acr.UserDialogs;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Refit;
using NRIApp.Helpers;
using NRIApp.LocalService.Features.Interfaces;
using NRIApp.LocalService.Features.Models;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Linq;

namespace NRIApp.LocalService.Features.ViewModels
{
    public class LS_Onlineclasses_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs _Dialog = UserDialogs.Instance;
        public static List<Onlineclasses> classlistings { get; set; }
        IUserDialogs dialog = UserDialogs.Instance;
        public Command TapDetail { get; set; }
        public LS_Onlineclasses_VM(string tagurl)
        {
            Getonlineclasses("yoga-classes");
            TapDetail = new Command<Onlineclasses>(Gotodetailpage);
        }
        private async void Getonlineclasses(string tagurl)
        {
            try
            {
                var connected = CrossConnectivity.Current.IsConnected;
                if (connected == true)
                {
                    _Dialog.ShowLoading("");
                    var nsAPI = RestService.For<ILS_Listing>(Commonsettings.Localservices);
                    LSOnlinedata list = await nsAPI.Getonlineclasses(tagurl, "1");
                    OnPropertyChanged(nameof(classlistings));
                    classlistings = list.ROW_DATA;
                    var currentpage = GetCurrentPage();
                    ListView listvew = currentpage.FindByName<ListView>("listdata");
                    StackLayout stack = currentpage.FindByName<StackLayout>("nolistblk");
                    StackLayout stacknointernet = currentpage.FindByName<StackLayout>("nointernet");
                    if (list.ROW_DATA.Count>0)
                    {
                        foreach (var i in classlistings)
                        {
                            i.sizeallowed = "Average class size : " + i.sizeallowed + " students";
                            i.age = i.minage + " - " + i.maxage;
                            i.amount = "$ " + i.amount;
                            i.seatsleft = i.seatsleft + " Seats Left";
                        }
                    }
                    else
                    {
                        listvew.IsVisible = false;
                        stack.IsVisible = true;

                        stacknointernet.IsVisible = false;

                    }
                    listvew.IsVisible = true;
                    listvew.ItemsSource = classlistings;
                    dialog.HideLoading();

                }
                else
                {
                    var currentpage = GetCurrentPage();
                    ListView listvew = currentpage.FindByName<ListView>("listdata");
                    StackLayout stack = currentpage.FindByName<StackLayout>("nolistblk");
                    StackLayout stacknointernet = currentpage.FindByName<StackLayout>("nointernet");
                    listvew.IsVisible = false;
                    stack.IsVisible = false;
                    stacknointernet.IsVisible = true;
                }
            }
            catch (Exception e)
            {

                string Error = e.Message + e.StackTrace;
                dialog.HideLoading();
                var conne = CrossConnectivity.Current.IsConnected;
                if (conne == false)
                {
                    var currentpage = GetCurrentPage();
                    ListView listvew = currentpage.FindByName<ListView>("listdata");
                    StackLayout stack = currentpage.FindByName<StackLayout>("nolistblk");
                    StackLayout stacknointernet = currentpage.FindByName<StackLayout>("nointernet");
                    listvew.IsVisible = false;
                    stack.IsVisible = false;
                    stacknointernet.IsVisible = true;
                }
            }
        }

        public async void Gotodetailpage(Onlineclasses cls)
        {
            dialog.ShowLoading("");
            var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Detail.LS_Online_Detail(cls.classmasterid));
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
    }
}
