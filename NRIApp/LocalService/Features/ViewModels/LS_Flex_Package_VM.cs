using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.LocalService.Features.Interfaces;
using NRIApp.LocalService.Features.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using Xamarin.Forms;

namespace NRIApp.LocalService.Features.ViewModels
{
    public class LS_Flex_Package_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs _Dialog = UserDialogs.Instance;

        private int _pack1_days = 0;
        public int pack1_days
        {
            get
            {
                return _pack1_days;
            }
            set
            {
                _pack1_days = value;
                OnPropertyChanged(nameof(pack1_days));
            }
        }
        private int _pack2_days = 0;
        public int pack2_days
        {
            get
            {
                return _pack2_days;
            }
            set
            {
                _pack2_days = value;
                OnPropertyChanged(nameof(pack2_days));
            }
        }
        private int _pack3_days = 0;
        public int pack3_days
        {
            get
            {
                return _pack3_days;
            }
            set
            {
                _pack3_days = value;
                OnPropertyChanged(nameof(pack3_days));
            }
        }
        private int _pack4_days = 0;
        public int pack4_days
        {
            get
            {
                return _pack4_days;
            }
            set
            {
                _pack4_days = value;
                OnPropertyChanged(nameof(pack4_days));
            }
        }
        private int _pack5_days = 0;
        public int pack5_days
        {
            get
            {
                return _pack5_days;
            }
            set
            {
                _pack5_days = value;
                OnPropertyChanged(nameof(pack5_days));
            }
        }
        private int _pack6_days = 0;
        public int pack6_days
        {
            get
            {
                return _pack6_days;
            }
            set
            {
                _pack6_days = value;
                OnPropertyChanged(nameof(pack6_days));
            }
        }

        private string _pack1_days_txt ="";
        public string pack1_days_txt
        {
            get
            {
                return _pack1_days_txt;
            }
            set
            {
                _pack1_days_txt = value;
                OnPropertyChanged(nameof(pack1_days_txt));
            }
        }
        private string _pack2_days_txt = "";
        public string pack2_days_txt
        {
            get
            {
                return _pack2_days_txt;
            }
            set
            {
                _pack2_days_txt = value;
                OnPropertyChanged(nameof(pack2_days_txt));
            }
        }
        private string _pack3_days_txt = "";
        public string pack3_days_txt
        {
            get
            {
                return _pack3_days_txt;
            }
            set
            {
                _pack3_days_txt = value;
                OnPropertyChanged(nameof(pack3_days_txt));
            }
        }
        private string _pack4_days_txt = "";
        public string pack4_days_txt
        {
            get
            {
                return _pack4_days_txt;
            }
            set
            {
                _pack4_days_txt = value;
                OnPropertyChanged(nameof(pack4_days_txt));
            }
        }
        private string _pack5_days_txt = "";
        public string pack5_days_txt
        {
            get
            {
                return _pack5_days_txt;
            }
            set
            {
                _pack5_days_txt = value;
                OnPropertyChanged(nameof(pack5_days_txt));
            }
        }
        private string _pack6_days_txt = "";
        public string pack6_days_txt
        {
            get
            {
                return _pack6_days_txt;
            }
            set
            {
                _pack6_days_txt = value;
                OnPropertyChanged(nameof(pack6_days_txt));
            }
        }


        private int _pack1_months = 0;
        public int pack1_months
        {
            get
            {
                return _pack1_months;
            }
            set
            {
                _pack1_months = value;
                OnPropertyChanged(nameof(pack1_months));
            }
        }
        private int _pack2_months = 0;
        public int pack2_months
        {
            get
            {
                return _pack2_months;
            }
            set
            {
                _pack2_months = value;
                OnPropertyChanged(nameof(pack2_months));
            }
        }
        private int _pack3_months = 0;
        public int pack3_months
        {
            get
            {
                return _pack3_months;
            }
            set
            {
                _pack3_months = value;
                OnPropertyChanged(nameof(pack3_months));
            }
        }
        private int _pack4_months = 0;
        public int pack4_months
        {
            get
            {
                return _pack4_months;
            }
            set
            {
                _pack4_months = value;
                OnPropertyChanged(nameof(pack4_months));
            }
        }
        private int _pack5_months = 0;
        public int pack5_months
        {
            get
            {
                return _pack5_months;
            }
            set
            {
                _pack5_months = value;
                OnPropertyChanged(nameof(pack5_months));
            }
        }
        private int _pack6_months = 0;
        public int pack6_months
        {
            get
            {
                return _pack6_months;
            }
            set
            {
                _pack6_months = value;
                OnPropertyChanged(nameof(pack6_months));
            }
        }
        private string _pack1_name = "";
        public string pack1_name
        {
            get
            {
                return _pack1_name;
            }
            set
            {
                _pack1_name = value;
                OnPropertyChanged(nameof(pack1_name));
            }
        }
        private string _pack2_name = "";
        public string pack2_name
        {
            get
            {
                return _pack2_name;
            }
            set
            {
                _pack2_name = value;
                OnPropertyChanged(nameof(pack2_name));
            }
        }
        private string _pack3_name = "";
        public string pack3_name
        {
            get
            {
                return _pack3_name;
            }
            set
            {
                _pack3_name = value;
                OnPropertyChanged(nameof(pack3_name));
            }
        }
        private string _pack4_name = "";
        public string pack4_name
        {
            get
            {
                return _pack4_name;
            }
            set
            {
                _pack4_name = value;
                OnPropertyChanged(nameof(pack4_name));
            }
        }
        private string _pack5_name = "";
        public string pack5_name
        {
            get
            {
                return _pack5_name;
            }
            set
            {
                _pack5_name = value;
                OnPropertyChanged(nameof(pack5_name));
            }
        }
        private string _pack6_name = "";
        public string pack6_name
        {
            get
            {
                return _pack6_name;
            }
            set
            {
                _pack6_name = value;
                OnPropertyChanged(nameof(pack6_name));
            }
        }
        private float _pack1_overall_amount = 0;
        public float pack1_overall_amount
        {
            get
            {
                return _pack1_overall_amount;
            }
            set
            {
                _pack1_overall_amount = value;
                OnPropertyChanged(nameof(pack1_overall_amount));
            }
        }
        private float _pack2_overall_amount = 0;
        public float pack2_overall_amount
        {
            get
            {
                return _pack2_overall_amount;
            }
            set
            {
                _pack2_overall_amount = value;
                OnPropertyChanged(nameof(pack2_overall_amount));
            }
        }
        private float _pack3_overall_amount = 0;
        public float pack3_overall_amount
        {
            get
            {
                return _pack3_overall_amount;
            }
            set
            {
                _pack3_overall_amount = value;
                OnPropertyChanged(nameof(pack3_overall_amount));
            }
        }
        private float _pack4_overall_amount = 0;
        public float pack4_overall_amount
        {
            get
            {
                return _pack4_overall_amount;
            }
            set
            {
                _pack4_overall_amount = value;
                OnPropertyChanged(nameof(pack4_overall_amount));
            }
        }
        private float _pack5_overall_amount = 0;
        public float pack5_overall_amount
        {
            get
            {
                return _pack5_overall_amount;
            }
            set
            {
                _pack5_overall_amount = value;
                OnPropertyChanged(nameof(pack5_overall_amount));
            }
        }
        private float _pack6_overall_amount = 0;
        public float pack6_overall_amount
        {
            get
            {
                return _pack6_overall_amount;
            }
            set
            {
                _pack6_overall_amount = value;
                OnPropertyChanged(nameof(pack6_overall_amount));
            }
        }

