using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task1
{
    public sealed class Custom1Attribute :System.Attribute
    {
        public string descCls { get; set; }
        public Custom1Attribute(string description)
        {
            descCls = description;
        }
        public Custom1Attribute() { }
    }
}
