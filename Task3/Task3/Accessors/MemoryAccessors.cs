using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
    class MemoryAccessors : IPersonAccessors
    {
        
        public void GetAll()
        {
            Console.WriteLine("All persons:");
            for (int i = 0; i < Personal.ListPersonal.Length; i++ )
                Console.WriteLine("->{0}", Personal.ListPersonal[i]);
        }

        public string GetByName(string name)
        {
            Console.Write("Get person by name : ");
            int index = Array.IndexOf<string>(Personal.ListPersonal, name);
            if (index < 0)
            {
                Console.WriteLine("person with name {0} does not exist!", name);
                return null;
            }
            return Personal.ListPersonal[index];
            //Console.WriteLine("{0}", Personal.ListPersonal[index]);
        }

        public void DeleteByName(string name) 
        {
            Console.Write("Delete person by name : ");
            int index = Array.IndexOf<string>(Personal.ListPersonal, name);
            if (index < 0)
            {
                Console.WriteLine("person with name {0} does not exist!", name);
                return;
            }
            Console.WriteLine(name);
            string[] list = new string[Personal.ListPersonal.Length - 1];
            for (int i = 0, j = 0; i < list.Length; i++, j++)
            {
                if (i == index)
                    j++;
                list[i] = Personal.ListPersonal[j];
            }
            Array.Resize(ref Personal.ListPersonal, Personal.ListPersonal.Length - 1);
            list.CopyTo(Personal.ListPersonal, 0);
        }

        public void Insert(string name)
        {
            Console.WriteLine("Insert person by name : {0}", name);
            Array.Resize(ref Personal.ListPersonal, Personal.ListPersonal.Length + 1);
            Personal.ListPersonal[Personal.ListPersonal.Length - 1] = name;
        }

    }
}
