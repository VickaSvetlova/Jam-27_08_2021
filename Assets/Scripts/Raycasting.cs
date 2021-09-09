using UnityEngine;

public static class Raycasting
{
    public static RaycastHit? RayCast(Vector3 origin, Vector3 direction, float distance, LayerMask layerMask)
    {
        Ray ray = new Ray(origin, direction);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, distance, layerMask))
        {
            Debug.DrawRay(origin, direction, Color.red);
            return hitInfo;
        }

        Debug.DrawRay(origin, direction, Color.yellow);
        return null;
    }
}