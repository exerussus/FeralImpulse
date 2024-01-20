
using Source.EasyECS;
using Source.Scripts.MonoBehaviours.Abstractions;
using UnityEngine;

public class SleeperTest : EasyMonoBehaviour, IEntityObject, ISleepable
{
    [SerializeField] private int entity;
    [SerializeField] private Vector3 homePosition;
    [SerializeField] private float sleepTime;
    [SerializeField] private float wakeupTime;
    [SerializeField] private Collider2D unitCollider;
    public int Entity => entity;
    public Transform Transform => transform;
    public Collider2D Collider => unitCollider;
    public Vector3 HomePosition => homePosition;
    public float SleepTime => sleepTime;
    public float WakeupTime => wakeupTime;
    public void InitializeEntity(int value)
    {
        
    }
}
