using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using Acr.UserDialogs;
using NRIApp.DayCare.Features.Post.Models;
using Refit;
using NRIApp.Helpers;
using NRIApp.DayCare.Features.Post.Interface;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;
using System.Xml.Linq;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using System.Text.RegularExpressions;
using NRIApp.DayCare.Features.Post.Views;

namespace NRIApp.DayCare.Features.Post.ViewModels
{
    public class Postfirst_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IUserDialogs dialog = UserDialogs.Instance;
        string ipaddress = DependencyService.Get<IIPAddressManager>().GetIPAddress();
        Daycareposting post = new Daycareposting();

        public Command nxtcmd { get; set; }

        List<tset> genderlist = new List<tset>();
        List<tset> liveoutpreferencelist = new List<tset>();
        List<tset> paidexperiencelist = new List<tset>();
        List<tset> homecareexperiencelist = new List<tset>();
        List<tset> languagelist = new List<tset>();
        List<tset> certificationlist = new List<tset>();
        List<tset> replyperiodlist = new List<tset>();
        List<tset> drivingYN = new List<tset>();
        List<tset> vehicleYN = new List<tset>();
        List<tset> licenseYN = new List<tset>();
        List<tset> activityenrichmentlist = new List<tset>();
        List<tset> establishmentyearlist = new List<tset>();
        public List<qandadata> Qandadataslist { get; set; }
        public List<tset> languagelstview { get; set; }
        //List<tset> carecenteryearlist = new List<tset>();
        //List<tset> familycareestablishmntyrlist = new List<tset>();
        //List<tset> eldercareyearlist = new List<tset>();
        //List<tset> eldercarecenteryearlist = new List<tset>();
        //List<tset> petcareyearlist = new List<tset>();
        public Command Taponpaytype { get; set; }
        public Command selectpaytype { get; set; }
        public Command Taponpaidexperience { get; set; }
        public Command selectpaidexperience { get; set; }
        public Command chbxContentViewTap { get; set; }
        public Command chbxPopupContentTap { get; set; }
        public Command gendertap { get; set; }
        public Command Taponcertification { get; set; }
        public Command certificationcmd { get; set; }
        public Command Taponreplyperiod { get; set; }
        public Command Taponyear { get; set; }
        public Command selectreplyperiod { get; set; }
        public Command drivetap { get; set; }
        public Command vehicletap { get; set; }
        public Command licensetap { get; set; }

