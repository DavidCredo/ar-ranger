using UnityEngine;

/// <summary>
/// Represents a creature in the game.
/// </summary>
public class Creature : MonoBehaviour
{
    [field: SerializeField] public CreatureDataSO Data { get; private set; }
}
