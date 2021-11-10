using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Posting.Interfaces;
using NRIApp.LocalJobs.Features.Posting.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NRIApp.LocalJobs.Features.Posting.ViewModels
{
    public class JobroleVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;
        public List<Jobroles> Jobroleslists { get; set; }
        public ICommand Jobrolecommand { get; set; }
        public ICommand Jobrolesbmtcommand { get; set; }
        Postingdata data = new Postingdata();

        private bool _MyneedsNxtVisible = false;
        public bool MyneedsNxtVisible
        {
            get { return _MyneedsNxtVisible; }
            set { _MyneedsNxtVisible = value; }
        }
        public JobroleVM(int funid, int jobtype, string functionalarea, Postingdata jdata)
        {
            data = jdata;
            data.functionalareaid = funid;
            data.jobtype = jobtype;
            data.functionalarea = functionalarea;
            Getjobroles(funid);
            Jobrolecommand = new Command<Jobroles>(Selectjobrole);
            Jobrolesbmtcommand = new Command(Jobrolesbmt);
        }

        private async void Getjobroles(int funid)
        {
          try
            {
                dialog.ShowLoading("", null);
                //if (data.ismyneed == "1" && !string.IsNullOrEmpty(data.rolename))// && string.IsNullOrEmpty(Jobrole))
                //{
                //    Jobrole = data.rolename;
                //}
                var nsAPI = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                Jobroleslist list = await nsAPI.GetJobroles(funid);
                if (data.ismyneed == "1")
                {
                    MyneedsNxtVisible = true;
                }
                foreach (var jrole in list.ROW_DATA)
                {
                    if (jrole.rolename == data.rolename)
                    {
                        jrole.textcolor = "#fbaa19";
                    }
                    else
                    {
                        jrole.textcolor = "#2c2d2f";
                    }
                }

                OnPropertyChanged(nameof(Jobroleslists));
                Jobroleslists = list.ROW_DATA;
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }

        public string _jobrole { get; set; }
        public string Jobrole
        {
            get { return _jobrole; }
            set
            {

                if (_jobrole != value)
                {
                    _jobrole = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Jobrole)));
                    if (SelJobrole != _jobrole)
                        ajaxjobrole(_jobrole);
                }
            }
        }

        public string _seljobrole { get; set; }
        public string SelJobrole
        {
            get { return _seljobrole; }
            set
            {
                _seljobrole = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelJobrole)));
            }
        }

        public string _jobroleid { get; set; }
        public string Jobroleid
        {
            get { return _jobroleid; }
            set
            {
                _jobroleid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Jobroleid)));

            }
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
        public async void Selectjobrole(Jobroles model)
        {
            SelJobrole = model.rolename;
            Jobroleid = model.contentid;
            Jobrole = model.rolename;
            data.roleid = model.contentid;
            data.rolename = model.rolename.Replace("&", "&amp;");
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Jobskills(data));
        }
        public async void ajaxjobrole(string role)
        {
            try
            {
                Jobroleid = "";
                var nsAPI = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                Jobroleslist list = await nsAPI.GetJobroleajax(role);
                foreach (var jrole in list.ROW_DATA)
                {
                    if (jrole.rolename == data.rolename)
                    {
                        jrole.textcolor = "#fbaa19";
                    }
                    else
                    {
                        jrole.textcolor = "#2c2d2f";
                    }
                }
                OnPropertyChanged(nameof(Jobroleslists));
                Jobroleslists = list.ROW_DATA;
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }

        public async void Jobrolesbmt()
        {
          try
            {
                
                if (!string.IsNullOrEmpty(data.rolename))
                {
                    var currentpage = GetCurrentPage();
                    //data.rolename = data.rolename.Replace("&", "&amp;");
                    await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Jobskills(data));
                }
                else
                    dialog.Toast("Please select the jobrole");
            }
            catch(Exception ex)
            {

            }
           
        }
    }
        
    }
