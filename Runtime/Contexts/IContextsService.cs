using System.Threading;
using System.Threading.Tasks;

namespace Juce.Core.Contexts
{
    public interface IContextsService
    {
        void Add(IContext context);

        Task UnloadCurrent(CancellationToken cancellationToken);
        Task Load<TContext>(IContextData data, CancellationToken cancellationToken) where TContext : IContext;
        Task Load<TContext>(CancellationToken cancellationToken) where TContext : IContext;
        void StartCurrent();
        Task UnloadCurrentAndLoad<TContext>(IContextData data, CancellationToken cancellationToken) where TContext : IContext;
        Task UnloadCurrentAndLoad<TContext>(CancellationToken cancellationToken) where TContext : IContext;
        Task LoadAndStartCurrent<TContext>(IContextData data, CancellationToken cancellationToken) where TContext : IContext;
        Task LoadAndStartCurrent<TContext>(CancellationToken cancellationToken) where TContext : IContext;
    }
}
