using UnityEngine;

/// <summary>
/// Class that defines an object that rotates on the Y-axis around a given transform position so it acts like debris
/// in a whirlwind. The object this class is attached to also rotate around it own center.
/// </summary>
public class WhirlwindObject : MonoBehaviour
{
    /// <summary>
    /// Initial offset placement ranges. Note: All values should be positive as we then rotate around Y to distribute.
    /// </summary>
    [SerializeField] private Vector2 _xOffsetPosRange = new Vector2(1f, 10f);
    [SerializeField] private Vector2 _yOffsetPosRange = new Vector2(1f, 10f);
    [SerializeField] private Vector2 _zOffsetPosRange = new Vector2(1f, 10f);
    
    /// <summary>
    /// How fast the object rotates around its own center.
    /// </summary>
    [SerializeField] private Vector2 _xRotAboutSelfRange = new Vector2(10f, 50f);
    [SerializeField] private Vector2 _yRotAboutSelfRange = new Vector2(10f, 50f);
    [SerializeField] private Vector2 _zRotAboutSelfRange = new Vector2(10f, 50f);

    /// <summary>
    /// How fast the object rotates around the center of the whirlwind in degs per sec.
    /// </summary>
    [SerializeField] private Vector2 _whirlwindRotRangeDegsPerSec = new Vector2(10f, 20f);

    /// <summary>
    /// Ranged for the red/green/blue components of this object materials random colour.
    /// </summary>
    [SerializeField] private Vector2 _redRange   = new Vector2(0.2f, 1.0f);
    [SerializeField] private Vector2 _greenRange = new Vector2(0.2f, 1.0f);
    [SerializeField] private Vector2 _blueRange  = new Vector2(0.2f, 1.0f);

    /// <summary>
    /// Flag to allow us to enable/disable emission on this object's material.
    /// </summary>
    [SerializeField] private bool _useEmissionOnMaterial = true;
    
    /// <summary>
    /// Random range for our emission intensity (i.e., glow).
    /// </summary>
    [SerializeField] private Vector2 _emissionIntensityRange = new Vector2(0.5f, 3f);

    /// <summary>
    /// The random emission intensity generated from our emission intensity range.
    /// </summary>
    private float _emissionIntensity;
    
    /// <summary>
    /// How fast this object will rotate around it's parent (i.e. the center of the whirlwind) in degs per sec.
    /// </summary>
    private float _whirlwindRotDegsPerSec;

    /// <summary>
    /// Euler angles for how many degrees per second this object will rotate about its own center.
    /// </summary>
    private Vector3 _rotateAboutSelfEulers;

    /// <summary>
    /// Property to hold delta time so we only have to call Time.deltaTime once per update.
    /// </summary>
    private float _deltaTime;
    
    /// <summary>
    /// Unity Start hook.
    /// </summary>
    public void Start()
    {
        // Place the whirlwind object a random world-space position within our specified ranges
        float xPos = Random.Range(_xOffsetPosRange.x, _xOffsetPosRange.y);
        float yPos = Random.Range(_yOffsetPosRange.x, _yOffsetPosRange.y);
        float zPos = Random.Range(_zOffsetPosRange.x, _zOffsetPosRange.y);
        this.transform.position = new Vector3(xPos, yPos, zPos);

        // Because our X and Z ranges are positive the object will end up in one quadrant - so rotate randomly around
        // its parent to distribute the objects
        float rotationAboutParentDegs = Random.Range(0f, 360f);
        this.transform.RotateAround(this.transform.parent.position, Vector3.up, rotationAboutParentDegs);
        
        // Pick how fast this object will rotate around its own center
        float individualXRotDegsPerSec = Random.Range(_xRotAboutSelfRange.x, _xRotAboutSelfRange.y);
        float individualYRotDegsPerSec = Random.Range(_yRotAboutSelfRange.x, _yRotAboutSelfRange.y);
        float individualZRotDegsPerSec = Random.Range(_zRotAboutSelfRange.x, _zRotAboutSelfRange.y);
        _rotateAboutSelfEulers = new Vector3(individualXRotDegsPerSec, individualYRotDegsPerSec, individualZRotDegsPerSec);
        
        // Pick how fast this object will rotate around the whirlwind center
        _whirlwindRotDegsPerSec = Random.Range(_whirlwindRotRangeDegsPerSec.x, _whirlwindRotRangeDegsPerSec.y);
        
        // Grab the material for this object
        var material = this.gameObject.GetComponent<MeshRenderer>().material;
            
        // Assign a random colour for this object..
        float r = Random.Range(_redRange.x, _redRange.y);
        float g = Random.Range(_greenRange.x, _greenRange.y);
        float b = Random.Range(_blueRange.x, _blueRange.y);
        var randomColour = new Color(r, g, b);
        material.color = randomColour;
        
        // ..and choose and apply a random emission intensity of the same colour.
        // Note: Emission intensity should be 2-to-the-power-of the intensity you want.
        // Source: https://forum.unity.com/threads/change-a-materials-emission-color-intensity-property.611206/#post-7916227
        _emissionIntensity = Mathf.Pow(2, Random.Range(_emissionIntensityRange.x, _emissionIntensityRange.y));
        material.SetColor("_EmissionColor", randomColour * _emissionIntensity);

        // Enable or disable our materials emission based on our flag.
        if (_useEmissionOnMaterial) { material.EnableKeyword("_EMISSION"); } else { material.DisableKeyword("_EMISSION"); }
    }
    
    /// <summary>
    /// Unity Update hook.
    /// </summary>
    public void Update()
    {
        _deltaTime = Time.deltaTime;
        
        // Rotate the whirlwind object around it's own center
        this.transform.Rotate(_rotateAboutSelfEulers * _deltaTime);
        
        // Rotate the whirlwind object around the center of the whirlwind (i.e., its parent)
        this.transform.RotateAround(this.transform.parent.position, Vector3.up, _whirlwindRotDegsPerSec * _deltaTime);
    }
    
}
