using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task6
{
    [TableAttribute("Student")]
    public class Student
    {
        [FieldAttribute("IdStud", "int")]
        public int field1 = 0;
        
        [FieldAttribute("FIOStud", "string")]
        public string field2 = null;
        
        [FieldAttribute("IdGroup", "int")]
        public int field3 = 0;

        public int IdStud 
        {
            get { return field1; }
            set { field1 = value; }
        }

        public string FIO 
        {
            get { return field2; }
            set { field2 = value; }
        }

        public int IdGrp 
        {
            get { return field3; }
            set { field3 = value; }
        }

        public Student() { }

        public Student(int primaryKey, string fio, int foreignKey)
        {
            field1 = primaryKey;
            field2 = fio;
            field3 = foreignKey;
        }
    }
}
