using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using Acr.UserDialogs;
using Refit;
using NRIApp.LocalJobs.Features.Detail.Models;
using NRIApp.Helpers;
using System.Threading.Tasks;
using NRIApp.DayCare.Features.Detail.Interface;

namespace NRIApp.DayCare.Features.Detail.ViewModels
{
    public class ReplytoReview_VM : INotifyPropertyChanged
    {
        IUserDialogs dialogs = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        public Command Replysubmit { get; set; }
        public List<Reviewreplydetaildata> Reviewreply { get; set; }
        private string _businessid;
        public string businessid
        {
            get { return _businessid; }
            set { _businessid = value; OnPropertyChanged(nameof(businessid)); }
        }
        private string _replycomment = "";
        public string replycomment
        {
            get { return _replycomment; }
            set { _replycomment = value; OnPropertyChanged(nameof(replycomment)); }
        }
        private string _Revimage = "";
        public string Revimage
        {
            get { return _Revimage; }
            set { _Revimage = value; }
        }
        private bool _Revimgvisible = false;
        public bool Revimgvisible
        {
            get { return _Revimgvisible; }
            set { _Revimgvisible = value; }
        }

        private bool _Revimgnovisible = false;
        public bool Revimgnovisible
        {
            get { return _Revimgnovisible; }
            set { _Revimgnovisible = value; }
        }
        private string _Revcontributor = "";
        public string Revcontributor
        {
            get { return _Revcontributor; }
            set { _Revcontributor = value; }
        }
        private string _Revpostedago = "";
        public string Revpostedago
        {
            get { return _Revpostedago; }
            set { _Revpostedago = value; }
        }
        private string _Revrating = "";
        public string Revrating
        {
            get { return _Revrating; }
            set { _Revrating = value; }
        }
        private string _Revdesc = "";
        public string Revdesc
        {
            get { return _Revdesc; }
            set { _Revdesc = value; }
        }
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
        private string _Revtext = "";
        public string Revtext
        {
            get { return _Revtext; }
            set { _Revtext = value; }
        }
        public ReplytoReview_VM(Rating_Details reviewdtl)
        {
            businessid = reviewdtl.objectid.ToString();
            Binddetailreviews(reviewdtl);
            Bindreviewreply(reviewdtl.reviewid.ToString(), businessid);
            Replysubmit = new Command(async () => await replysubmit(reviewdtl.reviewid.ToString(), businessid));
        }
        public async void Bindreviewreply(string reviewid, string businesid)
        {
            try
            {
                //reviewid = "124"; sulekha us llc
                dialogs.ShowLoading("", null);
                var detailpagedata = RestService.For<ICareDetail>(Commonsettings.DaycareAPI);
                var data = await detailpagedata.GetAdReviewReplydtl(reviewid, businesid);
                if (data.ROW_DATA.Count > 0)
                {
                    foreach (var item in data.ROW_DATA)
                    {
                        if (!string.IsNullOrEmpty(item.contributor))
                        {
                            item.replytext = item.contributor.Substring(0, 1).ToUpper();
                        }
                    }
                    OnPropertyChanged(nameof(Reviewreply));
                    Reviewreply = data.ROW_DATA;
                    var CurrentPage = GetCurrentpage();
                    var listreviewreply = CurrentPage.FindByName<HVScrollGridView>("listreviewreply");
                    listreviewreply.ItemsSource = null;
                    listreviewreply.ItemsSource = Reviewreply;
                }

                dialogs.HideLoading();
            }
            catch (Exception e)
            {
                dialogs.HideLoading();
            }
        }

        public async Task replysubmit(string reviewid, string businessd)
        {
            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
            {
                var currentpage = GetCurrentpage();
                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
            }
            else
            {
                if (string.IsNullOrEmpty(replycomment) || replycomment.Trim().Length==0)
                {
                    dialogs.Toast("Please enter your description...");
                }
                else
                {
                    var replyapi = RestService.For<ICareDetail>(Commonsettings.DaycareAPI);
                    var replyapidata = await replyapi.replytoreview(reviewid, businessid, Commonsettings.UserPid, replycomment, Commonsettings.UserName);
                    //returns 1 or 0
                    if (replyapidata != null)
                    {
                        var currentpage = GetCurrentpage();
                        replycomment = "";
                        Bindreviewreply(reviewid, businessd);
                    }
                }

            }
        }
        public void Binddetailreviews(Rating_Details reviewdtl)
        {
            try
            {
                //dialogs.ShowLoading("");
                //var detailreviewdata = RestService.For<ICareDetail>(Commonsettings.LocaljobsAPI);
                //var data = await detailreviewdata.GetheaderReview(reviewid, businesid);
                if (!string.IsNullOrEmpty(reviewdtl.shortdesc))
                {
                if (!string.IsNullOrEmpty(reviewdtl.photourl))
                {
                    Revimage = reviewdtl.photourl.ToString();
                    Revimgvisible = true;
                    Revimgnovisible = false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(reviewdtl.contributor))
                    {
                        Revtext = reviewdtl.contributor.Substring(0, 1).ToUpper();
                        Revimgvisible = false;
                        Revimgnovisible = true;
                    }
                }
                Revcontributor = reviewdtl.contributor.ToString();
                Revpostedago = reviewdtl.postedago.ToString();
                Revrating = reviewdtl.rating.ToString();
                Revdesc = reviewdtl.description.ToString();
                Loginname = Commonsettings.UserName;
                Revlogintext = Commonsettings.UserName.Substring(0, 1).ToUpper();
                OnPropertyChanged(nameof(Revimage)); OnPropertyChanged(nameof(Revimgvisible));
                OnPropertyChanged(nameof(Revimgnovisible)); OnPropertyChanged(nameof(Revtext));
                OnPropertyChanged(nameof(Revcontributor)); OnPropertyChanged(nameof(Revpostedago));
                OnPropertyChanged(nameof(Revrating)); OnPropertyChanged(nameof(Revdesc));
                OnPropertyChanged(nameof(Loginname)); OnPropertyChanged(nameof(Revlogintext));
                }
            }
          
            catch (Exception e)
            {

            }
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
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
