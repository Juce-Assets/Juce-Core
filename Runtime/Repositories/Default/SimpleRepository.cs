using System.Collections.Generic;

namespace Juce.Core.Repositories
{
    public class SimpleRepository<TObject> : IRepository<TObject>
    {
        private readonly List<TObject> items = new List<TObject>();

        public IReadOnlyList<TObject> Items => items;

        public void Add(TObject obj)
        {
            items.Add(obj);
        }

        public void Remove(TObject obj)
        {
            items.Remove(obj);
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool Contains(TObject obj)
        {
            return items.Contains(obj);
        }
    }
}
