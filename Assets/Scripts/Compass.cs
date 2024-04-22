using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Compass : MonoBehaviour
{
    private Player _player;

    private RectTransform _rectTransform;
    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    private void FixedUpdate()
    {
        _rectTransform.localRotation = Quaternion.Lerp(_rectTransform.localRotation,Quaternion.Euler(-_player.Ship.Transform.rotation.eulerAngles), Time.deltaTime);
    }

}
