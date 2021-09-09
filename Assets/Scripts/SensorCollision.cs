using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class SensorCollision : MonoBehaviour
    {
        [SerializeField] private Hater hater;

        private CapsuleCollider capsuleCollider;

        private void Start()
        {
            capsuleCollider = GetComponent<CapsuleCollider>();
        }

        private void OnTriggerStay(Collider other)
        {
            var tempPlayer = other.GetComponent<PlayerController>();
            if (tempPlayer != null)
            {
                if (tempPlayer.InArm)
                {
                    var diametr = capsuleCollider.radius;
                    var distanceHaterAnchor = Vector3.Distance(hater.transform.position, hater.AnchorRadius.position);
                    var distanceHaterPlayer = Vector3.Distance(hater.transform.position, tempPlayer.transform.position);
                    var distanceNear = distanceHaterAnchor - diametr;
                    var far = distanceHaterAnchor - distanceNear;
                    var player = distanceHaterPlayer - distanceNear;
                    var cof = 1 - player / far;
                    tempPlayer.InArm.SetDebaf(cof);
                    hater.SetPosition(tempPlayer.transform.position);
                } else if (hater.currentState == Hater.States.Chase) {
                    hater.SetState(Hater.States.Patrol);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var tempPlayer = other.GetComponent<PlayerController>();
            if (tempPlayer != null) {
                if (tempPlayer.InArm) {
                    hater.SetState(Hater.States.Scan);
                    tempPlayer.InArm.SetDebaf(1);
                }
            }
        }
    }
}