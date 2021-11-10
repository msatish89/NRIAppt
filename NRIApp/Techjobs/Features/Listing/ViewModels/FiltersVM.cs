using Acr.UserDialogs;

using NRIApp.Techjobs.Features.Listing.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NRIApp.Techjobs.Features.Listing.ViewModels
{
   public class FiltersVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SortClickedcommand { get; set; }
        List<Sorting> sort = new List<Sorting>();
        IUserDialogs dialog = UserDialogs.Instance;
        public facilities _Bizid;
        public facilities Bizid
        {
            get { return _Bizid; }
            set
            {
                _Bizid = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Bizid)));
            }
        }
        public static string orderbyval = "";
        public FiltersVM(string orderby)
        {
            orderbyval = orderby;
            Bindsortingdata(orderby);
            SortClickedcommand = new Command<Sorting>(async (ds) => await SortClick(ds));
        }
        public async void Bindsortingdata(string orderby)
        {
            dialog.ShowLoading("",null);
          //  sort.Add(new Sorting { text = "Default", id = "", image = "RadioButtonUnChecked.png" });
            sort.Add(new Sorting { text = "Featured", id = "featured", image = "RadioButtonUnChecked.png" });
            sort.Add(new Sorting { text = "Latest", id = "latest", image = "RadioButtonUnChecked.png" });
            sort.Add(new Sorting { text = "Distance", id = "distance", image = "RadioButtonUnChecked.png" });
           
            await Task.Delay(2000);
            var currentpage = GetCurrentPage();
            foreach (var item in sort)
            {
                if (orderby == item.id)
                {
                    item.image = "RadioButtonChecked.png";
                }
                else
                {
                    item.image = "RadioButtonUnChecked.png";
                }

            }
            var sortdta = currentpage.FindByName<ListView>("sortdata");
            sortdta.ItemsSource = sort;
            dialog.HideLoading();
        }
        public async Task SortClick(Sorting so)
        {
            foreach (var item in sort)
            {
                if (item.id == so.id)
                {
                    item.image = "RadioButtonChecked.png";
                    orderbyval = so.id;
                }
                else
                {
                    item.image = "RadioButtonUnChecked.png";
                }
            }
            var CurrentPage = GetCurrentPage();
            var sortdata = CurrentPage.FindByName<ListView>("sortdata");
            sortdata.ItemsSource = null;
            sortdata.ItemsSource = sort;
        }

        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.ModalStack.LastOrDefault();
            return currentPage;
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
