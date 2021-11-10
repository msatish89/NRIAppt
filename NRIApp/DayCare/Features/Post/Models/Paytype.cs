using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.DayCare.Features.Post.Models
{
    public class Paytype
    {
        public string paytype { get; set; }
        public int paytypeid { get; set; }
        public string checkimage { get; set; }
    }
    public class Careagegroup
    {
        public string agegroup { get; set; }
        public int ageid { get; set; }
    }
    public class Experience
    {
        public string years { get; set; }
        public string checkimage { get; set; }
    }
    public class Replyperiod
    {
        public string replytime { get; set; }
        public string checkimage { get; set; }
    }
    public class Carecenteryear
    {
        public string Establishmentyear { get; set; }
        public string checkimage { get; set; }
    }
    public class Language
    {
        public string language { get; set; }
    }
    public class activityenrichment
    {
        public string txt { get; set; }
        public string tag { get; set; }
    }
    public class activityenrichment_data
    {
        public List<activityenrichment> ROW_DATA { get; set; }
    }


    public class Daycarehours
    {
        public string hours { get; set; }
    }
}
