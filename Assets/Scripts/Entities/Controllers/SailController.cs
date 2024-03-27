using UnityEngine;

public class SailController
{
    private Sail[] _sails;
    public SailController(Sail[] sails)
    {
        _sails = sails;
    }
    public void ReactTo(Vector2 direction)
    {
        for (var i = 0; i < _sails.Length; i++)
        {
            var sail = _sails[i];

            if (!sail.IsStatic) sail.Rotate(direction);

            if (direction.y != 0)
            {
                if (direction.y > 0) sail.Increase();
                else sail.Decrease();
            }
        }
    }
}
