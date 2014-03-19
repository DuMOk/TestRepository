using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task1
{
    [Custom1Attribute("This is second child class")] 
    class ChildCls2 : BaseCls
    {
        [ObsoleteAttribute] 
        public double fieldDouble;
    }
}
