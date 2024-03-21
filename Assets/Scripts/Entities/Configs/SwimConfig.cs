using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/SwimConfig")]
public class SwimConfig : ScriptableObject
{
    public float MaxSpeed;
    public float Acceleration;
    public float Deceleration;
    public float RotationSpeed;
}
