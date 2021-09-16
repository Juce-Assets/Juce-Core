using System.Collections.Generic;

namespace Juce.Core.Factories
{
    public interface IRepository<TId, TObject>
    {
        IReadOnlyList<TObject> Objects { get; }

        void Add(TId id, TObject obj);
        void Remove(TId id);
        bool TryGet(TId id, out TObject obj);
    }
}
