using System.Threading.Tasks;

namespace Juce.Core.Loading.Tokens
{
    public interface ITaskLoadingToken
    {
        Task Complete();
    }
}
