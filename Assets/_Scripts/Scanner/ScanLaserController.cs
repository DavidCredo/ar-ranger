using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Controls the behavior of a scan laser in a Unity scene.
/// </summary>
public class ScanLaserController : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;

    [SerializeField] private float _laserLength = 10f;

    [SerializeField, ColorUsage(true, true)] Color _defaultColor;
    [SerializeField, ColorUsage(true, true)] Color _scanningColor;

    private Material _laserMaterial;

    void OnEnable()
    {
        _laserMaterial = _lineRenderer.material;
    }

    /// <summary>
    /// Sets the scanning state of the laser.
    /// </summary>
    /// <param name="zCoordinate">The z-coordinate of the laser's position.</param>
    public void SetScanningState(float zCoordinate)
    {
        _lineRenderer.SetPosition(1, new Vector3(0, 0, zCoordinate));
        if (_laserMaterial.color != _scanningColor)
        {
            _laserMaterial.DOColor(_scanningColor, "_Color", 0.3f).Play();
            _laserMaterial.DOVector(new Vector4(-4, 0, 0, 0), "_MainSpeed", 0.3f).Play();
        }
    }

    /// <summary>
    /// Sets the hit point of the laser.
    /// </summary>
    /// <param name="zCoordinate">The z-coordinate of the hit point.</param>
    public void SetLaserHitPoint(float zCoordinate)
    {
        _lineRenderer.SetPosition(1, new Vector3(0, 0, zCoordinate));
    }

    /// <summary>
    /// Resets the laser to its default state.
    /// </summary>
    public void ResetLaser()
    {
        _lineRenderer.SetPosition(1, new Vector3(0, 0, _laserLength));
        if (_laserMaterial.color != _defaultColor)
        {
            _laserMaterial.DOColor(_defaultColor, "_Color", 0.3f).Play();
            _laserMaterial.DOVector(new Vector4(-1, 0, 0, 0), "_MainSpeed", 0.3f).Play();
        }
    }

    /// <summary>
    /// Disables the laser.
    /// </summary>
    public void DisableLaser()
    {
        _lineRenderer.enabled = false;
    }

    /// <summary>
    /// Enables the laser.
    /// </summary>
    public void EnableLaser()
    {
        _lineRenderer.enabled = true;
    }
}
