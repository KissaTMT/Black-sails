using Entities.Ships;
using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    public Player Player { get; private set; }

    [SerializeField] private Ship _ship;
    public override void InstallBindings()
    {
        PlayerBinding();
    }
    private void PlayerBinding()
    {
        var ship = Container.InstantiatePrefab(_ship);
        Player = Container.InstantiateComponent<Player>(ship);
        Player.name = "Player";
        Player.Init();
        Container.Bind<Player>().FromInstance(Player);
    }
}
