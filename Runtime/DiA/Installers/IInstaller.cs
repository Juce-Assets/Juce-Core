using Juce.Core.Di.Builder;

namespace Juce.Core.Di.Installers
{
    public interface IInstaller 
    {
        void Install(IDiContainerBuilder container);
    }
}