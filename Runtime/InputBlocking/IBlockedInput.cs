namespace Juce.Core.InputBlocking
{
    public interface IBlockedInput
    {
        void BlockAll(object owner);
        void UnblockAll(object owner);
    }
}
