using Juce.Core.DI.Builder;
using System;

namespace Juce.Core.DI.Extensions
{
    public static class DIDisposableExtensions
    {
        public static IDIBindingActionBuilder<T> LinkDisposable<T>(this IDIBindingActionBuilder<T> actionBuilder)
            where T : IDisposable
        {
            return actionBuilder.WhenDispose((o) => o.Dispose());
        }
    }
}
