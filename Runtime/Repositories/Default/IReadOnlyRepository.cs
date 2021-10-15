using System.Collections.Generic;

namespace Juce.Core.Repositories
{
    public interface IReadOnlyRepository<TObject>
    {
        IReadOnlyList<TObject> Items { get; }

        bool Contains(TObject obj);
    }
}