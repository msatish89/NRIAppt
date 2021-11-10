using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using Xamarin.Forms;
using Acr.UserDialogs;
using NRIApp.Helpers;
using Refit;
using NRIApp.DayCare.Features.Post.Interface;
using NRIApp.DayCare.Features.Post.Models;
using System.Threading.Tasks;

namespace NRIApp.DayCare.Features.Post.ViewModels
{
    public class Package_VM :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialogs = UserDialogs.Instance;
        Daycareposting post = new Daycareposting();
        public Command TapOnFreePackage { get; set; }
       // public Command TapOnSponsoredPackage { get; set; }
        public Command TapOnbasicPackage { get; set; }
        public Command TapOnStandardPackage { get; set; }
        public Command TapOnPremiumPackage { get; set; }
        public Command TapOnElitePackage { get; set; }
        public Command TapOnStarterPackage { get; set; }
        public Command TapOnPremiumPlusPackage { get; set; }

        public bool _premiumpkg = false;
        public bool premiumpkgvisible
        {
            get { return _premiumpkg; }
            set
            {
                _premiumpkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpkgvisible)));
            }
        }
        public string _premiumpkgamnt = "$52";
        public string premiumpkgamnt
        {
            get { return _premiumpkgamnt; }
            set
            {
                _premiumpkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpkgamnt)));
            }
        }
        public string _premiumpkgdays = "60";
        public string premiumpkgdays
        {
            get { return _premiumpkgdays; }
            set
            {
                _premiumpkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpkgdays)));
            }
        }
        public bool _premiumbtn = true;
        public bool premiumbtn
        {
            get { return _premiumbtn; }
            set
            {
                _premiumbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumbtn)));
            }
        }
        public bool _selpremiumbtn = false;
        public bool Selpremiumbtn
        {
            get { return _selpremiumbtn; }
            set
            {
                _selpremiumbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selpremiumbtn)));
            }
        }
        public bool _standardpkg = false;
        public bool standardpkgvisible
        {
            get { return _standardpkg; }
            set
            {
                _standardpkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgvisible)));
            }
        }
        public string _standardpkgamnt = "$35";
        public string standardpkgamnt
        {
            get { return _standardpkgamnt; }
            set
            {
                _standardpkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgamnt)));
            }
        }
        public string _standardpkgdays = "35";
        public string standardpkgdays
        {
            get { return _standardpkgdays; }
            set
            {
                _standardpkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgdays)));
            }
        }
        public bool _standardpkgbtn = true;
        public bool standardpkgbtn
        {
            get { return _standardpkgbtn; }
            set
            {
                _standardpkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgbtn)));
            }
        }
        public bool _selstandardpkgbtn = false;
        public bool Selstandardpkgbtn
        {
            get { return _selstandardpkgbtn; }
            set
            {
                _selstandardpkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selstandardpkgbtn)));
            }
        }
        public bool _basicpkg = false;
        public bool basicpkgvisible
        {
            get { return _basicpkg; }
            set
            {
                _basicpkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicpkgvisible)));
            }
        }
        public string _basicpkgamnt = "$25";
        public string basicpkgamnt
        {
            get { return _basicpkgamnt; }
            set
            {
                _basicpkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicpkgamnt)));
            }
        }
        public string _basicpkgdays = "15";
        public string basicpkgdays
        {
            get { return _basicpkgdays; }
            set
            {
                _basicpkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicpkgdays)));

            }
        }
        public bool _basicbtn = true;
        public bool basicbtn
        {
            get { return _basicbtn; }
            set
            {
                _basicbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicbtn)));
            }
        }
        public bool _selbasicbtn = false;
        public bool Selbasicbtn
        {
            get { return _selbasicbtn; }
            set
            {
                _selbasicbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selbasicbtn)));
            }
        }
        public bool _freepkgvisible = false;
        public bool freepkgvisible
        {
            get { return _freepkgvisible; }
            set
            {
                _freepkgvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(freepkgvisible)));
            }
        }
        public string _freepkgamnt = "$0";
        public string freepkgamnt
        {
            get { return _freepkgamnt; }
            set
            {
                _freepkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(freepkgamnt)));
            }
        }
        public string _freepkgdays = "2";
        public string freepkgdays
        {
            get { return _freepkgdays; }
            set
            {
                _freepkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(freepkgdays)));

            }
        }
        public bool _freebtn = true;
        public bool freebtn
        {
            get { return _freebtn; }
            set
            {
                _freebtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(freebtn)));
            }
        }
        public bool _Selfreebtn = false;
        public bool Selfreebtn
        {
            get { return _Selfreebtn; }
            set
            {
                _Selfreebtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selfreebtn)));
            }
        }
        public List<DC_Package_DATA> _pack { get; set; }
        public List<DC_Package_DATA> Pack
        {
            get{ return _pack; }
            set{ _pack = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pack))); }
        }
        //addonamount
        public string _premiumpkgaddonamnt = "";
        public string premiumpkgaddonamnt
        {
            get { return _premiumpkgaddonamnt; }
            set
            {
                _premiumpkgaddonamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpkgaddonamnt)));
            }
        }
        public string _standardpkgaddonamnt = "";
        public string standardpkgaddonamnt
        {
            get { return _standardpkgaddonamnt; }
            set
            {
                _standardpkgaddonamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgaddonamnt)));
            }
        }
        public string _basicpkgaddonamnt = "";
        public string basicpkgaddonamnt
        {
            get { return _basicpkgaddonamnt; }
            set
            {
                _basicpkgaddonamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(basicpkgaddonamnt)));
            }
        }
        public string _freepkgaddonamnt = "";
        public string freepkgaddonamnt
        {
            get { return _freepkgaddonamnt; }
            set
            {
                _freepkgaddonamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(freepkgaddonamnt)));
            }
        }
        //elite and premiumplus
        public string _elitepkgamnt = "";
        public string elitepkgamnt
        {
            get { return _elitepkgamnt; }
            set
            {
                _elitepkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(elitepkgamnt)));
            }
        }

        private string _elitepkgid = "";
        public string elitepkgid
        {
            get { return _elitepkgid; }
            set { _elitepkgid = value; OnPropertyChanged(nameof(elitepkgid)); }
        }

        public string _elitepkgdays = "2";
        public string elitepkgdays
        {
            get { return _elitepkgdays; }
            set
            {
                _elitepkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(elitepkgdays)));

            }
        }
        private string _eliteordrgrp = "";
        public string eliteordrgrp
        {
            get { return _eliteordrgrp; }
            set { _eliteordrgrp = value; OnPropertyChanged(nameof(eliteordrgrp)); }
        }

        public string _elitepkgaddonamnt = "";
        public string elitepkgaddonamnt
        {
            get { return _elitepkgaddonamnt; }
            set
            {
                _elitepkgaddonamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(elitepkgaddonamnt)));
            }
        }
        //premiumplus
        public string _premiumpluspkgamnt = "$0";
        public string premiumpluspkgamnt
        {
            get { return _premiumpluspkgamnt; }
            set
            {
                _premiumpluspkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpluspkgamnt)));
            }
        }

        private string _premiumpluspkgid = "";
        public string premiumpluspkgid
        {
            get { return _premiumpluspkgid; }
            set { _premiumpluspkgid = value; OnPropertyChanged(nameof(premiumpluspkgid)); }
        }

        public string _premiumpluspkgdays = "2";
        public string premiumpluspkgdays
        {
            get { return _premiumpluspkgdays; }
            set
            {
                _premiumpluspkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpluspkgdays)));

            }
        }
        private string _premiumpluspordrgrp = "";
        public string premiumpluspordrgrp
        {
            get { return _premiumpluspordrgrp; }
            set { _premiumpluspordrgrp = value; OnPropertyChanged(nameof(premiumpluspordrgrp)); }
        }

        public string _premiumpluspkgaddonamnt = "";
        public string premiumpluspkgaddonamnt
        {
            get { return _premiumpluspkgaddonamnt; }
            set
            {
                _premiumpluspkgaddonamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpluspkgaddonamnt)));
            }
        }

        public bool _elitepkgbtn = true;
        public bool elitepkgbtn
        {
            get { return _elitepkgbtn; }
            set
            {
                _elitepkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(elitepkgbtn)));
            }
        }
        public bool _selelitepkgbtn = false;
        public bool selelitepkgbtn
        {
            get { return _selelitepkgbtn; }
            set
            {
                _selelitepkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selelitepkgbtn)));
            }
        }

        public bool _starterpkgbtn = true;
        public bool starterpkgbtn
        {
            get { return _starterpkgbtn; }
            set
            {
                _starterpkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starterpkgbtn)));
            }
        }
        public bool _selstarterpkgbtn = false;
        public bool selstarterpkgbtn
        {
            get { return _selstarterpkgbtn; }
            set
            {
                _selstarterpkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selstarterpkgbtn)));
            }
        }
        public bool _premiumpluspkgbtn = true;
        public bool premiumpluspkgbtn
        {
            get { return _premiumpluspkgbtn; }
            set
            {
                _premiumpluspkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpluspkgbtn)));
            }
        }
        public bool _selpremiumpluspkgbtn = false;
        public bool selpremiumpluspkgbtn
        {
            get { return _selpremiumpluspkgbtn; }
            set
            {
                _selpremiumpluspkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(selpremiumpluspkgbtn)));
            }
        }
        //starter
        public string _starterpkgamnt = "";
        public string starterpkgamnt
        {
            get { return _starterpkgamnt; }
            set
            {
                _starterpkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starterpkgamnt)));
            }
        }

        private string _starterpkgid = "";
        public string starterpkgid
        {
            get { return _starterpkgid; }
            set { _starterpkgid = value; OnPropertyChanged(nameof(starterpkgid)); }
        }

        public string _starterpkgdays = "10";
        public string starterpkgdays
        {
            get { return _starterpkgdays; }
            set
            {
                _starterpkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starterpkgdays)));

            }
        }
        private string _starterordrgrp = "";
        public string starterordrgrp
        {
            get { return _starterordrgrp; }
            set { _starterordrgrp = value; OnPropertyChanged(nameof(starterordrgrp)); }
        }

        public string _starterpkgaddonamnt = "";
        public string starterpkgaddonamnt
        {
            get { return _starterpkgaddonamnt; }
            set
            {
                _starterpkgaddonamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starterpkgaddonamnt)));
            }
        }
        public bool _starterpkgvisible = false;
        public bool starterpkgvisible
        {
            get { return _starterpkgvisible; }
            set
            {
                _starterpkgvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(starterpkgvisible)));
            }
        }

        //private string _premiumAdpublishtxt = "";
        //public string premiumAdpublishtxt
        //{
        //    get { return _premiumAdpublishtxt; }
        //    set { _premiumAdpublishtxt = value; OnPropertyChanged(nameof(premiumAdpublishtxt)); }
        //}
        //private string _premiumvisibilityresponsetxt = "";
        //public string premiumvisibilityresponsetxt
        //{
        //    get { return _premiumvisibilityresponsetxt; }
        //    set { _premiumvisibilityresponsetxt = value; OnPropertyChanged(nameof(premiumvisibilityresponsetxt)); }
        //}
        //private string _premiumRecommendationmailertxt = "";
        //public string premiumRecommendationmailertxt
        //{
        //    get { return _premiumRecommendationmailertxt; }
        //    set { _premiumRecommendationmailertxt = value; OnPropertyChanged(nameof(premiumRecommendationmailertxt)); }
        //}
        //private string _promotiontxt = "";
        //public string promotiontxt
        //{
        //    get { return _promotiontxt; }
        //    set { _promotiontxt = value; OnPropertyChanged(nameof(promotiontxt)); }
        //}


        //private string _standardAdpublishtxt = "";
        //public string standardAdpublishtxt
        //{
        //    get { return _standardAdpublishtxt; }
        //    set { _standardAdpublishtxt = value; OnPropertyChanged(nameof(standardAdpublishtxt)); }
        //}
        //private string _standardRecommendationmailertxt = "";
        //public string standardRecommendationmailertxt
        //{
        //    get { return _standardRecommendationmailertxt; }
        //    set { _standardRecommendationmailertxt = value; OnPropertyChanged(nameof(standardRecommendationmailertxt)); }
        //}
        //private string _standardvisibilityresponsetxt = "";
        //public string standardvisibilityresponsetxt
        //{
        //    get { return _standardvisibilityresponsetxt; }
        //    set { _standardvisibilityresponsetxt = value; OnPropertyChanged(nameof(standardvisibilityresponsetxt)); }
        //}
        //private string _standardpromotiontxt = "";
        //public string standardpromotiontxt
        //{
        //    get { return _standardpromotiontxt; }
        //    set { _standardpromotiontxt = value; OnPropertyChanged(nameof(standardpromotiontxt)); }
        //}



        //private string _basicAdpublishtxt = "";
        //public string basicAdpublishtxt
        //{
        //    get { return _basicAdpublishtxt; }
        //    set { _basicAdpublishtxt = value; OnPropertyChanged(nameof(basicAdpublishtxt)); }
        //}
        //private string _basicvisibilityresponsetxt = "";
        //public string basicvisibilityresponsetxt
        //{
        //    get { return _basicvisibilityresponsetxt; }
        //    set { _basicvisibilityresponsetxt = value; OnPropertyChanged(nameof(basicvisibilityresponsetxt)); }
        //}
        //private string _basicRecommendationmailertxt = "";
        //public string basicRecommendationmailertxt
        //{
        //    get { return _basicRecommendationmailertxt; }
        //    set { _basicRecommendationmailertxt = value; OnPropertyChanged(nameof(basicRecommendationmailertxt)); }
        //}
        //private string _basicpromotiontxt = "";
        //public string basicpromotiontxt
        //{
        //    get { return _basicpromotiontxt; }
        //    set { _basicpromotiontxt = value; OnPropertyChanged(nameof(basicpromotiontxt)); }
        //}



        //private string _eliteAdpublishtxt = "";
        //public string eliteAdpublishtxt
        //{
        //    get { return _eliteAdpublishtxt; }
        //    set { _eliteAdpublishtxt = value; OnPropertyChanged(nameof(eliteAdpublishtxt)); }
        //}
        //private string _elitevisibilityresponsetxt = "";
        //public string elitevisibilityresponsetxt
        //{
        //    get { return _elitevisibilityresponsetxt; }
        //    set { _elitevisibilityresponsetxt = value; OnPropertyChanged(nameof(basicvisibilityresponsetxt)); }
        //}
        //private string _eliteRecommendationmailertxt = "";
        //public string eliteRecommendationmailertxt
        //{
        //    get { return _eliteRecommendationmailertxt; }
        //    set { _eliteRecommendationmailertxt = value; OnPropertyChanged(nameof(eliteRecommendationmailertxt)); }
        //}
        //private string _elitepromotiontxt = "";
        //public string elitepromotiontxt
        //{
        //    get { return _elitepromotiontxt; }
        //    set { _elitepromotiontxt = value; OnPropertyChanged(nameof(elitepromotiontxt)); }
        //}

        





        private bool _recommendationvisible = false;
        public bool recommendationvisible
        {
            get { return _recommendationvisible; }
            set { _recommendationvisible = value;OnPropertyChanged(nameof(recommendationvisible)); }
        }
        private string _basicadtxt = "Basic";
        public string basicadtxt
        {
            get { return _basicadtxt; }
            set { _basicadtxt = value; OnPropertyChanged(nameof(basicadtxt)); }
        }

        public Package_VM(Daycareposting postdata)
        {
            try
            {
                post = postdata;
                if (postdata.ismyneed =="1" & postdata.ismyneedpayment!="1")
                {
                    //post.mode = "edit";
                    if(string.IsNullOrEmpty(post.maincategory))
                    {
                        post.maincategory = "day-care";
                    }
                }
                if(postdata.supercategoryid=="1")
                {
                    if(postdata.secondarycategoryid=="8")//Nanny / Babysitter
                    {
                        recommendationvisible = true;
                    }
                    else if (postdata.secondarycategoryid == "9")//Care Center
                    {
                            basicadtxt = "Sponsored";
                            recommendationvisible = false;
                    }
                    else if (postdata.secondarycategoryid == "49")//Family Child Care
                    {
                        recommendationvisible = true;
                    }
                    else if (postdata.secondarycategoryid == "11")//Elder Care Provider Nurse
                    {
                        recommendationvisible = true;
                    }
                    else if (postdata.secondarycategoryid == "12")//Elder Care Center
                    {
                        basicadtxt = "Sponsored";
                        recommendationvisible = false;
                    }
                    else if (postdata.secondarycategoryid == "13")//Pet Care Provider
                    {
                        recommendationvisible = true;
                    }
                    else if (postdata.secondarycategoryid == "14")//Pet Care Center
                    {
                        basicadtxt = "Sponsored";
                        recommendationvisible = false;
                    }
                    else if (postdata.primarycategoryid == "44")//Housekeeper
                    {
                        recommendationvisible = true;
                    }
                    else if (postdata.primarycategoryid == "45")//Cook
                    {
                        recommendationvisible = true;
                    }
                }
                else
                {
                    if (postdata.secondarycategoryid == "8")//Nanny / Babysitter
                    {
                        recommendationvisible = true;
                    }
                    else if (postdata.secondarycategoryid == "9")//Care Center
                    {
                        recommendationvisible = false;
                    }
                    else if (postdata.secondarycategoryid == "32")//Nurse
                    {
                        recommendationvisible = true;
                    }
                    else if (postdata.secondarycategoryid == "12")//Elder Care Center
                    {
                        basicadtxt = "Sponsored";
                        recommendationvisible = false;
                    }
                    else if (postdata.secondarycategoryid == "13")//Pet Care Provider
                    {
                        recommendationvisible = true;
                    }
                    else if (postdata.secondarycategoryid == "33")//Pet Care Center
                    {
                        basicadtxt = "Sponsored";
                        recommendationvisible = false;
                    }
                    else if (postdata.secondarycategoryid == "44")//Housekeeper
                    {
                        recommendationvisible = true;
                    }
                    else if (postdata.secondarycategoryid == "45")//Cook
                    {
                        recommendationvisible = true;
                    }
                    else if (postdata.secondarycategoryid == "56")//Housekeeper & Cook
                    {
                        recommendationvisible = true;
                    }
                    else
                    {
                        recommendationvisible = false;
                    }
                }
              
                TapOnFreePackage = new Command<string>(SelectFreepkg);
                // TapOnSponsoredPackage = new Command<string>(SelectSponsoredpkg); //basicpkg
                TapOnbasicPackage = new Command<string>(SelectSponsoredpkg);
                TapOnStandardPackage = new Command<string>(SelectStandpkg);
                TapOnPremiumPackage = new Command<string>(SelectPremiumpkg);
                TapOnPremiumPlusPackage = new Command<string>(SelectPremiumPluspkg);
                TapOnElitePackage = new Command<string>(SelectElitepkg);
                TapOnStarterPackage = new Command<string>(SelectStarterpkg);

                getpackagedetails();
            }
            catch(Exception ex)
            {

            }
        }
        public async void SelectPremiumPluspkg(string amount)
        {
            try
            {
                var curpage = GetCurrentPage();
                basicbtn = true;
                Selbasicbtn = false;
                premiumbtn = true;
                Selpremiumbtn = false;
                freebtn = true;
                Selfreebtn = false;
                standardpkgbtn = true;
                Selstandardpkgbtn = false;
                elitepkgbtn = true;
                selelitepkgbtn = false;
                premiumpluspkgbtn = false;
                selpremiumpluspkgbtn = true;
                post.numberofdays = premiumpluspkgdays.Replace(" days", "");
                post.pkgid = premiumpluspkgid;
                post.ordergroup = premiumpluspordrgrp;
                post.addonamount = premiumpluspkgaddonamnt;
                post.ordertype = "Premium+";
                post.ordertypetxt = premiumplusordrtxt;
                dialogs.ShowLoading("", null);
                await Task.Delay(300);
                var DCpostAPI = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                var postresponse = await DCpostAPI.postsecondsubmit(post);
                dialogs.HideLoading();
                //if (!string.IsNullOrEmpty(postresponse.resultinformation))
                if (postresponse.resultinformation == "inserted success" || postresponse.resultinformation == "updated success")
                    await curpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Payment_dc(post, "Premium+", premiumpluspkgamnt));
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
            }
        }

        public async void SelectStarterpkg(string amount)
        {
            try
            {
                var curpage = GetCurrentPage();
                basicbtn = true;
                Selbasicbtn = false;
                premiumbtn = true;
                Selpremiumbtn = false;
                freebtn = true;
                Selfreebtn = false;
                standardpkgbtn = true;
                Selstandardpkgbtn = false;
                elitepkgbtn = true;
                selelitepkgbtn = false;
                premiumpluspkgbtn = true;
                selpremiumpluspkgbtn = false;
                starterpkgbtn = false;
                selstarterpkgbtn = true;
                post.numberofdays = starterpkgdays.Replace(" days", "");
                post.pkgid = starterpkgid;
                post.ordergroup = starterordrgrp;
                post.addonamount = starterpkgaddonamnt;
                post.ordertype = "Starter";
                post.ordertypetxt = starterordrtxt;
                dialogs.ShowLoading("", null);
                await Task.Delay(300);
                var DCpostAPI = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                var postresponse = await DCpostAPI.postsecondsubmit(post);
                dialogs.HideLoading();
                await curpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Payment_dc(post, "Starter", starterpkgamnt));
                ////if (!string.IsNullOrEmpty(postresponse.resultinformation))
                //if (postresponse.resultinformation == "inserted success" || postresponse.resultinformation == "updated success")
                //    await curpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Payment_dc(post, "Starter", starterpkgamnt));
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
            }
        }

        public async void SelectElitepkg(string amount)
        {
            try
            {
                var curpage = GetCurrentPage();
                basicbtn = true;
                Selbasicbtn = false;
                premiumbtn = true;
                Selpremiumbtn = false;
                freebtn = true;
                Selfreebtn = false;
                standardpkgbtn = true;
                Selstandardpkgbtn = false;
                elitepkgbtn = false;
                selelitepkgbtn = true;
                premiumpluspkgbtn = true;
                selpremiumpluspkgbtn = false;
                post.numberofdays = elitepkgdays.Replace(" days", "");
                post.pkgid = elitepkgid;
                post.ordergroup = eliteordrgrp;
                post.addonamount = elitepkgaddonamnt;
                post.ordertype = "Elite";
                post.ordertypetxt = eliteordrtxt;
                dialogs.ShowLoading("", null);
                await Task.Delay(300);
                var DCpostAPI = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                var postresponse = await DCpostAPI.postsecondsubmit(post);
                dialogs.HideLoading();
                //if (!string.IsNullOrEmpty(postresponse.resultinformation))
                if (postresponse.resultinformation == "inserted success" || postresponse.resultinformation == "updated success")
                    await curpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Payment_dc(post, "Elite", elitepkgamnt));
            }
            catch (Exception ex)
            {
                dialogs.HideLoading();
            }
        }


        public async void SelectFreepkg(string amount)
        {
            try
            {
                var curpage = GetCurrentPage();
                dialogs.ShowLoading("", null);
                basicbtn = false;
                Selbasicbtn = true;
                premiumbtn = true;
                Selpremiumbtn = false;
                freebtn = true;
                Selfreebtn = false;
                standardpkgbtn = true;
                Selstandardpkgbtn = false;
                post.numberofdays = freepkgdays.Replace(" days", "");
                post.ordergroup = freepordrgrp;
                post.ordertype = "free";
                post.ordertypetxt = freeordrtxt;
                post.pkgid = freepkgid;
                post.addonamount = freepkgaddonamnt;
                await Task.Delay(300);
                // await curpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Payment_dc(post, "basic", basicpkgamnt));
                var DCpostAPI = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                var postresponse = await DCpostAPI.postsecondsubmit(post);
                dialogs.HideLoading();
                //if(!string.IsNullOrEmpty(postresponse.resultinformation))
                if (postresponse.resultinformation == "inserted success" || postresponse.resultinformation == "updated success")
                {
                    //await curpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.JobSuccess(post.businessid, post.usertype));
                    // await curpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.OrderReceipt(post.businessid, post.numberofdays, post.ordertype));
                    await curpage.Navigation.PushAsync(new NRIApp.LocalJobs.Features.Posting.Views.Spammer());
                }
                else
                {
                    dialogs.Toast("Try again later...");
                }
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        public async void SelectSponsoredpkg(string amount)
        {
            try
            {
                var curpage = GetCurrentPage();
                dialogs.ShowLoading("", null);
                basicbtn = false;
                Selbasicbtn = true;
                premiumbtn = true;
                Selpremiumbtn = false;
                freebtn = true;
                Selfreebtn = false;
                standardpkgbtn = true;
                Selstandardpkgbtn = false;
                post.numberofdays = basicpkgdays.Replace(" days", "");
                post.ordergroup = sponsoredordrgrup;
                post.ordertype = "Sponsored";
                post.ordertypetxt = basicdordrtxt;
                post.pkgid = sponsoredpkgid;
                post.addonamount = basicpkgaddonamnt;
                await Task.Delay(300);
                var DCpostAPI = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                var postresponse = await DCpostAPI.postsecondsubmit(post);
                dialogs.HideLoading();
                //if (!string.IsNullOrEmpty(postresponse.resultinformation))
                if (postresponse.resultinformation == "inserted success" || postresponse.resultinformation == "updated success")
                    await curpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Payment_dc(post, "Sponsored", basicpkgamnt));
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        public async void SelectStandpkg(string amount)
        {
            try
            {
                var curpage = GetCurrentPage();
                dialogs.ShowLoading("", null);
                basicbtn = true;
                Selbasicbtn = false;
                premiumbtn = true;
                Selpremiumbtn = false;
                freebtn = true;
                Selfreebtn = false;
                standardpkgbtn = false;
                Selstandardpkgbtn = true;
                post.numberofdays = standardpkgdays.Replace(" days", "");
                post.ordergroup = standardordrgrup;
                post.pkgid = standardpkgid;
                post.ordertype = "Standard";
                post.ordertypetxt = standardordrtxt;
                post.addonamount = standardpkgaddonamnt;
                await Task.Delay(300);
                var DCpostAPI = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                var postresponse = await DCpostAPI.postsecondsubmit(post);
                dialogs.HideLoading();
                //if (!string.IsNullOrEmpty(postresponse.resultinformation))
                if (postresponse.resultinformation == "inserted success" || postresponse.resultinformation == "updated success")
                    await curpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Payment_dc(post, "standard", standardpkgamnt));
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        public async void SelectPremiumpkg(string amount)
        {
            try
            {
                var curpage = GetCurrentPage();
                basicbtn = true;
                Selbasicbtn = false;
                premiumbtn = false;
                Selpremiumbtn = true;
                freebtn = true;
                Selfreebtn = false;
                standardpkgbtn = true;
                Selstandardpkgbtn = false;
                post.numberofdays = premiumpkgdays.Replace(" days", "");
                post.pkgid = premiumpkgid;
                post.ordergroup = premiumordrgrup;
                post.ordertype = "premium";
                post.ordertypetxt = premiumordrtxt;
                post.addonamount = premiumpkgaddonamnt;
                dialogs.ShowLoading("", null);
                await Task.Delay(300);
                var DCpostAPI = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                var postresponse = await DCpostAPI.postsecondsubmit(post);
                dialogs.HideLoading();
                //if (!string.IsNullOrEmpty(postresponse.resultinformation))
                if (postresponse.resultinformation == "inserted success" || postresponse.resultinformation == "updated success")
                    await curpage.Navigation.PushAsync(new NRIApp.DayCare.Features.Post.Views.Payment_dc(post, "premium", premiumpkgamnt));
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        private string _freepkgid = "";
        public string freepkgid
        {
            get { return _freepkgid; }
            set { _freepkgid = value;OnPropertyChanged(nameof(freepkgid)); }
        }
        private string _sponsoredpkgid = "";
        public string sponsoredpkgid
        {
            get { return _sponsoredpkgid; }
            set { _sponsoredpkgid = value; OnPropertyChanged(nameof(sponsoredpkgid)); }
        }
        private string _standardpkgid = "";
        public string standardpkgid
        {
            get { return _standardpkgid; }
            set { _standardpkgid = value; OnPropertyChanged(nameof(standardpkgid)); }
        }
        private string _premiumpkgid = "";
        public string premiumpkgid
        {
            get { return _premiumpkgid; }
            set { _premiumpkgid = value; OnPropertyChanged(nameof(premiumpkgid)); }
        }

        private string _freepordrgrp = "";
        public string freepordrgrp
        {
            get { return _freepordrgrp; }
            set { _freepordrgrp = value; OnPropertyChanged(nameof(freepordrgrp)); }
        }
        private string _sponsoredordrgrup = "";
        public string sponsoredordrgrup
        {
            get { return _sponsoredordrgrup; }
            set { _sponsoredordrgrup = value; OnPropertyChanged(nameof(sponsoredordrgrup)); }
        }
        private string _standardordrgrup = "";
        public string standardordrgrup
        {
            get { return _standardordrgrup; }
            set { _standardordrgrup = value; OnPropertyChanged(nameof(standardordrgrup)); }
        }
        private string _premiumordrgrup = "";
        public string premiumordrgrup
        {
            get { return _premiumordrgrup; }
            set { _premiumordrgrup = value; OnPropertyChanged(nameof(premiumordrgrup)); }
        }
        private string _categoryflag = "";
        public string categoryflag
        {
            get { return _categoryflag; }
            set { _categoryflag = value; OnPropertyChanged(nameof(categoryflag)); }
        }
        public bool _elitepkgvisible = false;
        public bool elitepkgvisible
        {
            get { return _elitepkgvisible; }
            set
            {
                _elitepkgvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(elitepkgvisible)));
            }
        }
        public bool _premiumpluspkgvisible = false;
        public bool premiumpluspkgvisible
        {
            get { return _premiumpluspkgvisible; }
            set
            {
                _premiumpluspkgvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpluspkgvisible)));
            }
        }
        private string _standardordrtxt = "";
        public string standardordrtxt
        {
            get { return _standardordrtxt; }
            set { _standardordrtxt = value; OnPropertyChanged(nameof(standardordrtxt)); }
        }
        private string _premiumordrtxt = "";
        public string premiumordrtxt
        {
            get { return _premiumordrtxt; }
            set { _premiumordrtxt = value; OnPropertyChanged(nameof(premiumordrtxt)); }
        }
        private string _basicdordrtxt = "";
        public string basicdordrtxt
        {
            get { return _basicdordrtxt; }
            set { _basicdordrtxt = value; OnPropertyChanged(nameof(basicdordrtxt)); }
        }
        private string _freeordrtxt = "";
        public string freeordrtxt
        {
            get { return _freeordrtxt; }
            set { _freeordrtxt = value; OnPropertyChanged(nameof(freeordrtxt)); }
        }
        private string _premiumplusordrtxt = "";
        public string premiumplusordrtxt
        {
            get { return _premiumplusordrtxt; }
            set { _premiumplusordrtxt = value; OnPropertyChanged(nameof(premiumplusordrtxt)); }
        }
        private string _eliteordrtxt = "";
        public string eliteordrtxt
        {
            get { return _eliteordrtxt; }
            set { _eliteordrtxt = value; OnPropertyChanged(nameof(eliteordrtxt)); }
        }
        private string _starterordrtxt = "";
        public string starterordrtxt
        {
            get { return _starterordrtxt; }
            set { _starterordrtxt = value; OnPropertyChanged(nameof(starterordrtxt)); }
        }
        public async void getpackagedetails()
        {
            try
            {
                dialogs.ShowLoading("",null);
                var dcpackageapi = RestService.For<IDCpackage>(Commonsettings.DaycareAPI);
                    DCpackage list = await dcpackageapi.getdcpackage(post.businessid,post.supercategoryid,post.primarycategoryid,post.secondarycategoryid, post.tertiarycategoryid,post.newcityurl, Commonsettings.UserPid, post.usertype);
                //prc_get_daycareratecard_amount @service='daycare',@businessid='253932',@supercategoryid='1',@primarycategoryid='3',@secondarycategoryid ='9',@tertiarycategoryid ='24',@newcityurl ='new-york-ny',@usertype ='1',@userpid ='43528159' 
                //DCpackage list = await dcpackageapi.getdcpackage("253932", "1", "3", "9", "24", "new-york-ny", "43528159", "1");

                Pack = list.ROW_DATA;
                if (list.ROW_DATA.Count() > 0)
                {
                    foreach (var i in Pack)
                    {
                        //if (i.packagetext == "Standard")
                        if(i.ordergroup=="25" || i.ordergroup=="70"|| i.packagetext == "Standard")
                        {
                            standardpkgdays = i.noofdays + " days";
                            standardpkgamnt = "$" + i.amount;
                            standardpkgid = i.packageid;
                            categoryflag = i.categoryflag;
                            standardpkgvisible = true;
                            standardordrgrup = i.ordergroup;
                            standardordrtxt = i.ordertype;
                            standardpkgaddonamnt = i.addonamount.ToString();
                        }
                        else if(i.ordergroup=="20"||i.packagetext == "Premium")
                        {
                            premiumpkgamnt = "$" + i.amount;
                            premiumpkgid = i.packageid;
                            premiumpkgdays = i.noofdays + " days";
                            categoryflag = i.categoryflag;
                            premiumordrtxt = i.ordertype;
                            premiumpkgvisible = true;
                            premiumordrgrup = i.ordergroup;
                            premiumpkgaddonamnt = i.addonamount.ToString();
                        }
                        else if (i.ordergroup == "30"||i.packagetext == "Sponsored")
                        {
                            basicpkgamnt = "$" + i.amount;
                            sponsoredpkgid = i.packageid;
                            basicpkgdays = i.noofdays + " days";
                            categoryflag = i.categoryflag;
                            basicpkgvisible = true;
                            sponsoredordrgrup = i.ordergroup;
                            basicdordrtxt = i.ordertype;
                            basicpkgaddonamnt = i.addonamount.ToString();
                        }
                        else if (i.ordergroup == "50"||i.packagetext == "Free")
                        {
                            freepkgamnt = "$" + i.amount;
                            freepkgid = i.packageid;
                            freepkgdays = i.noofdays + " days";
                            categoryflag = i.categoryflag;
                            freepordrgrp = i.ordergroup;
                            freeordrtxt = i.ordertype;
                            freepkgaddonamnt = i.addonamount.ToString();
                            if (post.freeadstatus == "1")
                            {
                                freepkgvisible = false;
                            }
                            else
                            {
                                freepkgvisible = true;
                            }
                            //freepkgvisible = true;
                        }
                        else if (i.ordergroup == "10"||i.packagetext == "Premium+")
                        {
                            premiumpluspkgamnt = "$" + i.amount;
                            premiumpluspkgid = i.packageid;
                            premiumpluspkgdays = i.noofdays + " days";
                            categoryflag = i.categoryflag;
                            premiumpluspordrgrp = i.ordergroup;
                            premiumplusordrtxt = i.ordertype;
                            premiumpluspkgaddonamnt = i.addonamount.ToString();
                            premiumpluspkgvisible = true;
                        }
                        else if (i.ordergroup == "5"||i.packagetext == "Elite")
                        {
                            elitepkgamnt = "$" + i.amount;
                            elitepkgid = i.packageid;
                            elitepkgdays = i.noofdays + " days";
                            categoryflag = i.categoryflag;
                            eliteordrgrp = i.ordergroup;
                            elitepkgaddonamnt = i.addonamount.ToString();
                            elitepkgvisible = true;
                        }
                        else if (i.ordergroup == "80"||i.packagetext == "Starter")
                        {
                            starterpkgamnt = "$" + i.amount;
                            starterpkgid = i.packageid;
                            starterpkgdays = i.noofdays + " days";
                            categoryflag = i.categoryflag;
                            starterordrgrp = i.ordergroup;
                            starterpkgaddonamnt = i.addonamount.ToString();
                            starterpkgvisible = true;
                        }
                    }
                    //  post.categoryflag = categoryflag;
                    //myneed
                    //if(post.ismyneed=="1" || (post.ismyneedpayment=="1" && !string.IsNullOrEmpty(post.ordertype)))
                    //{
                    //    var receipt = RestService.For<NRIApp.MyNeeds.Features.Interfaces.IMyNeedData>(Commonsettings.RoommatesAPI);
                    //    var receiptData = await receipt.getreceipt(post.businessid, post.Businesstitleurl, "Day Care");
                    //    if(receiptData!=null)
                    //    {
                    //        post.ordertype = receiptData.RowData[0].Ordertype;
                    //    }
                    //}
                    if ((!string.IsNullOrEmpty(post.ordertype) && post.ismyneed == "1") || (!string.IsNullOrEmpty(post.ordertype) && post.ismyneedpayment == "1") || !string.IsNullOrEmpty(post.Agentadordertype))
                    {
                        if ((!string.IsNullOrEmpty(post.ordertype) || !string.IsNullOrEmpty(post.Agentadordertype)) && post.ismyneed == "1")
                        {
                            freepkgvisible = false;
                        }
                        else
                        {
                            freepkgvisible = true;
                        }
                        if (post.ordertype == "Standard") //|| post.Agentadordertype == "Standard"
                        {
                            // SelectStandpkg(standardpkgamnt);
                            basicbtn = true;
                            Selbasicbtn = false;
                            premiumbtn = true;
                            Selpremiumbtn = true;
                            elitepkgbtn = true;
                            selelitepkgbtn = false;
                            //freebtn = true;
                            //Selfreebtn = false;
                            standardpkgbtn = false;
                            Selstandardpkgbtn = true;
                            premiumpluspkgbtn = true;
                            selpremiumpluspkgbtn = false;

                            basicpkgvisible = false;
                            starterpkgvisible = false;
                        }
                        else if (post.ordertype == "basic" || post.ordertype == "Featured" ) //|| post.Agentadordertype == "premium"
                        {
                            basicbtn = true;
                            Selbasicbtn = false;
                            premiumbtn = true;
                            Selpremiumbtn = true;
                            //freebtn = true;
                            //Selfreebtn = false;
                            standardpkgbtn = true;
                            Selstandardpkgbtn = false;
                            elitepkgbtn = true;
                            selelitepkgbtn = false;
                            premiumpluspkgbtn = true;
                            selpremiumpluspkgbtn = false;
                            starterpkgbtn = false;
                            selstarterpkgbtn = true;

                            //starterpkgvisible = false;
                            //standardpkgvisible = false;
                        }
                        else if (post.ordertype == "Starter") //|| post.Agentadordertype == "premium"
                        {
                            basicbtn = true;
                            Selbasicbtn = false;
                            premiumbtn = true;
                            Selpremiumbtn = true;
                            //freebtn = true;
                            //Selfreebtn = false;
                            standardpkgbtn = true;
                            Selstandardpkgbtn = false;
                            elitepkgbtn = true;
                            selelitepkgbtn = false;
                            premiumpluspkgbtn = true;
                            selpremiumpluspkgbtn = false;
                            starterpkgbtn = false;
                            selstarterpkgbtn = true;

                            //starterpkgvisible = false;
                            //standardpkgvisible = false;
                        }
                        else if (post.ordertype == "Premium") //|| post.Agentadordertype == "premium"
                        {
                            basicbtn = true;
                            Selbasicbtn = false;
                            premiumbtn = true;
                            Selpremiumbtn = true;
                            //freebtn = true;
                            //Selfreebtn = false;
                            standardpkgbtn = true;
                            Selstandardpkgbtn = false;
                            elitepkgbtn = true;
                            selelitepkgbtn = false;
                            premiumpluspkgbtn = true;
                            selpremiumpluspkgbtn = false;

                            //basicpkgvisible = false;
                            //starterpkgvisible = false;
                            //standardpkgvisible = false;
                        }
                        else if (post.ordertype == "Premium+") //|| post.Agentadordertype == "Premium+"
                        {
                            basicbtn = true;
                            Selbasicbtn = false;
                            premiumbtn = true;
                            Selpremiumbtn = false;
                            //freebtn = true;
                            //Selfreebtn = false;
                            standardpkgbtn = true;
                            Selstandardpkgbtn = false;
                            elitepkgbtn = true;
                            selelitepkgbtn = false;
                            premiumpluspkgbtn = false;
                            selpremiumpluspkgbtn = true;

                            //basicpkgvisible = false;
                            //starterpkgvisible = false;
                            //standardpkgvisible = false;
                            //premiumpkgvisible = false;
                        }
                        else if (post.ordertype == "Sponsored") //|| post.Agentadordertype == "Sponsored"
                        {
                            basicbtn = false;
                            Selbasicbtn = true;
                            premiumbtn = true;
                            Selpremiumbtn = false;
                            //freebtn = true;
                            //Selfreebtn = false;
                            standardpkgbtn = true;
                            Selstandardpkgbtn = false;
                            elitepkgbtn = true;
                            selelitepkgbtn = false;
                            premiumpluspkgbtn = true;
                            selpremiumpluspkgbtn = false;
                            
                        }
                        else if (post.ordertype == "Elite") // || post.Agentadordertype == "Elite"
                        {
                            basicbtn = true;
                            Selbasicbtn = false;
                            premiumbtn = true;
                            Selpremiumbtn = false;
                            //freebtn = true;
                            //Selfreebtn = false;
                            standardpkgbtn = true;
                            Selstandardpkgbtn = false;
                            elitepkgbtn = false;
                            selelitepkgbtn = true;
                            premiumpluspkgbtn = true;
                            selpremiumpluspkgbtn = false;

                            //basicpkgvisible = false;
                            //starterpkgvisible = false;
                            //standardpkgvisible = false;
                            //premiumpkgvisible = false;
                        }
                    }
                    dialogs.HideLoading();
                }
                dialogs.HideLoading();
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
            }
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static Page GetCurrentPage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}
