using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkyboxSwitcher : MonoBehaviour
{
    [SerializeField] private TMP_Text _skyboxNameLabel;

    [SerializeField] private List<Material> _skyboxMaterials = new List<Material>();


    private int _currentSkyboxMaterialIndex = 0;

    //private List<ReflectionProbe> _reflectionProbes;

    // Start is called before the first frame update
    void Start()
    {
        // Warn if the skybox level isn't connected or there are no skybox materials
        if (!_skyboxNameLabel) { Debug.LogWarning("Please attach skybox name label!"); }
        if (_skyboxMaterials.Count == 0) { Debug.LogWarning("Skybox materials list is empty! Please add some!"); }

        // Set the initial skybox and update the text label
        RenderSettings.skybox = _skyboxMaterials[_currentSkyboxMaterialIndex];
        _skyboxNameLabel.text = _skyboxMaterials[_currentSkyboxMaterialIndex].name;
    }

    /// <summary>
    /// Method to re-render all the reflection probes when we change skybox texture.
    /// </summary>
    private void UpdateAllReflectionProbes()
    {
        // Find all the reflection probes
        var reflectionProbes = FindObjectsByType<ReflectionProbe>(FindObjectsSortMode.None);
        foreach (var probe in reflectionProbes) { probe.RenderProbe(); }
    }

    public void OnClickNextSkybox()
    {
        // Move to the next skybox index, wrapping around as needs be, then assign it
        _currentSkyboxMaterialIndex = (_currentSkyboxMaterialIndex + 1) % _skyboxMaterials.Count;
        RenderSettings.skybox = _skyboxMaterials[_currentSkyboxMaterialIndex];
        _skyboxNameLabel.text = _skyboxMaterials[_currentSkyboxMaterialIndex].name;

        UpdateAllReflectionProbes();
    }

    public void OnClickPreviousSkybox()
    {
        // Move to the previous skybox index, wrapping around as needs be, then assign it
        _currentSkyboxMaterialIndex--;
        if (_currentSkyboxMaterialIndex < 0) { _currentSkyboxMaterialIndex = _skyboxMaterials.Count - 1; }
        RenderSettings.skybox = _skyboxMaterials[_currentSkyboxMaterialIndex];
        _skyboxNameLabel.text = _skyboxMaterials[_currentSkyboxMaterialIndex].name;

        UpdateAllReflectionProbes();
    }
}