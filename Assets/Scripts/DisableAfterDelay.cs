using System.Collections;

using UnityEngine;

/// <summary>
/// Class to wait a while then destroy the GameObject it's attached to.
/// </summary>
public class DisableAfterDelay : MonoBehaviour
{
    [SerializeField] private GameObject _gameObjectToDestroy;
    
    /// <summary>
    /// How many seconds to wait before destroying this object.
    /// </summary>
    [SerializeField] private float _delayUntilWeDisableThisObjectSecs = 5f;
    
    /// <summary>
    /// Unity Start hook.
    /// </summary>
    void Start()
    {
        // If no game object was specified we'll destroy the GameObject this script is attached to
        if (!_gameObjectToDestroy) { _gameObjectToDestroy = this.transform.gameObject; }
        
        StartCoroutine(nameof(WaitThenDisableThisObject));
    }

    /// <summary>
    /// Coroutine to wait and then destroy a given GameObject.
    /// </summary>
    private IEnumerator WaitThenDisableThisObject()
    {
        yield return new WaitForSeconds(_delayUntilWeDisableThisObjectSecs);
        Destroy(_gameObjectToDestroy);
    }
}
