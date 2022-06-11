namespace Juce.Core.Results
{
	public class TaskResult<T> : ITaskResult<T>
	{
        private readonly T _value;

        public bool HasResult { get; }

        private TaskResult(bool hasResult, T value)
        {
            HasResult = hasResult;
            _value = value;
        }

        public static TaskResult<T> FromResult(T value)
        {
            return new TaskResult<T>(true, value);
        }

        public static TaskResult<T> FromEmpty()
        {
            return new TaskResult<T>(false, default);
        }

        public bool TryGetResult(out T result)
        {
            result = _value;
            return HasResult;
        }
    }
}
