using Juce.Core.DI.Container;
using System;

namespace Juce.Core.DI.Bindings
{
    public class NewInstanceBinding : DIBinding
    {
        public NewInstanceBinding(Type identifierType, Type actualType) : base(identifierType, actualType)
        {

        }

        protected override object OnBind(IDIResolveContainer container)
        {
            return Activator.CreateInstance(ActualType);
        }
    }
}
