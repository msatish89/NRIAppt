using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.MyNeeds.Features.Models;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using Refit;
using Xamarin.Forms;

namespace NRIApp.MyNeeds.Features.ViewModel
{
    public class Replyresponse_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialogs = UserDialogs.Instance;
        public Command Responsereplysubmit { get; set; }
        public List<MyNeedData> Reviewreply { get; set; }
        //private string _adid;
        //public string adid
        //{
        //    get { return _adid; }
        //    set { _adid = value; OnPropertyChanged(nameof(adid)); }
        //}
        //private string _replycomment = "";
        //public string replycomment
        //{
        //    get { return _replycomment; }
        //    set { _replycomment = value; OnPropertyChanged(nameof(replycomment)); }
        //}
        //private string _Revimage = "";
        //public string Revimage
        //{
        //    get { return _Revimage; }
        //    set { _Revimage = value; }
        //}
        //private bool _Revimgvisible = false;
        //public bool Revimgvisible
        //{
        //    get { return _Revimgvisible; }
        //    set { _Revimgvisible = value; }
        //}

        //private bool _Revimgnovisible = false;
        //public bool Revimgnovisible
        //{
        //    get { return _Revimgnovisible; }
        //    set { _Revimgnovisible = value; }
        //}
        //private string _Revcontributor = "";
        //public string Revcontributor
        //{
        //    get { return _Revcontributor; }
        //    set { _Revcontributor = value; }
        //}
        //private string _Revpostedago = "";
        //public string Revpostedago
        //{
        //    get { return _Revpostedago; }
        //    set { _Revpostedago = value; }
        //}
        //private string _Revrating = "";
        //public string Revrating
        //{
        //    get { return _Revrating; }
        //    set { _Revrating = value; }
        //}
        //private string _Revdesc = "";
        //public string Revdesc
        //{
        //    get { return _Revdesc; }
        //    set { _Revdesc = value; }
        //}
        private string _Loginname = "";
        public string Loginname
        {
            get { return _Loginname; }
            set { _Loginname = value; }
        }
        private string _Revlogintext = "";
        public string Revlogintext
        {
            get { return _Revlogintext; }
            set { _Revlogintext = value; }
        }
        //private string _Revtext = "";
        //public string Revtext
        //{
        //    get { return _Revtext; }
        //    set { _Revtext = value; }
        //}
        private string _shortdesc = "";
        public string shortdesc
        {
            get { return _shortdesc; }
            set { _shortdesc = value; }
        }
        private string _contactname = "";
        public string contactname
        {
            get { return _contactname; }
            set { _contactname = value; }
        }
        private string _contactnameimg = "";
        public string contactnameimg
        {
            get { return _contactnameimg; }
            set { _contactnameimg = value; }
        }
        private string _postedago = "";
        public string postedago
        {
            get { return _postedago; }
            set { _postedago = value; }
        }
        private string _Replydesc = "";
        public string Replydesc
        {
            get { return _Replydesc; }
            set { _Replydesc = value; }
        }
        private string _responselogintext = "";
        public string responselogintext
        {
            get { return _responselogintext; }
            set { _responselogintext = value; }
        }

        private string _responseshortdesc = "";
        public string responseshortdesc
        {
            get { return _responseshortdesc; }
            set { _responseshortdesc = value; }
        }
        private string _respondersname = "";
        public string respondersname
        {
            get { return _respondersname; }
            set { _respondersname = value; }
        }
        private bool _responseListviewlayout = false;
        public bool responseListviewlayout
        {
            get { return _responseListviewlayout; }
            set { _responseListviewlayout = value; OnPropertyChanged(nameof(responseListviewlayout)); }
        }
        
