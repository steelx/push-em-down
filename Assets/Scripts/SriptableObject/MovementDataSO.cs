using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Agent/MovementData")]
public class MovementDataSO : ScriptableObject
{
    [Range(0f, 10)]
    public float MaxSpeed = 5f;

    [Range(0.1f, 100)]
    public float Acceleration = 50, Deacceleration = 50;

    [Range(0.1f, 50)]
    public float JumpForce = 3f;

    [Range(0.1f, 50)]
    public float MouseSensiX = 3f;
    [Range(0.1f, 50)]
    public float MouseSensiY = 3f;
    [Range(-90f, 90f)]
    public float MaximumAngleX = 90f;
    [Range(-90f, 90f)]
    public float MinimumAngleX = -90f;
}
