using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NRIApp.Events.Features.Listing.ViewModel
{
    class PensumCollection : INotifyPropertyChanged
    {
        // IUserDialogs _Dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand TapCommand { get; set; }

        public ObservableCollection<PensumField> Items { get; set; }

        public PensumCollection()
        {
            Items = new ObservableCollection<PensumField>()
            {
                new PensumField()
                {
                    FromTime = new TimeSpan(0, 8, 0, 0),
                    ToTime = new TimeSpan(0, 16, 0, 0),
                  
                    Location = "Berndorf"
                },
                new PensumField()
                {
                    FromTime = new TimeSpan(0, 8, 0, 0),
                    ToTime = new TimeSpan(0, 16, 0, 0),
                    Location = "Berndorf"
                },
                new PensumField()
                {
                    FromTime = new TimeSpan(0, 8, 0, 0),
                    ToTime = new TimeSpan(0, 16, 0, 0),
                    Location = "Berndorf"
                }
        };
            TapCommand = new Command(OnImgTapped);

        }

        private void OnImgTapped(object obj)
        {
            var senderName = obj as string;
            if (senderName == null)
                return;
            switch (senderName.ToLower())
            {
                case "save":
                    // do you work
                    break;
                case "add":
                    if (Items == null) Items = new ObservableCollection<PensumField>();
                    Items.Add(new PensumField()
                    {
                        FromTime = new TimeSpan(0, 8, 0, 0),
                        ToTime = new TimeSpan(0, 16, 0, 0),
                      
                        Location = "Berndorf"
                    }); // adds the default field :)
                    break;
                case "remove":
                    if (Items == null || Items.Any() == false)
                        return;
                    Items.RemoveAt(Items.Count - 1); // deletes the last field :)
                    break;
                default: break;
            }
        }
    }
}
