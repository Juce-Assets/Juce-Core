using System.Collections.Generic;
using System.Linq;

namespace Juce.Core.Repositories
{
    public sealed class SimpleKeyValueRepository<TId, TObject> : IKeyValueRepository<TId, TObject>
    {
        private readonly Dictionary<TId, TObject> items = new Dictionary<TId, TObject>();

        public IReadOnlyList<TObject> Items => items.Values.ToList();

        public void Add(TId id, TObject obj)
        {
            items.Add(id, obj);
        }

        public void Remove(TId id)
        {
            items.Remove(id);
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool TryGet(TId id, out TObject obj)
        {
            return items.TryGetValue(id, out obj);
        }
    }
}
