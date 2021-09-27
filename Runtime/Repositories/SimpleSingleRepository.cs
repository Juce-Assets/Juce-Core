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

        public void Remove()
        {
            item = default;
            HasItem = false;
        }

        public bool TryGet(out TObject obj)
        {
            obj = item;
            return HasItem;
        }
    }
}
