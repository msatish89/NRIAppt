using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Techjobs.Features.LeadForm.Models
{
    public class Modules
    {
        public string contentid { get; set; }
        public string module { get; set; }
        public string tagtype { get; set; }
        public string titleurl { get; set; }

    }

    public class Moduleajax
    {
        public List<Modules> ROW_DATA { get; set; }
    }

    public class Modulesbizcnt
    {
        public string businesscount { get; set; }
    }

    public class Bizcnt
    {
        public List<Modulesbizcnt> ROW_DATA { get; set; }
    }
}
