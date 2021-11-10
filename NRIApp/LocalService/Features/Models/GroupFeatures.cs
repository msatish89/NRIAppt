using NRIApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;

namespace NRIApp.LocalService.Features.Models
{
   public class GroupFeatures : ObservableRangeCollection<BizFeatures> , INotifyPropertyChanged
    {
        private ObservableRangeCollection<BizFeatures> Service = new ObservableRangeCollection<BizFeatures>();
        public GroupFeatures(LS_PAYMENT_FEATURE serv, bool expanded = false)
        {
            this.Gropservice = serv;
            _expanded = expanded;
            foreach (LS_PAYMENT_FEATURES room in serv.Services)
            {
                
                Service.Add(new BizFeatures(room));

            }
            if (expanded) AddRange(Service);
        }
        public GroupFeatures() { }
        private bool _expanded;
        public bool Expanded
        {
            get
            {
                return _expanded;
            }
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Expanded"));
                    OnPropertyChanged(new PropertyChangedEventArgs("StateIcon"));
                    if (_expanded)
                    {
                        AddRange(Service);
                    }
                    else
                    {
                        Clear();
                    }
                }
            }
        }
        int height;
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Height"));
            }
        }
        public string StateIcon
        {
            get
            {
                if (Expanded)
                {
                    return "downarrowGrey.png";
                }
                else
                {
                    return "uparrowGrey.png";
                }
            }
        }
        public string Name
        {
            get
            {
                return Gropservice.Heading;
            }
        }
        public LS_PAYMENT_FEATURE Gropservice
        {
            get;
            set;
        }
    }

  

}
