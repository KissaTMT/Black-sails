using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform Transform => _transform;
    public Transform Gun => _gun;
    public Vector3 Aim => _gun.right * _range;
    public float Range => _range;

    [SerializeField] private Transform _gun;

    [SerializeField] private Cannonball _cannonballPrefab;
    [SerializeField] private ParticleSystem _explosion;

    [SerializeField] private float _range;
    [SerializeField] private float _cooldawnTime;

    private Transform _transform;
    private Animator _animator;

    private AutoMonoPool<Cannonball> _pool;
    private Cooldawn _cooldawn;
    public virtual void Init()
    {
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _pool = new AutoMonoPool<Cannonball>(_cannonballPrefab, 1);
        _cooldawn = new Cooldawn(_cooldawnTime);
    }
    public void Shoot()
    {
        if (_cooldawn.IsReady()) Launch();
    }
    private void Launch()
    {
        _explosion.Play();
        _animator.SetTrigger("Shoot");
        _cooldawn.Start();
        var cannonball = _pool.Get();
        cannonball.Transform.position = _gun.position;
        cannonball.Launch(Aim);
    }
    private void Awake()
    {
        Init();
    }
}
