using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace NRIApp.LocalService.Features.Models
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        bool isEmpty = false;
        public bool IsEmpty
        {
            get { return isEmpty; }
            set
            {
                isEmpty = value;
                OnPropertyChanged(nameof(IsEmpty));
                
            }
        }
         int height;
        public  int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                OnPropertyChanged(nameof(Height));
               
            }
        }
        //private void OnEmptyChanged(BaseViewModel baseViewModel, PropertyChangedEventArgs propertyChangedEventArgs)
        //{
        //    CrossToastPopUp.Current.ShowToastMessage("No Data Found");
        //}

        string busyText = string.Empty;
        string title = string.Empty;



        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string BusyText
        {
            get => busyText;
            set => SetProperty(ref busyText, value);
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
