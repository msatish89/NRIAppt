using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.LocalService.Features.Interfaces;
using NRIApp.LocalService.Features.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.RangeSlider.Forms;

namespace NRIApp.LocalService.Features.ViewModels
{
    public class LS_Listing_Search : INotifyPropertyChanged
    {
        IUserDialogs _Dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public static int issearchtext = 0;
        public static int issortby = 1;
        
        public string _templistingsearch;
        private static List<LEAF_TAGS_DATA> _getlsleafdata { get; set; }
        public static List<LEAF_TAGS_DATA> getlsleafdata
        {
            get { return _getlsleafdata; }

            set
            {
                _getlsleafdata = value;
                //  OnPropertyChanged(nameof(getlsleafdata));
            }
        }
        private List<LISTING_SORTING> _GetSorting;

        public List<LISTING_SORTING> GetSorting
        {
            get
            {
                return _GetSorting;
            }
            set { _GetSorting = value; }
        }
        private string _listingsearch { get; set; }
        public string listingsearch
        {
            get { return _listingsearch; }
            set
            {
                _listingsearch = value;
                if (string.IsNullOrEmpty(listingsearch))
                {
                   
                    OnPropertyChanged(nameof(Searchlisting));
                }
                if (_templistingsearch != listingsearch && listingsearch != "")
                {
                    getlistingsearchtext();
                }
            }
        }
        
        public static int Filteropennow = 0;
        private string _Filteropenimg = "OffButton.png";
        public string Filteropenimg
        {
            get { return _Filteropenimg; }
            set
            {
                _Filteropenimg = value;
                OnPropertyChanged(nameof(Filteropenimg));
            }
        }
        private bool _Isleaftypeavail = false;
        public bool Isleaftypeavail
        {
            get { return _Isleaftypeavail; }
            set
            {
                _Isleaftypeavail = value;
                OnPropertyChanged(nameof(Isleaftypeavail));
            }
        }
        private bool _Categoryblk = false;
        public bool Categoryblk
        {
            get { return _Categoryblk; }
            set
            {
                _Categoryblk = value;
                OnPropertyChanged(nameof(Categoryblk));
            }
        }
        public int Priceimgnum = 0;
        public string _Priceimg = "downarrowGrey.png";
        public string Priceimg
        {
            get { return _Priceimg; }
            set
            {
                _Priceimg = value;
                OnPropertyChanged(nameof(Priceimg));
            }
        }
        private bool _Rangeblk = false;
        public bool Rangeblk
        {
            get { return _Rangeblk; }
            set
            {
                _Rangeblk = value;
                OnPropertyChanged(nameof(Rangeblk));
            }
        }
        public int Categoryimgnum = 0;
        public string _Categoryimg = "downarrowGrey.png";
        public string Categoryimg
        {
            get { return _Categoryimg; }
            set
            {
                _Categoryimg = value;
                OnPropertyChanged(nameof(Categoryimg));
            }
        }
        private Command _Backbtncommand;
        public Command Backbtncommand
        {
            get { return _Backbtncommand; }
            set { _Backbtncommand = value; }
        }
        public ICommand Resetcommand { get; set; }
        public ICommand Clickopennow { get; set; }
        public ICommand Listingsearchcommand { get; set; }
        public ICommand Rangeclick { get; set; }
        public ICommand Categoryclick { get; set; }
        public ICommand ListingFilterClick { get; set; }
        public ICommand Sortypetaaped { get; set; }
        public ICommand SortClickedcommand { get; set; }
        public ICommand SortApplyClick { get; set; }
        public List<LISTING_SEARCH_DEFAULT_DATA> Searchlisting { get; set; }
        List<LISTING_SORTING> sort = new List<LISTING_SORTING>();
        public LS_Listing_Search(string ptagid, string cityurl)
        {
            Binddefalutvalues(ptagid, cityurl);
            Backbtncommand = new Command(async () => await BackbtncommandClick());
            Listingsearchcommand=new Command<LISTING_SEARCH_DEFAULT_DATA>(async (LSD) => await Selectlistingsearch(LSD));
        }
        public string Name { get; set; }

        bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set { SetProperty(ref isVisible, value); }
        }

        bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { SetProperty(ref isSelected, value); }
        }
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        

        public LS_Listing_Search(string ptagid,string filter,string type)
        {
            LS_ViewModel.page = filter;
            if (type=="1")
            {
               
                Bindfilterdata(ptagid);
                Resetcommand = new Command(Clicktoreset);
                Clickopennow = new Command(Checkopennow);
                Rangeclick = new Command(Rangeblkclick);
                Categoryclick = new Command(Categoryblkclick);
                ListingFilterClick = new Command(ClickListingFilter);
                Backbtncommand = new Command(async () => await BackbtncommandfilterClick());
            }
            else if (type=="2")
            {
               
                Bindsortingdata();
                Backbtncommand = new Command(async () => await BackbtncommandfilterClick());
                SortClickedcommand = new Command<LISTING_SORTING>(async (ds) => await SortClick(ds));
                SortApplyClick = new Command(SortApply);
            }
           
        }
       
      
            public async Task BackbtncommandClick()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ee)
            {

            }
        }
        public async Task BackbtncommandfilterClick()
        {
            try { 
            await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            catch (Exception ee)
            {

            }
        }
        
        public async void Binddefalutvalues(string ptagid, string cityurl)
        {
            try
            {
                _Dialog.ShowLoading("", null);
                var searchlist = RestService.For<ILS_Listing>(Commonsettings.TechjobsAPI);
                var data = await searchlist.GetListingSearchData(ptagid,cityurl);
                if (data.ROW_DATA.Count > 0)
                {

                    foreach (var i in data.ROW_DATA)
                    {
                        if (i.type == 1)
                            i.header = "Top Categories";
                        if (i.type == 2)
                            i.header = "Top Other Categories";
                        if (i.type == 3)
                            i.header = "Top Business";
                    }
                    var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();
                    var sorted = from srch in data.ROW_DATA group srch by srch.header into searchGroup select new Grouping<string, LISTING_SEARCH_DEFAULT_DATA>(searchGroup.Key, searchGroup);
                    var entry = CurrentPage.FindByName<ListView>("lslistlistview");
                    entry.ItemsSource = sorted;
                }
                _Dialog.HideLoading();
            }
            catch (Exception e)
            {
                _Dialog.HideLoading();
            }
        }

        public async void getlistingsearchtext()
        {
            try
            {

                var search = RestService.For<ILS_Listing>(Commonsettings.TechjobsAPI);
                var list = await search.GetListingSearchajax(listingsearch,Commonsettings.Usercityurl,Commonsettings.Usermetrourl,Convert.ToString(Commonsettings.UserLat),Commonsettings.UserLong.ToString());
             
                foreach (var i in list.ROW_DATA)
                {
                    if (i.type == 1|| i.type == 2)
                        i.header = "Categories";                   
                    if (i.type == 3)
                        i.header = "Business";
                }
                var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();
                var sorted = from srch in list.ROW_DATA group srch by srch.header into searchGroup select new Grouping<string, LISTING_SEARCH_DEFAULT_DATA>(searchGroup.Key, searchGroup);
                var entry = CurrentPage.FindByName<ListView>("lslistlistview");
                entry.ItemsSource = sorted;
               
            }
            catch (Exception e) { }

        }

        public async Task Selectlistingsearch(LISTING_SEARCH_DEFAULT_DATA LSD)
        {
            issearchtext = 1;
            LS_ViewModel.filterprimarytagid = ""; LS_ViewModel.filterprimarytag = "";
            if (LSD.type==3)
            {
                LS_LISTINGMODEL_DATA LMD = new LS_LISTINGMODEL_DATA()
                {
                    adid=LSD.tagid.ToString(),
                    title=LSD.tag,
                    titleurl=LSD.tagurl,
                    newcityurl=LSD.cityurl,
                    maincityurl=LSD.cityurl
                };
                
                var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
                await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Detail.LS_Detail(LMD));
            }
            else
            {
                
               
               ViewModels.LS_ViewModel.stagid=  LSD.stagid;
                ViewModels.LS_ViewModel.selectedtagtype =  LSD.type;
                ViewModels.LS_ViewModel.searchtag = LSD.tag;
               
                    ViewModels.LS_ViewModel.primarytagid = ViewModels.LS_ViewModel.searchtagid = LSD.tagid.ToString();
               
                
                var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
                await currentpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Listing.LS_Listing());
            }
        }

        public async void Bindfilterdata(string ptagid)
        {
            try
            {
                _Dialog.ShowLoading("", null);
                var listingdata = RestService.For<IServiceAPI>(Commonsettings.Localservices);
                
                var data = await listingdata.GetLeaftypes(Convert.ToInt32(ptagid));
                getlsleafdata = data.ROW_DATA;
                await Task.Delay(4000);
                var currentpage = ViewModels.LS_ViewModel.GetCurrentModalPage();
                if (getlsleafdata != null && getlsleafdata.Count > 0)
                {
                    Isleaftypeavail = true;
                    
                   
                   StackLayout layoutinnercheckbox = currentpage.FindByName<StackLayout>("layoutinnercheckbox");
                 
                    foreach (var item in data.ROW_DATA)
                    {
                        BoxView box = new BoxView();
                        box.BackgroundColor = Color.FromHex("e0e0e0");
                        box.HeightRequest = 1;
                        Plugin.InputKit.Shared.Controls.CheckBox chb = new Plugin.InputKit.Shared.Controls.CheckBox(); chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;chb.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check; chb.Color = Color.DarkGreen; chb.BoxBackgroundColor = Color.LightGray;
                        //chb.OnImg = "CheckBoxChecked.png";
                        //chb.OffImg = "CheckBoxUnChecked.png";
                        chb.Text = item.tag;
                        if (ViewModels.LS_Listing_Search.issearchtext==1 && ViewModels.LS_ViewModel.selectedtagtype==1&&item.tagid==Convert.ToInt32(ViewModels.LS_ViewModel.primarytagid))
                        {
                           
                                chb.IsChecked = true;
                            
                        }                        
                        else if (ViewModels.LS_Listing_Search.issearchtext == 0&&!string.IsNullOrEmpty(ViewModels.LS_ViewModel.filterprimarytagid))
                        {
                            string[] indexvalue = ViewModels.LS_ViewModel.filterprimarytagid.Split(',');
                            int numIndex = Array.IndexOf(indexvalue, item.tagid.ToString());
                           if (Array.IndexOf(indexvalue,item.tagid.ToString()) >= 0)
                            {
                                chb.IsChecked = true;
                            }
                        }
                        else if (ViewModels.LS_Listing_Search.issearchtext == 1 && !string.IsNullOrEmpty(LS_ViewModel.filterprimarytagid))
                        {
                            string[] indexvalue = LS_ViewModel.filterprimarytagid.Split(',');
                            int numIndex = Array.IndexOf(indexvalue, item.tagid.ToString());
                            if (Array.IndexOf(indexvalue, item.tagid.ToString()) >= 0)
                            {
                                chb.IsChecked = true;
                            }
                        }
                        //chb.ShowLabel = true;
                        chb.CheckChanged += Chb_FilterCheckChanged;
                        chb.ClassId = item.tagid.ToString();
                        chb.Padding = 5;
                        layoutinnercheckbox.Children.Add(chb);
                        layoutinnercheckbox.Children.Add(box);
                    }

                   

                }
                else
                {
                    Isleaftypeavail = false;
                }
                RangeSlider slider = currentpage.FindByName<RangeSlider>("slidervalue");
                slider.LowerValue = LS_ViewModel.distancefrom;
                slider.UpperValue = LS_ViewModel.distanceto;
                if (LS_ViewModel.openorclose==1)
                {
                    Filteropennow = 1;
                    Filteropenimg = "OnButton.png";
                }
                else
                {
                    Filteropennow = 0;
                    Filteropenimg = "OffButton.png";
                }
                _Dialog.HideLoading();
                OnPropertyChanged(nameof(Filteropenimg));
                OnPropertyChanged(nameof(Isleaftypeavail));
            }
            catch(Exception e)
            {
                _Dialog.HideLoading();
            }
            }



        private void Chb_FilterCheckChanged(object sender, EventArgs e)
        {
            var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
            if (category.IsChecked==true)
            {
                if (LS_ViewModel.filterprimarytagid != null && LS_ViewModel.filterprimarytagid != "")
                {
                    if (!LS_ViewModel.filterprimarytagid.Contains(category.ClassId))
                    {
                        LS_ViewModel.filterprimarytagid += category.ClassId + ",";
                    }
                }
                else
                {

                    LS_ViewModel.filterprimarytagid += category.ClassId + ",";
                }
                if (LS_ViewModel.filterprimarytag != null && LS_ViewModel.filterprimarytag != "")
                {
                    if (!LS_ViewModel.filterprimarytag.Contains(category.Text))
                    {
                        LS_ViewModel.filterprimarytag += category.Text + ",";
                    }
                }
                else
                {

                    LS_ViewModel.filterprimarytag += category.Text + ",";
                }

            }
            else
            {

                string[] indexvalue = LS_ViewModel.filterprimarytagid.Split(',');
                int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                LS_ViewModel.filterprimarytagid = string.Join(",", indexvalue);

                string[] indextext = LS_ViewModel.filterprimarytagid.Split(',');
                int txtIndex = Array.IndexOf(indextext, category.Text);
                indextext = indextext.Where((val, idx) => idx != txtIndex).ToArray();
                LS_ViewModel.filterprimarytagid = string.Join(",", indextext);
            }


        }

        public void Clicktoreset()
        {
            var currentpage =ViewModels.LS_ViewModel.GetCurrentModalPage();
            RangeSlider slider = currentpage.FindByName<RangeSlider>("slidervalue");
            slider.LowerValue = 0;
            slider.UpperValue = 1000;
            Filteropenimg = "OffButton.png";
            Filteropennow = 0;
            OnPropertyChanged(nameof(Filteropenimg));
             StackLayout stackcategory = currentpage.FindByName<StackLayout>("layoutinnercheckbox");
           // ScrollView stackcategory = currentpage.FindByName<ScrollView>("layoutinnerscrollcheckbox");
            Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray;chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
            foreach (var item in stackcategory.Children)
            {
                if (item.GetType() == chbx.GetType())
                {
                    var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                    if (cat.IsChecked == true)
                    {
                        cat.IsChecked = false;
                    }
                }

            }
            LS_ViewModel.filterprimarytag = LS_ViewModel.filterprimarytagid="";

        }
        public void Checkopennow()
        {
            if (Filteropennow == 1)
            {
                Filteropenimg = "OffButton.png";
                Filteropennow = 0;
            }
            else
            {
                Filteropenimg = "OnButton.png";
                Filteropennow = 1;
            }
            OnPropertyChanged(nameof(Filteropenimg));
        }
        public void Rangeblkclick()
        {
            if (Priceimgnum == 0)
            {
                Priceimg = "minusGrey.png";
                Priceimgnum = 1;
                Rangeblk = true;
            }
            else
            {
                Priceimg = "downarrowGrey.png";
                Priceimgnum = 0;
                Rangeblk = false;
            }
            OnPropertyChanged(nameof(Priceimg));
            OnPropertyChanged(nameof(Rangeblk));
        }
        public void Categoryblkclick()
        {
            if (Categoryimgnum == 0)
            {
                Categoryimg = "minusGrey.png";
                Categoryimgnum = 1;
                Categoryblk = true;
            }
            else
            {
                Categoryimg = "downarrowGrey.png";
                Categoryimgnum = 0;
                Categoryblk = false;
            }
            OnPropertyChanged(nameof(Categoryimg));
            OnPropertyChanged(nameof(Categoryblk));
        }
        public async void ClickListingFilter()
        {
            var curpage = ViewModels.LS_ViewModel.GetCurrentPage();            
            var modalpage = ViewModels.LS_ViewModel.GetCurrentModalPage();
            var distnce=modalpage.FindByName<RangeSlider>("slidervalue");
            LS_ViewModel.distancefrom = Convert.ToInt32(distnce.LowerValue);
            LS_ViewModel.distanceto = Convert.ToInt32(distnce.UpperValue);
            LS_ViewModel.openorclose = Filteropennow;
            LS_ViewModel.isfiltered = 1;
            try { 
            await curpage.Navigation.PopModalAsync();
            await curpage.Navigation.PushAsync(new Views.Listing.LS_Listing());
            curpage.Navigation.RemovePage(curpage.Navigation.NavigationStack[curpage.Navigation.NavigationStack.Count - 2]);
            }
            catch (Exception ee)
            {

            }

        }
        public async  void Bindsortingdata()
        {
            try {
                _Dialog.ShowLoading("");
                sort.Add(new LISTING_SORTING { id = "date", text = "Default", image = "RadioButtonUnChecked.png" });
                sort.Add(new LISTING_SORTING { id = "offers", text = "Offers", image = "RadioButtonUnChecked.png" });
                sort.Add(new LISTING_SORTING { id = "reviews", text = "Reviews", image = "RadioButtonUnChecked.png" });
                sort.Add(new LISTING_SORTING { id = "a-z", text = "A to Z", image = "RadioButtonUnChecked.png" });
                sort.Add(new LISTING_SORTING { id = "photos", text = "Photos", image = "RadioButtonUnChecked.png" });
                sort.Add(new LISTING_SORTING { id = "distance", text = "Distance", image = "RadioButtonUnChecked.png" });
                await Task.Delay(2000);
                var CurrentPage = ViewModels.LS_ViewModel.GetCurrentModalPage();
                var sortdata = CurrentPage.FindByName<ListView>("sortdata");
                foreach (var item in sort)
                {
                    if (LS_ViewModel.sortselected==item.id)
                    {
                        item.image = "RadioButtonChecked.png";
                    }
                    else
                    {
                        item.image = "RadioButtonUnChecked.png";
                    }

                   

                }
                sortdata.ItemsSource = sort;
                _Dialog.HideLoading();
            }
            catch (Exception e) { _Dialog.HideLoading(); }
            OnPropertyChanged(nameof(GetSorting));
        }
        public async Task SortClick(LISTING_SORTING ls)
        {
            foreach (var item in sort)
            {
                if (item.id==ls.id)
                {
                    item.image = "RadioButtonChecked.png";
                    LS_ViewModel.sortselected = ls.id;
                }
                else
                {
                    item.image = "RadioButtonUnChecked.png";
                }
            }
            var CurrentPage = ViewModels.LS_ViewModel.GetCurrentModalPage();
            var sortdata = CurrentPage.FindByName<ListView>("sortdata");
            sortdata.ItemsSource = null;
            sortdata.ItemsSource = sort;
        }
        public async void SortApply()
        {
            var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
            try { 
            await curpage.Navigation.PopModalAsync();
            await curpage.Navigation.PushAsync(new Views.Listing.LS_Listing());
            curpage.Navigation.RemovePage(curpage.Navigation.NavigationStack[curpage.Navigation.NavigationStack.Count - 2]);
            }
            catch (Exception ee)
            {

            }

        }
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
