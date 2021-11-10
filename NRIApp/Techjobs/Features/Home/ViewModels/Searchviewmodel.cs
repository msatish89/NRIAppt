using System;
using System.Collections.Generic;
using System.Text;
using NRIApp.Techjobs.Features.Home.Interfaces;
using NRIApp.Techjobs.Features.Home.Models;
using Refit;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using System.Runtime.CompilerServices;
using NRIApp.Helpers;

namespace NRIApp.Techjobs.Features.Home.ViewModels
{
    public class Searchviewmodel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand Searchcommand { get; set; }
        public ICommand Backbtncommand { get; set; }
        public Searchviewmodel()
        {
            Searchcommand = new Command<SearchTags>(async (model) => await SearchClick(model));
            Backbtncommand = new Command(getprevpage);
        }

        public async Task SearchClick(SearchTags data)
        {
            var currentpage = GetCurrentPage();
            if (data.resulttype == "modules")
                await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.LeadForm.Views.Leadform(data.tags,data.id));
            else if (data.resulttype == "courses")
                await currentpage.Navigation.PushAsync(new NRIApp.Techjobs.Features.Home.Views.Modules(data.id,data.tags));
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
