namespace Juce.Core.Factories
{
    public interface IFactory<TCreation>
    {
        bool TryCreate(out TCreation creation);
    }

    public interface IFactory<TDefinition, TCreation>
    {
        bool TryCreate(TDefinition definition, out TCreation creation);
    }
}