        public Command Taponcarecenterdays { get; set; }
        public Command Taponfromtimehours { get; set; }
        public Command Tapontotimehours { get; set; }
        public Command selectDCday { get; set; }
        public Command selectfromDChour { get; set; }
        public Command selecttoDChour { get; set; }
        public Command Taptoaddcarecenterhours { get; set; }
        public Command Taptoremovecarecenterhours { get; set; }
        public Command selectestablishyear { get; set; }
        public Command selectliveoutpreference { get; set; }
        public Command taptoaddlanguage { get; set; }
        List<Paytype> paytypelistdata = new List<Paytype>()
        {
            new Paytype{ paytype="Hourly",paytypeid=182,checkimage="RadioButtonUnChecked.png"},
            new Paytype{ paytype="Daily",paytypeid=183,checkimage="RadioButtonUnChecked.png"},
            new Paytype{ paytype="Weekly",paytypeid=184,checkimage="RadioButtonUnChecked.png"},
            new Paytype{ paytype="Monthly",paytypeid=185,checkimage="RadioButtonUnChecked.png"},
        };
        List<tset> DCdaylists = new List<tset>()
        {
            new tset{ tsetval ="Weekdays",tsetvalimg="RadioButtonUnChecked.png",tsetvalID=""},
            new tset{tsetval="Weekends",tsetvalimg="RadioButtonUnChecked.png",tsetvalID=""},
            new tset{tsetval="Monday",tsetvalimg="RadioButtonUnChecked.png",tsetvalID="Mon"},
            new tset{tsetval="Tuesday",tsetvalimg="RadioButtonUnChecked.png",tsetvalID="Tue"},
            new tset{tsetval="Wednesday",tsetvalimg="RadioButtonUnChecked.png",tsetvalID="Wed"},
            new tset{tsetval="Thursday",tsetvalimg="RadioButtonUnChecked.png",tsetvalID="Thu"},
            new tset{tsetval="Friday",tsetvalimg="RadioButtonUnChecked.png",tsetvalID="Fri"},
            new tset{tsetval="Saturday",tsetvalimg="RadioButtonUnChecked.png",tsetvalID="Sat"},
            new tset{tsetval="Sunday",tsetvalimg="RadioButtonUnChecked.png",tsetvalID="Sun"},
        };
        List<tset> fromtimehourslist = new List<tset>()
        {
            new tset{tsetval="12:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="12:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="1:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="1:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="2:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="2:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="3:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="3:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="4:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="4:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="5:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="5:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="6:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="6:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="7:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="7:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="8:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="8:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="9:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="9:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="10:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="10:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="11:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="11:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="12:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="12:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="1:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="1:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="2:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="2:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="3:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="3:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="4:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="4:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="5:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="5:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="6:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="6:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="7:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="7:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="8:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="8:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="9:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="9:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="10:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="10:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="11:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="11:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
        };
        List<tset> totimehourslist = new List<tset>()
        {
            new tset{tsetval="12:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="12:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="1:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="1:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="2:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="2:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="3:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="3:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="4:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="4:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="5:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="5:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="6:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="6:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="7:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="7:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="8:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="8:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="9:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="9:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="10:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="10:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="11:00 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="11:30 am",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="12:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="12:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="1:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="1:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="2:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="2:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="3:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="3:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="4:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="4:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="5:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="5:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="6:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="6:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="7:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="7:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="8:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="8:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="9:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="9:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="10:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="10:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="11:00 pm",tsetvalimg="RadioButtonUnChecked.png"},
            new tset{tsetval="11:30 pm",tsetvalimg="RadioButtonUnChecked.png"},
        };
        public List<DC_Availability> availablescheulelist = new List<DC_Availability>()
        {
            new DC_Availability{weekdayID="su",avalilabilityID="emsu",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="su",avalilabilityID="lmsu",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="su",avalilabilityID="easu",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="su",avalilabilityID="lasu",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="su",avalilabilityID="eesu",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="su",avalilabilityID="lesu",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="su",avalilabilityID="onsu",CheckImage="CheckBoxUnChecked.png"},


            new DC_Availability{weekdayID="mo",avalilabilityID="emmo",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="mo",avalilabilityID="lmmo",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="mo",avalilabilityID="eamo",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="mo",avalilabilityID="lamo",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="mo",avalilabilityID="eemo",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="mo",avalilabilityID="lemo",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="mo",avalilabilityID="onmo",CheckImage="CheckBoxUnChecked.png"},

            new DC_Availability{weekdayID="tu",avalilabilityID="emtu",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="tu",avalilabilityID="lmtu",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="tu",avalilabilityID="eatu",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="tu",avalilabilityID="latu",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="tu",avalilabilityID="eetu",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="tu",avalilabilityID="letu",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="tu",avalilabilityID="ontu",CheckImage="CheckBoxUnChecked.png"},

            new DC_Availability{weekdayID="we",avalilabilityID="emwe",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="we",avalilabilityID="lmwe",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="we",avalilabilityID="eawe",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="we",avalilabilityID="lawe",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="we",avalilabilityID="eewe",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="we",avalilabilityID="lewe",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="we",avalilabilityID="onwe",CheckImage="CheckBoxUnChecked.png"},

            new DC_Availability{weekdayID="th",avalilabilityID="emth",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="th",avalilabilityID="lmth",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="th",avalilabilityID="eath",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="th",avalilabilityID="lath",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="th",avalilabilityID="eeth",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="th",avalilabilityID="leth",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="th",avalilabilityID="onth",CheckImage="CheckBoxUnChecked.png"},

            new DC_Availability{weekdayID="fr",avalilabilityID="emfr",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="fr",avalilabilityID="lmfr",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="fr",avalilabilityID="eafr",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="fr",avalilabilityID="lafr",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="fr",avalilabilityID="eefr",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="fr",avalilabilityID="lefr",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="fr",avalilabilityID="onfr",CheckImage="CheckBoxUnChecked.png"},

            new DC_Availability{weekdayID="sa",avalilabilityID="emsa",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="sa",avalilabilityID="lmsa",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="sa",avalilabilityID="easa",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="sa",avalilabilityID="lasa",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="sa",avalilabilityID="eesa",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="sa",avalilabilityID="lesa",CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{weekdayID="sa",avalilabilityID="onsa",CheckImage="CheckBoxUnChecked.png"},

        };
        public List<DC_Availability> availabilitytbldatalist = new List<DC_Availability>()
        {
            new DC_Availability{ weekdays="Sun",weekdayID="su", timings ="(6am-9am)", timingsID ="em", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Sun",weekdayID="su", timings="(9am-12pm)", timingsID="lm", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Sun",weekdayID="su", timings="(12am-6am)", timingsID="on", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Sun",weekdayID="su", timings="(12pm-3pm)", timingsID="ea", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Sun",weekdayID="su",timings="(3pm-6pm)", timingsID="la", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Sun",weekdayID="su", timings="(6pm-9pm)", timingsID="ee", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Sun",weekdayID="su", timings="(9pm-12am)", timingsID="le", CheckImage="CheckBoxUnChecked.png"},

            new DC_Availability{ weekdays="Mon",weekdayID="mo", timings ="(6am-9am)", timingsID ="em", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Mon",weekdayID="mo", timings="(9am-12pm)", timingsID="lm", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Mon",weekdayID="mo", timings="(12am-6am)", timingsID="on", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Mon",weekdayID="mo", timings="(12pm-3pm)", timingsID="ea", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Mon",weekdayID="mo", timings="(3pm-6pm)", timingsID="la", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Mon",weekdayID="mo", timings="Early Evening(6pm-9pm)", timingsID="ee", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Mon",weekdayID="mo", timings="Late Evening(9pm-12am)", timingsID="le", CheckImage="CheckBoxUnChecked.png"},

            new DC_Availability{ weekdays="Tue",weekdayID="tu", timings ="Early Morning(6am-9am)", timingsID ="em", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Tue",weekdayID="tu", timings="Late Morning(9am-12pm)", timingsID="lm", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Tue",weekdayID="tu", timings="Overnight(12am-6am)", timingsID="on", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Tue",weekdayID="tu", timings="Early Afternoon(12pm-3pm)", timingsID="ea", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Tue", weekdayID="tu",timings="Late Afternoon(3pm-6pm)", timingsID="la", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Tue",weekdayID="tu", timings="Early Evening(6pm-9pm)", timingsID="ee", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Tue",weekdayID="tu", timings="Late Evening(9pm-12am)", timingsID="le", CheckImage="CheckBoxUnChecked.png"},

            new DC_Availability{ weekdays="Wed",weekdayID="we", timings ="Early Morning(6am-9am)", timingsID ="em", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Wed",weekdayID="we", timings="Late Morning(9am-12pm)", timingsID="lm", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Wed",weekdayID="we", timings="Overnight(12am-6am)", timingsID="on", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Wed",weekdayID="we", timings="Early Afternoon(12pm-3pm)", timingsID="ea", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Wed",weekdayID="we",timings="Late Afternoon(3pm-6pm)", timingsID="la", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Wed",weekdayID="we", timings="Early Evening(6pm-9pm)", timingsID="ee", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Wed",weekdayID="we", timings="Late Evening(9pm-12am)", timingsID="le", CheckImage="CheckBoxUnChecked.png"},

            new DC_Availability{ weekdays="Thursday",weekdayID="th", timings ="Early Morning(6am-9am)", timingsID ="em", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Thursday",weekdayID="th", timings="Late Morning(9am-12pm)", timingsID="lm", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Thursday",weekdayID="th", timings="Overnight(12am-6am)", timingsID="on", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Thursday",weekdayID="th", timings="Early Afternoon(12pm-3pm)", timingsID="ea", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Thursday",weekdayID="th",timings="Late Afternoon(3pm-6pm)", timingsID="la", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Thursday",weekdayID="th", timings="Early Evening(6pm-9pm)", timingsID="ee", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Thursday",weekdayID="th", timings="Late Evening(9pm-12am)", timingsID="le", CheckImage="CheckBoxUnChecked.png"},

            new DC_Availability{ weekdays="Friday",weekdayID="fr", timings ="Early Morning(6am-9am)", timingsID ="em", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Friday",weekdayID="fr", timings="Late Morning(9am-12pm)", timingsID="lm", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Friday",weekdayID="fr", timings="Overnight(12am-6am)", timingsID="on", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Friday",weekdayID="fr", timings="Early Afternoon(12pm-3pm)", timingsID="ea", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Friday",weekdayID="fr",timings="Late Afternoon(3pm-6pm)", timingsID="la", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Friday",weekdayID="fr", timings="Early Evening(6pm-9pm)", timingsID="ee", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Friday",weekdayID="fr", timings="Late Evening(9pm-12am)", timingsID="le", CheckImage="CheckBoxUnChecked.png"},

            new DC_Availability{ weekdays="Saturday",weekdayID="sa", timings ="Early Morning(6am-9am)", timingsID ="em", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Saturday",weekdayID="sa", timings="Late Morning(9am-12pm)", timingsID="lm", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Saturday",weekdayID="sa", timings="Overnight(12am-6am)", timingsID="on", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Saturday",weekdayID="sa", timings="Early Afternoon(12pm-3pm)", timingsID="ea", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Saturday",weekdayID="sa",timings="Late Afternoon(3pm-6pm)", timingsID="la", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Saturday",weekdayID="sa", timings="Early Evening(6pm-9pm)", timingsID="ee", CheckImage="CheckBoxUnChecked.png"},
            new DC_Availability{ weekdays="Saturday",weekdayID="sa", timings="Late Evening(9pm-12am)", timingsID="le", CheckImage="CheckBoxUnChecked.png"},
        }; //CheckBoxChecked.png
        public List<DC_Availability> availabilitytbldatalist1 = new List<DC_Availability>()
        {

        };
        public void checkavailabilitytable(DC_Availability dta)
        {

        }
        private string _salarytxt = "";
        public string salarytxt
        {
            get { return _salarytxt; }
            set { _salarytxt = value; OnPropertyChanged(nameof(salarytxt)); }
        }
        private string _paytypetxt = "Hourly";
        public string paytypetxt
        {
            get { return _paytypetxt; }
            set { _paytypetxt = value; OnPropertyChanged(nameof(paytypetxt)); }
        }
        private int _paytypeid = 182;
        public int paytypeid
        {
            get { return _paytypeid; }
            set { _paytypeid = value; OnPropertyChanged(nameof(paytypeid)); }
        }
        private string _payratetxt = "";
        public string payratetxt
        {
            get { return _payratetxt; }
            set { _payratetxt = value; OnPropertyChanged(nameof(payratetxt)); }
        }
        private string _Maxsal = "";
        public string Maxsal
        {
            get { return _Maxsal; }
            set { _Maxsal = value; OnPropertyChanged(nameof(Maxsal)); }
        }
        private string _gendertxt = "";
        public string gendertxt
        {
            get { return _gendertxt; }
            set { _gendertxt = value; OnPropertyChanged(nameof(gendertxt)); }
        }
        private bool _gendertxtvisible = false;
        public bool gendertxtvisible
        {
            get { return _gendertxtvisible; }
            set { _gendertxtvisible = value; OnPropertyChanged(nameof(gendertxtvisible)); }
        }
        private bool _jobtypelblvisible = false;
        public bool jobtypelblvisible
        {
            get { return _jobtypelblvisible; }
            set { _jobtypelblvisible = value; OnPropertyChanged(nameof(jobtypelblvisible)); }
        }

        private string _qaexperiencetxt = "";
        public string qaexperiencetxt
        {
            get { return _qaexperiencetxt; }
            set { _qaexperiencetxt = value; OnPropertyChanged(nameof(qaexperiencetxt)); }
        }
        private bool _experiencelblvisible = false;
        public bool experiencelblvisible
        {
            get { return _experiencelblvisible; }
            set { _experiencelblvisible = value; OnPropertyChanged(nameof(experiencelblvisible)); }
        }
        private string _replyperiodtxt = "";
        public string replyperiodtxt
        {
            get { return _replyperiodtxt; }
            set { _replyperiodtxt = value; OnPropertyChanged(nameof(replyperiodtxt)); }
        }
        private string _replyperiodtxtID = "";
        public string replyperiodtxtID
        {
            get { return _replyperiodtxtID; }
            set { _replyperiodtxtID = value; OnPropertyChanged(nameof(replyperiodtxtID)); }
        }
        private string _replyperiodquestionID = "";
        public string replyperiodquestionID
        {
            get { return _replyperiodquestionID; }
            set { _replyperiodquestionID = value; OnPropertyChanged(nameof(replyperiodquestionID)); }
        }

        private string _languageqatxt = "";
        public string languageqatxt
        {
            get { return _languageqatxt; }
            set { _languageqatxt = value; OnPropertyChanged(nameof(languageqatxt)); }
        }

        private List<careservicedata> _serviceneedlstdata;
        public List<careservicedata> serviceneedlstdata
        {
            get { return _serviceneedlstdata; }
            set { _serviceneedlstdata = value; OnPropertyChanged(nameof(serviceneedlstdata)); }
        }
        private List<careservicedata> _jobtypelstdata;
        public List<careservicedata> jobtypelstdata
        {
            get { return _jobtypelstdata; }
            set { _jobtypelstdata = value; OnPropertyChanged(nameof(jobtypelstdata)); }
        }
        private List<careservicedata> _agegrouplstdata;
        public List<careservicedata> agegrouplstdata
        {
            get { return _agegrouplstdata; }
            set { _agegrouplstdata = value; OnPropertyChanged(nameof(agegrouplstdata)); }
        }
        private string _checkedagegrouptypevalue = "";
        public string checkedagegrouptypevalue
        {
            get { return _checkedagegrouptypevalue; }
            set { _checkedagegrouptypevalue = value; OnPropertyChanged(nameof(checkedagegrouptypevalue)); }
        }
        private string _checkedneedservicetypevalue = "";
        public string checkedneedservicetypevalue
        {
            get { return _checkedneedservicetypevalue; }
            set { _checkedneedservicetypevalue = value; OnPropertyChanged(nameof(checkedneedservicetypevalue)); }
        }
        private string _checkedjobtypevalue = "";
        public string checkedjobtypevalue
        {
            get { return _checkedjobtypevalue; }
            set { _checkedjobtypevalue = value; OnPropertyChanged(nameof(checkedjobtypevalue)); }
        }
        private string _checkedlanguagevalue = "";
        public string checkedlanguagevalue
        {
            get { return _checkedlanguagevalue; }
            set { _checkedlanguagevalue = value; OnPropertyChanged(nameof(checkedlanguagevalue)); }
        }
        private bool _activityenrichmntblkvisible = false;
        public bool activityenrichmntblkvisible
        {
            get { return _activityenrichmntblkvisible; }
            set { _activityenrichmntblkvisible = value; OnPropertyChanged(nameof(activityenrichmntblkvisible)); }
        }
        private bool _establismentyrblkvisible = false;
        public bool establismentyrblkvisible
        {
            get { return _establismentyrblkvisible; }
            set { _establismentyrblkvisible = value; OnPropertyChanged(nameof(establismentyrblkvisible)); }
        }
        private string _paidexperienceyrtxtID = "";
        public string paidexperienceyrtxtID
        {
            get { return _paidexperienceyrtxtID; }
            set { _paidexperienceyrtxtID = value; OnPropertyChanged(nameof(paidexperienceyrtxtID)); }
        }
        private bool _addmorelanguagevisible = false;
        public bool addmorelanguagevisible
        {
            get { return _addmorelanguagevisible; }
            set { _addmorelanguagevisible = value; OnPropertyChanged(nameof(addmorelanguagevisible)); }
        }
        private string _Addmorelanguagetext = "";
        public string Addmorelanguagetext
        {
            get { return _Addmorelanguagetext; }
            set { _Addmorelanguagetext = value; OnPropertyChanged(nameof(_Addmorelanguagetext)); }
        }
        private string _certificationtxt = "";
        public string certificationtxt
        {
            get { return _certificationtxt; }
            set { _certificationtxt = value; OnPropertyChanged(nameof(certificationtxt)); }
        }
        private bool _certificationlistviewvisble = false;
        public bool certificationlistviewvisble
        {
            get { return _certificationlistviewvisble; }
            set { _certificationlistviewvisble = value; OnPropertyChanged(nameof(certificationlistviewvisble)); }
        }
        private bool _chbxcontentviewvisible = false;
        public bool chbxcontentviewvisible
        {
            get { return _chbxcontentviewvisible; }
            set { _chbxcontentviewvisible = value; OnPropertyChanged(nameof(chbxcontentviewvisible)); }
        }
        private string _checkedcertificationvalue = "";
        public string checkedcertificationvalue
        {
            get { return _checkedcertificationvalue; }
            set { _checkedcertificationvalue = value; OnPropertyChanged(nameof(checkedcertificationvalue)); }
        }
        private string _checkedactivitypevalue = "";
        public string checkedactivitypevalue
        {
            get { return _checkedactivitypevalue; }
            set { _checkedactivitypevalue = value; OnPropertyChanged(nameof(checkedactivitypevalue)); }
        }
        private bool _paidexperiencelistviewvisble = false;
        public bool paidexperiencelistviewvisble
        {
            get { return _paidexperiencelistviewvisble; }
            set { _paidexperiencelistviewvisble = value; OnPropertyChanged(nameof(paidexperiencelistviewvisble)); }
        }
        private bool _establishyearlistviewvisble = false;
        public bool establishyearlistviewvisble
        {
            get { return _establishyearlistviewvisble; }
            set { _establishyearlistviewvisble = value; OnPropertyChanged(nameof(establishyearlistviewvisble)); }
        }
        private bool _contentviewvisible = false;
        public bool contentviewvisible
        {
            get { return _contentviewvisible; }
            set { _contentviewvisible = value; OnPropertyChanged(nameof(contentviewvisible)); }
        }
        private bool _certificationscrolvisible = false;
        public bool certificationscrolvisible
        {
            get { return _certificationscrolvisible; }
            set { _certificationscrolvisible = value; OnPropertyChanged(nameof(certificationscrolvisible)); }
        }
        private bool _replyperiodlistviewvisble = false;
        public bool replyperiodlistviewvisble
        {
            get { return _replyperiodlistviewvisble; }
            set { _replyperiodlistviewvisble = value; OnPropertyChanged(nameof(replyperiodlistviewvisble)); }
        }
        private bool _drivetxtvisible = false;
        public bool drivetxtvisible
        {
            get { return _drivetxtvisible; }
            set { _drivetxtvisible = value; OnPropertyChanged(nameof(drivetxtvisible)); }
        }
        private bool _vehicletxtvisible = false;
        public bool vehicletxtvisible
        {
            get { return _vehicletxtvisible; }
            set { _vehicletxtvisible = value; OnPropertyChanged(nameof(vehicletxtvisible)); }
        }
        private bool _licensetxtvisible = false;
        public bool licensetxtvisible
        {
            get { return _licensetxtvisible; }
            set { _licensetxtvisible = value; OnPropertyChanged(nameof(licensetxtvisible)); }
        }
        private string _drivetxt = "";
        public string drivetxt
        {
            get { return _drivetxt; }
            set { _drivetxt = value; OnPropertyChanged(nameof(drivetxt)); }
        }
        private string _vehicletxt = "";
        public string vehicletxt
        {
            get { return _vehicletxt; }
            set { _vehicletxt = value; OnPropertyChanged(nameof(vehicletxt)); }
        }
        private string _licensetxt = "";
        public string licensetxt
        {
            get { return _licensetxt; }
            set { _licensetxt = value; OnPropertyChanged(nameof(licensetxt)); }
        }
        private string _activitytxt = "";
        public string activitytxt
        {
            get { return _activitytxt; }
            set { _activitytxt = value; OnPropertyChanged(nameof(activitytxt)); }
        }
        private string _establishmentyrtxt = "";
        public string establishmentyrtxt
        {
            get { return _establishmentyrtxt; }
            set { _establishmentyrtxt = value; OnPropertyChanged(nameof(establishmentyrtxt)); }
        }
        private string _establishyeartxtID = "";
        public string establishyeartxtID
        {
            get { return _establishyeartxtID; }
            set { _establishyeartxtID = value; OnPropertyChanged(nameof(establishyeartxtID)); }
        }
        private bool _respondlblvisible = false;
        public bool respondlblvisible
        {
            get { return _respondlblvisible; }
            set { _respondlblvisible = value; OnPropertyChanged(nameof(respondlblvisible)); }
        }

        private bool _daylistvisible = false;
        public bool daylistvisible
        {
            get { return _daylistvisible; }
            set { _daylistvisible = value; OnPropertyChanged(nameof(daylistvisible)); }
        }

        private string _liveoutprferencetxtID = "";
        public string liveoutprferencetxtID
        {
            get { return _liveoutprferencetxtID; }
            set { _liveoutprferencetxtID = value; OnPropertyChanged(nameof(liveoutprferencetxtID)); }
        }
        private string _liveoutprferencetxt = "";
        public string liveoutprferencetxt
        {
            get { return _liveoutprferencetxt; }
            set { _liveoutprferencetxt = value; OnPropertyChanged(nameof(liveoutprferencetxt)); }
        }
        private bool _liveoutprferencevisible = false;
        public bool liveoutprferencevisible
        {
            get { return _liveoutprferencevisible; }
            set { _liveoutprferencevisible = value; OnPropertyChanged(nameof(liveoutprferencevisible)); }
        }

        private bool _languagelayoutvisible = false;
        public bool languagelayoutvisible
        {
            get { return _languagelayoutvisible; }
            set { _languagelayoutvisible = value; OnPropertyChanged(nameof(languagelayoutvisible)); }
        }

        //availability - needed date 
        private string _fromdatetxt = "";
        public string fromdatetxt
        {
            get { return _fromdatetxt; }
            set { _fromdatetxt = value; OnPropertyChanged(nameof(fromdatetxt)); }
        }
        private string _servicedatetxt = "";
        public string servicedatetxt
        {
            get { return _servicedatetxt; }
            set { _servicedatetxt = value; OnPropertyChanged(nameof(servicedatetxt)); }
        }
        private string _todatetxt = "";
        public string todatetxt
        {
            get { return _todatetxt; }
            set { _todatetxt = value; OnPropertyChanged(nameof(todatetxt)); }
        }
        //private string _fromdate = "";
        //public string fromdate
        //{
        //    get { return _fromdate; }
        //    set { _fromdate = value;
        //        OnPropertyChanged(nameof(fromdate)); }
        //}
        //  public string TempaddresslocationName = "";
        private DateTime _fromdate;
        public DateTime fromdate
        {
            get { return _fromdate; }
            set
            {
                _fromdate = value;
                OnPropertyChanged(nameof(fromdate));
                TimeSpan ts = todate.Date - fromdate.Date;
                if (ts.Days > 2 && post.tertiarycategoryid != "19" && (post.supercategoryid == "2" || post.supercategoryid == "1") && post.secondarycategoryid != "13")
                {
                    //getdaysschedulelist();
                    dayschedulevisible = true;
                }
                else
                {
                    dayschedulevisible = false;
                }
            }
        }

        private DateTime _todate;
        public DateTime todate
        {
            get { return _todate; }
            set
            {
                _todate = value;
                OnPropertyChanged(nameof(todate));
                TimeSpan ts = todate.Date - fromdate.Date;
                if (ts.Days > 2 && post.tertiarycategoryid != "19" && (post.supercategoryid == "2" || post.supercategoryid == "1") && post.secondarycategoryid != "13")
                {
                    // getdaysschedulelist();
                    dayschedulevisible = true;
                }
                else
                {
                    dayschedulevisible = false;
                }
                //  dayschedulevisible = true;
            }
        }
        private bool _dayschedulevisible = false;
        public bool dayschedulevisible
        {
            get { return _daylistvisible; }
            set { _daylistvisible = value; OnPropertyChanged(nameof(dayschedulevisible)); }
        }

        //private string _mindate = DateTime.Today.ToString("MM-DD-YYYY");
        //public string mindate
        //{
        //    get { return _mindate; }
        //    set { _mindate = value; OnPropertyChanged(nameof(mindate)); }
        //}
        private DateTime _mindate = DateTime.Today;
        public DateTime mindate
        {
            get { return _mindate; }
            set { _mindate = value; OnPropertyChanged(nameof(mindate)); }
        }
        private bool _paytypelistviewvisble = false;
        public bool paytypelistviewvisble
        {
            get { return _paytypelistviewvisble; }
            set { _paytypelistviewvisble = value; OnPropertyChanged(nameof(paytypelistviewvisble)); }
        }
        private string _agegrouplbltxt = "";
        public string agegrouplbltxt
        {
            get { return _agegrouplbltxt; }
            set { _agegrouplbltxt = value; OnPropertyChanged(nameof(agegrouplbltxt)); }
        }
        private bool _tohourpickervisible = false;
        public bool tohourpickervisible
        {
            get { return _tohourpickervisible; }
            set { _tohourpickervisible = value; OnPropertyChanged(nameof(tohourpickervisible)); }
        }
        private bool _fromhourpickervisible = false;
        public bool fromhourpickervisible
        {
            get { return _fromhourpickervisible; }
            set { _fromhourpickervisible = value; OnPropertyChanged(nameof(fromhourpickervisible)); }
        }
        private bool _carecenterhoursvisible = false;
        public bool carecenterhoursvisible
        {
            get { return _carecenterhoursvisible; }
            set { _carecenterhoursvisible = value; OnPropertyChanged(nameof(carecenterhoursvisible)); }
        }
        private bool _carecenterhourslistvisible = false;
        public bool carecenterhourslistvisible
        {
            get { return _carecenterhourslistvisible; }
            set { _carecenterhourslistvisible = value; OnPropertyChanged(nameof(carecenterhourslistvisible)); }
        }
        private string _fromhour = "12.00 am";
        public string fromhour
        {
            get { return _fromhour; }
            set { _fromhour = value; OnPropertyChanged(nameof(fromhour)); }
        }
        private string _tohour = "12.00 am";
        public string tohour
        {
            get { return _tohour; }
            set { _tohour = value; OnPropertyChanged(nameof(tohour)); }
        }
        private string _dayID = "";
        public string dayID
        {
            get { return _dayID; }
            set { _dayID = value; OnPropertyChanged(nameof(dayID)); }
        }
        private string _dayvalueID = "";
        public string dayvalueID
        {
            get { return _dayvalueID; }
            set { _dayvalueID = value; OnPropertyChanged(nameof(dayvalueID)); }
        }

        private List<dchourtime> _addtimingslist;
        public List<dchourtime> addtimingslist
        {
            get { return _addtimingslist; }
            set { _addtimingslist = value; OnPropertyChanged(nameof(addtimingslist)); }
        }
        //private List<dchourtime> _copyaddtimingslist;
        //public List<dchourtime> copyaddtimingslist
        //{
        //    get { return _copyaddtimingslist; }
        //    set { _copyaddtimingslist = value; OnPropertyChanged(nameof(copyaddtimingslist)); }
        //}
        private bool _availablefromtovisible = false;
        public bool availablefromtovisible
        {
            get { return _availablefromtovisible; }
            set { _availablefromtovisible = value; OnPropertyChanged(nameof(availablefromtovisible)); }
        }
        private bool _agegrouplblvisible = false;
        public bool agegrouplblvisible
        {
            get { return _agegrouplblvisible; }
            set { _agegrouplblvisible = value; OnPropertyChanged(nameof(agegrouplblvisible)); }
        }
        private bool _gendervisiblity = false;
        public bool gendervisiblity
        {
            get { return _gendervisiblity; }
            set { _gendervisiblity = value; OnPropertyChanged(nameof(gendervisiblity)); }
        }
        private bool _needservicelblvisible = false;
        public bool needservicelblvisible
        {
            get { return _needservicelblvisible; }
            set { _needservicelblvisible = value; OnPropertyChanged(nameof(needservicelblvisible)); }
        }
        private string _needservicelbltxt = "";
        public string needservicelbltxt
        {
            get { return _needservicelbltxt; }
            set { _needservicelbltxt = value; OnPropertyChanged(nameof(needservicelbltxt)); }
        }

        private string _checkedotherprovidervalue = "";
        public string checkedotherprovidervalue
        {
            get { return _checkedotherprovidervalue; }
            set { _checkedotherprovidervalue = value; OnPropertyChanged(nameof(checkedotherprovidervalue)); }
        }


        //tocheck newly added language 
        private string _Addmorelbllanguagetxt = "";
        public string Addmorelbllanguagetxt
        {
            get { return _Addmorelbllanguagetxt; }
            set { _Addmorelbllanguagetxt = value; OnPropertyChanged(nameof(Addmorelbllanguagetxt)); }
        }
        private string _addlanguagebtntxt = "Add";
        public string addlanguagebtntxt
        {
            get { return _addlanguagebtntxt; }
            set { _addlanguagebtntxt = value; OnPropertyChanged(nameof(addlanguagebtntxt)); }
        }
        private string _rateplacehldrtxt = "";
        public string rateplacehldrtxt
        {
            get { return _rateplacehldrtxt; }
            set { _rateplacehldrtxt = value; OnPropertyChanged(nameof(rateplacehldrtxt)); }
        }
        private bool _virtualdetailpopupvisible = false;
        public bool virtualdetailpopupvisible
        {
            get { return _virtualdetailpopupvisible; }
            set { _virtualdetailpopupvisible = value; OnPropertyChanged(nameof(virtualdetailpopupvisible)); }
        }
        private bool _needvirtualstack = false;
        public bool needvirtualstack
        {
            get { return _needvirtualstack; }
            set { _needvirtualstack = value; OnPropertyChanged(nameof(needvirtualstack)); }
        }
        private bool _offervirtualstack = false;
        public bool offervirtualstack
        {
            get { return _offervirtualstack; }
            set { _offervirtualstack = value; OnPropertyChanged(nameof(offervirtualstack)); }
        }
        private string _virtualcheckdtltext = "";
        public string virtualcheckdtltext
        {
            get { return _virtualcheckdtltext; }
            set { _virtualcheckdtltext = value; OnPropertyChanged(nameof(virtualcheckdtltext)); }
        }

        public List<string> availabitytabledata { get; set; }
        public string availabilitytable { get; set; }

        bool validation()
        {
            bool isvalid = true;
            //paytype
            if (payratetypecisible == true)
            {
                if ((string.IsNullOrEmpty(payratetxt) || payratetxt.Trim().Length == 0) || payratetxt == "." || payratetxt.Contains("-"))
                {
                    if (post.secondarycategoryid != "9")
                    {
                        dialog.Toast("Enter min salary amount");
                        isvalid = false;
                        return false;
                    }

                }
                if (!string.IsNullOrEmpty(payratetxt) && post.secondarycategoryid != "9")
                {
                    if (payratetxt.Contains("."))
                    {
                        if (Convert.ToDecimal(payratetxt) < 1)
                        {
                            dialog.Toast("Enter a valid salary amount");
                            isvalid = false;
                            return false;
                        }
                    }
                }
                if ((string.IsNullOrEmpty(Maxsal) || Maxsal.Trim().Length == 0) || Maxsal == "." || Maxsal.Contains("-"))
                {
                    if (post.secondarycategoryid != "9")
                    {
                        dialog.Toast("Enter max salary amount");
                        isvalid = false;
                        return false;
                    }

                }
                if (!string.IsNullOrEmpty(Maxsal) && post.secondarycategoryid != "9")
                {
                    if (Maxsal.Contains("."))
                    {
                        if (Convert.ToDecimal(Maxsal) < 1)
                        {
                            dialog.Toast("Enter a valid salary amount");
                            isvalid = false;
                            return false;
                        }
                    }
                    if (Convert.ToDecimal(payratetxt) > Convert.ToDecimal(Maxsal))
                    {
                        dialog.Toast("Max salary should be greater than min salary");
                        isvalid = false;
                        return false;
                    }
                }
                //string.IsNullOrEmpty(paytypeid.ToString()) || 
                if ((string.IsNullOrEmpty(paytypeid.ToString()) || paytypeid == 0) && post.secondarycategoryid != "9")
                {
                    isvalid = false;
                    dialog.Toast("Select salary type");
                    return false;
                }
            }
            if (needservicelblvisible == true)
            {
                if (string.IsNullOrEmpty(checkedneedservicetypevalue))
                {
                    isvalid = false;
                    dialog.Toast("Please select which service you can need/provide");
                    return false;
                }
            }
            if (agegrouplblvisible == true && string.IsNullOrEmpty(checkedagegrouptypevalue))
            {
                isvalid = false;
                dialog.Toast("Select age group");
                return false;
            }
            if (gendertxtvisible == true && string.IsNullOrEmpty(genderID))
            {
                isvalid = false;
                dialog.Toast("Select gender");
                return false;
            }
            if (experiencelblvisible == true && string.IsNullOrEmpty(paidexperienceyrtxtID))
            {
                isvalid = false;
                dialog.Toast("Select experience");
                return false;
            }

            if (workdescriptionvisible == true && workdescription.Trim().Length == 0)
            {
                isvalid = false;
                dialog.Toast("Please enter work experience");
                return false;
            }
            if (languagelayoutvisible == true && string.IsNullOrEmpty(checkedlanguagevalue))
            {
                isvalid = false;
                dialog.Toast("Select language");
                return false;
            }
            if (certificationlistviewvisble == true && string.IsNullOrEmpty(_checkedcertificationvalue))
            {
                isvalid = false;
                dialog.Toast("Select Certifications done");
                return false;
            }
            if (respondlblvisible == true && string.IsNullOrEmpty(replyperiodtxtID))
            {
                isvalid = false;
                dialog.Toast("Select response time");
                return false;
            }

            if (activityenrichmntblkvisible == true && string.IsNullOrEmpty(checkedactivitypevalue))
            {
                isvalid = false;
                dialog.Toast("Select activity types");
                return false;
            }
            if (establismentyrblkvisible == true && string.IsNullOrEmpty(establishyeartxtID))
            {
                isvalid = false;
                dialog.Toast("Select year");
                return false;
            }
            if (liveoutprferencevisible == true && string.IsNullOrEmpty(liveoutprferencetxtID))
            {
                isvalid = false;
                dialog.Toast("Select live out / live in preferences");
                return false;
            }
            if (fromdate > todate)
            {
                isvalid = false;
                dialog.Toast("To date should be greater than From date");
                return false;
            }
            //carecentervisible
            //if(otherprovidervisible==true && string.IsNullOrEmpty(checkedotherprovidervalue))
            //{
            //    isvalid = false;
            //    dialog.Toast("Select other services you provide ");
            //    return false;
            //}
            return isvalid;
        }
        public Postfirst_VM(Daycareposting postdata)
        {
            try
            {
                post = postdata;
                if (post.ismyneed == "1")
                {
                    if (!string.IsNullOrEmpty(postdata.Salaryrate))
                    {
                        mindate = DateTime.ParseExact(postdata.availablefrom,"MM/dd/yyyy",null);
                        fromdate = DateTime.ParseExact(postdata.availablefrom, "MM/dd/yyyy", null);
                        todate = DateTime.ParseExact(postdata.Availableto, "MM/dd/yyyy", null);
                        //fromdate = Convert.ToDateTime(postdata.availablefrom);
                        //todate = Convert.ToDateTime(postdata.Availableto);

                        payratetxt = postdata.Salaryrate;
                        Maxsal = postdata.salaryratemax;
                        paytypetxt = postdata.Salarymode;
                        paidexperienceyrtxt = postdata.Experience;
                        paidexperienceyrtxtID = postdata.Experience;
                        replyperiodtxtID = postdata.responsetime;
                        if (!string.IsNullOrEmpty(paidexperienceyrtxt))
                        {
                            if (paidexperienceyrtxt != "Beginner" && Convert.ToInt32(paidexperienceyrtxt) >= 1 && Convert.ToInt32(paidexperienceyrtxt) <= 10)
                            {
                                workdescriptionvisible = true;
                            }
                            else
                            {
                                workdescriptionvisible = false;
                            }
                        }

                        workdescription = postdata.workdesc;
                        establishyeartxtID = postdata.establishmentyear;
                        gendertxt = postdata.gender;
                        genderID = postdata.Genderid;
                        qaexperiencetxt = postdata.Experience;
                        liveoutprferencetxt = postdata.preference;
                        if (postdata.supercategoryid == "1" && postdata.maincategoryid == "1")
                        {
                            transportationtextID = postdata.tranportation;
                            educationtextID = postdata.Education;
                            smoketextID = postdata.smoke;
                            comfpetstextID = postdata.comfortablewithpets;
                            smokecheck();
                            comfpetservice();
                            transeporationservice();
                            if (educationtextID == "High School")
                            {
                                educationcheck();
                            }
                            else if (educationtextID == "High Degree")
                            {
                                highdegreecheck();
                            }
                            else if (educationtextID == "Degree")
                            {
                                degreecheck();
                            }
                            else if (educationtextID == "others")
                            {
                                otherscheck();
                            }
                        }
                    }
                }
                if (postdata.supercategoryid == "1")
                {
                    virtualcheckdtltext = "Select this option only if you provide babysitting service digitally. You are ready to engage kids through online mediums.";
                }
                else
                {
                    virtualcheckdtltext = "Select this option only if you need a care provider to engage with your child digitally and not in person. During a virtual sitting, children cannot be left unattended. ";
                }
                if (postdata.supercategoryid == "1" && postdata.maincategoryid == "1")
                {
                    newqandvisible = true;
                    jobtypelblvisible = true;
                    getjobtype();
                }
                else
                {
                    newqandvisible = false;
                }
                nxtcmd = new Command(postfirstsubmit);
                gendertap = new Command<tset>(selectedgender);
                drivetap = new Command<tset>(selecteddriveyn);
                vehicletap = new Command<tset>(selectedvehicleyn);
                licensetap = new Command<tset>(selectedlicenseyn);
                Taponpaytype = new Command<Paytype>(showpaytypelist);
                selectpaytype = new Command<Paytype>(selectpaytypedata);
                Taponpaidexperience = new Command<tset>(showpaidexperiencelist);
                selectpaidexperience = new Command<tset>(selectpaidexperiencedata);
                Taponreplyperiod = new Command(showreplyperiodlist);
                selectreplyperiod = new Command<tset>(selectreplyperioddata);
                chbxContentViewTap = new Command(Closecontentview);
                chbxPopupContentTap = new Command(showcontentview);
                Taponcertification = new Command(showcertifications);
                certificationcmd = new Command(closecertificationlist);
                Taponyear = new Command<tset>(showstablishyearlist);

                Taponcarecenterdays = new Command<tset>(showdays);
                Taponfromtimehours = new Command(showfromtimepicker);
                Tapontotimehours = new Command(showtotimepicker);
                selectDCday = new Command<tset>(selecteddays);
                selectfromDChour = new Command<tset>(selectfromhour);
                selecttoDChour = new Command<tset>(selecttohour);
                Taptoaddcarecenterhours = new Command(addhourstimings);
                Taptoremovecarecenterhours = new Command<dchourtime>(removehourstimings);
                selectestablishyear = new Command<tset>(selectestablishyeardata);
                selectliveoutpreference = new Command<tset>(selectedliveoutpreference);
                taptoaddlanguage = new Command(addlanguage);
                getpostfirstdata();

                checkalltap = new Command(checkalldays);
                uncheckalltap = new Command(uncheckcheckalldays);
                weekdaystap = new Command(checkweekdays);
                weekendstap = new Command(checkweekends);
                //  checkall();
                //virtualnanny
                onlineservicetap = new Command(provideonlineservice);
                transportationservicetap = new Command(transeporationservice);
                comfpetstap = new Command(comfpetservice);
                dousmoketap = new Command(smokecheck);
                highschooltap = new Command(educationcheck);
                degreetap = new Command(degreecheck);
                highdegreetap = new Command(highdegreecheck);
                otherstap = new Command(otherscheck);
                taponvirtualcheckdtls = new Command(virtualchckdetails);
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        //RadioButtonChecked.png
        //RadioButtonUnChecked.png
        public void virtualchckdetails()
        {
            if (post.supercategoryid == "1")
            {
                offervirtualstack = true;
            }
            else
            {
                needvirtualstack = true;
            }
            contentviewvisible = true;
            fromhourpickervisible = false;
            paytypelistviewvisble = false;
            virtualdetailpopupvisible = true;
            establishyearlistviewvisble = false;
            paidexperiencelistviewvisble = false;
            replyperiodlistviewvisble = false;
            tohourpickervisible = false;
            carecenterhourslistvisible = false;
        }
        public void provideonlineservice()
        {
            if (onlineservicetextID == 0 || post.virtualnanny == "1")
            {
                onlineservicetextID = 1;
                onlineserviceimg = "RadioButtonChecked.png";
                inpersonserviceimg = "RadioButtonUnChecked.png";
            }
            else
            {
                onlineservicetextID = 0;
                onlineserviceimg = "RadioButtonUnChecked.png";
                inpersonserviceimg = "RadioButtonChecked.png";
            }

        }
        public void transeporationservice()
        {
            if (transportationtextID == 0)
            {
                transportationtextID = 1;
                transportationONimg = "RadioButtonChecked.png";
                transportationOFFimg = "RadioButtonUnChecked.png";
            }
            else
            {
                transportationtextID = 0;
                transportationONimg = "RadioButtonUnChecked.png";
                transportationOFFimg = "RadioButtonChecked.png";
            }

        }
        public void comfpetservice()
        {
            if (comfpetstextID == 0)
            {
                comfpetstextID = 1;
                comfpetsONimg = "RadioButtonChecked.png";
                comfpetsOFFimg = "RadioButtonUnChecked.png";
            }
            else
            {
                comfpetstextID = 0;
                comfpetsONimg = "RadioButtonUnChecked.png";
                comfpetsOFFimg = "RadioButtonChecked.png";
            }
        }

        public void smokecheck()
        {
            if (smoketextID == 0)
            {
                smoketextID = 1;
                smokeONimg = "RadioButtonChecked.png";
                smokeOFFimg = "RadioButtonUnChecked.png";
            }
            else
            {
                smoketextID = 0;
                smokeONimg = "RadioButtonUnChecked.png";
                smokeOFFimg = "RadioButtonChecked.png";
            }

        }
        public void educationcheck()
        {
            educationtextID = "High School";
            highschoolimg = "RadioButtonChecked.png";
            degreeimg = "RadioButtonUnChecked.png";
            highdegreeimg = "RadioButtonUnChecked.png";
            otherimg = "RadioButtonUnChecked.png";
        }
        public void degreecheck()
        {
            educationtextID = "Degree";
            highschoolimg = "RadioButtonUnChecked.png";
            degreeimg = "RadioButtonChecked.png";
            highdegreeimg = "RadioButtonUnChecked.png";
            otherimg = "RadioButtonUnChecked.png";
        }
        public void highdegreecheck()
        {
            educationtextID = "High Degree";
            highschoolimg = "RadioButtonUnChecked.png";
            degreeimg = "RadioButtonUnChecked.png";
            highdegreeimg = "RadioButtonChecked.png";
            otherimg = "RadioButtonUnChecked.png";
        }
        public void otherscheck()
        {
            educationtextID = "Others";
            highschoolimg = "RadioButtonUnChecked.png";
            degreeimg = "RadioButtonUnChecked.png";
            highdegreeimg = "RadioButtonUnChecked.png";
            otherimg = "RadioButtonChecked.png";
        }


        private List<Otherprovider_DATA> _otherservicelist;
        public List<Otherprovider_DATA> otherservicelist
        {
            get { return _otherservicelist; }
            set { _otherservicelist = value; OnPropertyChanged(nameof(otherservicelist)); }
        }

        //also provide services (adtnlcats)
        public async void getotherserviceprovider()
        {
            try
            {
                var otherserviceproviderapi = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                var otherserviceproviderapidata = await otherserviceproviderapi.getotherprovider(post.businessid);
                // otherservicelist = otherserviceproviderapidata.ROW_DATA;
                otherservicelist = new List<Otherprovider_DATA>();

                foreach (var dt in otherserviceproviderapidata.ROW_DATA)
                {

                    //if((post.secondarycategoryid != dt.secondarycategoryid.ToString()) && (post.tertiarycategoryid!=dt.tertiarycategoryid.ToString()))
                    if (!(dt.adtnlcats.Contains(post.secondarycategoryid + ":" + post.tertiarycategoryid)))
                    {
                        // otherservicelist.Remove(dt);
                        otherservicelist.Add(dt);
                    }
                }

                var currentpage = Getcurrentpage();
                var otherproviderlayout = currentpage.FindByName<StackLayout>("otherproviderlayout");
                //foreach (var data in otherserviceproviderapidata.ROW_DATA)
                foreach (var data in otherservicelist)
                {
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    //chbx.HeightRequest = 20;
                    //chbx.WidthRequest = 20;
                    chbx.Text = data.otherprovider;
                    chbx.ClassId = data.adtnlcats;
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_OtherserviceCheckChanged;
                    chbx.Padding = 5;
                    if (post.ismyneed == "1")
                    {
                        if (!string.IsNullOrEmpty(post.otherserviceprovider))
                        {
                            string[] otherlst = post.otherserviceprovider.Split(',');
                            foreach (var i in otherlst)
                            {
                                if (data.adtnlcats == i)
                                {
                                    chbx.IsChecked = true;
                                    if (string.IsNullOrEmpty(checkedotherprovidervalue))
                                    {
                                        checkedotherprovidervalue += chbx.ClassId;
                                    }
                                    else
                                    {
                                        checkedotherprovidervalue += "," + chbx.ClassId;
                                    }
                                }
                            }
                        }
                    }

                    otherproviderlayout.Children.Add(chbx);
                }
                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private void Chb_OtherserviceCheckChanged(object sender, EventArgs e)
        {
            try
            {
                dialog.ShowLoading("", null);
                var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
                if (category.IsChecked == true)
                {
                    if (checkedotherprovidervalue != null && checkedotherprovidervalue != "")
                    {
                        if (!checkedotherprovidervalue.Contains(category.ClassId))
                        {
                            checkedotherprovidervalue += "," + category.ClassId;
                        }
                    }
                    else
                    {
                        checkedotherprovidervalue += category.ClassId;
                    }
                }
                else
                {
                    string[] indexvalue = checkedotherprovidervalue.Split(',');
                    int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                    indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                    checkedotherprovidervalue = string.Join(",", indexvalue);
                }
                if(checkedsupercategorytagurl.Contains("nanny") && checkedsupercategorytagurl.Contains("babysitter"))
                {
                    checkedsupercategorytagurl = checkedsupercategorytagurl.Replace("babysitter", "");
                }
                else if(string.IsNullOrEmpty(checkedsupercategorytagurl))
                {
                    checkedsupercategorytagurl = post.categoryurl;
                }
                if (!string.IsNullOrEmpty(checkedotherprovidervalue))
                {
                    foreach (var data in otherservicelist)
                    {

                        string[] otherlst = checkedotherprovidervalue.Split(',');
                        foreach (var i in otherlst)
                        {
                            if (data.adtnlcats == i)
                            {

                                if (string.IsNullOrEmpty(checkedsupercategorytagurl))
                                {
                                    checkedsupercategorytagurl += data.supertagurl;
                                }
                                else
                                {
                                    if(!checkedsupercategorytagurl.Contains(data.supertagurl)  && data.supertagurl != post.categoryurl)
                                    {
                                        if(!(post.categoryurl == "nanny" && data.supertagurl == "babysitter")&& !(data.supertagurl == "nanny" && post.categoryurl == "babysitter"))
                                        {
                                            checkedsupercategorytagurl += "," + data.supertagurl;
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                else
                {
                    checkedsupercategorytagurl = post.categoryurl;
                }
                getneedservice();
                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                dialog.HideLoading();
            }
        }
        public void getdaysschedulelist()
        {
            try
            {
                dialog.ShowLoading("", null);
                dayschedulevisible = true;
                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }

        public void checkalldays()
        {
            try
            {
                dialog.ShowLoading("", null);
                Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;

                var currentpage = Getcurrentpage();
                Button checkall = currentpage.FindByName<Button>("checkall");
                Button uncheckall = currentpage.FindByName<Button>("uncheckall");
                Button weekdays = currentpage.FindByName<Button>("weekdays");
                Button weekends = currentpage.FindByName<Button>("weekends");
                checkall.BackgroundColor = Color.FromHex("#2e74f0");
                checkall.TextColor = Color.FromHex("#ffffff");
                uncheckall.BackgroundColor = Color.FromHex("#ffffff");
                uncheckall.TextColor = Color.FromHex("#2e74f0");
                weekdays.BackgroundColor = Color.FromHex("#ffffff");
                weekdays.TextColor = Color.FromHex("#2e74f0");
                weekends.BackgroundColor = Color.FromHex("#ffffff");
                weekends.TextColor = Color.FromHex("#2e74f0");
                StackLayout dayschedulelist1 = currentpage.FindByName<StackLayout>("dayschedulelist1");
                foreach (var item in dayschedulelist1.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == false)
                        {
                            cat.IsChecked = true;
                        }
                    }
                }
                StackLayout dayschedulelist2 = currentpage.FindByName<StackLayout>("dayschedulelist2");
                foreach (var item in dayschedulelist2.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == false)
                        {
                            cat.IsChecked = true;
                        }
                    }
                }
                StackLayout dayschedulelist3 = currentpage.FindByName<StackLayout>("dayschedulelist3");
                foreach (var item in dayschedulelist3.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == false)
                        {
                            cat.IsChecked = true;
                        }
                    }
                }
                StackLayout dayschedulelist4 = currentpage.FindByName<StackLayout>("dayschedulelist4");
                foreach (var item in dayschedulelist4.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == false)
                        {
                            cat.IsChecked = true;
                        }
                    }
                }
                StackLayout dayschedulelist5 = currentpage.FindByName<StackLayout>("dayschedulelist5");
                foreach (var item in dayschedulelist5.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == false)
                        {
                            cat.IsChecked = true;
                        }
                    }
                }
                StackLayout dayschedulelist6 = currentpage.FindByName<StackLayout>("dayschedulelist6");
                foreach (var item in dayschedulelist6.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == false)
                        {
                            cat.IsChecked = true;
                        }
                    }
                }
                StackLayout dayschedulelist7 = currentpage.FindByName<StackLayout>("dayschedulelist7");
                foreach (var item in dayschedulelist7.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == false)
                        {
                            cat.IsChecked = true;
                        }
                    }
                }
                checkedschedulevalue = "emsu,lmsu,easu,lasu,eesu,lesu,onsu,emsa,lmsa,easa,lasa,eesa,lesa,onsa,emmo,lmmo,eamo,lamo,eemo,lemo,onmo,emtu,lmtu,eatu,latu,eetu,letu,ontu,emwe,lmwe,eawe,lawe,eewe,lewe,onwe,emth,lmth,eath,lath,eeth,leth,onth,emfr,lmfr,eafr,lafr,eefr,lefr,onfr";
                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                dialog.HideLoading();
            }
        }
        public void checkweekdays()
        {
            try
            {
                dialog.ShowLoading("", null);
                Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;

                var currentpage = Getcurrentpage();
                Button checkall = currentpage.FindByName<Button>("checkall");
                Button uncheckall = currentpage.FindByName<Button>("uncheckall");
                Button weekdays = currentpage.FindByName<Button>("weekdays");
                Button weekends = currentpage.FindByName<Button>("weekends");
                checkall.BackgroundColor = Color.FromHex("#ffffff");
                checkall.TextColor = Color.FromHex("#2e74f0");
                uncheckall.BackgroundColor = Color.FromHex("#ffffff");
                uncheckall.TextColor = Color.FromHex("#2e74f0");
                weekdays.BackgroundColor = Color.FromHex("#2e74f0");
                weekdays.TextColor = Color.FromHex("#ffffff");
                weekends.BackgroundColor = Color.FromHex("#ffffff");
                weekends.TextColor = Color.FromHex("#2e74f0");
                StackLayout dayschedulelist1 = currentpage.FindByName<StackLayout>("dayschedulelist1");
                foreach (var item in dayschedulelist1.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == true)
                        {
                            cat.IsChecked = false;
                        }
                    }
                }
                StackLayout dayschedulelist2 = currentpage.FindByName<StackLayout>("dayschedulelist2");
                foreach (var item in dayschedulelist2.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == false)
                        {
                            cat.IsChecked = true;
                        }
                    }
                }
                StackLayout dayschedulelist3 = currentpage.FindByName<StackLayout>("dayschedulelist3");
                foreach (var item in dayschedulelist3.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == false)
                        {
                            cat.IsChecked = true;
                        }
                    }
                }
                StackLayout dayschedulelist4 = currentpage.FindByName<StackLayout>("dayschedulelist4");
                foreach (var item in dayschedulelist4.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == false)
                        {
                            cat.IsChecked = true;
                        }
                    }
                }
                StackLayout dayschedulelist5 = currentpage.FindByName<StackLayout>("dayschedulelist5");
                foreach (var item in dayschedulelist5.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == false)
                        {
                            cat.IsChecked = true;
                        }
                    }
                }
                StackLayout dayschedulelist6 = currentpage.FindByName<StackLayout>("dayschedulelist6");
                foreach (var item in dayschedulelist6.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == false)
                        {
                            cat.IsChecked = true;
                        }
                    }
                }
                StackLayout dayschedulelist7 = currentpage.FindByName<StackLayout>("dayschedulelist7");
                foreach (var item in dayschedulelist7.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == true)
                        {
                            cat.IsChecked = false;
                        }
                    }
                }
                checkedschedulevalue = "emmo,lmmo,eamo,lamo,eemo,lemo,onmo,emtu,lmtu,eatu,latu,eetu,letu,ontu,emwe,lmwe,eawe,lawe,eewe,lewe,onwe,emth,lmth,eath,lath,eeth,leth,onth,emfr,lmfr,eafr,lafr,eefr,lefr,onfr";
                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public void checkweekends()
        {
            try
            {
                dialog.ShowLoading("", null);
                Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                var currentpage = Getcurrentpage();
                Button checkall = currentpage.FindByName<Button>("checkall");
                Button uncheckall = currentpage.FindByName<Button>("uncheckall");
                Button weekdays = currentpage.FindByName<Button>("weekdays");
                Button weekends = currentpage.FindByName<Button>("weekends");
                checkall.BackgroundColor = Color.FromHex("#ffffff");
                checkall.TextColor = Color.FromHex("#2e74f0");
                uncheckall.BackgroundColor = Color.FromHex("#ffffff");
                uncheckall.TextColor = Color.FromHex("#2e74f0");
                weekdays.BackgroundColor = Color.FromHex("#ffffff");
                weekdays.TextColor = Color.FromHex("#2e74f0");
                weekends.BackgroundColor = Color.FromHex("#2e74f0");
                weekends.TextColor = Color.FromHex("#ffffff");
                StackLayout dayschedulelist1 = currentpage.FindByName<StackLayout>("dayschedulelist1");
                foreach (var item in dayschedulelist1.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == false)
                        {
                            cat.IsChecked = true;
                        }
                    }
                }
                StackLayout dayschedulelist2 = currentpage.FindByName<StackLayout>("dayschedulelist2");
                foreach (var item in dayschedulelist2.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == true)
                        {
                            cat.IsChecked = false;
                        }
                    }
                }
                StackLayout dayschedulelist3 = currentpage.FindByName<StackLayout>("dayschedulelist3");
                foreach (var item in dayschedulelist3.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == true)
                        {
                            cat.IsChecked = false;
                        }
                    }
                }
                StackLayout dayschedulelist4 = currentpage.FindByName<StackLayout>("dayschedulelist4");
                foreach (var item in dayschedulelist4.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == true)
                        {
                            cat.IsChecked = false;
                        }
                    }
                }
                StackLayout dayschedulelist5 = currentpage.FindByName<StackLayout>("dayschedulelist5");
                foreach (var item in dayschedulelist5.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == true)
                        {
                            cat.IsChecked = false;
                        }
                    }
                }
                StackLayout dayschedulelist6 = currentpage.FindByName<StackLayout>("dayschedulelist6");
                foreach (var item in dayschedulelist6.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == true)
                        {
                            cat.IsChecked = false;
                        }
                    }
                }
                StackLayout dayschedulelist7 = currentpage.FindByName<StackLayout>("dayschedulelist7");
                foreach (var item in dayschedulelist7.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == false)
                        {
                            cat.IsChecked = true;
                        }
                    }
                }
                checkedschedulevalue = "emsu,lmsu,easu,lasu,eesu,lesu,onsu,emsa,lmsa,easa,lasa,eesa,lesa,onsa";
                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public void uncheckcheckalldays()
        {
            try
            {
                dialog.ShowLoading("", null);
                Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                var currentpage = Getcurrentpage();
                Button checkall = currentpage.FindByName<Button>("checkall");
                Button uncheckall = currentpage.FindByName<Button>("uncheckall");
                Button weekdays = currentpage.FindByName<Button>("weekdays");
                Button weekends = currentpage.FindByName<Button>("weekends");
                checkall.BackgroundColor = Color.FromHex("#ffffff");
                checkall.TextColor = Color.FromHex("#2e74f0");
                uncheckall.BackgroundColor = Color.FromHex("#2e74f0");
                uncheckall.TextColor = Color.FromHex("#ffffff");
                weekdays.BackgroundColor = Color.FromHex("#ffffff");
                weekdays.TextColor = Color.FromHex("#2e74f0");
                weekends.BackgroundColor = Color.FromHex("#ffffff");
                weekends.TextColor = Color.FromHex("#2e74f0");
                StackLayout dayschedulelist1 = currentpage.FindByName<StackLayout>("dayschedulelist1");
                foreach (var item in dayschedulelist1.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == true)
                        {
                            cat.IsChecked = false;
                        }
                    }
                }
                StackLayout dayschedulelist2 = currentpage.FindByName<StackLayout>("dayschedulelist2");
                foreach (var item in dayschedulelist2.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == true)
                        {
                            cat.IsChecked = false;
                        }
                    }
                }
                StackLayout dayschedulelist3 = currentpage.FindByName<StackLayout>("dayschedulelist3");
                foreach (var item in dayschedulelist3.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == true)
                        {
                            cat.IsChecked = false;
                        }
                    }
                }
                StackLayout dayschedulelist4 = currentpage.FindByName<StackLayout>("dayschedulelist4");
                foreach (var item in dayschedulelist4.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == true)
                        {
                            cat.IsChecked = false;
                        }
                    }
                }
                StackLayout dayschedulelist5 = currentpage.FindByName<StackLayout>("dayschedulelist5");
                foreach (var item in dayschedulelist5.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == true)
                        {
                            cat.IsChecked = false;
                        }
                    }
                }
                StackLayout dayschedulelist6 = currentpage.FindByName<StackLayout>("dayschedulelist6");
                foreach (var item in dayschedulelist6.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == true)
                        {
                            cat.IsChecked = false;
                        }
                    }
                }
                StackLayout dayschedulelist7 = currentpage.FindByName<StackLayout>("dayschedulelist7");
                foreach (var item in dayschedulelist7.Children)
                {
                    if (item.GetType() == chbx.GetType())
                    {
                        var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                        if (cat.IsChecked == true)
                        {
                            cat.IsChecked = false;
                        }
                    }
                }
                checkedschedulevalue = "";
                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }

        public void showpaytypelist(Paytype data)
        {
            var curentpage = Getcurrentpage();
            var paytypeList = curentpage.FindByName<Helpers.ListviewScrollbar>("paytypelist");

            foreach (var item in paytypelistdata)
            {
                if (paytypetxt == item.paytype)
                {
                    item.checkimage = "RadioButtonChecked.png";
                }
                else
                {
                    item.checkimage = "RadioButtonUnChecked.png";
                }
            }
            paytypeList.ItemsSource = null;
            paytypeList.ItemsSource = paytypelistdata;
            contentviewvisible = true;
            paytypelistviewvisble = true;
            virtualdetailpopupvisible = false;
            establishyearlistviewvisble = false;
            paidexperiencelistviewvisble = false;
            replyperiodlistviewvisble = false;
            fromhourpickervisible = false;
            tohourpickervisible = false;

        }
        public void selectpaytypedata(Paytype data)
        {
            paytypetxt = data.paytype;
            paytypeid = data.paytypeid;
            contentviewvisible = false;
            var curntpg = Getcurrentpage();
            Label paytypetxtclr = curntpg.FindByName<Label>("Paytypetxt");
            paytypetxtclr.TextColor = Color.FromHex("#212121");
        }
        public void selecteddays(tset day)
        {
            dayID = day.tsetval;
            contentviewvisible = false;
            dayvalueID = day.tsetvalID;
            // establishyearlistviewvisble = false;
            // certificationscrolvisible = false;
            //// certificationlistviewvisble = false;
            // paidexperiencelistviewvisble = false;
            // replyperiodlistviewvisble = false;
            // daylistvisible = false;
            // fromhourpickervisible = false;
            // tohourpickervisible = false;

        }
        public void showdays(tset days)
        {
            try
            {
                var currentpage = Getcurrentpage();
                var dcdaylist = currentpage.FindByName<ListView>("dcdaylist");
                foreach (var data in DCdaylists)
                {
                    if (dayID == data.tsetval)
                    {
                        data.tsetvalimg = "RadioButtonChecked.png";
                    }
                    else
                    {
                        data.tsetvalimg = "RadioButtonUnChecked.png";
                    }
                }



                dcdaylist.ItemsSource = null;
                dcdaylist.ItemsSource = DCdaylists;
                contentviewvisible = true;
                paytypelistviewvisble = false;
                virtualdetailpopupvisible = false;
                establishyearlistviewvisble = false;
                paidexperiencelistviewvisble = false;
                replyperiodlistviewvisble = false;
                fromhourpickervisible = false;
                tohourpickervisible = false;
                carecenterhourslistvisible = true;
            }
            catch (Exception ex)
            {

            }
        }
        public void showfromtimepicker()
        {
            try
            {
                var currentpage = Getcurrentpage();
                var dcfromhourpickerlist = currentpage.FindByName<ListviewScrollbar>("dcfromhourpickerlist");
                foreach (var data in fromtimehourslist)
                {
                    if (fromhour == data.tsetval)
                    {
                        data.tsetvalimg = "RadioButtonChecked.png";
                    }
                    else
                    {
                        data.tsetvalimg = "RadioButtonUnChecked.png";
                    }
                }
                dcfromhourpickerlist.ItemsSource = null;
                dcfromhourpickerlist.ItemsSource = fromtimehourslist;
                contentviewvisible = true;
                fromhourpickervisible = true;
                paytypelistviewvisble = false;
                virtualdetailpopupvisible = false;
                establishyearlistviewvisble = false;
                paidexperiencelistviewvisble = false;
                replyperiodlistviewvisble = false;
                tohourpickervisible = false;
                carecenterhourslistvisible = false;
            }
            catch (Exception ex)
            {

            }

        }
        public void showtotimepicker()
        {
            try
            {
                var currentpage = Getcurrentpage();
                var dctohourpickerlist = currentpage.FindByName<ListviewScrollbar>("dctohourpickerlist");
                foreach (var data in totimehourslist)
                {
                    if (tohour == data.tsetval)
                    {
                        data.tsetvalimg = "RadioButtonChecked.png";
                    }
                    else
                    {
                        data.tsetvalimg = "RadioButtonUnChecked.png";
                    }
                }

                dctohourpickerlist.ItemsSource = null;
                dctohourpickerlist.ItemsSource = totimehourslist;
                contentviewvisible = true;
                tohourpickervisible = true;
                fromhourpickervisible = false;
                paytypelistviewvisble = false;
                virtualdetailpopupvisible = false;
                establishyearlistviewvisble = false;
                paidexperiencelistviewvisble = false;
                replyperiodlistviewvisble = false;
                carecenterhourslistvisible = false;
            }
            catch (Exception ex)
            {

            }

        }
        public void selectfromhour(tset data)
        {
            fromhour = data.tsetval;
            contentviewvisible = false;
        }
        public void selecttohour(tset data)
        {
            tohour = data.tsetval;
            contentviewvisible = false;
        }

        public void addhourstimings()
        {
            try
            {
                if (string.IsNullOrEmpty(dayID))
                {
                    dialog.Toast("Please select day format!");
                }
                //else if (string.IsNullOrEmpty(fromhour))
                //{
                //    dialog.Toast("Please select From hour!");
                //}
                //else if(string.IsNullOrEmpty(tohour))
                //{
                //    dialog.Toast("Please select To hour!");
                //}
                else
                {
                    if (dayID == "Weekends")//weekend - id=2
                    {
                        if (addtimingslist == null)
                        {
                            addtimingslist = new List<dchourtime>()
                    {
                        new dchourtime{ day="Saturday", fromhour = fromhour,tohour=tohour,hourID="2",dayidvalue="Sat"},
                        new dchourtime{ day="Sunday",fromhour=fromhour,tohour=tohour,hourID="2",dayidvalue="Sun"},
                    };

                        }
                        else
                        {
                            addtimingslist.Add(new dchourtime { day = "Saturday", fromhour = fromhour, tohour = tohour, hourID = "2", dayidvalue = "Sat" });
                            addtimingslist.Add(new dchourtime { day = "Sunday", fromhour = fromhour, tohour = tohour, hourID = "2", dayidvalue = "Sun" });

                            //  availabilitytable = "Sat::" + fromhour + "::" + tohour + ", Sun::" + fromhour + "::" + tohour;
                        }

                    }
                    else if (dayID == "Weekdays") // weekdays id-5
                    {
                        if (addtimingslist == null)
                        {
                            addtimingslist = new List<dchourtime>()
                    {
                        new dchourtime{ day="Monday",fromhour= fromhour ,tohour=tohour, hourID ="5" , dayidvalue="Mon"},
                        new dchourtime{ day="Tuesday",fromhour=fromhour,tohour=tohour,hourID="5",dayidvalue="Tue"},
                        new dchourtime{ day="Wednesday",fromhour=fromhour,tohour=tohour,hourID="5",dayidvalue="Wed"},
                        new dchourtime{ day="Thursday",fromhour=fromhour,tohour=tohour,hourID="5",dayidvalue="Thu"},
                        new dchourtime{ day="Friday",fromhour=fromhour,tohour=tohour,hourID="5",dayidvalue="Fri"},
                    };

                            // availabilitytable = Sun::12:00 am::12:00 am , Mon::12:00 am::12:00 am , Tue::12:00 am::12:00 am , Wed::12:00 am::12:00 am , Thu::12:00 am::12:00 am , Fri::12:00 am::12:00 am
                        }
                        else
                        {
                            addtimingslist.Add(new dchourtime { day = "Monday", fromhour = fromhour, tohour = tohour, hourID = "5", dayidvalue = "Mon" });
                            addtimingslist.Add(new dchourtime { day = "Tuesday", fromhour = fromhour, tohour = tohour, hourID = "5", dayidvalue = "Tue" });
                            addtimingslist.Add(new dchourtime { day = "Wednesday", fromhour = fromhour, tohour = tohour, hourID = "5", dayidvalue = "Wed" });
                            addtimingslist.Add(new dchourtime { day = "Thursday", fromhour = fromhour, tohour = tohour, hourID = "5", dayidvalue = "Thu" });
                            addtimingslist.Add(new dchourtime { day = "Friday", fromhour = fromhour, tohour = tohour, hourID = "5", dayidvalue = "Fri" });
                        }


                    }
                    else //days id=1
                    {
                        if (addtimingslist == null)
                        {
                            addtimingslist = new List<dchourtime>()
                    {
                        new dchourtime{day=dayID,fromhour=fromhour,tohour=tohour,hourID="1",dayidvalue=dayvalueID}
                    };
                        }
                        else
                        {
                            addtimingslist.Add(new dchourtime { day = dayID, fromhour = fromhour, tohour = tohour, hourID = "1", dayidvalue = dayvalueID });
                        }
                    }

                    var currentpage = Getcurrentpage();

                    HVScrollGridView addhourstimingslist = currentpage.FindByName<HVScrollGridView>("addhourstimingslist");
                    addhourstimingslist.ItemsSource = null;
                    addhourstimingslist.ItemsSource = addtimingslist;

                    availabitytabledata = new List<string>();
                    foreach (var data in addtimingslist)
                    {
                        string availablehourtxt = data.dayidvalue + "::" + fromhour + "::" + tohour;
                        if (availabitytabledata == null)
                        {
                            //availabitytabledata = new List<string>();
                            availabitytabledata.Add(availablehourtxt);
                        }
                        else
                        {
                            availabitytabledata.Add(availablehourtxt);
                        }
                    }
                    string[] availabilityarr = availabitytabledata.ToArray();
                    availabilitytable = string.Join(",", availabilityarr);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public List<dchourtime> copyaddtimingslist { get; set; }
        public void removehourstimings(dchourtime removedata)
        {
            try
            {
                dialog.ShowLoading();
                // copyaddtimingslist = addtimingslist;
                foreach (var data in addtimingslist)
                {
                    if (removedata.day == data.day && removedata.hourID == "2")
                    {
                        copyaddtimingslist = new List<dchourtime>()
                    {
                        //new dchourtime{day=dayID,fromhour=fromhour,tohour=tohour,hourID="1"}
                        data
                    };
                        //  copyaddtimingslist.Add(data);
                    }
                    else if (removedata.day == data.day && removedata.hourID == "5")
                    {
                        copyaddtimingslist = new List<dchourtime>()
                        {
                            data
                        };
                        // copyaddtimingslist.Add(data);
                    }
                    else
                    {
                        if (removedata.day == data.day)
                        {
                            copyaddtimingslist = new List<dchourtime>()
                            {
                            data
                            };
                        }
                    }
                }

                if (addtimingslist.Contains(copyaddtimingslist.Last()))
                {
                    addtimingslist.Remove(copyaddtimingslist.Last());
                }

                //addtimingslist = copyaddtimingslist;
                var currentpage = Getcurrentpage();
                HVScrollGridView addhourstimingslist = currentpage.FindByName<HVScrollGridView>("addhourstimingslist");
                addhourstimingslist.ItemsSource = null;
                addhourstimingslist.ItemsSource = addtimingslist;


                availabitytabledata = new List<string>();
                foreach (var data in addtimingslist)
                {
                    string availablehourtxt = data.dayidvalue + "::" + fromhour + "::" + tohour;
                    if (availabitytabledata == null)
                    {
                        // availabitytabledata = new List<string>();
                        availabitytabledata.Add(availablehourtxt);
                    }
                    else
                    {
                        availabitytabledata.Add(availablehourtxt);
                    }
                }
                string[] availabilityarr = availabitytabledata.ToArray();
                availabilitytable = string.Join(",", availabilityarr);


                dialog.HideLoading();
            }
            catch (Exception ex)
            {

            }
        }
        public void showstablishyearlist(tset data)
        {
            try
            {
                var curentpage = Getcurrentpage();


                foreach (var item in establishmentyearlist)
                {
                    if (establishyeartxtID == item.tsetval || (post.ismyneed == "1" && post.Experience == item.tsetval))
                    {
                        item.tsetvalimg = "RadioButtonChecked.png";
                    }
                    else
                    {
                        item.tsetvalimg = "RadioButtonUnChecked.png";
                    }
                }
                var establishyearlist = curentpage.FindByName<Helpers.ListviewScrollbar>("establishyearlist");
                establishyearlist.ItemsSource = null;
                establishyearlist.ItemsSource = establishmentyearlist;
                contentviewvisible = true;
                paytypelistviewvisble = false;
                virtualdetailpopupvisible = false;
                fromhourpickervisible = false;
                tohourpickervisible = false;
                establishyearlistviewvisble = true;
                carecenterhourslistvisible = false;
                paidexperiencelistviewvisble = false;
                replyperiodlistviewvisble = false;
            }
            catch (Exception ex)
            {

            }
        }
        public void selectestablishyeardata(tset data)
        {
            establishyeartxtID = data.tsetval;
            contentviewvisible = false;
            var curntpg = Getcurrentpage();
            Label paytypetxtclr = curntpg.FindByName<Label>("establishyeartxtID");
            paytypetxtclr.TextColor = Color.FromHex("#212121");
        }
        public void getactivityenrichmentdata()
        {
            try
            {
                //  dialog.ShowLoading();
                var currentpage = Getcurrentpage();
                var activitylayout = currentpage.FindByName<StackLayout>("activitylayout");
                //var DCapi = RestService.For<ICategory>(Commonsettings.LocaljobsAPI);
                //var list = await DCapi.getactivityenrichment();
                //getenrichmentdata = list.ROW_DATA;
                //if (getenrichmentdata != null)
                //{
                foreach (var data in activityenrichmentlist)
                {
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    chbx.Text = data.tsetval;//"Music &amp; Singing"
                    chbx.ClassId = "<answer>" + data.tsetval + "</answer>";
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_activityCheckChanged;
                    chbx.Padding = 5;
                    if (post.ismyneed == "1")
                    {
                        if (!string.IsNullOrEmpty(post.activityenrichments))
                        {
                            string[] activitytypelst = post.activityenrichments.Split(',');
                            foreach (var i in activitytypelst)
                            {
                                if (data.tsetval == i)
                                {
                                    chbx.IsChecked = true;
                                    checkedactivitypevalue += chbx.ClassId;
                                }
                            }
                        }
                    }

                    activitylayout.Children.Add(chbx);
                }
                // }
                //   dialog.HideLoading();
            }
            catch (Exception ex)
            {
            }
        }
        private void Chb_activityCheckChanged(object sender, EventArgs e)
        {
            try
            {
                var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
                if (category.IsChecked == true)
                {
                    if (checkedactivitypevalue != null && checkedactivitypevalue != "")
                    {
                        if (!checkedactivitypevalue.Contains(category.ClassId))
                        {
                            // checkedactivitypevalue += "," + category.ClassId;
                            checkedactivitypevalue += category.ClassId;
                        }
                    }
                    else
                    {
                        checkedactivitypevalue += category.ClassId;
                    }
                }
                else
                {
                    string[] indexvalue = checkedactivitypevalue.Split(',');
                    int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                    indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                    checkedactivitypevalue = string.Join("", indexvalue);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public void showreplyperiodlist()
        {
            var currentpage = Getcurrentpage();
            var replyperiodlistview = currentpage.FindByName<Helpers.ListviewScrollbar>("replyperiodlist");
            foreach (var item in replyperiodlist)
            {
                if (replyperiodtxtID == item.tsetval)
                {
                    item.tsetvalimg = "RadioButtonChecked.png";
                }
                else
                {
                    item.tsetvalimg = "RadioButtonUnChecked.png";
                }
            }
            replyperiodlistview.ItemsSource = null;
            replyperiodlistview.ItemsSource = replyperiodlist;
            contentviewvisible = true;
            paytypelistviewvisble = false;
            virtualdetailpopupvisible = false;
            fromhourpickervisible = false;
            tohourpickervisible = false;
            establishyearlistviewvisble = false;
            // certificationscrolvisible = false;
            //  certificationlistviewvisble = false;
            paidexperiencelistviewvisble = false;
            replyperiodlistviewvisble = true;
        }
        public void selectreplyperioddata(tset data)
        {
            try
            {
                //foreach (var item in replyperiodlist)
                //{
                //    if (item.tsetval == data.tsetval)
                //    {
                //        item.tsetvalimg = "RadioButtonChecked.png";
                //    }
                //    else
                //    {
                //        item.tsetvalimg = "RadioButtonUnChecked.png";
                //    }
                //}
                //var currentpg = Getcurrentpage();
                //var replyperiodlistview = currentpg.FindByName<Helpers.ListviewScrollbar>("replyperiodlist");
                //replyperiodlistview.ItemsSource = null;
                //replyperiodlistview.ItemsSource = replyperiodlist;


                replyperiodtxtID = data.tsetval;
                contentviewvisible = false;
                var currentpage = Getcurrentpage();
                Label replyperiodlbl = currentpage.FindByName<Label>("replyperiodtxt");
                replyperiodlbl.TextColor = Color.FromHex("#212121");
            }
            catch (Exception ex)
            {

            }
        }
        public void closecertificationlist()
        {
            if (string.IsNullOrEmpty(checkedcertificationvalue))
            {
                dialog.Toast("Kindly select the certification courses you have done...");
            }
            else
            {
                contentviewvisible = false;
            }
        }
        public void showcertifications()
        {
            chbxcontentviewvisible = true;
            // certificationlistviewvisble = true;
            // certificationscrolvisible = true;
            contentviewvisible = true;
            paidexperiencelistviewvisble = false;
            replyperiodlistviewvisble = false;
        }
        public void Closecontentview()
        {
            contentviewvisible = false;
        }
        public void showcontentview()
        {
            contentviewvisible = true;
        }


        public void showpaidexperiencelist(tset data)
        {
            try
            {
                dialog.ShowLoading("", null);
                var currentpage = Getcurrentpage();
                var paidexperiencelistview = currentpage.FindByName<Helpers.ListviewScrollbar>("paidexperiencelist");
                foreach (var item in paidexperiencelist)
                {
                    if (paidexperienceyrtxtID == item.tsetval)
                    {
                        item.tsetvalimg = "RadioButtonChecked.png";
                    }
                    else
                    {
                        item.tsetvalimg = "RadioButtonUnChecked.png";
                    }
                }

                paidexperiencelistview.ItemsSource = null;
                paidexperiencelistview.ItemsSource = paidexperiencelist;
                contentviewvisible = true;
                certificationscrolvisible = false;
                paytypelistviewvisble = false;
                virtualdetailpopupvisible = false;
                establishyearlistviewvisble = false;
                paidexperiencelistviewvisble = true;
                replyperiodlistviewvisble = false;
                fromhourpickervisible = false;
                tohourpickervisible = false;
                dialog.HideLoading();
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private string _workdescription = "";
        public string workdescription
        {
            get { return _workdescription; }
            set { _workdescription = value; OnPropertyChanged(nameof(workdescription)); }
        }
        private bool _workdescriptionvisible = false;
        public bool workdescriptionvisible
        {
            get { return _workdescriptionvisible; }
            set { _workdescriptionvisible = value; OnPropertyChanged(nameof(workdescriptionvisible)); }
        }
        private string _paidexperienceyrtxt = "";
        public string paidexperienceyrtxt
        {
            get { return _paidexperienceyrtxt; }
            set { _paidexperienceyrtxt = value; OnPropertyChanged(nameof(paidexperienceyrtxt)); }
        }
        public void selectpaidexperiencedata(tset data)
        {
            try
            {
                paidexperienceyrtxtID = data.tsetval;
                contentviewvisible = false;
                var curntpg = Getcurrentpage();
                Label paytypetxtclr = curntpg.FindByName<Label>("paidexperienceyrtxt");
                paytypetxtclr.Text = data.tsetval;
                paytypetxtclr.TextColor = Color.FromHex("#212121");
                string yearsofexp = data.tsetval;
                if (yearsofexp == "10+")
                {
                    yearsofexp = "10";
                }
                if (data.tsetval != "Beginner" && Convert.ToInt32(yearsofexp) >= 1 && Convert.ToInt32(yearsofexp) <= 10)
                {
                    workdescriptionvisible = true;
                }
                else
                {
                    workdescriptionvisible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private string _genderID = "";
        public string genderID
        {
            get { return _genderID; }
            set { _genderID = value; OnPropertyChanged(nameof(genderID)); }
        }
        private string _genderquestionID = "";
        public string genderquestionID
        {
            get { return _genderquestionID; }
            set { _genderquestionID = value; OnPropertyChanged(nameof(genderquestionID)); }
        }

        public void selectedgender(tset data)
        {
            foreach (var item in genderlist)
            {
                if (item.tsetval == data.tsetval)
                {
                    item.tsetvalimg = "RadioButtonChecked.png";
                    genderID = item.tsetval;
                }
                else
                {
                    item.tsetvalimg = "RadioButtonUnChecked.png";
                }
            }

            var currentpg = Getcurrentpage();
            ListView genderlistid = currentpg.FindByName<ListView>("genderlistid");
            genderlistid.ItemsSource = null;
            genderlistid.ItemsSource = genderlist;
        }

        private string _certificationquestionID = "";
        public string certificationquestionID
        {
            get { return _certificationquestionID; }
            set { _certificationquestionID = value; OnPropertyChanged(nameof(certificationquestionID)); }
        }
        private string _workdescquestionID = "";
        public string workdescquestionID
        {
            get { return _workdescquestionID; }
            set { _workdescquestionID = value; OnPropertyChanged(nameof(workdescquestionID)); }
        }
        private string _enrichmentquestionID = "";
        public string enrichmentquestionID
        {
            get { return _enrichmentquestionID; }
            set { _enrichmentquestionID = value; OnPropertyChanged(nameof(enrichmentquestionID)); }
        }
        private string _LanguagequestionID = "";
        public string LanguagequestionID
        {
            get { return _LanguagequestionID; }
            set { _LanguagequestionID = value; OnPropertyChanged(nameof(LanguagequestionID)); }
        }
        private string _paidexperiencequestionID = "";
        public string paidexperiencequestionID
        {
            get { return _paidexperiencequestionID; }
            set { _paidexperiencequestionID = value; OnPropertyChanged(nameof(paidexperiencequestionID)); }
        }
        private string _establishmentyearquestionID = "";
        public string establishmentyearquestionID
        {
            get { return _establishmentyearquestionID; }
            set { _establishmentyearquestionID = value; OnPropertyChanged(nameof(establishmentyearquestionID)); }
        }
        private bool _otherprovidervisible = false;
        public bool otherprovidervisible
        {
            get { return _otherprovidervisible; }
            set { _otherprovidervisible = value; OnPropertyChanged(nameof(otherprovidervisible)); }
        }
        private bool _fromtodatevisible = false;
        public bool fromtodatevisible
        {
            get { return _fromtodatevisible; }
            set { _fromtodatevisible = value; OnPropertyChanged(nameof(fromtodatevisible)); }
        }
        private bool _payratetypecisible = false;
        public bool payratetypecisible
        {
            get { return _payratetypecisible; }
            set { _payratetypecisible = value; OnPropertyChanged(nameof(payratetypecisible)); }
        }
        private bool _virtualnannylayout = false;
        public bool virtualnannylayout
        {
            get { return _virtualnannylayout; }
            set { _virtualnannylayout = value; OnPropertyChanged(nameof(virtualnannylayout)); }
        }
        private string _onlineservicetext = "";
        public string onlineservicetext
        {
            get { return _onlineservicetext; }
            set { _onlineservicetext = value; OnPropertyChanged(nameof(onlineservicetext)); }
        }
        private string _inpersonservicetext = "";
        public string inpersonservicetext
        {
            get { return _inpersonservicetext; }
            set { _inpersonservicetext = value; OnPropertyChanged(nameof(inpersonservicetext)); }
        }
        private int _onlineservicetextID = 0;
        public int onlineservicetextID
        {
            get { return _onlineservicetextID; }
            set { _onlineservicetextID = value; OnPropertyChanged(nameof(onlineservicetextID)); }
        }
        private string _onlineserviceimg = "RadioButtonUnChecked.png";
        public string onlineserviceimg
        {
            get { return _onlineserviceimg; }
            set { _onlineserviceimg = value; OnPropertyChanged(nameof(onlineserviceimg)); }
        }
        private string _inpersonserviceimg = "RadioButtonChecked.png";
        public string inpersonserviceimg
        {
            get { return _inpersonserviceimg; }
            set { _inpersonserviceimg = value; OnPropertyChanged(nameof(inpersonserviceimg)); }
        }

        private int _transportationtextID = 0;
        public int transportationtextID
        {
            get { return _transportationtextID; }
            set { _transportationtextID = value; OnPropertyChanged(nameof(transportationtextID)); }
        }
        private string _transportationONimg = "RadioButtonUnChecked.png";
        public string transportationONimg
        {
            get { return _transportationONimg; }
            set { _transportationONimg = value; OnPropertyChanged(nameof(transportationONimg)); }
        }
        private string _transportationOFFimg = "RadioButtonChecked.png";
        public string transportationOFFimg
        {
            get { return _transportationOFFimg; }
            set { _transportationOFFimg = value; OnPropertyChanged(nameof(transportationOFFimg)); }
        }

        private int _comfpetstextID = 0;
        public int comfpetstextID
        {
            get { return _comfpetstextID; }
            set { _comfpetstextID = value; OnPropertyChanged(nameof(comfpetstextID)); }
        }
        private string _comfpetsONimg = "RadioButtonUnChecked.png";
        public string comfpetsONimg
        {
            get { return _comfpetsONimg; }
            set { _comfpetsONimg = value; OnPropertyChanged(nameof(comfpetsONimg)); }
        }
        private string _comfpetsOFFimg = "RadioButtonChecked.png";
        public string comfpetsOFFimg
        {
            get { return _comfpetsOFFimg; }
            set { _comfpetsOFFimg = value; OnPropertyChanged(nameof(comfpetsOFFimg)); }
        }

        private int _smoketextID = 0;
        public int smoketextID
        {
            get { return _smoketextID; }
            set { _smoketextID = value; OnPropertyChanged(nameof(smoketextID)); }
        }
        private string _smokeONimg = "RadioButtonUnChecked.png";
        public string smokeONimg
        {
            get { return _smokeONimg; }
            set { _smokeONimg = value; OnPropertyChanged(nameof(smokeONimg)); }
        }
        private string _smokeOFFimg = "RadioButtonChecked.png";
        public string smokeOFFimg
        {
            get { return _smokeOFFimg; }
            set { _smokeOFFimg = value; OnPropertyChanged(nameof(smokeOFFimg)); }
        }

        private string _educationtextID = "Others";
        public string educationtextID
        {
            get { return _educationtextID; }
            set { _educationtextID = value; OnPropertyChanged(nameof(educationtextID)); }
        }
        private string _highschoolimg = "RadioButtonUnChecked.png";
        public string highschoolimg
        {
            get { return _highschoolimg; }
            set { _highschoolimg = value; OnPropertyChanged(nameof(highschoolimg)); }
        }
        private string _degreeimg = "RadioButtonUnChecked.png";
        public string degreeimg
        {
            get { return _degreeimg; }
            set { _degreeimg = value; OnPropertyChanged(nameof(degreeimg)); }
        }
        private string _highdegreeimg = "RadioButtonUnChecked.png";
        public string highdegreeimg
        {
            get { return _highdegreeimg; }
            set { _highdegreeimg = value; OnPropertyChanged(nameof(highdegreeimg)); }
        }
        private string _otherimg = "RadioButtonChecked.png";
        public string otherimg
        {
            get { return _otherimg; }
            set { _otherimg = value; OnPropertyChanged(nameof(otherimg)); }
        }
        //private int _inpersonservicetextID = 0;
        //public int inpersonservicetextID
        //{
        //    get { return _inpersonservicetextID; }
        //    set { _inpersonservicetextID = value; OnPropertyChanged(nameof(inpersonservicetextID)); }
        //}
        private bool _newqandvisible = false;
        public bool newqandvisible
        {
            get { return _newqandvisible; }
            set { _newqandvisible = value; OnPropertyChanged(nameof(newqandvisible)); }
        }

        public async void getpostfirstdata()
        {
            try
            {
                dialog.ShowLoading("", null);
                var DCapi = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);

                var list = await DCapi.getqandalist(post.businessid, "1");
                //  var list = await DCapi.getqandalist("254000");

                ListView qandalist = new ListView();

                XmlDocument doc = new XmlDocument();
                foreach (var data in list.ROW_DATA)
                {
                    if (data.questionid == "30")
                    {
                        workdescquestionID = "::" + data.questionid + "::" + data.questionorder;
                    }
                    if (!string.IsNullOrEmpty(data.xml_answers))
                    {
                        doc.LoadXml(data.xml_answers);
                        string jsonText = JsonConvert.SerializeXmlNode(doc);
                        XmlNode djkh = JsonConvert.DeserializeXmlNode(jsonText);
                        string val = djkh.InnerXml.Replace("</option><option>", ",").Replace("<option>", "").Replace("</option>", "").Replace("<options>", "").Replace("</options>", "");
                        string[] xmlval = System.Text.RegularExpressions.Regex.Split(val, ",");
                        if (data.questionid == "6" || data.questionid == "20")
                        {
                            gendertxtvisible = true;
                            gendertxt = data.question;
                            foreach (var j in xmlval)
                            {
                                if (j == "Any")
                                {
                                    //<answers><answer>Female</answer></answers>::6::2
                                    genderlist.Add(new tset { tsetval = j, tsetvalimg = "RadioButtonChecked.png" });
                                    genderID = j;
                                    genderquestionID = "::" + data.questionid + "::" + data.questionorder;
                                }
                                else
                                {
                                    if (j == "Female" && xmlval.Count() != 3)
                                    {
                                        genderlist.Add(new tset { tsetval = j, tsetvalimg = "RadioButtonChecked.png" });
                                        genderID = j;
                                        genderquestionID = "::" + data.questionid + "::" + data.questionorder;
                                    }
                                    else
                                    {
                                        genderlist.Add(new tset { tsetval = j, tsetvalimg = "RadioButtonUnChecked.png" });
                                        genderquestionID = "::" + data.questionid + "::" + data.questionorder;
                                    }
                                }
                            }
                            getgenderlists();
                        }
                        else if (data.questionid == "3" || data.questionid == "9" || data.questionid == "14")
                        {
                            qaexperiencetxt = data.question;
                            experiencelblvisible = true;
                            foreach (var j in xmlval)
                            {
                                paidexperiencelist.Add(new tset { tsetval = j });
                            }
                            paidexperiencequestionID = "::" + data.questionid + "::" + data.questionorder;
                        }
                        else if (data.questionid == "5" || data.questionid == "19")
                        {
                            languagelayoutvisible = true;
                            languageqatxt = data.question;
                            foreach (var j in xmlval)
                            {
                                languagelist.Add(new tset { tsetval = j });
                            }
                            LanguagequestionID = "::" + data.questionid + "::" + data.questionorder;
                            getlanguage("");
                        }
                        else if (data.questionid == "8")
                        {
                            certificationlistviewvisble = true;
                            certificationtxt = data.question;
                            foreach (var j in xmlval)
                            {
                                certificationlist.Add(new tset { tsetval = j });
                            }
                            certificationquestionID = "::" + data.questionid + "::" + data.questionorder;
                            getcertification();
                        }
                        else if (data.questionid == "42")
                        {
                            respondlblvisible = true;
                            replyperiodtxt = data.question;
                            foreach (var j in xmlval)
                            {
                                replyperiodlist.Add(new tset { tsetval = j });
                            }
                            replyperiodquestionID = "::" + data.questionid + "::" + data.questionorder;
                        }
                        else if (data.questionid == "43")
                        {
                            drivetxtvisible = true;
                            drivetxt = data.question;
                            foreach (var j in xmlval)
                            {
                                drivingYN.Add(new tset { tsetval = j, tsetvalID = "<answers><answer>" + j + "</answer></answers>::" + data.questionid + "::" + data.questionorder });
                            }
                            getdrivelist();
                        }
                        else if (data.questionid == "44")
                        {
                            vehicletxt = data.question;
                            vehicletxtvisible = true;
                            foreach (var j in xmlval)
                            {
                                vehicleYN.Add(new tset { tsetval = j, tsetvalID = "<answers><answer>" + j + "</answer></answers>::" + data.questionid + "::" + data.questionorder });
                            }
                            getvehiclelist();
                        }
                        else if (data.questionid == "45")
                        {
                            licensetxt = data.question;
                            licensetxtvisible = true;
                            foreach (var j in xmlval)
                            {
                                licenseYN.Add(new tset { tsetval = j, tsetvalID = "<answers><answer>" + j + "</answer></answers>::" + data.questionid + "::" + data.questionorder });
                            }
                            getlicenselist();
                        }

                        else if (data.questionid == "47")
                        {
                            activityenrichmntblkvisible = true;
                            activitytxt = data.question;
                            foreach (var j in xmlval)
                            {
                                activityenrichmentlist.Add(new tset { tsetval = j });
                            }
                            enrichmentquestionID = "::" + data.questionid + "::" + data.questionorder;
                            getactivityenrichmentdata();
                        }
                        else if (data.questionid == "35" || data.questionid == "13" || data.questionid == "11" || data.questionid == "12" || data.questionid == "14")
                        {
                            establismentyrblkvisible = true;
                            establishmentyrtxt = data.question;
                            foreach (var j in xmlval)
                            {
                                establishmentyearlist.Add(new tset { tsetval = j });
                                // familycareestablishmntyrlist.Add(new tset { tsetval = j });
                            }
                            establishmentyearquestionID = "::" + data.questionid + "::" + data.questionorder;
                        }
                        //else if (data.questionid == "12")
                        //{
                        //    foreach (var j in xmlval)
                        //    {
                        //        carecenteryearlist.Add(new tset { tsetval = j });
                        //    }
                        //}
                        //else if (data.questionid == "13")
                        //{
                        //    foreach (var j in xmlval)
                        //    {
                        //        eldercareyearlist.Add(new tset { tsetval = j });
                        //    }
                        //}
                        //else if (data.questionid == "11")
                        //{
                        //    foreach (var j in xmlval)
                        //    {
                        //        eldercarecenteryearlist.Add(new tset { tsetval = j });
                        //    }
                        //}
                        //else if (data.questionid == "14")
                        //{
                        //    foreach (var j in xmlval)
                        //    {
                        //        petcareyearlist.Add(new tset { tsetval = j });
                        //    }
                        //}
                        else if (data.questionid == "9")
                        {
                            foreach (var j in xmlval)
                            {
                                homecareexperiencelist.Add(new tset { tsetval = j });
                            }
                        }
                        //wanted posting 
                        else if (data.questionid == "18" || data.questionid == "22")
                        {
                            // Do you prefer Live out care provider also ?
                            liveoutprferencevisible = true;
                            liveoutprferencetxt = data.question;
                            foreach (var j in xmlval)
                            {
                                liveoutpreferencelist.Add(new tset { tsetval = j, tsetvalimg = "RadioButtonUnChecked.png" });
                            }
                            getliveoutpreferencelist();
                        }
                    }

                }
                //if(post.primarycategoryid=="27" || post.primarycategoryid == "28")
                //{
                //    getneedservice();
                //}
                if (post.primarycategoryid == "3" || post.primarycategoryid == "25")
                {
                    agegrouplblvisible = true;
                    getagegroupdata("");
                }
                var currentpage = Getcurrentpage();
                if (post.supercategoryid == "1")
                {
                    servicedatetxt = "Mention your availabilty";
                    fromdatetxt = "Available From";
                    todatetxt = "Available To";
                    agegrouplbltxt = "Which age groups are you experienced with?";
                    // rateplacehldrtxt = "Enter your ideal salary here";
                    rateplacehldrtxt = "Expected salary(in $)";

                    ListView gendrlst = currentpage.FindByName<ListView>("genderlistid");
                    gendrlst.HeightRequest = 70;
                }
                else
                {
                    servicedatetxt = "Service needed dates";
                    fromdatetxt = "From";
                    todatetxt = "To";
                    agegrouplbltxt = "Which age groups do you need care?";
                    //rateplacehldrtxt = "Enter your expected budget";
                    rateplacehldrtxt = "Expected budget";
                    ListView gendrlst = currentpage.FindByName<ListView>("genderlistid");
                    gendrlst.HeightRequest = 105;
                }
                if ((post.primarycategoryid != "45" || post.secondarycategoryid == "49") || (post.primarycategoryid == "27" || post.primarycategoryid == "28"))
                {
                    if (post.supercategoryid == "1")
                    {
                        needservicelbltxt = "What Services do you offer?";
                    }
                    else if (post.supercategoryid == "2")
                    {
                        needservicelbltxt = "What Services do you need?";
                    }
                    getneedservice();
                }
                if (post.secondarycategoryid == "9")
                {
                    //daycarehoursvisible
                    carecenterhoursvisible = true;
                }

                if ((post.secondarycategoryid == "9" && post.supercategoryid == "1") || (post.primarycategoryid == "4" && post.secondarycategoryid == "12") || (post.supercategoryid == "1" && post.secondarycategoryid == "14"))
                {
                    fromtodatevisible = false;
                    otherprovidervisible = false;
                    //dayschedulevisible = false;
                    payratetypecisible = false;
                }
                else
                {
                    fromtodatevisible = true;
                    otherprovidervisible = true;
                    //dayschedulevisible = true;
                    payratetypecisible = true;
                }
                if (post.supercategoryid == "1" && post.secondarycategoryid != "9")
                {
                    otherprovidervisible = true;
                }
                else
                {
                    otherprovidervisible = false;
                }

                if (post.secondarycategoryid == "49" && post.supercategoryid == "1")
                {
                    otherprovidervisible = false;
                    payratetypecisible = false;
                }
                if (post.secondarycategoryid == "9" && post.supercategoryid == "2")
                {
                    payratetypecisible = false;
                    carecenterhoursvisible = false;
                }
                showdayschedulelist();
                //virtualnanny
                if (post.supercategoryid == "1" && post.secondarycategoryid == "8")
                {
                    virtualnannylayout = true;
                    onlineservicetext = "Yes, I provide online service";
                    inpersonservicetext = "No, I don’t provide online service";
                }
                if (post.supercategoryid == "2" && (post.secondarycategoryid == "25" || post.secondarycategoryid == "8"))
                {
                    virtualnannylayout = true;
                    onlineservicetext = "This is a virtual job";
                    inpersonservicetext = "I need in-person services only";
                }

                if ((post.supercategoryid == "2" && post.primarycategoryid == "25") || (post.supercategoryid == "2" && post.primarycategoryid == "26"))
                {
                    var optDCapi = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                    var optlist = await DCapi.getqandalist(post.businessid, "0");
                    ListView optqandalist = new ListView();
                    XmlDocument optdoc = new XmlDocument();
                    foreach (var optdata in optlist.ROW_DATA)
                    {
                        if (!string.IsNullOrEmpty(optdata.xml_answers))
                        {
                            doc.LoadXml(optdata.xml_answers);
                            string jsonText = JsonConvert.SerializeXmlNode(doc);
                            XmlNode djkh = JsonConvert.DeserializeXmlNode(jsonText);
                            string val = djkh.InnerXml.Replace("</option><option>", ",").Replace("<option>", "").Replace("</option>", "").Replace("<options>", "").Replace("</options>", "");
                            string[] xmlval = System.Text.RegularExpressions.Regex.Split(val, ",");
                            if (optdata.questionid == "5" || optdata.questionid == "19")
                            {
                                languagelayoutvisible = true;
                                languageqatxt = optdata.question;
                                foreach (var j in xmlval)
                                {
                                    languagelist.Add(new tset { tsetval = j });
                                }
                                LanguagequestionID = "::" + optdata.questionid + "::" + optdata.questionorder;
                                getlanguage("");
                            }

                        }
                    }

                }


                getotherserviceprovider();



                //await Task.Delay(300);
                //dialog.HideLoading();

                //I need Care » Care for my Kids » Care Center
                //no qanda - wat service do you need - which age group you need care servie - availability from and to

                //I need Care » Care for my Pets » Pet Care Provider / Pet Care Center /Housekeeper/cook/Housekeeper & cook
                //wat service do you need -  What Services do you need? - needed from
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public Command checkalltap { get; set; }
        public Command uncheckalltap { get; set; }
        public Command weekdaystap { get; set; }
        public Command weekendstap { get; set; }
        public Command onlineservicetap { get; set; }
        public Command transportationservicetap { get; set; }
        public Command comfpetstap { get; set; }
        public Command dousmoketap { get; set; }
        public Command highschooltap { get; set; }
        public Command degreetap { get; set; }
        public Command highdegreetap { get; set; }
        public Command otherstap { get; set; }
        public Command taponvirtualcheckdtls { get; set; }


        public List<DC_Availability> dayschedulelist1 { get; set; }
        public List<DC_Availability> dayschedulelist2 { get; set; }
        public List<DC_Availability> dayschedulelist3 { get; set; }
        public List<DC_Availability> dayschedulelist4 { get; set; }
        public List<DC_Availability> dayschedulelist5 { get; set; }
        public List<DC_Availability> dayschedulelist6 { get; set; }
        public List<DC_Availability> dayschedulelist7 { get; set; }

        public List<DC_Availability> dayscheduleskiplist1 { get; set; }
        public List<DC_Availability> dayscheduleskiplist2 { get; set; }
        public List<DC_Availability> dayscheduleskiplist3 { get; set; }
        public List<DC_Availability> dayscheduleskiplist4 { get; set; }
        public List<DC_Availability> dayscheduleskiplist5 { get; set; }
        public List<DC_Availability> dayscheduleskiplist6 { get; set; }

        public void showdayschedulelist()
        {
            try
            {
                double lstcnt = availablescheulelist.Count;
                //double halfcnt = Math.Round(lstcnt / 4, 1);
                dayschedulelist1 = availablescheulelist.Take(7).ToList(); //Convert.ToInt32(halfcnt)).ToList();
                dayscheduleskiplist1 = availablescheulelist.Skip(7).ToList();
                dayschedulelist2 = dayscheduleskiplist1.Take(7).ToList();

                dayscheduleskiplist2 = dayscheduleskiplist1.Skip(7).ToList();
                dayschedulelist3 = dayscheduleskiplist2.Take(7).ToList();
                dayscheduleskiplist3 = dayscheduleskiplist2.Skip(7).ToList();
                dayschedulelist4 = dayscheduleskiplist3.Take(7).ToList();
                dayscheduleskiplist4 = dayscheduleskiplist3.Skip(7).ToList();
                dayschedulelist5 = dayscheduleskiplist4.Take(7).ToList();
                dayscheduleskiplist5 = dayscheduleskiplist4.Skip(7).ToList();
                dayschedulelist6 = dayscheduleskiplist5.Take(7).ToList();
                dayschedulelist7 = dayscheduleskiplist5.Skip(7).ToList();

                foreach (var data in dayschedulelist1)
                {
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    //chbx.BoxSizeRequest = 19;
                    chbx.ClassId = data.avalilabilityID;
                    chbx.Padding = new Thickness(0, 0, 0, 0);
                    chbx.Margin = new Thickness(0, 0, 0, 0);
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_visatypeCheckChanged;
                    if (post.ismyneed == "1")
                    {
                        if (!string.IsNullOrEmpty(post.Availability))
                        {
                            string[] datelst = post.Availability.Split(',');
                            foreach (var i in datelst)
                            {
                                if (chbx.ClassId == i)
                                {
                                    chbx.IsChecked = true;
                                    if (string.IsNullOrEmpty(checkedschedulevalue))
                                    {
                                        checkedschedulevalue += chbx.ClassId;
                                    }
                                    else
                                    {
                                        checkedschedulevalue += "," + chbx.ClassId;
                                    }
                                }
                            }
                        }
                    }
                    var currentpg = Getcurrentpage();
                    StackLayout dayschedulelistt = currentpg.FindByName<StackLayout>("dayschedulelist1");
                    dayschedulelistt.Children.Add(chbx);
                    // Visatypelayout.Children.Add(boxx);
                }
                foreach (var data in dayschedulelist2)
                {

                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    //chbx.BoxSizeRequest = 19;
                    chbx.ClassId = data.avalilabilityID;
                    //chbx.ShowLabel = true;
                    chbx.Padding = new Thickness(0, 0, 0, 0);
                    chbx.Margin = new Thickness(0, 0, 0, 0);
                    chbx.CheckChanged += Chb_visatypeCheckChanged;
                    if (post.ismyneed == "1")
                    {
                        if (!string.IsNullOrEmpty(post.Availability))
                        {
                            string[] datelst = post.Availability.Split(',');
                            foreach (var i in datelst)
                            {
                                if (chbx.ClassId == i)
                                {
                                    chbx.IsChecked = true;
                                    if (string.IsNullOrEmpty(checkedschedulevalue))
                                    {
                                        checkedschedulevalue += chbx.ClassId;
                                    }
                                    else
                                    {
                                        checkedschedulevalue += "," + chbx.ClassId;
                                    }
                                }
                            }
                        }
                    }
                    var currentpg = Getcurrentpage();
                    StackLayout dayschedulelistt = currentpg.FindByName<StackLayout>("dayschedulelist2");
                    dayschedulelistt.Children.Add(chbx);
                }
                foreach (var data in dayschedulelist3)
                {

                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    //chbx.BoxSizeRequest = 19;
                    chbx.ClassId = data.avalilabilityID;
                    chbx.Padding = new Thickness(0, 0, 0, 0);
                    chbx.Margin = new Thickness(0, 0, 0, 0);
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_visatypeCheckChanged;
                    if (post.ismyneed == "1")
                    {
                        if (!string.IsNullOrEmpty(post.Availability))
                        {
                            string[] datelst = post.Availability.Split(',');
                            foreach (var i in datelst)
                            {
                                if (chbx.ClassId == i)
                                {
                                    chbx.IsChecked = true;
                                    if (string.IsNullOrEmpty(checkedschedulevalue))
                                    {
                                        checkedschedulevalue += chbx.ClassId;
                                    }
                                    else
                                    {
                                        checkedschedulevalue += "," + chbx.ClassId;
                                    }
                                }
                            }
                        }
                    }
                    var currentpg = Getcurrentpage();
                    StackLayout dayschedulelistt = currentpg.FindByName<StackLayout>("dayschedulelist3");
                    dayschedulelistt.Children.Add(chbx);
                }
                foreach (var data in dayschedulelist4)
                {
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    //chbx.BoxSizeRequest = 19;
                    chbx.Padding = new Thickness(0, 0, 0, 0);
                    chbx.Margin = new Thickness(0, 0, 0, 0);
                    chbx.ClassId = data.avalilabilityID;
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_visatypeCheckChanged;
                    if (post.ismyneed == "1")
                    {
                        if (!string.IsNullOrEmpty(post.Availability))
                        {
                            string[] datelst = post.Availability.Split(',');
                            foreach (var i in datelst)
                            {
                                if (chbx.ClassId == i)
                                {
                                    chbx.IsChecked = true;
                                    if (string.IsNullOrEmpty(checkedschedulevalue))
                                    {
                                        checkedschedulevalue += chbx.ClassId;
                                    }
                                    else
                                    {
                                        checkedschedulevalue += "," + chbx.ClassId;
                                    }
                                }
                            }
                        }
                    }
                    var currentpg = Getcurrentpage();
                    StackLayout dayschedulelistt = currentpg.FindByName<StackLayout>("dayschedulelist4");
                    dayschedulelistt.Children.Add(chbx);
                }
                foreach (var data in dayschedulelist5)
                {
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    //chbx.BoxSizeRequest = 19;
                    chbx.ClassId = data.avalilabilityID;
                    //chbx.ShowLabel = true;
                    chbx.Padding = new Thickness(0, 0, 0, 0);
                    chbx.Margin = new Thickness(0, 0, 0, 0);
                    chbx.CheckChanged += Chb_visatypeCheckChanged;
                    if (post.ismyneed == "1")
                    {
                        if (!string.IsNullOrEmpty(post.Availability))
                        {
                            string[] datelst = post.Availability.Split(',');
                            foreach (var i in datelst)
                            {
                                if (chbx.ClassId == i)
                                {
                                    chbx.IsChecked = true;
                                    if (string.IsNullOrEmpty(checkedschedulevalue))
                                    {
                                        checkedschedulevalue += chbx.ClassId;
                                    }
                                    else
                                    {
                                        checkedschedulevalue += "," + chbx.ClassId;
                                    }
                                }
                            }
                        }
                    }
                    var currentpg = Getcurrentpage();
                    StackLayout dayschedulelistt = currentpg.FindByName<StackLayout>("dayschedulelist5");
                    dayschedulelistt.Children.Add(chbx);
                }
                foreach (var data in dayschedulelist6)
                {
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    //chbx.BoxSizeRequest = 19;
                    chbx.ClassId = data.avalilabilityID;
                    //chbx.ShowLabel = true;
                    chbx.Padding = new Thickness(0, 0, 0, 0);
                    chbx.Margin = new Thickness(0, 0, 0, 0);
                    chbx.CheckChanged += Chb_visatypeCheckChanged;
                    if (post.ismyneed == "1")
                    {
                        if (!string.IsNullOrEmpty(post.Availability))
                        {
                            string[] datelst = post.Availability.Split(',');
                            foreach (var i in datelst)
                            {
                                if (chbx.ClassId == i)
                                {
                                    chbx.IsChecked = true;
                                    if (string.IsNullOrEmpty(checkedschedulevalue))
                                    {
                                        checkedschedulevalue += chbx.ClassId;
                                    }
                                    else
                                    {
                                        checkedschedulevalue += "," + chbx.ClassId;
                                    }
                                }
                            }
                        }
                    }
                    var currentpg = Getcurrentpage();
                    StackLayout dayschedulelistt = currentpg.FindByName<StackLayout>("dayschedulelist6");
                    dayschedulelistt.Children.Add(chbx);
                }
                foreach (var data in dayschedulelist7)
                {
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    //chbx.BoxSizeRequest = 19;
                    chbx.ClassId = data.avalilabilityID;
                    //chbx.ShowLabel = true;
                    chbx.Padding = new Thickness(0, 0, 0, 0);
                    chbx.Margin = new Thickness(0, 0, 0, 0);
                    chbx.CheckChanged += Chb_visatypeCheckChanged;
                    if (post.ismyneed == "1")
                    {
                        if (!string.IsNullOrEmpty(post.Availability))
                        {
                            string[] datelst = post.Availability.Split(',');
                            foreach (var i in datelst)
                            {
                                if (chbx.ClassId == i)
                                {
                                    chbx.IsChecked = true;
                                    if (string.IsNullOrEmpty(checkedschedulevalue))
                                    {
                                        checkedschedulevalue += chbx.ClassId;
                                    }
                                    else
                                    {
                                        checkedschedulevalue += "," + chbx.ClassId;
                                    }
                                }
                            }
                        }
                    }
                    var currentpg = Getcurrentpage();
                    StackLayout dayschedulelistt = currentpg.FindByName<StackLayout>("dayschedulelist7");
                    dayschedulelistt.Children.Add(chbx);
                }

            }
            catch (Exception ex)
            {

            }
        }
        private string _checkedschedulevalue = "";
        public string checkedschedulevalue
        {
            get { return _checkedschedulevalue; }
            set { _checkedschedulevalue = value; OnPropertyChanged(nameof(checkedschedulevalue)); }
        }
        private void Chb_visatypeCheckChanged(object sender, EventArgs e)
        {
            try
            {
                var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
                if (category.IsChecked == true)
                {
                    if (checkedschedulevalue != null && checkedschedulevalue != "")
                    {
                        if (!checkedschedulevalue.Contains(category.ClassId))
                        {
                            checkedschedulevalue += "," + category.ClassId;
                        }
                    }
                    else
                    {
                        checkedschedulevalue += category.ClassId;
                    }
                }
                else
                {
                    string[] indexvalue = checkedschedulevalue.Split(',');
                    int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                    indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                    checkedschedulevalue = string.Join(",", indexvalue);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public void showstablishyearlist(Carecenteryear data)
        {
            var curentpage = Getcurrentpage();
            var establishyearlistview = curentpage.FindByName<Helpers.ListviewScrollbar>("establishyearlist");

            foreach (var item in establishmentyearlist)
            {
                if (establishyeartxtID == item.tsetval)
                {
                    item.tsetvalimg = "RadioButtonChecked.png";
                }
                else
                {
                    item.tsetvalimg = "RadioButtonUnChecked.png";
                }
            }
            establishyearlistview.ItemsSource = null;
            establishyearlistview.ItemsSource = establishmentyearlist;
            contentviewvisible = true;
            paytypelistviewvisble = false;
            virtualdetailpopupvisible = false;
            establishyearlistviewvisble = true;
            paidexperiencelistviewvisble = false;
            replyperiodlistviewvisble = false;
            fromhourpickervisible = false;
            tohourpickervisible = false;
        }
        public void getdrivelist()
        {
            try
            {
                var currentpg = Getcurrentpage();
                ListView drivelistid = currentpg.FindByName<ListView>("drivelistid");
                foreach (var item in drivingYN)
                {
                    if (item.tsetval.ToLower() == "no")
                    {
                        item.tsetvalimg = "RadioButtonChecked.png";
                        drivetxtID = item.tsetvalID;
                    }
                    else
                    {
                        item.tsetvalimg = "RadioButtonUnChecked.png";
                    }
                }
                drivelistid.ItemsSource = drivingYN;
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private string _drivetxtID = "";
        public string drivetxtID
        {
            get { return _drivetxtID; }
            set { _drivetxtID = value; OnPropertyChanged(nameof(drivetxtID)); }
        }
        public void selecteddriveyn(tset data)
        {
            foreach (var item in drivingYN)
            {
                if (item.tsetval == data.tsetval)
                {
                    item.tsetvalimg = "RadioButtonChecked.png";
                    drivetxtID = item.tsetvalID;
                }
                else
                {
                    item.tsetvalimg = "RadioButtonUnChecked.png";
                }
            }
            var currentpg = Getcurrentpage();
            ListView drivelistid = currentpg.FindByName<ListView>("drivelistid");
            drivelistid.ItemsSource = null;
            drivelistid.ItemsSource = drivingYN;
        }
        private string _vehicletxtID = "";
        public string vehicletxtID
        {
            get { return _vehicletxtID; }
            set { _vehicletxtID = value; OnPropertyChanged(nameof(vehicletxtID)); }
        }
        public void getvehiclelist()
        {
            var currentpg = Getcurrentpage();
            ListView vehiclelistid = currentpg.FindByName<ListView>("vehiclelistid");
            foreach (var item in vehicleYN)
            {
                if (item.tsetval.ToLower() == "no")
                {
                    item.tsetvalimg = "RadioButtonChecked.png";
                    vehicletxtID = item.tsetvalID;
                }
                else
                {
                    item.tsetvalimg = "RadioButtonUnChecked.png";
                }
            }
            vehiclelistid.ItemsSource = vehicleYN;
        }
        public void selectedvehicleyn(tset data)
        {
            foreach (var item in vehicleYN)
            {
                if (item.tsetval == data.tsetval)
                {
                    item.tsetvalimg = "RadioButtonChecked.png";
                    vehicletxtID = item.tsetvalID;
                }
                else
                {
                    item.tsetvalimg = "RadioButtonUnChecked.png";
                }
            }
            var currentpg = Getcurrentpage();
            ListView vehiclelistid = currentpg.FindByName<ListView>("vehiclelistid");
            vehiclelistid.ItemsSource = null;
            vehiclelistid.ItemsSource = vehicleYN;
        }
        private string _licensetxtID = "";
        public string licensetxtID
        {
            get { return _licensetxtID; }
            set { _licensetxtID = value; OnPropertyChanged(nameof(licensetxtID)); }
        }
        public void getlicenselist()
        {
            var currentpg = Getcurrentpage();
            ListView licenselistid = currentpg.FindByName<ListView>("licenselistid");
            foreach (var item in licenseYN)
            {
                if (item.tsetval.ToLower() == "no")
                {
                    item.tsetvalimg = "RadioButtonChecked.png";
                    licensetxtID = item.tsetvalID;
                }
                else
                {
                    item.tsetvalimg = "RadioButtonUnChecked.png";
                }
            }
            licenselistid.ItemsSource = licenseYN;
        }
        public void selectedlicenseyn(tset data)
        {
            foreach (var item in licenseYN)
            {
                if (item.tsetval == data.tsetval)
                {
                    item.tsetvalimg = "RadioButtonChecked.png";
                    licensetxtID = item.tsetvalID;
                }
                else
                {
                    item.tsetvalimg = "RadioButtonUnChecked.png";
                }
            }
            var currentpg = Getcurrentpage();
            ListView licenselistid = currentpg.FindByName<ListView>("licenselistid");
            licenselistid.ItemsSource = null;
            licenselistid.ItemsSource = licenseYN;
        }
        //public void getreplyperiod(Replyperiod data)
        public void getreplyperiod()
        {
            var currentpage = Getcurrentpage();
            var replyperiodlistview = currentpage.FindByName<Helpers.ListviewScrollbar>("replyperiodlist");

            foreach (var item in replyperiodlist)
            {
                if (replyperiodtxtID == item.tsetval)
                {
                    item.tsetvalimg = "RadioButtonChecked.png";
                }
                else
                {
                    item.tsetvalimg = "RadioButtonUnChecked.png";
                }
            }
            replyperiodlistview.ItemsSource = null;
            replyperiodlistview.ItemsSource = replyperiodlist;
            contentviewvisible = true;
            paytypelistviewvisble = false;
            virtualdetailpopupvisible = false;
            establishyearlistviewvisble = false;
            paidexperiencelistviewvisble = false;
            replyperiodlistviewvisble = true;
            fromhourpickervisible = false;
            tohourpickervisible = false;
        }
        public async void getcertification()
        {

            try
            {
                await Task.Delay(1000);
                var currentpage = Getcurrentpage();
                var certificationlayout = currentpage.FindByName<StackLayout>("certificationlayout");
                foreach (var dta in certificationlist)
                {
                    BoxView boxx = new BoxView();
                    boxx.BackgroundColor = Color.FromHex("e0e0e0");
                    boxx.HeightRequest = 1;
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    // chbx.Size = 20;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    chbx.Text = dta.tsetval;
                    chbx.ClassId = "<answer>" + dta.tsetval + "</answer>";
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_certificationCheckChanged;
                    chbx.Padding = 5;
                    if (post.ismyneed == "1")
                    {
                        if (!string.IsNullOrEmpty(post.certifications))
                        {
                            string[] certificationtypelst = post.certifications.Split(',');
                            foreach (var i in certificationtypelst)
                            {
                                if (dta.tsetval == i)
                                {
                                    chbx.IsChecked = true;
                                    if (string.IsNullOrEmpty(checkedagegrouptypevalue))
                                    {
                                        _checkedcertificationvalue += chbx.ClassId;
                                    }
                                    else
                                    {
                                        _checkedcertificationvalue += "," + chbx.ClassId;
                                    }
                                }
                            }
                        }
                    }
                    certificationlayout.Children.Add(chbx);
                    certificationlayout.Children.Add(boxx);

                }
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private void Chb_certificationCheckChanged(object sender, EventArgs e)
        {
            try
            {
                var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
                if (category.IsChecked == true)
                {
                    if (_checkedcertificationvalue != null && _checkedcertificationvalue != "")
                    {
                        if (!_checkedcertificationvalue.Contains(category.ClassId))
                        {
                            //_checkedcertificationvalue += "," + category.ClassId;
                            _checkedcertificationvalue += category.ClassId;
                        }
                    }
                    else
                    {
                        // _checkedcertificationvalue += "<answers>"+category.ClassId+"</answers>";
                        _checkedcertificationvalue += category.ClassId;
                    }
                }
                else
                {
                    string[] indexvalue = _checkedcertificationvalue.Split(',');
                    int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                    indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                    _checkedcertificationvalue = string.Join("", indexvalue);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public void getgenderlists()
        {
            var currentpg = Getcurrentpage();
            ListView genderlistid = currentpg.FindByName<ListView>("genderlistid");
            if (post.ismyneed == "1")
            {
                foreach (var dta in genderlist)
                {
                    if (post.gender == dta.tsetval)
                    {
                        dta.tsetvalimg = "RadioButtonChecked.png";
                    }
                    else
                    {
                        dta.tsetvalimg = "RadioButtonUnChecked.png";
                    }
                }
            }

            genderlistid.ItemsSource = genderlist;
        }
        public void getliveoutpreferencelist()
        {
            var currentpg = Getcurrentpage();
            ListView liveoutpreferencelst = currentpg.FindByName<ListView>("liveoutpreferencelist");
            liveoutpreferencelst.ItemsSource = liveoutpreferencelist;
        }
        public void selectedliveoutpreference(tset data)
        {
            try
            {
                foreach (var item in liveoutpreferencelist)
                {
                    if (item.tsetval == data.tsetval)
                    {
                        item.tsetvalimg = "RadioButtonChecked.png";
                        liveoutprferencetxtID = item.tsetval;
                    }
                    else
                    {
                        item.tsetvalimg = "RadioButtonUnChecked.png";
                    }
                }
                var currentpg = Getcurrentpage();
                ListView liveoutpreferencelst = currentpg.FindByName<ListView>("liveoutpreferencelist");
                liveoutpreferencelst.ItemsSource = null;
                liveoutpreferencelst.ItemsSource = liveoutpreferencelist;
            }
            catch (Exception ex)
            {

            }
        }

        public async void getagegroupdata(string agegroup)
        {
            try
            {
                //  dialog.ShowLoading("", null);
                await Task.Delay(500);
                var dcapi = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                careservice agegroupdata = await dcapi.getagegroup();
                agegrouplstdata = agegroupdata.ROW_DATA;
                //agegrouplist = agegrouplistdata;

                foreach (var data in agegrouplstdata)
                {
                    BoxView boxx = new BoxView();
                    boxx.BackgroundColor = Color.FromHex("e0e0e0");
                    boxx.HeightRequest = 1;
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    chbx.Text = data.primarytag;
                    //chbx.ClassId = data.primarytagid.ToString();
                    chbx.ClassId = data.primarytagid.ToString() + "::" + data.primarytag + "::" + data.primarytagurl;
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_agegrouptypeCheckChanged;
                    chbx.Padding = 5;
                    if (post.ismyneed == "1")
                    {
                        if (!string.IsNullOrEmpty(post.agegroups))
                        {
                            string[] agetypelst = post.agegroups.Split(',');
                            foreach (var i in agetypelst)
                            {
                                if (data.primarytag == i)
                                {
                                    chbx.IsChecked = true;
                                    if (string.IsNullOrEmpty(checkedagegrouptypevalue))
                                    {
                                        checkedagegrouptypevalue += chbx.ClassId;
                                    }
                                    else
                                    {
                                        checkedagegrouptypevalue += "," + chbx.ClassId;
                                    }
                                }
                            }
                        }
                    }

                    var currentpage = Getcurrentpage();
                    StackLayout agegrouplayout = currentpage.FindByName<StackLayout>("agegrouplayout");
                    agegrouplayout.Children.Add(chbx);
                    agegrouplayout.Children.Add(boxx);

                }
                //}
                // dialog.HideLoading();
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }

        }

        private void Chb_agegrouptypeCheckChanged(object sender, EventArgs e)
        {
            try
            {
                var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
                if (category.IsChecked == true)
                {
                    if (checkedagegrouptypevalue != null && checkedagegrouptypevalue != "")
                    {
                        if (!checkedagegrouptypevalue.Contains(category.ClassId))
                        {
                            checkedagegrouptypevalue += "," + category.ClassId;
                        }
                    }
                    else
                    {
                        checkedagegrouptypevalue += category.ClassId;
                    }
                }
                else
                {
                    string[] indexvalue = checkedagegrouptypevalue.Split(',');
                    int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                    indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                    checkedagegrouptypevalue = string.Join(",", indexvalue);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

            }
        }
        public async void getlanguage(string language)
        {
            try
            {
                await Task.Delay(1000);
                languagelstview = languagelist;

                var currentpage = Getcurrentpage();
                var languagelayout = currentpage.FindByName<StackLayout>("languagelayout");
                foreach (var dta in languagelstview)
                {
                    BoxView boxx = new BoxView();
                    boxx.BackgroundColor = Color.FromHex("e0e0e0");
                    boxx.HeightRequest = 1;
                    Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                    // chbx.Size = 20;
                    //chbx.OnImg = "CheckBoxChecked.png";
                    //chbx.OffImg = "CheckBoxUnChecked.png";
                    chbx.Text = dta.tsetval;
                    chbx.ClassId = "<answer>" + dta.tsetval + "</answer>";
                    //chbx.ShowLabel = true;
                    chbx.CheckChanged += Chb_languageCheckChanged;
                    chbx.Padding = 5;
                    if (post.ismyneed == "1")
                    {
                        if (!string.IsNullOrEmpty(post.languages))
                        {
                            string[] languagetypelst = post.languages.Split(',');
                            foreach (var i in languagetypelst)
                            {
                                if (dta.tsetval == i)
                                {
                                    chbx.IsChecked = true;
                                    if (string.IsNullOrEmpty(checkedlanguagevalue))
                                    {
                                        checkedlanguagevalue += chbx.ClassId;
                                    }
                                    else
                                    {
                                        checkedlanguagevalue += "," + chbx.ClassId;
                                    }
                                }
                            }
                        }
                    }

                    languagelayout.Children.Add(chbx);
                    // languagelayout.Children.Add(boxx);
                }
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private void Chb_languageCheckChanged(object sender, EventArgs e)
        {
            try
            {

                var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
                if (category.Text == "Add More")
                {
                    if (category.IsChecked == true == true)
                    {
                        Addmorelanguagetext = "";
                        addmorelanguagevisible = true;
                        addlanguagebtnvisible = true;
                    }
                    else
                    {
                        string txt = "<answer>" + Addmorelanguagetext + "</answer>";
                        checkedlanguagevalue.Replace(txt, "");
                        //Addmorelanguagetext = "";
                        addmorelanguagevisible = false;
                        addlanguagebtnvisible = false;
                    }
                }
                else
                {
                    // addmorelanguagevisible = false;
                    if (category.IsChecked == true)
                    {
                        if (checkedlanguagevalue != null && checkedlanguagevalue != "")
                        {
                            if (!checkedlanguagevalue.Contains(category.ClassId))
                            {
                                checkedlanguagevalue += "," + category.ClassId;
                            }
                        }
                        else
                        {
                            checkedlanguagevalue += category.ClassId;
                        }
                    }
                    else
                    {
                        string[] indexvalue = checkedlanguagevalue.Split(',');
                        int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                        indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                        checkedlanguagevalue = string.Join("", indexvalue);
                    }


                }

            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private bool _addlanguagebtnvisible = false;
        public bool addlanguagebtnvisible
        {
            get { return _addlanguagebtnvisible; }
            set { _addlanguagebtnvisible = value; OnPropertyChanged(nameof(addlanguagebtnvisible)); }
        }
        private string _addedcheckedlanguagetxt = "";
        public string addedcheckedlanguagetxt
        {
            get { return _addedcheckedlanguagetxt; }
            set { _addedcheckedlanguagetxt = value; OnPropertyChanged(nameof(addedcheckedlanguagetxt)); }
        }

        public List<string> removestringtxtlst = new List<string>();
        public void addlanguage()
        {
            try
            {

                if (addlanguagebtntxt == "Add")
                {

                    if (Addmorelanguagetext.Trim().Length != 0)
                    {
                        addlanguagebtntxt = "Remove";
                        if (Addmorelanguagetext.Contains(","))
                        {
                            string[] txtlanguage = Addmorelanguagetext.Split(',');
                            foreach (var data in txtlanguage)
                            {
                                // Addmorelanguagetext += "<answers>" + data + "</answers>";
                                checkedlanguagevalue += ",<answer>" + data + "</answer>";
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(checkedlanguagevalue))
                            {
                                checkedlanguagevalue += "<answer>" + Addmorelanguagetext + "</answer>";
                                addedcheckedlanguagetxt = "<answer>" + Addmorelanguagetext + "</answer>";
                            }
                            else
                            {
                                checkedlanguagevalue += ",<answer>" + Addmorelanguagetext + "</answer>";
                                addedcheckedlanguagetxt = ",<answer>" + Addmorelanguagetext + "</answer>";
                            }

                        }
                    }
                    else
                    {
                        dialog.Toast("Enter a language to add");
                    }
                    //addlanguagebtnvisible = false;
                }
                else if (addlanguagebtntxt == "Remove")
                {
                    addlanguagebtntxt = "Add";
                    if (Addmorelanguagetext.Trim().Length != 0)
                    {
                        if (!string.IsNullOrEmpty(addedcheckedlanguagetxt))
                        {
                            checkedlanguagevalue = checkedlanguagevalue.Replace(addedcheckedlanguagetxt, "");
                        }

                        if (Addmorelanguagetext.Contains(","))
                        {
                            string[] txtlanguage = Addmorelanguagetext.Split(',');

                            foreach (var data in txtlanguage)
                            {
                                // Addmorelanguagetext += "<answers>" + data + "</answers>";
                                if (removestringtxtlst.Count != 0)
                                {
                                    removestringtxtlst.Add(",<answer>" + data + "</answer>");
                                }
                                else
                                {
                                    removestringtxtlst.Add("<answer>" + data + " </answer>");
                                }

                                // checkedlanguagevalue += ",<answers>" + data + "</answers>";

                            }
                        }
                        if (removestringtxtlst.Count > 0)
                        {
                            foreach (var list in removestringtxtlst)
                            {
                                if (checkedlanguagevalue.Contains(list))
                                {
                                    checkedlanguagevalue = checkedlanguagevalue.Replace(list, "");
                                }
                            }
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private void chb_needservicetypechanges(object sender, EventArgs e)
        {
            try
            {
                var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
                if (category.IsChecked == true)
                {
                    if (checkedneedservicetypevalue != null && checkedneedservicetypevalue != "")
                    {
                        if (!checkedneedservicetypevalue.Contains(category.ClassId))
                        {
                            checkedneedservicetypevalue += "," + category.ClassId;
                        }
                    }
                    else
                    {
                        checkedneedservicetypevalue += category.ClassId;
                    }
                }
                else
                {
                    string[] indexvalue = checkedneedservicetypevalue.Split(',');
                    int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                    indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                    checkedneedservicetypevalue = string.Join(",", indexvalue);
                }
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private string _supertagid = "";
        public string supertagid
        {
            get { return _supertagid; }
            set { _supertagid = value; OnPropertyChanged(nameof(supertagid)); }
        }
        private string _checkedsupercategorytagurl = "";
        public string checkedsupercategorytagurl
        {
            get { return _checkedsupercategorytagurl; }
            set { _checkedsupercategorytagurl = value; OnPropertyChanged(nameof(checkedsupercategorytagurl)); }
        }
        public async void getneedservice()
        {
            try
            {
                await Task.Delay(100);
                var currentpage = Getcurrentpage();
                StackLayout needservicelayout = currentpage.FindByName<StackLayout>("needservicelayout");


                 dialog.ShowLoading("", null);
                if (!string.IsNullOrEmpty(post.categoryurl))
                {
                    string caturl = post.categoryurl;
                    if (!string.IsNullOrEmpty(checkedsupercategorytagurl))
                    {
                        needservicelayout.Children.Clear();
                        
                        if (checkedsupercategorytagurl.Contains("nanny") && checkedsupercategorytagurl.Contains("babysitter"))
                        {
                            checkedsupercategorytagurl = checkedsupercategorytagurl.Replace("babysitter", "");
                        }

                        caturl = checkedsupercategorytagurl;
                    }
                    await Task.Delay(100);
                    var dcapi = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                    careservice needservicedata = await dcapi.getnewneedservice(caturl, post.businessid);
                    serviceneedlstdata = needservicedata.ROW_DATA;
                    if (serviceneedlstdata != null && serviceneedlstdata.Count > 0)
                    {
                        supertagid = needservicedata.ROW_DATA[0].supertagid;
                        needservicelblvisible = true;
                        foreach (var data in serviceneedlstdata)
                        {
                            BoxView boxx = new BoxView();
                            boxx.BackgroundColor = Color.FromHex("e0e0e0");
                            boxx.HeightRequest = 1;
                            Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                            //chbx.OnImg = "CheckBoxChecked.png";
                            //chbx.OffImg = "CheckBoxUnChecked.png";
                            chbx.Text = data.primarytag.Replace("&amp", "").Replace("&amp;", "").Replace("amp;", "");
                            //18::Activities::activities
                            chbx.ClassId = data.primarytagid.ToString() + "::" + data.primarytag + "::" + data.primarytagurl;
                            //chbx.ShowLabel = true;
                            chbx.CheckChanged += chb_needservicetypechanges;
                            chbx.Padding = 5;
                            if (post.ismyneed == "1")
                            {
                                if (!string.IsNullOrEmpty(post.caretypes))
                                {
                                    string[] serviceprovidertypelst = post.caretypes.Split(',');
                                    foreach (var i in serviceprovidertypelst)
                                    {
                                        if (data.primarytag == i)
                                        {
                                            chbx.IsChecked = true;
                                            if (string.IsNullOrEmpty(checkedneedservicetypevalue))
                                            {
                                                checkedneedservicetypevalue += chbx.ClassId;
                                            }
                                            else
                                            {
                                                checkedneedservicetypevalue += "," + chbx.ClassId;
                                            }
                                        }
                                    }
                                }
                            }

                            needservicelayout.Children.Add(chbx);
                            needservicelayout.Children.Add(boxx);
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
    

        public async void getjobtype()
        {
            try
            {

                    var dcapi = RestService.For<IDaycareposting>(Commonsettings.DaycareAPI);
                    careservice needservicedata = await dcapi.gjobtype(post.businessid);
                    jobtypelstdata = needservicedata.ROW_DATA;
                var currentpage = Getcurrentpage();
                StackLayout jobtypelayout = currentpage.FindByName<StackLayout>("jobtypelayout");
                if (jobtypelstdata != null && jobtypelstdata.Count > 0)
                    {
                        //supertagid = needservicedata.ROW_DATA[0].supertagid;
                        //needservicelblvisible = true;
                        foreach (var data in jobtypelstdata)
                        {
                            BoxView boxx = new BoxView();
                            boxx.BackgroundColor = Color.FromHex("e0e0e0");
                            boxx.HeightRequest = 1;
                            Plugin.InputKit.Shared.Controls.CheckBox chbx = new Plugin.InputKit.Shared.Controls.CheckBox(); chbx.Color = Color.DarkGreen; chbx.BoxBackgroundColor = Color.LightGray; chbx.Type = Plugin.InputKit.Shared.Controls.CheckBox.CheckType.Check;
                            //chbx.OnImg = "CheckBoxChecked.png";
                            //chbx.OffImg = "CheckBoxUnChecked.png";
                            chbx.Text = data.primarytag.Replace("&amp", "").Replace("&amp;", "").Replace("amp;", "");
                            //18::Activities::activities
                            chbx.ClassId = data.primarytagid.ToString() + "::" + data.primarytag + "::" + data.primarytagurl;
                            //chbx.ShowLabel = true;
                            chbx.CheckChanged += chb_jobtypechanges;
                            chbx.Padding = 5;
                            if (post.ismyneed == "1")
                            {
                                if (!string.IsNullOrEmpty(post.jobtype))
                                {
                                    string[] serviceprovidertypelst = post.jobtype.Split(',');
                                    foreach (var i in serviceprovidertypelst)
                                    {
                                        if (data.primarytag == i)
                                        {
                                            chbx.IsChecked = true;
                                            if (string.IsNullOrEmpty(checkedjobtypevalue))
                                            {
                                            checkedjobtypevalue += chbx.ClassId;
                                            }
                                            else
                                            {
                                            checkedjobtypevalue += "," + chbx.ClassId;
                                            }
                                        }
                                    }
                                }
                            }

                        jobtypelayout.Children.Add(chbx);
                        jobtypelayout.Children.Add(boxx);
                        }

                    }
                    //  dialog.HideLoading();
                

            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        private void chb_jobtypechanges(object sender, EventArgs e)
        {
            try
            {
                var category = sender as Plugin.InputKit.Shared.Controls.CheckBox;
                if (category.IsChecked == true)
                {
                    if (checkedjobtypevalue != null && checkedjobtypevalue != "")
                    {
                        if (!checkedjobtypevalue.Contains(category.ClassId))
                        {
                            checkedjobtypevalue += "," + category.ClassId;
                        }
                    }
                    else
                    {
                        checkedjobtypevalue += category.ClassId;
                    }
                }
                else
                {
                    string[] indexvalue = checkedjobtypevalue.Split(',');
                    int numIndex = Array.IndexOf(indexvalue, category.ClassId.ToString());
                    indexvalue = indexvalue.Where((val, idx) => idx != numIndex).ToArray();
                    checkedjobtypevalue = string.Join(",", indexvalue);
                }
            }
            catch (Exception ex)
            {
                dialog.HideLoading();
            }
        }
        public async void postfirstsubmit()
        {
            try
            {

                if (validation())
                {
                    dialog.ShowLoading("", null);
                    post.supertagid = supertagid;
                    post.salary = payratetxt;
                    post.salaryratemax = Maxsal;
                    post.salarytype = paytypetxt;
                    post.salarytypeid =paytypeid.ToString();
                    //<answers><answer>" + j + "</answer></answers>::" + data.questionid + "::" + data.questionorder
                    if (gendertxtvisible==true)
                        post.gender = "<answers><answer>"+ genderID + "</answer></answers>"+genderquestionID;
                    if(experiencelblvisible==true)
                    {
                        post.careexperience = "<answers>" + "<answer>" + paidexperienceyrtxtID + "</answer>" + "</answers>" + paidexperiencequestionID;
                    }
                    if(establismentyrblkvisible==true)
                    {
                        post.careexperience = "<answers>" + "<answer>" + establishyeartxtID + "</answer>" + "</answers>" + establishmentyearquestionID;
                    }
                    if(languagelayoutvisible==true)
                    {
                        if(post.ismyneed=="1"&& !string.IsNullOrEmpty(post.Languageid))
                        {
                            post.languages = "<answers>" + checkedlanguagevalue.Replace(",", "") + "</answers>" + post.Languageid;
                        }
                        else
                        {
                            post.languages = "<answers>" + checkedlanguagevalue.Replace(",", "") + "</answers>" + LanguagequestionID;
                        }
                    }
                        
                    if(certificationlistviewvisble==true)
                        post.certifications = "<answers>"+ checkedcertificationvalue +"</answers>"+ certificationquestionID;
                    if(respondlblvisible==true)
                        post.responsetime = "<answers>" + replyperiodtxtID + "</answers>" + replyperiodquestionID;
                    if (drivetxtvisible == true)
                        post.drive = drivetxtID;
                    if(vehicletxtvisible==true)
                        post.vehicle = vehicletxtID;
                    if(licensetxtvisible==true)
                        post.license = licensetxtID;
                    if (activityenrichmntblkvisible == true)
                        post.activityenrichments = "<answers>" + checkedactivitypevalue + "</answers>" + enrichmentquestionID;
                    if (liveoutprferencevisible == true)
                        post.preference = liveoutprferencetxtID;
                    if (needservicelblvisible == true)
                        post.caretypes = checkedneedservicetypevalue;
                    if (agegrouplblvisible == true)
                        post.agegroups = checkedagegrouptypevalue;

                    if(!string.IsNullOrEmpty(establishyeartxtID))
                    {
                        post.establishmentyear = establishyeartxtID;
                    }
                    //02 / 29 / 2020
                    post.fromdate = fromdate.ToString("MM/dd/yyyy");
                    post.todate = todate.ToString("MM/dd/yyyy");
                    if (dayschedulevisible == true)
                        post.availablitytable = checkedschedulevalue;

                    if(otherprovidervisible==true)
                    {
                      post.otherserviceprovider = checkedotherprovidervalue;
                    }
                    if (certificationlistviewvisble == true && !string.IsNullOrEmpty(checkedcertificationvalue))
                    {
                        post.certifications = "<answers>" + checkedcertificationvalue + "</answers>" + certificationquestionID;
                    }
                    if(carecenterhoursvisible == true)
                    {
                        post.availablitytable = availabilitytable;
                    }
                    if(workdescriptionvisible==true)
                    {
                        if(post.ismyneed=="1" && !string.IsNullOrEmpty(post.Worktype))
                        {
                            post.workdesc = "<answers><answer>" + workdescription + "</answer></answers>" + post.Worktype;
                        }
                        else
                        {
                            post.workdesc = "<answers><answer>" + workdescription + "</answer></answers>" + workdescquestionID;
                        }
                    }
                    //virtualnanny
                    post.virtualnanny = onlineservicetextID.ToString();
                    if (post.supercategoryid == "1" && post.maincategoryid == "1")
                    {
                        post.tranportation = transportationtextID;
                        post.Education = educationtextID;
                        post.smoke = smoketextID;
                        post.comfortablewithpets = comfpetstextID;
                        post.jobtype = checkedjobtypevalue;
                    }
                        var currentpage = Getcurrentpage();
                    await currentpage.Navigation.PushAsync(new PostSecond(post));
                    dialog.HideLoading();
                }
            }
            catch(Exception ex)
            {
                dialog.HideLoading();
            }
        }

        public void checkall()
        {
            var currentpage = Getcurrentpage();
            Task.Delay(2000);
            StackLayout stck = new StackLayout();
            Image emsu = new Image();
            emsu.Source = "";
            emsu.ClassId = "";
            
            StackLayout grid = currentpage.FindByName<StackLayout>("dayschedulegrid");

            stck.Children.Add(grid);
            var jk=((Label)stck.Children.ElementAt(4)).Text;
            foreach (var item in grid.Children)
            {
                //if (item.GetType() == chbx.GetType())
                //{
                //    var cat = item as Plugin.InputKit.Shared.Controls.CheckBox;
                //    if (cat.IsChecked == true)
                //    {
                //        cat.IsChecked = false;
                //    }
                //}
            }
            foreach (var ki in grid.Children)
            {

            }
            //var ff = grid.Children.GetType();
            
        }
        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Page Getcurrentpage()
        {
            var currentpage = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();
            return currentpage;
        }
    }
}
