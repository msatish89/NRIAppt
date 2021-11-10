using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Linq;
using NRIApp.Roommates.Features.Post.Models;
using NRIApp.Roommates.Features.Post.Interface;
using NRIApp.Helpers;
using Refit;
using Acr.UserDialogs;
using NRIApp.Roommates.Features.Post.Views;

namespace NRIApp.Roommates.Features.Post.ViewModels
{
    public class Package_VM : INotifyPropertyChanged
    {
        // public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;
        public Command<string> TapOnElitePackage { get; set; }
        public Command<string> TapOnPremiumplusPackage { get; set; }
        public Command<string> TapOnPremiumPackage { get; set; }
        public Command<string> TapOnStandardPackage { get; set; }
        public Command TapOnFreePackage { get; set; }
       
        public string _freepkgdays = "5";
        public string _standardpkgdays = "20";
        public string _standardpkgamnt = "$25";
        public string _premiumpkgdays = "35";
        public string _premiumpkgamnt = "$35";
        public string _premiumpluspkgdays = "40";
        public string _premiumpluspkgamnt = "$45";
        public string _elitepkgdays = "45";
        public string _elitepkgamnt = "$60";
        public string _premiumtotalads = "";
        public string _premiumplustotalads = "";
        public string _elitepkgads = "";
        public string _stdtotalads = "";
        public bool _freepkgbtn = true;
        public bool _selfreepkgbtn = false;
        public bool _standardpkgbtn = true;
        public bool _selstandardpkgbtn = false;
        public bool _premiumbtn = true;
        public bool _selpremiumbtn = false;
        public bool _premiumplusbtn = true;
        public bool _selpremiumplusbtn = false;
        public bool _elitebtn = true;
        public bool _selelitebtn = false;
        public bool _freepkg = false;
        public bool _standardpkg = false;
        public bool _premiumpkg = false;
        public bool _premiumpluspkg = false;
        public bool _elitepkg = false;
        public bool _featuretlevisible = false;
        public bool _agentvisible = false;
        public bool _nonagentvisible = false;

