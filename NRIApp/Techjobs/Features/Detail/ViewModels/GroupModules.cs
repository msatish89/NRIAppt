using NRIApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using NRIApp.Techjobs.Features.Detail.Models;

namespace NRIApp.Techjobs.Features.Detail.ViewModels
{
   public class GroupModules : ObservableRangeCollection<BizModule>, INotifyPropertyChanged
    {
        private ObservableRangeCollection<BizModule> modlist = new ObservableRangeCollection<BizModule>();
        public GroupModules(GROUP_Bizmodules bizmod, bool expanded = false)
        {
            this.Bizmod = bizmod;
            _expanded = expanded;
            foreach (Bizmoduleslist p in bizmod.Modules)
            {
                if (p.trainingmode == "3")
                {
                    p.onlinemode = "Online";

                    p.inclassmode = "Inclass - " + p.trainingcity;
                    p.Classroommodestack = true;
                    p.Onlinemodestack = true;
                }
                else if (p.trainingmode == "1")
                {
                    p.inclassmode = "Inclass - " + p.trainingcity;

                    p.Classroommodestack = true;
                    p.Onlinemodestack = false;
                }
                else
                {
                    p.onlinemode = "Online";
                    p.Classroommodestack = false;
                    p.Onlinemodestack = true;
                }
                modlist.Add(new BizModule(p));

            }
            if (expanded) AddRange(modlist);
        }
        public GroupModules() { }
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
                        AddRange(modlist);
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
                return Bizmod.Heading;
            }
        }
        public GROUP_Bizmodules Bizmod
        {
            get;
            set;
        }
    }
}
