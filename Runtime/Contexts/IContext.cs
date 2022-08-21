using Juce.Core.Disposables;
using System.Threading;
using System.Threading.Tasks;

namespace Juce.Core.Contexts
{
    public interface IContext : IAsyncDisposable
    {
        Task Load(IContextData stateData, CancellationToken cancellationToken);
        void Start();
    }
}
