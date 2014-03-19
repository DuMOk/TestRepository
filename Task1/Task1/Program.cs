using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseCls Obj1 = new BaseCls();
            ChildCls1 Obj2 = new ChildCls1();
            ChildCls2 Obj3 = new ChildCls2();
            ReflectOnAttribute(Obj1.GetType());
            ReflectOnAttribute(Obj2.GetType());
            ReflectOnAttribute(Obj3.GetType());
            Console.ReadLine();
        }
        static void ReflectOnAttribute(Type t)
        {
            Console.WriteLine("---------------Attributes--------------");
            object[] attrCls = t.GetCustomAttributes(false);
            Console.WriteLine("Attributes for class {0}:", t);
            if (attrCls.Length == 0)
            {
                Console.WriteLine("The attributes was not found");
            }
            else
            {
                foreach (object at in attrCls)
                    Console.WriteLine("-> {0}", at);
            }
            FieldInfo[] attrField = t.GetFields();
            Console.WriteLine("----------Attributes for fields--------");
            for (int i = 0; i < attrField.Length; i++)
            {
                object[] att = attrField[i].GetCustomAttributes(false);
                Console.WriteLine("Attributes for field {0}:", attrField[i].Name);
                if (att.Length == 0)
                {
                    Console.WriteLine("The attributes was not found");
                }
                else
                {
                    foreach (object at in att)
                        Console.WriteLine("-> {0} ", at);
                }
            }
            Console.WriteLine();
        }
    }
}
