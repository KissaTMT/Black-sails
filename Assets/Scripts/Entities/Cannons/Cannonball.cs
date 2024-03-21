using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : Entity, IPoolable
{
    public event Action OnRelease;
    public void Launch(Vector2 direction)
    {
        StartCoroutine(FlightRoutine(direction));
    }
    private void OnDisable()
    {
        OnRelease?.Invoke();
    }
    private IEnumerator FlightRoutine(Vector2 direction)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            transform.position = Vector2.MoveTowards(transform.position, direction, 100 * Time.deltaTime);
            if (Vector2.Distance(transform.position, direction) < float.Epsilon) break;
        }
        gameObject.SetActive(false);
    }
}
