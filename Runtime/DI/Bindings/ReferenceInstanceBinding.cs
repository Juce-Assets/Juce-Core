using System;

namespace Juce.Core.DI
{
    public class ReferenceInstanceBinding : DIBinding
    {
        private readonly object obj;

        public ReferenceInstanceBinding(Type type, object obj) : base(type)
        {
            this.obj = obj;
        }

        protected override object OnBind(IDIResolveContainer container)
        {
            return obj;
        }
    }
}
