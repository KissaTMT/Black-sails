using UnityEngine;

public class RadarTarget
{
    public readonly Transform Transform;
    public Vector2 Position => Transform.position;
    public Vector2 Direction => Transform.up;
    public Vector2 Size => _collider.bounds.size;
    public (Vector2 min, Vector2 max) Offsets => (_collider.bounds.min, _collider.bounds.max);
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
