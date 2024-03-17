using System;
using UnityEngine;

public class InputHandler : IDisposable, IInput
{
    public event Action<Vector2> OnMovementAction;

    private InputActions _inputs;

    public InputHandler()
    {
        _inputs = new InputActions();

        _inputs.Enable();
        _inputs.Gameplay.Movement.performed += movement => OnMovementAction?.Invoke(movement.ReadValue<Vector2>());
        _inputs.Gameplay.Movement.canceled += movement => OnMovementAction?.Invoke(Vector2.zero);
    }
    public void Dispose()
    {
        _inputs.Disable();
        _inputs.Gameplay.Movement.performed -= movement => OnMovementAction?.Invoke(movement.ReadValue<Vector2>());
        _inputs.Gameplay.Movement.canceled -= movement => OnMovementAction?.Invoke(Vector2.zero);
    }
}
