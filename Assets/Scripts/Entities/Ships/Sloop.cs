using UnityEngine;

namespace Entities.Ships
{
    public class Sloop : Ship
    {
        [SerializeField] private Transform _sail;
        private SailController _sailController;
        public override void Init()
        {
            base.Init();
            _sailController = new SailController(_sail);
        }
        public override void Attack()
        {
            Debug.Log(GetType() + " " + "Attack");
        }

        public override void Swim(Vector2 direction)
        {
            swimController.Swim(direction);
            _sailController.ReactTo(direction);
        }
    }
}
