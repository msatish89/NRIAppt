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
   public class FunctionalareaVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;
        public List<Functionalarea> Functionalareas { get; set; }
        public ICommand Functionalareacommand { get; set; }
        public ICommand functionalareatap { get; set; }
        Postingdata jobdata = new Postingdata();
        string clrchange = "0";
        public FunctionalareaVM(int jobtype,Postingdata data)
        {
            jobdata = data;
            getfunctionalareas(jobtype);
            Functionalareacommand = new Command<Functionalarea>(ClickFunctionalarea);
            functionalareatap = new Command(needFunctionalarea);
        }
        private async void getfunctionalareas(int jobtype)
        {
           try
            {
                dialog.ShowLoading("", null);
                var nsAPI = RestService.For<IPosting>(Commonsettings.LocaljobsAPI);
                Functionalarealist list = await nsAPI.Getfunctionalarea(jobtype);
                foreach (var funcarea in list.ROW_DATA)
                {
                    if (funcarea.categoryname == jobdata.rolename || funcarea.categoryname == jobdata.Secondarycategoryvalue)
                    {
                        funcarea.textcolor = "#fbaa19";
                        clrchange = "1";
                    }
                    else
                    {
                        funcarea.textcolor = "#2c2d2f";
                    }
                }
                OnPropertyChanged(nameof(Functionalareas));
                Functionalareas = list.ROW_DATA;
                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }

        private async void ClickFunctionalarea(Functionalarea list)
        {
            //validation check
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Jobrole(list.secondarycategoryid,list.primarycategoryid,list.categoryname,jobdata));
        }
        public void needFunctionalarea()
        {
            if(clrchange=="0")
            {
                dialog.Toast("Select functional area");
            }
            else
            {
                var currentpage = GetCurrentPage();
                currentpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Jobrole(jobdata.Secondarycategoryid, jobdata.Primarycategoryid, jobdata.Secondarycategoryvalue, jobdata));
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
    }
}
