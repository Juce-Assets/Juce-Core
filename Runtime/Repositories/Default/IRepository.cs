using System.Collections.Generic;

namespace Juce.Core.Repositories
{
    public interface IRepository<TObject> : IReadOnlyRepository<TObject>
    {
        void Add(TObject obj);
        void Remove(TObject obj);
    }
}
