using NRIApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;

namespace NRIApp.LocalService.Features.Models
{
   public class GroupServices : ObservableRangeCollection<BizService> , INotifyPropertyChanged
    {
        private ObservableRangeCollection<BizService> Service = new ObservableRangeCollection<BizService>();
        public GroupServices(LS_AD_GROUP_SERVICES serv, bool expanded = false)
        {
            this.Gropservice = serv;
            _expanded = expanded;
            foreach (LS_AD_SERVICES_DATA room in serv.Services)
            {
                if (room.isheader==0)
                {
                    room.originalpricevisible=room.originalprice == "0.00" ? room.originalpricevisible = false : room.originalpricevisible = true;
                    Service.Add(new BizService(room));
                }
               
            }
            if (expanded) AddRange(Service);
        }
        public GroupServices() { }
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
                    return "minus.png";
                }
                else
                {
                    return "plus.png";
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
        public LS_AD_GROUP_SERVICES Gropservice
        {
            get;
            set;
        }
    }

  

}
