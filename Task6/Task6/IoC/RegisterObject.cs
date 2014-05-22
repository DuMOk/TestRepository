using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    public class RegisterObject
    {
        public RegisterObject(Type typeToResolve, Type concreteType)
        {
            TypeToResolve = typeToResolve;
            ConcreteType = concreteType;
        }

        public Type TypeToResolve { get; private set; }

        public Type ConcreteType { get; private set; }

        public object Instance { get; private set; }

        public void CreateInstance(params object[] args)
        {
            this.Instance = Activator.CreateInstance(this.ConcreteType, args);
        }
    }
}
