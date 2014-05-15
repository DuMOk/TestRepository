using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
    [TableAttribute("Person")]
    class ClsPerson
    {
        [FieldAttribute("IdPerson", "int")]
        public int field1 = 0;
        [FieldAttribute("NamePerson", "string")]
        public string field2 = null;
        public ClsPerson() { }
        public ClsPerson(int num, string str)
        {
            field1 = num;
            field2 = str;
        }
    }
}
