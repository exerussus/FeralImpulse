using Source.EasyECS.Interfaces;
using Source.Scripts.MonoBehaviours.Abstractions;

namespace Source.Scripts.ECS.Components.Data
{
    /// <summary>
    /// explosion data
    /// </summary>

    public struct ExplosionData : IEcsData<IExplosible>
    {
        public float Radius;
        public float Impulse;
    
        public void InitializeValues(IExplosible argument)
        {
            Radius = argument.Radius;
            Impulse = argument.Impulse;
        }
    }
}