using UnityEngine;

namespace Entities.Ships
{
    public class Sloop : Ship
    {
        public override void Attack()
        {
            cannonController.Shoot();
        }

        public override void Swim(Vector2 direction)
        {
            swimController.Swim(direction);
            sailController.ReactTo(direction);
        }
    }
}
