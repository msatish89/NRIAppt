using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Techjobs.Features.LeadForm.Models
{
   public class Leadsentbiz
    {
        public string businessname { get; set; }
        public string businessid { get; set; }
        public string citystatecode { get; set; }
        public string phone5 { get; set; }
    }

    public class Leadsentbizlist
    {
        public List<Leadsentbiz> ROW_DATA { get; set; }
    }
}
