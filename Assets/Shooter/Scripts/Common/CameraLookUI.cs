using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookUI : MonoBehaviour
{
    private Camera _gameCamera;
    private void Start()
    {
        _gameCamera = Camera.main;
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position +_gameCamera.transform.rotation*Vector3.back ,
            _gameCamera.transform.rotation * Vector3.up);
    }
}
