using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

/// <summary>
/// dash information in progress
/// </summary>

/// /// <param name="float">TimeRemaining</param>
/// /// <param name="Vector2">Direction</param>

public struct DashReloadData : IEcsData<float>
{
    public float TimeRemaining;

    public void InitializeValues(float timeRemaining)
    {
        TimeRemaining = timeRemaining;
    }
}
