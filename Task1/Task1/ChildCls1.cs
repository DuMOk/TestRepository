using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task1
{
    [Custom1Attribute("This is first child class")]
    class ChildCls1 : BaseCls
    {
        [ObsoleteAttribute]
        public int fieldInt;
    }
}
