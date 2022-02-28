using System.Threading.Tasks;

namespace Juce.Core.Loading
{
    public interface ITaskLoadingToken
    {
        Task Complete();
    }
}
