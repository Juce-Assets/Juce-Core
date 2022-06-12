using System;

namespace Juce.Core.Loading.Process
{
    public sealed class LoadingProcess : ILoadingProcess
    {
        public event Action OnCompleted;

        public bool HasParent { get; }

        private LoadingProcess(bool hasParent)
        {
            HasParent = hasParent;
        }

        public static ILoadingProcess New()
        {
            return new LoadingProcess(hasParent: false);
        }

        public ILoadingProcess NewChild()
        {
            LoadingProcess childProcess = new LoadingProcess(hasParent: true);

            return childProcess;
        }

        public void Complete()
        {
            OnCompleted?.Invoke();
        }
    }
}
