/// <summary>
/// Represents a strategy for handling the execution of an ability performed event.
/// </summary>
/// <typeparam name="T">The type of the ability performed event.</typeparam>
public interface IAbilityPerformedStrategy<T> where T : IEvent
{
    /// <summary>
    /// Executes the strategy for handling the ability performed event.
    /// </summary>
    /// <param name="abilityPerformedEvent">The ability performed event to be handled.</param>
    void Execute(T abilityPerformedEvent);
}
