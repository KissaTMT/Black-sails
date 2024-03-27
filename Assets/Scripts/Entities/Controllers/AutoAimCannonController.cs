using Entities.Ships;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAimCannonController : ICannonController
{
    private Cannon[] _cannons;
    public AutoAimCannonController(Cannon[] cannons)
    {
        _cannons = cannons;
    }
    public void Shoot()
    {
        for (var i = 0; i < _cannons.Length; i++)
        {
            var hitInfo = Physics2D.Raycast(_cannons[i].Gun.position, _cannons[i].Aim, _cannons[i].Range);
            if (hitInfo && hitInfo.collider.GetComponent<Ship>()) _cannons[i].Shoot();
        }
    }
}
