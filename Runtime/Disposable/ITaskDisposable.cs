using System.Threading.Tasks;

namespace Juce.Core.Disposables
{
    public interface ITaskDisposable<T>
    {
        public T Value { get; }

        Task Dispose();
    }
}
