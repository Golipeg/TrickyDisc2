using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotator : MonoBehaviour
{
    [SerializeField] private float _minAngleZ;
    [SerializeField] private float _maxAngleZ;
    [SerializeField] private float _duration;
    [Range(0, 1)] [SerializeField] private float _time;

    private Quaternion _quaternionMinAngleZ;
    private Quaternion _quaternionMaxAngleZ;
    private float _currentTime;
    private bool _canRotate;


    private void Awake()
    {
        _quaternionMaxAngleZ = Quaternion.Euler(0f, 0f, _maxAngleZ);
        _quaternionMinAngleZ = Quaternion.Euler(0f, 0f, _minAngleZ);
        _canRotate = true;

    }

    private void Update()
    {
        if (_canRotate)
        {
            Rotate();
        }
       
    }

    private void Rotate()
    {
        _currentTime += Time.deltaTime;
        var progress = Mathf.PingPong(_currentTime, _duration) / _duration;
        transform.rotation = Quaternion.Lerp(_quaternionMinAngleZ, _quaternionMaxAngleZ,
            progress);
    }

    public void StartRotate()
    {
        _canRotate = true;
    }

    public void StopRotate()
    {
        _canRotate = false;
    }
}