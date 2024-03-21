using UnityEngine;

namespace Entities.Ships
{
    public abstract class Ship : Entity
    {
        protected Cannon[] cannons;

        protected ISwimController swimController;

        protected SwimStats swimStats;


        [SerializeField] private SwimConfig _swimConfig;

        public override void Init()
        {
            base.Init();

            swimStats = new SwimStats(_swimConfig);
            swimController = new PhysicsSwimController(transform, rigidbody, swimStats);

            cannons = GetComponentsInChildren<Cannon>();
        }
        public abstract void Swim(Vector2 direction);
        public abstract void Attack();
    }
}