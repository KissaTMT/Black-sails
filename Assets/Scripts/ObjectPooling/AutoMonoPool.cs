using System;
using UnityEngine;

public class AutoMonoPool<T> : MonoPool<T>, IDisposable where T : MonoBehaviour, IPoolable
{
    public AutoMonoPool(Func<T> onCreate, int capacity = 32) : base(onCreate, capacity) { }
    public AutoMonoPool(Func<T> onCreate, Action<T> onDestroy, int capacity = 32) : base(onCreate, onDestroy, capacity) { }
    public AutoMonoPool(T item, int capacity = 32) : base(item, capacity) { }
    public new AutoMonoPool<T> DoNotExpand() => (AutoMonoPool<T>)base.DoNotExpand();

    public void Dispose()
    {
        foreach (var item in pool)
        {
            item.OnRelease -= () => Release(item);
        }
    }

    protected override T Create()
    {
        var createdItem = base.Create();
        createdItem.OnRelease += () => Release(createdItem);
        return createdItem;
    }
}