using System;

namespace Juce.Core.DI
{
    public interface IDIBindingBuilder<T>
    {
        IDIBindingActionBuilder<T> FromNew();
        IDIBindingActionBuilder<T> FromInstance(T instance);
        IDIBindingActionBuilder<T> FromFunction(Func<IDIResolveContainer, T> func);
    }
}