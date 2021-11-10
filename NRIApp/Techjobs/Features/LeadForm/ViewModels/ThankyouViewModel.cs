using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using NRIApp.Techjobs.Features.LeadForm.Models;
using Plugin.Messaging;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NRIApp.Techjobs.Features.LeadForm.ViewModels
{
   public class ThankyouViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand Closecommand { get; set; }
        public ICommand Phonecallcommand { get; set; }
        IUserDialogs dialog = UserDialogs.Instance;
        public ThankyouViewModel(string respid)
        {
            getbusinesslist(respid);
             Closecommand = new Command(Gotohomepage);
            Phonecallcommand = new Command<Leadsentbiz>(Clicktocall);
        }

        public bool _visible = false;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Visible)));

            }
        }

        public bool _stackvisible = false;
        public bool StackVisible
        {
            get { return _stackvisible; }
            set
            {
                _stackvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StackVisible)));
            }
        }

        public List<Leadsentbiz> _bizlist { get; set; }
        public List<Leadsentbiz> Bizlist
        {
            get
            {
                return _bizlist;
            }
            set
            {
                _bizlist = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bizlist)));
            }
        }
        async void getbusinesslist(string respid)
        {
            dialog.ShowLoading("", null);
            var nsAPI = RestService.For<ILeadformService>(Commonsettings.TechjobsAPI);
            Leadsentbizlist list = await nsAPI.Getbizlist(respid);
            Bizlist = list.ROW_DATA;
            
            if (Bizlist.Count >0)
            {
                Visible = true;
                StackVisible = false;
            }
                
            else
            {
                Visible = false;
                StackVisible = true;
            }

            dialog.HideLoading();
            
        }

        public void Clicktocall(Leadsentbiz biz)
        {
            var phonecall = CrossMessaging.Current.PhoneDialer;
            if (phonecall.CanMakePhoneCall)
                phonecall.MakePhoneCall(biz.phone5, null);
        }

        public async void Gotohomepage()
        {
            var currentpage = GetCurrentPage();
            //currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 2]);
            //await currentpage.Navigation.PopAsync();
            await currentpage.Navigation.PopToRootAsync();
        }

        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
    }
}
