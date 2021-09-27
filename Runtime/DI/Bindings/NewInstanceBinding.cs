using System;

namespace Juce.Core.DI
{
    public class NewInstanceBinding : DIBinding
    {
        public NewInstanceBinding(Type type) : base(type)
        {

        }

        protected override object OnBind(IDIResolveContainer container)
        {
            return Activator.CreateInstance(Type);
        }
    }
}
