using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task6
{
    [TableAttribute("StudGroup")]
    public class Group
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

        public string NameGroup
        {
            get { return field2; }
            set { field2 = value; }
        }

        public Group() { }

        public Group(int primaryKey, string name)
        {
            field1 = primaryKey;
            field2 = name;
        }
    }
}
