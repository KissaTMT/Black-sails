using Entities.Ships;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Player : MonoBehaviour
{
    public Ship Ship {get; private set;}

    private IInput _input;

    private Vector2 _inputDirection;
    [Inject]
    public void Construct(IInput input)
    {
        _input = input;

        _input.OnMovementAction += SetDirection;
        _input.OnAttackAction += Attack;
    }
    public void Init()
    {
        Ship = GetComponent<Ship>();
    }
    private void OnDisable()
    {
        _input.OnMovementAction -= SetDirection;
        _input.OnAttackAction -= Attack;
    }
    private void SetDirection(Vector2 direction) => _inputDirection = direction;

    private void Attack()
    {
        Ship.Attack();
    }
    private void FixedUpdate()
    {
        Ship.Swim(_inputDirection);
    }
}
