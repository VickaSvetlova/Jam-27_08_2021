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
                if (tempPlayer.idea && !tempPlayer.isHidden) {
                    tempPlayer.idea.AddDebuf(1);
                    hater.SetState(Hater.States.Chase);
                } 
            }
        }

        private void OnTriggerStay(Collider other) {
            if (hater.currentState == Hater.States.Chase && other.tag == "Player") {
                var tempPlayer = other.GetComponent<PlayerController>();
                if (tempPlayer.isHidden) tempPlayer.SetHidden(false);
                if (tempPlayer.idea) {
                    hater.SetPosition(tempPlayer.transform.position);
                } else {
                    hater.SetState(Hater.States.Patrol);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player") {
                var tempPlayer = other.GetComponent<PlayerController>();
                if (tempPlayer.idea && !tempPlayer.isHidden) {
                    tempPlayer.idea.AddDebuf(-1);
                    hater.SetState(Hater.States.Scan);
                }
            }
        }
    }
}