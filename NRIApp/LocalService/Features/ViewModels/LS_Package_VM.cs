using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.LocalService.Features.Interfaces;
using NRIApp.LocalService.Features.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NRIApp.LocalService.Features.ViewModels
{
   public class LS_Package_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs _Dialog = UserDialogs.Instance;

        private string _Noofdays1="";
        public string Noofdays1
        {
            get
            {
                return _Noofdays1;
            }
            set
            {
                _Noofdays1 = value;
                OnPropertyChanged(nameof(Noofdays1));
            }
        }
        private string _Noofdays2 = "";
        public string Noofdays2
        {
            get
            {
                return _Noofdays2;
            }
            set
            {
                _Noofdays2 = value;
                OnPropertyChanged(nameof(Noofdays2));
            }
        }
        private string _Noofdays3 = "";
        public string Noofdays3
        {
            get
            {
                return _Noofdays3;
            }
            set
            {
                _Noofdays3 = value;
                OnPropertyChanged(nameof(Noofdays3));
            }
        }
        private string _Noofdays4 = "";
        public string Noofdays4
        {
            get
            {
                return _Noofdays4;
            }
            set
            {
                _Noofdays4 = value;
                OnPropertyChanged(nameof(Noofdays4));
            }
        }
        private string _Noofdays5 = "";
        public string Noofdays5
        {
            get
            {
                return _Noofdays5;
            }
            set
            {
                _Noofdays5 = value;
                OnPropertyChanged(nameof(Noofdays5));
            }
        }

        private string _Packageamount1 = "";
        public string Packageamount1
        {
            get
            {
                return _Packageamount1;
            }
            set
            {
                _Packageamount1 = value;
                OnPropertyChanged(nameof(Packageamount1));
            }
        }
        private string _Packageamount2 = "";
        public string Packageamount2
        {
            get
            {
                return _Packageamount2;
            }
            set
            {
                _Packageamount2 = value;
                OnPropertyChanged(nameof(Packageamount2));
            }
        }
        private string _Packageamount3 = "";
        public string Packageamount3
        {
            get
            {
                return _Packageamount3;
            }
            set
            {
                _Packageamount3 = value;
                OnPropertyChanged(nameof(Packageamount3));
            }
        }
        private string _Packageamount4= "";
        public string Packageamount4
        {
            get
            {
                return _Packageamount4;
            }
            set
            {
                _Packageamount4 = value;
                OnPropertyChanged(nameof(Packageamount4));
            }
        }
        private string _Packageamount5 = "";
        public string Packageamount5
        {
            get
            {
                return _Packageamount5;
            }
            set
            {
                _Packageamount5 = value;
                OnPropertyChanged(nameof(Packageamount5));
            }
        }

        private string _Packageoriginalamount1 = "";
        public string Packageoriginalamount1
        {
            get
            {
                return _Packageoriginalamount1;
            }
            set
            {
                _Packageoriginalamount1 = value;
                OnPropertyChanged(nameof(Packageoriginalamount1));
            }
        }
        private string _Packageoriginalamount2 = "";
        public string Packageoriginalamount2
        {
            get
            {
                return _Packageoriginalamount2;
            }
            set
            {
                _Packageoriginalamount2 = value;
                OnPropertyChanged(nameof(Packageoriginalamount2));
            }
        }
        private string _Packageoriginalamount3 = "";
        public string Packageoriginalamount3
        {
            get
            {
                return _Packageoriginalamount3;
            }
            set
            {
                _Packageoriginalamount3 = value;
                OnPropertyChanged(nameof(Packageoriginalamount3));
            }
        }
        private string _Packageoriginalamount4 = "";
        public string Packageoriginalamount4
        {
            get
            {
                return _Packageoriginalamount4;
            }
            set
            {
                _Packageoriginalamount4 = value;
                OnPropertyChanged(nameof(Packageoriginalamount4));
            }
        }
        private string _Packageoriginalamount5 = "";
        public string Packageoriginalamount5
        {
            get
            {
                return _Packageoriginalamount5;
            }
            set
            {
                _Packageoriginalamount5 = value;
                OnPropertyChanged(nameof(Packageoriginalamount5));
            }
        }

        private string _Packageperdayamount1 = "";
        public string Packageperdayamount1
        {
            get
            {
                return _Packageperdayamount1;
            }
            set
            {
                _Packageperdayamount1 = value;
                OnPropertyChanged(nameof(Packageperdayamount1));
            }
        }
        private string _Packageperdayamount2 = "";
        public string Packageperdayamount2
        {
            get
            {
                return _Packageperdayamount2;
            }
            set
            {
                _Packageperdayamount2 = value;
                OnPropertyChanged(nameof(Packageperdayamount2));
            }
        }
        private string _Packageperdayamount3 = "";
        public string Packageperdayamount3
        {
            get
            {
                return _Packageperdayamount3;
            }
            set
            {
                _Packageperdayamount3 = value;
                OnPropertyChanged(nameof(Packageperdayamount3));
            }
        }
        private string _Packageperdayamount4 = "";
        public string Packageperdayamount4
        {
            get
            {
                return _Packageperdayamount4;
            }
            set
            {
                _Packageperdayamount4 = value;
                OnPropertyChanged(nameof(Packageperdayamount4));
            }
        }
        private string _Packageperdayamount5 = "";
        public string Packageperdayamount5
        {
            get
            {
                return _Packageperdayamount5;
            }
            set
            {
                _Packageperdayamount5 = value;
                OnPropertyChanged(nameof(Packageperdayamount5));
            }
        }


        private string _Packagesavepercentage1 = "";
        public string Packagesavepercentage1
        {
            get
            {
                return _Packagesavepercentage1;
            }
            set
            {
                _Packagesavepercentage1 = value;
                OnPropertyChanged(nameof(Packagesavepercentage1));
            }
        }
        private string _Packagesavepercentage2 = "";
        public string Packagesavepercentage2
        {
            get
            {
                return _Packagesavepercentage2;
            }
            set
            {
                _Packagesavepercentage2 = value;
                OnPropertyChanged(nameof(Packagesavepercentage2));
            }
        }
        private string _Packagesavepercentage3 = "";
        public string Packagesavepercentage3
        {
            get
            {
                return _Packagesavepercentage3;
            }
            set
            {
                _Packagesavepercentage3 = value;
                OnPropertyChanged(nameof(Packagesavepercentage3));
            }
        }
        private string _Packagesavepercentage4 = "";
        public string Packagesavepercentage4
        {
            get
            {
                return _Packagesavepercentage4;
            }
            set
            {
                _Packagesavepercentage4 = value;
                OnPropertyChanged(nameof(Packagesavepercentage4));
            }
        }
        private string _Packagesavepercentage5 = "";
        public string Packagesavepercentage5
        {
            get
            {
                return _Packagesavepercentage5;
            }
            set
            {
                _Packagesavepercentage5 = value;
                OnPropertyChanged(nameof(Packagesavepercentage5));
            }
        }

        //Additional cities
        private string _Additionalcities = "";
        public string Additionalcities
        {
            get
            {
                return _Additionalcities;
            }
            set
            {
                _Additionalcities = value;
                OnPropertyChanged(nameof(Additionalcities));
                if (!string.IsNullOrEmpty(Additionalcities))
                {
                    Bindadditionalcities();
                }
               

            }
        }
        private bool _Cityavail = true;
        public bool Cityavail
        {
            get { return _Cityavail; }
            set
            {
                _Cityavail = value;
                OnPropertyChanged(nameof(Cityavail));
            }
        }
        private List<LOCATION_DATA> _BindLocation;
        public List<LOCATION_DATA> BindLocation
        {
            get { return _BindLocation; }
            set
            {
                _BindLocation = value;

            }
        }
        private List<LOCATION_MERGE_DATA> _BindNearbycities;
        public List<LOCATION_MERGE_DATA> BindNearbycities
        {
            get { return _BindNearbycities; }
            set
            {
                _BindNearbycities = value;

            }
        }
        private string _Citiestext = "";
        public string Citiestext
        {
            get { return _Citiestext; }
            set { _Citiestext = value;
                OnPropertyChanged(nameof(Citiestext));
            }
        }
        private string _Cityamt = "";
        public string Cityamt
        {
            get { return _Cityamt; }
            set
            {
                _Cityamt = value;
                OnPropertyChanged(nameof(Cityamt));
            }
        }
        private string _Cityorgamt = "";
        public string Cityorgamt
        {
            get { return _Cityorgamt; }
            set
            {
                _Cityorgamt = value;
                OnPropertyChanged(nameof(Cityorgamt));
            }
        }
        private string _Cityamtperday = "";
        public string Cityamtperday
        {
            get { return _Cityamtperday; }
            set
            {
                _Cityamtperday = value;
                OnPropertyChanged(nameof(Cityamtperday));
            }
        }
        private string _Cityamtsaveper = "";
        public string Cityamtsaveper
        {
            get { return _Cityamtsaveper; }
            set
            {
                _Cityamtsaveper = value;
                OnPropertyChanged(nameof(Cityamtsaveper));
            }
        }
        private string _Citytotalamt = "";
        public string Citytotalamt
        {
            get { return _Citytotalamt; }
            set
            {
                _Citytotalamt = value;
                OnPropertyChanged(nameof(Citytotalamt));
            }
        }
        private string _Totalpayamount = "";
        public string Totalpayamount
        {
            get { return _Totalpayamount; }
            set
            {
                _Totalpayamount = value;
                OnPropertyChanged(nameof(Totalpayamount));
            }
        }

        private bool _btnonNoofdays1 = true;
        public bool btnonNoofdays1
        {
            get { return _btnonNoofdays1; }
            set
            {
                _btnonNoofdays1 = value;
                OnPropertyChanged(nameof(btnonNoofdays1));
            }
        }
        private bool _btnoffNoofdays1 = false;
        public bool btnoffNoofdays1
        {
            get { return _btnoffNoofdays1; }
            set
            {
                _btnoffNoofdays1 = value;
                OnPropertyChanged(nameof(btnoffNoofdays1));
            }
        }


        private bool _btnonNoofdays2 = true;
        public bool btnonNoofdays2
        {
            get { return _btnonNoofdays2; }
            set
            {
                _btnonNoofdays2 = value;
                OnPropertyChanged(nameof(btnonNoofdays2));
            }
        }
        private bool _btnoffNoofdays2 = false;
        public bool btnoffNoofdays2
        {
            get { return _btnoffNoofdays2; }
            set
            {
                _btnoffNoofdays2 = value;
                OnPropertyChanged(nameof(btnoffNoofdays2));
            }
        }

        private bool _btnonNoofdays3 = true;
        public bool btnonNoofdays3
        {
            get { return _btnonNoofdays3; }
            set
            {
                _btnonNoofdays3 = value;
                OnPropertyChanged(nameof(btnonNoofdays3));
            }
        }
        private bool _btnoffNoofdays3 = false;
        public bool btnoffNoofdays3
        {
            get { return _btnoffNoofdays3; }
            set
            {
                _btnoffNoofdays3 = value;
                OnPropertyChanged(nameof(btnoffNoofdays3));
            }
        }
        private bool _btnonNoofdays4 = true;
        public bool btnonNoofdays4
        {
            get { return _btnonNoofdays4; }
            set
            {
                _btnonNoofdays4 = value;
                OnPropertyChanged(nameof(btnonNoofdays4));
            }
        }
        private bool _btnoffNoofdays4 = false;
        public bool btnoffNoofdays4
        {
            get { return _btnoffNoofdays4; }
            set
            {
                _btnoffNoofdays4 = value;
                OnPropertyChanged(nameof(btnoffNoofdays4));
            }
        }
        private bool _btnonNoofdays5 = true;
        public bool btnonNoofdays5
        {
            get { return _btnonNoofdays5; }
            set
            {
                _btnonNoofdays5 = value;
                OnPropertyChanged(nameof(btnonNoofdays5));
            }
        }
        private bool _btnoffNoofdays5 = false;
        public bool btnoffNoofdays5
        {
            get { return _btnoffNoofdays5; }
            set
            {
                _btnoffNoofdays5 = value;
                OnPropertyChanged(nameof(btnoffNoofdays5));
            }
        }

        private ObservableCollection<GroupServices> items;
        public ObservableCollection<GroupServices> Items
        {
            get { return items; }
            set { items = value; }
        }
        //Additional cities

        public Command Choosepackage { get; set; }
        public Command Stepmoreclick { get; set; }
        public Command CityListcommand { get; set; }
        public Command Backbtncommand { get; set; }
        public Command Modifycitiescommand { get; set;  }
        public Command SkipAddtionalcities { get; set; }
        public Command Submitaddtionalcities { get; set; }

        public string sAdid = "";
        public LS_Package_VM(string adid)
        {
            sAdid = adid;
            BindPackageDetails();
            Choosepackage = new Command<string>(Selectpackage);
            Stepmoreclick = new Command<string>(Clickedstepmore);
            SkipAddtionalcities = new Command(GotoValueServices);
            Submitaddtionalcities = new Command(GotoValueServices);
        }
        decimal totalamt = 0, skipamt=0,cityamt = 0,sPackageamount=0;
        string selecteddays = "";
        SELECTED_PACKAGE sPack = new SELECTED_PACKAGE();
        public LS_Package_VM(int citycount, SELECTED_PACKAGE sp)
        {
            CityListcommand = new Command<LOCATION_DATA>(Cityselection);
            Bindnearbycities();
            Backbtncommand = new Command(async () => await BackbtncommandClick());
            Modifycitiescommand = new Command(Openselectedcities);
            SkipAddtionalcities = new Command(GotoSkipValueServices);
            Submitaddtionalcities = new Command(GotoValueServices);
            if (citycount !=0)
            {
                Selectedcitiescount();
            }
            Cityamt = "$" + sp.mcamount;
            Cityorgamt = "$" + sp.mcoriginalamt;
            Cityamtperday = sp.mcsaveperday;
            Cityamtsaveper = "(" + sp.mcsavepercentage + ")";
            Citytotalamt ="$"+ sp.amount;
            skipamt= totalamt =Convert.ToDecimal(sp.amount);
            cityamt = sp.mcamount;
            sPack.amount = sp.amount;
            sPack.mcamount = sp.mcamount;
            sPack.mcoriginalamt = sp.mcoriginalamt;
            sPack.mcsavepercentage = sp.mcsavepercentage;
            sPack.mcsaveperday = sp.mcsaveperday;
            sPack.packageamount = sp.packageamount;
            sPack.valueserviceamount = sp.valueserviceamount;
            selecteddays= sPack.noofdays = sp.noofdays;
            sPack.adid = sp.adid;
            Totalpayamount ="$"+ Convert.ToString(totalamt);
        }
        public async void GotoSkipValueServices()
        {
            var curpage = LS_ViewModel.GetCurrentPage();
            string data = LS_ViewModel.packagevalueamountdetails[selecteddays].ToString();
            if (LS_ViewModel.addionalcities.Count > 0)
            {
                decimal citychooseamt = LS_ViewModel.addionalcities.Count * sPack.mcamount;
                decimal totamt = Convert.ToDecimal(Totalpayamount.Replace("$","")) - citychooseamt;
                Totalpayamount =Convert.ToString(totamt);
                LS_ViewModel.addionalcities.Clear();
                totalamt = totamt;
                sPack.amount = totalamt;
            }
            var stackmod = curpage.FindByName<StackLayout>("stackmodify");
            stackmod.IsVisible = false;
            Citiestext = "";
             Totalpayamount =Convert.ToString(sPack.amount);
            skipamt = Convert.ToDecimal(Totalpayamount);
            if (BindNearbycities != null)
            {
               
                var stack = curpage.FindByName<StackLayout>("stacknearbycities");
                stack.Children.Clear();
                foreach (var item in BindNearbycities)
                {
                    BoxView box = new BoxView();
                    box.BackgroundColor = Color.FromHex("e0e0e0");
                    box.HeightRequest = 1;

                    AsNum.XFControls.CheckBox chb = new AsNum.XFControls.CheckBox();
                    chb.Text = item.city + ", " + item.statecode + ", " + item.countrycode;
                    chb.OnImg = "CheckBoxChecked.png";
                    chb.OffImg = "CheckBoxUnChecked.png";
                    chb.WidthRequest = 18;
                    chb.HeightRequest = 18;
                    chb.ShowLabel = true;
                    chb.CheckChanged += Chb_CheckChanged;
                    chb.ClassId = item.citystatecodeurl;
                    chb.Padding = 5;
                    if (LS_ViewModel.addionalcities.ContainsKey(item.citystatecodeurl))
                    {
                        chb.Checked = true;
                        //totalamt = totalamt + cityamt;
                        // Totalpayamount = "$" + Convert.ToString(totalamt);
                    }


                    stack.Children.Add(chb);
                    stack.Children.Add(box);
                }
            }
            await curpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Posting.LS_Value_Services(skipamt, data, sPack));
        }
        public async void GotoValueServices()
        {

            if (LS_ViewModel.addionalcities.Count!=0)
            {
                //LS_ViewModel.Valueaddedcontentid = "";
                //LS_ViewModel.valueadded = 0;
                //totalamt = totalamt- sPack.valueserviceamount;
                var curpage = LS_ViewModel.GetCurrentPage();
                string data = LS_ViewModel.packagevalueamountdetails[selecteddays].ToString();
                sPack.addtionalcitiesamount = LS_ViewModel.addionalcities.Count * sPack.mcamount;
                await curpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Posting.LS_Value_Services(totalamt, data, sPack));
            }
            else
            {
                _Dialog.Toast("Choose addtional cities");
            }
           
        }

        public async void Openselectedcities()
        {
            var curpage = LS_ViewModel.GetCurrentPage();
            await curpage.Navigation.PushModalAsync(new NRIApp.LocalService.Features.Views.Posting.LS_Additional_Cities_Display(sPack));
                       
        }

        public void Selectedcitiescount()
        {
            try
            {
                var curpage = LS_ViewModel.GetCurrentPage();
                var stack = curpage.FindByName<StackLayout>("stackmodify");
                var btnnext = curpage.FindByName<Button>("btncitynext");
                var btnskip = curpage.FindByName<Button>("btncityskip");
                if (LS_ViewModel.addionalcities.Count > 0)
                {
                   
                    stack.IsVisible = true;
                    btnnext.IsVisible = true;
                    btnskip.IsVisible = false;

                    if (LS_ViewModel.addionalcities.Count == 1)
                    {

                        Citiestext = "Totally " + LS_ViewModel.addionalcities.Count + " city selected by you";
                    }
                    else
                    {
                        Citiestext = "Totally " + LS_ViewModel.addionalcities.Count + " citie(s) selected by you";
                    }
                }
                else
                {
                    stack.IsVisible = false;
                    btnnext.IsVisible = false;
                    btnskip.IsVisible = true;
                }

            }
            catch (Exception ee)
            {

            }
        }

        public async Task BackbtncommandClick()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async void Bindadditionalcities()
        {
            try
            {
                var location = RestService.For<IServiceAPI>(NRIApp.Helpers.Commonsettings.Localservices);
                var data = await location.GetusandcanadaLocationData(Additionalcities);
                foreach (var item in data.ROW_DS)
                {
                    item.citystatecode = item.city + ", " + item.statecode+", "+item.countrycode;
                }
                BindLocation = data.ROW_DS;
                Cityavail = true;
                OnPropertyChanged(nameof(BindLocation));
                OnPropertyChanged(nameof(Cityavail));

            }
            catch (Exception e)
            {

            }
        }
        public void Cityselection(LOCATION_DATA ld)
        {
            try
            {
                if (!LS_ViewModel.addionalcities.ContainsKey(ld.newcityurl))
                {


                    totalamt = totalamt + cityamt;
                    Totalpayamount = "$"+Convert.ToString(totalamt);
                   LS_ViewModel.addionalcities.Add(ld.newcityurl, ld.city + ", " + ld.statecode + ", " + ld.countrycode);

                    Additionalcities = "";


                    int index = BindNearbycities.FindIndex(item => item.citystatecodeurl == ld.newcityurl);
                    if (index>=0)
                    {


                        var curpage1 = LS_ViewModel.GetCurrentPage();
                        var stack1 = curpage1.FindByName<StackLayout>("stacknearbycities");

                        stack1.Children.Clear();
                        foreach (var item in BindNearbycities)
                        {

                            BoxView box = new BoxView();
                            box.BackgroundColor = Color.FromHex("e0e0e0");
                            box.HeightRequest = 1;

                            AsNum.XFControls.CheckBox chb = new AsNum.XFControls.CheckBox();
                            chb.Text = item.city + ", " + item.statecode + ", " + item.countrycode;
                            chb.OnImg = "CheckBoxChecked.png";
                            chb.OffImg = "CheckBoxUnChecked.png";
                            chb.WidthRequest = 18;
                            chb.HeightRequest = 18;
                            chb.ShowLabel = true;
                            chb.CheckChanged += Chb_CheckChanged;
                            chb.ClassId = item.citystatecodeurl;
                            chb.Padding = 5;
                            if (item.citystatecodeurl == ld.newcityurl)
                            {
                                chb.Checked = true;
                            }
                            stack1.Children.Add(chb);
                            stack1.Children.Add(box);
                        }
                    }




                    Selectedcitiescount();
                    _Dialog.Toast("You have added " + ld.city + ", " + ld.statecode + ", " + ld.countrycode);

                    BindLocation.Clear();
                    Cityavail = false;
                    OnPropertyChanged(nameof(BindLocation));
                    OnPropertyChanged(nameof(Cityavail));
                }
                else
                {
                    _Dialog.Toast("Already added " + ld.city + ", " + ld.statecode + ", " + ld.countrycode);
                }
            }catch(Exception ee)
            {

            }
            

        }
        

        public async void Bindnearbycities()
        {
            try
            {
                _Dialog.ShowLoading("");
                var nearbycities = RestService.For<IServiceAPI>(Commonsettings.Localservices);

                //if (string.IsNullOrEmpty(Commonsettings.Usermetrourl))
                //{
                //    Commonsettings.Usermetrourl = "new-york-metro-area";
                //}
                var data = await nearbycities.GetNearbycities(Commonsettings.Usermetrourl);
                if (data.ROW_DS != null)
                {
                    BindNearbycities = data.ROW_DS;
                    var curpage = LS_ViewModel.GetCurrentPage();
                    var stack = curpage.FindByName<StackLayout>("stacknearbycities");
                    stack.Children.Clear();
                    foreach (var item in data.ROW_DS)
                    {
                        BoxView box = new BoxView();
                        box.BackgroundColor = Color.FromHex("e0e0e0");
                        box.HeightRequest = 1;
                        
                        AsNum.XFControls.CheckBox chb = new AsNum.XFControls.CheckBox();
                        chb.Text = item.city + ", " + item.statecode + ", " + item.countrycode;
                        chb.OnImg = "CheckBoxChecked.png";
                        chb.OffImg = "CheckBoxUnChecked.png";
                        chb.WidthRequest = 18;
                        chb.HeightRequest = 18;
                        chb.ShowLabel = true;                        
                        chb.CheckChanged += Chb_CheckChanged;
                        chb.ClassId = item.citystatecodeurl;
                        chb.Padding = 5;
                        if (LS_ViewModel.addionalcities.ContainsKey(item.citystatecodeurl))
                        {
                            chb.Checked = true;
                            //totalamt = totalamt + cityamt;
                           // Totalpayamount = "$" + Convert.ToString(totalamt);
                        }


                        stack.Children.Add(chb);
                        stack.Children.Add(box);
                    }
                }
                Selectedcitiescount();
                _Dialog.HideLoading();
            }
            catch(Exception e)
            {

            }
            
        }

        public void Chb_CheckChanged(object sender, EventArgs e)
        {
            var chk = sender as AsNum.XFControls.CheckBox;
            if (chk.Checked)
            {
                if (!LS_ViewModel.addionalcities.ContainsKey(chk.ClassId))
                {
                   
                    LS_ViewModel.addionalcities.Add(chk.ClassId, chk.Text);
                    totalamt = totalamt + cityamt;
                    Totalpayamount = "$" + Convert.ToString(totalamt);
                    Selectedcitiescount();


                }
                else
                {
                    _Dialog.Toast("Already added " +chk.Text);
                }
            }
            else
            {

                LS_ViewModel.addionalcities.Remove(chk.ClassId);

                totalamt = totalamt - cityamt;
                Totalpayamount = "$" + Convert.ToString(totalamt);
                Selectedcitiescount();
               


            }
            }

        public async  void Selectpackage(string days)
        {
            selecteddays = days.Replace(" Days", "");
            string packamnt =LS_ViewModel.packageamount[days.Replace(" Days", "")].ToString();
            int packcityamnt =Convert.ToInt32(packagecityamount[days.Replace(" Days", "")].ToString());
            string citypackagedetails= packagecityamountdetails[days.Replace(" Days", "")].ToString();
            string[] packagearr = citypackagedetails.Split(',');
            int packcityorgamnt = Convert.ToInt32(packagearr[1].ToString());
            sPackageamount= totalamt =Convert.ToDecimal(packamnt);
            SELECTED_PACKAGE sp = new SELECTED_PACKAGE()
            {
                noofdays= selecteddays,
                packageamount= totalamt,
                amount = totalamt,
                mcamount = packcityamnt,
                mcoriginalamt = packcityorgamnt,
                mcsavepercentage = packagearr[2].ToString(),
                mcsaveperday = packagearr[3].ToString()
            };
            LS_ViewModel.addionalcities.Clear();
            LS_ViewModel.valueadded = 0;
            LS_ViewModel.Valueaddedcontentid = "";
            var curpage = LS_ViewModel.GetCurrentPage();

            string[] noofdaysarr= new string[LS_ViewModel.packageamount.Count];
            int i = 0;
            foreach (KeyValuePair<string, string> dayskey in LS_ViewModel.packageamount)
            {

                noofdaysarr[i] = dayskey.Key;
                i = i + 1;
              
            }

            int a = Array.IndexOf(noofdaysarr, selecteddays);
            if (a==0)
            {
                btnonNoofdays1 = false; btnoffNoofdays1 = true;
                btnonNoofdays2 = true; btnoffNoofdays2 = false;
                btnonNoofdays3 = true; btnoffNoofdays3 = false;
                btnonNoofdays4 = true; btnoffNoofdays4 = false;
                btnonNoofdays5 = true; btnoffNoofdays5 = false;

            }
            else if (a == 1)
            {
                btnonNoofdays2 = false; btnoffNoofdays2 = true;
                btnonNoofdays1 = true; btnoffNoofdays1 = false;
                btnonNoofdays3 = true; btnoffNoofdays3 = false;
                btnonNoofdays4 = true; btnoffNoofdays4 = false;
                btnonNoofdays5 = true; btnoffNoofdays5 = false;

            }
            else if (a == 2)
            {
                btnonNoofdays3 = false; btnoffNoofdays3 = true;
                btnonNoofdays1 = true; btnoffNoofdays1 = false;
                btnonNoofdays2 = true; btnoffNoofdays2 = false;
                btnonNoofdays4 = true; btnoffNoofdays4 = false;
                btnonNoofdays5 = true; btnoffNoofdays5 = false;

            }
            else if (a == 3)
            {
                btnonNoofdays4 = false; btnoffNoofdays4 = true;
                btnonNoofdays1 = true; btnoffNoofdays1 = false;
                btnonNoofdays2 = true; btnoffNoofdays2 = false;
                btnonNoofdays3 = true; btnoffNoofdays3 = false;
                btnonNoofdays5 = true; btnoffNoofdays5 = false;

            }
            else if (a == 4)
            {
                btnonNoofdays5 = false; btnoffNoofdays5 = true;
                btnonNoofdays1 = true; btnoffNoofdays1 = false;
                btnonNoofdays2 = true; btnoffNoofdays2 = false;
                btnonNoofdays3 = true; btnoffNoofdays3 = false;
                btnonNoofdays4 = true; btnoffNoofdays4 = false;

            }
            OnPropertyChanged(nameof(btnonNoofdays1)); OnPropertyChanged(nameof(btnoffNoofdays1));
            OnPropertyChanged(nameof(btnonNoofdays2)); OnPropertyChanged(nameof(btnoffNoofdays2));
            OnPropertyChanged(nameof(btnonNoofdays3)); OnPropertyChanged(nameof(btnoffNoofdays3));
            OnPropertyChanged(nameof(btnonNoofdays4)); OnPropertyChanged(nameof(btnoffNoofdays4));
            OnPropertyChanged(nameof(btnonNoofdays5)); OnPropertyChanged(nameof(btnoffNoofdays5));

            await curpage.Navigation.PushAsync(new LocalService.Features.Views.Posting.LS_Additional_Cities(0, sp));

        }
        public void Clickedstepmore(string stepno)
        {
            string stackname = "stackstepmore" + stepno;
            string lblname = "lblstepmore" + stepno;
            var curpage = ViewModels.LS_ViewModel.GetCurrentPage();
            var stack = curpage.FindByName<StackLayout>(stackname);
            var labl = curpage.FindByName<Label>(lblname);
            if (stack.IsVisible == true)
            {
                stack.IsVisible = false;
                labl.Text = labl.Text.Replace("less", "more");
            }
            else
            {
                stack.IsVisible = true;
                labl.Text = labl.Text.Replace("more", "less");
            }
        }
        
        IDictionary<string, string> packagecityamount = new Dictionary<string, string>();
        IDictionary<string, string> packagecityamountdetails = new Dictionary<string, string>();
        


        public async void BindPackageDetails()
        {
            try
            {
                var package = RestService.For<IServiceAPI>(Commonsettings.TechjobsAPI);
                var data = await package.GetPackageData();
                if (data !=null)
                {
                   

                    Noofdays1 =  data.ROW_DATA[0].noofdays.ToString()+" Days";
                    Noofdays2 =  data.ROW_DATA[1].noofdays.ToString()+" Days";
                    Noofdays3 =  data.ROW_DATA[2].noofdays.ToString()+" Days";
                    Noofdays4 =  data.ROW_DATA[3].noofdays.ToString()+" Days";
                    Noofdays5 =  data.ROW_DATA[4].noofdays.ToString()+" Days";


                    Packageamount1 = "$"+data.ROW_DATA[0].amount.ToString();
                    Packageamount2 = "$"+data.ROW_DATA[1].amount.ToString();
                    Packageamount3 = "$"+data.ROW_DATA[2].amount.ToString();
                    Packageamount4 = "$"+data.ROW_DATA[3].amount.ToString();
                    Packageamount5 = "$"+data.ROW_DATA[4].amount.ToString();

                  

                    Packageoriginalamount1 = "$"+  data.ROW_DATA[0].originalamt.ToString();
                    Packageoriginalamount2 = "$"+  data.ROW_DATA[1].originalamt.ToString();
                    Packageoriginalamount3 = "$"+  data.ROW_DATA[2].originalamt.ToString();
                    Packageoriginalamount4 = "$"+  data.ROW_DATA[3].originalamt.ToString();
                    Packageoriginalamount5 = "$"+  data.ROW_DATA[4].originalamt.ToString();

                    Packagesavepercentage1 = data.ROW_DATA[0].savepercentage.ToString();
                    Packagesavepercentage2 = data.ROW_DATA[1].savepercentage.ToString();
                    Packagesavepercentage3 = data.ROW_DATA[2].savepercentage.ToString();
                    Packagesavepercentage4 = data.ROW_DATA[3].savepercentage.ToString();
                    Packagesavepercentage5 = data.ROW_DATA[4].savepercentage.ToString();

                    Packageperdayamount1 =  data.ROW_DATA[0].saveperday.ToString();
                    Packageperdayamount2 =  data.ROW_DATA[1].saveperday.ToString();
                    Packageperdayamount3 =  data.ROW_DATA[2].saveperday.ToString();
                    Packageperdayamount4 =  data.ROW_DATA[3].saveperday.ToString();
                    Packageperdayamount5 =  data.ROW_DATA[4].saveperday.ToString();

                    OnPropertyChanged(nameof(Noofdays1)); OnPropertyChanged(nameof(Noofdays2));
                    OnPropertyChanged(nameof(Noofdays3)); OnPropertyChanged(nameof(Noofdays4));
                    OnPropertyChanged(nameof(Noofdays5));
                    OnPropertyChanged(nameof(Packageamount1)); OnPropertyChanged(nameof(Packageamount2));
                    OnPropertyChanged(nameof(Packageamount3)); OnPropertyChanged(nameof(Packageamount4));
                    OnPropertyChanged(nameof(Packageamount5));
                    OnPropertyChanged(nameof(Packageoriginalamount1)); OnPropertyChanged(nameof(Packageoriginalamount2));
                    OnPropertyChanged(nameof(Packageoriginalamount3)); OnPropertyChanged(nameof(Packageoriginalamount4));
                    OnPropertyChanged(nameof(Packageoriginalamount5));
                    OnPropertyChanged(nameof(Packagesavepercentage1)); OnPropertyChanged(nameof(Packagesavepercentage2));
                    OnPropertyChanged(nameof(Packagesavepercentage3)); OnPropertyChanged(nameof(Packagesavepercentage4));
                    OnPropertyChanged(nameof(Packagesavepercentage5));
                    OnPropertyChanged(nameof(Packageperdayamount1)); OnPropertyChanged(nameof(Packageperdayamount2));
                    OnPropertyChanged(nameof(Packageperdayamount3)); OnPropertyChanged(nameof(Packageperdayamount4));
                    OnPropertyChanged(nameof(Packageperdayamount5));

                    for (int i = 0; i < data.ROW_DATA.Count ; i++)
                    {
                        LS_ViewModel.packageamount.Add(data.ROW_DATA[i].noofdays.ToString(), data.ROW_DATA[i].amount.ToString());
                        packagecityamount.Add(data.ROW_DATA[i].noofdays.ToString(), data.ROW_DATA[i].mcamount.ToString());
                        packagecityamountdetails.Add(data.ROW_DATA[i].noofdays.ToString(), data.ROW_DATA[i].mcamount.ToString() + "," + data.ROW_DATA[i].mcoriginalamt.ToString() + "," + data.ROW_DATA[i].mcsavepercentage.ToString() + "," + data.ROW_DATA[i].mcsaveperday.ToString());
                        LS_ViewModel.packagevalueamountdetails.Add(data.ROW_DATA[i].noofdays.ToString(), data.ROW_DATA[i].addonamountbasic.ToString() + "," + data.ROW_DATA[i].addonamountsilver.ToString() + "," + data.ROW_DATA[i].addonamountbasic_originalamt.ToString() + "," + data.ROW_DATA[i].addonamountbasic_savepercentage.ToString() + "," + data.ROW_DATA[i].addonamountbasic_saveperday.ToString() + "," + data.ROW_DATA[i].addonamountsilver_originalamt.ToString() + "," + data.ROW_DATA[i].addonamountsilver_savepercentage.ToString() + "," + data.ROW_DATA[i].addonamountsilver_saveperday.ToString());
                    }
                    if (LS_ViewModel.isOTPpage == 1)
                    {
                        var currentpage = ViewModels.LS_ViewModel.GetCurrentPage();
                        currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                        LS_ViewModel.isOTPpage = 0;

                    }

                }
            }
            catch(Exception e)
            {

            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
