namespace Juce.Core.DI
{
    public interface IDIBindingAction
    {
        void Execute(IDIResolveContainer resolver, object obj);
    }
}
