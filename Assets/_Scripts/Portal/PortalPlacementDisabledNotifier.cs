using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Notifies when portal placement is disabled.
/// </summary>
public class PortalPlacementDisabledNotifier : MonoBehaviour
{
    [SerializeField] PortalController _portalController;

    /// <summary>
    /// Called when a portal gesture is performed.
    /// </summary>
    public void OnPortalGesturePerformed()
    {
        if (!_portalController.isActiveAndEnabled)
        {
            Debug.Log("Portal-PLatzierungsgeste ausgeführt, obwohl Portal bereits betreten wurde. Nutzer informieren!");
        }
    }
}
