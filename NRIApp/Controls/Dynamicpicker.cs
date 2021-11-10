using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Controls
{
    public class Person
    {
        public string Name { get; set; }
    }
    public class PersonHolder
    {
        public Person Person { get; set; }
        public int Quantity { get; set; }
    }
}
