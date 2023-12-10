using UnityEngine;

/// <summary>
/// Class to spawn a bunch of objects which will rotate around the central point of this GameObject's transform.
/// </summary>
public class WhirlwindCubeManager : Singleton<WhirlwindCubeManager>
{
    /// <summary>
    /// Just a flag so we can disable spawning the prefabs should we wish to check performance without them.
    /// </summary>
    [SerializeField] private bool _spawnPrefabs = true;
    
    /// <summary>
    /// Array of whirlwind objects we can instantiate. Each prefab should have a WhirldwindObject attached or it's not
    /// going to do much.
    /// </summary>
    [SerializeField] private GameObject[] _whirlwindObjectPrefabs;
    
    /// <summary>
    /// How many whirlwind objects to spawn?
    /// </summary>
    [SerializeField, Range(1, 1000)] private int _numWhirlwindObjects = 50;
    
    /// <summary>
    /// Unity Start hook.
    /// </summary>
    void Start()
    {
        // Bail if we shouldn't spawn prefabs
        if (!_spawnPrefabs) { return; }
        
        for (int i = 0; i < _numWhirlwindObjects; ++i)
        {
            // Pick a random prefab from the list..
            int randomPrefabIndex = Random.Range(0, _whirlwindObjectPrefabs.Length);
            
            // ..and instantiate the chosen whirlwind object, parenting it to this manager.
            // Note: We don't keep a reference to the instantiated object - we just let them take care of themselves.
            Instantiate(_whirlwindObjectPrefabs[randomPrefabIndex], this.transform);
        }
    }
}
