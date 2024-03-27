using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UserAimCannonController : ICannonController
{
    private List<Cannon> _cannonsOnLeftSide;
    private List<Cannon> _cannonsOnRightSide;

    private Camera _cameraMain;
    private Vector2 _aimPosition;
    public UserAimCannonController(Cannon[] cannons)
    {
        _cameraMain = Camera.main;
        _cannonsOnLeftSide = new List<Cannon>();
        _cannonsOnRightSide = new List<Cannon>();

        for(var i=0;i<cannons.Length;i++)
        {
            var cannon = cannons[i];
            if (cannon.Transform.localPosition.x > 0) _cannonsOnRightSide.Add(cannon);
            else _cannonsOnLeftSide.Add(cannon);
        }
    }
    public void Shoot()
    {
        if (_aimPosition.x > 0) Shoot(_cannonsOnRightSide);
        else Shoot(_cannonsOnLeftSide);
    }
    private void Shoot(List<Cannon> cannons)
    {
        for(var i=0;i<cannons.Count;i++)
        {
            cannons[i].Shoot();
        }
    }
}
