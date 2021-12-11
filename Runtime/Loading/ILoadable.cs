using System.Threading.Tasks;

namespace Juce.Core.Loading
{
    public interface ILoadable<TLoadResult>
    {
        Task<TLoadResult> Load();
        Task Unload();
    }
}
