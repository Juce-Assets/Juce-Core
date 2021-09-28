using Juce.Core.DI.Builder;

namespace Juce.Core.DI.Installers
{
    public interface IInstaller 
    {
        void Install(IDIContainerBuilder container);
    }
}