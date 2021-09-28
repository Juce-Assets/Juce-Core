using Juce.Core.DI.Container;

namespace Juce.Core.DI.BindingActions
{
    public interface IDIBindingAction
    {
        void Execute(IDIResolveContainer resolver, object obj);
    }
}
