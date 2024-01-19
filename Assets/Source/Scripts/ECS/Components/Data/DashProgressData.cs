using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

/// <summary>
/// dash information in progress
/// </summary>

/// /// <param name="float">TimeRemaining</param>
/// /// <param name="Vector2">Direction</param>
public struct DashProgressData : IEcsData<DashData, Vector2>
{
    public float TimeRemaining;
    public Vector2 Direction;

    public void InitializeValues(DashData dashData, Vector2 direction)
    {
        TimeRemaining = dashData.Time;
        Direction = direction;
    }
}
