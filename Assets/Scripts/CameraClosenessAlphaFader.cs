using UnityEngine;

/// <summary>
/// Class to fade out the alpha of an object's material if it gets too close to the camera (because clipping through
/// the near plane of the camera look bleh!)
/// </summary>
public class CameraClosenessAlphaFader : MonoBehaviour
{
    /// <summary>
    /// If the transform position of this object is within this distance of the camera we will start to fade out the
    /// GameObject's material.
    /// </summary>
    [SerializeField] private float _startFadeDistance = 2f;
    
    /// <summary>
    /// Within this distance from the camera the GameObject's material will be completely transparent (i.e., it's alpha
    /// will be 0f).
    /// </summary>
    [SerializeField] private float _completeFadeDistance = 1f;

    /// <summary>
    /// Animation curve to control the alpha lerp, should we wish it to be non-linear.
    /// </summary>
    [SerializeField] private AnimationCurve _alphaFadeAnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    
    /// <summary>
    /// Cached reference to the main camera.
    /// </summary>
    private Camera _camera;

    /// <summary>
    /// Cached reference to the object's material.
    /// </summary>
    private Material _material;

    /// <summary>
    /// We'll keep track of the original object colour and a version with zero alpha and lerp between them as the object
    /// gets close to the camera.
    /// </summary>
    private Color _originalColour;
    private Color _originalColourWithZeroAlpha;

    /// <summary>
    /// The distance / range over which the alpha fading occurs, calculated as _startFadeDistance minus _completeFadeDistance.
    /// </summary>
    private float _fadeRange;
    
     /// <summary>
     /// Unity Start hook. 
     /// </summary>
    void Start()
    {
        _camera = Camera.main;

        _material = this.gameObject.GetComponent<MeshRenderer>().material;
        if (!_material) { Debug.LogWarning("Could not get material!"); }

        // Keep a copy of the original colour and a version where the alpha value is zero
        _originalColour = _material.color;
        _originalColourWithZeroAlpha = new Color(_originalColour.r, _originalColour.g, _originalColour.b, 0f);

        // Work out the distance of which the fade will occur
        _fadeRange = _startFadeDistance - _completeFadeDistance;
    }

    // Update is called once per frame
    void Update()
    {
        // Work out how fare away from the camera this object is
        float distance = Vector3.Distance(_camera.transform.position, this.transform.position);

        // If it's within our start-to-fade threshold..
        if (distance < _startFadeDistance)
        {
            // ..get a clamped distance that hits zero at our 'complete-fade-out' distance threshold.
            float clampedDistance = distance - _completeFadeDistance;
            if (clampedDistance < 0f) { clampedDistance = 0f; }
            
            // Work this as a value 0..1 over our fade range & put it through the animation curve
            float t = clampedDistance / _fadeRange;
            float lerpFactor = _alphaFadeAnimationCurve.Evaluate(t);

            // Lerp between the original-with-alpha and non-alpha versions of the GameObject's colour
            _material.color = Color.Lerp(_originalColourWithZeroAlpha, _originalColour, lerpFactor);
        }
        else
        {
            // Just to make sure we don't leave something 0.99f alpha or such if it's outside the range we'll set the
            // original (with alpha) colour.
            _material.color = _originalColour;
        }
    }
}
