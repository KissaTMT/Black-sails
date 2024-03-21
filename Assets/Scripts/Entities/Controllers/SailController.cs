using UnityEngine;

public class SailController
{
    private const float ROTATE_SPEED = 0.5f;
    private Transform _transform;

    private Vector2 _size;
    private float _increaseModifier;
    public SailController(Transform transform)
    {
        _transform = transform;
        _size = transform.localScale;

        _increaseModifier = 0.1f;
    }
    public SailController(Transform transform, float increaseModifier)
    {
        _transform = transform;
        _size = transform.localScale;

        _increaseModifier = increaseModifier;
    }
    public void ReactTo(Vector2 direction)
    {
        if (direction.y != 0) _transform.localScale = direction.y > 0 ? Increase() : Decrease();
        Rotate(direction);
    }
    private Vector2 Increase()
    {
        return new Vector2(_transform.localScale.x, Mathf.Lerp(_transform.localScale.y, _size.y + _increaseModifier, Time.deltaTime));
    }
    private Vector2 Decrease()
    {
        return new Vector2(_transform.localScale.x, Mathf.Lerp(_transform.localScale.y, _size.y - _increaseModifier, Time.deltaTime));
    }
    private void Rotate(Vector2 direction)
    {
        var angle = direction.x * Mathf.Rad2Deg/3;
        _transform.localRotation = Quaternion.Lerp(_transform.localRotation, Quaternion.Euler(0, 0, -angle), ROTATE_SPEED * Time.deltaTime);
    }
}
