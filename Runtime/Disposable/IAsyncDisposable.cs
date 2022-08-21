using System.Threading.Tasks;

namespace Juce.Core.Disposables
{
    public interface IAsyncDisposable
    {
        Task DisposeAsync();
    }

    public interface IAsyncDisposable<T>
    {
        public T Value { get; }

        Task DisposeAsync();
    }
}
