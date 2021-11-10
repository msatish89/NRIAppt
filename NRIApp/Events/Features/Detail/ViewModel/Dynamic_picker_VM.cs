using NRIApp.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace NRIApp.Events.Features.Detail.ViewModel
{
    class Dynamic_picker_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<string> pickerItems;
        public ObservableCollection<string> PickerItems
        {
            get { return pickerItems; }
            set
            {
                pickerItems = value;
                OnPropertyChanged();
            }
        }

        public Dynamic_picker_VM()
        {
            PickerItems = new ObservableCollection<string>();
            for (int i = 0; i <= 4; i++)
            {
                this.PickerItems.Add(i.ToString());
            }
        }
        public ObservableCollection<PersonHolder> PersonHolders { get; set; } =
          new ObservableCollection<PersonHolder>();

        PersonHolder personHolder = new PersonHolder();
        personHolder.Person = new Person() { Name = "Jesse" };
        PersonHolders.Add(personHolder);
 
          personHolder = new PersonHolder();
        personHolder.Person = new Person() { Name = "Karl" };
        PersonHolders.Add(personHolder);
            public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
