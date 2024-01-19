using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDashable
{
    public float DashSpeed { get; }
    public float DashTime { get; }
    public float Reload { get; }
    public float StaminaPrice { get; }

}
