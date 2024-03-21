using System;
using UnityEngine;

public class InputHandler : IDisposable, IInput
{
    public event Action<Vector2> OnMovementAction;
    public event Action OnAttackAction;
    public event Action<bool> OnContiniousAttackAction;

    private InputActions _inputs;

    public InputHandler()
    {
        _inputs = new InputActions();

        _inputs.Enable();
        _inputs.Gameplay.Movement.performed += movement => OnMovementAction?.Invoke(movement.ReadValue<Vector2>());
        _inputs.Gameplay.Movement.canceled += movement => OnMovementAction?.Invoke(Vector2.zero);

        _inputs.Gameplay.Attack.performed += attack => OnAttackAction?.Invoke();

        _inputs.Gameplay.Attack.performed += attack => OnContiniousAttackAction?.Invoke(true);
        _inputs.Gameplay.Attack.canceled += attack => OnContiniousAttackAction?.Invoke(false);
    }
    public void Dispose()
    {
        _inputs.Disable();
        _inputs.Gameplay.Movement.performed -= movement => OnMovementAction?.Invoke(movement.ReadValue<Vector2>());
        _inputs.Gameplay.Movement.canceled -= movement => OnMovementAction?.Invoke(Vector2.zero);

        _inputs.Gameplay.Attack.performed -= attack => OnAttackAction?.Invoke();

        _inputs.Gameplay.Attack.performed -= attack => OnContiniousAttackAction?.Invoke(true);
        _inputs.Gameplay.Attack.canceled -= attack => OnContiniousAttackAction?.Invoke(false);

    }
}
