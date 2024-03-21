using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Entity : MonoBehaviour
{
    public Transform Transform => transform;
    public Rigidbody2D Rigidbody => rigidbody;

    protected new Transform transform;
    protected new Rigidbody2D rigidbody;

    public virtual void Init()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Awake()
    {
        Init();
    }
}
