using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NRIApp.Techjobs.Features.Detail.Models
{
   public class ModuleDetail
    {
        public string pagedescription { get; set; }
        public string trainingmode { get; set; }
        public string title { get; set; }
        public string businesscity { get; set; }
        public string businessname { get; set; }
        public string module { get; set; }
        public string businessmoduleid { get; set; }
        public string moduleurl { get; set; }
        public string businesstitleurl { get; set; }
        public string isbookmarked { get; set; }
        public string supertagid { get; set; }
    }

    public class ModuleDetaildata
    {
        [JsonProperty(PropertyName = "ROW_DATA")]
        public List<ModuleDetail> ROW_DATA { get; set; }
    }

    public class Trainingdateslist
    {
        public string startdate { get; set; }
        public string trainingmode { get; set; }
    }

    public class Trainingdates
    {
        public List<Trainingdateslist> ROW_DATA { get; set; }
    }

    public class Serviceslist
    {
        public string services { get; set; }
    }
    public class Services
    {
        public List<Serviceslist> ROW_DATA { get; set; }
    }
    public class Videolist
    {
        public string imageurl { get; set; }
        public string videourl { get; set; }
    }
    public class Videos
    {
        public List<Videolist> ROW_DATA { get; set; }
    }

    public class Bizmoduleslist
    {
        public string module { get; set; }
        public string moduleid { get; set; }
        public string trainingmode { get; set; }
        public string trainingdate { get; set; }
        public string trainingcity { get; set; }
        public string totalrecs { get; set; }
        public int pageno { get; set; }
        public string onlinemode { get; set; }
        public string inclassmode { get; set; }
        public bool Onlinemodestack { get; set; }
        public bool Classroommodestack { get; set; }
        public bool detailblkvisible { get; set; }
        public string image { get; set; }
        public string course { get; set; }
        public string courseid { get; set; }
    }
    public class Bizmodules
    {
        public List<Bizmoduleslist> ROW_DATA { get; set; }
    }

    public class GROUP_Bizmodules
    {
        public string Heading { get; set; }
        public string image { get; set; }
        public List<Bizmoduleslist> Modules { get; set; }
        public GROUP_Bizmodules() { }
        public GROUP_Bizmodules(string name, List<Bizmoduleslist> MODULES)
        {
            Heading = Heading;
            Modules = MODULES;
        }
    }

    public class BizModule
    {
        private Bizmoduleslist _service;
        public BizModule(Bizmoduleslist service)
        {
            _service = service;
        }
        public string Module
        {
            get
            {
                return _service.module;
            }
        }
        public string Moduleid
        {
            get
            {
                return _service.moduleid;
            }
        }
        public string Trainingmode
        {
            get
            {
                return _service.trainingmode;
            }
        }
        public string Trainingdate
        {
            get
            {
                return _service.trainingdate;
            }
        }

        public string trainingcity
        {
            get
            {
                return _service.trainingcity;
            }
        }
        public string Onlinemode
        {
            get
            {
                return _service.onlinemode;
            }
        }
        public string Inclassmode
        {
            get
            {
                return _service.inclassmode;
            }
        }
        public bool onlinemodestack
        {
            get
            {
                return _service.Onlinemodestack;
            }
        }
        public bool Inclassmodestack
        {
            get
            {
                return _service.Classroommodestack;
            }
        }
        public Bizmoduleslist Modlist
        {
            get => _service;
        }

        
    }

    public class Result
    {
        public List<ResultData> ROW_DATA { get; set; }
    }

    public class ResultData
    {
        public string resulinformation { get; set; }
    }

}
