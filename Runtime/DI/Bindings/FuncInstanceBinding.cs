using Juce.Core.DI.Container;
using System;

namespace Juce.Core.DI.Bindings
{
    public class FuncInstanceBinding : DIBinding
    {
        private readonly Func<IDIResolveContainer, object> func;

        public FuncInstanceBinding(
            Type identifierType, 
            Type actualType, 
            Func<IDIResolveContainer, object> func
            ) 
            : base(identifierType, actualType)
        {
            this.func = func;
        }

        protected override object OnBind(IDIResolveContainer container)
        {
            return func.Invoke(container);
        }
    }
}
