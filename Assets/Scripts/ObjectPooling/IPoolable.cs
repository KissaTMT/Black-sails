using System;

public interface IPoolable
{
    public event Action OnRelease;
}
