/// <summary>
/// Represents an ability system that can activate and deactivate abilities.
/// </summary>
public interface IAbilitySystem
{
    /// <summary>
    /// Activates the specified ability.
    /// </summary>
    /// <param name="ability">The ability to activate.</param>
    void ActivateAbility(IAbility ability);

    /// <summary>
    /// Activates all abilities.
    /// </summary>
    void ActivateAllAbilities();

    /// <summary>
    /// Deactivates the specified ability.
    /// </summary>
    /// <param name="ability">The ability to deactivate.</param>
    void DeactivateAbility(IAbility ability);

    /// <summary>
    /// Deactivates all abilities.
    /// </summary>
    void DeactivateAllAbilities();
}
