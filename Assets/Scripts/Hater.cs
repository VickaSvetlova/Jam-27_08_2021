using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Hater : MonoBehaviour
{
    public enum States
    {
        Patrol,
        Chase,
        Scan,
        Wait
    }

    [SerializeField] private Transform anchorTransform;
    [SerializeField] private Transform anchorRadius;
    [SerializeField] private Transform anchorCenterRadius;
    [SerializeField] private float patrolRadius;
    [SerializeField] private GameObject body;
    [SerializeField] private float distanceClouse = 0;
    [SerializeField] private float speedMovePatrol;
    [SerializeField] private float speedMoveChase;
    [SerializeField] private float speedRotation = 10f;
    [SerializeField] private float speedRotationChase = 10f;
    [SerializeField] private float timeToScan = 2f;
    [SerializeField] private float turnSpeed;
    private float timeScan;
    private Rigidbody rigidBody;
    public States currentState;
    private States lastState;
    public Vector3 targetMove;
    public Vector3 targetDir;
    private Vector3 lastPosPlayer;
    private Quaternion scanRotation;

    public float DEbug;

    [SerializeField] private LayerMask raycastLayerMask;
    [SerializeField] private LayerMask raycastWithCharLayerMask;

    public Transform AnchorRadius
    {
        get => anchorRadius;
        set => anchorRadius = value;
    }


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        currentState = States.Patrol;
        targetMove = GetRandomPointInRadiusPatrol();
        targetDir = targetMove - transform.position;
        targetDir.y = 0;
        timeScan = timeToScan;
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case States.Patrol:
                Vector3 pos = transform.position;
                pos.y = 0;
                Vector3 tpos = targetMove;
                tpos.y = 0;
                DEbug = Vector3.Distance(pos, tpos);
                if (Vector3.Distance(pos, tpos) > distanceClouse)
                {
                    MoveToPosition(targetMove, targetDir);
                }
                else
                {
                    targetMove = GetRandomPointInRadiusPatrol();
                    targetDir = targetMove - transform.position;
                    targetDir.y = 0;
                }

                break;
            case States.Chase:
                MoveToPosition(targetMove, targetDir);
                break;
            case States.Scan:
                if (timeScan < 0)
                {
                    SetState(States.Patrol);
                }
                else {
                    float factor = 1f - timeScan / timeToScan;
                    if (factor > .5f) {
                        Quaternion dir = scanRotation * Quaternion.AngleAxis(45f, Vector3.up);
                        body.transform.rotation =
                            Quaternion.Lerp(body.transform.rotation, dir, (factor - .5f) * 2f);
                    } else if (factor < .5f) {
                        Quaternion dir = scanRotation * Quaternion.AngleAxis(-45f, Vector3.up);
                        body.transform.rotation =
                            Quaternion.Lerp(body.transform.rotation, dir, factor * 2f);
                    }
                    timeScan -= Time.fixedDeltaTime;
                }

                break;
            case States.Wait:
                break;
        }
    }

    private void MoveToPosition(Vector3 move, Vector3 dir) {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position + Vector3.up * 50f, 1f, Vector3.down, out hit, 100f, raycastLayerMask)) {
            move.y = hit.point.y + 5f;
        } else {
            move.y = anchorTransform.position.y;
        }
        float speed = (currentState == States.Chase) ? speedMoveChase : speedMovePatrol;
        float speedRot = (currentState == States.Chase) ? speedRotationChase : speedRotation;
        rigidBody.MovePosition(
            Vector3.MoveTowards(transform.position, move, Time.deltaTime * speed));
        body.transform.forward =
            Vector3.Lerp(body.transform.forward, dir, Time.deltaTime * speedRot);
    }

    Vector3 GetRandomPointInRadiusPatrol()
    {
        var randAngle = Random.Range(0, 360);
        var randomDistance = Random.Range(0, patrolRadius);
        var direction = Quaternion.Euler(0, randAngle, 0) * Vector3.forward;
        var targetPos = anchorTransform.position + direction * randomDistance;
        return targetPos;
    }

    public void SetPosition(Vector3 character)
    {
        RaycastHit hit;
        if (Physics.Raycast(character + Vector3.up * 100f, Vector3.down, out hit, 200f, raycastWithCharLayerMask)) {
            var tempChar = hit.collider.GetComponent<PlayerController>();
            if (tempChar != null) {
                targetDir = character - transform.position;
                targetDir.y = 0;
                var distance = Vector3.Distance(anchorCenterRadius.position, transform.position);
                targetMove = character - targetDir.normalized * distance;
                SetState(States.Chase);
                return;
            }
        }
        SetState(States.Scan);
    }

    public void SetState(States state)
    {
        lastState = currentState;
        currentState = state;
        if (state == States.Scan) {
            timeScan = timeToScan;
            scanRotation = body.transform.rotation;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }
}