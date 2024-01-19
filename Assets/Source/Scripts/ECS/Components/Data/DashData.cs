using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

/// <summary>
/// Dash parametrs: Time and speed init
/// </summary>
public struct DashData : IEcsData<IDashable>
{
    public float Time;
    public float Speed;
    public float Reload;
    public float StaminaPrice;

    public void InitializeValues(IDashable dashable)
    {
        Time = dashable.DashTime;
        Speed = dashable.DashSpeed;
        Reload = dashable.Reload;
        StaminaPrice = dashable.StaminaPrice;
    }
}
