using Juce.Core.Di.Container;

namespace Juce.Core.Di.BindingActions
{
    public interface IDiBindingActionA
    {
        void Execute(IDiResolveContainerA resolver, object obj);
    }
}
