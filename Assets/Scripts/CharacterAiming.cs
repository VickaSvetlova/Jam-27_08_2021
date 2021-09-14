using DefaultNamespace;
using UnityEngine;

public class CharacterAiming : MonoBehaviour, IIsActive
{
    [SerializeField] private float turnSpeed = 15;
    private Camera camera;
    [SerializeField] private float distanceRay = 2;

    private void Start()
    {
        camera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        if (!isActive) return;
        float yawCamera = camera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0),
            turnSpeed * Time.fixedDeltaTime);
    }

    public bool isActive { get; set; }

    private void OnDrawGizmos()
    {
        // Gizmos.color = Color.green;
        // Gizmos.DrawRay(camera.transform.position, camera.transform.forward * distanceRay);
    }
}