using System;
using System.Collections.Generic;
using ModestTree;

public class Pool<T>
{
    public int Capacity => _capacity;
    public int Count => pool.Count;

    protected Stack<T> pool;

    private Func<T> OnCreate;
    private Action<T> OnGet;
    private Action<T> OnRelease;
    private Action<T> OnDestroy;

    private List<T> _activeObjects;

    private int _capacity;
    private bool _autoExpand;

    public Pool(Func<T> onCreate, Action<T> onGet, Action<T> onRelease, Action<T> onDestroy,int capacity = 32)
    {
        pool = new Stack<T>(capacity);
        _activeObjects = new List<T>(capacity);

        OnCreate = onCreate;
        OnGet = onGet;
        OnRelease = onRelease;
        OnDestroy = onDestroy;

        _capacity = capacity;
        _autoExpand = true;

        Fill();
    }
    public T Get()
    {
        if (pool.IsEmpty())
        {
            if (_autoExpand) Create();
            else throw new Exception("Pool is empty! Use TryGet() for don't auto expand pool");
        }
        return GetLastItem();
    }
    public bool TryGet(out T item)
    {
        item = default;
        if (!pool.IsEmpty()) item = GetLastItem();
        return item.Equals(default) ? false : true;
    }
    public void Release(T item)
    {
        _activeObjects.Remove(item);
        OnRelease(item);
        pool.Push(item);
    }
    public void ReleaseAll()
    {
        foreach(var item in _activeObjects.ToArray())
        {
            Release(item);
        }
    }
    public void Destroy(T item)
    {
        _activeObjects.Remove(item);
        OnDestroy(item);
    }
    public void Clear()
    {
        ReleaseAll();

        for (var i = 0; i < pool.Count; i++)
        {
            var item = pool.Pop();
            OnDestroy(item);
        }
    }
    public Pool<T> DoNotExpand()
    {
        _autoExpand = false;
        return this;
    }
    protected virtual T Create()
    {
        var item = OnCreate();
        Release(item);
        return item;
    }
    private T GetLastItem()
    {
        var item = pool.Pop();
        _activeObjects.Add(item);
        OnGet(item);
        return item;
    }
    private void Fill()
    {
        for (var i = 0; i < _capacity; i++)
        {
            Create();
        }
    }
}