[System.Serializable]
public struct ProhibitedAreaEvent : IEvent
{
    /// <summary>
    /// Indicates whether the player is approaching the end of the map.
    /// </summary>
    public bool ApproachingEndOfMap;

    /// <summary>
    /// Indicates whether the player is currently in a prohibited area.
    /// </summary>
    public bool IsInProhibitedArea;
}
