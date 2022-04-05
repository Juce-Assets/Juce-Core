using Juce.Core.Di.Builder;
using System;

namespace Juce.Core.Di.Extensions
{
    public static class DiDisposableExtensionsA
    {
        public static IDiBindingActionBuilderA<T> LinkDisposable<T>(this IDiBindingActionBuilderA<T> actionBuilder)
            where T : IDisposable
        {
            return actionBuilder.WhenDispose((o) => o.Dispose());
        }
    }
}
