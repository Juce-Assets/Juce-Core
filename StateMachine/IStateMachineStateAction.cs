namespace Juce.Core.State
{
    public interface IStateMachineStateAction
    {
        void OnEnter();
        void OnExit();
    }
}
