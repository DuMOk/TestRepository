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
        public string Path = null;
        private Logger _myLog = LogManager.GetCurrentClassLogger();
        
        public List<T> GetAll()
        {
            try
            {
                StreamReader sReader = new StreamReader(Path);
                string person = null;
                var strings = new List<T>();
                
                while ((person = sReader.ReadLine()) != null)
                    strings.Add((T)Convert.ChangeType(person, typeof(T)));
                
                sReader.Close();
                
                return strings;
            }
            catch (FileNotFoundException ex)
            {
                _myLog.Error(ex);
                throw;
            }
        }

        public T GetByName(string name)
        {
            try
            {
                StreamReader sReader = new StreamReader(Path);
                string person = null;
                
                while ((person = sReader.ReadLine()) != null)
                {
                    if (person == name)
                    {
                        sReader.Close();
                        
                        return (T)Convert.ChangeType(person, typeof(T));
                    }
                }

                sReader.Close();
                
                return default(T);
            }
            catch (FileNotFoundException ex)
            {
                _myLog.Error(ex);
                throw;
            }
        }
       
        public void DeleteByName(string name)
        {
            try
            {
                string[] persons = File.ReadAllLines(Path);
                int index = Array.IndexOf<string>(persons, name);
               
                if (index < 0)
                    return;
                
                string[] newPersons = new string[persons.Length - 1];
                
                for (int i = 0, j = 0; i < newPersons.Length; i++, j++)
                {
                    if (i == index)
                        j++;
                    
                    newPersons[i] = persons[j];
                }
                
                File.WriteAllLines(Path, newPersons);
            }
            catch (FileNotFoundException ex)
            {
                _myLog.Error(ex);
                throw;
            }
        }
      
        public void Insert(T name)
        {
            try
            {
                StreamWriter sWrite = new StreamWriter(Path, true);
                sWrite.WriteLine(name);
                sWrite.Close();
            }
            catch (FileNotFoundException ex)
            {
                _myLog.Error(ex);
                throw;
            }
        }
    }
}
