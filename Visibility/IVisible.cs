using System;

namespace Juce.Core.Time
{
    public interface IVisible
    {
        void Show(bool instantly, Action onFinish = null);
        void Hide(bool instantly, Action onFinish = null);
    }
}
