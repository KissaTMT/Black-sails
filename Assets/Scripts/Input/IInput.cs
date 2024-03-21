using System;
using UnityEngine;

public interface IInput
{
    public event Action<Vector2> OnMovementAction;
    public event Action OnAttackAction;
    public event Action<bool> OnContiniousAttackAction;
}
