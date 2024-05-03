using UnityEngine;

public struct Bounds
{
    public Vector2 Min;
    public Vector2 Max;

    public Vector2 BottomLeft; 
    public Vector2 BottomRight; 
    public Vector2 TopLeft; 
    public Vector2 TopRight;

    public Bounds(Vector2 min, Vector2 max)
    {
        Min = min;
        Max = max;

        BottomLeft = min;
        BottomRight = new Vector2(max.x, min.y);
        TopLeft = new Vector2(min.x, max.y);
        TopRight = max;
    }
}
public class RadarTarget
{
    public readonly Transform Transform;
    public Collider2D Collider => _collider;
    public Vector2 Position => Transform.position;
    public Vector2 Direction => Transform.up;
    public Vector2 Size => _collider.bounds.size;
    public Vector2 Extents => _collider.bounds.extents;
    public Bounds Bounds => new Bounds(_collider.bounds.min, _collider.bounds.max);
    public float Rotation => Transform.rotation.eulerAngles.z;

    private Collider2D _collider;

    public RadarTarget(Collider2D collider)
    {
        _collider = collider;
        Transform = collider.GetComponent<Transform>();
    }
    public Vector2 CalculateDelta(Vector2 position) => Position - position;
    public float CalculateAngleBetweenDirections(Vector2 direction) => Vector2.SignedAngle(direction, Direction);
}