        private int _pack1_overall_amount_max = 0;
        public int pack1_overall_amount_max
        {
            get
            {
                return _pack1_overall_amount_max;
            }
            set
            {
                _pack1_overall_amount_max = value;
                OnPropertyChanged(nameof(pack1_overall_amount_max));
            }
        }
        private int _pack2_overall_amount_max = 0;
        public int pack2_overall_amount_max
        {
            get
            {
                return _pack2_overall_amount_max;
            }
            set
            {
                _pack2_overall_amount_max = value;
                OnPropertyChanged(nameof(pack2_overall_amount_max));
            }
        }
        private int _pack3_overall_amount_max = 0;
        public int pack3_overall_amount_max
        {
            get
            {
                return _pack3_overall_amount_max;
            }
            set
            {
                _pack3_overall_amount_max = value;
                OnPropertyChanged(nameof(pack3_overall_amount_max));
            }
        }
        private int _pack4_overall_amount_max = 0;
        public int pack4_overall_amount_max
        {
            get
            {
                return _pack4_overall_amount_max;
            }
            set
            {
                _pack4_overall_amount_max = value;
                OnPropertyChanged(nameof(pack4_overall_amount_max));
            }
        }
        private int _pack5_overall_amount_max = 0;
        public int pack5_overall_amount_max
        {
            get
            {
                return _pack5_overall_amount_max;
            }
            set
            {
                _pack5_overall_amount_max = value;
                OnPropertyChanged(nameof(pack5_overall_amount_max));
            }
        }
        private int _pack6_overall_amount_max = 0;
        public int pack6_overall_amount_max
        {
            get
            {
                return _pack6_overall_amount_max;
            }
            set
            {
                _pack6_overall_amount_max = value;
                OnPropertyChanged(nameof(pack6_overall_amount_max));

            }
        }

        private bool _framepack1 = true;
        public bool framepack1
        {
            get
            {
                return _framepack1;
            }
            set
            {
                _framepack1 = value;
                OnPropertyChanged(nameof(framepack1));

            }
        }
        private bool _framepack2 = false;
        public bool framepack2
        {
            get
            {
                return _framepack2;
            }
            set
            {
                _framepack2 = value;
                OnPropertyChanged(nameof(framepack2));

            }
        }
        private bool _framepack3 = false;
        public bool framepack3
        {
            get
            {
                return _framepack3;
            }
            set
            {
                _framepack3 = value;
                OnPropertyChanged(nameof(framepack3));

            }
        }
        private bool _framepack4 = false;
        public bool framepack4
        {
            get
            {
                return _framepack4;
            }
            set
            {
                _framepack4 = value;
                OnPropertyChanged(nameof(framepack4));

            }
        }
        private bool _framepack5 = false;
        public bool framepack5
        {
            get
            {
                return _framepack5;
            }
            set
            {
                _framepack5 = value;
                OnPropertyChanged(nameof(framepack5));

            }
        }
        private bool _framepack6 = false;
        public bool framepack6
        {
            get
            {
                return _framepack6;
            }
            set
            {
                _framepack6 = value;
                OnPropertyChanged(nameof(framepack6));

            }
        }

