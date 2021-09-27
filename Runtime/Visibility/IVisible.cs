using System;
using System.Threading.Tasks;

namespace Juce.Core.Visibility
{
    public interface IVisible
    {
        Task Show(bool instantly);
        Task Hide(bool instantly);
    }
}
