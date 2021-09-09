using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest : MonoBehaviour
{


    public new Collider collider;
    public Transform treePrefab;
    public int treeCount = 100;

    public float multiX, multiY;

    // Start is called before the first frame update
    void Start()
    {
    }

    public Transform treees;

    //[ContextMenu("Generate")]
    //void Generate() {
    //    foreach (Transform tree in treees) {
    //        DestroyImmediate(tree.gameObject, true);
    //    }

    //    var bounds = collider.bounds;
    //    for (int i = 0; i < treeCount; i++) {
    //        float x = Random.Range(bounds.min.x + 1f, bounds.max.x - 1f);
    //        float z = Random.Range(bounds.min.z + 1f, bounds.max.z - 1f);
    //        Vector3 pos = new Vector3(x, bounds.max.y + 1f, z);
    //        RaycastHit hit;
    //        if (Physics.Raycast(pos, Vector3.down, out hit)) {
    //            treees.Add(Instantiate(treePrefab, hit.point, Quaternion.identity, transform));
    //        }
    //    }
    //}

    [ContextMenu("Random")]
    void Randomize() {
        foreach (Transform tree in treees) {
            tree.GetComponent<TreeRandomizer>().Randomize();
        }
    }

}
