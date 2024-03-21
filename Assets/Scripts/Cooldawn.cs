using UnityEngine;

public class Cooldawn
{
    private float _cooldawnTime;
    private float _nextTime;

    public Cooldawn(float cooldawnTime) => _cooldawnTime = cooldawnTime;
    public void Start() => _nextTime = Time.time + _cooldawnTime;
    public bool IsReady() => Time.time >= _nextTime;
}
