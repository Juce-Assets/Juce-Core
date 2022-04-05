using Juce.Core.Di.Container;
using System;

namespace Juce.Core.Di.Bindings
{
    public class NewInstanceBinding : DiBindingA
    {
        public NewInstanceBinding(Type identifierType, Type actualType) : base(identifierType, actualType)
        {

        }

        protected override object OnBind(IDiResolveContainerA container)
        {
            return Activator.CreateInstance(ActualType);
        }
    }
}
