using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using NRIApp.Helpers;
using NRIApp.Signin.Interfaces;
using NRIApp.Signin.Models;
using NRIApp.Signin.ViewModels;
using Refit;
using Xamarin.Forms;
using NRIApp.USHome.Models;
using System.Text.RegularExpressions;
using Acr.UserDialogs;
using Plugin.Connectivity;

namespace NRIApp.Signin.ViewModels
{
    public class Signinviewmodel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand signincommand { get; set; }
        public ICommand signupcommand { get; set; }
        public ICommand opensignup { get; set; }
        public ICommand openlogin { get; set; }
        public ICommand skipcommand { get; set; }
        public ICommand openforgotpass { get; set; }
        public ICommand forgotpasscommand { get; set; }
        public ICommand firstsignincommand { get; set; }
        public ICommand Openmobilenocommand { get; set; }
        public ICommand Otpcommand { get; set; }
        public ICommand ResendOtpcommand { get; set; }
        public ICommand Editphnocommand { get; set; }
        IUserDialogs dialog = UserDialogs.Instance;
        public Signinviewmodel()
        {
            signincommand = new Command(Signin);
            firstsignincommand = new Command(Firstsignin);
            signupcommand = new Command(Signup);
            opensignup = new Command(opensignuppage);
            skipcommand = new Command(skippage);
            openforgotpass = new Command(forgotpassword);
            forgotpasscommand = new Command(sbmtforgotpass);
            openlogin = new Command(openloginpage);
            Openmobilenocommand = new Command(Openmobileno);
            Otpcommand = new Command(otpsubmit);
            ResendOtpcommand = new Command(resendotp);
            Editphnocommand = new Command(editphno);
        }

      
        public void Openmobileno()
        {
            StackVisible = false;
            Visible = true;
        }
        public bool _stackvisible = false;
        public bool StackVisible
        {
            get { return _stackvisible; }
            set
            {
                _stackvisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StackVisible)));
            }
        }
        public string _otp = "";
        public string OTP
        {
            get { return _otp; }
            set
            {
                _otp = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OTP)));
            }
        }
        public string _otptext { get; set; }
        public string OTPText
        {
            get { return _otptext; }
            set
            {
                _otptext = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OTPText)));
            }
        }

        private string _Selectcountry = "+1";
        public string Selectcountry
        {
            get { return _Selectcountry; }
            set { _Selectcountry = value; }
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

        public List<Logindetails> _login { get; set; }
        public List<Logindetails> LoginD
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LoginD)));
            }
        }

        public List<signupdetails> _signupdetails { get; set; }
        public List<signupdetails> Signupdetails
        {
            get
            {
                return _signupdetails;
            }
            set
            {
                _signupdetails = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Signupdetails)));
            }
        }


        public string _name = "";
        public string UName
        {
            get { return _name; }
            set
            {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UName)));
            }
        }

        public string _email = "";
        public string UEmail
        {
            get { return _email; }
            set
            {
                    _email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UEmail)));

            }
        }

        public string _password = "";
        public string Password
        {
            get { return _password; }
            set
            {

                _password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));

            }
        }
        public string _mobileno = "";
        public string Mobileno
        {
            get { return _mobileno; }
            set
            {
                    _mobileno = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Mobileno)));

            }
        }

        bool validate = true;
        public bool validateloginform()
        {
            validate = true;
            if (UEmail.Trim().Length == 0)
            {
                validate = false;
                dialog.Toast("Please enter your email");
            }
            else if (!CheckValidEmail(UEmail))
            {
                validate = false;
                dialog.Toast("Please enter valid email id");
            }
            else if (Password.Trim().Length == 0)
            {
                validate = false;
                dialog.Toast("Please enter your email");
            }
          
            return validate;
        }

        bool CheckValidPhone(string inputPhone)
        {
            bool bPhnumValid = false;
            string firstchar = inputPhone.Substring(0, 1);
            if (inputPhone.Length > 6)
            {
                for (var iphnum = 0; iphnum < 6; iphnum++)
                {
                    string chara = inputPhone[iphnum].ToString();
                    if (chara != firstchar)
                        bPhnumValid = true;

                }
            }
            return bPhnumValid;
        }
        public bool validatesignupnform()
        {
            validate = true;
            if (UName.Trim().Length == 0)
            {
                //alert
                validate = false;
                //  dialog.ShowLoading("Loading");
                dialog.Toast("Please enter username");

            }
            else if (!CheckAplhaOnly(UName))
            {
                validate = false;
                dialog.Toast("Please enter a valid username");

            }
            else if (UEmail.Trim().Length == 0)
            {
                validate = false;
                dialog.Toast("Please enter your email");
            }
            else if (!CheckValidEmail(UEmail))
            {
                validate = false;
                dialog.Toast("Please enter valid Email ID");
            }
            else if (Password.Trim().Length == 0)
            {
                validate = false;
                dialog.Toast("Please enter your password");
            }
            else if (string.IsNullOrEmpty(Mobileno))
            {
                validate = false;
                dialog.Toast("Enter your mobile number");
            }
            else if (Mobileno.Length < 10)
            {
                validate = false;
                dialog.Toast("Please enter a 10-digit mobile number");
            }
            else if (!CheckValidPhone(Mobileno))
            {
                validate = false;
                dialog.Toast("Please enter valid mobile number");
            }
            return validate;
        }
        bool CheckAplhaOnly(string inputString)
        {
            return Regex.IsMatch(inputString, "^[a-zA-Z \\.-]+$");
        }
        bool CheckValidEmail(string inputEmail)
        {
            return Regex.IsMatch(inputEmail, "[\\w\\.-]+@([\\w\\-]+\\.)+[A-Z]{2,4}$", RegexOptions.IgnoreCase);
        }

        async void Signin()
        {
            if(Commonsettings.UserPid !="" && Commonsettings.UserPid!=null)
            {
               
              var answer = await dialog.ConfirmAsync("","Are you sure?", "OK", "Cancel");
                if(answer)
                {
                    Commonsettings.UserEmail = "";
                    Commonsettings.UserMobileno = "";
                    Commonsettings.UserName = "";
                    Commonsettings.UserPid = "";
                }
            }
            else
            {
                validateloginform();
                if (validate == true)
                {
                    dialog.ShowLoading("", MaskType.Black);
                   // Visible = true;
                    var currentpage = GetCurrentPage();
                    var nsAPI = RestService.For<ISignin>(Commonsettings.TechjobsAPI);
                    Login log = new Login()
                    {
                        useremail = UEmail,
                        password = Password
                    };
                    Loginsucess list = await nsAPI.SulekhaLogin(log);
                    LoginD = list.ROW_PRC_CHECKIN_MOBILE_LOGIN;

                    if (LoginD != null)
                    {
                            if (LoginD.Last().status == "1")
                            {
                                Commonsettings.UserEmail = LoginD.Last().email;
                                Commonsettings.UserMobileno = LoginD.Last().phone;
                                Commonsettings.UserName = LoginD.Last().name;
                                Commonsettings.UserPid = LoginD.Last().pid;
                            Commonsettings.UserId = LoginD.Last().userid;
                                dialog.HideLoading();
                               // Visible = false;

                                await currentpage.Navigation.PopModalAsync();
                            }
                            else
                            {
                                //Visible = false;
                                dialog.HideLoading();
                                dialog.Toast("Please enter valid credentials");
                            }

                    }
                    else
                    {
                        //Visible = false;
                        dialog.HideLoading();
                        dialog.Toast("Please enter valid credentials");
                    }
                }
            }
           
          
        }

        async void Firstsignin()
        {
            try
            {
                validateloginform();
                if (validate == true)
                {
                    dialog.ShowLoading("", MaskType.Black);
                    var currentpage = GetCurrentPage();
                    var nsAPI = RestService.For<ISignin>(Commonsettings.TechjobsAPI);
                    Login log = new Login()
                    {
                        useremail = UEmail,
                        password = Password
                    };
                    Loginsucess list = await nsAPI.SulekhaLogin(log);
                    LoginD = list.ROW_PRC_CHECKIN_MOBILE_LOGIN;
                    if (LoginD != null)
                    {
                        if (LoginD.Last().status == "1")
                        {
                            Commonsettings.UserEmail = LoginD.Last().email;
                            Commonsettings.UserMobileno = LoginD.Last().phone;
                            Commonsettings.UserName = LoginD.Last().name;
                            Commonsettings.UserPid = LoginD.Last().pid;
                            Commonsettings.UserId = LoginD.Last().userid;
                            Loginlock details = await nsAPI.Loginlocking(Commonsettings.UserEmail, Commonsettings.UserDeviceId, Commonsettings.UserMobileOS, Commonsettings.Userappversion, "2");
                            dialog.HideLoading();
                            if (Commonsettings.isadpage == "1")
                           await currentpage.Navigation.PushAsync(new NRIApp.USHome.Views.Ads());
                            else
                            {
                                Application.Current.MainPage = new NavigationPage(new NRIApp.USHome.Views.HomePage());
                                ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#e30045");
                                ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
                            }

                        }
                        else
                        {
                            dialog.HideLoading();
                            dialog.Toast("Please enter valid credentials");
                        }

                    }
                    else
                    {
                        dialog.HideLoading();
                        dialog.Toast("Please enter valid credentials");
                    }
                }
            }catch(Exception e)
            {

            }
        }

        async void Signup()
        {
            validatesignupnform();
            if (validate == true)
            {
                //Visible = true;
                dialog.ShowLoading("", MaskType.Black);
                var currentpage = GetCurrentPage();
                var nsAPI = RestService.For<ISignin>(Commonsettings.TechjobsAPI);
                signup log = new signup()
                {
                    username = UName,
                    email = UEmail,
                    password = Password,
                    mobileno = Mobileno,
                    countrycode = Selectcountry,
                    deviceid = Commonsettings.UserDeviceId,
                    deviceos = Commonsettings.UserMobileOS
                };
                signupdetails list = await nsAPI.Sulekhasignup(log);
               // Signupdetails = list.ROW_PRC_ADD_USER_INFO;
                        if (list.result == "inserted")
                        {
                            Commonsettings.UserEmail = UEmail;
                            Commonsettings.UserName = UName;
                            Commonsettings.UserPid = list.pid;
                            OTP = list.otpno;
                            dialog.HideLoading();
                    await currentpage.Navigation.PushAsync(new NRIApp.Signin.Views.OtpVerify(OTP,Mobileno,Selectcountry));
                    await currentpage.Navigation.PopModalAsync();
                    //var answer = await dialog.ConfirmAsync("Created Sucessfully", "", "OK", "");
                    //if(answer)
                    //{
                    //    Application.Current.MainPage = new NavigationPage(new NRIApp.USHome.Views.HomePage());
                    //    ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#e30045");
                    //    ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
                    //}

                    // await currentpage.Navigation.PushModalAsync(new NRIApp.USHome.Views.Homemaster());
                    // await currentpage.Navigation.PopModalAsync();
                }

                        else if(list.result == "already exists")
                        {
                            dialog.HideLoading();
                            dialog.Toast("Already exists");
                        }
                        else
                {
                    dialog.HideLoading();
                    dialog.Toast("Try Again");
                }
            }

        }

        public async void otpsubmit()
        {
            var connected = CrossConnectivity.Current.IsConnected;
            try
            {
                if (connected == true)
                {
                    if (OTPText == OTP)
                    {
                        dialog.ShowLoading("", MaskType.Black);
                        var currentpage = GetCurrentPage();
                        var nsAPI = RestService.For<ISignin>(Commonsettings.TechjobsAPI);
                        signup log = new signup()
                        {
                            email = UEmail,
                            mobileno = Mobileno,
                            countrycode = Selectcountry,
                            deviceid = Commonsettings.UserDeviceId,
                            deviceos = Commonsettings.UserMobileOS
                        };
                        signupdetails list = await nsAPI.Verifyotp(log);
                        await currentpage.Navigation.PopModalAsync();
                        var answer = await dialog.ConfirmAsync("Created Sucessfully", "", "OK", "");
                        if(answer)
                        {
                            Application.Current.MainPage = new NavigationPage(new NRIApp.USHome.Views.HomePage());
                            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#e30045");
                            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
                        }
                       

                    }

                    else
                    {
                        dialog.Toast("Enter valid OTP");
                    }
                }
                else
                    dialog.Toast("Kindly check your internet connection");
            }
            catch (Exception ex)
            {
                string exe = ex.Message.ToString();
                dialog.HideLoading();
                if (connected == false)
                    dialog.Toast("Kindly check your internet connection");
            }

        }
        public async void resendotp()
        {
            try
            {
                    dialog.ShowLoading("", MaskType.Black);
                    var currentpage = GetCurrentPage();
                    var nsAPI = RestService.For<ISignin>(Commonsettings.TechjobsAPI);
                    signupdetails list = await nsAPI.Resendotp(Mobileno,Selectcountry);
                    if (list.otpno != "")
                    {
                        Visible = false;
                        OTP = list.otpno;
                        dialog.HideLoading();
                        dialog.Toast("OTP has been sent to your mobile number");

                    }
                    else
                    {
                        Visible = false;
                        dialog.HideLoading();
                        dialog.Toast("Please try again");
                    }
            }
            catch (Exception ex)
            {
                string exe = ex.Message.ToString();
                dialog.HideLoading();
            }

        }
        public async void editphno()
        {
            var connected = CrossConnectivity.Current.IsConnected;
            bool phvalidate = true;
            if (connected == true)
            {
                var currentpage = GetCurrentPage();
                if (string.IsNullOrEmpty(Mobileno))
                {
                    phvalidate = false;
                    dialog.Toast("Enter your mobile number");
                }
                else if (Mobileno.Length < 10)
                {
                    phvalidate = false;
                     dialog.Toast("Please enter a 10-digit mobile number");
                }
                else if (!CheckValidPhone(Mobileno))
                {
                    phvalidate = false;
                     dialog.Toast("Please enter valid mobile number");
                }
                if (phvalidate == true)
                {
                    dialog.ShowLoading("", MaskType.Black);
                    var nsAPI = RestService.For<ISignin>(Commonsettings.TechjobsAPI);
                    signupdetails list = await nsAPI.Editphno(Mobileno, Selectcountry,Commonsettings.UserDeviceId,Commonsettings.UserEmail);
                    if (list.otpno != "")
                    {
                        OTP = list.otpno;
                        dialog.HideLoading();
                        dialog.Toast("OTP has been sent to your mobile number");

                    }
                    else
                    {
                        dialog.HideLoading();
                        dialog.Toast("Please try again");
                    }
                    StackVisible = true;
                    Visible = false;
                }

            }
            else
                dialog.Toast("Kindly check your internet connection");


        }
        public async void opensignuppage()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Signup());
        }

        public async void openloginpage()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
        }

        public async void forgotpassword()
        {
            var currentpage = GetCurrentPage();
            await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Forgotpassword());
        }

        public async void skippage()
        {
            var currentpage = GetCurrentPage();
            try
            {
                await currentpage.Navigation.PopModalAsync();
            }catch(Exception e)
            {

            }
        }

        public async void sbmtforgotpass()
        {
            validate = true;
             if (UEmail.Trim().Length == 0)
            {
                validate = false;
                dialog.Toast("Please enter your email");
            }
            else if (!CheckValidEmail(UEmail))
            {
                validate = false;
                dialog.Toast("Please enter valid Email ID");
            }
             else if(validate==true)
            {
               // Visible = true;
                dialog.ShowLoading("", MaskType.Black);
                var currentpage = GetCurrentPage();
                var nsAPI = RestService.For<ISignin>(Commonsettings.TechjobsAPI);
                Forgotpass Passdetails = await nsAPI.Getpassword(UEmail);
                if (Passdetails.output== "sucess")
                {
                    // Visible = false;
                    dialog.HideLoading();
                   // var answer = await dialog.ConfirmAsync("", "Password has been sent to your mailid", "OK", "");
                    dialog.Alert("Password has been sent to your mail id", "", "OK");
                    await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                }
                   
                else
                {
                    //Visible = false;
                    dialog.HideLoading();
                    dialog.Toast("Email ID is not exist");
                }
                    
            }
        }
        private Page GetCurrentPage()
        {
            var currentPage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentPage;
        }
    }

}
