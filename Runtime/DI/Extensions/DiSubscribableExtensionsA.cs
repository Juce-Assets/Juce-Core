using Juce.Core.Di.Builder;
using Juce.Core.Subscribables;

namespace Juce.Core.Di.Extensions
{
    public static class DiSubscribableExtensionsA
    {
        public static IDiBindingActionBuilderA<T> LinkSubscribable<T>(this IDiBindingActionBuilderA<T> actionBuilder) 
            where T : ISubscribable
        {
            actionBuilder.WhenInit((c, o) => o.Subscribe());
            actionBuilder.WhenDispose((o) => o.Unsubscribe());

            return actionBuilder;
        }
    }
}
