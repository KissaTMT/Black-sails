using Cinemachine;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private Transform _target;

    [Inject]
    public void Construct(Player player)
    {
        _target = player.Ship.Transform;
    }

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _virtualCamera.Follow = _target;
    }
}
