using System.Threading;
using System.Threading.Tasks;

namespace Juce.Core.Visibility
{
    public interface IVisible
    {
        Task SetVisible(bool visible, bool instantly, CancellationToken cancellationToken);
    }
}
