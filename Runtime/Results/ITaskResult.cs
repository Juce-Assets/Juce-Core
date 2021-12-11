namespace Juce.Core.Results
{
	public interface ITaskResult<T>
	{
		public bool HasResult { get; }
		public T Value { get; }
	}
}
