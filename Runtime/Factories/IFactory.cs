namespace Juce.Core.Factories
{
    public interface IFactory<TDefinition, TCreation>
    {
        bool TryCreate(TDefinition definition, out TCreation creation);
    }
}
