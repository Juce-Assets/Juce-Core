namespace Juce.Core.Architecture
{
    public interface IEntity<T>
    {
        T TypeId { get; }
        int InstanceId { get; }
    }
}