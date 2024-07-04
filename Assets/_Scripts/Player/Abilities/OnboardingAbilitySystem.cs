using System.Collections.Generic;
using ARRanger;
using ARRanger.DependencyInjection;
using UnityEngine;

/// <summary>
/// Represents a system that manages abilities for a player.
/// </summary>
public class OnboardingAbilitySystem : PersistentSingleton<OnboardingAbilitySystem>, IDependencyProvider, IAbilitySystem
{
    private HashSet<IAbility> _abilities = new HashSet<IAbility>();

    void Start()
    {
        _abilities.Add(GetComponent<ScanAbility>().ScanAbilitySO);
        _abilities.Add(GetComponent<TeleportAbility>().teleportAbilitySO);
        _abilities.Add(GetComponent<PortalPlacementAbility>().PortalPlacementAbilitySO);

        if (TeleportationPlacesStack.TryGetInstance() != null)
        {
            Debug.Log("Deactivating all abilities.");
            DeactivateAllAbilities();
        }
    }

    [Provide]
    IAbilitySystem ProvideAbilitySystem()
    {
        return this;
    }

    public void ActivateAbility(IAbility ability)
    {
        if (!_abilities.Contains(ability))
        {
            Debug.LogError($"Ability {ability} is not registered with the AbilitySystem.");
        }
        else
        {
            ability.Activate();
        }
    }

    public void ActivateAllAbilities()
    {
        foreach (var ability in _abilities)
        {
            Debug.Log($"Activating ability {ability}");
            ability.Activate();
        }
    }

    public void DeactivateAbility(IAbility ability)
    {
        if (!_abilities.Contains(ability))
        {
            Debug.LogError($"Ability {ability} is not registered with the AbilitySystem.");
        }
        else
        {
            Debug.Log($"Deactivating ability {ability}");
            ability.Deactivate();
        }
    }

    public void DeactivateAllAbilities()
    {
        foreach (var ability in _abilities)
        {
            Debug.Log($"Deactivating ability {ability}");
            ability.Deactivate();
        }
    }

    public void SkipOnboarding()
    {
        Debug.Log("Skipping onboarding.");
        ActivateAllAbilities();
        if (_abilities.Contains(GetComponent<PortalPlacementAbility>().PortalPlacementAbilitySO))
        {
            EventBus<PortalEnteredEvent>.Raise(new PortalEnteredEvent());
        }
    }
}
