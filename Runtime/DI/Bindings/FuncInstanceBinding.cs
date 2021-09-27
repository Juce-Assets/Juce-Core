using System;

namespace Juce.Core.DI
{
    public class FuncInstanceBinding : DIBinding
    {
        private readonly Func<IDIResolveContainer, object> func;

        public FuncInstanceBinding(Type type, Func<IDIResolveContainer, object> func) : base(type)
        {
            this.func = func;
        }

        protected override object OnBind(IDIResolveContainer container)
        {
            return func.Invoke(container);
        }
    }
}
