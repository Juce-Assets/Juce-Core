using Juce.Core.Disposables;
using System.Threading.Tasks;

namespace Juce.Core.Di.Contexts
{
    public interface IAsyncDiContext<TResult>
    {
        Task<IAsyncDisposable<TResult>> Install();
    }
}
