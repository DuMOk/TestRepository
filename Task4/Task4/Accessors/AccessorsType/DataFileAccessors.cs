using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using NLog;

namespace Accessors
{
    public class DataFileAccessors<T> : IDataAccessors<T>
    {
        public string path = null;
        private Logger myLog = LogManager.GetCurrentClassLogger();
        
        public List<T> GetAll()
        {
            try
            {
                StreamReader sreader = new StreamReader(path);
                string person = null;
                var strings = new List<T>();
                while ((person = sreader.ReadLine()) != null)
                    strings.Add((T)Convert.ChangeType(person, typeof(T)));
                sreader.Close();
                return strings;
            }
            catch (FileNotFoundException ex)
            {
                myLog.Error(ex);
                throw;
            }
        }

        public T GetByName(string name)
        {
            try
            {
                StreamReader sreader = new StreamReader(path);
                string person = null;
                while ((person = sreader.ReadLine()) != null)
                {
                    if (person == name)
                    {
                        sreader.Close();
                        return (T)Convert.ChangeType(person, typeof(T));
                    }
                }
                sreader.Close();
                return default(T);
            }
            catch (FileNotFoundException ex)
            {
                myLog.Error(ex);
                throw;
            }
        }
       
        public void DeleteByName(string name)
        {
            try
            {
                string[] persons = File.ReadAllLines(path);
                int index = Array.IndexOf<string>(persons, name);
                if (index < 0)
                    return;
                Console.WriteLine(name);
                string[] newpersons = new string[persons.Length - 1];
                for (int i = 0, j = 0; i < newpersons.Length; i++, j++)
                {
                    if (i == index)
                        j++;
                    newpersons[i] = persons[j];
                }
                File.WriteAllLines(path, newpersons);
            }
            catch (FileNotFoundException ex)
            {
                myLog.Error(ex);
                throw;
            }
        }
      
        public void Insert(T name)
        {
            try
            {
                StreamWriter swrite = new StreamWriter(path, true);
                swrite.WriteLine(name);
                swrite.Close();
            }
            catch (FileNotFoundException ex)
            {
                myLog.Error(ex);
                throw;
            }
        }
    }
}
