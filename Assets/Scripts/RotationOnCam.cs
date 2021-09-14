using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationOnCam : MonoBehaviour
{
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void FixedUpdate()
    {
        transform.forward = camera.transform.forward;
    }
}