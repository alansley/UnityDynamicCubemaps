using UnityEngine;

/// <summary>
/// Class to spawn a circle of objects which bob up and down.
/// </summary>
public class BobbingObjectSpawner : MonoBehaviour
{
    /// <summary>
    /// How many bobbing objects should we have?
    /// </summary>
    [SerializeField, Range(4, 72)] private int _numInstances = 36;
    
    /// <summary>
    /// List of prefabs to choose from - we take each in turn and modulus the index, so 0/1/2/ 0/1/2/ 0/1/2 etc.
    /// </summary>
    [SerializeField] private GameObject[] _prefabs;

    /// <summary>
    /// How far out from the camera should the bobbing objects be placed.
    /// </summary>
    [SerializeField] private float _distanceOffset = 10f;

    /// <summary>
    /// Cosine wave amplitude - higher values give bigger waves.
    /// </summary>
    [SerializeField] private float _waveAmplitude = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
        // How far will be rotate the position of each object instantiated
        float rotStepDegs = 360f / _numInstances;
        
        // Spawn all bobbing objects.
        for (int i = 0; i < _numInstances; ++i)
        {
            // Which prefab should we spawn? We'll cycle through the one's available..
            int prefabIndex = i % _prefabs.Length;

            // How far we're stepping around the Y-axis
            float rotAmountDegs = rotStepDegs * i;
            
            // Use the same value in rads for the Y-offset
            float rotAmountRads = rotAmountDegs * (Mathf.PI / 180f);

            float cosineOffset = i * 4f;
            
            // Decide where we want to place the spawned object
            Vector3 positionWS = new Vector3(0f, (Mathf.Cos(rotAmountRads * 10f) * _waveAmplitude) - 2f, _distanceOffset);

            // Instantiate a prefab, parenting it to this object spawner
            var go = Instantiate(_prefabs[prefabIndex], positionWS, Quaternion.identity, this.transform);

            // Place our instantiated object at the LOCAL version of our spawn position so it's around the cubemap 
            // creating cameras
            go.transform.localPosition = positionWS;

            // Give each object an offset so it bobs in a different phase
            go.GetComponent<CosineBobbler>().SetCosineOffset(cosineOffset);
            
            // Rotate the object around the spawner's position so they form a circle around the Y-axis
            go.transform.RotateAround(this.transform.position, Vector3.up, rotAmountDegs);
        }
    }
}
