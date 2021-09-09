using System;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    private MeshRenderer renderer;
    public SoundEffector.TypeFloor TypeFloor;

    private void OnEnable()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;
    }
}