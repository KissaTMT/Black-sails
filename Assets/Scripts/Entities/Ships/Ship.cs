using UnityEngine;

namespace Entities.Ships
{
    public abstract class Ship : MonoBehaviour
    {
        public Transform Transform => transform;

        protected Cannon[] cannons;
        protected Sail[] sails;

        protected new Transform transform;

        protected new Rigidbody2D rigidbody;

        protected ISwimController swimController;
        protected ICannonController cannonController;
        protected SailController sailController;

        protected SwimStats swimStats;


        [SerializeField] private SwimConfig _swimConfig;

        public virtual void Init()
        {
            transform = GetComponent<Transform>();
            rigidbody = GetComponent<Rigidbody2D>();

            cannons = GetComponentsInChildren<Cannon>();
            sails = GetComponentsInChildren<Sail>();

            swimStats = new SwimStats(_swimConfig);
            swimController = new PhysicsSwimController(transform, rigidbody, swimStats);
            sailController = new SailController(sails);
            cannonController = new AutoAimCannonController(cannons);
        }
        public abstract void Swim(Vector2 direction);
        public abstract void Attack();

        private void Awake()
        {
            Init();
        }
    }
}