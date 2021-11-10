using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalService.Features.Models
{
    public class LS_PRIME_FORM_DATA
    {
        public string questionid { get; set; }
        public string optionid { get; set; }
        public string questionname { get; set; }

        public string option { get; set; }
        public string controltype { get; set; }
        public string ismandatory { get; set; }

        public string alttext { get; set; }
        public string questionids { get; set; }
        public string openmainflag { get; set; }

        public string opensubflag { get; set; }
    }

    public class LS_PRIME_FORM
    {
        public List<LS_PRIME_FORM_DATA> ROW_DATA { get; set; }
    }

    public class LS_FORM_Radio
    {
        public string Text { get; set; }
        public string Image { get; set; }
        public string Id { get; set; }
    }
    public class LS_PRIME_PAKAGE_MONTHS
    {
        public string Text { get; set; }
        public string Id { get; set; }
    }


}
