using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Radar : IDisposable
{
    public IReadOnlyList<RadarTarget> Targets => _targets;
    public Transform Transform => _transform;

    private MonoBehaviour _owner;
    private int _radius;
    private float _delay;

    private Coroutine _main;
    private Transform _transform;

    private List<RadarTarget> _targets;
    private Collider2D[] _colliders;
    public Radar(MonoBehaviour owner, int radius, float delay = 1)
    {
        _owner = owner;
        _radius = radius;
        _delay = delay;

        _transform = _owner.GetComponent<Transform>();
        _targets = new List<RadarTarget>(16);
        _colliders = new Collider2D[_targets.Capacity];
    }
    public void Start() => _main = _owner.StartCoroutine(SearchRoutine());
    public void Stop() => _owner.StopCoroutine(_main);
    public List<RadarTarget> Search()
    {
        var count = Physics2D.OverlapCircleNonAlloc(_transform.position, _radius, _colliders);

        if (count > 0)
        {
            for (var i = 0; i < count; i++)
            {
                var item = new RadarTarget(_colliders[i]);
                if (item.Transform.Equals(_transform) || _targets.Contains(item)) continue;
                _targets.Add(item);
            }
        }
        return _targets;
    }
    public void Dispose() => Stop();
    public bool TryGetIntersection(Vector2 start, Vector2 end, (Vector2 min, Vector2 max) offsets, out Vector2 result)
    {
        var topRight = new Vector2(offsets.max.x, offsets.max.y);
        var topLeft = new Vector2(offsets.min.x, offsets.max.y);
        var bottomRight = new Vector2(offsets.max.x, offsets.min.y);
        var bottomLeft = new Vector2(offsets.min.x, offsets.min.y);

        var hasIntersection = false;
        var intersection = Vector2.positiveInfinity;

        void AddIntersectPoint(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
        {
            hasIntersection = true;
            CalculateIntersectPoint(a, b, c, d, out var point);
            if(Vector2.Distance(a,point) < Vector2.Distance(a,intersection)) intersection = point;
        }

        if (IsIntersectSegment(start, end, topRight, bottomRight)) AddIntersectPoint(start,end, topRight, bottomRight);
        if (IsIntersectSegment(start, end, topLeft, bottomLeft)) AddIntersectPoint(start, end, topLeft, bottomLeft);
        if (IsIntersectSegment(start, end, topRight, topLeft)) AddIntersectPoint(start, end, topRight, topLeft);
        if (IsIntersectSegment(start, end, bottomRight, bottomLeft)) AddIntersectPoint(start, end, bottomRight, bottomLeft);

        if (hasIntersection)
        {
            result = intersection;
            return true;
        }
        else
        {
            result = Vector2.positiveInfinity;
            return false;
        }
    }
    private IEnumerator SearchRoutine()
    {
        var delay = new WaitForSeconds(_delay);

        while (true)
        {
            Search();
            yield return delay;
        }
    }
    private bool IsIntersectProjections(float min1, float max1, float min2, float max2)
    {
        if (min1 > max1)
        {
            var t = min1;
            min1 = max1;
            max1 = t;
        }
        if (min2 > max2)
        {
            var t = min2;
            min2 = max2;
            max2 = t;
        }
        return Mathf.Max(min1, min2) <= Mathf.Min(max1, max2);
    }
    private bool IsIntersectSegmentProjections(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
    {
        return IsIntersectProjections(a.x, b.x, c.x, d.x) && IsIntersectProjections(a.y, b.y, c.y, d.y);
    }
    private Vector2 CalculateIntersectPoint(Vector2 a, Vector2 b, Vector2 c, Vector2 d, out Vector2 result)
    {
        return result = a + (b - a) * ((Vector3.Cross(d - c, c).z + Vector3.Cross(a, d - c).z) / Vector3.Cross(d - c, b - a).z);
    }
    private bool IsIntersectSegment(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
    {
        if (!IsIntersectSegmentProjections(a, b, c, d)) return false;

        var ab = b - a;
        var ac = c - a;
        var ad = d - a;

        var cd = d - c;
        var ca = a - c;
        var cb = b - c;

        var cross_abac = Vector3.Cross(ab, ac).z;
        var cross_abad = Vector3.Cross(ac, ad).z;
        var cross_cdca = Vector3.Cross(cd, ca).z;
        var cross_cdcb = Vector3.Cross(cb, cb).z;

        return (cross_abac <= 0 && cross_abad >= 0 || cross_abac >= 0 && cross_abad <= 0) && (cross_cdca <= 0 && cross_cdcb >= 0 || cross_cdca >= 0 && cross_cdcb <= 0);
    }
}
