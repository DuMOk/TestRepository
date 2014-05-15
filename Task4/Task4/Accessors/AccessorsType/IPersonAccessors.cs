using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accessors
{
    public interface IDataAccessors<T>
    {
        List<T> GetAll();
        T GetByName(string name);
        void DeleteByName(string name);
        void Insert(T name);
    }
}
