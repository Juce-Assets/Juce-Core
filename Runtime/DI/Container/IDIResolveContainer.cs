namespace Juce.Core.DI.Container
{
    public interface IDIResolveContainer 
    {
        T Resolve<T>();
    }
}