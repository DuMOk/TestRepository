using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    public class MyIoC : IIoCContainer
    {
        private readonly IList<RegisterObject> _regObj = new List<RegisterObject>();

        public void Register<TTypeToResolve, TConcrete>()
        {
            _regObj.Add(new RegisterObject(typeof(TTypeToResolve), typeof(TConcrete)));
        }

        public TTypeToResolve Resolve<TTypeToResolve>()
        {
            return (TTypeToResolve)ResolveObject(typeof(TTypeToResolve));
        }

        private object ResolveObject(Type typeToResolve)
        {
            var registeredObject = _regObj.FirstOrDefault(o => o.TypeToResolve == typeToResolve);
            return GetInstance(registeredObject);
        }

        private object GetInstance(RegisterObject registeredObject)
        {
            if (registeredObject.Instance == null )
            {
                var parameters = ResolveConstructorParameters(registeredObject);
                registeredObject.CreateInstance(parameters.ToArray());
            }
            return registeredObject.Instance;
        }

        private IEnumerable<object> ResolveConstructorParameters(RegisterObject registeredObject)
        {
            var constructorInfo = registeredObject.ConcreteType.GetConstructors().First();
            foreach (var parameter in constructorInfo.GetParameters())
            {
                yield return ResolveObject(parameter.ParameterType);
            }
        }
    }
}
