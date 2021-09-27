using System.Collections.Generic;
using System.Linq;

namespace Juce.Core.Repositories
{
    public class SimpleKeyValueRepository<TId, TObject> : IKeyValueRepository<TId, TObject>
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

        public bool TryGet(TId id, out TObject obj)
        {
            return items.TryGetValue(id, out obj);
        }
    }
}
