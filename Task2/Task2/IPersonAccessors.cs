using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task2
{
    public interface IPersonAccessors
    {
        void GetAll();
        void GetByName(string name);
        void DeleteByName(string name);
        void Insert(string name);
    }
}
