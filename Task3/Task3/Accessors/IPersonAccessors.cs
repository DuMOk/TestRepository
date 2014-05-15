using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3
{
    public interface IPersonAccessors
    {
        void GetAll();
        string GetByName(string name);
        void DeleteByName(string name);
        void Insert(string name);
    }

}
