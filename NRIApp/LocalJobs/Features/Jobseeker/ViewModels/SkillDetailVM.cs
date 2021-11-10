using NRIApp.Helpers;
using NRIApp.LocalJobs.Features.Jobseeker.Interface;
using NRIApp.LocalJobs.Features.Jobseeker.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using Acr.UserDialogs;

namespace NRIApp.LocalJobs.Features.Jobseeker.ViewModels
{
    public class SkillDetailVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialogs = UserDialogs.Instance;
        public Command Taponexperience { get; set; }
        public Command ContentViewTap { get; set; }
        public Command PopupContentTap { get; set; }
        public Command clickratingcommand { get; set; }
        public Command savedetails { get; set; }
        public Command canceldtl { get; set; }
        public Command removedtl { get; set; }
        public Command<CParams> selectexperience { get; set; }
        Jobseekers_DATA data = new Jobseekers_DATA();
        public List<SKILLDATA> SkillsList { get; set; }
        public List<CParams> experienceyrslist = new List<CParams>()
        {
            new CParams{years="Freshers",yearsid=0,  checkimage="RadioButtonUnChecked.png"},
            new CParams{years="1 Year",yearsid=1, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="2 Years",yearsid=2, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="3 Years",yearsid=3, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="4 Years",yearsid=4, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="5 Years",yearsid=5, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="6 Years",yearsid=6, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="7 Years",yearsid=7, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="8 Years",yearsid=8, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="9 Years",yearsid=9, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="10 Years",yearsid=10, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="11 Years",yearsid=11, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="12 Years",yearsid=12, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="13 Years",yearsid=13, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="14 Years",yearsid=14, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="15 Years",yearsid=15, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="16 Years",yearsid=16, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="17 Years",yearsid=17, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="18 Years",yearsid=18, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="19 Years",yearsid=19, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="20 Years",yearsid=20, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="21 Years",yearsid=21, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="22 Years",yearsid=22, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="23 Years",yearsid=23, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="24 Years",yearsid=24, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="25 Years",yearsid=25, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="26 Years",yearsid=26, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="27 Years",yearsid=27, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="28 Years",yearsid=28, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="29 Years",yearsid=29, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="30 Years",yearsid=30, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="31 Years",yearsid=31, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="32 Years",yearsid=32, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="33 Years",yearsid=33, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="34 Years",yearsid=34, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="35 Years",yearsid=35, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="36 Years",yearsid=36, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="37 Years",yearsid=37, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="38 Years",yearsid=38, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="39 Years",yearsid=39, checkimage="RadioButtonUnChecked.png"},
            new CParams{years="40 Years",yearsid=40, checkimage="RadioButtonUnChecked.png"},
            //new CParams{years="40+ Years",yearsid=40, checkimage="RadioButtonUnChecked.png"},
        };
        private string _experienceyrtxt = "Select";
        public string experienceyrtxt
        {
            get { return _experienceyrtxt; }
            set { _experienceyrtxt = value; OnPropertyChanged(nameof(experienceyrtxt)); }
        }
        private int _experienceyrtxtID;
        public int experienceyrtxtID
        {
            get { return _experienceyrtxtID; }
            set { _experienceyrtxtID = value; OnPropertyChanged(nameof(experienceyrtxtID)); }
        }
        private bool _contentviewvisible = false;
        public bool contentviewvisible
        {
            get { return _contentviewvisible; }
            set { _contentviewvisible = value; OnPropertyChanged(nameof(contentviewvisible)); }
        }
        private bool _experiencelistviewvisble = false;
        public bool experiencelistviewvisble
        {
            get { return _experiencelistviewvisble; }
            set { _experiencelistviewvisble = value; OnPropertyChanged(nameof(experiencelistviewvisble)); }
        }
        private string _rating1 = "RatingColorGray1.png";
        public string rating1
        {
            get { return _rating1; }
            set { _rating1 = value;OnPropertyChanged(nameof(rating1)); }
        }
        private string _rating2 = "RatingColorGray2.png";
        public string rating2
        {
            get { return _rating2; }
            set { _rating2 = value; OnPropertyChanged(nameof(rating2)); }
        }
        private string _rating3 = "RatingColorGray3.png";
        public string rating3
        {
            get { return _rating3; }
            set { _rating3 = value; OnPropertyChanged(nameof(rating3)); }
        }
        private string _rating4 = "RatingColorGray4.png";
        public string rating4
        {
            get { return _rating4; }
            set { _rating4 = value; OnPropertyChanged(nameof(rating4)); }
        }
        private string _rating5 = "RatingColorGray5.png";
        public string rating5
        {
            get { return _rating5; }
            set { _rating5 = value; OnPropertyChanged(nameof(rating5)); }
        }
        private string _rating6 = "RatingColorGray6.png";
        public string rating6
        {
            get { return _rating6; }
            set { _rating6 = value; OnPropertyChanged(nameof(rating6)); }
        }
        private string _rating7 = "RatingColorGray7.png";
        public string rating7
        {
            get { return _rating7; }
            set { _rating7 = value; OnPropertyChanged(nameof(rating7)); }
        }
        private string _rating8 = "RatingColorGray8.png";
        public string rating8
        {
            get { return _rating8; }
            set { _rating8 = value; OnPropertyChanged(nameof(rating8)); }
        }
        private string _rating9 = "RatingColorGray9.png";
        public string rating9
        {
            get { return _rating9; }
            set { _rating9 = value; OnPropertyChanged(nameof(rating9)); }
        }
        private string _rating10 = "RatingColorGray10.png";
        public string rating10
        {
            get { return _rating10; }
            set { _rating10 = value; OnPropertyChanged(nameof(rating10)); }
        }
        private string _rating = "";
        public string rating
        {
            get { return _rating; }
            set { _rating = value; OnPropertyChanged(nameof(rating)); }
        }
        private string _Ratingbar = "";
        public string Ratingbar
        {
            get { return _Ratingbar; }
            set { _Ratingbar = value; OnPropertyChanged(nameof(Ratingbar)); }
        }
        private string _skilltext = "";
        public string skilltext
        {
            get { return _skilltext; }
            set { _skilltext = value; OnPropertyChanged(nameof(skilltext)); }
        }
        private bool _removebtnvisible = false;
        public bool removebtnvisible
        {
            get { return _removebtnvisible; }
            set { _removebtnvisible = value; OnPropertyChanged(nameof(removebtnvisible)); }
        }
        private string _skillcontentID = "";
        public string skillcontentID
        {
            get { return _skillcontentID; }
            set { _skillcontentID = value;OnPropertyChanged(nameof(skillcontentID)); }
        }
        public SkillDetailVM(Jobseekers_DATA Sdata)
        {
            data = Sdata;
            getskilldatas(Sdata);

            Taponexperience = new Command(showexperiencelist);
            ContentViewTap = new Command(Closecontentview);
            PopupContentTap = new Command(showcontentview);
            clickratingcommand = new Command(clickoverallrating);
            savedetails = new Command(saveskillsdetails);
            selectexperience = new Command<CParams>(SelectexperienceyrsID);
            canceldtl = new Command(cancel);
            removedtl = new Command(removeskilldetail);
        }

