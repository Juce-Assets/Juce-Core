using Juce.Core.Di.Builder;
using System;

namespace Juce.Core.Di.Extensions
{
    public static class DiDisposableExtensions
    {
        public static IDiBindingActionBuilder<T> LinkDisposable<T>(this IDiBindingActionBuilder<T> actionBuilder)
            where T : IDisposable
        {
            return actionBuilder.WhenDispose((o) => o.Dispose());
        }
    }
}
