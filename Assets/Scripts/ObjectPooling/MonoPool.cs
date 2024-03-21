using System;
using UnityEngine;

public class MonoPool<T> : Pool<T> where T : MonoBehaviour
{
    public MonoPool(Func<T> onCreate, int capacity = 32) : base(onCreate, GetAction, ReleaseAction, DestroyAction, capacity) { }
    public MonoPool(Func<T> onCreate,Action<T> onDestroy, int capacity = 32) : base(onCreate, GetAction, ReleaseAction, onDestroy, capacity) { }
    public MonoPool(T item, int capacity = 32) : base(() => CreateAction(item), GetAction, ReleaseAction, DestroyAction, capacity) { }
    public new MonoPool<T> DoNotExpand() => (MonoPool<T>)base.DoNotExpand();
    private static T CreateAction(T item) => UnityEngine.Object.Instantiate(item);
    private static void GetAction(T item) => item.gameObject.SetActive(true);
    private static void ReleaseAction(T item) => item.gameObject.SetActive(false);
    private static void DestroyAction(T item) => GameObject.Destroy(item);
}
