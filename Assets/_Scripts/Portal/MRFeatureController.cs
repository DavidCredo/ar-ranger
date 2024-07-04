using UnityEngine;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Controls the features of a mixed reality portal.
/// </summary>
public class MRFeatureController : MonoBehaviour
{
    [SerializeField] private UniversalRendererData _rendererData;

    private ScriptableRendererFeature _insidePortalFeatureOpaque;
    private ScriptableRendererFeature _insidePortalFeatureTransparent;
    private ScriptableRendererFeature _outsidePortalFeatureOpaque;
    private ScriptableRendererFeature _outsidePortalFeatureTransparent;

    void OnEnable()
    {
        _insidePortalFeatureTransparent = _rendererData.rendererFeatures[0];
        _insidePortalFeatureOpaque = _rendererData.rendererFeatures[1];
        _outsidePortalFeatureTransparent = _rendererData.rendererFeatures[2];
        _outsidePortalFeatureOpaque = _rendererData.rendererFeatures[3];
    }

    /// <summary>
    /// Disables pass-through rendering for the mixed reality portal.
    /// </summary>
    public void DisablePassThrough()
    {
        _insidePortalFeatureOpaque.SetActive(false);
        _insidePortalFeatureTransparent.SetActive(false);
        _outsidePortalFeatureOpaque.SetActive(true);
        _outsidePortalFeatureTransparent.SetActive(true);
    }
}
