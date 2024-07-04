/// <summary>
/// Represents an event that occurs during a scanning process.
/// </summary>
public struct ScanEvent : IEvent
{
    /// <summary>
    /// Gets the progress of the scanning process.
    /// </summary>
    public float ScanProgress { get; private set; }

    /// <summary>
    /// Gets the data of the scanned creature.
    /// </summary>
    public ICreature CreatureData { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the scanning process is being aborted.
    /// </summary>
    public bool IsAborting { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the scanning process is complete.
    /// </summary>
    public readonly bool IsComplete => ScanProgress >= 1f;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScanEvent"/> struct.
    /// </summary>
    /// <param name="scanProgress">The progress of the scanning process.</param>
    /// <param name="creatureData">The data of the scanned creature.</param>
    /// <param name="isAborting">A value indicating whether the scanning process is being aborted.</param>
    public ScanEvent(float scanProgress, ICreature creatureData, bool isAborting)
    {
        ScanProgress = scanProgress;
        CreatureData = creatureData;
        IsAborting = isAborting;
    }
}
