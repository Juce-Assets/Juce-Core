using Juce.Core.Di.Container;
using System;

namespace Juce.Core.Di.Bindings
{
    public sealed class FuncInstanceBinding : DiBinding
    {
        private readonly Func<IDiResolveContainer, object> func;

        public FuncInstanceBinding(
            Type identifierType, 
            Type actualType, 
            Func<IDiResolveContainer, object> func
            ) 
            : base(identifierType, actualType)
        {
            this.func = func;
        }

        protected override object OnBind(IDiResolveContainer container)
        {
            return func.Invoke(container);
        }
    }
}
