using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
    [TableAttribute("Employees")]
    class ClsEmpl
    {
        [FieldAttribute("IdEmpl", "int")]
        public int field1 = 0;
        [FieldAttribute("Post", "string")]
        public string field2 = null;
        public ClsEmpl() { }
        public ClsEmpl(int num, string str)
        {
            field1 = num;
            field2 = str;
        }
    }
}
