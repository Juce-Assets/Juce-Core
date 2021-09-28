using Juce.Core.DI.Container;
using System;

namespace Juce.Core.DI.Bindings
{
    public class ReferenceInstanceBinding : DIBinding
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

        protected override object OnBind(IDIResolveContainer container)
        {
            return obj;
        }
    }
}
