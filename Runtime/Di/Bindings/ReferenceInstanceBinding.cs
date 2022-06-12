using Juce.Core.Di.Container;
using System;

namespace Juce.Core.Di.Bindings
{
    public sealed class ReferenceInstanceBinding : DiBinding
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

        protected override object OnBind(IDiResolveContainer container)
        {
            return obj;
        }
    }
}
