using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    private Vector2 _size;
    private Radar _radar;

    private Dictionary<Vector2, (RadarTarget target, Vector2 point)> _intersects;
    public Navigator(Vector2 size, Radar radar)
    {
        _size = size;
        _radar = radar;

        _intersects = new Dictionary<Vector2, (RadarTarget target, Vector2 point)>();
    }
    public Vector2 GetNextPointToTarget(Vector2 source, Vector2 target)
    {
        var delta = target - source;
        var normal0Delta = new Vector2(delta.y, -delta.x).normalized * _size.x;
        var normal1Delta = new Vector2(-delta.y, delta.x).normalized * _size.x;

        _intersects.Clear();

        GetIntersectsWithTargetsNonAloc(source, target);
        GetIntersectsWithTargetsNonAloc(source + normal0Delta, target + normal0Delta);
        GetIntersectsWithTargetsNonAloc(source + normal1Delta, target + normal1Delta);

        //if(_intersects.Count == 0) return target;

        var nearestIntersect = GetNearestIntersect(source);

        Debug.DrawLine(source, target, _intersects.ContainsKey(source) ? Color.red : Color.green);
        Debug.DrawLine(source + normal0Delta, target + normal0Delta, _intersects.ContainsKey(source + normal0Delta) ? Color.red : Color.green);
        Debug.DrawLine(source + normal1Delta, target + normal1Delta, _intersects.ContainsKey(source + normal1Delta) ? Color.red : Color.green);

        return source;
    }
    private (RadarTarget target, Vector2 point) GetNearestIntersect(Vector2 source)
    {
        var target = _radar.Targets[0];
        var point = Vector2.zero;
        var min = float.MaxValue;
        foreach (var intersect in _intersects.Values)
        {
            var deltaSqrMagnitude = (intersect.point - source).sqrMagnitude;
            if (deltaSqrMagnitude < min)
            {
                min = deltaSqrMagnitude;
                target = intersect.target;
                point = intersect.point;
            }
        }
        return (target, point);
    }
    private void GetIntersectsWithTargetsNonAloc(Vector2 source, Vector2 target)
    {
        var min = float.MaxValue;
        for (var i = 0; i < _radar.Targets.Count; i++)
        {
            var radarTarget = _radar.Targets[i];
            if (_radar.TryGetIntersection(source, target, radarTarget.Bounds, out var point))
            {
                var sqrMagnitude = (point - source).sqrMagnitude;
                if (sqrMagnitude < min)
                {
                    _intersects[source] = (radarTarget, point);
                    min = sqrMagnitude;
                }
            }
        }
    }
}