        private string _pack1backgroundcolor = "#2e74f0";
        public string pack1backgroundcolor
        {
            get
            {
                return _pack1backgroundcolor;
            }
            set
            {
                _pack1backgroundcolor = value;
                OnPropertyChanged(nameof(pack1backgroundcolor));

            }
        }
        private string _pack1textcolor = "#ffffff";
        public string pack1textcolor
        {
            get
            {
                return _pack1textcolor;
            }
            set
            {
                _pack1textcolor = value;
                OnPropertyChanged(nameof(pack1textcolor));

            }
        }

        private string _pack2backgroundcolor = "#ffffff";
        public string pack2backgroundcolor
        {
            get
            {
                return _pack2backgroundcolor;
            }
            set
            {
                _pack2backgroundcolor = value;
                OnPropertyChanged(nameof(pack2backgroundcolor));

            }
        }
        private string _pack2textcolor = "#2e74f0";
        public string pack2textcolor
        {
            get
            {
                return _pack2textcolor;
            }
            set
            {
                _pack2textcolor = value;
                OnPropertyChanged(nameof(pack2textcolor));

            }
        }

        private string _pack3backgroundcolor = "#ffffff";
        public string pack3backgroundcolor
        {
            get
            {
                return _pack3backgroundcolor;
            }
            set
            {
                _pack3backgroundcolor = value;
                OnPropertyChanged(nameof(pack3backgroundcolor));

            }
        }
        private string _pack3textcolor = "#2e74f0";
        public string pack3textcolor
        {
            get
            {
                return _pack3textcolor;
            }
            set
            {
                _pack3textcolor = value;
                OnPropertyChanged(nameof(pack3textcolor));

            }
        }


        private string _pack4backgroundcolor = "#ffffff";
        public string pack4backgroundcolor
        {
            get
            {
                return _pack4backgroundcolor;
            }
            set
            {
                _pack4backgroundcolor = value;
                OnPropertyChanged(nameof(pack4backgroundcolor));

            }
        }
        private string _pack4textcolor = "#2e74f0";
        public string pack4textcolor
        {
            get
            {
                return _pack4textcolor;
            }
            set
            {
                _pack4textcolor = value;
                OnPropertyChanged(nameof(pack4textcolor));

            }
        }

        private string _pack5backgroundcolor = "#ffffff";
        public string pack5backgroundcolor
        {
            get
            {
                return _pack5backgroundcolor;
            }
            set
            {
                _pack5backgroundcolor = value;
                OnPropertyChanged(nameof(pack5backgroundcolor));

            }
        }
        private string _pack5textcolor = "#2e74f0";
        public string pack5textcolor
        {
            get
            {
                return _pack5textcolor;
            }
            set
            {
                _pack5textcolor = value;
                OnPropertyChanged(nameof(pack5textcolor));

            }
        }

        private string _pack6backgroundcolor = "#ffffff";
        public string pack6backgroundcolor
        {
            get
            {
                return _pack6backgroundcolor;
            }
            set
            {
                _pack6backgroundcolor = value;
                OnPropertyChanged(nameof(pack6backgroundcolor));

            }
        }
        private string _pack6textcolor = "#2e74f0";
        public string pack6textcolor
        {
            get
            {
                return _pack6textcolor;
            }
            set
            {
                _pack6textcolor = value;
                OnPropertyChanged(nameof(pack6textcolor));

            }
        }




        private string _Choosedamount = "0";
        public string Choosedamount
        {
            get
            {
                return _Choosedamount;
            }
            set
            {
                _Choosedamount = value;
                OnPropertyChanged(nameof(Choosedamount));

            }
        }
        private string _image1 = "nextarrow.png";
        public string image1
        {
            get
            {
                return _image1;
            }
            set
            {
                _image1 = value;
                OnPropertyChanged(nameof(image1));

            }
        }
        private string _image2 = "nextarrow.png";
        public string image2
        {
            get
            {
                return _image2;
            }
            set
            {
                _image2 = value;
                OnPropertyChanged(nameof(image2));

            }
        }

        private string _image3 = "nextarrow.png";
        public string image3
        {
            get
            {
                return _image3;
            }
            set
            {
                _image3 = value;
                OnPropertyChanged(nameof(image3));

            }
        }
        private string _image4 = "nextarrow.png";
        public string image4
        {
            get
            {
                return _image4;
            }
            set
            {
                _image4 = value;
                OnPropertyChanged(nameof(image4));

            }
        }

        private string _image5 = "nextarrow.png";
        public string image5
        {
            get
            {
                return _image5;
            }
            set
            {
                _image5 = value;
                OnPropertyChanged(nameof(image5));

            }
        }
        private string _image6 = "nextarrow.png";
        public string image6
        {
            get
            {
                return _image6;
            }
            set
            {
                _image6 = value;
                OnPropertyChanged(nameof(image6));

            }
        }


        private string _image7 = "nextarrow.png";
        public string image7
        {
            get
            {
                return _image7;
            }
            set
            {
                _image7 = value;
                OnPropertyChanged(nameof(image7));

            }
        }
        private string _image8 = "nextarrow.png";
        public string image8
        {
            get
            {
                return _image8;
            }
            set
            {
                _image8 = value;
                OnPropertyChanged(nameof(image8));

            }
        }






