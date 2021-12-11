using Juce.Core.Results;
using System.Threading.Tasks;

namespace Juce.Core.Factories
{
    public interface ITaskFactory<TDefinition, TCreation>
    {
        Task<ITaskResult<TCreation>> TryCreate(TDefinition definition);
    }

    public interface ITaskFactory<TCreation>
    {
        Task<ITaskResult<TCreation>> TryCreate();
    }
}
