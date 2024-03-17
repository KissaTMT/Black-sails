public class SwimStats
{
    public float MaxSpeed
    {
        get => _maxSpeed;
        set
        {
            _maxSpeed = value;
        }
    }
    public float Acceleration
    {
        get => _acceleration;
        set
        {
            _acceleration = value;
        }
    }
    public float Deceleration
    {
        get => _deceleration;
        set
        {
            _deceleration = value;
        }
    }
    public float RotationSpeed
    {
        get => _rotationSpeed;
        set
        {
            RotationSpeed = value;
        }
    }

    private float _maxSpeed;
    private float _acceleration;
    private float _deceleration;
    private float _rotationSpeed;

    public SwimStats(SwimConfig swimConfig)
    {
        _maxSpeed = swimConfig.MaxSpeed;
        _acceleration = swimConfig.Acceleration;
        _deceleration = swimConfig.Deceleration;
        _rotationSpeed = swimConfig.RotationSpeed;
    }
    public SwimStats(float maxSpeed, float acceleration, float deceleration, float rotationSpeed)
    {
        _maxSpeed = maxSpeed;
        _acceleration = acceleration;
        _deceleration = deceleration;
        _rotationSpeed = rotationSpeed;
    }
}