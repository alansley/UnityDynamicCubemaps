using UnityEngine;

using TMPro;

/// <summary>
/// Class to allow us to adjust the Metallic and Smoothness values of a material via sliders.
/// </summary>
public class CentralSphereMaterialAdjuster : MonoBehaviour
{
    /// <summary>
    /// Labels to change the text on as the sliders move.
    /// </summary>
    [SerializeField] private TMP_Text _metallicSliderValueLabel;
    [SerializeField] private TMP_Text _smoothnessSliderValueLabel;
    
    /// <summary>
    /// Cached reference to the material on this GameObject.
    /// </summary>
    private Material _thisMaterial;
    
    /// <summary>
    /// Indices to access the "_Metallic" and "_Smoothness" shader properties (index access is faster than string-based)
    /// </summary>
    private int _metallicPropertyIndex;
    private int _smoothnessPropertyIndex;
    
    /// <summary>
    /// Unity Start hook.
    /// </summary>
    void Start()
    {
        // Grab the material we're going to be adjusting
        _thisMaterial = this.gameObject.GetComponent<MeshRenderer>().material;
        if (!_thisMaterial) { Debug.LogError("[CentralSphereMaterialAdjuster] Could not get material!"); }

        // Grab the indices of shader properties so we can quickly set them based on slider movement
        // CAREFUL: We cannot used the ID obtained from `_thisMaterial.shader.FindPropertyIndex("_Metallic")` here - it doesn't work!
        _metallicPropertyIndex = Shader.PropertyToID("_Metallic");
        _smoothnessPropertyIndex = Shader.PropertyToID("_Smoothness");
        Debug.Log($"Metallic property is at index: {_metallicPropertyIndex}");
        Debug.Log($"Smoothness property is at index: {_smoothnessPropertyIndex}");
        
        // Warn if slider value labels aren't connected
        if (!_metallicSliderValueLabel)   { Debug.LogWarning("[CentralSphereMaterialAdjuster] Metallic slider label not connected!");   }
        if (!_smoothnessSliderValueLabel) { Debug.LogWarning("[CentralSphereMaterialAdjuster] Smoothness slider label not connected!"); }
    }

    /// <summary>
    /// Method to set the material metallic property on slider value changed.
    /// </summary>
    /// <param name="value">The new value of the slider.</param>
    public void OnMetallicSliderValueChanged(float value)
    {
        _thisMaterial.SetFloat(_metallicPropertyIndex, value);
        _metallicSliderValueLabel.text = value.ToString("N2");
    }
    
    /// <summary>
    /// Method to set the material smoothness property on slider value changed.
    /// </summary>
    /// <param name="value">The new value of the slider.</param>
    public void OnSmoothnessSliderValueChanged(float value)
    {
        _thisMaterial.SetFloat(_smoothnessPropertyIndex, value);
        _smoothnessSliderValueLabel.text = value.ToString("N2");
    }
}
