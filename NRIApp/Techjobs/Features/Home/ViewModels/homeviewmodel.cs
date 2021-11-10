using System;
using System.Collections.Generic;
using System.Text;
using Refit;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using System.Runtime.CompilerServices;
using NRIApp.Techjobs.Features.Home.Interfaces;
using NRIApp.Techjobs.Features.Home.Models;
using NRIApp.Helpers;
using Acr.UserDialogs;
using System.Net;
using Plugin.Connectivity;

namespace NRIApp.Techjobs.Features.Home.ViewModels
{
   public class homeviewmodel : INotifyPropertyChanged
    {
       public event PropertyChangedEventHandler PropertyChanged;
        public ICommand Datacommand { get; set; }
        public ICommand Backbtncommand { get; set; }
        public ICommand Modulecommand { get; set; }
        public ICommand Searchbarcommand { get; set; }
        IUserDialogs dialog = UserDialogs.Instance;
        public homeviewmodel(string id, string type)
        {
            if (type == "course")
                getcourses();
            else if (type == "module")
                getmodules(id);

            Datacommand = new Command<Tags>(async (model) => await Handleselectedcourse(model));
            Modulecommand = new Command<PTags>(async (model) => await Handleselectedmodule(model));
            Backbtncommand = new Command(getprevpage);
            Searchbarcommand = new Command(searchbar);
        }

        public bool _visible = false;
        public bool Visible
        {
            get { return _visible; }
            set
            {
                _visible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Visible)));

            }
        }
        public List<Tags> _courses { get; set; }
        public List<Tags> Courses
        {
            get
            {
                return _courses;
            }
            set
            {
                _courses = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Courses)));
            }
        }

        public List<PTags> _modules { get; set; }
        public List<PTags> Modules
        {
            get
            {
                return _modules;
            }
            set
            {
                _modules = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Modules)));
            }
        }
        async void getcourses()
        {
               dialog.ShowLoading("",null);
            var connected = CrossConnectivity.Current.IsConnected;
            try
            {
                var nsAPI = RestService.For<IHomeservice>(Commonsettings.TechjobsAPI);
                Courselist list = await nsAPI.Getcourses();
                Courses = list.ROW_DATA;
                Visible = false;
                dialog.HideLoading();
            }
            catch(Exception e)
            {
                string Error = e.Message + e.StackTrace;
                dialog.HideLoading();
               // var conne = CrossConnectivity.Current.IsConnected;
            }
           

        }

        async void getmodules(string courseid)
        {
            dialog.ShowLoading("", null);
            var connected = CrossConnectivity.Current.IsConnected;
            try
            {
                var nsAPI = RestService.For<IHomeservice>(Commonsettings.TechjobsAPI);
                Modulelist list = await nsAPI.Getmodules(courseid);
                Modules = list.ROW_DATA;
            dialog.HideLoading();
            Visible = false;
            }
            catch (Exception e)
            {
                string Error = e.Message + e.StackTrace;
                dialog.HideLoading();
                dialog.Toast("Kindly check your internet connection");

            }
        }

        public async Task Handleselectedcourse(Tags model)
        {
            var currentpage = GetCurrentPage();
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == true)
            await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.Home.Views.Modules(model.supertagid.ToString(), model.supertag.ToString()));
            else
                dialog.Toast("Kindly check your internet connection");
        }

        public async Task Handleselectedmodule(PTags model)
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == true)
            {
                var currentpage = GetCurrentPage();
                await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.LeadForm.Views.Leadform(model.module, model.moduleid));
            }
            else
                dialog.Toast("Kindly check your internet connection");

        }
        public async Task Respform(PTags data)
        {
            var currentpage = GetCurrentPage();
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected == true)
                await currentpage.Navigation.PushModalAsync(new NRIApp.Techjobs.Features.LeadForm.Views.Leadform(data.module,data.moduleid));
            else
                dialog.Toast("Kindly check your internet connection");
        }

        public async void searchbar()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.Home.Views.Search());
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
        public async void getprevpage()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PopAsync();
        }
    }
}
