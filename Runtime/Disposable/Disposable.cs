﻿using System;

namespace Juce.Core.Disposables
{
    public class Disposable<T> : IDisposable<T>
    {
        private readonly Action<T> onDispose;

        private bool disposed;

        public T Value { get; }

        public Disposable(T value, Action<T> onDispose)
        {
            Value = value;
            this.onDispose = onDispose;
        }

        public void Dispose()
        {
            if(disposed)
            {
                return;
            }

            disposed = true;

            onDispose?.Invoke(Value);
        }
    }
}
