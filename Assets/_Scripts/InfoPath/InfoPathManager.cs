using System.Collections;
using System.Collections.Generic;
using ARRanger;
using ARRanger.DependencyInjection;
using UnityEngine;

/// <summary>
/// Manages the positions of information paths in the scene.
/// </summary>
public class InfoPathManager : PersistentSingleton<InfoPathManager>
{
    /// <summary>
    /// Gets the dictionary of information path positions.
    /// </summary>
    public Dictionary<string, InfoPathData> InfoPathPositions { get; private set; } = new Dictionary<string, InfoPathData>();

    /// <summary>
    /// Adds or updates the position of an information path.
    /// </summary>
    /// <param name="infoPathName">The name of the information path.</param>
    /// <param name="infoPathData">The data of the information path.</param>
    public void AddInfoPathPosition(string infoPathName, InfoPathData infoPathData)
    {
        if (InfoPathPositions.ContainsKey(infoPathName))
        {
            InfoPathPositions[infoPathName] = infoPathData;
        }
        else
        {
            InfoPathPositions.Add(infoPathName, infoPathData);
        }
    }
}

public struct InfoPathData
{
    public Vector3 Position;
    public Quaternion Rotation;

    public float PlayerYOffset;
}
