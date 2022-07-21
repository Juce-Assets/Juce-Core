using Juce.Core.Disposables;

namespace Juce.Core.Di.Contexts
{
    public interface IDiContext<TResult>
    {
        IDisposable<TResult> Install();
    }
}
