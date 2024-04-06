using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour, IPoolable
{
    public event Action OnRelease;
    public Transform Transform => _transform;

    [SerializeField] private ParticleSystem _explositon;
    [SerializeField] private float _flightSpeed = 10;

    private Transform _transform;
    private Rigidbody2D _rigidbody;

    private Vector2 _direction;
    public void Launch(Vector2 direction)
    {
        _direction = direction;
    }
    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        StartCoroutine(DeactiveRoutine());
    }
    private void OnDisable()
    {
        OnRelease?.Invoke();
    }
    private void FixedUpdate()
    {
        NonPhysicsFlight();
    }
    private void PhysicsFlight()
    {
        _rigidbody.MovePosition(_rigidbody.position + _direction * _flightSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, _direction) < float.Epsilon) gameObject.SetActive(false);
    }
    private void NonPhysicsFlight()
    {
        var target = transform.position + (Vector3)_direction;
        transform.position = Vector2.MoveTowards(transform.position, target, _flightSpeed * 10 * Time.deltaTime);
        if (Vector2.Distance(transform.position, target) < float.Epsilon) gameObject.SetActive(false);
    }
    private IEnumerator DeactiveRoutine()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Instantiate(_explositon, _transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
