using UnityEngine;

public interface ISwimController
{
    public float CurrentSpeed { get; }
    public void Swim(Vector2 direction);
}