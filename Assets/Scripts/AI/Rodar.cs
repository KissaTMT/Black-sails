using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rodar
{
    public IReadOnlyList<Collider2D> Targets => _targets;

    private MonoBehaviour _owner;
    private int _radius;
    private float _delay;

    private Coroutine _main;
    private Transform _transform;

    private List<Collider2D> _targets;
    public Rodar(MonoBehaviour owner, int radius, float delay = 1)
    {
        _owner = owner;
        _radius = radius;
        _delay = delay;

        _transform = _owner.GetComponent<Transform>();
        _targets = new List<Collider2D>(16);
    }
    public void Start() => _main = _owner.StartCoroutine(Search());
    public void Stop() => _owner.StopCoroutine(_main);
    private IEnumerator Search()
    {
        var results = new Collider2D[_targets.Capacity];
        var delay = new WaitForSeconds(_delay);

        while (true)
        {
            var count = Physics2D.OverlapCircleNonAlloc(_transform.position, _radius, results);

            if (count > 0) _targets.AddRange(results.Where(item => item && !item.transform.Equals(_transform) && !_targets.Contains(item)));

            yield return delay;
        }
    }
}
