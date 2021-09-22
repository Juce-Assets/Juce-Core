namespace Juce.Core.Repositories
{
    public interface ISingleRepository<TObject>
    {
        bool HasItem { get; }

        void Set(TObject obj);
        void Remove();
        bool TryGet(out TObject obj);
    }
}