        private bool _Listview1show = true;
        public bool Listview1show
        {
            get
            {
                return _Listview1show;
            }
            set
            {
                _Listview1show = value;
                OnPropertyChanged(nameof(Listview1show));

            }
        }
        private bool _Listview2show = false;
        public bool Listview2show
        {
            get
            {
                return _Listview2show;
            }
            set
            {
                _Listview2show = value;
                OnPropertyChanged(nameof(Listview2show));

            }
        }
        private bool _Listview3show = false;
        public bool Listview3show
        {
            get
            {
                return _Listview3show;
            }
            set
            {
                _Listview3show = value;
                OnPropertyChanged(nameof(Listview3show));

            }
        }
        private bool _Listview4show = false;
        public bool Listview4show
        {
            get
            {
                return _Listview4show;
            }
            set
            {
                _Listview4show = value;
                OnPropertyChanged(nameof(Listview4show));

            }
        }
        private bool _Listview5show = false;
        public bool Listview5show
        {
            get
            {
                return _Listview5show;
            }
            set
            {
                _Listview5show = value;
                OnPropertyChanged(nameof(Listview5show));

            }
        }
        private bool _Listview6show = false;
        public bool Listview6show
        {
            get
            {
                return _Listview6show;
            }
            set
            {
                _Listview6show = value;
                OnPropertyChanged(nameof(Listview6show));

            }
        }
        private bool _Listview7show = false;
        public bool Listview7show
        {
            get
            {
                return _Listview7show;
            }
            set
            {
                _Listview7show = value;
                OnPropertyChanged(nameof(Listview7show));

            }
        }
        private bool _Listview8show = false;
        public bool Listview8show
        {
            get
            {
                return _Listview8show;
            }
            set
            {
                _Listview8show = value;
                OnPropertyChanged(nameof(Listview8show));

            }
        }


        private int _Height1 = 50;
        public int Height1
        {
            get
            {
                return _Height1;
            }
            set
            {
                _Height1 = value;
                OnPropertyChanged(nameof(Height1));

            }
        }
       
        public static IDictionary<int, float> flexpackageamount = new Dictionary<int, float>();
        public static IDictionary<int, int> flexpackagetype = new Dictionary<int, int>();
        List<int> daysdata = new List<int>();
        List<LS_PAYMENT_FEATURE> listgrpdata = new List<LS_PAYMENT_FEATURE>();
        public Command Choosemonths { get; set; }
        public Command Selectessential { get; set; }
        public Command Selectpremier { get; set; }
        public Command Chooseflexpackage { get; set; }
        public Command Choosepackagetype { get; set; }
        //public Command Listview1tapped { get; set; }
        public ICommand Listview1tapped1 { get; set; }
        public Command<LS_PAYMENT_FEATURES> Listviewwithdata { get; set; }
        public ICommand Listview1tapped { get; set; }
        public ICommand Listview2tapped { get; set; }
        public ICommand Listview3tapped { get; set; }
        public ICommand Listview4tapped { get; set; }
        public ICommand Listview5tapped { get; set; }
        public ICommand Listview6tapped { get; set; }
        public ICommand Listview7tapped { get; set; }
        public ICommand Listview8tapped { get; set; }
        public Command<GroupFeatures> RefreshItemsCommand
        {
            get;
            set;
        }
        private GroupFeatures _oldFeature;
        List<LS_FORM_Radio> data = new List<LS_FORM_Radio>();
        private ObservableCollection<GroupFeatures> items;
        public ObservableCollection<GroupFeatures> Items
        {
            get { return items; }
            set { items = value; }
        }

        public string sAdid = "", sOldclpostid = "";
        public LS_Flex_Package_VM(string oldclpostid, string adid)
        {
            items = new ObservableCollection<GroupFeatures>();
            // Bindpackagemonth();
            //Choosemonths = new Command<LS_FORM_Radio>(Selectmonths);
            sOldclpostid = oldclpostid; sAdid = adid;
            Selectessential = new Command(Selectpackessential);
            Selectpremier = new Command(Selectpackpremier);
            Chooseflexpackage = new Command<int>(Selectpackage);
            Choosepackagetype = new Command<int>(Selectpackagetype);
            Bindpackageamount();
            RefreshItemsCommand = new Command<GroupFeatures>((Items2) => ExecuteRefreshItemsCommand(Items2));
            // Listview1tapped = new Command<IEnumerable<LS_PAYMENT_FEATURES>>(Listcheckdata1);

            if (LS_ViewModel.isrenewclickble == 1)
            {
                Bindrenewpackage(oldclpostid);
            }





        }

