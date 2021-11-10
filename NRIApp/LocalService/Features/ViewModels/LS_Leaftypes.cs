using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NRIApp.LocalService.Features.Models;
using Refit;
using Xamarin.Forms;

namespace NRIApp.LocalService.Features.ViewModels
{
    public class LS_Leaftypes : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static string Checkedvalues;
        public static string Checkedtext;
        private static bool _isBusy = false;
        public static bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                //ViewModel.LS_ViewModel.OnPropertyChanged();
            }
        }
        private List<LEAF_TAGS_DATA> _getlsleafdata { get; set; }
        public ICommand Backbtncommand { get; set; }
        public static Command checkedclik { get; set; }
        public LS_Leaftypes(int id)
        {

            Backbtncommand = new Command(async () => await BackbtncommandClick());
        }


        public async Task BackbtncommandClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }catch(Exception ee)
            {

            }
        }
    }
}
