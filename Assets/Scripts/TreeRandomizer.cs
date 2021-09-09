using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRandomizer : MonoBehaviour
{

    public LayerMask layerMask;

    private void Start() {
        
    }

    public void Randomize()
    {
        transform.localEulerAngles = new Vector3(0, Random.Range(0f, 360f), 0);
        transform.localScale = new Vector3(1f, Random.Range(0.9f, 1.3f), 1f);
        Vector3 pos = transform.position + Vector3.up * 10f;
        RaycastHit hit;
        if (Physics.Raycast(pos, Vector3.down, out hit, 20f, layerMask)) {
            transform.position = hit.point - Vector3.up * .2f;
        }
    }
}
