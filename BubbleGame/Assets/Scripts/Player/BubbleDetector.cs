using UnityEngine;
using UnityEngine.Serialization;

public class BubbleDetector : MonoBehaviour
{
    // オフセット
    private Vector3 offset;

    // 球の半径
    private float radius;

    // rayの長さ
    private float length;

    // ハイトからのどれくらいの距離まで接地とみなすか？
    private float distanceThreshould;

    // ハイトのレイヤ
    private int layerMask;

    public bool IsHit { get; protected set; }

    public float Distance { get; protected set; }

    public Vector3 HitPosition { get; protected set; }

    public Vector3 Normal { get; protected set; }

    public Collider Collider { get; protected set; }

    public RaycastHit RaycastHit { get; protected set; }

    public Vector3 Direction { get; set; }

    public void Initialize(float radius, float length, float margin, float distanceThreshould, int layerMask)
    {
        offset.y = radius + margin;
        this.radius = radius;
        this.length = length;
        this.distanceThreshould = distanceThreshould;
        this.layerMask = layerMask;
    }

    public void UpdateDetection(Vector3 _direction)
    {
        Vector3 hitPosition;
        Vector3 normal;
        float distance;

        var collider = HitDetector.HitDetection(transform, offset, Direction, radius, length + offset.y, layerMask, out distance, out hitPosition,
            out normal);
        if (collider != null)
        {
            IsHit = distance <= distanceThreshould;
            Distance = distance;
            HitPosition = hitPosition;
            Normal = normal;
            Collider = collider;
            RaycastHit = HitDetector.LatestRaycastHit;
        }
        else
        {
            IsHit = false;
            Distance = distance;
            HitPosition = hitPosition;
            Normal = normal;
            Collider = null;
            RaycastHit = new RaycastHit();
        }
    }
}