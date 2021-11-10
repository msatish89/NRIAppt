using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using NRIApp.LocalService.Features.Interfaces;
using NRIApp.LocalService.Features.Models;
using Xamarin.Forms;
using Acr.UserDialogs;
using System.Threading.Tasks;

namespace NRIApp.LocalService.Features.ViewModels
{
  public  class LS_Servicetype :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs _Dialog = UserDialogs.Instance;
        public ICommand Servicetypetaaped { get; set; }
        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public LS_Servicetype(string type)
        {
            BindServicetype(type);
            Servicetypetaaped = new Command<SERVICE_TYPE>(SubmitServicetypetaaped);
        }

        private List<SERVICE_TYPE> _getServicetype;

        public List<SERVICE_TYPE> getServicetype
        {
            get
            {
                return _getServicetype;
            }
            set { _getServicetype = value; }
        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void BindServicetype(string type)
        {
            try
            {
                _Dialog.ShowLoading("", null);
                List<SERVICE_TYPE> st = new List<SERVICE_TYPE>();
                
                st.Add(new SERVICE_TYPE { id = 1, Servicetype = "I need a service provider",selectedcolor= "#2c2d2f" });
                if (type == "1")
                {
                    st.Add(new SERVICE_TYPE { id = 2, Servicetype = "I am a service provider", selectedcolor = "#fbaa19" });
                }
                else
                {
                    st.Add(new SERVICE_TYPE { id = 2, Servicetype = "I am a service provider", selectedcolor = "#2c2d2f" });
                }
                
                OnPropertyChanged(nameof(getServicetype));
                _getServicetype = st;
                _Dialog.HideLoading();
            }catch(Exception e) { }

        }

        public async void SubmitServicetypetaaped(SERVICE_TYPE st)
        {
            try
            {
                _Dialog.ShowLoading("", null);
                await Task.Delay(1000);
                var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
                await currentpage.Navigation.PushAsync(new Views.LS_Primarytags(st.id));
               
            }catch(Exception e) { }
           
        }
    }
}
