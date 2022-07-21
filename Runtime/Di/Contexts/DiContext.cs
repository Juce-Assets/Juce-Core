using Juce.Core.Di.Container;
using Juce.Core.Di.Extensions;
using Juce.Core.Di.Installers;
using Juce.Core.Disposables;

namespace Juce.Core.Di.Contexts
{
    public sealed class DiContext<TResult> : IDiContext<TResult>
    {
        readonly IInstaller[] _installers;

        public DiContext(params IInstaller[] installers)
        {
            _installers = installers;
        }

        public IDisposable<TResult> Install()
        {
            IDiContainer container = DiContainerBuilderExtensions.BuildFromInstallers(_installers);

            void Dispose(TResult result)
            {
                container.Dispose();
            }

            TResult result = container.Resolve<TResult>();

            return new CallbackDisposable<TResult>(
                result,
                Dispose
            );
        }
    }
}
