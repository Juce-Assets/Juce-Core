using System;

namespace Juce.Core.DI.Data
{
    public class Class3Test : IDisposable
    {
        public Class1Test Class1Test { get; private set; }
        public bool Disposed { get; set; }

        public void Init()
        {

        }

        public void Init(Class1Test class1Test)
        {
            Class1Test = class1Test;
        }

        public void Dispose()
        {
            Disposed = true;
        }
    }
}
