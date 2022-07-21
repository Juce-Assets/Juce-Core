using System;
using Juce.Core.Di.Builder;

namespace Juce.Core.Di.Installers
{
    public sealed class CallbackInstaller : IInstaller
    {
        readonly Action<IDiContainerBuilder> callback;

        public CallbackInstaller(
            Action<IDiContainerBuilder> callback
            )
        {
            this.callback = callback;
        }

        public void Install(IDiContainerBuilder container)
        {
            callback.Invoke(container);
        }
    }
}
