using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Accessors
{
    [TableAttribute("StudGroup")]
    public class ClsGroup
    {
        [FieldAttribute("IdGroup", "int")]
        public int field1 = 0;
        [FieldAttribute("NameGroup", "string")]
        public string field2 = null;

        public int IdGroup
        {
            get { return field1; }
            set { field1 = value; }
        }
        public string NameGrp
        {
            get { return field2; }
            set { field2 = value; }
        }

        public ClsGroup() { }
        public ClsGroup(int num, string str)
        {
            field1 = num;
            field2 = str;
        }
    }
}
