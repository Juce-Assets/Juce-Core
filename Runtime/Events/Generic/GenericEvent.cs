namespace Juce.Core.Events.Generic
{
    public delegate void GenericEvent<TSender, TEventArgs>(TSender sender, TEventArgs eventArgs);
}
