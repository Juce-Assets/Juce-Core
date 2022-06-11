namespace Juce.Core.Results
{
	public interface ITaskResult<T>
	{
		bool HasResult { get; }
		bool TryGetResult(out T result);
	}
}
