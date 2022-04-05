using Juce.Core.Di.Container;

namespace Juce.Core.Di.BindingActions
{
    public interface IDiBindingAction
    {
        void Execute(IDiResolveContainer resolver, object obj);
    }
}
