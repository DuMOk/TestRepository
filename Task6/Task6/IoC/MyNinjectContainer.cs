using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject;

namespace Task6
{
    class MyNinjectContainer : IIoCContainer
    {
        private IKernel _container = new StandardKernel();

        public void Register<TTypeToResolve, TConcrete>()
        {
            Type typeToResolve = typeof(TTypeToResolve);
            Type concrete = typeof(TConcrete);
            _container.Bind(typeToResolve).To(concrete);
        }

        public TTypeToResolve Resolve<TTypeToResolve>()
        {
            return _container.Get<TTypeToResolve>();
        }
    }
}
