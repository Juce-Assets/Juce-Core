using Juce.Core.DI.Builder;
using Juce.Core.Subscribables;

namespace Juce.Core.DI.Extensions
{
    public static class DISubscribableExtensions
    {
        public static IDIBindingActionBuilder<T> LinkSubscribable<T>(this IDIBindingActionBuilder<T> actionBuilder) 
            where T : ISubscribable
        {
            actionBuilder.WhenInit((c, o) => o.Subscribe());
            actionBuilder.WhenDispose((o) => o.Unsubscribe());

            return actionBuilder;
        }
    }
}
