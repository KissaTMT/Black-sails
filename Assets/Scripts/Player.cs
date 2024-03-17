using Entities.Ships;
using UnityEngine;
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
    }
    public void Init()
    {
        Ship = GetComponent<Ship>();
        Ship.Init();
    }
    private void OnDisable()
    {
        _input.OnMovementAction -= SetDirection;
    }
    private void SetDirection(Vector2 direction) => _inputDirection = direction;
    private void FixedUpdate()
    {
        Ship.Swim(_inputDirection);
    }
}
