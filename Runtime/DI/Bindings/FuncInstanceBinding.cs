using Juce.Core.Di.Container;
using System;

namespace Juce.Core.Di.Bindings
{
    public class FuncInstanceBinding : DiBindingA
    {
        private readonly Func<IDiResolveContainerA, object> func;

        public FuncInstanceBinding(
            Type identifierType, 
            Type actualType, 
            Func<IDiResolveContainerA, object> func
            ) 
            : base(identifierType, actualType)
        {
            this.func = func;
        }

        protected override object OnBind(IDiResolveContainerA container)
        {
            return func.Invoke(container);
        }
    }
}
