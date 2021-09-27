using System.Collections.Generic;

namespace Juce.Core.Repositories
{
    public interface IKeyValueRepository<TId, TObject>
    {
        IReadOnlyList<TObject> Items { get; }

        void Add(TId id, TObject obj);
        void Remove(TId id);
        bool TryGet(TId id, out TObject obj);
    }
}
