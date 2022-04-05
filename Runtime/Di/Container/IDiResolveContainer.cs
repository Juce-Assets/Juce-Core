namespace Juce.Core.Di.Container
{
    public interface IDiResolveContainer
    {
        T Resolve<T>();
    }
}