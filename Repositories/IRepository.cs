using System.Collections.Generic;

namespace Juce.Core.Repositories
{
    public interface IRepository<TObject>
    {
        IReadOnlyList<TObject> Items { get; }

        void Add(TObject obj);
        void Remove(TObject obj);
        bool Contains(TObject obj);
    }
}
