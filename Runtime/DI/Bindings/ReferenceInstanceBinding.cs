using Juce.Core.Di.Container;
using System;

namespace Juce.Core.Di.Bindings
{
    public class ReferenceInstanceBinding : DiBindingA
    {
        private readonly object obj;

        public ReferenceInstanceBinding(
            Type identifierType, 
            Type actualType, 
            object obj
            ) 
            : base(identifierType, actualType)
        {
            this.obj = obj;
        }

        protected override object OnBind(IDiResolveContainerA container)
        {
            return obj;
        }
    }
}
