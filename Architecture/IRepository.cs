namespace Juce.Core.Architecture
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
    }
}