using UnityEngine;
using UnityEngine.Networking;

public static class HitDetector
{
    public const int MaxHitDetection = 16;

    public static RaycastHit LatestRaycastHit { get; private set; }

    private static RaycastHit[] temporalyRayCastHits = new RaycastHit[MaxHitDetection];

    public static Collider HitDetection(
        Transform transform, Vector3 offsetPosition, Vector3 direction, float radius, float length, int layerMask,
        out float distance, out Vector3 hitPosition, out Vector3 normal)
    {
        hitPosition = Vector3.zero;
        distance = 0.0f;
        normal = Vector3.zero;

        Vector3 position = transform.position;
        Vector3 checkPosition = transform.TransformPoint(offsetPosition);
        Vector3 checkDirection = transform.TransformDirection(direction);
        int hitNum = Physics.SphereCastNonAlloc(checkPosition, radius, checkDirection, temporalyRayCastHits, length, layerMask);

        if (hitNum <= 0)
        {
            return null;
        }

        Collider result = null;
        var minDistance = float.MaxValue;
        for (var i = 0; i < hitNum; i++)
        {
            var raycastHit = temporalyRayCastHits[i];
            var tmpHitPosition = raycastHit.point;
            var tmpDistance = Vector3.Dot(position - tmpHitPosition, raycastHit.normal);
            if (tmpDistance < minDistance)
            {
                LatestRaycastHit = raycastHit;
                result = raycastHit.collider;
                hitPosition = tmpHitPosition;
                distance = tmpDistance;
                normal = raycastHit.normal;
                minDistance = tmpDistance;
            }
        }
        
        return result;
    }
}