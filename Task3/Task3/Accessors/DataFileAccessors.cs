using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Task3
{
    class DataFileAccessors : IPersonAccessors
    {

        public void GetAll()
        {
            Console.WriteLine("All persons:");
            StreamReader sreader = new StreamReader("Personal.txt");
            string person = null;
            while ((person = sreader.ReadLine()) != null)
            {
                Console.WriteLine("->{0}", person);
            }
            sreader.Close();
        }

        public string GetByName(string name)
        {
            Console.Write("Get person by name : ");
            StreamReader sreader = new StreamReader("Personal.txt");
            string person = null;
            while ((person = sreader.ReadLine()) != null)
            {
                if (person == name)
                {
                    sreader.Close();                    
                    return person;
                }
            }
            Console.WriteLine("person with name {0} does not exist!", name);
            sreader.Close();
            return null;
        }
       
        public void DeleteByName(string name)
        {
            Console.Write("Delete person by name : ");
            string[] persons = File.ReadAllLines("Personal.txt");
            int index = Array.IndexOf<string>(persons, name);
            if (index < 0)
            {
                Console.WriteLine("person with name {0} does not exist!", name);
                return;
            }
            Console.WriteLine(name);
            string[] newpersons = new string[persons.Length - 1];
            for (int i = 0, j = 0; i < newpersons.Length; i++, j++)
            {
                if (i == index)
                    j++;
                newpersons[i] = persons[j];
            }
            File.WriteAllLines("Personal.txt", newpersons);
        }
      
        public void Insert(string name)
        {
            Console.WriteLine("Insert person by name : {0}", name);
            StreamWriter swrite = new StreamWriter("Personal.txt", true);
            swrite.WriteLine(name);
            swrite.Close();

        }

    }
}
