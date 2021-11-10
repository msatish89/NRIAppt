using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace NRIApp.Events.Features.Listing.ViewModel
{
   public class picker_vm : INotifyPropertyChanged
    {
       // IUserDialogs _Dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public List<listdatapicker> Eventsbanner { get; set; }
        public ObservableCollection<datapicker> Monkeys { get; set; }


        public static List<String> pensumTypes { get; set; }    //list for the picker to choose from
        public ObservableCollection<PensumField> pensumFields { get; set; }
        public picker_vm()
        {
            pensumFields = new ObservableCollection<PensumField>();

            pensumTypes = new List<String>();
            //Fill PensumTypes
            pensumTypes.Add("type1");
            pensumTypes.Add("type2");
            pensumTypes.Add("type3");
            Monkeys = new ObservableCollection<datapicker>();
            Bindpickervalue();
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Bindpickervalue()
        {
            List<datapicker> data = new List<datapicker>()
            {
                new datapicker{id=1,name="1"},
                 new datapicker{id=2,name="2"},
                  new datapicker{id=3,name="3"},
                   new datapicker{id=4,name="4"}

            };
            List<datapicker> t = new List<datapicker>();

            foreach (var item in data)
            {
                Monkeys.Add(item);
            };
            List<listdatapicker> listdata = new List<listdatapicker>(){
                new listdatapicker { name = "Test1" },
                 new listdatapicker {  name = "Test2" },
                  new listdatapicker {  name = "Test3" },
                   new listdatapicker {  name = "Test4" }

            };
          

        Eventsbanner = listdata;
            OnPropertyChanged(nameof(Eventsbanner));
            OnPropertyChanged(nameof(Monkeys));
        }
        public static Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();

            return currentPage;
        }
    }
    public class datapicker
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class listdatapicker
    {
       
        public string name { get; set; }

        public List<datapicker> datapickers { get; set; }
    }

    public class PensumField
    {
        public PensumField()
        {
            datapickers = new List<datapicker>() {  new datapicker{id=1,name="1"},
                 new datapicker{id=2,name="2"},
                  new datapicker{id=3,name="3"},
                   new datapicker{id=4,name="4"} };
        }
        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public string SelectedType { get; set; }
        public string Location { get; set; }
        public List<datapicker> datapickers { get; }

        /// <summary>
        /// Test property
        /// </summary>
        public string FromAsString => FromTime.ToString();
    }
}
