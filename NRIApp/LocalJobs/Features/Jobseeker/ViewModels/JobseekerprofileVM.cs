using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using NRIApp.LocalJobs.Features.Jobseeker.Interface;
using NRIApp.Helpers;
using Refit;
using Acr.UserDialogs;
using Plugin.Media;
using System.IO;
using Plugin.Media.Abstractions;
using NRIApp.LocalJobs.Features.Jobseeker.Models;
using System.Windows.Input;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Net;
using Plugin.FilePicker.Abstractions;
using Plugin.FilePicker;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using Xamarin.Forms.Xaml;

//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace NRIApp.LocalJobs.Features.Jobseeker.ViewModels
{
    public class JobseekerprofileVM:INotifyPropertyChanged
    {
        IUserDialogs dialog = UserDialogs.Instance;
        public event PropertyChangedEventHandler PropertyChanged;
        
        public FileData _filedata;
        private List<SKILLDATA> _skilllist;
        public List<SKILLDATA> skilllist
        {
            get { return _skilllist; }
            set { _skilllist = value;OnPropertyChanged(nameof(skilllist)); }
        }
        private List<EXPERIENCEDATA> _experiencelist;
        public List<EXPERIENCEDATA> experiencelist
        {
            get { return _experiencelist; }
            set { _experiencelist = value; OnPropertyChanged(nameof(experiencelist)); }
        }
        public Command contacteditTap { get; set; }
        public Command selectprofilephoto { get; set; }
        public Command selectjobrole { get; set; }
        public Command Rtitlecmd { get; set; }
        public Command Descriptioncmd { get; set; }
        public Command Professionalcmd { get; set; }
        public Command addskillstap { get; set; }
        public Command<SKILLDATA> editskillstap { get; set; }
        public Command<EXPERIENCEDATA> editexperiencetap { get; set; }
        public Command addexperiencetap { get; set; }
        public Command addpreferencetap { get; set; }
        public Command choosefile { get; set; }
        public Command uploadresume { get; set; }
        public Command Taptodownload { get; set; }
        public Command twittertap { get; set; }
        public Command facebooktap { get; set; }
        public Command websitetap { get; set; }
        public Command linkedintap { get; set; }
        


        private string _email="Email";
        public string email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(email)); }
        }
        private string _jobrole = "Job Role";
        public string jobrole
        {
            get { return _jobrole; }
            set { _jobrole = value; OnPropertyChanged(nameof(jobrole)); }
        }
        private string _resumetitle = "Enter your Resume title";
        public string resumetitle
        {
            get { return _resumetitle; }
            set { _resumetitle = value; OnPropertyChanged(nameof(resumetitle)); }
        }
        private string _profiledescription = "";
        public string profiledescription
        {
            get { return _profiledescription; }
            set { _profiledescription = value; OnPropertyChanged(nameof(profiledescription)); }
        }
        private bool _descriptionvisible=false;
        public bool descriptionvisible
        {
            get { return _descriptionvisible; }
            set { _descriptionvisible = value; OnPropertyChanged(nameof(descriptionvisible)); }
        }
        private double _profilecompletevalue;
        public double profilecompletevalue
        {
            get { return _profilecompletevalue; }
            set { _profilecompletevalue = value; OnPropertyChanged(nameof(profilecompletevalue)); }
        }
        private string _Profilecompletetxt = "";
        public string Profilecompletetxt
        {
            get { return _Profilecompletetxt; }
            set { _Profilecompletetxt = value; OnPropertyChanged(nameof(Profilecompletetxt)); }
        }
        private string _Profilepercentage = "";
        public string Profilepercentage
        {
            get { return _Profilepercentage; }
            set { _Profilepercentage = value; OnPropertyChanged(nameof(Profilepercentage)); }
        }
        private string _education = "";
        public string education
        {
            get { return _education; }
            set { _education = value; OnPropertyChanged(nameof(education)); }
        }
        private string _ismasterdegree = "";
        public string ismasterdegree
        {
            get { return _ismasterdegree; }
            set { _ismasterdegree = value; OnPropertyChanged(nameof(ismasterdegree)); }
        }
        private string _salaryrange = "";
        public string salaryrange
        {
            get { return _salaryrange; }
            set { _salaryrange = value; OnPropertyChanged(nameof(salaryrange)); }
        }
        private string _experience = "";
        public string experience
        {
            get { return _experience; }
            set { _experience = value; OnPropertyChanged(nameof(experience)); }
        }
        private string _functionalarea = "";
        public string functionalarea
        {
            get { return _functionalarea; }
            set { _functionalarea = value; OnPropertyChanged(nameof(functionalarea)); }
        }
        private string _employementtype = "";
        public string employementtype
        {
            get { return _employementtype; }
            set { _employementtype = value; OnPropertyChanged(nameof(employementtype)); }
        }
        private string _jobstatus = "";
        public string jobstatus
        {
            get { return _jobstatus; }
            set { _jobstatus = value; OnPropertyChanged(nameof(jobstatus)); }
        }
        private string _jobcity = "";
        public string jobcity
        {
            get { return _jobcity; }
            set { _jobcity = value; OnPropertyChanged(nameof(jobcity)); }
        }
        private string _jobalerts = "";
        public string jobalerts
        {
            get { return _jobalerts; }
            set { _jobalerts = value; OnPropertyChanged(nameof(jobalerts)); }
        }
        private string _hideprofile = "";
        public string hideprofile
        {
            get { return _hideprofile; }
            set { _hideprofile = value; OnPropertyChanged(nameof(hideprofile)); }
        }
        private string _hideprofiletxt = "";
        public string hideprofiletxt
        {
            get { return _hideprofiletxt; }
            set { _hideprofiletxt = value; OnPropertyChanged(nameof(hideprofiletxt)); }
        }


        private string _name = "Contact name";
        public string name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(name)); }
        }
        private string _countrycode = "+1";
        public string countrycode
        {
            get { return _countrycode; }
            set { _countrycode = value;OnPropertyChanged(nameof(countrycode)); }
        }
        private string _mobileno = "Phone";
        public string mobileno
        {
            get { return _mobileno; }
            set { _mobileno = value; OnPropertyChanged(nameof(mobileno)); }
        }
        private string _city = "City";
        public string city
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged(nameof(city)); }
        }
        private string _workauth = "Work authorization";
        public string workauth
        {
            get { return _workauth; }
            set { _workauth = value; OnPropertyChanged(nameof(workauth)); }
        }
        private bool _skillslistviewvisible =false;
        public bool skillslistviewvisible
        {
            get { return _skillslistviewvisible; }
            set { _skillslistviewvisible = value; OnPropertyChanged(nameof(skillslistviewvisible)); }
        }
        private bool _isvisblesalaryrange = false;
        public bool isvisblesalaryrange
        {
            get { return _isvisblesalaryrange; }
            set { _isvisblesalaryrange = value; OnPropertyChanged(nameof(isvisblesalaryrange)); }
        }
        private bool _resumefilenamevisible = false;
        public bool resumefilenamevisible
        {
            get { return _resumefilenamevisible; }
            set { _resumefilenamevisible = value; OnPropertyChanged(nameof(resumefilenamevisible)); }
        }
        private bool _resumedownloadisvisible = false;
        public bool resumedownloadisvisible
        {
            get { return _resumedownloadisvisible; }
            set { _resumedownloadisvisible = value; OnPropertyChanged(nameof(resumedownloadisvisible)); }
        }
        private bool _lastupdatedvisible = false;
        public bool lastupdatedvisible
        {
            get { return _lastupdatedvisible; }
            set { _lastupdatedvisible = value; OnPropertyChanged(nameof(lastupdatedvisible)); }
        }
        private string _resumefilename = "";
        public string resumefilename
        {
            get { return _resumefilename; }
            set { _resumefilename = value; OnPropertyChanged(nameof(resumefilename)); }
        }
        private string _lastupdated = "";
        public string lastupdated
        {
            get { return _lastupdated; }
            set { _lastupdated = value; OnPropertyChanged(nameof(lastupdated)); }
        }
        private string _profilepicture = "ProfileUserIcon.png"; 
        public string profilepicture
        {
            get { return _profilepicture; }
            set { _profilepicture = value; OnPropertyChanged(nameof(profilepicture)); }
        }
        private bool _twittervisible = false;
        public bool twittervisible
        {
            get { return _twittervisible; }
            set { _twittervisible = value; OnPropertyChanged(nameof(twittervisible)); }
        }
        private bool _facebookvisible = false;
        public bool facebookvisible
        {
            get { return _facebookvisible; }
            set { _facebookvisible = value; OnPropertyChanged(nameof(facebookvisible)); }
        }
        private bool _websitevisible = false;
        public bool websitevisible
        {
            get { return _websitevisible; }
            set { _websitevisible = value; OnPropertyChanged(nameof(websitevisible)); }
        }
        private bool _linkedinvisible = false;
        public bool linkedinvisible
        {
            get { return _linkedinvisible; }
            set { _linkedinvisible = value; OnPropertyChanged(nameof(linkedinvisible)); }
        }
        private bool _resumetitlevisible = false;
        public bool resumetitlevisible
        {
            get { return _resumetitlevisible; }
            set { _resumetitlevisible = value; OnPropertyChanged(nameof(resumetitlevisible)); }
        }
        private bool _educationvisible = false;
        public bool educationvisible
        {
            get { return _educationvisible; }
            set { _educationvisible = value; OnPropertyChanged(nameof(educationvisible)); }
        }
        private bool _masterdegreevisible = false;
        public bool masterdegreevisible
        {
            get { return _masterdegreevisible; }
            set { _masterdegreevisible = value; OnPropertyChanged(nameof(masterdegreevisible)); }
        }
        private bool _jobcityvisible = false;
        public bool jobcityvisible
        {
            get { return _jobcityvisible; }
            set { _jobcityvisible = value; OnPropertyChanged(nameof(jobcityvisible)); }
        }
        private bool _jobalertsvisible = false;
        public bool jobalertsvisible
        {
            get { return _jobalertsvisible; }
            set { _jobalertsvisible = value; OnPropertyChanged(nameof(jobalertsvisible)); }
        }
        private bool _hideprofilevisible = false;
        public bool hideprofilevisible
        {
            get { return _hideprofilevisible; }
            set { _hideprofilevisible = value; OnPropertyChanged(nameof(hideprofilevisible)); }
        }
        private bool _experiencevisible = false;
        public bool experiencevisible
        {
            get { return _experiencevisible; }
            set { _experiencevisible = value; OnPropertyChanged(nameof(experiencevisible)); }
        }
        private bool _functionalareavisible = false;
        public bool functionalareavisible
        {
            get { return _functionalareavisible; }
            set { _functionalareavisible = value; OnPropertyChanged(nameof(functionalareavisible)); }
        }
        private bool _employementtypevisible = false;
        public bool employementtypevisible
        {
            get { return _employementtypevisible; }
            set { _employementtypevisible = value; OnPropertyChanged(nameof(employementtypevisible)); }
        }

        private bool _jobstatusvisible = false;
        public bool jobstatusvisible
        {
            get { return _jobstatusvisible; }
            set { _jobstatusvisible = value; OnPropertyChanged(nameof(jobstatusvisible)); }
        }
        private string _jobresumeid = "";
        public string jobresumeid
        {
            get { return _jobresumeid; }
            set { _jobresumeid = value; OnPropertyChanged(nameof(jobresumeid)); }
        }
        private string _selectedfilename = "Select file";
        public string selectedfilename
        {
            get { return _selectedfilename; }
            set { _selectedfilename = value; OnPropertyChanged(nameof(selectedfilename)); }
        }
        private string _selectedfilenametxt = "";
        public string selectedfilenametxt
        {
            get { return _selectedfilenametxt; }
            set { _selectedfilenametxt = value; OnPropertyChanged(nameof(selectedfilenametxt)); }
        }
        private bool _Choosephotocontentviewvisible;
        public bool Choosephotocontentviewvisible
        {
            get { return _Choosephotocontentviewvisible; }
            set { _Choosephotocontentviewvisible = value; OnPropertyChanged(nameof(Choosephotocontentviewvisible)); }
        }
        public Command takeprofilephoto { get; set; }
        public Command ContentViewTap { get; set; }
        public Command PopupContentTap { get; set; }
        Jobseekers_DATA dataaddskill = new Jobseekers_DATA();
        Jobseekers_DATA jobseekerdata = new Jobseekers_DATA();
        public JobseekerprofileVM(Jobseekers_DATA data)
        {
            getjobprofile(data);
            
            contacteditTap = new Command(async () => await gotoprofile(data));
            selectprofilephoto = new Command(async () => await uploadprofilepic());
            Rtitlecmd = new Command(async () => await gotoresumetitle(data));
            Descriptioncmd = new Command(async () => await gotodescription(data));
            Professionalcmd = new Command(async () => await gotoprofesionaldetail(data));
            addskillstap = new Command(async () => await gotoskillsdtl(data));
            editskillstap = new Command<SKILLDATA>(gotoeditskillsdtl);
            editexperiencetap = new Command<EXPERIENCEDATA>(gotoeditexperiencedtl);
            addexperiencetap = new Command(async () => await gotoexperience(data));
            addpreferencetap = new Command(async () => await gotopreference(data));
            choosefile = new Command(Choosefile);
            //uploadresume = new Command(Upload);
            
            uploadresume = new Command(async () => await Upload());
            Taptodownload = new Command(downloadresume);
            twittertap = new Command(gototwitterlink);
            facebooktap = new Command(gotofacebooklink);
            websitetap = new Command(gotowebsitelink);
            linkedintap = new Command(gotolinkedinlink);

            // takeprofilephoto = new Command(async () => await takephoto());
            takeprofilephoto = new Command(takephoto);

            ContentViewTap = new Command(Closecontentview);
            PopupContentTap = new Command(showcontentview);
        }
        public void Closecontentview()
        {
            Choosephotocontentviewvisible = false;
        }
        public void showcontentview()
        {
            Choosephotocontentviewvisible = true;
        }
        public async void takephoto()
        {
            //reference https://github.com/jamesmontemagno/MediaPlugin
            try
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    var answer = await dialog.ConfirmAsync("No Camera", " No camera avaialble.", "OK", "");
                    //if (answer)
                    //{
                    //}
                    //await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                    return;
                }
                else
                {
                    //_mediaFile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    dialog.ShowLoading("", null);
                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                        Directory = "Sample",
                        Name = "test.jpg",
                        AllowCropping = true,
                        
                       
                    });

                    if (file == null)
                    {
                        dialog.HideLoading();
                        return;
                    }
                    else
                    {
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                            string URI = "https://localjobs.sulekha.com/mobileappresume/mobileappresume.aspx?type=photoupload";
                            byte[] HtmlResult = wc.UploadFile(URI, "POST", file.Path);
                            string responsebody = Encoding.UTF8.GetString(HtmlResult);
                            if (!string.IsNullOrEmpty(responsebody) && responsebody != "Unable to save image." && responsebody != "Invalid size")
                            {
                                //await currentpage.Navigation.PopAsync();
                                dataaddskill.photos = responsebody;
                                dataaddskill.block = "profile";
                                var profileupdata = RestService.For<IJobseeker>(Commonsettings.LocaljobsAPI);
                                var profiledatas = await profileupdata.profilepagecreate(dataaddskill);
                                file.Dispose();
                                var curntpg = GetCurrentpage();
                                curntpg.Navigation.RemovePage(curntpg.Navigation.NavigationStack[curntpg.Navigation.NavigationStack.Count - 1]);
                                await curntpg.Navigation.PushAsync(new Jobseeker.View.Jobseekerprofile(dataaddskill));
                            }
                            else if (responsebody == "Unable to save image.")
                            {
                                dialog.Toast("Unable to save image.");
                            }
                            else if (responsebody == "Invalid size")
                            {
                                dialog.Toast("Image format is not correct Size, so unable to upload.");
                            }
                            else
                            {
                                dialog.Toast("couldn't upload...try again later...");
                            }
                        }

                        await Task.Delay(200);
                        dialog.HideLoading();
                        //file path alert
                        //var answer1 = await dialog.ConfirmAsync("File Location", file.Path, "OK");
                        //var currentpage = GetCurrentpage();
                        //Image image = currentpage.FindByName<Image>("profilepictureimg");
                        //image.Source = ImageSource.FromStream(() =>
                        //{
                        //    var stream = _mediaFile.GetStream();

                        //    //   _mediaFile.Dispose();
                        //    // file.Dispose();     
                        //    return stream;
                        //});

                        // _mediaFile.Dispose(); //app crashes when taking photos -sometimes ?
                        //image.Source = image;
                    }
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
                dialog.Toast("Unable to save the file...Please try again later...");
            }
        }
        public void addskilljob(Jobseekers_DATA data)
        {
            dataaddskill = data;
        }
        public void gototwitterlink()
        {
            try
            {
                Device.OpenUri(new Uri(dataaddskill.twitter));
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
            
        }
        public void gotofacebooklink()
        {
            try
            {
                Device.OpenUri(new Uri(dataaddskill.facebook));
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
                dialog.Toast("Invalid url...Kindly update!");
            }
            
        }
        public void gotowebsitelink()
        {
            try
            {
                Device.OpenUri(new Uri(dataaddskill.blog));
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
            
        }
        public void gotolinkedinlink()
        {
            try
            {
                Device.OpenUri(new Uri(dataaddskill.linkedin));
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
            
        }
        public async void getjobprofile(Jobseekers_DATA data)
        {
            try
            {
                dialog.ShowLoading("", null);
                if (Commonsettings.UserEmail != null && Commonsettings.UserEmail != "")
                     email = Commonsettings.UserEmail;
                if (Commonsettings.UserName != null && Commonsettings.UserName != "")
                    name = Commonsettings.UserName;
                if (Commonsettings.UserMobileno != null && Commonsettings.UserMobileno != "")
                    mobileno = Commonsettings.UserMobileno;
                //if (Commonsettings.Usercity != null && Commonsettings.Usercity != "")
                //    city = Commonsettings.Usercity;
               
               // string profileid = "";
                var JSprofile = RestService.For<IJobseeker>(Commonsettings.LocaljobsAPI);
                var JSprofileres = await JSprofile.filljobprofile(email, data.contentid);
                await Task.Delay(500);
                data.contentid= JSprofileres.ROW_DATA.Last().contentid;
                data.city = JSprofileres.ROW_DATA.Last().city;
                data.cityurl = JSprofileres.ROW_DATA.Last().cityurl;
                data.jobrole = JSprofileres.ROW_DATA.Last().jobrole;
                data.resumetitle = JSprofileres.ROW_DATA.Last().resumetitle;
                data.profiledesc = JSprofileres.ROW_DATA.Last().profiledesc;
                data.fillcount = JSprofileres.ROW_DATA.Last().fillcount;
                data.fillcount = JSprofileres.ROW_DATA.Last().education;
                data.usmasterdegree = JSprofileres.ROW_DATA.Last().usmasterdegree;
                data.salaryfrom = JSprofileres.ROW_DATA.Last().salaryfrom;
                data.salaryto = JSprofileres.ROW_DATA.Last().salaryto;
                data.salarymode= JSprofileres.ROW_DATA.Last().salarymode;
                data.jobcity = JSprofileres.ROW_DATA.Last().jobcity;
                data.jobalerts = JSprofileres.ROW_DATA.Last().jobalerts;
                data.hideprofile = JSprofileres.ROW_DATA.Last().hideprofile;
                data.experience = JSprofileres.ROW_DATA.Last().experience;
                data.functionalarea = JSprofileres.ROW_DATA.Last().functionalarea;
                data.employementtype = JSprofileres.ROW_DATA.Last().employementtype;
                data.jobstatus = JSprofileres.ROW_DATA.Last().jobstatus;
                data.photos= JSprofileres.ROW_DATA.Last().photos;
                data.jobresumesid= JSprofileres.ROW_DATA.Last().jobresumesid;
                jobresumeid= JSprofileres.ROW_DATA.Last().jobresumesid.ToString();
                data.pid = Commonsettings.UserPid;
                data.jobcityurl = JSprofileres.ROW_DATA.Last().jobcityurl;
                data.linkedin = JSprofileres.ROW_DATA.Last().linkedin;
                data.facebook = JSprofileres.ROW_DATA.Last().facebook;
                data.twitter = JSprofileres.ROW_DATA.Last().twitter;
                data.blog = JSprofileres.ROW_DATA.Last().blog;
                data.isitskill = JSprofileres.ROW_DATA.Last().isitskill;
                data.levelcomplete = JSprofileres.ROW_DATA.Last().levelcomplete;
                if(!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().linkedin))
                {
                    linkedinvisible = true;
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().facebook))
                {
                    facebookvisible = true;
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().twitter))
                {
                    twittervisible = true;
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().blog))
                {
                    websitevisible = true;
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().photos))
                {
                    profilepicture = JSprofileres.ROW_DATA.Last().photos;
                    data.photos = JSprofileres.ROW_DATA.Last().photos;
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().workauthorization))
                {
                    workauth = JSprofileres.ROW_DATA.Last().workauthorization.Replace(",",", ");
                    data.workauthorization= JSprofileres.ROW_DATA.Last().workauthorization;
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().resumepath) || !string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().jobresumesid.ToString()))
                {
                    data.resumepath = JSprofileres.ROW_DATA.Last().resumepath;
                    resumefilenamevisible = true;
                    resumedownloadisvisible = true;
                    if(!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().lastupdated))
                    {
                        lastupdated = JSprofileres.ROW_DATA.Last().lastupdated;
                        lastupdatedvisible = true;
                    }
                    else
                    {
                        lastupdatedvisible = false;
                    }
                    if(!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().jobrole))
                    {
                        resumefilename = JSprofileres.ROW_DATA.Last().jobrole + ".doc";
                    }
                    else
                    {
                        resumefilename = "Resume.doc";
                    }
                    
                    
                }

                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().contactperson))
                {
                    name = JSprofileres.ROW_DATA.Last().contactperson;
                    data.contactperson = JSprofileres.ROW_DATA.Last().contactperson;
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().city))
                {
                    city = JSprofileres.ROW_DATA.Last().city;
                    data.city = JSprofileres.ROW_DATA.Last().city;
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().email))
                {
                    email = JSprofileres.ROW_DATA.Last().email;
                    data.email = JSprofileres.ROW_DATA.Last().email;
                }
               
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().phone))
                {
                    mobileno = JSprofileres.ROW_DATA.Last().phone;
                    data.phone= JSprofileres.ROW_DATA.Last().phone;
                }
              
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().jobrole))
                {
                    jobrole = JSprofileres.ROW_DATA.Last().jobrole;
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().resumetitle))
                {
                    resumetitle = JSprofileres.ROW_DATA.Last().resumetitle;
                    resumetitlevisible = true;
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().profiledesc))
                {
                    descriptionvisible = true;
                    profiledescription = JSprofileres.ROW_DATA.Last().profiledesc;
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().fillcount))
                {
                    int filcnt = int.Parse(JSprofileres.ROW_DATA.Last().fillcount);
                    if(filcnt<=5)
                    {
                        profilecompletevalue = 0.2;
                        Profilecompletetxt = "Profile Strength (Low)";
                        Profilepercentage = "20%";
                    }
                    else if (filcnt > 5 && filcnt <= 10)
                    {
                        profilecompletevalue = 0.4;
                        Profilecompletetxt = "Profile Strength (Medium)";
                        Profilepercentage = "40%";
                    }
                    else if (filcnt > 10 && filcnt <= 15)
                    {
                        profilecompletevalue = 0.6;
                        Profilecompletetxt = "Profile Strength (Good)";
                        Profilepercentage = "60%";
                    }
                    else if (filcnt > 15 && filcnt <= 20)
                    {
                        profilecompletevalue = 0.75;
                        Profilecompletetxt = "Profile Strength (Good)";
                        Profilepercentage = "75%";
                    }
                    else if (filcnt > 20 && filcnt <= 25)
                    {
                        profilecompletevalue = 0.85;
                        Profilecompletetxt = "Profile Strength (Very Good)";
                        Profilepercentage = "85%";
                    }
                    else if (filcnt > 25 && filcnt <= 30)
                    {
                        profilecompletevalue = 0.95;
                        Profilecompletetxt = "Profile Strength (Completed)";
                        Profilepercentage = "95%";
                    }
                    else if (filcnt > 30)
                    {
                        profilecompletevalue = 1;
                        Profilecompletetxt = "Profile Strength (Completed)";
                        Profilepercentage = "100%";
                    }
                }
                else
                {
                    profilecompletevalue = 2;
                    Profilecompletetxt = "Profile Strength (Low)";
                    Profilepercentage = "20%";
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().education))
                {
                    educationvisible = true;
                    education = JSprofileres.ROW_DATA.Last().education;
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().usmasterdegree.ToString()))
                {
                    masterdegreevisible = true;
                    ismasterdegree = JSprofileres.ROW_DATA.Last().usmasterdegree.ToString();
                    if (ismasterdegree == "0")
                    {
                        ismasterdegree = "No";
                    }
                    else
                    {
                        ismasterdegree = "Yes";
                    }
                }
                    
                JSprofileres.ROW_DATA.Last().salaryfrom = Math.Round(JSprofileres.ROW_DATA.Last().salaryfrom, 1);
                JSprofileres.ROW_DATA.Last().salaryto = Math.Round(JSprofileres.ROW_DATA.Last().salaryto, 1);
                if (Math.Round(JSprofileres.ROW_DATA.Last().salaryfrom, 1) == 0 && Math.Round(JSprofileres.ROW_DATA.Last().salaryto, 1) == 0)
                {
                    isvisblesalaryrange = false;
                }
                else
                {
                    isvisblesalaryrange = true;
                    if (JSprofileres.ROW_DATA.Last().salarymode == "182,Hourly")
                        JSprofileres.ROW_DATA.Last().salarymode = "Hour"; 
                    if (JSprofileres.ROW_DATA.Last().salarymode == "183,Daily")
                        JSprofileres.ROW_DATA.Last().salarymode = "Day";
                    if (JSprofileres.ROW_DATA.Last().salarymode == "184,Weekly")
                        JSprofileres.ROW_DATA.Last().salarymode = "Week";
                    if (JSprofileres.ROW_DATA.Last().salarymode == "185,Monthly")
                        JSprofileres.ROW_DATA.Last().salarymode = "Month";
                    if (JSprofileres.ROW_DATA.Last().salarymode == "192,Yearly")
                        JSprofileres.ROW_DATA.Last().salarymode = "Year";

                   // JSprofileres.ROW_DATA.Last().salarymode =   data.salarymode;
                    if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().salarymode))
                    {
                        salaryrange = "$" + JSprofileres.ROW_DATA.Last().salaryfrom + " - " + "$" + JSprofileres.ROW_DATA.Last().salaryto + " / " + JSprofileres.ROW_DATA.Last().salarymode;
                    }
                    else
                    {
                        salaryrange = "$" + JSprofileres.ROW_DATA.Last().salaryfrom + " - " + "$" + JSprofileres.ROW_DATA.Last().salaryto;
                    }
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().jobcity))
                {
                    jobcityvisible = true;
                    jobcity = JSprofileres.ROW_DATA.Last().jobcity;
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().jobalerts))
                {
                    jobalertsvisible = true;
                    jobalerts = JSprofileres.ROW_DATA.Last().jobalerts;
                }
                hideprofile = JSprofileres.ROW_DATA.Last().hideprofile;
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().hideprofile))
                {
                    hideprofilevisible = true;
                    if (JSprofileres.ROW_DATA.Last().hideprofile == "1")
                    {
                        hideprofiletxt = "Hide my profile to non-recruiters";
                    }
                    else if(JSprofileres.ROW_DATA.Last().hideprofile == "0")
                    {
                        hideprofiletxt = "Show my profile to everyone";
                    }
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().experience.ToString()))
                {
                    experiencevisible = true;
                    if(data.experience==0)
                    {
                        experience = "Freshers";
                    }
                    else
                    {
                        experience = JSprofileres.ROW_DATA.Last().experience.ToString();
                    }
                    
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().functionalarea))
                {
                    functionalareavisible = true;
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().employementtype))
                {
                    employementtypevisible = true;
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().jobstatus))
                {
                    jobstatusvisible = true;
                }
                if(!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().functionalarea))
                {
                    functionalarea = JSprofileres.ROW_DATA.Last().functionalarea;
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().employementtype))
                {
                    employementtype = JSprofileres.ROW_DATA.Last().employementtype;
                }
                if (!string.IsNullOrEmpty(JSprofileres.ROW_DATA.Last().jobstatus))
                {
                    jobstatus = JSprofileres.ROW_DATA.Last().jobstatus;
                }




                

                getskillsdata(JSprofileres.ROW_DATA.Last().contentid);
                getexperiencedata(JSprofileres.ROW_DATA.Last().contentid);
                jobseekerdata = data;
                addskilljob(data);
                await Task.Delay(3000);

                dialog.HideLoading();
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
                string msg = ex.Message;
            }
        }
        public async Task gotoprofile(Jobseekers_DATA Sdata)
        {
            var currentpage = GetCurrentpage();
            await currentpage.Navigation.PushAsync(new Jobseeker.View.ProfileDetails(Sdata));
        }
        public async Task gotoresumetitle(Jobseekers_DATA Sdata)
        {
            var currentpage = GetCurrentpage();
            await currentpage.Navigation.PushAsync(new Jobseeker.View.ResumeTitle(Sdata));
        }
        public async Task gotodescription(Jobseekers_DATA Sdata)
        {
            var currentpage = GetCurrentpage();
            await currentpage.Navigation.PushAsync(new Jobseeker.View.ProfileDescription(Sdata));
        }
        public async Task gotoprofesionaldetail(Jobseekers_DATA Sdata)
        {
            var currentpage = GetCurrentpage();
            await currentpage.Navigation.PushAsync(new Jobseeker.View.ProfessionalDetails(Sdata));
        }
        public async Task gotoskillsdtl(Jobseekers_DATA Sdata)
        {
            try
            {
                Sdata.skillcontentid = "";
                //dataaddskill.jobprofileid = Sdata.jobprofileid;
                Sdata.editskills = "";
                Sdata.skillyearsofexperience = "";
                Sdata.skillrating = "";
                var currentpage = GetCurrentpage();
                await currentpage.Navigation.PushAsync(new Jobseeker.View.SkillDetail(Sdata));

            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void gotoeditskillsdtl(SKILLDATA Sdata)
        {
           try
            {
                dataaddskill.skillcontentid = Sdata.contentid;
                //dataaddskill.jobprofileid = Sdata.jobprofileid;
                dataaddskill.editskills = Sdata.skills;
                dataaddskill.skillyearsofexperience = Sdata.yearsofexperience;
                dataaddskill.skillrating = Sdata.rating;
                var currentpage = GetCurrentpage();
                await currentpage.Navigation.PushAsync(new Jobseeker.View.SkillDetail(dataaddskill));
            }
            catch(Exception ex)
            {

            }
        }
        public async void gotoeditexperiencedtl(EXPERIENCEDATA Sdata)
        {
            try
            {
                dataaddskill.txtcmyname = Sdata.jobcompany;
                dataaddskill.txtrole = Sdata.designation;
                dataaddskill.frmmonthnametxt = Sdata.frmmonthnametxt;
                dataaddskill.frmmonth = Sdata.frmmonth.ToString();
                dataaddskill.frmyear = Sdata.frmyear.ToString();
                dataaddskill.tomonth = Sdata.tomonth.ToString();
                dataaddskill.tomonthname = Sdata.tomonthname.ToString();
                dataaddskill.toyear = Sdata.toyear.ToString();
                dataaddskill.expcontentid = Sdata.contentid.ToString();
                dataaddskill.txtareadescrip = Sdata.description.ToString();
                var currentpage = GetCurrentpage();
                await currentpage.Navigation.PushAsync(new Jobseeker.View.ExperienceDetails(dataaddskill));
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async Task gotoexperience(Jobseekers_DATA Sdata)
        {
            Sdata.txtcmyname = "";
            Sdata.txtrole = "";
            Sdata.frmmonthnametxt = "";
            Sdata.frmmonth = "";
            Sdata.frmyear = "";
            Sdata.tomonth = "";
            Sdata.tomonthname = "";
            Sdata.toyear = "";
            Sdata.expcontentid = "";
            Sdata.txtareadescrip = "";
            var currentpage = GetCurrentpage();
            await currentpage.Navigation.PushAsync(new Jobseeker.View.ExperienceDetails(Sdata));
        }
        public async Task gotopreference(Jobseekers_DATA Sdata)
        {
            var currentpage = GetCurrentpage();
            await currentpage.Navigation.PushAsync(new Jobseeker.View.PreferenceDetails(Sdata));
        }
        public async void getskillsdata(int profileid)
        {
            try
            {
                if(!string.IsNullOrEmpty(profileid.ToString()))
                {
                    var skillapi = RestService.For<IJobseeker>(Commonsettings.LocaljobsAPI);
                    var skilldata = await skillapi.fillskillfields(profileid);
                    if(skilldata!=null&& skilldata.ROW_DATA.Count>0)
                    {
                        skillslistviewvisible = true;
                        foreach (var data in skilldata.ROW_DATA)
                        {
                            data.rating = data.rating + "/10".ToString();
                            data.yearsofexperience = data.yearsofexperience + " Years";
                        }
                        skilllist = skilldata.ROW_DATA;
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
        public async void getexperiencedata(int profileid)
        {
            try
            {
                if (!string.IsNullOrEmpty(profileid.ToString()))
                {
                    var experienceapi = RestService.For<IJobseeker>(Commonsettings.LocaljobsAPI);
                    var experiencedata = await experienceapi.fillexperiencefield(profileid);
                    if (experiencedata != null && experiencedata.ROW_DATA.Count>0)
                    {
                        //wat abt tilldate 
                        foreach (var data in experiencedata.ROW_DATA)
                        {
                            data.frmmonthnametxt = data.frmmonthname;
                            data.frmmonthname = data.frmmonthname + " " + data.frmyear + " to " + data.tomonthname + " " + data.toyear;
                        }
                        experiencelist = experiencedata.ROW_DATA;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        private Plugin.Media.Abstractions.MediaFile _mediaFile;
        public static string profilepicurl = "";

        public async Task uploadprofilepic()
        {
            try
            {
                
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    dialog.Toast("This is not support on your device.");
                    return;
                }
                else
                {
                    dialog.ShowLoading("", null);
                 
                    _mediaFile = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions()).ConfigureAwait(true);
                   // String URI = "http://localjobs.sulekha.com/mobileappresume/mobileappresume.aspx?type=resumelistingupload&name=" + name + "&mobileno=" + mobileno + "&code=" + countrycode + "&email=" + email + "&jobrole=" + jobrole;
                    string URI = "https://localjobs.sulekha.com/mobileappresume/mobileappresume.aspx?type=photoupload";
                    
                    if (string.IsNullOrEmpty(_mediaFile.Path))
                    {
                        dialog.HideLoading();
                        dialog.Toast("Select file then upload");
                    }
                    else
                    {
                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                            byte[] HtmlResult = wc.UploadFile(URI, "POST", _mediaFile.Path);
                            string responsebody = Encoding.UTF8.GetString(HtmlResult);
                            if (!string.IsNullOrEmpty(responsebody) && responsebody != "Unable to save image." && responsebody != "Invalid size" && responsebody != "Unable to proceess the uploadimage request")
                            {
                                //await currentpage.Navigation.PopAsync();
                                dataaddskill.photos = responsebody;
                                dataaddskill.block = "profile";
                                var profileupdata = RestService.For<IJobseeker>(Commonsettings.LocaljobsAPI);
                                var profiledatas = await profileupdata.profilepagecreate(dataaddskill);
                                var currentpage = GetCurrentpage();
                                //  currentpage.Navigation.RemovePage(currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1]);
                                Xamarin.Forms.Page page1 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1];
                                currentpage.Navigation.RemovePage(page1);
                                await currentpage.Navigation.PushAsync(new Jobseeker.View.Jobseekerprofile(dataaddskill));
                            }
                            else if (responsebody == "Unable to save image.")
                            {
                                dialog.Toast("Unable to save image.");
                            }
                            else if(responsebody== "Unable to proceess the uploadimage request")
                            {
                                dialog.Toast("Unable to proceess the uploadimage request...Kindly select from some other folders!");
                            }
                            else if (responsebody == "Invalid size")
                            {
                                dialog.Toast("Image format is not correct Size, so unable to upload.");
                            }
                            else
                            {
                                dialog.Toast("couldn't upload...try again later...");
                            }
                        }
                        
                    }
                    dialog.HideLoading();
                }
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        string extension = "";
        public async void Choosefile()
        {
            try
            {
              if (!CrossMedia.Current.IsPickPhotoSupported)
              {
                  dialog.Toast("This is not support on your device.");
                  return;
               }
              else
                {
                    _filedata = await CrossFilePicker.Current.PickFile();
                    if (!string.IsNullOrEmpty(_filedata.FilePath))
                    {
                        FileInfo fi = new FileInfo(_filedata.FilePath);
                        selectedfilename = _filedata.FileName;
                        selectedfilenametxt = _filedata.FileName;
                        extension = Path.GetExtension(_filedata.FileName);
                        if (extension.ToLower() == ".doc" || extension.ToLower() == ".docx" || extension.ToLower() == ".pdf")
                        {
                            selectedfilename = _filedata.FileName;
                        }
                        else
                        {
                            dialog.ShowLoading("");
                            selectedfilename = "Select file";
                            await System.Threading.Tasks.Task.Delay(500);
                            var currentpage = GetCurrentpage();
                            await currentpage.DisplayAlert("Alert", "Select doc,docx,pdf files only", "", "Ok");
                            dialog.HideLoading();
                            //dialog.Toast("Error");
                            // dialog.Alert("Select doc,docx,pdf files only","","ok");
                        }
                    }
                }
               
                
                //string extension = fi.Extension;

            }
            catch (Exception ex)
            {
                dialog.HideLoading();
                // string msg = e.Message;
                //dialog.Toast("select file from file manager then upload");
               // await App.Current.MainPage.DisplayAlert("Alert", "Some error occurred, try again.", "Ok");
                //await App.Current.MainPage.DisplayAlert("Alert", "Permission Denied. Can not continue, try again.", "Ok");
            }

        }
       bool fileuploadvalidation()
        {
            bool result = true;
            //if (string.IsNullOrEmpty(mobileno))
            //{
            //    dialog.Toast("Please enter mobileno(Click edit icon to enter the fields)");
            //    result = false;
            //    return false;
            //}
            if (mobileno.Length < 10)
            {
                dialog.Toast("Please enter a 10-digit mobile number");
                result = false;
                return false;
            }
            if (!CheckValidPhone(mobileno))
            {
                dialog.Toast("Please enter valid mobile number");
                result = false;
                return false;
            }

            return result;
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
        public async Task Upload()
        {
            try
            {
                if(string.IsNullOrEmpty(mobileno) || mobileno.Trim().Length==0 || mobileno== "Phone")
                {
                    var currentpg = GetCurrentpage();
                  //  var confirmasync = dialog.Alert("Alert", "Enter the mobile number to upload resume!", "Ok");
                    var answer = await dialog.ConfirmAsync("Enter the mobile number to upload resume!", "Alert", "Ok", "Cancel");
                    if(answer)
                    {
                        await currentpg.Navigation.PushAsync(new LocalJobs.Features.Jobseeker.View.ProfileDetails(jobseekerdata));
                    }
                }
                else
                {
                    var currentpage = GetCurrentpage();
                    if (fileuploadvalidation())
                    {
                        dialog.ShowLoading("", null);
                        await Task.Delay(100);

                        // String URI = "https://techjobs.sulekha.com/mobileapp/localjobs/resume.aspx?type=resumelistingupload&name=" + name + "&mobileno=" + mobileno + "&code=" + countrycode + "&email=" + email + "&jobrole=" + jobrole;

                        if (string.IsNullOrEmpty(selectedfilenametxt))
                        {
                            dialog.Toast("Select file then upload");
                            dialog.HideLoading();
                        }
                        else
                        {
                            if (Commonsettings.UserPid == "0" || Commonsettings.UserPid == null || Commonsettings.UserPid == "")
                            {

                                await currentpage.Navigation.PushModalAsync(new NRIApp.Signin.Views.Login());
                            }
                            else
                            {
                                String URI = "https://localjobs.sulekha.com/mobileappresume/mobileappresume.aspx?type=resumelistingupload&name=" + name + "&mobileno=" + mobileno + "&code=" + countrycode + "&email=" + email + "&jobrole=" + jobrole;
                                string FileName = "";
                                if (Commonsettings.UserMobileOS.ToLower() == "android")
                                {
                                    var path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
                                    var filename = Path.Combine(path.ToString(), _filedata.FileName);
                                    FileName = filename;
                                }
                                else
                                {
                                    FileName = _filedata.FilePath;
                                    // var path =  Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
                                    // //var iospath = System.IO.Directory.GetDirectoryRoot(Environment.CurrentDirectory);
                                    // var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                                    // var filename = Path.Combine(documents, _filedata.FileName);
                                    //// var filename = Path.Combine(path.ToString(), _filedata.FileName);
                                    // FileName = filename;
                                }

                                // var path =  Environment.get GetExternalStoragePublicDirectory(Environment.DirectoryDownloads);

                                using (WebClient wc = new WebClient())
                                {
                                    wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                                    byte[] HtmlResult = wc.UploadFile(URI, "POST", FileName);
                                    //byte[] HtmlResult = wc.UploadFile(URI, FileName);
                                    if (HtmlResult != null)
                                    {
                                        string responsebody = Encoding.UTF8.GetString(HtmlResult);
                                        if (!string.IsNullOrEmpty(responsebody))
                                        {
                                            dialog.Toast("Uploaded Successfully");
                                        }
                                        resumedownloadisvisible = true;
                                        resumefilename = "Resume.doc";

                                        //uploadsuccess
                                        Xamarin.Forms.Page page1 = currentpage.Navigation.NavigationStack[currentpage.Navigation.NavigationStack.Count - 1];
                                        currentpage.Navigation.RemovePage(page1);
                                        await currentpage.Navigation.PushAsync(new Jobseeker.View.Jobseekerprofile(jobseekerdata));
                                    }
                                    else
                                    {
                                        //mention mb(less than 3 MB)
                                        dialog.Toast("Files may not be in a correct format/ Size issues...Select some other file and try again");
                                    }
                                }
                            }
                        }
                        dialog.HideLoading();

                    }
                }
               
               
            }
            catch (Exception e)
            {
                string msg = e.Message;
                dialog.HideLoading();
                dialog.Toast("Files may not be in a correct format/ Size issues...Select some other file and try again");
            }
        }
        //public static void verifyStoragePermissions(Android.App.Activity activity)
        //{
        //    // Check if we have write permission
        //    int permission = Android.ActivityCompat.checkSelfPermission(activity, Android.Manifest.Permission.WriteExternalStorage);

        //    if (permission != PackageManager.PERMISSION_GRANTED)
        //    {
        //        // We don't have permission so prompt the user
        //        ActivityCompat.requestPermissions(
        //                activity,
        //                PERMISSIONS_STORAGE,
        //                REQUEST_EXTERNAL_STORAGE
        //        );
        //    }
        //}
        public async void downloadresume()
        {
           try
            {
                string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
                //https://techjobs.sulekha.com/Mobileapp/localjobs
                var resumedownloadapi = RestService.For<IJobseeker>("https://localjobs.sulekha.com/mobileappresume");
                var downloaddata = await resumedownloadapi.downloadresumeurl(Commonsettings.UserPid, Commonsettings.UserMobileno, Commonsettings.UserEmail, jobresumeid,ipaddress);
                if(!string.IsNullOrEmpty(downloaddata.resumepath))
                {
                   // Device.OpenUri(new Uri(downloaddata.resumepath));
                    Device.OpenUri(new Uri(downloaddata.resumepath));
                }
                else
                {
                    dialog.Toast("Couldn't download...try again later...");
                }
            }
            catch(Exception ex)
            {

            }
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
