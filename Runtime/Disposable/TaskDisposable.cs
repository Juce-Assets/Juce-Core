﻿using System;
using System.Threading.Tasks;

namespace Juce.Core.Disposables
{
    public class TaskDisposable<T> : ITaskDisposable<T>
    {
        private readonly Func<T, Task> onDispose;

        private bool disposed;

        public T Value { get; }

        public TaskDisposable(T value, Func<T, Task> onDispose)
        {
            Value = value;
            this.onDispose = onDispose;
        }

        public Task Dispose()
        {
            if(disposed)
            {
                return Task.CompletedTask;
            }

            disposed = true;

            return onDispose?.Invoke(Value);
        }
    }
}
