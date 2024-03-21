using UnityEngine;

public class Cannon : Entity
{
    [SerializeField] private Transform _gun;
    [SerializeField] private Transform _target;

    [SerializeField] private Cannonball _cannonballPrefab;
    [SerializeField] private float _cooldawnTime;
    [SerializeField] private ParticleSystem _explosion;

    private Animator _animator;

    private AutoMonoPool<Cannonball> _pool;
    private Cooldawn _cooldawn;
    public override void Init()
    {
        base.Init();
        _pool = new AutoMonoPool<Cannonball>(_cannonballPrefab, 2);
        _animator = GetComponent<Animator>();
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
        var cannonball = _pool.Get();
        cannonball.Transform.position = _gun.position;
        cannonball.Launch(_target.position);
        _cooldawn.Start();
    }
}
