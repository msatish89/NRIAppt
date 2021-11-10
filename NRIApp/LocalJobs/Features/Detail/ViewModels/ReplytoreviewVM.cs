using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace NRIApp.LocalJobs.Features.Detail.ViewModels
{
    public class ReplytoreviewVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ReplytoreviewVM()
        {

        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page GetCurrentpage()
        {
            var Currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return Currentpage;
        }

    }
}
