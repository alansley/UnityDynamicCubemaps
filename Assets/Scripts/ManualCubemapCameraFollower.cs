using UnityEngine;

public class ManualCubemapCameraFollower : MonoBehaviour
{
    /// <summary>
    /// The transform of the manual cubemap that we will move with the camera position.
    /// </summary>
    [SerializeField] private Transform _manualCubemapTransform;

    /// <summary>
    /// Cached reference to the main camera.
    /// </summary>
    private Camera _camera;

    /// <summary>
    /// Unity Start hook.
    /// </summary>
    void Start()
    {
        _camera = Camera.main;
        if (!_manualCubemapTransform) { _manualCubemapTransform = this.transform; }
    }

    /// <summary>
    /// Unity Update hook.
    /// </summary>
    void Update() { _manualCubemapTransform.position = _camera.transform.position; }
}
