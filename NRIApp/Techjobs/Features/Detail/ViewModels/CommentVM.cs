using Acr.UserDialogs;
using NRIApp.Helpers;
using NRIApp.Techjobs.Features.Detail.Interfaces;
using NRIApp.Techjobs.Features.Detail.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NRIApp.Techjobs.Features.Detail.ViewModels
{
   public class CommentVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;
        public ICommand AddCommentcommand { get; set; }
        public string revid = "", bizid = "";
        public CommentVM(string reviewid, string businessid)
        {
            LoginName = Commonsettings.UserName;
            Loginicon = LoginName.Substring(0, 1).ToUpper();
            revid = reviewid;
            bizid = businessid;
            Getreviewbyid(reviewid);
            Getreplys(reviewid,businessid);
            AddCommentcommand = new Command(AddComment);
        }
        public List<Replies> _replies { get; set; }
        public List<Replies> replies
        {
            get
            {
                return _replies;
            }
            set
            {
                _replies = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(replies)));
            }
        }
        public string _name = "";
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));

            }
        }
        public string _rating = "";
        public string Rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Rating)));

            }
        }
        public string _userreview = "";
        public string Userreview
        {
            get { return _userreview; }
            set
            {
                _userreview = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Userreview)));

            }
        }
        public string _profilepic = "";
        public string Profilepic
        {
            get { return _profilepic; }
            set
            {
                _profilepic = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Profilepic)));

            }
        }
        public string _rplycomment = "";
        public string Rplycomment
        {
            get { return _rplycomment; }
            set
            {
                _rplycomment = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Rplycomment)));

            }
        }
        public string _noprofilepic = "";
        public string NoProfilepic
        {
            get { return _noprofilepic; }
            set
            {
                _noprofilepic = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NoProfilepic)));

            }
        }
        private bool _rplyblk = false;
        public bool Rplyblk
        {
            get { return _rplyblk; }
            set
            {
                _rplyblk = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Rplyblk)));
            }
        }
        private bool _picisshow = false;
        public bool PicIsShow
        {
            get { return _picisshow; }
            set
            {
                _picisshow = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PicIsShow)));
            }
        }
        private bool _nopic = false;
        public bool NoPic
        {
            get { return _nopic; }
            set
            {
                _nopic = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NoPic)));
            }
        }

        private bool _sucessblk = false;
        public bool Sucssblk
        {
            get { return _sucessblk; }
            set
            {
                _sucessblk = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Sucssblk)));
            }
        }

        public string _loginname = "";
        public string LoginName
        {
            get { return _loginname; }
            set
            {
                _loginname = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoginName)));

            }
        }
        public string _loginicon = "";
        public string Loginicon
        {
            get { return _loginicon; }
            set
            {
                _loginicon = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Loginicon)));

            }
        }
        public async void Getreviewbyid(string reviewid)
        {
            dialog.ShowLoading(null);
            var nsAPI = RestService.For<IDetailservice>(Commonsettings.TechjobsAPI);
            Getreviews list = await nsAPI.Getreviewsbyid(reviewid);
            Name = list.ROW_DATA.Last().name;
            Rating = list.ROW_DATA.Last().rating;
            Userreview = list.ROW_DATA.Last().userreview;
            Profilepic = list.ROW_DATA.Last().profilepicurl;
            NoProfilepic = list.ROW_DATA.Last().name.Substring(0, 1).ToUpper();
            if (Profilepic != "" && Profilepic != null)
                PicIsShow = true;
            else
                NoPic = true;
            dialog.HideLoading();
        }

        public async void Getreplys(string reviewid, string businessid)
        {
            dialog.ShowLoading(null);
            var nsAPI = RestService.For<IDetailservice>(Commonsettings.TechjobsAPI);
            GetReplies list = await nsAPI.Getreplys(reviewid,businessid);
            
            if (list.ROW_DATA != null)
            {
                replies = list.ROW_DATA;
                Rplyblk = true;
            }
              
            dialog.HideLoading();
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
        public async void AddComment()
        {
                if (Rplycomment != "" && Rplycomment != null)
                {
                dialog.ShowLoading(null);
                    var nsAPI = RestService.For<IDetailservice>(Commonsettings.TechjobsAPI);
                    Comment cm = new Comment()
                    {
                        name = Commonsettings.UserName,
                        userpid = Commonsettings.UserPid,
                        businessid = bizid,
                        reviewid = revid,
                        desc = Rplycomment
                    };
                    Resultinfo res = await nsAPI.Addcomment(cm);
                    Sucssblk = true;
                Rplycomment = "";
                dialog.HideLoading();
                }
                else
                    dialog.Toast("Please add comment");
           
        }
    }
}
