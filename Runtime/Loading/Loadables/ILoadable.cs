using System.Threading.Tasks;

namespace Juce.Core.Loading.Loadables
{
    public interface ILoadable<TLoadResult>
    {
        Task<TLoadResult> Load();
        Task Unload();
    }
}
