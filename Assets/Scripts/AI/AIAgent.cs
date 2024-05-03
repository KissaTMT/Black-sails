using Entities.Ships;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    private const int RODAR_RADIUS = 40;
    private const float RODAR_DELAY = 2;
    public Ship Ship { get; private set; }

    [SerializeField] private Vector2 _targetPosition;
    private Radar _radar;
    private Navigator _navigator;
    public void Init()
    {
        Ship = GetComponent<Ship>();
        _radar = new Radar(this, RODAR_RADIUS, RODAR_DELAY);
        _navigator = new Navigator(GetComponent<CapsuleCollider2D>().size,_radar);
    }
    private void Attack()
    {
        Ship.Attack();
    }
    private void Awake()
    {
        Init();
    }
    private void Start()
    {
        _radar.Start();
    }
    private void FixedUpdate()
    {
        Ship.Swim(GetDirectionToTarget(_navigator.GetNextPointToTarget(Ship.Transform.position, _targetPosition)));
        if (Vector2.Distance(Ship.Transform.position, _radar.Targets[0].Position) <= Ship.CannonsRange) Attack();
    }
    private Vector2 GetDirectionToTarget(Vector3 position) => new Vector2(Vector2.SignedAngle(position - Ship.Transform.position, Ship.Transform.up), 0);

    private Vector2 GetPointToCannonFire(Vector3 position)
    {
        var delta = position - Ship.Transform.position;

        (float left, float right) angle = (Vector2.SignedAngle(delta, -Ship.Transform.right), Vector2.SignedAngle(delta, Ship.Transform.right));

        return 2 * delta - delta.normalized * Ship.CannonsRange;
    }
}