        public async void Bindrenewpackage(string sAdid)
        {
            try
            {
                if (!string.IsNullOrEmpty(sAdid))
                {
                    _Dialog.ShowLoading("");
                    await Task.Delay(3000);
                    var addetails = RestService.For<IServiceAPI>(Commonsettings.Localservices);
                    var data = await addetails.Getrenewpackagedetails(sAdid);
                    if (data.ROW_DATA.Count > 0)
                    {
                        var curpage = LS_ViewModel.GetCurrentPage();

                        int numberdays =Convert.ToInt32(data.ROW_DATA[0].numberofdays);

                        _Dialog.HideLoading();
                        Selectpackage(numberdays);

                        Selectpackagetype(numberdays);

                    }
                   
                }

            }
            catch(Exception e)
            {

            }
        }
        private void ExecuteRefreshItemsCommand(GroupFeatures HV)
        {
            if (_oldFeature == HV)
            {
                // click twice on the same item will hide it  
                HV.Expanded = !HV.Expanded;
                if (!HV.Expanded)
                {
                    Height1 = (listgrpdata.Count * 40) + (listgrpdata.Count * 10);
                    OnPropertyChanged(nameof(Height1));
                }
                else
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        Height1 = ((HV.Count + listgrpdata.Count) * 40) ;
                    }
                    else
                    {
                        Height1 = ((HV.Count + listgrpdata.Count) * 40) + ((HV.Count + listgrpdata.Count) * 20);
                    }
                   
                    OnPropertyChanged(nameof(Height1));
                }

            }
            else
            {
                if (_oldFeature != null)
                {
                    // hide previous selected item  
                    _oldFeature.Expanded = false;
                    Height1 = (listgrpdata.Count * 40) + (listgrpdata.Count * 10); ;
                    OnPropertyChanged(nameof(Height1));
                }
                // show selected item  
                HV.Expanded = true;
                if (Device.RuntimePlatform == Device.iOS)
                {
                    Height1 = ((HV.Count + listgrpdata.Count) * 40);
                }
                else
                {
                    Height1 = ((HV.Count + listgrpdata.Count) * 40) + ((HV.Count + listgrpdata.Count) * 20);
                }
                OnPropertyChanged(nameof(Height1));

            }
            _oldFeature = HV;

        }

      

       



     
        public List<LS_PAYMENT_FEATURES> Paymentfeatures { get; set; }
        string list1 = "0", list2 = "0", list3 = "0", list4 = "0", list5 = "0", list6 = "0", list7 = "0", list8 = "0", strlisttype = "1";

        string list1visible = "0", list2visible = "0", list3visible = "0", list4visible = "0", list5visible = "0", list6visible = "0", list7visible = "0", list8visible = "0";

        public async void Bindfeaturesbyday(string days)
        {
            await Task.Delay(500);
           // Device.BeginInvokeOnMainThread(() => { _Dialog.ShowLoading(""); });
            listgrpdata.Clear();
            Items.Clear();
            string featurebyday = "";
            XmlDocument xml = new XmlDocument();
            string xmllink = "https://techjobs.sulekha.com/mobileapp/LS_package_features.xml";
            xml.Load(xmllink);  // suppose that str string contains "<Names>...</Names>"

            XmlNodeList xnList = xml.SelectNodes("/Features/Feature[@val='" + days + "']");

            foreach (XmlNode xn in xnList)
            {
                featurebyday = xn.InnerText;
            }
            if (featurebyday != "")
            {
                string[] totalsplit = featurebyday.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);  //System.Text.RegularExpressions.Regex.Split(featurebyday, "||"); 
                List<LS_PAYMENT_FEATURES> listpay = new List<LS_PAYMENT_FEATURES>();
                foreach (var item in totalsplit)
                {
                    string[] singledata = item.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    string[] featruredesc = singledata[1].Split(',');

                    if (featruredesc.Length > 0)
                    {
                        foreach (var fdesc in featruredesc)
                        {
                            listpay.Add(new LS_PAYMENT_FEATURES { head = singledata[0], desc = fdesc, isshow = false, image = "nextarrow.png", listtype = strlisttype });
                        }
                    }

                }
                var LSg = listpay.GroupBy(m => m.head).ToList();
                LS_PAYMENT_FEATURE list;
                foreach (var item in LSg)
                {
                    list = new LS_PAYMENT_FEATURE()
                    {
                        Heading = item.Key,
                        Services = item.ToList()

                    };
                    if (list != null)
                    {
                        listgrpdata?.Add(list);
                    }

                    
                }
                if (listgrpdata != null && listgrpdata.Count > 0)
                {

                    Height1 = (listgrpdata.Count * 40) + ( 10); ;


                    foreach (var h in listgrpdata)
                    {

                        Items.Add(new GroupFeatures(h));
                    }

                }
            }
            else
            {
                Items.Clear();
            }




            // _Dialog.HideLoading();
            Device.BeginInvokeOnMainThread(() => { _Dialog.HideLoading(); });

        }

        public async void Bindfeatures(string days)
        {
            try
            {

                list1 = "0"; list2 = "0"; list3 = "0"; list4 = "0"; list5 = "0"; list6 = "0"; list7 = "0"; list8 = "0";
                string featurebyday = "";
                XmlDocument xml = new XmlDocument();
                string xmllink = "https://techjobs.sulekha.com/mobileapp/LS_package_features.xml";
                xml.Load(xmllink);  // suppose that str string contains "<Names>...</Names>"

                XmlNodeList xnList = xml.SelectNodes("/Features/Feature[@val='" + days + "']");

                foreach (XmlNode xn in xnList)
                {
                    featurebyday = xn.InnerText;
                }

                if (featurebyday != "")
                {
                    string[] totalsplit = featurebyday.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);  //System.Text.RegularExpressions.Regex.Split(featurebyday, "||"); 







                    foreach (var item in totalsplit)
                    {
                        string[] singledata = item.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                        string[] featruredesc = singledata[1].Split(',');

                        List<LS_PAYMENT_FEATURES> listpay = new List<LS_PAYMENT_FEATURES>();

                        if (featruredesc.Length > 0)
                        {

                            foreach (var fdesc in featruredesc)
                            {
                                if (list1 == "0")
                                {
                                    strlisttype = "1";
                                }
                                else if (list2 == "0")
                                {
                                    strlisttype = "2";
                                }
                                else if (list3 == "0")
                                {
                                    strlisttype = "3";
                                }
                                else if (list4 == "0")
                                {
                                    strlisttype = "4";
                                }
                                else if (list5 == "0")
                                {
                                    strlisttype = "5";
                                }
                                else if (list6 == "0")
                                {
                                    strlisttype = "6";
                                }
                                else if (list7 == "0")
                                {
                                    strlisttype = "7";
                                }
                                else if (list8 == "0")
                                {
                                    strlisttype = "8";
                                }
                                listpay.Add(new LS_PAYMENT_FEATURES { head = singledata[0], desc = fdesc, isshow = false, image = "nextarrow.png", listtype = strlisttype });
                            }
                        }



                        var sorted = from srch in listpay group srch by srch.head into searchGroup select new Grouping<string, LS_PAYMENT_FEATURES>(searchGroup.Key, searchGroup);

                        var CurrentPage = ViewModels.LS_ViewModel.GetCurrentPage();
                        if (list1 == "0")
                        {
                            var entry1 = CurrentPage.FindByName<ListView>("lslistfeatures1");

                            entry1.ItemsSource = sorted;
                            list1 = "1";
                        }
                        else if (list2 == "0")
                        {
                            var entry2 = CurrentPage.FindByName<ListView>("lslistfeatures2");
                            entry2.ItemsSource = sorted;
                            list2 = "1";
                        }
                        else if (list3 == "0")
                        {
                            var entry3 = CurrentPage.FindByName<ListView>("lslistfeatures3");
                            entry3.ItemsSource = sorted;

                            list3 = "1";
                        }
                        else if (list4 == "0")
                        {
                            var entry4 = CurrentPage.FindByName<ListView>("lslistfeatures4");
                            entry4.ItemsSource = sorted;

                            list4 = "1";
                        }
                        else if (list5 == "0")
                        {
                            var entry5 = CurrentPage.FindByName<ListView>("lslistfeatures5");
                            entry5.ItemsSource = sorted;

                            list5 = "1";
                        }
                        else if (list6 == "0")
                        {
                            var entry6 = CurrentPage.FindByName<ListView>("lslistfeatures6");
                            entry6.ItemsSource = sorted;

                            list6 = "1";
                        }
                        else if (list7 == "0")
                        {
                            var entry7 = CurrentPage.FindByName<ListView>("lslistfeatures7");
                            entry7.ItemsSource = sorted;

                            list7 = "1";
                        }
                        else if (list8 == "0")
                        {
                            var entry8 = CurrentPage.FindByName<ListView>("lslistfeatures8");
                            entry8.ItemsSource = sorted;

                            list8 = "1";
                        }

                        //list = new LS_AD_GROUP_FEATURES()
                        //{
                        //    Heading = singledata[0],
                        //    Services = grpdata.ToList()

                        //};
                        //listgrpdata?.Add(list);


                    }
                }
            }
            catch (Exception e)
            {

            }
        }

     

        public void Selectpackessential()
        {
          
            var curpage = LS_ViewModel.GetCurrentPage();
            var step1 = curpage.FindByName<StackLayout>("step1payemt");
            var step2 = curpage.FindByName<StackLayout>("step2payemt");
            var btn1 = curpage.FindByName<BoxView>("btnessentials");
            var btn2 = curpage.FindByName<BoxView>("btnpremier");
            btn1.BackgroundColor = Color.FromHex("#2e74f0"); btn2.BackgroundColor = Color.FromHex("#a5acb0");
            step1.IsVisible = true; step2.IsVisible = false;
            Selectpackagetype(pack1_days);
        }
        public void Selectpackpremier()
        {
          
            var curpage = LS_ViewModel.GetCurrentPage();
            var step1 = curpage.FindByName<StackLayout>("step1payemt");
            var step2 = curpage.FindByName<StackLayout>("step2payemt");
            var btn1 = curpage.FindByName<BoxView>("btnessentials");
            var btn2 = curpage.FindByName<BoxView>("btnpremier");
            btn1.BackgroundColor = Color.FromHex("#a5acb0"); btn2.BackgroundColor = Color.FromHex("#2e74f0");
            step1.IsVisible = false; step2.IsVisible = true;
            Selectpackagetype(pack4_days);
           
        }
        public async void Bindpackageamount()
        {
            try
            {

                Device.BeginInvokeOnMainThread(() => { _Dialog.ShowLoading(""); });
                var location = RestService.For<ILS_Prime_Form>(NRIApp.Helpers.Commonsettings.Localservices);
                string designtype = "";
                var data = await location.Getlsflexpackage(sOldclpostid);
                if (data.ROW_DATA.Count > 0 && data != null)
                {
                    pack1_name = data.ROW_DATA[0].pack1_name;
                    pack2_name = data.ROW_DATA[0].pack2_name;
                    pack3_name = data.ROW_DATA[0].pack3_name;
                    pack4_name = data.ROW_DATA[0].pack4_name;
                    pack5_name = data.ROW_DATA[0].pack5_name;
                    pack6_name = data.ROW_DATA[0].pack6_name;

                    pack1_overall_amount = data.ROW_DATA[0].pack1_overall_amount;
                    pack2_overall_amount = data.ROW_DATA[0].pack2_overall_amount;
                    pack3_overall_amount = data.ROW_DATA[0].pack3_overall_amount;
                    pack4_overall_amount = data.ROW_DATA[0].pack4_overall_amount;
                    pack5_overall_amount = data.ROW_DATA[0].pack5_overall_amount;
                    pack6_overall_amount = data.ROW_DATA[0].pack6_overall_amount;


                    pack1_days = data.ROW_DATA[0].pack1_days;
                    pack2_days = data.ROW_DATA[0].pack2_days;
                    pack3_days = data.ROW_DATA[0].pack3_days;
                    pack4_days = data.ROW_DATA[0].pack4_days;
                    pack5_days = data.ROW_DATA[0].pack5_days;
                    pack6_days = data.ROW_DATA[0].pack6_days;

                    pack1_days_txt = "("+data.ROW_DATA[0].pack1_days+" Days)";
                    pack2_days_txt = "("+data.ROW_DATA[0].pack2_days+" Days)";
                    pack3_days_txt = "("+data.ROW_DATA[0].pack3_days+" Days)";
                    pack4_days_txt = "("+data.ROW_DATA[0].pack4_days+" Days)";
                    pack5_days_txt = "("+data.ROW_DATA[0].pack5_days+" Days)";
                    pack6_days_txt = "("+data.ROW_DATA[0].pack6_days+" Days)";

                    Choosedamount = "$" + Convert.ToString(pack1_overall_amount);
                    flexpackageamount.Clear();
                    flexpackageamount.Add(pack1_days, pack1_overall_amount);
                    flexpackageamount.Add(pack2_days, pack2_overall_amount);
                    flexpackageamount.Add(pack3_days, pack3_overall_amount);
                    flexpackageamount.Add(pack4_days, pack4_overall_amount);
                    flexpackageamount.Add(pack5_days, pack5_overall_amount);
                    flexpackageamount.Add(pack6_days, pack6_overall_amount);

                    flexpackagetype.Clear();
                    flexpackagetype.Add(pack1_days, data.ROW_DATA[0].pack1_id);
                    flexpackagetype.Add(pack2_days, data.ROW_DATA[0].pack2_id);
                    flexpackagetype.Add(pack3_days, data.ROW_DATA[0].pack3_id);
                    flexpackagetype.Add(pack4_days, data.ROW_DATA[0].pack4_id);
                    flexpackagetype.Add(pack5_days, data.ROW_DATA[0].pack5_id);
                    flexpackagetype.Add(pack6_days, data.ROW_DATA[0].pack6_id);
                    daysdata.Clear();
                    daysdata.Add(pack1_days);
                    daysdata.Add(pack2_days);
                    daysdata.Add(pack3_days);
                    daysdata.Add(pack4_days);
                    daysdata.Add(pack5_days);
                    daysdata.Add(pack6_days);
                    designtype = data.ROW_DATA[0].designtype;

                    if (designtype == "0")
                    {
                        framepack1 = false;
                        framepack2 = false;
                        framepack3 = false;
                        OnPropertyChanged(nameof(framepack1));
                        OnPropertyChanged(nameof(framepack2));
                        OnPropertyChanged(nameof(framepack3));

                        var curpage = LS_ViewModel.GetCurrentPage();

                        var essenbtn = curpage.FindByName<StackLayout>("stackessential");
                        essenbtn.IsVisible = false;

                        var paymentbtn = curpage.FindByName<StackLayout>("step1payemt");
                        paymentbtn.IsVisible = false;


                        var paymentbtn2 = curpage.FindByName<StackLayout>("step2payemt");
                        paymentbtn2.IsVisible = true;

                        var btn4 = curpage.FindByName<Button>("btnpack4name");


                        Selectpackagetype(pack4_days);
                    }
                    else
                    {
                        Selectpackagetype(pack1_days);
                    }
                   
                   
                    //   await Task.Delay(2000);
                    //var curpage = LS_ViewModel.GetCurrentPage();
                    //var step1 = curpage.FindByName<StackLayout>("step1payemt");
                    //var step2 = curpage.FindByName<StackLayout>("step2payemt");
                    //var btn1 = curpage.FindByName<BoxView>("btnessentials");
                    //var btn2 = curpage.FindByName<BoxView>("btnpremier");
                    //btn1.BackgroundColor = Color.FromHex("#2e74f0"); btn2.BackgroundColor = Color.FromHex("#a5acb0");
                    //step1.IsVisible = true; step2.IsVisible = false;

                    //    pack2backgroundcolor = pack3backgroundcolor = pack4backgroundcolor = pack5backgroundcolor = pack6backgroundcolor = pack1textcolor = "#ffffff";
                    //    pack2textcolor = pack3textcolor = pack4textcolor = pack5textcolor = pack6textcolor = pack1backgroundcolor = "#2e74f0";
                    //    framepack1 = true;
                    //    framepack2 = framepack3 = framepack4 = framepack5 = framepack6 = false;

                    //listgrpdata.Clear();
                    //Items.Clear();
                    //string featurebyday = "";
                    //XmlDocument xml = new XmlDocument();
                    //string xmllink = "https://techjobs.sulekha.com/mobileapp/LS_package_features.xml";
                    //xml.Load(xmllink);  // suppose that str string contains "<Names>...</Names>"

                    //XmlNodeList xnList = xml.SelectNodes("/Features/Feature[@val='" + pack1_days + "']");

                    //foreach (XmlNode xn in xnList)
                    //{
                    //    featurebyday = xn.InnerText;
                    //}
                    //if (featurebyday != "")
                    //{
                    //    string[] totalsplit = featurebyday.Split(new[] { "||" }, StringSplitOptions.RemoveEmptyEntries);  //System.Text.RegularExpressions.Regex.Split(featurebyday, "||"); 






                    //    List<LS_PAYMENT_FEATURES> listpay = new List<LS_PAYMENT_FEATURES>();
                    //    foreach (var item in totalsplit)
                    //    {
                    //        string[] singledata = item.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    //        string[] featruredesc = singledata[1].Split(',');



                    //        if (featruredesc.Length > 0)
                    //        {

                    //            foreach (var fdesc in featruredesc)
                    //            {

                    //                listpay.Add(new LS_PAYMENT_FEATURES { head = singledata[0], desc = fdesc, isshow = false, image = "nextarrow.png", listtype = strlisttype });
                    //            }
                    //        }

                    //    }
                    //    var LSg = listpay.GroupBy(m => m.head).ToList();
                    //    LS_PAYMENT_FEATURE list;
                    //    foreach (var item in LSg)
                    //    {
                    //        list = new LS_PAYMENT_FEATURE()
                    //        {
                    //            Heading = item.Key,
                    //            Services = item.ToList()

                    //        };
                    //        listgrpdata?.Add(list);




                    //    }
                    //    if (listgrpdata != null && listgrpdata.Count > 0)
                    //    {

                    //        Height1 = (listgrpdata.Count * 40) + (10); ;


                    //        foreach (var h in listgrpdata)
                    //        {

                    //            Items.Add(new GroupFeatures(h));
                    //        }

                    //    }
                    //}
                    //else
                    //{
                    //    Items.Clear();
                    //}
                    //  Device.BeginInvokeOnMainThread(() => { _Dialog.HideLoading(); });
                    // Selectpackessential();
                }

            }
            catch (Exception e)
            {

            }
        }

        public async void Selectpackage(int days)
        {
            try
            {
                _Dialog.ShowLoading("");
                var spackage = flexpackageamount[days];
                var spackagetype = flexpackagetype[days];
                var curpage = LS_ViewModel.GetCurrentPage();
                LS_FLEX_SELECT_PACKAGE fsp = new LS_FLEX_SELECT_PACKAGE()
                {
                    package = spackage,
                    noofdays = Convert.ToString(days),
                    packagetype = Convert.ToString(spackagetype),
                    oldclpostid = sOldclpostid,
                    hdnactualamount = Convert.ToString(spackage)
                };

                var choosepackagedata = RestService.For<ILS_Prime_Form>(Commonsettings.TechjobsAPI);

                var result = await choosepackagedata.Selectedflexpackage(fsp);
                if (result != null)
                {
                    await curpage.Navigation.PushAsync(new NRIApp.LocalService.Features.Views.Posting.LS_Flex_Order_Summary(result.ROW_DATA[0], fsp));
                }


                _Dialog.HideLoading();
            }
            catch (Exception e)
            {
                _Dialog.HideLoading();
            }
        }
        public void Selectpackagetype(int days)
        {
            Device.BeginInvokeOnMainThread(() => { _Dialog.ShowLoading(""); });
            
            var curpage = LS_ViewModel.GetCurrentPage();
            if (days == pack1_days)
            {
                pack2backgroundcolor = pack3backgroundcolor = pack4backgroundcolor = pack5backgroundcolor = pack6backgroundcolor = pack1textcolor = "#ffffff";
                pack2textcolor = pack3textcolor = pack4textcolor = pack5textcolor = pack6textcolor = pack1backgroundcolor = "#2e74f0";
                framepack1 = true;
                framepack2 = framepack3 = framepack4 = framepack5 = framepack6 = false;

            }
            else if (days == pack2_days)
            {
                pack1backgroundcolor = pack3backgroundcolor = pack4backgroundcolor = pack5backgroundcolor = pack6backgroundcolor = pack6backgroundcolor = pack2textcolor = "#ffffff";
                pack1textcolor = pack3textcolor = pack4textcolor = pack5textcolor = pack6textcolor = pack2backgroundcolor = "#2e74f0";
                framepack2 = true;
                framepack1 = framepack3 = framepack4 = framepack5 = framepack6 = false;
            }
            else if (days == pack3_days)
            {
                pack1backgroundcolor = pack2backgroundcolor = pack4backgroundcolor = pack5backgroundcolor = pack6backgroundcolor = pack3textcolor = "#ffffff";
                pack1textcolor = pack2textcolor = pack4textcolor = pack5textcolor = pack6textcolor = pack3backgroundcolor = "#2e74f0";
                framepack3 = true;
                framepack1 = framepack2 = framepack4 = framepack5 = framepack6 = false;
            }
            else if (days == pack4_days)
            {
                pack1backgroundcolor = pack2backgroundcolor = pack3backgroundcolor = pack5backgroundcolor = pack6backgroundcolor = pack4textcolor = "#ffffff";
                pack1textcolor = pack2textcolor = pack3textcolor = pack5textcolor = pack6textcolor = pack4backgroundcolor = "#2e74f0";
                framepack4 = true;
                framepack1 = framepack2 = framepack3 = framepack5 = framepack6 = false;
            }
            else if (days == pack5_days)
            {
                pack1backgroundcolor = pack2backgroundcolor = pack3backgroundcolor = pack4backgroundcolor = pack6backgroundcolor = pack5textcolor = "#ffffff";
                pack1textcolor = pack2textcolor = pack3textcolor = pack4textcolor = pack6textcolor = pack5backgroundcolor = "#2e74f0";
                framepack5 = true;
                framepack1 = framepack2 = framepack3 = framepack4 = framepack6 = false;
            }
            else if (days == pack6_days)
            {
                pack1backgroundcolor = pack2backgroundcolor = pack3backgroundcolor = pack4backgroundcolor = pack5backgroundcolor = pack6textcolor = "#ffffff";
                pack1textcolor = pack2textcolor = pack3textcolor = pack4textcolor = pack5textcolor = pack6backgroundcolor = "#2e74f0";
                framepack6 = true;
                framepack1 = framepack2 = framepack3 = framepack4 = framepack5 = false;
            }

            Choosedamount = "$" + flexpackageamount[days].ToString();
            Bindfeaturesbyday(Convert.ToString(days));

           
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
