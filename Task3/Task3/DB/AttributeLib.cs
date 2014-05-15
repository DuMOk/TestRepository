using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TableAttribute : System.Attribute
    {
        public string TableName { get; set; }
        public TableAttribute() { }
        public TableAttribute(string name)
        {
            TableName = name;
        }
    }
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FieldAttribute : System.Attribute
    {
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public FieldAttribute() { }
        public FieldAttribute(string name, string type)
        {
            FieldName = name;
            FieldType = type;
        }
    }
}
