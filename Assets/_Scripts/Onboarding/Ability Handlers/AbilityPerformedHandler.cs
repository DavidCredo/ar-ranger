/// <summary>
/// Represents a handler for performing an ability.
/// </summary>
/// <typeparam name="T">The type of event to handle.</typeparam>
public class AbilityPerformedHandler<T> where T : IEvent
{
    private EventBinding<T> _eventBinding;

    public AbilityPerformedHandler(EventBinding<T> eventBinding)
    {
        _eventBinding = eventBinding;
    }

    /// <summary>
    /// Registers the handler to the event.
    /// </summary>
    public void RegisterToEvent()
    {
        EventBus<T>.Register(_eventBinding);
    }

    /// <summary>
    /// Unregisters the handler from the event.
    /// </summary>
    public void UnregisterFromEvent()
    {
        EventBus<T>.Unregister(_eventBinding);
    }
}