        public bool agentvisible
        {
            get { return _agentvisible; }
            set
            {
                _agentvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(agentvisible)));

            }
        }
        public bool nonagentvisible
        {
            get { return _nonagentvisible; }
            set
            {
                _nonagentvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(nonagentvisible)));

            }
        }

        public bool freepkgvisible
        {
            get { return _freepkg; }
            set
            {
                _freepkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(freepkgvisible)));

            }
        }
        public bool standardpkgvisible
        {
            get { return _standardpkg; }
            set
            {
                _standardpkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgvisible)));

            }
        }
        public bool premiumpkgvisible
        {
            get { return _premiumpkg; }
            set
            {
                _premiumpkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpkgvisible)));

            }
        }
        public bool featuretlevisible
        {
            get { return _featuretlevisible; }
            set
            {
                _featuretlevisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(featuretlevisible)));
            }
        }
        public bool premiumpluspkgvisible
        {
            get { return _premiumpluspkg; }
            set
            {
                _premiumpluspkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpluspkgvisible)));
            }
        }
        public bool elitepkgvisible
        {
            get { return _elitepkg; }
            set
            {
                _elitepkg = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(elitepkgvisible)));
            }
        }
        public bool freepkgbtn
        {
            get { return _freepkgbtn; }
            set
            {
                _freepkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(freepkgbtn)));

            }
        }
        public bool Selfreepkgbtn
        {
            get { return _selfreepkgbtn; }
            set
            {
                _selfreepkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selfreepkgbtn)));

            }
        }
        public bool standardpkgbtn
        {
            get { return _standardpkgbtn; }
            set
            {
                _standardpkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgbtn)));

            }
        }
        public bool Selstandardpkgbtn
        {
            get { return _selstandardpkgbtn; }
            set
            {
                _selstandardpkgbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selstandardpkgbtn)));

            }
        }
        public bool premiumbtn
        {
            get { return _premiumbtn; }
            set
            {
                _premiumbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumbtn)));

            }
        }
        public bool Selpremiumbtn
        {
            get { return _selpremiumbtn; }
            set
            {
                _selpremiumbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selpremiumbtn)));

            }
        }
        public bool premiumplusbtn
        {
            get { return _premiumplusbtn; }
            set
            {
                _premiumplusbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumplusbtn)));

            }
        }
        public bool Selpremiumplusbtn
        {
            get { return _selpremiumplusbtn; }
            set
            {
                _selpremiumplusbtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selpremiumplusbtn)));

            }
        }
        public bool elitebtn
        {
            get { return _elitebtn; }
            set
            {
                _elitebtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(elitebtn)));

            }
        }
        public bool Selelitebtn
        {
            get { return _selelitebtn; }
            set
            {
                _selelitebtn = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selelitebtn)));

            }
        }

        public string freepkgdays
        {
            get { return _freepkgdays; }
            set
            {
                _freepkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(freepkgdays)));

            }
        }
        public string standardpkgdays
        {
            get { return _standardpkgdays; }
            set
            {
                _standardpkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgdays)));

            }
        }
        public string standardpkgamnt
        {
            get { return _standardpkgamnt; }
            set
            {
                _standardpkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(standardpkgamnt)));

            }
        }
        public string premiumpkgdays
        {
            get { return _premiumpkgdays; }
            set
            {
                _premiumpkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpkgdays)));

            }
        }
        public string premiumpkgamnt
        {
            get { return _premiumpkgamnt; }
            set
            {
                _premiumpkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpkgamnt)));

            }
        }
        public string premiumpluspkgdays
        {
            get { return _premiumpluspkgdays; }
            set
            {
                _premiumpluspkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpluspkgdays)));

            }
        }
        public string premiumpluspkgamnt
        {
            get { return _premiumpluspkgamnt; }
            set
            {
                _premiumpluspkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumpluspkgamnt)));

            }
        }
        public string elitepkgdays
        {
            get { return _elitepkgdays; }
            set
            {
                _elitepkgdays = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(elitepkgdays)));

            }
        }
        public string elitepkgamnt
        {
            get { return _elitepkgamnt; }
            set
            {
                _elitepkgamnt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(elitepkgamnt)));

            }
        }
        public string elitepkgads
        {
            get { return _elitepkgads; }
            set
            {
                _elitepkgads = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(elitepkgads)));
            }
        }
        public string premiumplustotalads
        {
            get { return _premiumplustotalads; }
            set
            {
                _premiumplustotalads = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumplustotalads)));
            }
        }
        public string premiumtotalads
        {
            get { return _premiumtotalads; }
            set
            {
                _premiumtotalads = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(premiumtotalads)));
            }
        }
        public string stdtotalads
        {
            get { return _stdtotalads; }
            set
            {
                _stdtotalads = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(stdtotalads)));
            }
        }
        public List<Packages> _pack { get; set; }
        public List<Packages> Pack
        {
            get
            {
                return _pack;
            }
            set
            {
                _pack = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pack)));
            }
        }
        private static string adid = "";
        private static int agentid;
        private static string usertype = "";
        Postfirst pft = new Postfirst();
        
        public Package_VM(Postfirst pst)
        {
            pft = pst;
            adid = pst.adid.ToString();
            agentid = pst.agentid;
           if(string.IsNullOrEmpty(pst.usertype))
            {
                usertype = "1";
                pst.usertype = "1";
            }
           else
            {
                usertype = pst.usertype;
            }
            
            TapOnElitePackage = new Command<string>(SelectElitepkg);
            TapOnPremiumplusPackage = new Command<string>(SelectPremiumpluspkg);
            TapOnPremiumPackage = new Command<string>(SelectPremiumpkg);
            TapOnStandardPackage = new Command<string>(SelectStandpkg);
            TapOnFreePackage = new Command(SelectFreepkg);
            Getpackages(pst.adid, pst.userpid,pst.usertype);
            if (lcfsecond.postscuccesbackbtn == 1 && Commonsettings.UserDeviceOSVersion=="android")
            {
                var currentpage = GetCurrentPage();
                currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
            }
            // Getpackages(9923650, "16487447");
        }
        public async void SelectPremiumpluspkg(string amnnt)
        {
            var curpage = GetCurrentPage();
            dialog.ShowLoading("");
            premiumplusbtn = false;
            Selpremiumplusbtn = true;
            premiumbtn = true;
            Selpremiumbtn = false;
            elitebtn = true;
            Selelitebtn = false;
            standardpkgbtn = true;
            Selstandardpkgbtn = false;
            freepkgbtn = true;
            Selfreepkgbtn = false;
            var api = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
            Pkgsucess list = await api.updatepackage(adid.ToString(), premiumpluspkgdays, "Premiumplus",premiumplustotalads,agentid);
            if (list.resultinformation == "updated successfully")
            {
                dialog.HideLoading();
                await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Payment(amnnt, "Premiumplus", adid, pft));
            }
            else
            {
                dialog.HideLoading();
                await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Thankyou());
                // dialog.Toast("You have reached your maximum limits");
            }
        }
        public async void SelectPremiumpkg(string amnnt)
        {
            try
            {
                var curpage = GetCurrentPage();
                dialog.ShowLoading("");
                premiumplusbtn = true;
                Selpremiumplusbtn = false;
                premiumbtn = false;
                Selpremiumbtn = true;
                elitebtn = true;
                Selelitebtn = false;
                standardpkgbtn = true;
                Selstandardpkgbtn = false;
                freepkgbtn = true;
                Selfreepkgbtn = false;
                var api = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                Pkgsucess list = await api.updatepackage(adid.ToString(), premiumpkgdays, "Premium", premiumtotalads, agentid);

                if (list.resultinformation == "updated successfully")
                {
                    dialog.HideLoading();
                    await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Payment(amnnt, "Premium", adid, pft));
                }
                else
                {
                    dialog.HideLoading();
                    await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Thankyou());
                    //dialog.Toast("You have reached your maximum limits");
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void SelectStandpkg(string amnnt)
        {
           try
            {
                var curpage = GetCurrentPage();
                dialog.ShowLoading("");
                premiumplusbtn = true;
                Selpremiumplusbtn = false;
                premiumbtn = true;
                Selpremiumbtn = false;
                elitebtn = true;
                Selelitebtn = false;
                standardpkgbtn = false;
                Selstandardpkgbtn = true;
                freepkgbtn = true;
                Selfreepkgbtn = false;
                var api = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                Pkgsucess list = await api.updatepackage(adid.ToString(), standardpkgdays, "Standard", stdtotalads, agentid);

                if (list.resultinformation == "updated successfully")
                {
                    dialog.HideLoading();
                    await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Payment(amnnt, "Standard", adid, pft));
                }
                else
                {
                    dialog.HideLoading();
                    //dialog.Toast("You have reached your maximum limits");
                    await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Thankyou());
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void SelectFreepkg()
        {
            try
            {
                var curpage = GetCurrentPage();
                dialog.ShowLoading("");
                var nodays = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                Pkgsucess FreeadDuration = await nodays.getnoofdays(usertype);
                var api = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                Pkgsucess list = await api.updatepackage(adid.ToString(), FreeadDuration.resultinformation, "Free", "", agentid);

                if (list.resultinformation == "updated successfully")
                {
                    dialog.HideLoading();
                    //await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Payment("", adid, pft));
                    if (list.isfreeadpublish == "1")
                        await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.postsuccess(adid, usertype, "Free"));
                    else
                        await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Thankyou());
                }
                else
                {
                    dialog.HideLoading();
                    await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Thankyou());
                    //dialog.Toast("You have reached your maximum limits");
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void Getpackages(int adid, string pid,string usertype)
        {
            try
            {
                dialog.ShowLoading("");
                if(string.IsNullOrEmpty(pid))
                {
                    pid = Commonsettings.UserPid;
                }
                var api = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                Packagelist list = await api.getpackages(adid.ToString(), pid , usertype);
                Pack = list.ROW_DATA;

                if (list.ROW_DATA.Count() > 0)
                {
                    foreach (var i in Pack)
                    {
                        if (i.ordertype == "Free")
                        {
                            freepkgdays = i.noofdays + " days";
                            freepkgvisible = true;
                        }
                        else if (i.ordertype == "Standard")
                        {
                            standardpkgdays = i.noofdays + " days";
                            standardpkgamnt = "$" + i.amount;
                            standardpkgvisible = true;
                            if (usertype == "2")
                            {
                                stdtotalads = i.featuretitle + " ads";
                                featuretlevisible = true;
                            }
                        }
                        else if (i.ordertype == "Premium")
                        {
                            premiumpkgamnt = "$" + i.amount;
                            premiumpkgdays = i.noofdays + " days";
                            premiumpkgvisible = true;
                            if (usertype == "2")
                            {
                                premiumtotalads = i.featuretitle + " ads";
                                featuretlevisible = true;
                            }
                        }
                        else if (i.ordertype == "Premiumplus")
                        {
                            premiumpluspkgamnt = "$" + i.amount;
                            premiumpluspkgdays = i.noofdays + " days";
                            premiumpluspkgvisible = true;
                            if (usertype == "2")
                            {
                                premiumplustotalads = i.featuretitle + " ads";
                                featuretlevisible = true;
                            }
                        }
                        else if (i.ordertype == "Elite")
                        {
                            elitepkgamnt = "$" + i.amount.ToString();
                            elitepkgdays = i.noofdays + " days";
                            elitepkgvisible = true;
                            if (usertype == "2")
                            {
                                elitepkgads = i.featuretitle + " ads";
                                featuretlevisible = true;
                            }
                        }
                    }
                    if (usertype == "2")
                    {
                        nonagentvisible = false;
                        agentvisible = true;
                    }
                    else
                    {
                        nonagentvisible = true;
                        agentvisible = false;
                    }
                    if((!string.IsNullOrEmpty(pft.Ordertype)&& pft.ismyneed =="1" && pft.usertype == "1") ||(!string.IsNullOrEmpty(pft.Ordertype) && pft.ismyneedpayment == "1"&& pft.usertype == "1") )
                    {
                        
                        if(pft.Ordertype=="Standard")
                        {
                            // SelectStandpkg(standardpkgamnt);
                            premiumplusbtn = true;
                            Selpremiumplusbtn = false;
                            premiumbtn = true;
                            Selpremiumbtn = false;
                            elitebtn = true;
                            Selelitebtn = false;
                            standardpkgbtn = false;
                            Selstandardpkgbtn = true;
                            freepkgbtn = true;
                            Selfreepkgbtn = false;
                        }
                        else if (pft.Ordertype == "Premium")
                        {
                            premiumplusbtn = true;
                            Selpremiumplusbtn = false;
                            premiumbtn = false;
                            Selpremiumbtn = true;
                            elitebtn = true;
                            Selelitebtn = false;
                            standardpkgbtn = true;
                            Selstandardpkgbtn = false;
                            freepkgbtn = true;
                            Selfreepkgbtn = false;
                            standardpkgvisible = false;
                        }
                        else if (pft.Ordertype == "Premiumplus")
                        {
                            premiumplusbtn = false;
                            Selpremiumplusbtn = true;
                            premiumbtn = true;
                            Selpremiumbtn = false;
                            elitebtn = true;
                            Selelitebtn = false;
                            standardpkgbtn = true;
                            Selstandardpkgbtn = false;
                            freepkgbtn = true;
                            Selfreepkgbtn = false;
                            standardpkgvisible = false;
                            premiumpkgvisible = false;
                        }
                        else if (pft.Ordertype == "Elite")
                        {
                            premiumplusbtn = true;
                            Selpremiumplusbtn = false;
                            premiumbtn = true;
                            Selpremiumbtn = false;
                            elitebtn = false;
                            Selelitebtn = true;
                            standardpkgbtn = true;
                            Selstandardpkgbtn = false;
                            freepkgbtn = true;
                            Selfreepkgbtn = false;
                            standardpkgvisible = false;
                            premiumpkgvisible = false;
                            premiumpluspkgvisible = false;
                            
                        }

                        if (!string.IsNullOrEmpty(pft.Ordertype) && pft.ismyneed == "1")
                        {
                            freepkgvisible = false;
                        }
                        else
                        {
                            freepkgvisible = true;
                        }
                    }
                    dialog.HideLoading();
                }
                else
                    dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void SelectElitepkg( string amnnt)
        {
            var curpage = GetCurrentPage();
            try
            {
                dialog.ShowLoading("");
                premiumplusbtn = true;
                Selpremiumplusbtn = false;
                premiumbtn = true;
                Selpremiumbtn = false;
                elitebtn = false;
                Selelitebtn = true;
                standardpkgbtn = true;
                Selstandardpkgbtn = false;
                freepkgbtn = true;
                Selfreepkgbtn = false;

                

                var api = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
                Pkgsucess list = await api.updatepackage(adid.ToString(), elitepkgdays, "Elite","",agentid);
               
                if (list.resultinformation == "updated successfully")
                {
                    dialog.HideLoading();
                    await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Payment(amnnt, "Elite", adid, pft));
                }
                else
                {
                    dialog.HideLoading();
                    await curpage.Navigation.PushAsync(new NRIApp.Roommates.Features.Post.Views.Thankyou());
                    //dialog.Toast("You have reached your maximum limits");
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                dialog.HideLoading();
            }
           
           
        }
        public async void insertagentdetails()
        {
            var PostSubmit = RestService.For<IRMCategory>(Commonsettings.RoommatesAPI);
            var data = await PostSubmit.otppostsecondsubmit(pft);

            if (data != null)
            {
                var curpage = GetCurrentPage();
                if (data.type == "0")
                {
                    await curpage.Navigation.PushAsync(new PostOTP(pft, data.pinno));
                }
                else
                {
                    if (data.ispayment == 1)
                    {
                        await curpage.Navigation.PushAsync(new Package_RM(pft));
                    }
                    else
                    {
                        // await curpage.Navigation.PushAsync(new Thankyou());
                        freeadpost(data.adid, pft.usertype);
                    }
                }

            }
        }
        public async void freeadpost(string adid, string usertype)
        {
            //var nodays = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
            //Pkgsucess FreeadDuration = await nodays.getnoofdays(usertype);
            var api = RestService.For<IPayment>(Commonsettings.RoommatesAPI);
            Pkgsucess list = await api.updatepackage(adid, freepkgdays, "Free","",agentid);
            if (list.resultinformation == "updated successfully")
            {
                var curpage = GetCurrentPage();
                await curpage.Navigation.PushAsync(new postsuccess(adid,usertype,"Free"));
            }
            else
            {
                dialog.HideLoading();
                dialog.Toast("You have reached your maximum limits");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public Page GetCurrentPage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}