        private string _Mailercontent = "";
        public string Mailercontent
        {
            get { return _Mailercontent; }
            set { _Mailercontent = value; OnPropertyChanged(nameof(Mailercontent)); }
        }
        Ad_Response_Data data = new Ad_Response_Data();
        public Replyresponse_VM(Ad_Response_Data needdta)
        {
            //adid = needdta.Adid;
            data = needdta;
            //Bindresponsereply(needdta);
            Bindresponsedetails(needdta);
            Responsereplysubmit = new Command(async () => await replysubmit());
        }
        public async void Bindresponsedetails(Ad_Response_Data needdtas)
        {
            try
            {
                dialogs.ShowLoading("", null);
                respondersname = needdtas.Contactname;
                responselogintext=needdtas.Contactname.Substring(0, 1).ToUpper();
                postedago = needdtas.Postedago; 
                responseshortdesc = needdtas.Shortdesc;
                var Adresponse = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                var AdresponseData = await Adresponse.getadresponsereplydetails(data.Adid, needdtas.Respid ,Commonsettings.UserPid,needdtas.Email,needdtas.Services);
                //shortdesc = needdtas.Shortdesc;
                //contactname = needdtas.Contactname;
                //contactnameimg = needdtas.Contactname.Substring(0, 1).ToUpper();
                //postedago = needdtas.Postedago;
                //Revlogintext = NRIApp.Helpers.Commonsettings.UserName.Substring(0, 1).ToUpper();
                //Loginname = NRIApp.Helpers.Commonsettings.UserName;
                if (AdresponseData != null && AdresponseData.RowData.Count!=0)
                {
                    responseListviewlayout = true;
                    
                    foreach (var item in AdresponseData.RowData)
                    {
                        //if (!string.IsNullOrEmpty(item.Contactname))
                        //{
                        //    item.replytext = item.Contactname.Substring(0, 1).ToUpper();
                        //}
                        if (!string.IsNullOrEmpty(item.Adphonenumber))
                        {
                            item.Mobileno = item.Adphonenumber + " (Phone Verified)";
                        }
                        else
                        {
                            item.Mobileno = "Not Available";
                        }
                        item.Contactname =Commonsettings.UserName;
                        //item.contactnameimg = needdtas.Contactname.Substring(0, 1).ToUpper();
                        item.contactnameimg = NRIApp.Helpers.Commonsettings.UserName.Substring(0, 1).ToUpper();
                        //item.Loginname = NRIApp.Helpers.Commonsettings.UserName;
                    }
                }
                var CurrentPage = GetCurrentpage();
                var listreviewreply = CurrentPage.FindByName<ListView>("responseListview");
                listreviewreply.ItemsSource = null;
                listreviewreply.ItemsSource = AdresponseData.RowData;
                //Reviewreply
                dialogs.HideLoading();
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        Ad_Response_Data replydata = new Ad_Response_Data();
        string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
        public async Task replysubmit()
        {
            try
            {
                if(string.IsNullOrEmpty(Mailercontent) || Mailercontent.Trim().Length==0)
                {
                    dialogs.Toast("Description can't be empty");
                }
                else
                {
                    dialogs.ShowLoading("", null);
                    replydata.Adid = data.Adid;
                    replydata.Respid = data.Respid;
                    replydata.businessid = data.businessid;
                    replydata.Services = data.Services;
                    replydata.Userpid =Commonsettings.UserPid;
                    replydata.Shortdesc = Mailercontent;
                    replydata.Contactname = data.Contactname;
                    replydata.Email = data.Email;
                    if(!string.IsNullOrEmpty(data.Adphonenumber))
                    {
                        replydata.Mobileno = data.Adphonenumber;
                    }
                    else
                    {
                        replydata.Mobileno = data.Mobileno;
                    }
                    
                    replydata.Ip = ipaddress;
                    var Adreply = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                    var AdresponseData = await Adreply.adreply(replydata);
                    if (AdresponseData != null)
                    {
                        if (AdresponseData.resultinformation == "success")
                        {
                            Mailercontent = "";
                            Bindresponsedetails(data);
                            dialogs.Toast("Responded Successfully!");
                        }
                        else
                        {
                            dialogs.Toast("Could not be submitted please try again later");
                        }
                    }
                    dialogs.HideLoading();
                }
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page GetCurrentpage()
        {
            var Currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return Currentpage;
        }
    }
}
