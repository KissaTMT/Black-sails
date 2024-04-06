using Entities.Ships;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    private const int RODAR_RADIUS = 40;
    private const float RODAR_DELAY = 2;
    public Ship Ship { get; private set; }

    private Rodar _rodar;
    private int _target;

    public void Init()
    {
        Ship = GetComponent<Ship>();
        _rodar = new Rodar(this, RODAR_RADIUS, RODAR_DELAY);
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
        _rodar.Start();
        _target = Random.Range(0, _rodar.Targets.Count);
    }
    private void FixedUpdate()
    {
        var position = _rodar.Targets[_target].transform.position;
        Vector2 direction;
        if (Vector2.Distance(position, Ship.Transform.position) > Ship.CannonsRange) direction = GetDirectionToTarget(position);
        else direction = GetDirectionToTarget(GetPointToCannonFire(position));
        Ship.Swim(direction);
        if (Vector2.Distance(Ship.Transform.position, position) <= Ship.CannonsRange) Attack();
    }
    private Vector2 GetDirectionToTarget(Vector3 position) => new Vector2(Vector2.SignedAngle(position - Ship.Transform.position, Ship.Transform.up), 1);

    private Vector2 GetPointToCannonFire(Vector3 position)
    {
        var delta = position - Ship.Transform.position;

        (float left, float right) angle = (Vector2.SignedAngle(delta, -Ship.Transform.right), Vector2.SignedAngle(delta, Ship.Transform.right));

        return 2 * delta - delta.normalized * Ship.CannonsRange;
    }
}
