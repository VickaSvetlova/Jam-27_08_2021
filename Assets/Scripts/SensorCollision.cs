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

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                var tempPlayer = other.GetComponent<PlayerController>();
                if (tempPlayer.InArm) {
                    tempPlayer.InArm.AddDebuf(1);
                    hater.SetState(Hater.States.Chase);
                } 
            }
        }

        private void OnTriggerStay(Collider other) {
            if (other.tag == "Player") {
                var tempPlayer = other.GetComponent<PlayerController>();
                if (tempPlayer.InArm) {
                    hater.SetPosition(tempPlayer.transform.position);
                } else if (hater.currentState == Hater.States.Chase) {
                    hater.SetState(Hater.States.Patrol);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player") {
                var tempPlayer = other.GetComponent<PlayerController>();
                if (tempPlayer.InArm) {
                    tempPlayer.InArm.AddDebuf(-1);
                    hater.SetState(Hater.States.Scan);
                }
            }
        }
    }
}