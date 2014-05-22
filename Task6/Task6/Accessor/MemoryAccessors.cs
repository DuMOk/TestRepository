using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    public class MemoryAccessors<T> : IDataAccessors<T>
    {
        public List<T> GetAll()
        {
            var strings = new List<T>();
            
            for (int i = 0; i < Personal.ListPersonal.Length; i++)
            {
                var item = Personal.ListPersonal[i];
                
                strings.Add((T)Convert.ChangeType(item, typeof(T)));
            }
            
            return strings;
        }

        public T GetByName(string name)
        {
            int index = Array.IndexOf<string>(Personal.ListPersonal, name);
            
            if (index < 0)
                return default(T);
            
            return (T)Convert.ChangeType(Personal.ListPersonal[index], typeof(T));
        }

        public void DeleteByName(string name)
        {
            int index = Array.IndexOf<string>(Personal.ListPersonal, name);
            
            if (index < 0)
                return;
            
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

        public void Insert(T name)
        {
            Array.Resize(ref Personal.ListPersonal, Personal.ListPersonal.Length + 1);
            Personal.ListPersonal[Personal.ListPersonal.Length - 1] = (string)Convert.ChangeType(name, typeof(T));
        }
    }
}
