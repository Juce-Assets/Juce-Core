using System.Threading;
using System.Threading.Tasks;

namespace Juce.Core.Sequencing.Instructions
{
    public interface IInstruction
    {
        Task Execute(CancellationToken cancellationToken);
    }
}
