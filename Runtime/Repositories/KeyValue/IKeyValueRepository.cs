using System.Collections.Generic;

namespace Juce.Core.Repositories
{
    public interface IKeyValueRepository<TId, TObject> : IReadOnlyKeyValueRepository<TId, TObject>
    {
        void Add(TId id, TObject obj);
        void Remove(TId id);
    }
}
