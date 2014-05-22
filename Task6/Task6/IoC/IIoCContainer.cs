using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    public interface IIoCContainer
    {
        void Register<TTypeToResolve, TConcrete>();
        TTypeToResolve Resolve<TTypeToResolve>();
    }
}
