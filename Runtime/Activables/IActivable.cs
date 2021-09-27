namespace Juce.Core.Activables
{
    public interface IActivable
    {
        bool Active { get; }

        void SetActive(bool active);
    }
}