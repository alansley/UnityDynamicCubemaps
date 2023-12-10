using UnityEngine;

/// <summary>
/// Class to bobble objects up and down with cosine amplitude.
/// </summary>
public class CosineBobbler : MonoBehaviour
{
    [SerializeField] private float _amplitudeFactor = 10f;

    /// <summary>
    /// Offset so each bobbling object can be a little bit different and we get moving waves rather than a fixed wave
    /// where the entire wave moves up and down in a 'frozen' fashion.
    /// </summary>
    private float _cosineOffset = 0f;
    
    /// <summary>
    /// Unity Update hook.
    /// </summary>
    void Update()
    {
        var tempPos = this.transform.position;
        tempPos.y += Mathf.Cos(Time.time + _cosineOffset) * _amplitudeFactor * Time.deltaTime;
        this.transform.position = tempPos;
    }

    public void SetCosineOffset(float value) { _cosineOffset = value; }
}
