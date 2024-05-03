using UnityEngine;

public class PhysicsSwimController : ISwimController
{
    public float CurrentSpeed => _currentSpeed;

    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private SwimStats _swimStats;

    private Vector2 _direction;

    private float _currentSpeed;

    public PhysicsSwimController(Transform transform, Rigidbody2D rigidbody, SwimStats swimStats)
    {
        _transform = transform;
        _rigidbody = rigidbody;
        _swimStats = swimStats;
    }
    public void Swim(Vector2 direction)
    {
        _direction = direction;

        if (_direction.x != 0) Turn();
        if (_direction.y != 0) _currentSpeed = _direction.y > 0 ? Overclocking() : Bracking();

        _rigidbody.velocity = _transform.up * _currentSpeed;
    }
    private float Overclocking()
    {
        return Mathf.Lerp(_currentSpeed, _swimStats.MaxSpeed, _swimStats.Acceleration * Time.deltaTime);
    }
    private float Bracking()
    {
        return Mathf.Lerp(_currentSpeed, 0, _swimStats.Deceleration * Time.deltaTime);
    }
    private void Turn()
    {
        var angle = Mathf.Sign(_direction.x) * Mathf.Rad2Deg;
        _rigidbody.rotation = Mathf.Lerp(_rigidbody.rotation, _rigidbody.rotation - angle, _swimStats.RotationSpeed * (_swimStats.MaxSpeed - _currentSpeed / 2) * Time.deltaTime);
    }
}