        public async void getskilldatas(Jobseekers_DATA Sdata)
        {
            try
            {
                dialogs.ShowLoading("", null);
                if(!string.IsNullOrEmpty(Sdata.editskills))
                {
                    skilltext = Sdata.editskills;
                }
                if (!string.IsNullOrEmpty(Sdata.skillyearsofexperience))
                {
                    if(Sdata.skillyearsofexperience=="0")
                    {
                        experienceyrtxt = "Freshers";
                        experienceyrtxtID = 0;
                    }
                    else
                    {
                        experienceyrtxt = Sdata.skillyearsofexperience;
                        experienceyrtxtID = Convert.ToInt32(Sdata.skillyearsofexperience.Replace("Years",""));
                    }
                    var curntpg = GetCurrentpage();
           
                }
                if (!string.IsNullOrEmpty(Sdata.skillrating))
                {
                    getrating(Sdata.skillrating.Replace("/10", ""));
                }
                if (!string.IsNullOrEmpty(Sdata.skillcontentid))
                {
                    removebtnvisible = true;
                    skillcontentID = Sdata.skillcontentid;
                }
                var skilldataapi = RestService.For<IJobseeker>(Commonsettings.LocaljobsAPI);
                var skilldatas = await skilldataapi.fillskillfields(Sdata.contentid);
                if (skilldatas != null)
                {
                    SkillsList = skilldatas.ROW_DATA;
                }


                dialogs.HideLoading();
            }
            catch(Exception ex)
            {
                dialogs.HideLoading();
            }
            
        }
        public void Closecontentview()
        {
            contentviewvisible = false;
        }
        public void showcontentview()
        {
            contentviewvisible = true;
        }
        public void showexperiencelist()
        {
            try
            {
                var curentpage = GetCurrentpage();
                var experiencerange = curentpage.FindByName<ListView>("experiencerange");
                foreach (var data in experienceyrslist)
                {
                    if (experienceyrtxt == data.years)
                    {
                        data.checkimage = "RadioButtonChecked.png";
                        experienceyrtxt = data.years.ToString();
                        experienceyrtxtID = data.yearsid;
                    }
                    else
                    {
                        data.checkimage = "RadioButtonUnChecked.png";
                    }
                }
                experiencerange.ItemsSource = null;
                experiencerange.ItemsSource = experienceyrslist;
                contentviewvisible = true;
                experiencelistviewvisble = true;
            }
            catch(Exception ex)
            {

            }
        }
        public void SelectexperienceyrsID(CParams c_param)
        {
            var curntpg = GetCurrentpage();
            Label experienceyrtxtclr = curntpg.FindByName<Label>("experienceyrtxt");
            experienceyrtxtclr.TextColor = Color.FromHex("#212121");
            experienceyrtxt = c_param.years;
            experienceyrtxtID = c_param.yearsid;
            contentviewvisible = false;
        }
        public void getrating (string id)
        {
            if (id == "1")
            {
                rating1 = "RatingColor1.png";
                rating2 = "RatingColorGray2";
                rating3 = "RatingColorGray3";
                rating4 = "RatingColorGray4";
                rating5 = "RatingColorGray5";
                rating6 = "RatingColorGray6";
                rating7 = "RatingColorGray7";
                rating8 = "RatingColorGray8";
                rating9 = "RatingColorGray9";
                rating10 = "RatingColorGray10";
                rating = "1";
                Ratingbar = "1/10";
            }
            else if (id == "2")
            {
                rating1 = "RatingColor1.png";
                rating2 = "RatingColor2.png";
                rating3 = "RatingColorGray3";
                rating4 = "RatingColorGray4";
                rating5 = "RatingColorGray5";
                rating6 = "RatingColorGray6";
                rating7 = "RatingColorGray7";
                rating8 = "RatingColorGray8";
                rating9 = "RatingColorGray9";
                rating10 = "RatingColorGray10";
                rating = "2";
                Ratingbar = "2/10";
            }
            else if (id == "3")
            {
                rating1 = "RatingColor1.png";
                rating2 = "RatingColor2.png";
                rating3 = "RatingColor3.png";
                rating4 = "RatingColorGray4";
                rating5 = "RatingColorGray5";
                rating6 = "RatingColorGray6";
                rating7 = "RatingColorGray7";
                rating8 = "RatingColorGray8";
                rating9 = "RatingColorGray9";
                rating10 = "RatingColorGray10";
                rating = "3";
                Ratingbar = "3/10";
            }
            else if (id == "4")
            {
                rating1 = "RatingColor1.png";
                rating2 = "RatingColor2.png";
                rating3 = "RatingColor3.png";
                rating4 = "RatingColor4.png";
                rating5 = "RatingColorGray5";
                rating6 = "RatingColorGray6";
                rating7 = "RatingColorGray7";
                rating8 = "RatingColorGray8";
                rating9 = "RatingColorGray9";
                rating10 = "RatingColorGray10";
                rating = "4";
                Ratingbar = "4/10";
            }
            else if (id == "5")
            {
                rating1 = "RatingColor1.png";
                rating2 = "RatingColor2.png";
                rating3 = "RatingColor3.png";
                rating4 = "RatingColor4.png";
                rating5 = "RatingColor5.png";
                rating6 = "RatingColorGray6";
                rating7 = "RatingColorGray7";
                rating8 = "RatingColorGray8";
                rating9 = "RatingColorGray9";
                rating10 = "RatingColorGray10";
                rating = "5";
                Ratingbar = "5/10";
            }
            else if (id == "6")
            {
                rating1 = "RatingColor1.png";
                rating2 = "RatingColor2.png";
                rating3 = "RatingColor3.png";
                rating4 = "RatingColor4.png";
                rating5 = "RatingColor5.png";
                rating6 = "RatingColor6.png";
                rating7 = "RatingColorGray7";
                rating8 = "RatingColorGray8";
                rating9 = "RatingColorGray9";
                rating10 = "RatingColorGray10";
                rating = "6";
                Ratingbar = "6/10";
            }
            else if (id == "7")
            {
                rating1 = "RatingColor1.png";
                rating2 = "RatingColor2.png";
                rating3 = "RatingColor3.png";
                rating4 = "RatingColor4.png";
                rating5 = "RatingColor5.png";
                rating6 = "RatingColor6.png";
                rating7 = "RatingColor7.png";
                rating8 = "RatingColorGray8";
                rating9 = "RatingColorGray9";
                rating10 = "RatingColorGray10";
                rating = "7";
                Ratingbar = "7/10";
            }
            else if (id == "8")
            {
                rating1 = "RatingColor1.png";
                rating2 = "RatingColor2.png";
                rating3 = "RatingColor3.png";
                rating4 = "RatingColor4.png";
                rating5 = "RatingColor5.png";
                rating6 = "RatingColor6.png";
                rating7 = "RatingColor7.png";
                rating8 = "RatingColor8.png";
                rating9 = "RatingColorGray9";
                rating10 = "RatingColorGray10";
                rating = "8";
                Ratingbar = "8/10";
            }
            else if (id == "9")
            {
                rating1 = "RatingColor1.png";
                rating2 = "RatingColor2.png";
                rating3 = "RatingColor3.png";
                rating4 = "RatingColor4.png";
                rating5 = "RatingColor5.png";
                rating6 = "RatingColor6.png";
                rating7 = "RatingColor7.png";
                rating8 = "RatingColor8.png";
                rating9 = "RatingColor9.png";
                rating10 = "RatingColorGray10";
                rating = "9";
                Ratingbar = "9/10";
            }
            else if (id == "10")
            {
                rating1 = "RatingColor1.png";
                rating2 = "RatingColor2.png";
                rating3 = "RatingColor3.png";
                rating4 = "RatingColor4.png";
                rating5 = "RatingColor5.png";
                rating6 = "RatingColor6.png";
                rating7 = "RatingColor7.png";
                rating8 = "RatingColor8.png";
                rating9 = "RatingColor9.png";
                rating10 = "RatingColor10.png";
                rating = "10";
                Ratingbar = "10/10";
            }
            else
            {
                rating1 = "RatingColorGray1.png";
                rating2 = "RatingColorGray2.png";
                rating3 = "RatingColorGray3.png";
                rating4 = "RatingColorGray4.png";
                rating5 = "RatingColorGray5.png";
                rating6 = "RatingColorGray6.png";
                rating7 = "RatingColorGray7.png";
                rating8 = "RatingColorGray8.png";
                rating9 = "RatingColorGray9.png";
                rating10 = "RatingColorGray10.png";
                rating = "0";
                id = "0";
                Ratingbar = "0/10";
            }
            rating = id;
        }
        private void clickoverallrating(object sender)
        {
            try
            {
                var view = sender as Xamarin.Forms.Image;

                string id = view.ClassId.ToString();
                if (id == "1")
                {
                    rating1 = "RatingColor1.png";
                    rating2 = "RatingColorGray2.png";
                    rating3 = "RatingColorGray3.png";
                    rating4 = "RatingColorGray4.png";
                    rating5 = "RatingColorGray5.png";
                    rating6 = "RatingColorGray6.png";
                    rating7 = "RatingColorGray7.png";
                    rating8 = "RatingColorGray8.png";
                    rating9 = "RatingColorGray9.png";
                    rating10 = "RatingColorGray10.png";
                    rating = "1";
                    Ratingbar = "1/10";
                }
                else if (id == "2")
                {
                    rating1 = "RatingColor1.png";
                    rating2 = "RatingColor2.png";
                    rating3 = "RatingColorGray3";
                    rating4 = "RatingColorGray4";
                    rating5 = "RatingColorGray5";
                    rating6 = "RatingColorGray6";
                    rating7 = "RatingColorGray7";
                    rating8 = "RatingColorGray8";
                    rating9 = "RatingColorGray9";
                    rating10 = "RatingColorGray10";
                    rating = "2";
                    Ratingbar = "2/10";
                }
                else if (id == "3")
                {
                    rating1 = "RatingColor1.png";
                    rating2 = "RatingColor2.png";
                    rating3 = "RatingColor3.png";
                    rating4 = "RatingColorGray4";
                    rating5 = "RatingColorGray5";
                    rating6 = "RatingColorGray6";
                    rating7 = "RatingColorGray7";
                    rating8 = "RatingColorGray8";
                    rating9 = "RatingColorGray9";
                    rating10 = "RatingColorGray10";
                    rating = "3";
                    Ratingbar = "3/10";
                }
                else if (id == "4")
                {
                    rating1 = "RatingColor1.png";
                    rating2 = "RatingColor2.png";
                    rating3 = "RatingColor3.png";
                    rating4 = "RatingColor4.png";
                    rating5 = "RatingColorGray5";
                    rating6 = "RatingColorGray6";
                    rating7 = "RatingColorGray7";
                    rating8 = "RatingColorGray8";
                    rating9 = "RatingColorGray9";
                    rating10 = "RatingColorGray10";
                    rating = "4";
                    Ratingbar = "4/10";
                }
                else if (id == "5")
                {
                    rating1 = "RatingColor1.png";
                    rating2 = "RatingColor2.png";
                    rating3 = "RatingColor3.png";
                    rating4 = "RatingColor4.png";
                    rating5 = "RatingColor5.png";
                    rating6 = "RatingColorGray6";
                    rating7 = "RatingColorGray7";
                    rating8 = "RatingColorGray8";
                    rating9 = "RatingColorGray9";
                    rating10 = "RatingColorGray10";
                    rating = "5";
                    Ratingbar = "5/10";
                }
                else if (id == "6")
                {
                    rating1 = "RatingColor1.png";
                    rating2 = "RatingColor2.png";
                    rating3 = "RatingColor3.png";
                    rating4 = "RatingColor4.png";
                    rating5 = "RatingColor5.png";
                    rating6 = "RatingColor6.png";
                    rating7 = "RatingColorGray7";
                    rating8 = "RatingColorGray8";
                    rating9 = "RatingColorGray9";
                    rating10 = "RatingColorGray10";
                    rating = "6";
                    Ratingbar = "6/10";
                }
                else if (id == "7")
                {
                    rating1 = "RatingColor1.png";
                    rating2 = "RatingColor2.png";
                    rating3 = "RatingColor3.png";
                    rating4 = "RatingColor4.png";
                    rating5 = "RatingColor5.png";
                    rating6 = "RatingColor6.png";
                    rating7 = "RatingColor7.png";
                    rating8 = "RatingColorGray8";
                    rating9 = "RatingColorGray9";
                    rating10 = "RatingColorGray10";
                    rating = "7";
                    Ratingbar = "7/10";
                }
                else if (id == "8")
                {
                    rating1 = "RatingColor1.png";
                    rating2 = "RatingColor2.png";
                    rating3 = "RatingColor3.png";
                    rating4 = "RatingColor4.png";
                    rating5 = "RatingColor5.png";
                    rating6 = "RatingColor6.png";
                    rating7 = "RatingColor7.png";
                    rating8 = "RatingColor8.png";
                    rating9 = "RatingColorGray9";
                    rating10 = "RatingColorGray10";
                    rating = "8";
                    Ratingbar = "8/10";
                }
                else if (id == "9")
                {
                    rating1 = "RatingColor1.png";
                    rating2 = "RatingColor2.png";
                    rating3 = "RatingColor3.png";
                    rating4 = "RatingColor4.png";
                    rating5 = "RatingColor5.png";
                    rating6 = "RatingColor6.png";
                    rating7 = "RatingColor7.png";
                    rating8 = "RatingColor8.png";
                    rating9 = "RatingColor9.png";
                    rating10 = "RatingColorGray10";
                    rating = "9";
                    Ratingbar = "9/10";
                }
                else if (id == "10")
                {
                    rating1 = "RatingColor1.png";
                    rating2 = "RatingColor2.png";
                    rating3 = "RatingColor3.png";
                    rating4 = "RatingColor4.png";
                    rating5 = "RatingColor5.png";
                    rating6 = "RatingColor6.png";
                    rating7 = "RatingColor7.png";
                    rating8 = "RatingColor8.png";
                    rating9 = "RatingColor9.png";
                    rating10 = "RatingColor10.png";
                    rating = "10";
                    Ratingbar = "10/10";
                }
                else
                {
                    rating1 = "RatingColorGray1.png";
                    rating2 = "RatingColorGray2.png";
                    rating3 = "RatingColorGray3.png";
                    rating4 = "RatingColorGray4.png";
                    rating5 = "RatingColorGray5.png";
                    rating6 = "RatingColorGray6.png";
                    rating7 = "RatingColorGray7.png";
                    rating8 = "RatingColorGray8.png";
                    rating9 = "RatingColorGray9.png";
                    rating10 = "RatingColorGray10.png";
                    rating = "0";
                    id = "0";
                    Ratingbar = "0/10";
                }
                rating = id;
            }
            catch(Exception ex)
            {

            }
        }
        public bool validation()
        {
            bool result = true;
            if(string.IsNullOrEmpty(skilltext) || skilltext.Trim().Length == 0)
            {
                dialogs.Toast("Please enter skill");
                return false;
            }
            if (string.IsNullOrEmpty(experienceyrtxtID.ToString()))
            {
                dialogs.Toast("Please select Experience");
                return false;
            }
            return result;
        }
        public async void saveskillsdetails()
        {
            try
            {
                dialogs.ShowLoading("", null);
                if (validation())
                {
                    data.skillexperience = experienceyrtxtID.ToString();
                    data.resumeskills = skilltext;
                    data.hdnreviewrating = rating;
                    
                    data.block = "skills";
                    var profileupdata = RestService.For<IJobseeker>(Commonsettings.LocaljobsAPI);
                    var profiledatas = await profileupdata.profilepagecreate(data);
                    if (profiledatas != null && profiledatas.resultinformation == "inserted sucessfully")
                    {
                        // await currentpage.Navigation.PopAsync();
                        var currentpage = GetCurrentpage();
                        //currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                        //currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                        Xamarin.Forms.Page page1 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1];
                        Xamarin.Forms.Page page2 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 2];
                        currentpage.Navigation.RemovePage(page1);
                        currentpage.Navigation.RemovePage(page2);
                        await currentpage.Navigation.PushAsync(new Jobseeker.View.Jobseekerprofile(data));
                        //}
                    }
                    else if (profiledatas.resultinformation == "invalid")
                    {
                        dialogs.Toast("Skill already exits");
                    }
                    else if (profiledatas.resultinformation == "updated sucess" || profiledatas.resultinformation == "updated sucessfully")
                    {
                        //dialogs.Toast("Skill updated successfully");
                        var currentpage = GetCurrentpage();
                        //currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                        //currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                        //await currentpage.Navigation.PushAsync(new Jobseeker.View.Jobseekerprofile(data));
                        Xamarin.Forms.Page page1 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1];
                        Xamarin.Forms.Page page2 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 2];
                        currentpage.Navigation.RemovePage(page1);
                        currentpage.Navigation.RemovePage(page2);
                        await currentpage.Navigation.PushAsync(new Jobseeker.View.Jobseekerprofile(data));
                    }
                    else
                    {
                        dialogs.Toast("Try again later...");
                    }
                }
                dialogs.HideLoading();
              
            }
            catch (Exception ex)
            {

            }
        }
        public async void removeskilldetail()
        {
            try
            {
                var deletedataApi = RestService.For<IJobseeker>(Commonsettings.LocaljobsAPI);
                var deletedatas = await deletedataApi.deleteskilldata(skillcontentID);
                //if (deletedatas != null)
                //{
                var currentpage = GetCurrentpage();
                //currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                //currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                //await currentpage.Navigation.PushAsync(new Jobseeker.View.Jobseekerprofile(data));
                Xamarin.Forms.Page page1 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1];
                Xamarin.Forms.Page page2 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 2];
                currentpage.Navigation.RemovePage(page1);
                currentpage.Navigation.RemovePage(page2);
                await currentpage.Navigation.PushAsync(new Jobseeker.View.Jobseekerprofile(data));
                //}
                //else
                //{
                //    dialogs.Toast("Try again later...");
                //}
            }
            catch(Exception ex)
            {
                dialogs.Toast("Try again later...");
            }
        }
        public void cancel()
        {
            var currentpage = GetCurrentpage();
            currentpage.Navigation.PopAsync();
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page GetCurrentpage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}
