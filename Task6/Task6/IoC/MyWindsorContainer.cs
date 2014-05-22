using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Core;
using Castle.Core.Resource;

namespace Task6
{
    public class MyWindsorContainer : IIoCContainer
    {
        private IWindsorContainer _container = new WindsorContainer();
        
        public void Register<TTypeToResolve, TConcrete>()
        {
            Type typeToResolve = typeof(TTypeToResolve);
            Type concrete = typeof(TConcrete);
            _container.Register(Component.For(typeToResolve).ImplementedBy(concrete));
        }
        
       public TTypeToResolve Resolve<TTypeToResolve>()
        {
            return _container.Resolve<TTypeToResolve>();
        }
    }
}
