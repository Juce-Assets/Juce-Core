using System.Collections.Generic;

namespace Juce.Core.Repositories
{
    public class SimpleSingleRepository<TObject> : ISingleRepository<TObject>
    {
        private TObject item;

        public bool HasItem { get; private set; }

        public void Set(TObject obj)
        {
            item = obj;
            HasItem = true;
        }

        public void Clear()
        {
            item = default;
            HasItem = false;
        }

        public bool TryGet(out TObject obj)
        {
            obj = item;
            return HasItem;
        }

        public bool Contains(TObject obj)
        {
            if (!HasItem)
            {
                return false;
            }

            return EqualityComparer<TObject>.Default.Equals(item, obj);
        }
    }
}
