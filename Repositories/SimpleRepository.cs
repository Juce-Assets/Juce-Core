using System.Collections.Generic;
using System.Linq;

namespace Juce.Core.Factories
{
    public class SimpleRepository<TId, TObject> : IRepository<TId, TObject>
    {
        private readonly Dictionary<TId, TObject> items = new Dictionary<TId, TObject>();

        public IReadOnlyList<TObject> Objects => items.Values.ToList();

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
