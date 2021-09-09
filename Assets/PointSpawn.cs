using UnityEngine;

public class PointSpawn : MonoBehaviour
{
    public bool isBusy { get; set; }

    private void OnTriggerStay(Collider other)
    {
        var tempOther = other.GetComponent<Idea>();
        if (tempOther != null)
        {
            isBusy = true;
        }
        else
        {
            isBusy = false;
        }
    }

}