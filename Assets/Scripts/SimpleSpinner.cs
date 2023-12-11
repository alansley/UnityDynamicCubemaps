using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SimpleSpinner : MonoBehaviour
{
    [FormerlySerializedAs("rotEulers")] [SerializeField] private Vector3 _rotEulers = new Vector3(10f, 20f, 30f);

    [SerializeField] private Space _coordinateSystem = Space.World;

    private Transform _transformToRotate;

    // Start is called before the first frame update
    void Start()
    {
        if (!_transformToRotate) { _transformToRotate = this.transform; }

    }

    // Update is called once per frame
    void Update()
    {
        _transformToRotate.Rotate(_rotEulers * Time.deltaTime, _coordinateSystem);
    }
}