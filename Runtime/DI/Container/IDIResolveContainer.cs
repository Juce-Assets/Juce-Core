namespace Juce.Core.DI
{
    public interface IDIResolveContainer 
    {
        T Resolve<T>();
    }
}