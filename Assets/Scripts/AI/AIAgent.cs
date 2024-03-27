using Entities.Ships;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    public Ship Ship { get; private set; }
    private List<Transform> _targets;
    public void Init()
    {
        Ship = GetComponent<Ship>();
        _targets = new List<Transform>();   
    }
    private void Attack()
    {
        Ship.Attack();
    }
    private void Awake()
    {
        Init();
    }
    private void Start()
    {
        StartCoroutine(SearchTargetsRoutine());
    }
    private void FixedUpdate()
    {
        Ship.Swim((_targets[0].transform.position - Ship.Transform.position).normalized);
    }
    private IEnumerator SearchTargetsRoutine()
    {
        var delay = new WaitForSeconds(1);
        var targetColliders = new Collider2D[4];
        while (true)
        {
            var count = Physics2D.OverlapCircleNonAlloc(Ship.Transform.position, 40, targetColliders);
            if (count > 0)
            {
                for (var i = 0; i < count; i++)
                {
                    var target = targetColliders[i];
                    if (target.transform.Equals(Ship.Transform) || _targets.Contains(target.transform)) continue;
                    _targets.Add(target.transform);
                }
            }
            yield return delay;
        }
    }
}
